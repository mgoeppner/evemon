using System;
using System.Drawing;
using System.Windows.Forms;

namespace EVEMon.Common.Controls
{
    public class NoFlickerListBox : ListBox
    {
        /// <summary>
        /// The long-press time in milliseconds - change to pull from Windows settings where possible
        /// </summary>
        private const double TOUCH_HOLD_TIME = 250.0;

        private DateTime pointerDown;

        public NoFlickerListBox() : base()
        {
            pointerDown = DateTime.MinValue;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ListBox.DrawItem"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs"/> that contains the event data.</param>
        /*protected override void OnDrawItem(DrawItemEventArgs e)
        {
            var newBounds = new Rectangle(0, 0, e.Bounds.Width, e.Bounds.Height);

            if (newBounds.Width == 0 || newBounds.Height == 0)
                return;

            // stacked using blocks to avoid indentation, don't need to call IDisposable.Dispose explicitly
            using (var currentContext = new BufferedGraphicsContext())
            using (BufferedGraphics bufferedGraphics = currentContext.Allocate(e.Graphics, newBounds))
            {
                var newArgs = new DrawItemEventArgs(bufferedGraphics.Graphics, e.Font,
                    newBounds, e.Index, e.State, e.ForeColor, e.BackColor);

                // Supply the real DrawItem with the off-screen graphics context
                base.OnDrawItem(newArgs);

                NativeMethods.CopyGraphics(e.Graphics, e.Bounds, bufferedGraphics.Graphics, new Point(0, 0));
            }
        }*/
        
        /// <summary>
        /// The list's window procedure.
        /// </summary>
        /// <param name="m">A Windows Message Object.</param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
            case NativeMethods.WM_ERASEBKGND:
                PaintNonItemRegion();
                m.Msg = NativeMethods.WM_NULL;
                break;
            case NativeMethods.WM_POINTERDOWN:
                uint id = (uint)m.WParam & NativeMethods.PT_POINTERID_MASK;
                if (NativeMethods.GetPointerType(id, out int pPointerType))
                {
                    if (pPointerType == NativeMethods.PT_TOUCH)
                        // Touch press down
                        pointerDown = DateTime.UtcNow;
                    else
                        pointerDown = DateTime.MinValue;
                    // Handle the event
                    m.Msg = NativeMethods.WM_NULL;
                    m.Result = new IntPtr(1);
                } else
                    pointerDown = DateTime.MinValue;
                break;
            case NativeMethods.WM_POINTERUP:
                // Check for tap and hold
                if (DateTime.UtcNow.Subtract(pointerDown).TotalMilliseconds >= TOUCH_HOLD_TIME)
                {
                    // Extract position from the event and convert to control coordinates
                    var pos = PointToClient(new Point(m.LParam.ToInt32()));
                    OnMouseDown(new MouseEventArgs(MouseButtons.Right, 1, pos.X, pos.Y, 0));
                }
                // Handle the event
                m.Msg = NativeMethods.WM_NULL;
                m.Result = new IntPtr(1);
                pointerDown = DateTime.MinValue;
                break;
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// Paints the non item region.
        /// </summary>
        private void PaintNonItemRegion()
        {
            using (var g = Graphics.FromHwnd(Handle))
            {
                using (var r = new Region(ClientRectangle))
                {
                    for (int i = 0; i < Items.Count; i++)
                    {
                        Rectangle itemRect = GetItemRectangle(i);
                        r.Exclude(itemRect);
                    }

                    g.FillRegion(SystemBrushes.Window, r);
                }
            }
        }
    }
}
