using System;
using System.Drawing;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Controls;
using EVEMon.Common.Enumerations.UISettings;
using EVEMon.Common.Models;

namespace EVEMon.SkillPlanner
{
    /// <summary>
    /// Actions to take on obsolete entries.
    /// </summary>
    public enum ObsoleteEntriesAction
    {
        None = 0,
        KeepAll = 1,
        RemoveAll = 2,
        RemoveConfirmed = 3
    }

    /// <summary>
    /// Displays a list of obsolete skills in the plan, allowing the user
    /// to select an action. The action is returned to the caller.
    /// </summary>
    public partial class ObsoleteEntriesWindow : EVEMonForm
    {
        private readonly Plan m_plan;
        private ObsoleteEntriesAction m_result = ObsoleteEntriesAction.None;
        private ObsoleteEntriesAction m_previewResult = ObsoleteEntriesAction.None;


        #region Static Methods

        /// <summary>
        /// Creates the dialog based upon a plan.
        /// </summary>
        /// <param name="plan">Plan to display obsolete entries from</param>
        /// <returns>Action to be taken</returns>
        public static ObsoleteEntriesAction ShowDialog(Plan plan)
        {
            if (plan == null)
                return ObsoleteEntriesAction.None;

            if (!plan.ContainsObsoleteEntries)
                return ObsoleteEntriesAction.None;

            using (ObsoleteEntriesWindow form = new ObsoleteEntriesWindow(plan))
            {
                return form.ShowObsoleteEntriesDialog();
            }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="plan">Plan to display obsolete entries from</param>
        private ObsoleteEntriesWindow(Plan plan)
        {
            InitializeComponent();

            m_plan = plan;

            if (Settings.UI.PlanWindow.ObsoleteEntryRemovalBehaviour == ObsoleteEntryRemovalBehaviour.RemoveConfirmed)
            {
                ObsoleteEntriesListView.Columns.RemoveAt(ObsoleteEntriesListView.Columns.Count - 1);
                RemoveConfirmedButton.Visible = false;
            }

            UpdateListView();
            AutoFitColumnHeaders();
        }

        #endregion


        #region Private Helper Methods

        /// <summary>
        /// Displays the dialog and returns the Action Result.
        /// </summary>
        /// <returns></returns>
        private ObsoleteEntriesAction ShowObsoleteEntriesDialog()
        {
            ShowDialog();
            return m_result;
        }

        /// <summary>
        /// Update the list view with items from the plan, colour if required.
        /// </summary>
        private void UpdateListView()
        {
            // if the user has already decided just let the window close
            if (m_result != ObsoleteEntriesAction.None)
                return;

            ObsoleteEntriesListView.BeginUpdate();
            ObsoleteEntriesListView.Items.Clear();

            foreach (PlanEntry entry in m_plan.ObsoleteEntries)
            {
                var lvi = new ListViewItem(entry.ToString());

                if (entry.CharacterSkill.LastConfirmedLvl >= entry.Level)
                {
                    lvi.SubItems.Add("Confirmed (API)");
                    lvi.Tag = true;
                }
                else
                {
                    lvi.SubItems.Add("Unconfirmed");
                    lvi.Tag = false;
                }

                FormatListViewItem(lvi);

                ObsoleteEntriesListView.Items.Add(lvi);
            }

            ObsoleteEntriesListView.EndUpdate();
        }

        /// <summary>
        /// Format a ListViewItem based upon its status and the current.
        /// preview result.
        /// </summary>
        /// <param name="lvi"><c>ListViewItem</c> to format</param>
        /// <exception cref="NotImplementedException"></exception>
        private void FormatListViewItem(ListViewItem lvi)
        {
            bool confirmed = (bool)lvi.Tag;

            switch (m_previewResult)
            {
                case ObsoleteEntriesAction.KeepAll:
                    FormatListViewKeepStyle(lvi);
                    break;
                case ObsoleteEntriesAction.RemoveAll:
                    FormatListViewRemoveStyle(lvi);
                    break;
                case ObsoleteEntriesAction.RemoveConfirmed:
                    if (confirmed)
                        FormatListViewRemoveStyle(lvi);
                    else
                        FormatListViewKeepStyle(lvi);
                    break;
                case ObsoleteEntriesAction.None:
                    FormatListViewNormalStyle(lvi);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Format the ListViewItem in the Normal Style.
        /// </summary>
        /// <param name="lvi"><c>ListViewItem</c> to format</param>
        private static void FormatListViewNormalStyle(ListViewItem lvi)
        {
            lvi.ForeColor = Color.Black;
            lvi.Font = new Font(lvi.Font, FontStyle.Regular);
        }

        /// <summary>
        /// Format the ListViewItem in the Remove Style
        /// </summary>
        /// <param name="lvi"><c>ListViewItem</c> to format</param>
        private static void FormatListViewRemoveStyle(ListViewItem lvi)
        {
            lvi.ForeColor = Color.Red;
            lvi.Font = new Font(lvi.Font, FontStyle.Strikeout);
        }

        /// <summary>
        /// Format the ListViewItem in the Keep Style
        /// </summary>
        /// <param name="lvi"><c>ListViewItem</c> to format</param>
        private static void FormatListViewKeepStyle(ListViewItem lvi)
        {
            lvi.ForeColor = Color.DarkGreen;
            lvi.Font = new Font(lvi.Font, FontStyle.Regular);
        }

        /// <summary>
        /// Fit the column headers to their content
        /// </summary>
        private void AutoFitColumnHeaders()
        {
            foreach (ColumnHeader column in ObsoleteEntriesListView.Columns)
            {
                column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }

        #endregion


        #region Event Handlers

        /// <summary>
        /// Keep All Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeepAllButton_Click(object sender, EventArgs e)
        {
            m_result = ObsoleteEntriesAction.KeepAll;
            Close();
        }

        /// <summary>
        /// Remove All Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveAllButton_Click(object sender, EventArgs e)
        {
            m_result = ObsoleteEntriesAction.RemoveAll;
            Close();
        }

        /// <summary>
        /// Remove Confirmed Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveConfirmedButton_Click(object sender, EventArgs e)
        {
            m_result = ObsoleteEntriesAction.RemoveConfirmed;
            Close();
        }

        /// <summary>
        /// Keep All Mouse Hover / Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeepAllButton_Focused(object sender, EventArgs e)
        {
            m_previewResult = ObsoleteEntriesAction.KeepAll;
            UpdateListView();
        }

        /// <summary>
        /// Remove All Mouse Hover / Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveAllButton_Focused(object sender, EventArgs e)
        {
            m_previewResult = ObsoleteEntriesAction.RemoveAll;
            UpdateListView();
        }

        /// <summary>
        /// Remove Confirmed Mouse Hover / Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveConfirmedButton_Focused(object sender, EventArgs e)
        {
            m_previewResult = ObsoleteEntriesAction.RemoveConfirmed;
            UpdateListView();
        }

        /// <summary>
        /// Any button Mouse/Focus Leave 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnyButton_Leave(object sender, EventArgs e)
        {
            m_previewResult = ObsoleteEntriesAction.None;
            UpdateListView();
        }

        #endregion
    }
}
