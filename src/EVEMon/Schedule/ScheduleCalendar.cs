using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using EVEMon.Common.Extensions;
using EVEMon.Common.Scheduling;

namespace EVEMon.Schedule
{
    public class ScheduleCalendar : CalendarControl
    {
        private readonly Collection<ScheduleEntry> m_entries = new Collection<ScheduleEntry>();

        private const int LegendX = 5;
        private const int LegendY = 5;
        private const int LegendWidth = 200;
        private const int LegendHeight = 40;
        private const int LegendPadding = 5;
        private const int LegendBox = 10;
        private const int LegendSpacingX = 110;
        private const int LegendSpacingY = 18;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleCalendar"/> class.
        /// </summary>
        public ScheduleCalendar()
        {
            RecurringColor2 = Color.LightGreen;
            RecurringColor = Color.Green;
            SingleColor2 = Color.LightBlue;
            SingleColor = Color.Blue;
            BlockingColor = Color.Red;
            TextColor = Color.White;
            EntryFont = new Font(Font.FontFamily, 7.0f);
        }

        /// <summary>
        /// Gets or sets the entry font.
        /// </summary>
        /// <value>The entry font.</value>
        public Font EntryFont { get; set; }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the blocking.
        /// </summary>
        /// <value>The color of the blocking.</value>
        public Color BlockingColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the single.
        /// </summary>
        /// <value>The color of the single.</value>
        public Color SingleColor { get; set; }

        /// <summary>
        /// Gets or sets the single color2.
        /// </summary>
        /// <value>The single color2.</value>
        public Color SingleColor2 { get; set; }

        /// <summary>
        /// Gets or sets the color of the recurring.
        /// </summary>
        /// <value>The color of the recurring.</value>
        public Color RecurringColor { get; set; }

        /// <summary>
        /// Gets or sets the recurring color2.
        /// </summary>
        /// <value>The recurring color2.</value>
        public Color RecurringColor2 { get; set; }

        /// <summary>
        /// Gets the entries.
        /// </summary>
        /// <value>The entries.</value>
        public Collection<ScheduleEntry> Entries => m_entries;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            EntryFont.Dispose();
            EntryFont = null;
        }

