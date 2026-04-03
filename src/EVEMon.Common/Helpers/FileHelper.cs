using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using EVEMon.Common.Extensions;

namespace EVEMon.Common.Helpers
{
    public static class FileHelper
    {
        private static bool? s_removeReadOnlyAttributes;
        private static readonly object s_removeReadOnlyAttributesLock = new object(); // Nullable<T> assignment is not atomic

        /// <summary>
        /// Opens a file, offering the user to retry if an exception occurs.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="allowIgnore"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">filename</exception>
        public static Stream OpenRead(string filename, bool allowIgnore)
        {
            filename.ThrowIfNull(nameof(filename));

            Uri uri = new Uri(filename);

            if (uri.IsFile && !File.Exists(uri.LocalPath))
                return null;

            // While problems happen and the user ask to retry...
            while (true)
            {
                try
                {
                    return new MemoryStream(File.ReadAllBytes(uri.LocalPath));
                }
                catch (UnauthorizedAccessException exc)
                {
                    ExceptionHandler.LogException(exc, true);

                    string message = exc.Message;
                    message += "\r\n\r\nEVEMon failed to read a file. " +
                               "You may have insufficient rights or a synchronization may be occuring. Choosing to " +
                               (allowIgnore ? "abort" : "cancel") + " will make EVEMon quit.";

                    DialogResult result = MessageBox.Show(message, @"Failed to read a file",
                        allowIgnore
                            ? MessageBoxButtons.AbortRetryIgnore
                            : MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Error);

                    // On abort, we quit the application
                    if (result == DialogResult.Abort || result == DialogResult.Cancel)
                    {
                        Application.Exit();
                        return null;
                    }

                    // The loop will begin again if the users asked to retry
                    if (result == DialogResult.Ignore)
                        return null;
                }
            }
        }

        /// <summary>
        /// Overwrites a destination file with the temporary file populated with content using the provided callback.
        /// This method will take care of the readonly attributes, prompting the user to EVEMon removing them.
        /// Finally, should a <see cref="UnauthorizedAccessException"/> occurs, EVEMon will display a readable message for the end-user.
        /// </summary>
        /// <param name="destFileName">The destination file name, it does not have to already exist</param>
        /// <param name="writeContentFunc"></param>
        /// <returns>
        /// False if the provided callback returned false or 
        /// if the user denied to remove the read-only attribute or 
        /// if he didn't have the permissions to write the file;
        /// true otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">destFileName or writeContentFunc</exception>
        public static async Task OverwriteOrWarnTheUserAsync(string destFileName,
            Func<Stream, Task<bool>> writeContentFunc)
        {
            destFileName.ThrowIfNull(nameof(destFileName));

            writeContentFunc.ThrowIfNull(nameof(writeContentFunc));

            string tempFileName = Path.GetTempFileName();
            try
            {
                using (FileStream fs = Util.GetFileStream(tempFileName, FileMode.Open))
                {
                    if (!await writeContentFunc(fs))
                        return;
                }

                CopyOrWarnTheUser(tempFileName, destFileName);
            }
            finally
            {
                DeleteFile(tempFileName);
            }
        }

        /// <summary>
        /// Overwrites a destination file with the provided source file by copying this one.
        /// This method will take care of the readonly attributes, prompting the user to EVEMon removing them.
        /// Finally, should a <see cref="UnauthorizedAccessException"/> occurs, EVEMon will display a readable message for the end-user.
        /// </summary>
        /// <param name="srcFileName">The source file name.</param>
        /// <param name="destFileName">The destination file name, it does not have to already exist</param>
        /// <returns>False if the user denied to remove the read-only attribute or if he didn't have the permissions to write the file; true otherwise.</returns>
        public static void CopyOrWarnTheUser(string srcFileName, string destFileName)
        {
            // While problems happen and the user ask to retry...
            while (true)
            {
                try
                {
                    // We need to make sure this file is not read-only
                    // If it is, this method will request the user the permission to automatically remove the readonly attributes
                    if (!EnsureWritable(destFileName))
                        return;

                    // Overwrite the file
                    CopyFile(srcFileName, destFileName);

                    // Ensures we didn't copied a read-only attribute, no permission required since the file has been overwritten
                    FileInfo destFile = new FileInfo(destFileName);
                    destFile.Refresh();
                    destFile.IsReadOnly = false;

                    // Quit the loop
                    return;
                }
                catch (UnauthorizedAccessException exc)
                {
                    ExceptionHandler.LogException(exc, true);

                    string message = exc.Message;
                    message += "\r\n\r\nEVEMon failed to save to a file. You may have " +
                        "insufficient rights or a synchronization may be active. " +
                        "Choosing to abort will make EVEMon quit.";

                    DialogResult result = MessageBox.Show(message, @"Failed to write over a file",
                        MessageBoxButtons.AbortRetryIgnore,
                        MessageBoxIcon.Error);

                    switch (result)
                    {
                        // On abort, we quit the application
                        case DialogResult.Abort:
                            Application.Exit();
                            return;
                        // The loop will begin again if the users asked to retry
                        case DialogResult.Ignore:
                            return;
                    }
                }
            }
        }

