using System;
using System.Drawing;
using System.Windows.Forms;
using EVEMon.Common.Data;

namespace EVEMon.ImplantControls
{
    /// <summary>
    /// A tooltip used to display the details of an implant.
    /// </summary>
    public partial class ImplantTooltip : Form
    {
        private const TextFormatFlags TooltipFlags =
            TextFormatFlags.WordBreak | TextFormatFlags.Left | TextFormatFlags.EndEllipsis;

        private Implant m_implant;
        private readonly Font m_toolTipFont;
        private readonly Font m_titleFont;
        private const int ToolTipMargin = 5;
        private const int InnerMargin = 10;
        private const int MaxWidth = 250;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ImplantTooltip()
        {
            InitializeComponent();
            m_toolTipFont = new Font(Font, FontStyle.Regular);
            m_titleFont = new Font(Font.FontFamily, 9.75f, FontStyle.Bold);
            ShowInTaskbar = false;
            TopMost = true;
        }

        /// <summary>
        /// Gets or sets the represented implant.
        /// </summary>
        public Implant Implant
        {
            get { return m_implant; }
            set
            {
                if (m_implant == value)
                    return;
                m_implant = value;
                OnImplantChanged();
                Invalidate();
            }
        }

        /// <summary>
        /// When an implant changes, we computes the required size.
        /// </summary>
        private void OnImplantChanged()
        {
            Size proposedSize = new Size(MaxWidth, 1000);
            Size titleSize = TextRenderer.MeasureText(m_implant.Name, m_titleFont, proposedSize);

            proposedSize = new Size(Math.Max(titleSize.Width, MaxWidth) + ToolTipMargin * 2, 1000);
            Size size = TextRenderer.MeasureText(m_implant.Description, m_toolTipFont, proposedSize, TooltipFlags);
            size.Height += titleSize.Height + InnerMargin + ToolTipMargin * 2;
            size.Width = Math.Max(size.Width, proposedSize.Width);

            Size = size;
        }

        /// <summary>
        /// Performs the painting.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Background
            using (SolidBrush brush = new SolidBrush(BackColor))
            {
                e.Graphics.FillRectangle(brush, DisplayRectangle);
            }

            // Border
            using (Pen pen = new Pen(SystemBrushes.WindowFrame))
            {
                Rectangle borderRect = new Rectangle(0, 0, DisplayRectangle.Width - 1, DisplayRectangle.Height - 1);
                e.Graphics.DrawRectangle(pen, borderRect);
            }

            // Title
            Size titleSize = new Size(Width - ToolTipMargin * 2, 1000);
            titleSize = TextRenderer.MeasureText(m_implant.Name, m_titleFont, titleSize);
            TextRenderer.DrawText(e.Graphics, m_implant.Name, m_titleFont,
                                  new Point(ToolTipMargin, ToolTipMargin), SystemColors.ControlText, TooltipFlags);

            // Content
            int top = ToolTipMargin + titleSize.Height + InnerMargin;
            Rectangle rect = new Rectangle(ToolTipMargin, top, Width - 2 * ToolTipMargin, Height - (top + ToolTipMargin));
            TextRenderer.DrawText(e.Graphics, m_implant.Description, m_toolTipFont, rect, SystemColors.ControlText, TooltipFlags);
        }
    }
}