        /// <summary>
        /// Paints the month entries for day.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="datetime">The datetime.</param>
        /// <param name="cellRect">The cell rect.</param>
        /// <exception cref="System.ArgumentNullException">graphics</exception>
        protected override void PaintMonthEntriesForDay(Graphics graphics, DateTime datetime, Rectangle cellRect)
        {
            graphics.ThrowIfNull(nameof(graphics));

            List<ScheduleEntry> todays = m_entries.Where(entry => entry.IsToday(datetime)).ToList();

            // Sort Todays Entries Alphabetically
            todays.Sort(new ScheduleEntryTitleComparer());

            Rectangle rect = cellRect;
            rect.X++;
            rect.Y += 14;
            rect.Width -= 1;
            rect.Height = 11;


            List<ScheduleEntry>.Enumerator e = todays.GetEnumerator();
            if (!e.MoveNext())
                return;

            while (e.Current != null)
            {
                ScheduleEntry entry = e.Current;

                if (entry is SimpleScheduleEntry)
                {
                    // Setup a nice Brush
                    Brush fillBrush = (entry.Options & ScheduleEntryOptions.Blocking) != 0
                                          ? new LinearGradientBrush(new Point(rect.X, rect.Y),
                                                                    new Point(rect.X + rect.Width, rect.Y + rect.Height),
                                                                    BlockingColor, SingleColor2)
                                          : new LinearGradientBrush(new Point(rect.X, rect.Y),
                                                                    new Point(rect.X + rect.Width, rect.Y + rect.Height),
                                                                    SingleColor, SingleColor2);

                    using (fillBrush)
                    {
                        // Check if the text fits
                        Size textsize = TextRenderer.MeasureText(entry.Title, EntryFont);
                        if (textsize.Width <= rect.Width)
                        {
                            graphics.FillRectangle(fillBrush, rect);
                            TextRenderer.DrawText(graphics, entry.Title, EntryFont, new Point(rect.X + 1, rect.Y), TextColor);
                        }
                        else
                        {
                            // Make sure the text fits
                            string shorttext = entry.Title + "..";
                            for (int i = entry.Title.Length - 1; i > 4; i--)
                            {
                                shorttext = entry.Title.Substring(0, i) + "..";
                                textsize = TextRenderer.MeasureText(shorttext, EntryFont);
                                if (textsize.Width <= rect.Width)
                                    break;
                            }
                            graphics.FillRectangle(fillBrush, rect);
                            TextRenderer.DrawText(graphics, shorttext, EntryFont, new Point(rect.X + 1, rect.Y), TextColor);
                        }
                    }
                }
                else if (entry is RecurringScheduleEntry)
                {
                    // Setup a nice Brush
                    Brush fillBrush = (entry.Options & ScheduleEntryOptions.Blocking) != 0
                                          ? new LinearGradientBrush(new Point(rect.X, rect.Y),
                                                                    new Point(rect.X + rect.Width, rect.Y + rect.Height),
                                                                    BlockingColor, RecurringColor2)
                                          : new LinearGradientBrush(new Point(rect.X, rect.Y),
                                                                    new Point(rect.X + rect.Width, rect.Y + rect.Height),
                                                                    RecurringColor, RecurringColor2);

                    using (fillBrush)
                    {
                        Size textsize = TextRenderer.MeasureText(entry.Title, EntryFont);
                        if (textsize.Width <= rect.Width)
                        {
                            graphics.FillRectangle(fillBrush, rect);
                            TextRenderer.DrawText(graphics, entry.Title, EntryFont, new Point(rect.X + 1, rect.Y), TextColor);
                        }
                        else
                        {
                            // Make sure the text fits
                            string shorttext = entry.Title + "..";
                            for (int i = entry.Title.Length - 1; i > 4; i--)
                            {
                                shorttext = entry.Title.Substring(0, i) + "..";
                                textsize = TextRenderer.MeasureText(shorttext, EntryFont);
                                if (textsize.Width <= rect.Width)
                                    break;
                            }
                            graphics.FillRectangle(fillBrush, rect);
                            TextRenderer.DrawText(graphics, shorttext, EntryFont, new Point(rect.X + 1, rect.Y), TextColor);
                        }
                    }
                }

                rect.Y += rect.Height + 1;

                // Check if we have room for one more entry?
                if (rect.Y + rect.Height > cellRect.Y + cellRect.Height)
                {
                    // No, are there more entries?
                    if (e.MoveNext())
                    {
                        // Yes, Draw something to let the user know
                        int toomuch = rect.Y + rect.Height - (cellRect.Y + cellRect.Height);
                        rect.Height -= toomuch;

                        // Make sure LinearGradientBrush doesn't get into any troubles (Out Of Memory Because both points are at the same position)
                        if (rect.Height == 0)
                            rect.Height = 1;


                        using (
                            Brush brush = new LinearGradientBrush(new Point(rect.X, rect.Y),
                                                                  new Point(rect.X, rect.Y + rect.Height), Color.Gray,
                                                                  Color.LightGray))
                        {
                            graphics.FillRectangle(brush, rect);
                        }
                    }
                    break;
                }

                // Yes, we have more room
                e.MoveNext();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data. </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Paint the Calendar
            base.OnPaint(e);

            // Paint some kind of Legend
            Graphics g = e.Graphics;

            Rectangle r = new Rectangle { X = LegendX, Y = LegendY, Height = LegendHeight, Width = LegendWidth };
            g.FillRectangle(Brushes.White, r);
            g.DrawRectangle(Pens.Black, r);

            r.X = LegendX + LegendPadding;
            r.Y = LegendY + LegendPadding;
            r.Width = LegendBox * 2;
            r.Height = LegendBox;
            using (
                Brush b = new LinearGradientBrush(new Point(r.X, r.Y), new Point(r.X + r.Width, r.Y), SingleColor, SingleColor2))
            {
                g.FillRectangle(b, r);
                g.DrawRectangle(Pens.Black, r);
                TextRenderer.DrawText(g, "Single Entry", Font, new Point(r.X + r.Width + 2, r.Y), Color.Black);
            }

            r.Y += LegendSpacingY;
            using (
                Brush b = new LinearGradientBrush(new Point(r.X, r.Y), new Point(r.X + r.Width, r.Y), RecurringColor,
                                                  RecurringColor2))
            {
                g.FillRectangle(b, r);
                g.DrawRectangle(Pens.Black, r);
                TextRenderer.DrawText(g, "Recurring Entry", Font, new Point(r.X + r.Width + 2, r.Y), Color.Black);
            }

            r.Y = LegendY + LegendPadding;
            r.X += LegendSpacingX;
            using (Brush b = new SolidBrush(BlockingColor))
            {
                g.FillRectangle(b, r);
                g.DrawRectangle(Pens.Black, r);
                TextRenderer.DrawText(g, "Blocked", Font, new Point(r.X + r.Width + 2, r.Y), Color.Black);
            }
        }
    }
}