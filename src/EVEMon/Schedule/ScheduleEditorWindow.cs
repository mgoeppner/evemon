using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Constants;
using EVEMon.Common.Controls;
using EVEMon.Common.CustomEventArgs;
using EVEMon.Common.Scheduling;

namespace EVEMon.Schedule
{
    public partial class ScheduleEditorWindow : EVEMonForm
    {
        private const int OneDaysSeconds = 86400;
        private const int DaysOfWeek = 7;

        private DateTime m_currentDate = DateTime.Now;
        private List<ScheduleEntry> m_lbEntriesData;

        /// <summary>
        /// Constructor
        /// </summary>
        public ScheduleEditorWindow()
        {
            InitializeComponent();

            newScheduleEntryToolStripMenuItem.Font = new Font(newScheduleEntryToolStripMenuItem.Font, FontStyle.Bold);

            // Load Calendar Colors
            calControl.BlockingColor = (Color)Settings.UI.Scheduler.BlockingColor;
            calControl.RecurringColor = (Color)Settings.UI.Scheduler.RecurringEventGradientStart;
            calControl.RecurringColor2 = (Color)Settings.UI.Scheduler.RecurringEventGradientEnd;
            calControl.SingleColor = (Color)Settings.UI.Scheduler.SimpleEventGradientStart;
            calControl.SingleColor2 = (Color)Settings.UI.Scheduler.SimpleEventGradientEnd;
            calControl.TextColor = (Color)Settings.UI.Scheduler.TextColor;
        }

        /// <summary>
        /// On load, initialize the days and months names, and set the calendar to the current date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleEditorWindow_Load(object sender, EventArgs e)
        {
            UpdateEntries();

            // Months names
            nudMonth.Items.Clear();
            string[] monthNames = CultureConstants.DefaultCulture.DateTimeFormat.MonthNames;

            // Add Months to control
            nudMonth.Items.Add(monthNames[0]);
            for (int i = 1; i <= CultureConstants.DefaultCulture.Calendar.GetMonthsInYear(m_currentDate.Year); i++)
            {
                nudMonth.Items.Add(
                    monthNames[CultureConstants.DefaultCulture.Calendar.GetMonthsInYear(m_currentDate.Year) - i]);
            }

            // Set controls to current date
            nudMonth.Items.Add(monthNames[CultureConstants.DefaultCulture.Calendar.GetMonthsInYear(m_currentDate.Year) - 1]);
            nudYear.Value = m_currentDate.Year;
            nudMonth.SelectedIndex = nudMonth.Items.Count - 1 - m_currentDate.Month;
            nudDay.Maximum = CultureConstants.DefaultCulture.Calendar.GetDaysInMonth(m_currentDate.Year, m_currentDate.Month) + 1;
            nudDay.Value = m_currentDate.Day;
            calControl.Date = m_currentDate;

            // Subscribe to global events
            EveMonClient.SchedulerChanged += EveMonClient_SchedulerChanged;
        }

        /// <summary>
        /// Unsubscribe events on closing.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            EveMonClient.SchedulerChanged -= EveMonClient_SchedulerChanged;
        }


        #region Content creation & update

        /// <summary>
        /// Anytime the scheduler changed, we update the entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_SchedulerChanged(object sender, EventArgs e)
        {
            UpdateEntries();
        }

        /// <summary>
        /// Update both the calendar control and the left listbox
        /// </summary>
        private void UpdateEntries()
        {
            UpdateListBoxEntries();
            UpdateCalendarEntries();
        }

        /// <summary>
        /// Update the calendar control.
        /// </summary>
        private void UpdateCalendarEntries()
        {
            calControl.Entries.Clear();
            foreach (ScheduleEntry temp in Scheduler.Entries)
            {
                calControl.Entries.Add(temp);
            }
            calControl.Invalidate();
        }

        /// <summary>
        /// Update the left listbox containing the scheduled entries list.
        /// </summary>
        private void UpdateListBoxEntries()
        {
            m_lbEntriesData = new List<ScheduleEntry>(Scheduler.Entries);
            m_lbEntriesData.Sort(new ScheduleEntryTitleComparer());

            lbEntries.Items.Clear();
            m_lbEntriesData.ForEach(x => lbEntries.Items.Add(x));

            lbEntries.SelectedIndex = m_lbEntriesData.Any() ? 0 : -1;
        }

