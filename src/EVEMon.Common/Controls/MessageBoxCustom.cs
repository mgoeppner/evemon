using System;
using System.Drawing;
using System.Windows.Forms;

namespace EVEMon.Common.Controls
{
    /// <summary>
    /// Class for a message box with integrated checkbox.
    /// </summary>
    public sealed partial class MessageBoxCustom : Form
    {
        private DialogResult m_dialogResult;


        #region Constructor

        /// <summary>
        /// Creates a new instance of <see cref="MessageBoxCustom"/>.
        /// </summary>
        public MessageBoxCustom()
        {
            InitializeComponent();
        }

        #endregion


        #region Public Properties

        /// <summary>
        /// Gets the button1.
        /// </summary>
        /// <value>The button1.</value>
        public Button Button1 => button1;

        public Button Button2 => button2;

        public Button Button3 => button3;

        public Label Message => msgText;

        public PictureBox PictureBox => msgIcon;

        public CheckBox CheckBox => cbOption;

        /// <summary>
        /// Gets a value indicating whether the checkbox is checked.
        /// </summary>
        /// <value><c>true</c> if the checkbox is checked; otherwise, <c>false</c>.</value>
        public static bool CheckBoxChecked { get; private set; }

        #endregion


        #region Static Public Methods

        /// <summary>
        /// Displays a message box.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="cbText">The cb text.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, string cbText,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None)
        {
            using (MessageBoxCustom form = new MessageBoxCustom())
            {
                return form.ShowDialog(owner, text, caption, cbText, buttons, icon);
            }
        }

        /// <summary>
        /// Displays a message box.
        /// </summary>
        /// <param name="owner">Owner window.</param>
        /// <param name="text">Text to display.</param>
        /// <param name="caption">Text to display in the title bar.</param>
        /// <returns>One of the <see cref="DialogResult"/> values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption)
            => Show(owner, text, caption, string.Empty);

        /// <summary>
        /// Displays a message box.
        /// </summary>
        /// <param name="owner">Owner window.</param>
        /// <param name="text">Text to display.</param>
        /// <returns>One of the <see cref="DialogResult"/> values.</returns>
        public static DialogResult Show(IWin32Window owner, string text) => Show(owner, text, string.Empty, string.Empty);

        #endregion


        # region Event Handlers

        /// <summary>
        /// Called when a button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnButtonClick(object sender, EventArgs e)
        {
            ButtonBase button = sender as Button;

            if (button == null)
                return;

            m_dialogResult = GetDialogResult(button.Text);
            Close();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the cbOption control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cbOption_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxChecked = cbOption.Checked;
        }

        # endregion


        # region Help Methods

        /// <summary>
        /// Displays a message box.
        /// </summary>
        /// <param name="owner">Owner window.</param>
        /// <param name="text">Text to display.</param>
        /// <param name="caption">Text to display in the title bar.</param>
        /// <param name="cbText">Text to display near check box.</param>
        /// <param name="buttons">Buttons to display in the message box.</param>
        /// <param name="icon">Icon to display in the mesage box.</param>
        /// <returns>One of the <see cref="DialogResult"/> values.</returns>
        private DialogResult ShowDialog(IWin32Window owner, string text, string caption, string cbText,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None)
        {
            button1.Click += OnButtonClick;
            button2.Click += OnButtonClick;
            button3.Click += OnButtonClick;

            Text = caption;
            msgText.Text = text;
            cbOption.Text = cbText;

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                NativeMethods.EnableMenuItem(NativeMethods.GetSystemMenu(Handle, false),
                    NativeMethods.SC_CLOSE, NativeMethods.MF_BYCOMMAND | NativeMethods.MF_GRAYED);
            }
            else
                ControlBox = false;

            SetButtonsToDisplay(buttons);

            SetIconToDisplay(icon);

            MessageBeep(icon);

            ShowDialog(owner);

            return m_dialogResult;
        }

        /// <summary>
        /// Sets buttons for the message box.
        /// </summary>
        /// <param name="buttons">Which buttons to display.</param>
        private void SetButtonsToDisplay(MessageBoxButtons buttons)
        {
            AcceptButton = button2;

            switch (buttons)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    button3.Text = "Ignore";
                    button2.Text = "Retry";
                    button1.Text = "Abort";
                    break;
                case MessageBoxButtons.OK:
                    button3.Visible = false;
                    button2.Visible = false;
                    button1.Text = "OK";
                    AcceptButton = button1;
                    break;
                case MessageBoxButtons.OKCancel:
                    button3.Visible = false;
                    button2.Text = "OK";
                    button1.Text = "Cancel";
                    break;
                case MessageBoxButtons.RetryCancel:
                    button3.Visible = false;
                    button2.Text = "Retry";
                    button1.Text = "Cancel";
                    break;
                case MessageBoxButtons.YesNo:
                    button3.Visible = false;
                    button2.Text = "Yes";
                    button1.Text = "No";
                    break;
                case MessageBoxButtons.YesNoCancel:
                    button3.Text = "Yes";
                    button2.Text = "No";
                    button1.Text = "Cancel";
                    AcceptButton = button3;
                    break;
            }
        }

        /// <summary>
        /// Sets icon for the message box.
        /// </summary>
        /// <param name="icon">Icon type.</param>
        private void SetIconToDisplay(MessageBoxIcon icon)
        {
            switch (icon.GetHashCode())
            {
                case 0:
                    break;
                case 16:
                    msgIcon.Image = SystemIcons.Hand.ToBitmap();
                    break;
                case 32:
                    msgIcon.Image = SystemIcons.Question.ToBitmap();
                    break;
                case 48:
                    msgIcon.Image = SystemIcons.Exclamation.ToBitmap();
                    break;
                case 64:
                    msgIcon.Image = SystemIcons.Asterisk.ToBitmap();
                    break;
            }
        }

        /// <summary>
        /// Plays one of the system message beeps.
        /// </summary>
        /// <param name="icon">Sound type to play.</param>
        private static void MessageBeep(MessageBoxIcon icon)
        {
            switch (icon.GetHashCode())
            {
                case 0:
                    System.Media.SystemSounds.Beep.Play();
                    break;

                case 16:
                    System.Media.SystemSounds.Hand.Play();
                    break;

                case 32:
                    System.Media.SystemSounds.Question.Play();
                    break;

                case 48:
                    System.Media.SystemSounds.Exclamation.Play();
                    break;

                case 64:
                    System.Media.SystemSounds.Asterisk.Play();
                    break;
            }
        }

        /// <summary>
        /// Returns dialog result based on button text.
        /// </summary>
        /// <param name="buttonText">Text on selected button.</param>
        /// <returns>Corresponding <see cref="DialogResult"/>.</returns>
        private static DialogResult GetDialogResult(string buttonText)
        {
            switch (buttonText)
            {
                case "Abort":
                    return DialogResult.Abort;
                case "Cancel":
                    return DialogResult.Cancel;
                case "Ignore":
                    return DialogResult.Ignore;
                case "No":
                    return DialogResult.No;
                case "OK":
                    return DialogResult.OK;
                case "Retry":
                    return DialogResult.Retry;
                case "Yes":
                    return DialogResult.Yes;
                default:
                    return DialogResult.None;
            }
        }

        # endregion
    }
}