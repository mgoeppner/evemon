using EVEMon.Common.Constants;
using EVEMon.Common.Controls;
using EVEMon.Common.CustomEventArgs;
using EVEMon.Common.Data;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Models;
using EVEMon.Common.Serialization.Settings;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EVEMon.ImplantControls
{
    /// <summary>
    /// This is the implant groups UI
    /// </summary>
    public sealed partial class ImplantSetsWindow : EVEMonForm
    {
        private const string PhantomSetName = "<New set>";

        private readonly long m_maxJumpClones;
        private readonly Character m_character;
        private readonly SerializableImplantSetCollection m_sets;

        /// <summary>
        /// Labels are substituted to comboboxes for read-only sets. It's because comboboxes cannot be readonly, only disabled. 
        /// But, then, they don't fire mouse events.
        /// </summary>
        private readonly Label[] m_labels = new Label[10];

        /// <summary>
        /// Default constructor for designer
        /// </summary>
        private ImplantSetsWindow()
        {
            InitializeComponent();
            ImplantSetsLabel.Font = new Font(ImplantSetsLabel.Font.FontFamily, 12F);
            ImplantsLabel.Font = new Font(ImplantsLabel.Font.FontFamily, 12F);
            RememberPositionKey = "ImplantSetsWindow";
        }

        /// <summary>
        /// Constructor used in code.
        /// </summary>
        /// <param name="character"></param>
        /// <exception cref="System.ArgumentNullException">character</exception>
        public ImplantSetsWindow(Character character)
            : this()
        {
            character.ThrowIfNull(nameof(character));

            m_character = character;
            m_sets = character.ImplantSets.Export();
            m_maxJumpClones = character.Skills[DBConstants.InfomorphPsychologySkillID].Level +
                              character.Skills[DBConstants.AdvancedInfomorphPsychologySkillID].Level;
        }

        /// <summary>
        /// On load, fill up the controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImpGroups_Load(object sender, EventArgs e)
        {
            // Header
            headerLabel.Text = string.Format(Properties.Resources.MessageJumpCloneSkills,
                m_character, m_maxJumpClones);

            // Populate implants combo boxes
            foreach (Control control in Controls)
            {
                AddComboBoxAndLabel(control);
            }

            // Adds the labels
            for (int i = 0; i < 10; i++)
            {
                Controls.Add(m_labels[i]);
            }

            // Sets the grid rows
            setsGrid.Rows.Clear();
            AddRow(m_sets.ActiveClone);
            foreach (SerializableSettingsImplantSet set in m_sets.JumpClones)
            {
                AddRow(set);
            }
            foreach (SerializableSettingsImplantSet set in m_sets.CustomSets)
            {
                AddRow(set);
            }

            for (var i = 0; i <= m_sets.JumpClones.Count; i++)
            {
                setsGrid.Rows[i].ReadOnly = true;
                setsGrid.Rows[i].Cells[0].Style.ForeColor = SystemColors.GrayText;
            }

            setsGrid.Rows[0].Selected = true;

            // Update the texts
            UpdateSlots();

            // Subscribe events
            setsGrid.AllowUserToDeleteRows = false;
            setsGrid.CellValidating += setsGrid_CellValidating;
            setsGrid.RowsRemoved += setsGrid_RowsRemoved;
            setsGrid.SelectionChanged += setsGrid_SelectionChanged;
            setsGrid.CellBeginEdit += setsGrid_CellBeginEdit;
            Disposed += OnDisposed;
        }

        /// <summary>
        /// Unsubscribe events on disposing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisposed(object sender, EventArgs e)
        {
            setsGrid.CellValidating -= setsGrid_CellValidating;
            setsGrid.RowsRemoved -= setsGrid_RowsRemoved;
            setsGrid.SelectionChanged -= setsGrid_SelectionChanged;
            setsGrid.CellBeginEdit -= setsGrid_CellBeginEdit;
            Disposed -= OnDisposed;
        }

        /// <summary>
        /// On close, closes the tooltip.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            m_fakeToolTip.Close();
            m_fakeToolTip = null;
        }

        /// <summary>
        /// Adds the combo box and label.
        /// </summary>
        /// <param name="control">The control.</param>
        private void AddComboBoxAndLabel(IDisposable control)
        {
            DropDownMouseMoveComboBox combo = control as DropDownMouseMoveComboBox;
            if (combo == null)
                return;

            int slotIndex;
            if (!combo.Name.Replace("cbSlot", string.Empty).TryParseInv(out slotIndex) ||
                    slotIndex < 1)
                return;
            ImplantSlots slot = (ImplantSlots)(slotIndex - 1);

            combo.Tag = slot;
            combo.MouseMove += combo_MouseMove;
            combo.DropDownClosed += combo_DropDownClosed;
            combo.DropDownMouseMove += combo_DropDownMouseMove;
            foreach (Implant implant in StaticItems.GetImplants(slot))
            {
                combo.Items.Add(implant);
            }

            Label tempLabel = null;
            try
            {
                tempLabel = new Label();
                tempLabel.MouseMove += label_MouseMove;
                tempLabel.AutoSize = false;
                tempLabel.Anchor = combo.Anchor;
                tempLabel.Bounds = combo.Bounds;
                tempLabel.TextAlign = ContentAlignment.MiddleLeft;

                Label label = tempLabel;
                tempLabel = null;

                m_labels[(int)slot] = label;
            }
            finally
            {
                tempLabel?.Dispose();
            }
        }

        /// <summary>
        /// Adds the row for the given set.
        /// </summary>
        /// <param name="set"></param>
        private void AddRow(SerializableSettingsImplantSet set)
        {
            DataGridViewRow tempRow = null;
            try
            {
                tempRow = new DataGridViewRow();
                tempRow.CreateCells(setsGrid, set.Name);
                tempRow.Tag = set;

                DataGridViewRow row = tempRow;
                tempRow = null;

                setsGrid.Rows.Add(row);
            }
            finally
            {
                tempRow?.Dispose();
            }
        }

        /// <summary>
        /// Update the comboboxes' selections.
        /// </summary>
        private void UpdateSlots()
        {
            SerializableSettingsImplantSet set = GetSelectedSet();

            // No set selected or row name empty?
            if (set == null || string.IsNullOrEmpty(set.Name))
            {
                foreach (DropDownMouseMoveComboBox combo in Controls.OfType<DropDownMouseMoveComboBox>())
                {
                    // Disable the combo with the <None> implant
                    combo.SelectedIndex = 0;
                    combo.Visible = true;
                    combo.Enabled = false;

                    // Hide the label used for read-only sets
                    ImplantSlots slot = (ImplantSlots)combo.Tag;
                    Label label = m_labels[(int)slot];
                    label.Visible = false;
                }
                return;
            }

            // Scroll through comboboxes
            bool isReadOnly = set == m_sets.ActiveClone || m_sets.JumpClones.Any(x => x == set);
            foreach (DropDownMouseMoveComboBox combo in Controls.OfType<DropDownMouseMoveComboBox>())
            {
                // Enable the combo with the <None> implant
                combo.SelectedIndex = 0;
                combo.Visible = !isReadOnly;
                combo.Enabled = true;

                ImplantSlots slot = (ImplantSlots)combo.Tag;
                Implant selectedImplant = GetImplant(set, slot);

                // Scroll through every implant and check whether it is the selected one.
                int index = 0;
                foreach (Implant implant in combo.Items)
                {
                    if (implant == selectedImplant)
                    {
                        combo.SelectedIndex = index;
                        break;
                    }
                    index++;
                }

                // Set "none" when the implant was not found.
                if (index == combo.Items.Count)
                    combo.SelectedIndex = 0;

                // Updates the label displayed for read-only sets.
                Label label = m_labels[(int)slot];
                label.Visible = isReadOnly;
                label.Text = selectedImplant.Name;
                label.Tag = selectedImplant;
            }
        }


        #region Helper getters and setters

        /// <summary>
        /// Gets the selected implant slot
        /// </summary>
        private SerializableSettingsImplantSet GetSelectedSet() => setsGrid.SelectedRows.Count == 0 ? null : (SerializableSettingsImplantSet)setsGrid.SelectedRows[0].Tag;

        /// <summary>
        /// Gets the implant name for the given slot and the provided set.
        /// </summary>
        /// <param name="set"></param>
        /// <param name="slot"></param>
        /// <returns></returns>
        private static Implant GetImplant(SerializableSettingsImplantSet set, ImplantSlots slot)
        {
            // Invoke the property getter with the matching name through reflection
            object implantName = typeof(SerializableSettingsImplantSet).GetProperty(slot.ToString()).GetValue(set, null);

            return StaticItems.GetImplants(slot)[(string)implantName];
        }

        /// <summary>
        /// Sets the implant name for the given slot and the provided set.
        /// </summary>
        /// <param name="set"></param>
        /// <param name="slot"></param>
        /// <param name="implant"></param>
        /// <returns></returns>
        private static void SetImplant(SerializableSettingsImplantSet set, ImplantSlots slot, Implant implant)
        {
            // Set may be null when the user is editing the phantom line
            if (set == null)
                return;

            // Invoke the property setter with the matching name through reflection
            typeof(SerializableSettingsImplantSet).GetProperty(slot.ToString()).SetValue(set, implant.Name, null);
        }

        #endregion


        #region Reaction to controls events

        /// <summary>
        /// Ensures we display &lt;New set&gt; when we begin a new row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setsGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // If cell is empty, replaces the content by <New set>
            DataGridViewRow row = setsGrid.Rows[e.RowIndex];
            object formattedValue = row.Cells[0].FormattedValue;
            if (formattedValue != null && row.Tag == null && string.IsNullOrEmpty(formattedValue.ToString()))
                row.Cells[0].Value = PhantomSetName;
        }

        /// <summary>
        /// When the selection changes, we update the controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setsGrid_SelectionChanged(object sender, EventArgs e)
        {
            // Enable/disable the top buttons
            if (setsGrid.SelectedRows.Count > 0)
                setsGrid.AllowUserToDeleteRows = setsGrid.SelectedRows[0].Index >= m_sets.JumpClones.Count + 1;

            UpdateSlots();
        }

        /// <summary>
        /// When a cell value changes, we replace the new name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setsGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewRow row = setsGrid.Rows[e.RowIndex];
            string text = e.FormattedValue?.ToString() ?? string.Empty;

            // If the user forgets the edition and there is no bound set
            // or the given name exceeds 255 characters
            // or the name is empty,
            // we replace <New set> by an empty value
            if ((row.Tag == null && text == PhantomSetName) ||
                text.Length > EveMonConstants.ImplantSetNameMaxLength ||
                string.IsNullOrWhiteSpace(text))
            {
                row.Cells[0].Value = string.Empty;
                return;
            }

            // Updates the set's name
            EnsureRowSetInitialized(row);
            if (row.Tag == null)
                return;

            SerializableSettingsImplantSet set = (SerializableSettingsImplantSet)row.Tag;
            if (e.FormattedValue != null)
                set.Name = e.FormattedValue.ToString();
        }

        /// <summary>
        /// When a row is removed, remove the matching set.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setsGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            int index = e.RowIndex - 1 - m_sets.JumpClones.Count;
            if (index >= 0)
                m_sets.CustomSets.RemoveAt(index);
        }

        /// <summary>
        /// When the selection changes, we change the implant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSlotN_DropDownClosed(object sender, EventArgs e)
        {
            if (setsGrid.SelectedRows.Count != 0)
                EnsureRowSetInitialized(setsGrid.SelectedRows[0]);

            DropDownMouseMoveComboBox combo = (DropDownMouseMoveComboBox)sender;
            ImplantSlots slot = (ImplantSlots)combo.Tag;
            SetImplant(GetSelectedSet(), slot, (Implant)combo.SelectedItem);
        }

        /// <summary>
        /// On cancel, nothing special.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// On OK, let's fetch the serialization object to the real implant sets.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            SerializableSettingsImplantSet set = GetSelectedSet();
            if (set != null && !string.IsNullOrWhiteSpace(set.Name))
                m_character.ImplantSets.Import(m_sets);

            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        private void EnsureRowSetInitialized(DataGridViewRow row)
        {
            if (row.Tag != null)
                return;
            object formattedValue = row.Cells[0].FormattedValue;
            if (formattedValue != null)
                row.Tag = new SerializableSettingsImplantSet { Name = formattedValue.ToString() };
            m_sets.CustomSets.Add((SerializableSettingsImplantSet)row.Tag);
        }

        #endregion


        #region Tooltips management

        private ImplantTooltip m_fakeToolTip = new ImplantTooltip();

        /// <summary>
        /// When the combo box's dropdown is closed, we hide the implant.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combo_DropDownClosed(object sender, EventArgs e)
        {
            m_fakeToolTip.Hide();
        }

        /// <summary>
        /// When the mouse moves over the implants labels (used for read-only sets), we display a tooltip.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_MouseMove(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;
            Implant implant = label.Tag as Implant;
            if (implant == null)
                return;

            Point point = e.Location;
            point.Y += 10;

            m_fakeToolTip.Location = label.PointToScreen(point);
            m_fakeToolTip.Implant = implant;
            m_fakeToolTip.ShowInactiveTopmost();
        }

        /// <summary>
        /// When the mouse moves over the implants combos (used for writable sets), we display a tooltip on the right of the combo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combo_MouseMove(object sender, MouseEventArgs e)
        {
            DropDownMouseMoveComboBox combo = (DropDownMouseMoveComboBox)sender;
            Implant implant = combo.SelectedItem as Implant;
            if (implant == null)
                return;

            if (m_fakeToolTip.Visible && m_fakeToolTip.Implant == implant)
                return;

            Point point = new Point { X = combo.Width + 5, Y = 1 };

            m_fakeToolTip.Location = combo.PointToScreen(point);
            m_fakeToolTip.Implant = implant;
            m_fakeToolTip.ShowInactiveTopmost();
        }

        /// <summary>
        /// When the mouse moves over one of the items of the combobox dropdown, we display a tooltip on the right of the dropdown.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DropDownMouseMoveEventArgs"/> instance containing the event data.</param>
        private void combo_DropDownMouseMove(object sender, DropDownMouseMoveEventArgs e)
        {
            Implant implant = e.Item as Implant;
            if (implant == null)
                return;

            Control control = (Control)sender;
            Point point = new Point(control.ClientRectangle.Right + 20, control.ClientRectangle.Top);

            m_fakeToolTip.Location = control.PointToScreen(point);
            m_fakeToolTip.Implant = implant;
            m_fakeToolTip.ShowInactiveTopmost();
        }

        /// <summary>
        /// When the mouse moves over this window, then it left the comboboxes and labels, so we hide the tooltip.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            m_fakeToolTip.Hide();
        }

        #endregion
    }
}
