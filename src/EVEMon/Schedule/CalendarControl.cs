using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using EVEMon.Common.Constants;
using EVEMon.Common.CustomEventArgs;
using EVEMon.Common.Extensions;

namespace EVEMon.Schedule
{
    public enum CalendarType
    {
        Month
    }

    public partial class CalendarControl : UserControl
    {

        public event EventHandler<DaySelectedEventArgs> DayClicked;
        public event EventHandler<DaySelectedEventArgs> DayDoubleClicked;

        private const double CellAspectRatio = 7.0d / 10.0d;
        private const int MaxRows = 6;
        private const int HeaderHeight = 20;
        private const int DayHeaderHeight = 20;

        private Point m_calTopLeft = new Point(0, 0);
        private Size m_cellSize = new Size(5, 5);
        private DayOfWeek m_firstDayOfWeek;
        private CalendarType m_calendarType = CalendarType.Month;
        private DateTime m_date;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarControl"/> class.
        /// </summary>
        protected CalendarControl()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            m_date = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the type of the calendar.
        /// </summary>
        /// <value>The type of the calendar.</value>
        public CalendarType CalendarType
        {
            get { return m_calendarType; }
            set
            {
                if (m_calendarType == value)
                    return;

                m_calendarType = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        public DateTime Date
        {
            get { return m_date; }
            set
            {
                if (m_date == value)
                    return;

                m_date = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Handles the Load event of the CalendarControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CalendarControl_Load(object sender, EventArgs e)
        {
            m_firstDayOfWeek = CultureConstants.DefaultCulture.DateTimeFormat.FirstDayOfWeek;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Brush b = new LinearGradientBrush(ClientRectangle, Color.LightBlue, Color.DarkBlue, 90.0F))
            {
                e.Graphics.FillRectangle(b, e.ClipRectangle);
            }

            switch (m_calendarType)
            {
                case CalendarType.Month:
                    PaintMonthCalendar(e);
                    HighlightToday(e);
                    HighlightDay(e);
                    break;
            }
        }

        /// <summary>
        /// Calculates the cell metrics.
        /// </summary>
        private void CalculateCellMetrics()
        {
            double maxCellWidth = Math.Floor(Convert.ToDouble(ClientSize.Width) / 8.0f);
            double maxCellHeight =
                Math.Floor(Convert.ToDouble(ClientSize.Height - HeaderHeight - DayHeaderHeight) /
                           Convert.ToDouble(MaxRows + 1));

            double heightWithMaxWidth = Math.Floor(maxCellWidth * CellAspectRatio);

            int effectiveHeight = Convert.ToInt32(heightWithMaxWidth);
            int effectiveWidth = Convert.ToInt32(maxCellWidth);
            if (heightWithMaxWidth > maxCellHeight)
            {
                double widthWithMaxHeight = Math.Floor(maxCellHeight / CellAspectRatio);
                effectiveHeight = Convert.ToInt32(maxCellHeight);
                effectiveWidth = Convert.ToInt32(widthWithMaxHeight);
            }

            int calWidth = effectiveWidth * 7;
            int calHeight = effectiveHeight * MaxRows;

            m_calTopLeft = new Point(ClientSize.Width / 2 - calWidth / 2,
                                     ClientSize.Height / 2 - (calHeight + HeaderHeight + DayHeaderHeight) / 2);
            m_cellSize = new Size(effectiveWidth, effectiveHeight);
        }

        /// <summary>
        /// Paints the month calendar.
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void PaintMonthCalendar(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            CalculateCellMetrics();

            DateTime mdt = new DateTime(m_date.Year, m_date.Month, 1);
            string ymDesc = mdt.ToString("y", CultureConstants.DefaultCulture);

            DayOfWeek firstDayDow = mdt.DayOfWeek;
            DayOfWeek cDow = m_firstDayOfWeek;
            using (Font boldf = new Font(Font, FontStyle.Bold))
            {
                Rectangle headerRect = new Rectangle(m_calTopLeft.X, m_calTopLeft.Y,
                                                     m_cellSize.Width * 7, HeaderHeight);
                using (Brush hb = new SolidBrush(Color.FromArgb(47, 77, 132)))
                {
                    graphics.FillRectangle(hb, headerRect);
                }
                graphics.DrawRectangle(Pens.Black, headerRect);
                TextRenderer.DrawText(graphics, ymDesc, boldf,
                                      new Rectangle(headerRect.Left + 1, headerRect.Top + 1, headerRect.Width - 2,
                                                    headerRect.Height - 2),
                                      Color.White, Color.Transparent,
                                      TextFormatFlags.EndEllipsis | TextFormatFlags.HorizontalCenter |
                                      TextFormatFlags.VerticalCenter);

                using (Brush db = new SolidBrush(Color.FromArgb(106, 149, 228)))
                {
                    for (int x = 0; x < 7; x++)
                    {
                        Rectangle cellRect = new Rectangle(m_calTopLeft.X + m_cellSize.Width * x,
                                                           m_calTopLeft.Y + HeaderHeight, m_cellSize.Width,
                                                           DayHeaderHeight);
                        graphics.FillRectangle(db, cellRect);
                        graphics.DrawRectangle(Pens.Black, cellRect);

                        string dayName = CultureConstants.DefaultCulture.DateTimeFormat.DayNames[(int)cDow];
                        TextRenderer.DrawText(graphics, dayName, boldf,
                                              new Rectangle(cellRect.Left + 1, cellRect.Top + 1, cellRect.Width - 2,
                                                            cellRect.Height - 2),
                                              Color.Black, Color.Transparent,
                                              TextFormatFlags.EndEllipsis | TextFormatFlags.HorizontalCenter |
                                              TextFormatFlags.VerticalCenter);

                        cDow = (DayOfWeek)(((int)cDow + 1) % 7);
                    }
                }
            }

            bool daysRunning = false;
            using (Brush validDayBrush = new LinearGradientBrush(
                new Rectangle(m_calTopLeft.X, m_calTopLeft.Y + HeaderHeight + DayHeaderHeight,
                              m_cellSize.Width * 7, m_cellSize.Height * MaxRows),
                Color.FromArgb(244, 244, 244), Color.FromArgb(203, 220, 228), LinearGradientMode.Vertical))
            {
                for (int y = 0; y < MaxRows; y++)
                {
                    cDow = m_firstDayOfWeek;
                    using (Brush invalidDayBrush = new LinearGradientBrush(
                        new Rectangle(m_calTopLeft.X,
                                      m_calTopLeft.Y + HeaderHeight + DayHeaderHeight + y * m_cellSize.Height,
                                      m_cellSize.Width * 7, m_cellSize.Height),
                        Color.FromArgb(169, 169, 169), Color.FromArgb(140, 140, 140), LinearGradientMode.Vertical))
                    {
                        for (int x = 0; x < 7; x++)
                        {
                            bool isValidDay = false;
                            int dayNum = 0;
                            if (!daysRunning && cDow == firstDayDow)
                                daysRunning = true;

                            if (daysRunning)
                            {
                                if (mdt.Month == m_date.Month)
                                {
                                    isValidDay = true;
                                    dayNum = mdt.Day;
                                    mdt = mdt + TimeSpan.FromDays(1);
                                }
                            }

                            Rectangle cellRect = new Rectangle(m_calTopLeft.X + m_cellSize.Width * x,
                                                               m_calTopLeft.Y + HeaderHeight + DayHeaderHeight +
                                                               m_cellSize.Height * y, m_cellSize.Width,
                                                               m_cellSize.Height);
                            graphics.FillRectangle(isValidDay ? validDayBrush : invalidDayBrush, cellRect);
                            graphics.DrawRectangle(Pens.Black, cellRect);

                            if (isValidDay)
                            {
                                TextRenderer.DrawText(graphics, dayNum.ToString(CultureConstants.DefaultCulture), Font,
                                                      new Point(cellRect.Left + 2, cellRect.Top + 2), Color.Black,
                                                      Color.Transparent,
                                                      TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);

                                DateTime datetime = new DateTime(m_date.Year, m_date.Month, dayNum);
                                PaintMonthEntriesForDay(graphics, datetime, cellRect);
                            }

                            cDow = (DayOfWeek)(((int)cDow + 1) % 7);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether the date at the specified point is a valid date.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public bool IsValidDate(Point point) => Date.Month == GetDateFromPoint(point).Month;

        /// <summary>
        /// Paints the month entries for day.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="datetime">The datetime.</param>
        /// <param name="cellRect">The cell rect.</param>
        protected virtual void PaintMonthEntriesForDay(Graphics graphics, DateTime datetime, Rectangle cellRect)
        {
            // No Implementation
        }

        /// <summary>
        /// Highlights the day.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        private void HighlightDay(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            CalculateCellMetrics();
            DateTime mdt = new DateTime(m_date.Year, m_date.Month, 1);

            int boxNumber = m_date.Day + ((int)mdt.DayOfWeek + (7 - (int)m_firstDayOfWeek)) % 7 - 1;
            int x = boxNumber % 7;
            int y = (int)Math.Floor(boxNumber / 7.0);
            Rectangle cellRect = new Rectangle(m_calTopLeft.X + m_cellSize.Width * x,
                                               m_calTopLeft.Y + HeaderHeight + DayHeaderHeight +
                                               m_cellSize.Height * y, m_cellSize.Width, m_cellSize.Height);
            g.DrawRectangle(Pens.DeepSkyBlue, cellRect);
        }

        /// <summary>
        /// Highlights the today.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        private void HighlightToday(PaintEventArgs e)
        {
            DateTime today = DateTime.Now;
            DateTime mdt = new DateTime(m_date.Year, m_date.Month, 1);
            if (today.Month != mdt.Month || today.Year != mdt.Year)
                return;

            Graphics g = e.Graphics;

            CalculateCellMetrics();

            int boxNumber = today.Day + ((int)mdt.DayOfWeek + (7 - (int)m_firstDayOfWeek)) % 7 - 1;
            int x = boxNumber % 7;
            int y = (int)Math.Floor(boxNumber / 7.0);
            Rectangle cellRect = new Rectangle(m_calTopLeft.X + m_cellSize.Width * x,
                                               m_calTopLeft.Y + HeaderHeight + DayHeaderHeight +
                                               m_cellSize.Height * y, m_cellSize.Width, m_cellSize.Height);
            g.DrawRectangle(Pens.Violet, cellRect);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Click"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            MouseEventArgs mouse = (MouseEventArgs)e;
            Point point = mouse.Location;

            DateTime newDate = GetDateFromPoint(point);
            bool isSameMonth = newDate.Month == m_date.Month;
            if (newDate == new DateTime(0))
                return;

            m_date = newDate;
            Invalidate();

            // Only send out the events if we clicked once
            if (mouse.Clicks == 1)
                DayClicked?.ThreadSafeInvoke(null, new DaySelectedEventArgs(m_date, isSameMonth, mouse, point));
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.DoubleClick"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            MouseEventArgs mouse = (MouseEventArgs)e;
            Point point = mouse.Location;
            DateTime newDate = GetDateFromPoint(point);
            bool isSameMonth = newDate.Month == m_date.Month;
            if (newDate == new DateTime(0))
                return;

            // Only send out the events if we double clicked
            if (mouse.Clicks == 2)
                DayDoubleClicked?.ThreadSafeInvoke(null, new DaySelectedEventArgs(m_date, isSameMonth, mouse, point));
        }

        // 
        /// <summary>
        /// Gets the date under a specific point (used for hover tips etc).
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        private DateTime GetDateFromPoint(Point p)
        {
            // Make sure the member values are set up
            CalculateCellMetrics();

            // Make sure we clicked on the scheduler
            if (p.X < m_calTopLeft.X || p.Y < m_calTopLeft.Y ||
                (p.X > m_calTopLeft.X + m_cellSize.Width * 7) ||
                (p.Y > m_calTopLeft.Y + m_cellSize.Height * MaxRows + HeaderHeight + DayHeaderHeight))
            {
                return new DateTime(0);
            }

            // We need an int value for the first day of the month
            DayOfWeek nFirstDayOfMonth = new DateTime(m_date.Year, m_date.Month, 1).DayOfWeek;
            int nStartDay = nFirstDayOfMonth - m_firstDayOfWeek;

            // Calculate the x/y position over the grid, and hence the day/week number the user is clicking on
            int day = (p.X -= m_calTopLeft.X) / m_cellSize.Width;
            int week = (p.Y -= m_calTopLeft.Y + HeaderHeight + DayHeaderHeight) / m_cellSize.Height;

            if (nStartDay < 0)
                week -= 1;

            day -= nStartDay;

            day += week * 7 + 1;

            DateTime dt = m_date;
            int month = m_date.Month;
            int year = m_date.Year;
            if (day > DateTime.DaysInMonth(dt.Year, dt.Month))
            {
                if (dt.Month + 1 > 12)
                {
                    year++;
                    month = 1;
                }
                else
                    month++;
                day -= DateTime.DaysInMonth(dt.Year, dt.Month);
            }
            else if (day <= 0)
            {
                if (dt.Month - 1 <= 0)
                {
                    year--;
                    month = 12;
                }
                else
                    month--;
                day += DateTime.DaysInMonth(year, month);
            }
            dt = new DateTime(year, month, day);

            return dt;
        }
    }
}