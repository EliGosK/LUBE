﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAP.Framework.Windows.Forms
{
    public partial class TopFormBase : Form
    {
        private const int WS_EX_TOOLWINDOW = 128;
        protected const int MA_NOACTIVATE = 3;
        internal const int SWP_NOSIZE = 1;
        internal const int SWP_NOMOVE = 2;
        internal const int SWP_NOACTIVATE = 16;
        internal const int SWP_SHOWWINDOW = 64;
        internal const int HWND_TOP = 0;
        internal const int HWND_TOPMOST = -1;

        #region Constructor      

        public TopFormBase()
        {
            this.Parent = (Control)null;
            this.TopLevel = true;
            this.ControlBox = false;
            this.StartPosition = FormStartPosition.Manual;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.Visible = false;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        #endregion

        public virtual bool ICapture
        {
            get
            {
                return this.Capture;
            }
            set
            {
                this.Capture = value;
            }
        }

        public virtual Rectangle RealBounds
        {
            get
            {
                return Rectangle.Empty;
            }
            set
            {
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 128;
                return createParams;
            }
        }

        protected virtual bool IsTopMost
        {
            get
            {
                return true;
            }
        }

        protected virtual IntPtr InsertAfterWindow
        {
            get
            {
                return (IntPtr)(this.IsTopMost ? -1 : 0);
            }
        }

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }

        protected virtual bool AllowMouseActivate
        {
            get
            {
                return false;
            }
        }

        

        [SecuritySafeCritical]
        protected virtual void UpdateZOrder(IntPtr after)
        {
            if (!this.Visible)
                return;
            if (after == IntPtr.Zero)
                after = this.InsertAfterWindow;
            uint num = 16;
            Rectangle realBounds = this.RealBounds;
            uint uFlags = num | 3U;
            TopFormBase.SetWindowPos(this.Handle, after, realBounds.X, realBounds.Y, realBounds.Width, realBounds.Height, uFlags);
        }

        protected void SetVisibleCoreBase(bool newVisible)
        {
            base.SetVisibleCore(newVisible);
        }

        [SecuritySafeCritical]
        protected override void SetVisibleCore(bool newVisible)
        {
            this.SetVisibleCoreBase(newVisible);
            if (!newVisible || !this.IsHandleCreated)
                return;
            TopFormBase.SetWindowPos(this.Handle, this.InsertAfterWindow, 0, 0, 0, 0, 83U);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 515:
                    this.OnDoubleClick(EventArgs.Empty);
                    return;
                case 533:
                    if (m.LParam == this.Handle)
                    {
                        this.OnGotCapture();
                        break;
                    }
                    this.OnLostCapture();
                    break;
                case 33:
                    if (!this.AllowMouseActivate)
                    {
                        m.Result = (IntPtr)3;
                        return;
                    }
                    break;
                case 36:
                    this.WMGetMinMazInfo(ref m);
                    return;
            }
            base.WndProc(ref m);
        }

        [SecuritySafeCritical]
        private void WMGetMinMazInfo(ref Message m)
        {
            base.WndProc(ref m);
            NativeMethods.MINMAXINFO from = NativeMethods.MINMAXINFO.GetFrom(m.LParam);
            from.ptMinTrackSize = new NativeMethods.POINT(1, 1);
            Marshal.StructureToPtr((object)from, m.LParam, true);
            m.Result = IntPtr.Zero;
        }

        protected virtual void OnLostCapture()
        {
        }

        protected virtual void OnGotCapture()
        {
        }

        [DllImport("USER32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        [DllImport("USER32.dll")]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    }
}
