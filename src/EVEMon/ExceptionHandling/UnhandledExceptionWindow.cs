using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Constants;
using EVEMon.Common.Controls;
using EVEMon.Common.Data;
using EVEMon.Common.Extensions;
using EVEMon.Common.Helpers;
using EVEMon.Common.Properties;

namespace EVEMon.ExceptionHandling
{
    /// <summary>
    /// Form to handle the display of the error report for easy bug reporting.
    /// </summary>
    public partial class UnhandledExceptionWindow : EVEMonForm
    {
        private readonly Exception m_exception;


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UnhandledExceptionWindow"/> class.
        /// </summary>
        private UnhandledExceptionWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnhandledExceptionWindow"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public UnhandledExceptionWindow(Exception exception)
            : this()
        {
            m_exception = exception;
        }

        #endregion


        #region Inherited Events

        /// <summary>
        /// Loads resources, generates the report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnhandledExceptionWindow_Load(object sender, EventArgs e)
        {
            WhatCanYouDoLabel.Font = new Font(WhatCanYouDoLabel.Font.FontFamily, 10F);

            SetBugImage();

            BuildExceptionMessage();
        }

        #endregion


        #region Content Management

        /// <summary>
        /// Builds the exception message.
        /// </summary>
        private void BuildExceptionMessage()
        {
            EveMonClient.StopTraceLogging();
            try
            {
                StringBuilder exceptionReport = new StringBuilder();
                exceptionReport.Append("EVEMon Version: ").AppendLine(EveMonClient.
                    FileVersionInfo.FileVersion);
                exceptionReport.Append(".NET Runtime Version: ").AppendLine(Environment.
                    Version.ToString());
                exceptionReport.Append("Operating System: ").AppendLine(Environment.OSVersion.
                    VersionString.ToString());
                exceptionReport.Append("Executable Path: ").AppendLine(Environment.CommandLine);
                exceptionReport.AppendLine(GetRecursiveStackTrace(m_exception)).AppendLine();
                exceptionReport.AppendLine(GetDatafileReport()).AppendLine();
                exceptionReport.AppendLine("Diagnostic Log:").AppendLine(GetTraceLog().Trim());
                TechnicalDetailsTextBox.Text = exceptionReport.ToString();
            }
            catch (InvalidOperationException ex)
            {
                ExceptionHandler.LogException(ex, true);
                TechnicalDetailsTextBox.Text = Properties.Resources.ErrorBuildingError;
            }
        }

        /// <summary>
        /// Gets the trace log.
        /// </summary>
        /// <returns></returns>
        private static string GetTraceLog()
        {
            string trace;
            FileStream traceStream = null;
            try
            {
                traceStream = Util.GetFileStream(EveMonClient.TraceFileNameFullPath,
                    FileMode.Open, FileAccess.Read);
                using (StreamReader traceReader = new StreamReader(traceStream))
                {
                    traceStream = null;
                    trace = traceReader.ReadToEnd();
                }
            }
            catch (IOException ex)
            {
                ExceptionHandler.LogException(ex, true);
                trace = Properties.Resources.ErrorNoTraceFile;
            }
            catch (UnauthorizedAccessException ex)
            {
                ExceptionHandler.LogException(ex, true);
                trace = Properties.Resources.ErrorNoTraceFile;
            }
            finally
            {
                traceStream?.Dispose();
            }
            return trace;
        }

        /// <summary>
        /// Sets the bug image.
        /// </summary>
        private void SetBugImage()
        {
            try
            {
                Bitmap bug = Resources.Bug;

                int oHeight = bug.Height;
                int oWidth = bug.Width;
                if (bug.Height <= BugPictureBox.ClientSize.Height)
                    return;

                double scale = (double)BugPictureBox.ClientSize.Height / bug.Height;
                oHeight = (int)(oHeight * scale);
                oWidth = (int)(oWidth * scale);
                BugPictureBox.Image = new Bitmap(bug, new Size(oWidth, oHeight));
                BugPictureBox.ClientSize = new Size(oWidth, oHeight);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogRethrowException(ex);
                throw;
            }
        }

        /// <summary>
        /// Gets the datafile report.
        /// </summary>
        /// <value>The datafile report.</value>
        private static string GetDatafileReport()
        {
            StringBuilder datafileReport = new StringBuilder();

            try
            {
                datafileReport.AppendLine("Datafile report:");

                foreach (string datafile in Datafile.GetFilesFrom(EveMonClient.EVEMonDataDir,
                    Datafile.DatafilesExtension))
                {
                    FileInfo info = new FileInfo(datafile);
                    Datafile file = new Datafile(Path.GetFileName(datafile));

                    datafileReport.AppendLine($"  {info.Name} ({info.Length / 1024}KiB - {file.MD5Sum})");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                ExceptionHandler.LogException(ex, true);
                datafileReport.AppendLine(Properties.Resources.ErrorNoDataFile);
            }

            return datafileReport.ToString();
        }

        /// <summary>
        /// Gets the recursive stack trace.
        /// </summary>
        /// <value>The recursive stack trace.</value>
        internal static string GetRecursiveStackTrace(Exception exception)
        {
            StringBuilder stackTraceBuilder = new StringBuilder();
            Exception ex = exception;

            stackTraceBuilder.Append(ex).AppendLine();

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;

                stackTraceBuilder.AppendLine().Append(ex).AppendLine();
            }

            // Remove project local path from message
            return stackTraceBuilder.ToString().RemoveProjectLocalPath();
        }

        #endregion


        #region Local Events

        /// <summary>
        /// Handles the Click event of the CloseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Handles the LinkClicked event of the CopyDetailsLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void CopyDetailsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Clipboard.SetText(TechnicalDetailsTextBox.Text, TextDataFormat.Text);
                MessageBox.Show(Properties.Resources.MessageCopiedDetails, "Copy",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ExternalException ex)
            {
                // Occurs when another process is using the clipboard
                ExceptionHandler.LogException(ex, true);
                MessageBox.Show(Properties.Resources.ErrorClipboardFailure, "Error copying",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Click event of the DataDirectoryButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void DataDirectoryButton_Click(object sender, EventArgs e)
        {
            Util.OpenURL(new Uri(EveMonClient.EVEMonDataDir));
        }

        /// <summary>
        /// Handles the LinkClicked event of the llblReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void llblReport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Util.OpenURL(new Uri(NetworkConstants.BitBucketIssuesBase));
        }

        /// <summary>
        /// Handles the LinkClicked event of the llblLatestBinaries control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void llblLatestBinaries_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Util.OpenURL(new Uri(NetworkConstants.GitHubDownloadsBase));
        }

        #endregion
    }
}
