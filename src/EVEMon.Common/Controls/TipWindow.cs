using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EVEMon.Common.Extensions;

namespace EVEMon.Common.Controls
{
    public sealed partial class TipWindow : Form
    {
        private readonly Form m_owner;

        /// <summary>
        /// Initializes a new instance of the <see cref="TipWindow"/> class.
        /// </summary>
        private TipWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TipWindow" /> class.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="title">The title.</param>
        /// <param name="tiptext">The tiptext.</param>
        /// <param name="key">The key.</param>
        /// <param name="checkboxVisible">if set to <c>true</c> the checkbox is visible.</param>
        private TipWindow(Form form, string title, string tiptext, string key, bool checkboxVisible)
            : this()
        {
            m_owner = form;

            Tag = key;
            cbDontShowAgain.Visible = checkboxVisible;
            pictureBox.Image = SystemIcons.Information.ToBitmap();

            Text = title;
            TipLabel.Text = tiptext;
            UpdateLocation();

            // Keep the tip glued to the owner's top right corner
            m_owner.Move += OwnerBoundsChanged;
            m_owner.Resize += OwnerBoundsChanged;
        }

        /// <summary>
        /// The tip must not steal focus from the owner window when it appears.
        /// </summary>
        protected override bool ShowWithoutActivation => true;

        /// <summary>
        /// Occurs when the window size changes; auto-sizing can resize it after it was
        /// positioned, so realign it with the owner's corner.
        /// </summary>
        /// <param name="e">A <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateLocation();
        }

        /// <summary>
        /// Occurs when the window closes.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.FormClosedEventArgs"/> that contains the event data.</param>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (m_owner != null)
            {
                m_owner.Move -= OwnerBoundsChanged;
                m_owner.Resize -= OwnerBoundsChanged;
            }

            base.OnFormClosed(e);
        }

        /// <summary>
        /// Occurs when the owner window moves or resizes.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OwnerBoundsChanged(object sender, EventArgs e)
        {
            UpdateLocation();
        }

        /// <summary>
        /// Aligns the top right corner of the tip window with the top right corner of the owner's client rectangle.
        /// </summary>
        private void UpdateLocation()
        {
            if (m_owner == null)
                return;

            Location = m_owner.PointToScreen(new Point(m_owner.ClientSize.Width - Width, 0));
        }

        /// <summary>
        /// Handles the Click event of the btnOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cbDontShowAgain.Checked)
            {
                Settings.UI.ConfirmedTips.Add((string)Tag);
                Settings.Save();
            }

            Close();
        }

        /// <summary>
        /// Show a "tip of the day"-like message on the top right corner of the given window.
        /// The tip is an owned window rather than a child control, so it always stays above
        /// the owner's other controls (a docked TabControl would otherwise cover it).
        /// </summary>
        /// <param name="form">The owner window.</param>
        /// <param name="key">The key used to store informations about messages the user already saw. Every messages is only displayed once.</param>
        /// <param name="title">The title of the tip window.</param>
        /// <param name="tiptext">The text of the tip window.</param>
        /// <param name="checkBoxVisible">if set to <c>true</c> the checkbox is visible.</param>
        /// <exception cref="System.ArgumentNullException">form</exception>
        public static void ShowTip(Form form, string key, string title, string tiptext, bool checkBoxVisible = true)
        {
            form.ThrowIfNull(nameof(form));

            if (Settings.UI.ConfirmedTips.Contains(key))
                return;

            // Quit if it's already shown
            if (form.OwnedForms.OfType<TipWindow>().Any())
                return;

            // Closes and gets disposed when clicking the OK button
            TipWindow tipWindow = new TipWindow(form, title, tiptext, key, checkBoxVisible);
            tipWindow.Show(form);
        }
    }
}