        /// <summary>
        /// Copies a file to another file.
        /// </summary>
        /// <param name="srcFileName">Name of the source file.</param>
        /// <param name="destFileName">Name of the destination file.</param>
        /// <returns></returns>
        private static void CopyFile(string srcFileName, string destFileName)
        {
            // Use a retry loop to handle transient IOException from concurrent file access
            const int maxRetries = 3;
            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                try
                {
                    using (Stream sourceStream = Util.GetFileStream(srcFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (Stream destStream = Util.GetFileStream(destFileName, FileMode.Create, FileAccess.Write))
                        sourceStream.CopyTo(destStream);
                    return;
                }
                catch (IOException) when (attempt < maxRetries - 1)
                {
                    // Brief delay before retry to let the other handle close
                    System.Threading.Thread.Sleep(100 * (attempt + 1));
                }
            }
        }

        /// <summary>
        /// Ensures the given file is writable.
        /// </summary>
        /// <param name="filename">The file to make writable.</param>
        /// <returns>False if file exists, is readonly and the user denied permission to make it writable; true otherwise.</returns>
        private static bool EnsureWritable(string filename)
        {
            FileInfo file = new FileInfo(filename);
            return !file.Exists || TryMakeWritable(file);
        }

        /// <summary>
        /// Try to make a file writable, requesting the user the right to remove the readonly attributes the first time
        /// </summary>
        /// <param name="file">The file to make writable. It must exists but does not have to be read-only</param>
        /// <returns>False if the file was readonly and the user denied permission to make it writable; true otherwise</returns>
        private static bool TryMakeWritable(FileInfo file)
        {
            // Return true if the file can be written to.
            if (!file.IsReadOnly)
                return true;

            // Return false if we're not allowed to remove the read-only attribute
            if (!HasPermissionToRemoveReadOnlyAttributes)
                return false;

            // Remove the attribute
            file.IsReadOnly = false;

            return true;
        }

        /// <summary>
        /// Request from the use the permission for EVEMon to remove the read-only attributes on its own files
        /// </summary>
        /// <returns>True if the user granted the rights to make its files writable, false otherwise</returns>
        private static bool HasPermissionToRemoveReadOnlyAttributes
        {
            get
            {
                lock (s_removeReadOnlyAttributesLock)
                {
                    // We request the user to allow us to remove the attributes
                    if (s_removeReadOnlyAttributes.HasValue)
                        return s_removeReadOnlyAttributes.Value;

                    // Prepare caption and text
                    string message = "EVEMon detected that some of its files are read-only, " +
                                     "preventing it to save its datas.\r\n\r\n";
                    message +=
                        "Choosing YES will allow EVEMon to remove the read-only attributes on its own files (only).\r\n";
                    message += "Choosing NO will force EVEMon to continue without writing its files. " +
                               "This can cause unexpected behaviours.\r\n\r\n";
                    message += "Note : if you restart EVEMon and it still encounters read-only files, " +
                               "you will be prompted again.";

                    // Display the message box
                    DialogResult result = MessageBox.Show(message, @"Allow EVEMon to make its files writable",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Error);

                    // User denied us the permission to make files writeable
                    s_removeReadOnlyAttributes = result != DialogResult.No;

                    // Returns the permission granted by the user
                    return s_removeReadOnlyAttributes.Value;
                }
            }
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public static bool DeleteFile(string filename)
        {
            bool success = false;
            try
            {
                File.Delete(filename);
                success = true;
            }
            catch (ArgumentException ex)
            {
                ExceptionHandler.LogException(ex, false);
            }
            catch (IOException ex)
            {
                ExceptionHandler.LogException(ex, false);
            }
            catch (UnauthorizedAccessException ex)
            {
                ExceptionHandler.LogException(ex, false);
            }

            return success;
        }
    }
}
