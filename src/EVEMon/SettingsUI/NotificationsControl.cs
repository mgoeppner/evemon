using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Extensions;
using EVEMon.Common.Notifications;
using EVEMon.Common.SettingsObjects;

namespace EVEMon.SettingsUI
{
    public partial class NotificationsControl : UserControl
    {
        // Would have love to use tableLayoutPanel, unfortunately, they are just a piece of trash.
        private const int RowHeight = 28;

        private readonly List<ComboBox> m_combos = new List<ComboBox>();
        private readonly List<CheckBox> m_checkboxes = new List<CheckBox>();
        private NotificationSettings m_settings;

        /// <summary>
        /// Constructor
        /// </summary>
        public NotificationsControl()
        {
            InitializeComponent();
            labelBehaviour.Font = new Font(labelBehaviour.Font, FontStyle.Bold);
            labelNotification.Font = new Font(labelNotification.Font, FontStyle.Bold);
            labelMainWindow.Font = new Font(labelMainWindow.Font, FontStyle.Bold);

            PopulateControl();
        }

        /// <summary>
        /// Gets or sets the settings to edit (should be a copy of the actual settings).
        /// </summary>
        [Browsable(false)]
        public NotificationSettings Settings
        {
            get { return m_settings; }
            set
            {
                if (value == null)
                    return;

                m_settings = value;

                foreach (ComboBox combo in m_combos)
                {
                    NotificationCategory cat = (NotificationCategory)combo.Tag;
                    int index = (int)m_settings.Categories[cat].ToolTipBehaviour;
                    combo.SelectedIndex = index;
                }

                foreach (CheckBox checkbox in m_checkboxes)
                {
                    NotificationCategory cat = (NotificationCategory)checkbox.Tag;
                    checkbox.Checked = m_settings.Categories[cat].ShowOnMainWindow;
                }
            }
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<NotificationCategory> Categories
            => Enum.GetValues(typeof(NotificationCategory)).Cast<NotificationCategory>()
                .Where(x => EveMonClient.IsDebugBuild || x != NotificationCategory.TestNofitication)
                .Where(x => x.HasHeader());

        /// <summary>
        /// When the selected index changes, we update the settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            NotificationCategory cat = (NotificationCategory)combo.Tag;
            m_settings.Categories[cat].ToolTipBehaviour = (ToolTipNotificationBehaviour)combo.SelectedIndex;
        }

        /// <summary>
        /// When the selected checkbox check state changes, we update the settings.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void checkbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            NotificationCategory cat = (NotificationCategory)checkbox.Tag;
            m_settings.Categories[cat].ShowOnMainWindow = checkbox.Checked;
        }

        /// <summary>
        /// Populates the control.
        /// </summary>
        private void PopulateControl()
        {
            int height = RowHeight;

            // Add the controls for every member of the enumeration
            foreach (NotificationCategory cat in Categories)
            {
                // Add the label
                AddLabel(height, cat);

                // Add the "system tray tooltip" combo box
                AddComboBox(height, cat);

                // Add the "main window" checkbox
                AddCheckBox(height, cat);

                // Updates the row ordinate
                height += RowHeight;
            }

            Height = height;
        }

        /// <summary>
        /// Adds the check box.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <param name="cat">The cat.</param>
        private void AddCheckBox(int height, NotificationCategory cat)
        {
            CheckBox tempCheckbox = null;
            try
            {
                tempCheckbox = new CheckBox();
                tempCheckbox.CheckedChanged += checkbox_CheckedChanged;
                tempCheckbox.Tag = cat;
                tempCheckbox.Text = "Show";
                tempCheckbox.Margin = new Padding(3);
                tempCheckbox.Height = RowHeight - 4;
                tempCheckbox.Width = labelMainWindow.Width;
                tempCheckbox.Location = new Point(labelMainWindow.Location.X + 15, height + 2);

                CheckBox checkbox = tempCheckbox;
                tempCheckbox = null;

                Controls.Add(checkbox);
                m_checkboxes.Add(checkbox);
            }
            finally
            {
                tempCheckbox?.Dispose();
            }
        }

        /// <summary>
        /// Adds the combo box.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <param name="cat">The cat.</param>
        private void AddComboBox(int height, NotificationCategory cat)
        {
            ComboBox tempCombo = null;
            try
            {
                tempCombo = new ComboBox();
                tempCombo.Items.AddRange(new object[] { "Never", "Once", "Repeat until clicked" });
                tempCombo.Tag = cat;
                tempCombo.SelectedIndex = 0;
                tempCombo.Margin = new Padding(3);
                tempCombo.Height = RowHeight - 4;
                tempCombo.Width = labelBehaviour.Width;
                tempCombo.DropDownStyle = ComboBoxStyle.DropDownList;
                tempCombo.Location = new Point(labelBehaviour.Location.X, height + 2);
                tempCombo.SelectedIndexChanged += combo_SelectedIndexChanged;

                ComboBox combo = tempCombo;
                tempCombo = null;

                Controls.Add(combo);
                m_combos.Add(combo);
            }
            finally
            {
                tempCombo?.Dispose();
            }
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <param name="cat">The cat.</param>
        private void AddLabel(int height, NotificationCategory cat)
        {
            Label tempLabel = null;
            try
            {
                tempLabel = new Label();
                tempLabel.AutoSize = false;
                tempLabel.Text = cat.GetHeader();
                tempLabel.TextAlign = ContentAlignment.MiddleLeft;
                tempLabel.Location = new Point(labelNotification.Location.X, height);
                tempLabel.Width = labelBehaviour.Location.X - 3;
                tempLabel.Height = RowHeight;

                Label label = tempLabel;
                tempLabel = null;

                Controls.Add(label);
            }
            finally
            {
                tempLabel?.Dispose();
            }
        }
    }
}