        /// <summary>
        /// Update the entry's description
        /// </summary>
        private void UpdateEntryDescription()
        {
            ScheduleEntry temp = m_lbEntriesData[lbEntries.SelectedIndex];
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Title: {temp.Title}");

            // Simple entry ?
            SimpleScheduleEntry simpleEntry = temp as SimpleScheduleEntry;
            if (simpleEntry != null)
            {
                sb
                    .AppendLine("One Off Entry")
                    .AppendLine($" Start: {simpleEntry.StartDate}")
                    .AppendLine($" End: {simpleEntry.EndDate}")
                    .AppendLine($" Expired: {simpleEntry.Expired}")
                    .AppendLine()
                    .AppendLine("Options")
                    .AppendLine($" Blocking: {(simpleEntry.Options & ScheduleEntryOptions.Blocking) != ScheduleEntryOptions.None}")
                    .AppendLine($" Silent: {(simpleEntry.Options & ScheduleEntryOptions.Quiet) != ScheduleEntryOptions.None}")
                    .AppendLine(
                        $" Uses Eve Time: {(simpleEntry.Options & ScheduleEntryOptions.EVETime) != ScheduleEntryOptions.None}");
            }
            // Or recurring entry ?
            else
            {
                RecurringScheduleEntry recurringEntry = (RecurringScheduleEntry)temp;

                sb
                    .AppendLine("Recurring Entry")
                    .AppendLine($" Start: {recurringEntry.StartDate.ToShortDateString()}")
                    .AppendLine($" End: {recurringEntry.EndDate.ToShortDateString()}")
                    .AppendLine($" Frequency: {recurringEntry.Frequency}");

                switch (recurringEntry.Frequency)
                {
                    case RecurringFrequency.Monthly:
                    {
                        sb
                            .AppendLine($"  Day of Month: {recurringEntry.DayOfMonth}")
                            .AppendLine($"  On Overflow: {recurringEntry.OverflowResolution}");
                    }
                        break;
                    case RecurringFrequency.Weekly:
                    {
                        DateTime nowish = DateTime.Now.Date;
                        DateTime initial =
                            recurringEntry.StartDate.AddDays((recurringEntry.DayOfWeek - recurringEntry.StartDate.DayOfWeek +
                                                              DaysOfWeek) % DaysOfWeek);
                        Double datediff = (DaysOfWeek * recurringEntry.WeeksPeriod -
                                           nowish.Subtract(initial).Days % (DaysOfWeek * recurringEntry.WeeksPeriod)) %
                                          (DaysOfWeek * recurringEntry.WeeksPeriod);

                        DateTime noWishDateTime =
                            nowish.AddDays(datediff).Add(TimeSpan.FromSeconds(recurringEntry.StartTimeInSeconds));

                        if (noWishDateTime < DateTime.Now)
                        {
                            datediff = datediff + DaysOfWeek * recurringEntry.WeeksPeriod;
                            noWishDateTime =
                                nowish.AddDays(datediff).Add(TimeSpan.FromSeconds(recurringEntry.StartTimeInSeconds));
                        }

                        sb
                            .AppendLine($"  Day of Week: {recurringEntry.DayOfWeek}")
                            .AppendLine($"  Every: {recurringEntry.WeeksPeriod}" +
                                        $" week{(recurringEntry.WeeksPeriod == 1 ? String.Empty : "s")}")
                            .AppendLine($"  Next: {noWishDateTime.ToShortDateString()}");
                    }
                        break;
                }

                if (recurringEntry.EndTimeInSeconds > OneDaysSeconds)
                    recurringEntry.EndTimeInSeconds -= OneDaysSeconds;

                sb
                    .AppendLine($" Start Time: {TimeSpan.FromSeconds(recurringEntry.StartTimeInSeconds)}")
                    .AppendLine($" End Time: {TimeSpan.FromSeconds(recurringEntry.EndTimeInSeconds)}")
                    .AppendLine($" Expired: {recurringEntry.Expired}")
                    .AppendLine()
                    .AppendLine("Options")
                    .AppendLine(
                        $" Blocking: {(recurringEntry.Options & ScheduleEntryOptions.Blocking) != ScheduleEntryOptions.None}")
                    .AppendLine($" Silent: {(recurringEntry.Options & ScheduleEntryOptions.Quiet) != ScheduleEntryOptions.None}")
                    .AppendLine(
                        $" Uses Eve Time: {(recurringEntry.Options & ScheduleEntryOptions.EVETime) != ScheduleEntryOptions.None}");
            }

            // Update the description
            lblEntryDescription.Text = sb.ToString();
        }

        /// <summary>
        /// Removes the selected entry.
        /// </summary>
        private void RemoveSelectedEntry()
        {
            int entryIndex = lbEntries.SelectedIndex;
            ScheduleEntry entry = m_lbEntriesData[entryIndex];
            Scheduler.Remove(entry);

            // When no entries left, clear the description label
            if (!m_lbEntriesData.Any())
                lbEntries_SelectedIndexChanged(null, EventArgs.Empty);
        }

