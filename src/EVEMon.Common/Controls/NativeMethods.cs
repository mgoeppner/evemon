using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using EVEMon.Common.Extensions;

namespace EVEMon.Common.Controls
{
    public static class NativeMethods
    {
        private const int HWND_TOPMOST = -1;
        private const int GWL_STYLE = -16;
        public const uint SWP_NOSIZE = 0x0001;
        public const uint SWP_NOMOVE = 0x0002;
        private const uint SW_SHOWNOACTIVATE = 0x0004;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint SRCCOPY = 0x00CC0020;
        private const uint WS_VSCROLL = 0x200000;
        private const uint WS_HSCROLL = 0x100000;

        internal const int WM_NULL = 0x0000;
        internal const int WM_SETREDRAW = 0x000B;
        internal const int WM_ERASEBKGND = 0x0014;
        internal const int WM_POINTERDOWN = 0x0246;
        internal const int WM_POINTERUP = 0x0247;

        internal const int PT_POINTER = 0x00000001;
        internal const int PT_TOUCH = 0x00000002;
        internal const int PT_PEN = 0x00000003;
        internal const int PT_MOUSE = 0x00000004;
        internal const uint PT_POINTERID_MASK = 0x0000FFFF;

        [DllImport("psapi.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EmptyWorkingSet(IntPtr proc);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr handle, int messg, IntPtr wparam, IntPtr lparam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr LoadCursorFromFile(string fileName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y,
            int cx, int cy, uint uFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth,
            int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, uint dwRop);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool LockWindowUpdate(IntPtr hWndLock);

        /// <summary>
        /// Retrieves the (touch) pointer event type, given the pointer ID.
        /// </summary>
        /// <param name="pPointerID">The pointer ID from the window event.</param>
        /// <param name="pPointerType">The location where the pointer event type will be placed.</param>
        /// <returns>true if the event type was placed into pPointerType, or false otherwise</returns>
        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPointerType(uint pPointerID, out int pPointerType);

        /// <summary>
        /// Locks or unlocks updates on this control; used to avoid the window blinking problem
        /// which occurs when Suspend/ResumeDrawing is called.
        /// </summary>
        /// <param name="form">The form to update.</param>
        /// <param name="locked">true to lock window drawing updates, or false to enable them.</param>
        public static void LockWindowUpdate(this Control form, bool locked)
        {
            // No op on non-windows
            int platform = (int)Environment.OSVersion.Platform;
            if (platform == 4 || platform == 6 || platform == 128)
                return;

            if (locked)
                LockWindowUpdate(form.Handle);
            else
                LockWindowUpdate(IntPtr.Zero);
        }

        /// <summary>
        /// Show the given form on topmost without activating it.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="uFlags">The u flags.</param>
        /// <exception cref="System.ArgumentNullException">form</exception>
        public static void ShowInactiveTopmost(this Control form, uint uFlags = 0)
        {
            // No op on non-windows
            int platform = (int)Environment.OSVersion.Platform;
            if (platform == 4 || platform == 6 || platform == 128)
                return;

            form.ThrowIfNull(nameof(form));

            // We store the 'left' and 'top' position because for some reason
            // on first execution of 'ShowWindow' the form position gets reset
            int left = form.Left;
            int top = form.Top;

            SetWindowPos(form.Handle, HWND_TOPMOST, left, top, form.Width, form.Height, SWP_NOACTIVATE | uFlags);
            ShowWindow(form.Handle, SW_SHOWNOACTIVATE);
        }

        /// <summary>
        /// Wrapper around BitBlt.
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="destClip">Clipping rectangle on dest</param>
        /// <param name="graphics"></param>
        /// <param name="bltFrom">Upper-left point on src to blt from</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">dest or graphics</exception>
        public static void CopyGraphics(Graphics dest, Rectangle destClip, Graphics graphics, Point bltFrom)
        {
            // No op on non-windows
            int platform = (int)Environment.OSVersion.Platform;
            if (platform == 4 || platform == 6 || platform == 128)
                return;

            dest.ThrowIfNull(nameof(dest));

            graphics.ThrowIfNull(nameof(graphics));

            BitBlt(dest.GetHdc(), destClip.Left, destClip.Top, destClip.Width, destClip.Height,
                graphics.GetHdc(), bltFrom.X, bltFrom.Y, SRCCOPY);
        }


        #region Graphic Text Character Spacing

        /// <summary>
        /// Sets the text character spacing.
        /// </summary>
        /// <param name="graphics">The g.</param>
        /// <param name="spacing">The spacing.</param>
        /// <exception cref="System.ArgumentNullException">graphics</exception>
        public static void SetTextCharacterSpacing(Graphics graphics, int spacing)
        {
            // No op on non-windows
            int platform = (int)Environment.OSVersion.Platform;
            if (platform == 4 || platform == 6 || platform == 128)
                return;

            graphics.ThrowIfNull(nameof(graphics));

            IntPtr hdc = graphics.GetHdc();
            SetTextCharacterExtra(hdc, spacing);
            graphics.ReleaseHdc();
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        private static extern int SetTextCharacterExtra(IntPtr hdc, int nCharExtra);

        #endregion


        #region Scrollbar visibility

        /// <summary>
        /// Gets if the vertical scrollbar is visible on the specified control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public static bool VerticalScrollBarVisible(this Control control)
        {
            // No op on non-windows
            int platform = (int)Environment.OSVersion.Platform;
            if (platform == 4 || platform == 6 || platform == 128)
                return true;

            uint wndStyle = GetWindowLong(control.Handle, GWL_STYLE);
            return (wndStyle & WS_VSCROLL) == WS_VSCROLL;
        }

        /// <summary>
        /// Gets if the horizontal scrollbar is visible on the specified control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public static bool HorizontalScrollBarVisible(this Control control)
        {
            // No op on non-windows
            int platform = (int)Environment.OSVersion.Platform;
            if (platform == 4 || platform == 6 || platform == 128)
                return true;

            uint wndStyle = GetWindowLong(control.Handle, GWL_STYLE);
            return (wndStyle & WS_HSCROLL) == WS_HSCROLL;
        }

        #endregion


        #region ScrollBar positioning

        /// <summary>
        /// Gets the sroll bar position of the list view.
        /// </summary>
        /// <param name="control">The list view.</param>
        /// <returns>The scroll bar position.</returns>
        /// <exception cref="System.ArgumentNullException">control</exception>
        public static int GetVerticalScrollBarPosition(this Control control)
        {
            // No op on non-windows
            int platform = (int)Environment.OSVersion.Platform;
            if (platform == 4 || platform == 6 || platform == 128)
                return 0;

            control.ThrowIfNull(nameof(control));

            Scrollinfo currentInfo = new Scrollinfo();
            currentInfo.cbSize = Marshal.SizeOf(currentInfo);
            currentInfo.fMask = (int)ScrollInfoMask.SIF_ALL;

            GetScrollInfo(control.Handle, (int)ScrollBarDirection.SB_VERT, ref currentInfo);
            return currentInfo.nPos;
        }

        /// <summary>
        /// Sets the scroll bar position of the list view.
        /// </summary>
        /// <param name="control">The list view.</param>
        /// <param name="position">The scroll bar position.</param>
        /// <exception cref="System.ArgumentNullException">control</exception>
        public static void SetVerticalScrollBarPosition(this Control control, int position)
        {
            // No op on non-windows
            int platform = (int)Environment.OSVersion.Platform;
            if (platform == 4 || platform == 6 || platform == 128)
                return;

            control.ThrowIfNull(nameof(control));

            SendMessage(new HandleRef(control, control.Handle), (uint)ListViewMessages.LVM_SCROLL, IntPtr.Zero, (IntPtr)position);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Scrollinfo
        {
            public int cbSize;
            public uint fMask;
            public int nMin;
            public int nMax;
            public uint nPage;
            public int nPos;
            public int nTrackPos;
        }

        private enum ScrollBarDirection
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        [Flags]
        private enum ScrollInfoMask
        {
            SIF_RANGE = 0x1,
            SIF_PAGE = 0x2,
            SIF_POS = 0x4,
            SIF_DISABLENOSCROLL = 0x8,
            SIF_TRACKPOS = 0x10,
            SIF_ALL = SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS
        }

        private enum ListViewMessages
        {
            LVM_FIRST = 0x1000,
            LVM_SCROLL = LVM_FIRST + 20
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetScrollInfo(IntPtr hwnd, int fnBar, ref Scrollinfo lpsi);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(HandleRef hwnd, uint msg, IntPtr wParam, IntPtr lParam);

        #endregion


        #region Tray Icon

        // All definitions taken from http://pinvoke.net
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SHAppBarMessage(uint dwMessage, ref AppBarData pData);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public const string TaskbarClass = "Shell_TrayWnd";

        [StructLayout(LayoutKind.Sequential)]
        internal struct AppBarData
        {
            private int cbSize;
            private readonly IntPtr hWnd;
            private readonly uint uCallbackMessage;
            private readonly uint uEdge;
            private readonly RECT m_rect;
            private readonly int lParam;

            public uint UEdge => uEdge;

            public RECT Rect => m_rect;

            public static AppBarData Create()
            {
                AppBarData appBarData = new AppBarData { cbSize = Marshal.SizeOf(typeof(AppBarData)) };
                return appBarData;
            }
        }

        public const int ABM_QUERYPOS = 2;
        public const int ABM_GETTASKBARPOS = 5;

        public const int ABE_LEFT = 0;
        public const int ABE_TOP = 1;
        public const int ABE_RIGHT = 2;
        public const int ABE_BOTTOM = 3;

        [Serializable, StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="RECT"/> struct.
            /// </summary>
            /// <param name="left">The left.</param>
            /// <param name="top">The top.</param>
            /// <param name="right">The right.</param>
            /// <param name="bottom">The bottom.</param>
            private RECT(int left, int top, int right, int bottom)
                : this()
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            /// <summary>
            /// Gets the left.
            /// </summary>
            /// <value>
            /// The left.
            /// </value>
            public int Left { get; }

            /// <summary>
            /// Gets the top.
            /// </summary>
            /// <value>
            /// The top.
            /// </value>
            public int Top { get; }

            /// <summary>
            /// Gets the right.
            /// </summary>
            /// <value>
            /// The right.
            /// </value>
            public int Right { get; }

            /// <summary>
            /// Gets the bottom.
            /// </summary>
            /// <value>
            /// The bottom.
            /// </value>
            public int Bottom { get; }

            /// <summary>
            /// Gets the height.
            /// </summary>
            /// <value>
            /// The height.
            /// </value>
            public int Height => Bottom - Top + 1;

            /// <summary>
            /// Gets the width.
            /// </summary>
            /// <value>
            /// The width.
            /// </value>
            public int Width => Right - Left + 1;

            /// <summary>
            /// Gets the size.
            /// </summary>
            /// <value>
            /// The size.
            /// </value>
            public Size Size => new Size(Width, Height);

            /// <summary>
            /// Gets the location.
            /// </summary>
            /// <value>
            /// The location.
            /// </value>
            public Point Location => new Point(Left, Top);

            /// <summary>
            /// Handy method for converting to a System.Drawing.Rectangle
            /// </summary>
            /// <returns></returns>
            public Rectangle ToRectangle() => Rectangle.FromLTRB(Left, Top, Right, Bottom);

            /// <summary>
            /// Froms the rectangle.
            /// </summary>
            /// <param name="rectangle">The rectangle.</param>
            /// <returns></returns>
            public static RECT FromRectangle(Rectangle rectangle)
                => new RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);

            public override int GetHashCode()
                => Left ^ ((Top << 13) | (Top >> 0x13))
                   ^ ((Width << 0x1a) | (Width >> 6))
                   ^ ((Height << 7) | (Height >> 0x19));


            #region Operator overloads

            public static implicit operator Rectangle(RECT rect)
                => Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);

            public static implicit operator RECT(Rectangle rect) => new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);

            #endregion
        }

        #endregion


        #region Custom Message Box

        public const int SC_CLOSE = 0xF060;
        public const int MF_BYCOMMAND = 0x0;
        public const int MF_GRAYED = 0x1;
        public const int MF_ENABLED = 0x0;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetSystemMenu(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        #endregion
    }
}
