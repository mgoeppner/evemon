using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using EVEMon.Common.Controls;

namespace EVEMon.SettingsUI
{
    public partial class TrayBaseWindow : Form
    {
        protected readonly FlowLayoutPanel MainFlowLayoutPanel = new FlowLayoutPanel();
        protected readonly Label ToolTipLabel = new Label();
        protected bool UpdatePending;


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TrayBaseWindow"/> class.
        /// </summary>
        protected TrayBaseWindow()
        {
            InitializeComponent();

            SuspendLayout();

            if (this is TrayPopupWindow)
                InitializeTrayPopWindowControls();

            if (this is TrayTooltipWindow)
                InitializeTrayTooltipWindowControls();

            ResumeLayout(false);
            PerformLayout();
        }

        /// <summary>
        /// Initializes the tray pop window controls.
        /// </summary>
        private void InitializeTrayPopWindowControls()
        {
            MainFlowLayoutPanel.AutoSize = true;
            MainFlowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            MainFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            MainFlowLayoutPanel.Location = new Point(4, 4);
            MainFlowLayoutPanel.Margin = new Padding(4);
            MainFlowLayoutPanel.BackColor = SystemColors.ControlLightLight;
            Controls.Add(MainFlowLayoutPanel);
        }

        /// <summary>
        /// Initializes the tray tooltip window controls.
        /// </summary>
        private void InitializeTrayTooltipWindowControls()
        {
            ToolTipLabel.AutoSize = true;
            ToolTipLabel.BackColor = SystemColors.ControlLightLight;
            ToolTipLabel.Location = new Point(4, 4);
            ToolTipLabel.Margin = new Padding(4);
            Controls.Add(ToolTipLabel);
        }

        #endregion

        
        #region Inherited Events

        /// <summary>
        /// On load, update controls.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode)
                return;

            // Look'n feel
            
            UpdateContent();
        }

        /// <summary>
        /// Sets this window as topmost without activating it.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Show the given form on topmost without activating it
            this.ShowInactiveTopmost(NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE);
        }

        /// <summary>
        /// On visible, checks whether an update is pending.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible && UpdatePending)
                UpdateContent();
        }

        /// <summary>
        /// Draws the rounded rectangle border and background.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Create graphics object to work with
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            // Define the size of the rectangle used for each of the 4 corner arcs.
            const int Radius = 4;
            Size cornerSize = new Size(Radius * 2, Radius * 2);

            // Draw the background and border line
            DrawBackground(g, cornerSize);
            DrawBorder(g, cornerSize);
        }

        /// <summary>
        /// Draws the rounded background.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="cornerSize">Size of the corner.</param>
        private void DrawBackground(Graphics g, Size cornerSize)
        {
            // Construct a GraphicsPath for the form
            using (GraphicsPath path = new GraphicsPath())
            {
                path.StartFigure();

                // Top left
                path.AddArc(new Rectangle(0, 0, cornerSize.Width, cornerSize.Height), 180, 90);

                // Top Right
                path.AddArc(new Rectangle(ClientRectangle.Width - cornerSize.Width, 0, cornerSize.Width, cornerSize.Height),
                            270, 90);

                // Bottom right
                path.AddArc(new Rectangle(ClientRectangle.Width - cornerSize.Width,
                                          ClientRectangle.Height - cornerSize.Height, cornerSize.Width, cornerSize.Height),
                            0, 90);

                // Bottom Left
                path.AddArc(new Rectangle(0, ClientRectangle.Height - cornerSize.Height,
                                          cornerSize.Width, cornerSize.Height), 90, 90);
                path.CloseFigure();

                Region = new Region(path);

                // Fill the background
                using (Brush fillBrush = new SolidBrush(SystemColors.ControlLightLight))
                {
                    g.FillPath(fillBrush, path);
                }
            }
        }

        /// <summary>
        /// Draws the rounded rectangle border.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="cornerSize"></param>
        private void DrawBorder(Graphics g, Size cornerSize)
        {
            // Construct a GraphicsPath for the border line
            using (GraphicsPath path = new GraphicsPath())
            {
                path.StartFigure();

                // Top left
                path.AddArc(new Rectangle(0, 0, cornerSize.Width, cornerSize.Height), 180, 90);

                // Top Right
                path.AddArc(new Rectangle(ClientRectangle.Width - cornerSize.Width - 1, 0, cornerSize.Width, cornerSize.Height),
                            270, 90);

                // Bottom right
                path.AddArc(new Rectangle(ClientRectangle.Width - cornerSize.Width - 1,
                                          ClientRectangle.Height - cornerSize.Height, cornerSize.Width,
                                          cornerSize.Height),
                            0, 90);

                // Bottom Left
                path.AddArc(new Rectangle(0, ClientRectangle.Height - cornerSize.Height,
                                          cornerSize.Width, cornerSize.Height), 90, 90);
                path.CloseFigure();

                // Draw the border
                g.DrawPath(SystemPens.WindowFrame, path);
            }
        }

        #endregion

        /// <summary>
        /// Updates the content.
        /// </summary>
        protected virtual void UpdateContent(){}

    }
}