        #endregion


        #region Calendar control events

        /// <summary>
        /// Anytime the mouse leaves the calendar control, hide the tooltip.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calControl_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Active = false;
        }

        /// <summary>
        /// Anytime the mouse enters the calendar control, display the tooltip.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calControl_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Active = false;
        }

        /// <summary>
        /// When the mouse gets pressed, we change the cursor.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void calControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            calControl.Cursor = Cursors.Default;
        }

        /// <summary>
        /// When the mouse moves over the calendar, we change the cursor.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void calControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;

            calControl.Cursor = calControl.IsValidDate(e.Location)
                ? CustomCursors.ContextMenu
                : Cursors.Default;
        }

        /// <summary>
        /// When the user double-clicks a day on the calendar control, we allow him to add a new entry.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DaySelectedEventArgs"/> instance containing the event data.</param>
        private void calControl_DayDoubleClicked(object sender, DaySelectedEventArgs e)
        {
            AddScheduleEntry(e.DateTime);
        }

        /// <summary>
        /// When the user clicks the calendar controls (main control)
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DaySelectedEventArgs"/> instance containing the event data.</param>
        private void calControl_DayClicked(object sender, DaySelectedEventArgs e)
        {
            UpdateDateTimeControls(e.DateTime);

            if (!e.DateTimeIsSameMonthAsPrevious)
                return;

            switch (e.Mouse.Button)
            {
                case MouseButtons.Left:
                    ShowCalendarTooltip(e.DateTime);
                    break;
                case MouseButtons.Right:
                    ShowCalendarContextMenu(e.DateTime, e.Location);
                    break;
            }
        }

        /// <summary>
        /// Updates the date time controls.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        private void UpdateDateTimeControls(DateTime dateTime)
        {
            nudDay.Value = dateTime.Day;
            nudMonth.SelectedIndex = nudMonth.Items.Count - 1 - dateTime.Month;
            nudYear.Value = dateTime.Year;
        }

        /// <summary>
        /// Show the context menu for the given day
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="location"></param>
        private void ShowCalendarContextMenu(DateTime datetime, Point location)
        {
            // Remove old submenus
            while (calContext.Items.Count > 2)
            {
                calContext.Items.RemoveAt(2);
            }

            // Set date Tag to new entry
            calContext.Items[0].Tag = datetime;

            // Add "Edit" menus for every schedule on this day
            foreach (ScheduleEntry entry in Scheduler.Entries.Where(x => x.IsToday(datetime)))
            {
                ToolStripItem tempItem = null;
                try
                {
                    tempItem = new ToolStripMenuItem();
                    tempItem.Click += editMenuItem_Click;
                    tempItem.Text = $"Edit \"{entry.Title}\"...";
                    tempItem.Tag = entry;

                    ToolStripItem item = tempItem;
                    tempItem = null;

                    calContext.Items.Add(item);
                }
                finally
                {
                    tempItem?.Dispose();
                }
            }

            // Show the toolstrip seperator if there are any entries
            toolStripSeperator.Visible = calContext.Items.Count > 2;

            // Display the menu
            calContext.Show(calControl, location);
        }

        /// <summary>
        /// Shows a tooltip enumerating all of entries for this day
        /// </summary>
        /// <param name="datetime"></param>
        private void ShowCalendarTooltip(DateTime datetime)
        {
            // How can you only localize the date?
            StringBuilder content = new StringBuilder();

            foreach (ScheduleEntry entry in Scheduler.Entries.Where(x => x.IsToday(datetime)).OrderBy(x => x.Title))
            {
                DateTime from = datetime;
                DateTime to = datetime;

                // Simple entry ?
                SimpleScheduleEntry simpleEntry = entry as SimpleScheduleEntry;
                if (simpleEntry != null)
                {
                    from = simpleEntry.StartDate;
                    to = simpleEntry.EndDate;
                }
                    // Or recurring entry ?
                else
                {
                    RecurringScheduleEntry recurring = (RecurringScheduleEntry)entry;

                    // Does this always have one entry?
                    IEnumerable<ScheduleDateTimeRange> ranges =
                        recurring.GetRangesInPeriod(new DateTime(datetime.Year, datetime.Month, datetime.Day, 0, 0, 0),
                                                    new DateTime(datetime.Year, datetime.Month, datetime.Day, 23, 59, 59));
                    IEnumerator<ScheduleDateTimeRange> enumranges = ranges.GetEnumerator();
                    while (enumranges.MoveNext())
                    {
                        ScheduleDateTimeRange r = enumranges.Current;
                        from = r.From;
                        to = r.To;
                    }
                }

                // If the "from" date is before the selected date
                if (!(from.Year == datetime.Year && from.Month == datetime.Month && from.Day == datetime.Day))
                {
                    // Set date to midnight today
                    from = new DateTime(datetime.Year, datetime.Month, datetime.Day, 0, 0, 0);
                }

                // If the "to" date is after the selected date
                if (!(to.Year == datetime.Year && to.Month == datetime.Month && to.Day == datetime.Day))
                {
                    // Set date to last second before tomorrows midnight
                    to = new DateTime(datetime.Year, datetime.Month, datetime.Day, 23, 59, 59);
                }

                // Append the tooltip content
                content.Append(entry.Title);

                if ((entry.Options & ScheduleEntryOptions.EVETime) != ScheduleEntryOptions.None)
                {
                    // In case local time conversion extends beyond the entry date,
                    // we display also the ending date
                    string toLocalTime = to.Day == to.ToLocalTime().Day
                        ? to.ToLocalTime().ToString("HH:mm", CultureConstants.DefaultCulture)
                        : to.ToLocalTime().ToString(CultureConstants.DefaultCulture);

                    content
                        .Append($" [ EVE Time: {from:HH:mm} - {to:HH:mm} ] ")
                        .Append($" [ Local Time: {from.ToLocalTime():HH:mm} - {toLocalTime} ] ");
                }
                else
                    content.Append($" [ {from:HH:mm} - {to:HH:mm} ] ");

                content.AppendLine();
            }

            toolTip.ToolTipTitle = $"Entries for {datetime:d}";
            toolTip.SetToolTip(calControl, content.ToString());
            toolTip.Active = true;
        }

        /// <summary>
        /// Context menu > Edit "entry name"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void editMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            ScheduleEntry entry = (ScheduleEntry)menu.Tag;

            // Allow the user to edit the entry
            using (EditScheduleEntryWindow f = new EditScheduleEntryWindow())
            {
                f.ScheduleEntry = entry;
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.Cancel)
                    return;

                Scheduler.Remove(entry);
                Scheduler.Add(f.ScheduleEntry);
            }
        }

        /// <summary>
        /// Context menu > New schedule entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newEntryMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            DateTime datetime = (DateTime)menu.Tag;
            AddScheduleEntry(datetime);
        }

        /// <summary>
        /// Adds the schedule entry.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        private static void AddScheduleEntry(DateTime datetime)
        {
            // Allow the user to define the new entry
            using (EditScheduleEntryWindow f = new EditScheduleEntryWindow(datetime))
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.Cancel)
                    return;

                // Add an entry
                Scheduler.Add(f.ScheduleEntry);
            }
        }

        #endregion


        #region Controls' handlers

        /// <summary>
        /// Toolbar > Delete entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbDeleteEntry_Click(object sender, EventArgs e)
        {
            if (lbEntries.SelectedIndex != -1)
                RemoveSelectedEntry();
        }

        /// <summary>
        /// Toolbar > Clear expired
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbClearExpired_Click(object sender, EventArgs e)
        {
            Scheduler.ClearExpired();

            // When no entries left, clear the description label
            if (!m_lbEntriesData.Any())
                lbEntries_SelectedIndexChanged(null, EventArgs.Empty);
        }

        /// <summary>
        /// When the user selects another entry on the left listbox, we update the labels below.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbEntries.SelectedIndex == -1)
            {
                lblEntryDescription.Text = String.Empty;
                return;
            }

            UpdateEntryDescription();
        }

        /// <summary>
        /// When the user double-clicks an entry on the left list box, we open the edition box for this entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbEntries_DoubleClick(object sender, EventArgs e)
        {
            if (lbEntries.SelectedIndex == -1)
                return;

            int entryIndex = lbEntries.SelectedIndex;
            ScheduleEntry entry = m_lbEntriesData[entryIndex];
            using (EditScheduleEntryWindow f = new EditScheduleEntryWindow())
            {
                f.ScheduleEntry = entry;
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.Cancel)
                    return;

                Scheduler.Remove(entry);
                Scheduler.Add(f.ScheduleEntry);
            }

            lbEntries.SelectedIndex = entryIndex;
        }

        /// <summary>
        /// Toolbar > Add entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addEntryMenuItem_Click(object sender, EventArgs e)
        {
            using (EditScheduleEntryWindow f = new EditScheduleEntryWindow())
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.Cancel)
                    return;

                Scheduler.Add(f.ScheduleEntry);
            }
        }

        /// <summary>
        /// Occur whenever the user changes the numeric box for the year
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudYear_ValueChanged(object sender, EventArgs e)
        {
            int oldyearnum = m_currentDate.Year;
            m_currentDate = m_currentDate.AddYears((int)nudYear.Value - m_currentDate.Year);
            if (m_currentDate.Month == 2 &&
                (CultureConstants.DefaultCulture.Calendar.IsLeapYear(m_currentDate.Year) ||
                 CultureConstants.DefaultCulture.Calendar.IsLeapYear(oldyearnum)))
            {
                nudDay.Maximum =
                    CultureConstants.DefaultCulture.Calendar.GetDaysInMonth(m_currentDate.Year, m_currentDate.Month) + 1;

                if (CultureConstants.DefaultCulture.Calendar.IsLeapYear(oldyearnum) && nudDay.Value > nudDay.Maximum)
                    nudDay.Value = nudDay.Maximum;
            }
            calControl.Date = m_currentDate;
        }

        /// <summary>
        /// Occur whenever the user changes the numeric box for the day
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudDay_ValueChanged(object sender, EventArgs e)
        {
            bool donex = false;
            bool doney = false;
            if (nudDay.Value == 0)
            {
                if (nudMonth.SelectedIndex == nudMonth.Items.Count - 2 && nudYear.Value == nudYear.Minimum)
                    nudDay.Value = 1;

                m_currentDate = m_currentDate.AddDays((int)nudDay.Value - m_currentDate.Day);
                nudDay.Maximum =
                    CultureConstants.DefaultCulture.Calendar.GetDaysInMonth(m_currentDate.Year, m_currentDate.Month) + 1;

                if (nudDay.Value == 0)
                {
                    nudDay.Value =
                        CultureConstants.DefaultCulture.Calendar.GetDaysInMonth(m_currentDate.Year, m_currentDate.Month);
                }

                donex = true;
            }

            if (!donex && nudDay.Value == nudDay.Maximum)
            {
                if (nudMonth.SelectedIndex == 1 && nudYear.Value == nudYear.Maximum)
                {
                    nudDay.Value =
                        CultureConstants.DefaultCulture.Calendar.GetDaysInMonth(m_currentDate.Year, m_currentDate.Month);

                    doney = true;
                }

                if (!doney)
                {
                    m_currentDate = m_currentDate.AddDays((int)nudDay.Value - m_currentDate.Day);
                    nudDay.Value = 1;
                    nudDay.Maximum =
                        CultureConstants.DefaultCulture.Calendar.GetDaysInMonth(m_currentDate.Year, m_currentDate.Month) + 1;
                }
                donex = true;
            }

            if (!donex)
                m_currentDate = m_currentDate.AddDays((int)nudDay.Value - m_currentDate.Day);

            calControl.Date = m_currentDate;
            nudYear.Value = m_currentDate.Year;
            nudMonth.SelectedIndex = CultureConstants.DefaultCulture.Calendar.GetMonthsInYear(m_currentDate.Year) -
                                     m_currentDate.Month + 1;
        }

        /// <summary>
        /// Occur whenever the user changes the numeric box for the month
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudMonth_ValueChanged(object sender, EventArgs e)
        {
            if (nudMonth.SelectedIndex == nudMonth.Items.Count - 1 && nudYear.Value == nudYear.Minimum)
                nudMonth.SelectedIndex = nudMonth.Items.Count - 2;

            if (nudMonth.SelectedIndex == 0 && nudYear.Value == nudYear.Maximum)
                nudMonth.SelectedIndex = 1;

            m_currentDate =
                m_currentDate.AddMonths(nudMonth.Items.Count - 1 - nudMonth.SelectedIndex - m_currentDate.Month);

            nudMonth.SelectedIndex = (nudMonth.SelectedIndex + (nudMonth.Items.Count - 3)) % (nudMonth.Items.Count - 2) + 1;

            if (nudDay.Value > CultureConstants.DefaultCulture.Calendar.GetDaysInMonth(m_currentDate.Year, m_currentDate.Month))
                nudDay.Value = CultureConstants.DefaultCulture.Calendar.GetDaysInMonth(m_currentDate.Year, m_currentDate.Month);

            nudDay.Maximum = CultureConstants.DefaultCulture.Calendar.GetDaysInMonth(m_currentDate.Year, m_currentDate.Month) + 1;
            calControl.Date = m_currentDate;
            nudYear.Value = m_currentDate.Year;
        }

        /// <summary>
        /// Handles the KeyDown event of the lbEntries control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void lbEntries_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    RemoveSelectedEntry();
                    break;
            }
        }

        #endregion
    }
}