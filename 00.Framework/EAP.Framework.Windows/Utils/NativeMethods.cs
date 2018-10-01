using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAP.Framework.Windows
{
    [SecuritySafeCritical]
    public class NativeMethods
    {
        public static int SYSRGN = 4;
        public const int SPI_GETNONCLIENTMETRICS = 41;
        public const int EM_SETWORDBREAKPROC = 208;
        public const int EM_GETWORDBREAKPROC = 209;
        public const int WB_LEFT = 0;
        public const int WB_RIGHT = 1;
        public const int WB_ISDELIMITER = 2;
        public const int RGN_AND = 1;
        public const int RGN_OR = 2;
        public const int RGN_XOR = 3;
        public const int RGN_DIFF = 4;
        public const int RGN_COPY = 5;
        public const int MAX_PATH = 260;
        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;
        private static bool? isWindow10;

        public static bool IsWindow10
        {
            get
            {
                if (NativeMethods.isWindow10.HasValue)
                    return NativeMethods.isWindow10.Value;
                string lptstrFilename = Environment.GetEnvironmentVariable("windir") + "\\System32\\kernel32.dll";
                int dwSize = 0;
                int fileVersionInfoSize = NativeMethods.GetFileVersionInfoSize(lptstrFilename, out dwSize);
                byte[] numArray = new byte[fileVersionInfoSize];
                if (!NativeMethods.GetFileVersionInfo(lptstrFilename, 0, fileVersionInfoSize, numArray))
                {
                    NativeMethods.isWindow10 = new bool?(false);
                    return NativeMethods.isWindow10.Value;
                }
                IntPtr lplpBuffer = IntPtr.Zero;
                int puLen = 0;
                NativeMethods.VerQueryValue(numArray, "\\VarFileInfo\\Translation", out lplpBuffer, out puLen);
                NativeMethods.LanguageCodePage structure = (NativeMethods.LanguageCodePage)Marshal.PtrToStructure(lplpBuffer, typeof(NativeMethods.LanguageCodePage));
                string lpSubBlock = string.Format("\\StringFileInfo\\{0:X4}{1:X4}\\ProductVersion", (object)structure.language, (object)structure.codePage);
                bool flag = NativeMethods.VerQueryValue(numArray, lpSubBlock, out lplpBuffer, out puLen);
                string stringAnsi = Marshal.PtrToStringAnsi(lplpBuffer);
                if (!flag)
                {
                    NativeMethods.isWindow10 = new bool?(false);
                    return NativeMethods.isWindow10.Value;
                }
                NativeMethods.isWindow10 = new bool?(stringAnsi.StartsWith("10."));
                return NativeMethods.isWindow10.Value;
            }
        }
       
        public static bool VerQueryValue(byte[] pBlock, string lpSubBlock, out IntPtr lplpBuffer, out int puLen)
        {
            return NativeMethods.UnsafeNativeMethods.VerQueryValue(pBlock, lpSubBlock, out lplpBuffer, out puLen);
        }

        public static int GetFileVersionInfoSize(string lptstrFilename, out int dwSize)
        {
            return NativeMethods.UnsafeNativeMethods.GetFileVersionInfoSize(lptstrFilename, out dwSize);
        }

        public static bool GetFileVersionInfo(string lptstrFilename, int dwHandleIgnored, int dwLen, byte[] lpData)
        {
            return NativeMethods.UnsafeNativeMethods.GetFileVersionInfo(lptstrFilename, dwHandleIgnored, dwLen, lpData);
        }

        public static int SetWindowSubclass(IntPtr hWnd, NativeMethods.Win32SubClassProc newProc, IntPtr uIdSubclass, IntPtr dwRefData)
        {
            return NativeMethods.UnsafeNativeMethods.SetWindowSubclass(hWnd, newProc, uIdSubclass, dwRefData);
        }

        public static IntPtr GetFocus()
        {
            return NativeMethods.UnsafeNativeMethods.GetFocus();
        }

        public static int RemoveWindowSubclass(IntPtr hWnd, NativeMethods.Win32SubClassProc newProc, IntPtr uIdSubclass)
        {
            return NativeMethods.UnsafeNativeMethods.RemoveWindowSubclass(hWnd, newProc, uIdSubclass);
        }

        public static IntPtr DefSubclassProc(IntPtr hWnd, IntPtr Msg, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethods.UnsafeNativeMethods.DefSubclassProc(hWnd, Msg, wParam, lParam);
        }

        public static bool FlashWindowEx(IntPtr hWnd, NativeMethods.FLASHW flags, int count, int timeout)
        {
            NativeMethods.FLASHWINFO info = new NativeMethods.FLASHWINFO();
            info.cbSize = (uint)Marshal.SizeOf((object)info);
            info.hwnd = hWnd;
            info.dwFlags = (uint)flags;
            info.uCount = (uint)count;
            info.dwTimeout = (uint)timeout;
            return NativeMethods.FlashWindowEx(info);
        }

        internal static bool FlashWindowEx(NativeMethods.FLASHWINFO info)
        {
            return NativeMethods.UnsafeNativeMethods.FlashWindowEx(ref info);
        }

        internal static int SHAppBarMessage(int dwMessage, ref NativeMethods.APPBARDATA pData)
        {
            return NativeMethods.UnsafeNativeMethods.SHAppBarMessage(dwMessage, ref pData);
        }

        public static bool DrawMenuBar(IntPtr hWnd)
        {
            return NativeMethods.UnsafeNativeMethods.DrawMenuBar(hWnd);
        }

        public static short GlobalAddAtom(string lpString)
        {
            return (short)NativeMethods.UnsafeNativeMethods.GlobalAddAtom(lpString);
        }

        public static IntPtr RemoveProp(IntPtr hWnd, string lpString)
        {
            return NativeMethods.UnsafeNativeMethods.RemoveProp(hWnd, lpString);
        }

        public static IntPtr GetCapture()
        {
            return NativeMethods.UnsafeNativeMethods.GetCapture();
        }

        public static bool SetProp(IntPtr hWnd, string lpString, IntPtr hData)
        {
            return NativeMethods.UnsafeNativeMethods.SetProp(hWnd, lpString, hData);
        }

        public static IntPtr LoadImage(IntPtr hinst, int iconId, int uType, int cxDesired, int cyDesired, int fuLoad)
        {
            return NativeMethods.UnsafeNativeMethods.LoadImage(hinst, iconId, (uint)uType, cxDesired, cyDesired, (uint)fuLoad);
        }

        public static int DestroyIcon(IntPtr hIcon)
        {
            return NativeMethods.UnsafeNativeMethods.DestroyIcon(hIcon);
        }

        public static IntPtr CreateWindowEx(int dwExStyle, IntPtr classAtom, string lpWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam)
        {
            return NativeMethods.UnsafeNativeMethods.CreateWindowEx(dwExStyle, classAtom, lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, hMenu, hInstance, lpParam);
        }

        public static bool DestroyWindow(IntPtr hWnd)
        {
            return NativeMethods.UnsafeNativeMethods.DestroyWindow(hWnd);
        }

        public static IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethods.UnsafeNativeMethods.DefWindowProc(hWnd, msg, wParam, lParam);
        }

        public static int RegisterClass(ref NativeMethods.WNDCLASS lpWndClass)
        {
            return NativeMethods.UnsafeNativeMethods.RegisterClass(ref lpWndClass);
        }

        public static bool UnregisterClass(IntPtr classAtom, IntPtr hInstance)
        {
            return NativeMethods.UnsafeNativeMethods.UnregisterClass(classAtom, hInstance);
        }

        public static int RestoreDC(IntPtr hdc, int savedDC)
        {
            return NativeMethods.UnsafeNativeMethods.RestoreDC(hdc, savedDC);
        }

        public static int SaveDC(IntPtr hdc)
        {
            return NativeMethods.UnsafeNativeMethods.SaveDC(hdc);
        }

        public static int BitBlt(HandleRef hDC, int x, int y, int nWidth, int nHeight, HandleRef hSrcDC, int xSrc, int ySrc, int dwRop)
        {
            return NativeMethods.UnsafeNativeMethods.BitBlt(hDC, x, y, nWidth, nHeight, hSrcDC, xSrc, ySrc, dwRop);
        }

        public static int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop)
        {
            return NativeMethods.UnsafeNativeMethods.BitBlt(hDC, x, y, nWidth, nHeight, hSrcDC, xSrc, ySrc, dwRop);
        }

        public static bool AlphaBlend(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hdcSrc, int xSrc, int ySrc, int nWidthSrc, int nHeightSrc, NativeMethods.BLENDFUNCTION blendFunction)
        {
            return NativeMethods.UnsafeNativeMethods.AlphaBlend(hDC, x, y, nWidth, nHeight, hdcSrc, xSrc, ySrc, nWidthSrc, nHeightSrc, blendFunction);
        }

        public static IntPtr SelectObject(HandleRef hdc, HandleRef obj)
        {
            return NativeMethods.UnsafeNativeMethods.SelectObject(hdc, obj);
        }

        public static bool GetClientRect(IntPtr hWnd, out NativeMethods.RECT rect)
        {
            return NativeMethods.UnsafeNativeMethods.GetClientRect(hWnd, out rect);
        }

        public static IntPtr SelectObject(IntPtr hdc, IntPtr obj)
        {
            return NativeMethods.UnsafeNativeMethods.SelectObject(hdc, obj);
        }

        public static IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse)
        {
            return NativeMethods.UnsafeNativeMethods.CreateRoundRectRgn(nLeftRect, nTopRect, nRightRect, nBottomRect, nWidthEllipse, nHeightEllipse);
        }

        public static int GetDIBits(HandleRef hdc, HandleRef hbm, int arg1, int arg2, IntPtr arg3, ref NativeMethods.BITMAPINFO_FLAT bmi, int arg5)
        {
            return NativeMethods.UnsafeNativeMethods.GetDIBits(hdc, hbm, arg1, arg2, arg3, ref bmi, arg5);
        }

        public static IntPtr CreateCompatibleBitmap(IntPtr hDC, int width, int height)
        {
            return NativeMethods.UnsafeNativeMethods.CreateCompatibleBitmap(hDC, width, height);
        }

        public static IntPtr CreateCompatibleBitmap(HandleRef hDC, int width, int height)
        {
            return NativeMethods.UnsafeNativeMethods.CreateCompatibleBitmap(hDC, width, height);
        }

        public static IntPtr CreateCompatibleDC(HandleRef hDC)
        {
            return NativeMethods.UnsafeNativeMethods.CreateCompatibleDC(hDC);
        }        

        public static IntPtr CreateCompatibleDC(IntPtr hDC)
        {
            return NativeMethods.UnsafeNativeMethods.CreateCompatibleDC(hDC);
        }

        public static bool DeleteDC(HandleRef hDC)
        {
            return NativeMethods.UnsafeNativeMethods.DeleteDC(hDC);
        }

        public static bool DeleteDC(IntPtr hDC)
        {
            return NativeMethods.UnsafeNativeMethods.DeleteDC(hDC);
        }

        public static bool DeleteObject(HandleRef hObject)
        {
            return NativeMethods.UnsafeNativeMethods.DeleteObject(hObject);
        }

        public static bool DeleteObject(IntPtr hObject)
        {
            return NativeMethods.UnsafeNativeMethods.DeleteObject(hObject);
        }

        public static IntPtr CreateDIBSection(HandleRef hdc, ref NativeMethods.BITMAPINFO_FLAT bmi, int iUsage, ref IntPtr ppvBits, IntPtr hSection, int dwOffset)
        {
            return NativeMethods.UnsafeNativeMethods.CreateDIBSection(hdc, ref bmi, iUsage, ref ppvBits, hSection, dwOffset);
        }

        public static IntPtr CreateDIBSection(IntPtr hdc, ref NativeMethods.BITMAPINFO_SMALL bmi, int iUsage, int pvvBits, IntPtr hSection, int dwOffset)
        {
            return NativeMethods.UnsafeNativeMethods.CreateDIBSection(hdc, ref bmi, iUsage, pvvBits, hSection, dwOffset);
        }

        public static int GetPaletteEntries(IntPtr hPal, int startIndex, int entries, byte[] palette)
        {
            return NativeMethods.UnsafeNativeMethods.GetPaletteEntries(hPal, startIndex, entries, palette);
        }

        public static bool TrackMouseEvent(NativeMethods.TRACKMOUSEEVENTStruct tme)
        {
            return NativeMethods.UnsafeNativeMethods._TrackMouseEvent(tme);
        }

        public static IntPtr TrackPopupMenu(IntPtr menuHandle, int uFlags, int x, int y, int nReserved, IntPtr hwnd, IntPtr par)
        {
            return NativeMethods.UnsafeNativeMethods.TrackPopupMenu(menuHandle, uFlags, x, y, nReserved, hwnd, par);
        }

        public static bool GetViewportOrgEx(IntPtr hDC, ref NativeMethods.POINT point)
        {
            return NativeMethods.UnsafeNativeMethods.GetViewportOrgEx(hDC, ref point);
        }

        public static bool ScrollWindowEx(IntPtr hWnd, int nXAmount, int nYAmount, NativeMethods.RECT rectScrollRegion, ref NativeMethods.RECT rectClip, IntPtr hrgnUpdate, ref NativeMethods.RECT prcUpdate, int flags)
        {
            return NativeMethods.UnsafeNativeMethods.ScrollWindowEx(hWnd, nXAmount, nYAmount, rectScrollRegion, ref rectClip, hrgnUpdate, ref prcUpdate, flags);
        }

        public static bool ScrollWindowEx(IntPtr hWnd, int nXAmount, int nYAmount, IntPtr rectScrollRegion, ref NativeMethods.RECT rectClip, IntPtr hrgnUpdate, ref NativeMethods.RECT prcUpdate, int flags)
        {
            return NativeMethods.UnsafeNativeMethods.ScrollWindowEx(hWnd, nXAmount, nYAmount, rectScrollRegion, ref rectClip, hrgnUpdate, ref prcUpdate, flags);
        }

        public static int ScrollWindowEx(IntPtr hWnd, int dx, int dy, ref NativeMethods.RECT scrollRect, ref NativeMethods.RECT clipRect, IntPtr hrgnUpdate, IntPtr updateRect, int flags)
        {
            return NativeMethods.UnsafeNativeMethods.ScrollWindowEx(hWnd, dx, dy, ref scrollRect, ref clipRect, hrgnUpdate, updateRect, flags);
        }

        public static bool ScrollWindow(IntPtr hWnd, int nXAmount, int nYAmount, ref NativeMethods.RECT rectScrollRegion, ref NativeMethods.RECT rectClip)
        {
            return NativeMethods.UnsafeNativeMethods.ScrollWindow(hWnd, nXAmount, nYAmount, ref rectScrollRegion, ref rectClip);
        }

        public static int ReleaseDC(IntPtr hWnd, IntPtr hDC)
        {
            return NativeMethods.UnsafeNativeMethods.ReleaseDC(hWnd, hDC);
        }

        public static IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, int flags)
        {
            return NativeMethods.UnsafeNativeMethods.GetDCEx(hWnd, hrgnClip, flags);
        }

        public static IntPtr GetWindowDC(IntPtr hWnd)
        {
            return NativeMethods.UnsafeNativeMethods.GetWindowDC(hWnd);
        }

        public static int GetClassLong(IntPtr hWnd, int flags)
        {
            return NativeMethods.UnsafeNativeMethods.GetClassLong(hWnd, flags);
        }

        public static int GetWindowLong(IntPtr hWnd, int flags)
        {
            return NativeMethods.UnsafeNativeMethods.GetWindowLong(hWnd, flags);
        }

        public static int SetWindowLong(IntPtr hWnd, int flags, int val)
        {
            return NativeMethods.UnsafeNativeMethods.SetWindowLong(hWnd, flags, val);
        }

        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            return NativeMethods.UnsafeNativeMethods.SetWindowLong(hWnd, nIndex, dwNewLong);
        }

        public static IntPtr GetDesktopWindow()
        {
            return NativeMethods.UnsafeNativeMethods.GetDesktopWindow();
        }

        public static bool RedrawWindow(IntPtr hWnd, IntPtr rcUpdate, IntPtr hrgnUpdate, int flags)
        {
            return NativeMethods.UnsafeNativeMethods.RedrawWindow(hWnd, rcUpdate, hrgnUpdate, flags);
        }

        public static short GetAsyncKeyState(int vKey)
        {
            return NativeMethods.UnsafeNativeMethods.GetAsyncKeyState(vKey);
        }

        public static bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int uFlags)
        {
            return NativeMethods.UnsafeNativeMethods.SetWindowPos(hWnd, hWndInsertAfter, x, y, cx, cy, uFlags);
        }

        public static int SetCapture(IntPtr hWnd)
        {
            return NativeMethods.UnsafeNativeMethods.SetCapture(hWnd);
        }

        public static bool ReleaseCapture()
        {
            return NativeMethods.UnsafeNativeMethods.ReleaseCapture();
        }

        public static bool IsWindowVisible(IntPtr hWnd)
        {
            return NativeMethods.UnsafeNativeMethods.IsWindowVisible(hWnd);
        }

        public static IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, int lParam)
        {
            return NativeMethods.UnsafeNativeMethods.SendMessage(hWnd, Msg, wParam, lParam);
        }

        public static bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow)
        {
            return NativeMethods.UnsafeNativeMethods.ShowScrollBar(hWnd, wBar, bShow);
        }

        public static IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam)
        {
            return NativeMethods.UnsafeNativeMethods.SendMessage(hWnd, Msg, wParam, lParam);
        }

        public static int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethods.UnsafeNativeMethods.SendMessage(hWnd, Msg, wParam, lParam);
        }

        public static int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref NativeMethods.COPYDATASTRUCT cds)
        {
            return NativeMethods.UnsafeNativeMethods.SendMessage(hWnd, Msg, wParam, ref cds);
        }

        public static int PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethods.UnsafeNativeMethods.PostMessage(hWnd, Msg, wParam, lParam);
        }

        public static bool IsZoomed(IntPtr hWnd)
        {
            return NativeMethods.UnsafeNativeMethods.IsZoomed(hWnd);
        }

        public static bool IsIconic(IntPtr hWnd)
        {
            return NativeMethods.UnsafeNativeMethods.IsIconic(hWnd);
        }

        public static bool GetWindowRect(IntPtr hWnd, ref NativeMethods.RECT lpRect)
        {
            return NativeMethods.UnsafeNativeMethods.GetWindowRect(hWnd, ref lpRect);
        }

        public static bool ValidateRect(IntPtr hWnd, ref NativeMethods.RECT lpRect)
        {
            return NativeMethods.UnsafeNativeMethods.ValidateRect(hWnd, ref lpRect);
        }

        public static IntPtr BeginPaint(IntPtr hWnd, [In, Out] ref NativeMethods.PAINTSTRUCT lpPaint)
        {
            return NativeMethods.UnsafeNativeMethods.BeginPaint(hWnd, ref lpPaint);
        }

        public static bool EndPaint(IntPtr hWnd, ref NativeMethods.PAINTSTRUCT lpPaint)
        {
            return NativeMethods.UnsafeNativeMethods.EndPaint(hWnd, ref lpPaint);
        }

        public static bool LockWindowUpdate(IntPtr hWnd)
        {
            return NativeMethods.UnsafeNativeMethods.LockWindowUpdate(hWnd);
        }

        public static int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw)
        {
            return NativeMethods.UnsafeNativeMethods.SetWindowRgn(hWnd, hRgn, redraw);
        }

        public static int GetClipBox(IntPtr hdc, out NativeMethods.RECT lprc)
        {
            return NativeMethods.UnsafeNativeMethods.GetClipBox(hdc, out lprc);
        }

        public static int CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1, IntPtr hrgnSrc2, int fnCombineMode)
        {
            return NativeMethods.UnsafeNativeMethods.CombineRgn(hrgnDest, hrgnSrc1, hrgnSrc2, fnCombineMode);
        }

        public static int ExcludeClipRect(IntPtr hdc, int left, int top, int right, int bottom)
        {
            return NativeMethods.UnsafeNativeMethods.ExcludeClipRect(hdc, left, top, right, bottom);
        }

        public static int GetClipRgn(IntPtr hdc, IntPtr hrgn)
        {
            return NativeMethods.UnsafeNativeMethods.GetClipRgn(hdc, hrgn);
        }

        public static int SelectClipRgn(IntPtr hdc, IntPtr hrgn)
        {
            return NativeMethods.UnsafeNativeMethods.SelectClipRgn(hdc, hrgn);
        }

        public static int ExtSelectClipRgn(IntPtr hdc, IntPtr hrgn, int mode)
        {
            return NativeMethods.UnsafeNativeMethods.ExtSelectClipRgn(hdc, hrgn, mode);
        }

        public static bool LPtoDP(IntPtr hdc, [In, Out] NativeMethods.POINT[] lpPoints, int nCount)
        {
            return NativeMethods.UnsafeNativeMethods.LPtoDP(hdc, lpPoints, nCount);
        }

        public static int GetUpdateRect(IntPtr hWnd, ref NativeMethods.RECT lpRect, bool erase)
        {
            return NativeMethods.UnsafeNativeMethods.GetUpdateRect(hWnd, ref lpRect, erase);
        }

        public static bool GetUpdateRgn(IntPtr hWnd, IntPtr hrgn, bool erase)
        {
            return NativeMethods.UnsafeNativeMethods.GetUpdateRgn(hWnd, hrgn, erase);
        }

        public static int GetRegionData(IntPtr hRgn, int dwCount, IntPtr lpRgnData)
        {
            return NativeMethods.UnsafeNativeMethods.GetRegionData(hRgn, dwCount, lpRgnData);
        }

        public static int OffsetRgn(IntPtr hRgn, int nXOffset, int nYOffset)
        {
            return NativeMethods.UnsafeNativeMethods.OffsetRgn(hRgn, nXOffset, nYOffset);
        }

        public static int MapWindowPoints(IntPtr hwndFrom, IntPtr hwndTo, ref NativeMethods.POINT lpPoints, [MarshalAs(UnmanagedType.U4)] int cPoints)
        {
            return NativeMethods.UnsafeNativeMethods.MapWindowPoints(hwndFrom, hwndTo, ref lpPoints, cPoints);
        }

        public static int GetRandomRgn(IntPtr hdc, IntPtr hrgn, int iNum)
        {
            return NativeMethods.UnsafeNativeMethods.GetRandomRgn(hdc, hrgn, iNum);
        }

        public static IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect)
        {
            return NativeMethods.UnsafeNativeMethods.CreateRectRgn(nLeftRect, nTopRect, nRightRect, nBottomRect);
        }

        public static bool RectVisible(IntPtr hdc, ref NativeMethods.RECT rect)
        {
            return NativeMethods.UnsafeNativeMethods.RectVisible(hdc, ref rect);
        }

        public static bool DragDetect(IntPtr hWnd, NativeMethods.POINT pt)
        {
            return NativeMethods.UnsafeNativeMethods.DragDetect(hWnd, pt);
        }

        public static int GetObject(IntPtr hObject, int nSize, [In, Out] NativeMethods.LOGFONT lf)
        {
            return NativeMethods.UnsafeNativeMethods.GetObject(hObject, nSize, lf);
        }

        public static IntPtr SelectPalette(IntPtr hdc, IntPtr hpal, bool bForceBackground)
        {
            return NativeMethods.UnsafeNativeMethods.SelectPalette(hdc, hpal, bForceBackground);
        }

        public static int RealizePalette(IntPtr hdc)
        {
            return NativeMethods.UnsafeNativeMethods.RealizePalette(hdc);
        }

        public static NativeMethods.HDC GetDC(NativeMethods.HWND handle)
        {
            return NativeMethods.UnsafeNativeMethods.GetDC(handle);
        }

        public static int GetDeviceCaps(NativeMethods.HDC hdc, int nIndex)
        {
            return NativeMethods.UnsafeNativeMethods.GetDeviceCaps((IntPtr)hdc, nIndex);
        }

        public static IntPtr GetCursor()
        {
            return NativeMethods.UnsafeNativeMethods.GetCursor();
        }

        public static bool SetSystemCursor(IntPtr hCursor, int id)
        {
            return NativeMethods.UnsafeNativeMethods.SetSystemCursor(hCursor, id);
        }

        internal static IntPtr CreateSolidBrush(NativeMethods.COLORREF aColorRef)
        {
            return NativeMethods.UnsafeNativeMethods.CreateSolidBrush(aColorRef);
        }

        public static bool FillRect(IntPtr hdc, ref NativeMethods.RECT rect, IntPtr hbrush)
        {
            return NativeMethods.UnsafeNativeMethods.FillRect(hdc, ref rect, hbrush);
        }

        public static bool FillRect(NativeMethods.HDC hdc, ref NativeMethods.RECT rect, IntPtr hbrush)
        {
            return NativeMethods.UnsafeNativeMethods.FillRect(hdc, ref rect, hbrush);
        }

        public static bool FillRgn(IntPtr hdc, IntPtr hrgn, IntPtr hbrush)
        {
            return NativeMethods.UnsafeNativeMethods.FillRgn(hdc, hrgn, hbrush);
        }

        public static int GetPixel(IntPtr hdc, int nXPos, int nYPos)
        {
            return NativeMethods.UnsafeNativeMethods.GetPixel(hdc, nXPos, nYPos);
        }

        public static bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, int dwRop)
        {
            return NativeMethods.UnsafeNativeMethods.StretchBlt(hdcDest, nXOriginDest, nYOriginDest, nWidthDest, nHeightDest, hdcSrc, nXOriginSrc, nYOriginSrc, nWidthSrc, nHeightSrc, dwRop);
        }

        public static bool UnhookWindowsHookEx(NativeMethods.HHOOK aHook)
        {
            return NativeMethods.UnsafeNativeMethods.UnhookWindowsHookEx(aHook);
        }

        public static IntPtr CopyIcon(IntPtr hCursor)
        {
            return NativeMethods.UnsafeNativeMethods.CopyIcon(hCursor);
        }

        public static IntPtr SetWindowsHookEx(int idHook, NativeMethods.LowLevelMouseProc lpfn, IntPtr hMod, int dwThreadId)
        {
            return NativeMethods.UnsafeNativeMethods.SetWindowsHookEx(idHook, lpfn, hMod, dwThreadId);
        }

        public static IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethods.UnsafeNativeMethods.CallNextHookEx(hhk, nCode, wParam, lParam);
        }

        public static IntPtr GetModuleHandle(string lpModuleName)
        {
            return NativeMethods.UnsafeNativeMethods.GetModuleHandle(lpModuleName);
        }

        public static bool SetViewportOrgEx(IntPtr hDC, int x, int y, out NativeMethods.POINT point)
        {
            return NativeMethods.UnsafeNativeMethods.SetViewportOrgEx(hDC, x, y, out point);
        }

        public static bool ChangeWindowMessageFilter(int msg, NativeMethods.ChangeWindowMessageFilterFlags flags)
        {
            return NativeMethods.UnsafeNativeMethods.ChangeWindowMessageFilter(msg, flags);
        }

        internal static bool IsLayoutRTL(IntPtr hwnd)
        {
            return (NativeMethods.GetWindowLong(hwnd, -20) & 4194304) != 0;
        }

        public static int ReadInt32(IntPtr ptr, int ofs)
        {
            return Marshal.ReadInt32(ptr, ofs);
        }

        public static void WriteInt32(IntPtr ptr, int ofs, int val)
        {
            Marshal.WriteInt32(ptr, ofs, val);
        }

        public static bool IsKeyboardContextMenuMessage(Message msg)
        {
            if (msg.Msg != 123)
                return false;
            Point point = new Point(msg.LParam.ToInt32());
            return point.X == -1 && point.Y == -1;
        }

        public static void ExcludeClipRect(IntPtr hdc, Rectangle rect)
        {
            NativeMethods.UnsafeNativeMethods.ExcludeClipRect(hdc, rect.X, rect.Y, rect.Right, rect.Bottom);
        }

        public static Rectangle[] GetClipRectsFromHDC(IntPtr hWnd, IntPtr hdc, bool offsetPoints)
        {
            IntPtr rectRgn = NativeMethods.CreateRectRgn(0, 0, 0, 0);
            try
            {
                if (NativeMethods.UnsafeNativeMethods.GetRandomRgn(hdc, rectRgn, NativeMethods.SYSRGN) != 1)
                    return (Rectangle[])null;
                if (offsetPoints)
                {
                    NativeMethods.POINT lpPoints = new NativeMethods.POINT();
                    NativeMethods.UnsafeNativeMethods.MapWindowPoints(IntPtr.Zero, hWnd, ref lpPoints, 1);
                    if (NativeMethods.IsLayoutRTL(hWnd))
                    {
                        Control control = Control.FromHandle(hWnd);
                        if (control != null)
                            lpPoints.X = lpPoints.X * -1 + control.Width;
                    }
                    NativeMethods.UnsafeNativeMethods.OffsetRgn(rectRgn, lpPoints.X, lpPoints.Y);
                }
                NativeMethods.RECT[] rectArray = NativeMethods.RectsFromRegion(rectRgn);
                if (rectArray == null || rectArray.Length == 0)
                    return (Rectangle[])null;
                Rectangle[] rectangleArray = new Rectangle[rectArray.Length];
                for (int index = 0; index < rectArray.Length; ++index)
                    rectangleArray[index] = rectArray[index].ToRectangle();
                return rectangleArray;
            }
            finally
            {
                NativeMethods.UnsafeNativeMethods.DeleteObject(rectRgn);
            }
        }

        public static int SignedLOWORD(IntPtr n)
        {
            return NativeMethods.SignedLOWORD((int)(long)n);
        }

        public static int SignedLOWORD(int n)
        {
            return (int)(short)(n & (int)ushort.MaxValue);
        }

        public static int SignedHIWORD(IntPtr n)
        {
            return NativeMethods.SignedHIWORD((int)(long)n);
        }

        public static int SignedHIWORD(int n)
        {
            return (int)(short)(n >> 16 & (int)ushort.MaxValue);
        }

        public static Point FromMouseLParam(ref Message m)
        {
            return new Point(NativeMethods.SignedLOWORD(m.LParam), NativeMethods.SignedHIWORD(m.LParam));
        }

        public static NativeMethods.RECT[] RectsFromRegion(IntPtr hRgn)
        {
            NativeMethods.RECT[] rectArray = (NativeMethods.RECT[])null;
            int regionData = NativeMethods.UnsafeNativeMethods.GetRegionData(hRgn, 0, IntPtr.Zero);
            if (regionData != 0)
            {
                IntPtr zero = IntPtr.Zero;
                IntPtr num = Marshal.AllocCoTaskMem(regionData);
                NativeMethods.UnsafeNativeMethods.GetRegionData(hRgn, regionData, num);
                NativeMethods.RGNDATAHEADER structure = (NativeMethods.RGNDATAHEADER)Marshal.PtrToStructure(num, typeof(NativeMethods.RGNDATAHEADER));
                if (structure.iType == 1)
                {
                    rectArray = new NativeMethods.RECT[structure.nCount];
                    int dwSize = structure.dwSize;
                    for (int index = 0; index < structure.nCount; ++index)
                    {
                        IntPtr ptr = new IntPtr(num.ToInt64() + (long)dwSize + (long)(Marshal.SizeOf(typeof(NativeMethods.RECT)) * index));
                        rectArray[index] = (NativeMethods.RECT)Marshal.PtrToStructure(ptr, typeof(NativeMethods.RECT));
                    }
                }
                if (num != IntPtr.Zero)
                    Marshal.FreeCoTaskMem(num);
            }
            return rectArray;
        }

        public static IntPtr CreateSolidBrush(Color aColor)
        {
            return NativeMethods.UnsafeNativeMethods.CreateSolidBrush(new NativeMethods.COLORREF(aColor));
        }

        public static IntPtr CreateSolidBrush(int argb)
        {
            return NativeMethods.UnsafeNativeMethods.CreateSolidBrush(new NativeMethods.COLORREF(argb));
        }

        public static Region CreateRoundRegion(Rectangle windowBounds, int ellipseSize)
        {
            IntPtr roundRectRgn = NativeMethods.UnsafeNativeMethods.CreateRoundRectRgn(windowBounds.X, windowBounds.Y, windowBounds.Width + 1, windowBounds.Height + 1, ellipseSize, ellipseSize);
            Region region = Region.FromHrgn(roundRectRgn);
            NativeMethods.DeleteObject(roundRectRgn);
            return region;
        }

        public static bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref NativeMethods.POINT pptDst, ref NativeMethods.SIZE pSizeDst, IntPtr hdcSrc, ref NativeMethods.POINT pptSrc, int crKey, ref NativeMethods.BLENDFUNCTION pBlend, int dwFlags)
        {
            return NativeMethods.UnsafeNativeMethods.UpdateLayeredWindow(hwnd, hdcDst, ref pptDst, ref pSizeDst, hdcSrc, ref pptSrc, crKey, ref pBlend, dwFlags);
        }

        public static bool InvalidateRgn(IntPtr hWnd, IntPtr hrgn, bool erase)
        {
            return NativeMethods.UnsafeNativeMethods.InvalidateRgn(hWnd, hrgn, erase);
        }

        public static bool SetLayeredWindowAttributes(IntPtr hwnd, int crKey, byte bAlpha, int dwFlags)
        {
            return NativeMethods.UnsafeNativeMethods.SetLayeredWindowAttributes(hwnd, crKey, bAlpha, (uint)dwFlags);
        }

        public static bool AdjustWindowRectEx(ref NativeMethods.RECT lpRect, int dwStyle, bool bMenu, int dwExStyle)
        {
            return NativeMethods.UnsafeNativeMethods.AdjustWindowRectEx(ref lpRect, dwStyle, bMenu, dwExStyle);
        }

        public static IntPtr FindWindow(string className, string windowText)
        {
            return NativeMethods.UnsafeNativeMethods.FindWindow(className, windowText);
        }

        public static int ShowWindow(IntPtr hWnd, int command)
        {
            return NativeMethods.UnsafeNativeMethods.ShowWindow(hWnd, command);
        }

        public static bool ShowWindow(IntPtr hWnd, NativeMethods.ShowWindowCommands command)
        {
            return NativeMethods.UnsafeNativeMethods.ShowWindow(hWnd, command);
        }

        public static bool GetWindowPlacement(IntPtr hWnd, out NativeMethods.WINDOWPLACEMENT lpwndpl)
        {
            return NativeMethods.UnsafeNativeMethods.GetWindowPlacement(hWnd, out lpwndpl);
        }

        public static IntPtr WindowFromPoint(Point pt)
        {
            return NativeMethods.UnsafeNativeMethods.WindowFromPoint(pt);
        }

        public static IntPtr GetWindow(IntPtr hWnd, int wCmd)
        {
            return NativeMethods.UnsafeNativeMethods.GetWindow(hWnd, (uint)wCmd);
        }

        public static IntPtr SetActiveWindow(IntPtr hWnd)
        {
            return NativeMethods.UnsafeNativeMethods.SetActiveWindow(hWnd);
        }

        public static bool SetForegroundWindow(IntPtr hWnd)
        {
            return NativeMethods.UnsafeNativeMethods.SetForegroundWindow(hWnd);
        }

        public static bool EnableWindow(IntPtr hWnd, bool enable)
        {
            return NativeMethods.UnsafeNativeMethods.EnableWindow(hWnd, enable);
        }

        public static bool IsWindowEnabled(IntPtr hWnd)
        {
            return NativeMethods.UnsafeNativeMethods.IsWindowEnabled(hWnd);
        }

        public static bool SystemParametersInfo(int uiAction, int uiParam, NativeMethods.NONCLIENTMETRICS pvParam, int fWinIni)
        {
            return NativeMethods.UnsafeNativeMethods.SystemParametersInfo(uiAction, uiParam, pvParam, fWinIni);
        }

        public static bool SetGestureConfig(IntPtr hWnd, int dwReserved, int cIDs, [In, Out] NativeMethods.GESTURECONFIG[] pGestureConfig, int cbSize)
        {
            return NativeMethods.UnsafeNativeMethods.SetGestureConfig(hWnd, dwReserved, cIDs, pGestureConfig, cbSize);
        }

        public static bool BeginPanningFeedback(IntPtr hWnd)
        {
            return NativeMethods.UnsafeNativeMethods.BeginPanningFeedback(hWnd);
        }

        public static bool EndPanningFeedback(IntPtr hWnd, bool fAnimateBack)
        {
            return NativeMethods.UnsafeNativeMethods.EndPanningFeedback(hWnd, fAnimateBack);
        }

        public static bool UpdatePanningFeedback(IntPtr hwnd, int lTotalOverpanOffsetX, int lTotalOverpanOffsetY, bool fInInertia)
        {
            return NativeMethods.UnsafeNativeMethods.UpdatePanningFeedback(hwnd, lTotalOverpanOffsetX, lTotalOverpanOffsetY, fInInertia);
        }

        public static bool CloseGestureInfoHandle(IntPtr hGestureInfo)
        {
            return NativeMethods.UnsafeNativeMethods.CloseGestureInfoHandle(hGestureInfo);
        }

        public static bool GetGestureInfo(IntPtr hGestureInfo, ref NativeMethods.GESTUREINFO pGestureInfo)
        {
            return NativeMethods.UnsafeNativeMethods.GetGestureInfo(hGestureInfo, ref pGestureInfo);
        }

        public static int DwmSetIconicThumbnail(IntPtr hwnd, IntPtr hbitmap, int flags)
        {
            return NativeMethods.UnsafeNativeMethods.DwmSetIconicThumbnail(hwnd, hbitmap, (uint)flags);
        }

        public static int DwmSetIconicLivePreviewBitmap(IntPtr hwnd, IntPtr hbitmap, IntPtr ptClient, int flags)
        {
            return NativeMethods.UnsafeNativeMethods.DwmSetIconicLivePreviewBitmap(hwnd, hbitmap, ptClient, (uint)flags);
        }

        public static int DwmSetWindowAttribute(IntPtr hwnd, int dwAttributeToSet, IntPtr pvAttributeValue, int cbAttribute)
        {
            return NativeMethods.UnsafeNativeMethods.DwmSetWindowAttribute(hwnd, (uint)dwAttributeToSet, pvAttributeValue, (uint)cbAttribute);
        }

        public static int DwmInvalidateIconicBitmaps(IntPtr hwnd)
        {
            return NativeMethods.UnsafeNativeMethods.DwmInvalidateIconicBitmaps(hwnd);
        }

        public static int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount)
        {
            return NativeMethods.UnsafeNativeMethods.GetWindowText(hWnd, lpString, nMaxCount);
        }

        public static bool ClientToScreen(IntPtr hwnd, ref NativeMethods.POINT point)
        {
            return NativeMethods.UnsafeNativeMethods.ClientToScreen(hwnd, ref point);
        }

        public static int ExtractIconEx(string fileName, int iconStartingIndex, IntPtr[] largeIcons, IntPtr[] smallIcons, int iconCount)
        {
            return NativeMethods.UnsafeNativeMethods.ExtractIconEx(fileName, iconStartingIndex, largeIcons, smallIcons, iconCount);
        }

        public static void SHAddToRecentDocs(NativeMethods.ShellAddToRecentDocs flags, [MarshalAs(UnmanagedType.LPWStr)] string path)
        {
            NativeMethods.UnsafeNativeMethods.SHAddToRecentDocs(flags, path);
        }

        public static void PropVariantClear([In, Out] NativeMethods.PropVariant pvar)
        {
            NativeMethods.UnsafeNativeMethods.PropVariantClear(pvar);
        }

        public static int RegisterWindowMessage(string lpProcName)
        {
            return (int)NativeMethods.UnsafeNativeMethods.RegisterWindowMessage(lpProcName);
        }

        public static void PostQuitMessage(int exitCode)
        {
            NativeMethods.UnsafeNativeMethods.PostQuitMessage(exitCode);
        }

        public static IntPtr LocalFree(IntPtr p)
        {
            return NativeMethods.UnsafeNativeMethods.LocalFree(p);
        }

        public static IntPtr LocalAlloc(int flag, int size)
        {
            return NativeMethods.UnsafeNativeMethods.LocalAlloc(flag, size);
        }

        public static bool ShowWindowAsync(IntPtr hWnd, int nCmdShow)
        {
            return NativeMethods.UnsafeNativeMethods.ShowWindowAsync(hWnd, nCmdShow);
        }

        public static void SetCurrentProcessExplicitAppUserModelID([MarshalAs(UnmanagedType.LPWStr)] string AppID)
        {
            NativeMethods.UnsafeNativeMethods.SetCurrentProcessExplicitAppUserModelID(AppID);
        }

        public static SizeF GetFontAutoScaleDimensions(Font font, Control control)
        {
            using (Graphics graphics = control.CreateGraphics())
            {
                IntPtr hdc = graphics.GetHdc();
                try
                {
                    return NativeMethods.GetFontAutoScaleDimensions(font, hdc);
                }
                finally
                {
                    graphics.ReleaseHdc(hdc);
                }
            }
        }

        public static SizeF GetFontAutoScaleDimensions(Font font)
        {
            return NativeMethods.GetFontAutoScaleDimensions(font, IntPtr.Zero);
        }

        public static SizeF GetFontAutoScaleDimensions(Font font, IntPtr sourceDC)
        {
            object wrapper = (object)font;
            SizeF empty = SizeF.Empty;
            IntPtr handle = sourceDC;
            if (handle == IntPtr.Zero)
                handle = NativeMethods.CreateCompatibleDC(IntPtr.Zero);
            if (handle == IntPtr.Zero)
                throw new Exception();
            IntPtr hfont = font.ToHfont();
            HandleRef handleRef1 = new HandleRef(wrapper, handle);
            try
            {
                HandleRef handleRef2 = new HandleRef(wrapper, hfont);
                HandleRef handleRef3 = new HandleRef(wrapper, NativeMethods.SelectObject(handleRef1, handleRef2));
                try
                {
                    NativeMethods.TEXTMETRIC lptm = new NativeMethods.TEXTMETRIC();
                    NativeMethods.GetTextMetrics(handleRef1, ref lptm);
                    empty.Height = (float)lptm.tmHeight;
                    if (((int)lptm.tmPitchAndFamily & 1) != 0)
                    {
                        NativeMethods.SIZE size = new NativeMethods.SIZE();
                        NativeMethods.UnsafeNativeMethods.GetTextExtentPoint32(handleRef1, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", ref size);
                        empty.Width = (float)(int)Math.Round((double)size.Width / (double)"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".Length);
                    }
                    else
                        empty.Width = (float)lptm.tmAveCharWidth;
                }
                finally
                {
                    NativeMethods.SelectObject(handleRef1, handleRef3);
                }
            }
            finally
            {
                if (sourceDC == IntPtr.Zero)
                    NativeMethods.DeleteDC(handleRef1);
                NativeMethods.DeleteObject(hfont);
            }
            return empty;
        }

        internal static int GetTextMetrics(HandleRef hDC, ref NativeMethods.TEXTMETRIC lptm)
        {
            if (Marshal.SystemDefaultCharSize != 1)
                return NativeMethods.UnsafeNativeMethods.GetTextMetricsW(hDC, ref lptm);
            NativeMethods.TEXTMETRICA lptm1 = new NativeMethods.TEXTMETRICA();
            int textMetricsA = NativeMethods.UnsafeNativeMethods.GetTextMetricsA(hDC, ref lptm1);
            lptm.tmHeight = lptm1.tmHeight;
            lptm.tmAscent = lptm1.tmAscent;
            lptm.tmDescent = lptm1.tmDescent;
            lptm.tmInternalLeading = lptm1.tmInternalLeading;
            lptm.tmExternalLeading = lptm1.tmExternalLeading;
            lptm.tmAveCharWidth = lptm1.tmAveCharWidth;
            lptm.tmMaxCharWidth = lptm1.tmMaxCharWidth;
            lptm.tmWeight = lptm1.tmWeight;
            lptm.tmOverhang = lptm1.tmOverhang;
            lptm.tmDigitizedAspectX = lptm1.tmDigitizedAspectX;
            lptm.tmDigitizedAspectY = lptm1.tmDigitizedAspectY;
            lptm.tmFirstChar = (char)lptm1.tmFirstChar;
            lptm.tmLastChar = (char)lptm1.tmLastChar;
            lptm.tmDefaultChar = (char)lptm1.tmDefaultChar;
            lptm.tmBreakChar = (char)lptm1.tmBreakChar;
            lptm.tmItalic = lptm1.tmItalic;
            lptm.tmUnderlined = lptm1.tmUnderlined;
            lptm.tmStruckOut = lptm1.tmStruckOut;
            lptm.tmPitchAndFamily = lptm1.tmPitchAndFamily;
            lptm.tmCharSet = lptm1.tmCharSet;
            return textMetricsA;
        }

        internal struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public uint uCallbackMessage;
            public uint uEdge;
            public NativeMethods.RECT rc;
            public IntPtr lParam;
        }

        public struct WNDCLASS
        {
            public int style;
            public Delegate lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszMenuName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszClassName;
        }

        public enum ShellAddToRecentDocs
        {
            Pidl = 1,
            PathA = 2,
            PathW = 3,
            AppIdInfo = 4,
            AppIdInfoIdList = 5,
            Link = 6,
            AppIdInfoLink = 7,
            ShellItem = 8,
        }

        public enum FLASHW
        {
            FLASHW_STOP = 0,
            FLASHW_CAPTION = 1,
            FLASHW_TRAY = 2,
            FLASHW_ALL = 3,
            FLASHW_TIMER = 4,
            FLASHW_TIMERNOFG = 12,
        }

        internal struct FLASHWINFO
        {
            public uint cbSize;
            public IntPtr hwnd;
            public uint dwFlags;
            public uint uCount;
            public uint dwTimeout;
        }

        [StructLayout(LayoutKind.Explicit)]
        public class PropVariant : IDisposable
        {
            [FieldOffset(0)]
            private ushort valueType;
            [FieldOffset(8)]
            private IntPtr ptr;
            [FieldOffset(8)]
            private int int32;

            public PropVariant(string value)
            {
                if (value == null)
                    return;
                this.valueType = (ushort)31;
                this.ptr = Marshal.StringToCoTaskMemUni(value);
            }

            public PropVariant(bool value)
            {
                this.valueType = (ushort)11;
                this.int32 = value ? -1 : 0;
            }

            public void Dispose()
            {
                NativeMethods.PropVariantClear(this);
                GC.SuppressFinalize((object)this);
            }
        }

        public enum ShowWindowCommands
        {
            Hide = 0,
            Normal = 1,
            ShowMinimized = 2,
            Maximize = 3,
            ShowMaximized = 3,
            ShowNoActivate = 4,
            Show = 5,
            Minimize = 6,
            ShowMinNoActive = 7,
            ShowNA = 8,
            Restore = 9,
            ShowDefault = 10,
            ForceMinimize = 11,
        }

        [Serializable]
        public struct WINDOWPLACEMENT
        {
            public int Length;
            public int Flags;
            public NativeMethods.ShowWindowCommands ShowCmd;
            public NativeMethods.POINT MinPosition;
            public NativeMethods.POINT MaxPosition;
            public NativeMethods.RECT NormalPosition;
        }

        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        public enum ScrollWindowExFlags
        {
            SW_SCROLLCHILDREN = 1,
            SW_INVALIDATE = 2,
            SW_ERASE = 4,
            SW_SMOOTHSCROLL = 16,
        }

        public struct Margins
        {
            public int Left;
            public int Right;
            public int Top;
            public int Bottom;
        }

        public struct BITMAPINFO_SMALL
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
            public byte bmiColors_rgbBlue;
            public byte bmiColors_rgbGreen;
            public byte bmiColors_rgbRed;
            public byte bmiColors_rgbReserved;
        }

        public struct BITMAPINFO_FLAT
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int bmiHeader_biSizeImage;
            public int bmiHeader_biXPelsPerMeter;
            public int bmiHeader_biYPelsPerMeter;
            public int bmiHeader_biClrUsed;
            public int bmiHeader_biClrImportant;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public byte[] bmiColors;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class BITMAPINFOHEADER
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;

            public BITMAPINFOHEADER()
            {
                this.biSize = 40;
            }
        }

        public struct HWND : IWin32Window
        {
            public static readonly NativeMethods.HWND Empty = new NativeMethods.HWND(IntPtr.Zero);
            private IntPtr _Handle;

            public static NativeMethods.HWND Desktop
            {
                get
                {
                    return (NativeMethods.HWND)NativeMethods.GetDesktopWindow();
                }
            }

            public bool IsEmpty
            {
                get
                {
                    return this._Handle == IntPtr.Zero;
                }
            }

            public bool IsVisible
            {
                get
                {
                    return NativeMethods.IsWindowVisible(this._Handle);
                }
            }

            public IntPtr Handle
            {
                get
                {
                    return this._Handle;
                }
            }

            public HWND(IntPtr aValue)
            {
                this._Handle = aValue;
            }

            public static implicit operator IntPtr(NativeMethods.HWND aHwnd)
            {
                return aHwnd.Handle;
            }

            public static implicit operator NativeMethods.HWND(IntPtr aIntPtr)
            {
                return new NativeMethods.HWND(aIntPtr);
            }

            public static bool operator ==(NativeMethods.HWND aHwnd1, NativeMethods.HWND aHwnd2)
            {
                if ((ValueType)aHwnd1 == null)
                    return (ValueType)aHwnd2 == null;
                return aHwnd1.Equals(aHwnd2);
            }

            public static bool operator ==(IntPtr aIntPtr, NativeMethods.HWND aHwnd)
            {
                if ((ValueType)aIntPtr == null)
                    return (ValueType)aHwnd == null;
                return aHwnd.Equals(aIntPtr);
            }

            public static bool operator ==(NativeMethods.HWND aHwnd, IntPtr aIntPtr)
            {
                if ((ValueType)aHwnd == null)
                    return (ValueType)aIntPtr == null;
                return aHwnd.Equals(aIntPtr);
            }

            public static bool operator !=(NativeMethods.HWND aHwnd1, NativeMethods.HWND aHwnd2)
            {
                return !(aHwnd1 == aHwnd2);
            }

            public static bool operator !=(IntPtr aIntPtr, NativeMethods.HWND aHwnd)
            {
                return !(aIntPtr == aHwnd);
            }

            public static bool operator !=(NativeMethods.HWND aHwnd, IntPtr aIntPtr)
            {
                return !(aHwnd == aIntPtr);
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;
                if (obj is NativeMethods.HWND)
                    return this.Equals((NativeMethods.HWND)obj);
                if (obj is IntPtr)
                    return this.Equals((IntPtr)obj);
                return false;
            }

            public bool Equals(IntPtr ptr)
            {
                return this._Handle.ToInt32().Equals(ptr.ToInt32());
            }

            public bool Equals(NativeMethods.HWND hwnd)
            {
                return this.Equals(hwnd._Handle);
            }

            public bool Equals(IWin32Window window)
            {
                return this.Equals(window.Handle);
            }

            public override int GetHashCode()
            {
                return this._Handle.GetHashCode();
            }

            public override string ToString()
            {
                return "{Handle=0x" + this._Handle.ToInt32().ToString("X8") + "}";
            }
        }

        public struct HDC
        {
            public static readonly NativeMethods.HDC Empty = new NativeMethods.HDC(0);
            private IntPtr _Handle;

            public IntPtr Handle
            {
                get
                {
                    return this._Handle;
                }
            }

            public bool IsEmpty
            {
                get
                {
                    return this._Handle == IntPtr.Zero;
                }
            }

            public HDC(IntPtr aValue)
            {
                this._Handle = aValue;
            }

            public HDC(int aValue)
            {
                this._Handle = new IntPtr(aValue);
            }

            public static implicit operator IntPtr(NativeMethods.HDC aHdc)
            {
                return aHdc.Handle;
            }

            public static implicit operator NativeMethods.HDC(IntPtr aIntPtr)
            {
                return new NativeMethods.HDC(aIntPtr);
            }

            public static bool operator ==(NativeMethods.HDC aHdc1, NativeMethods.HDC aHdc2)
            {
                if ((ValueType)aHdc1 == null)
                    return (ValueType)aHdc2 == null;
                return aHdc1.Equals(aHdc2);
            }

            public static bool operator ==(IntPtr aIntPtr, NativeMethods.HDC aHdc)
            {
                if ((ValueType)aIntPtr == null)
                    return (ValueType)aHdc == null;
                return aHdc.Equals(aIntPtr);
            }

            public static bool operator ==(NativeMethods.HDC aHdc, IntPtr aIntPtr)
            {
                if ((ValueType)aHdc == null)
                    return (ValueType)aIntPtr == null;
                return aHdc.Equals(aIntPtr);
            }

            public static bool operator !=(NativeMethods.HDC aHdc1, NativeMethods.HDC aHdc2)
            {
                return !(aHdc1 == aHdc2);
            }

            public static bool operator !=(IntPtr aIntPtr, NativeMethods.HDC aHdc)
            {
                return !(aIntPtr == aHdc);
            }

            public static bool operator !=(NativeMethods.HDC aHdc, IntPtr aIntPtr)
            {
                return !(aHdc == aIntPtr);
            }

            public override bool Equals(object aObj)
            {
                if (aObj == null)
                    return false;
                if (aObj is NativeMethods.HDC)
                    return this.Equals((NativeMethods.HDC)aObj);
                if (aObj is IntPtr)
                    return this.Equals((IntPtr)aObj);
                return false;
            }

            public bool Equals(NativeMethods.HDC aHDC)
            {
                return this._Handle.Equals((object)aHDC._Handle);
            }

            public bool Equals(IntPtr aIntPtr)
            {
                return this._Handle.Equals((object)aIntPtr);
            }

            public override int GetHashCode()
            {
                return this._Handle.GetHashCode();
            }

            public override string ToString()
            {
                return "{Handle=0x" + this._Handle.ToInt32().ToString("X8") + "}";
            }

            public void Release(NativeMethods.HWND window)
            {
                NativeMethods.ReleaseDC((IntPtr)window, (IntPtr)this);
            }

            public IntPtr SelectObject(IntPtr aGDIObj)
            {
                return NativeMethods.SelectObject((IntPtr)this, aGDIObj);
            }

            public NativeMethods.HDC CreateCompatible()
            {
                return (NativeMethods.HDC)NativeMethods.CreateCompatibleDC(this._Handle);
            }

            public IntPtr CreateCompatibleBitmap(int width, int height)
            {
                return NativeMethods.CreateCompatibleBitmap(this._Handle, width, height);
            }

            public IntPtr CreateCompatibleBitmap(Rectangle rectangle)
            {
                return this.CreateCompatibleBitmap(rectangle.Width, rectangle.Height);
            }

            public void Delete()
            {
                NativeMethods.DeleteDC(this._Handle);
            }
        }

        [CLSCompliant(false)]
        public struct COLORREF
        {
            private uint _ColorRef;

            public COLORREF(Color aValue)
            {
                int argb = aValue.ToArgb();
                int num1 = (argb & (int)byte.MaxValue) << 16;
                int num2 = argb & 16776960;
                this._ColorRef = (uint)((num2 | num2 >> 16 & (int)byte.MaxValue) & (int)ushort.MaxValue | num1);
            }

            public COLORREF(int lRGB)
            {
                this._ColorRef = (uint)lRGB;
            }

            public Color ToColor()
            {
                return Color.FromArgb((int)this._ColorRef & (int)byte.MaxValue, (int)this._ColorRef >> 8 & (int)byte.MaxValue, (int)this._ColorRef >> 16 & (int)byte.MaxValue);
            }

            public static NativeMethods.COLORREF FromColor(Color aColor)
            {
                return new NativeMethods.COLORREF(aColor);
            }

            public static Color ToColor(NativeMethods.COLORREF aColorRef)
            {
                return aColorRef.ToColor();
            }
        }

        public struct HHOOK
        {
            public static readonly NativeMethods.HHOOK Empty = new NativeMethods.HHOOK(0);
            private IntPtr _Handle;

            public IntPtr Handle
            {
                get
                {
                    return this._Handle;
                }
            }

            public bool IsEmpty
            {
                get
                {
                    return this._Handle == IntPtr.Zero;
                }
            }

            public HHOOK(IntPtr aValue)
            {
                this._Handle = aValue;
            }

            public HHOOK(int aValue)
            {
                this._Handle = new IntPtr(aValue);
            }

            public static implicit operator IntPtr(NativeMethods.HHOOK aHHook)
            {
                return aHHook.Handle;
            }

            public static implicit operator NativeMethods.HHOOK(IntPtr aIntPtr)
            {
                return new NativeMethods.HHOOK(aIntPtr);
            }

            public static bool operator ==(NativeMethods.HHOOK aHHook1, NativeMethods.HHOOK aHHook2)
            {
                if ((ValueType)aHHook1 == null)
                    return (ValueType)aHHook2 == null;
                return aHHook1.Equals(aHHook2);
            }

            public static bool operator ==(IntPtr aIntPtr, NativeMethods.HHOOK aHHook)
            {
                if ((ValueType)aIntPtr == null)
                    return (ValueType)aHHook == null;
                return aHHook.Equals(aIntPtr);
            }

            public static bool operator ==(NativeMethods.HHOOK aHHook, IntPtr aIntPtr)
            {
                if ((ValueType)aHHook == null)
                    return (ValueType)aIntPtr == null;
                return aHHook.Equals(aIntPtr);
            }

            public static bool operator !=(NativeMethods.HHOOK aHHook1, NativeMethods.HHOOK aHHook2)
            {
                return !(aHHook1 == aHHook2);
            }

            public static bool operator !=(IntPtr aIntPtr, NativeMethods.HHOOK aHHook)
            {
                return !(aIntPtr == aHHook);
            }

            public static bool operator !=(NativeMethods.HHOOK aHHook, IntPtr aIntPtr)
            {
                return !(aHHook == aIntPtr);
            }

            public override bool Equals(object aObj)
            {
                if (aObj == null)
                    return false;
                if (aObj is NativeMethods.HHOOK)
                    return this.Equals((NativeMethods.HHOOK)aObj);
                if (aObj is IntPtr)
                    return this.Equals((IntPtr)aObj);
                return false;
            }

            public bool Equals(NativeMethods.HHOOK aHHOOK)
            {
                return this._Handle.Equals((object)aHHOOK._Handle);
            }

            public bool Equals(IntPtr aIntPtr)
            {
                return this._Handle.Equals((object)aIntPtr);
            }

            public override int GetHashCode()
            {
                return this._Handle.GetHashCode();
            }

            public override string ToString()
            {
                return "{Handle=0x" + this._Handle.ToInt32().ToString("X8") + "}";
            }
        }

        public struct COPYDATASTRUCT : IDisposable
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;

            public void Dispose()
            {
                if (!(this.lpData != IntPtr.Zero))
                    return;
                NativeMethods.LocalFree(this.lpData);
                this.lpData = IntPtr.Zero;
            }
        }

        public enum SystemCursors
        {
            OCR_NORMAL = 32512,
            OCR_IBEAM = 32513,
            OCR_WAIT = 32514,
            OCR_CROSS = 32515,
            OCR_UP = 32516,
            OCR_SIZE = 32640,
            OCR_ICON = 32641,
            OCR_SIZENWSE = 32642,
            OCR_SIZENESW = 32643,
            OCR_SIZEWE = 32644,
            OCR_SIZENS = 32645,
            OCR_SIZEALL = 32646,
            OCR_ICOCUR = 32647,
            OCR_NO = 32648,
            OCR_HAND = 32649,
            OCR_APPSTARTING = 32650,
        }

        [Flags]
        public enum RasterOperations
        {
            SRCCOPY = 13369376,
            SRCPAINT = 15597702,
            SRCAND = 8913094,
            SRCINVERT = 6684742,
            SRCERASE = 4457256,
            NOTSRCCOPY = 3342344,
            NOTSRCERASE = 1114278,
            MERGECOPY = 12583114,
            MERGEPAINT = 12255782,
            PATCOPY = 15728673,
            PATPAINT = 16452105,
            PATINVERT = 5898313,
            DSTINVERT = 5570569,
            BLACKNESS = 66,
            WHITENESS = 16711778,
        }

        public enum MouseMessages
        {
            WM_MOUSEMOVE = 512,
            WM_LBUTTONDOWN = 513,
            WM_LBUTTONUP = 514,
            WM_RBUTTONDOWN = 516,
            WM_RBUTTONUP = 517,
            WM_MOUSEWHEEL = 522,
        }

        [Flags]
        public enum PrintOptions
        {
            PRF_CHECKVISIBLE = 1,
            PRF_NONCLIENT = 2,
            PRF_CLIENT = 4,
            PRF_ERASEBKGND = 8,
            PRF_CHILDREN = 16,
            PRF_OWNED = 32,
        }

        [StructLayout(LayoutKind.Sequential)]
        public class TRACKMOUSEEVENTStruct
        {
            public int cbSize;
            public int dwFlags;
            public IntPtr hwndTrack;
            public int dwHoverTime;

            public TRACKMOUSEEVENTStruct()
              : this(0, IntPtr.Zero, 0)
            {
            }

            public TRACKMOUSEEVENTStruct(int dwFlags, IntPtr hwndTrack, int dwHoverTime)
            {
                this.cbSize = Marshal.SizeOf(typeof(NativeMethods.TRACKMOUSEEVENTStruct));
                this.dwFlags = dwFlags;
                this.hwndTrack = hwndTrack;
                this.dwHoverTime = dwHoverTime;
            }
        }

        public struct NCCALCSIZE_PARAMS
        {
            public NativeMethods.RECT rgrc0;
            public NativeMethods.RECT rgrc1;
            public NativeMethods.RECT rgrc2;
            public IntPtr lppos;

            [SecuritySafeCritical]
            public static NativeMethods.NCCALCSIZE_PARAMS GetFrom(IntPtr lParam)
            {
                return (NativeMethods.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(lParam, typeof(NativeMethods.NCCALCSIZE_PARAMS));
            }

            [SecuritySafeCritical]
            public void SetTo(IntPtr lParam)
            {
                Marshal.StructureToPtr((object)this, lParam, false);
            }
        }

        public struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public bool fErase;
            public NativeMethods.RECT rcPaint;
            public bool fRestore;
            public bool fIncUpdate;
            public int reserved1;
            public int reserved2;
            public int reserved3;
            public int reserved4;
            public int reserved5;
            public int reserved6;
            public int reserved7;
            public int reserved8;
        }

        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public POINT(Point pt)
            {
                this.X = pt.X;
                this.Y = pt.Y;
            }

            public Point ToPoint()
            {
                return new Point(this.X, this.Y);
            }
        }

        public struct SIZE
        {
            public int Width;
            public int Height;

            public SIZE(int w, int h)
            {
                this.Width = w;
                this.Height = h;
            }

            public SIZE(Size size)
            {
                this.Width = size.Width;
                this.Height = size.Height;
            }

            public Size ToSize()
            {
                return new Size(this.Width, this.Height);
            }
        }

        public enum RegionDataHeaderTypes
        {
            Rectangles = 1,
        }

        public struct RGNDATAHEADER
        {
            public int dwSize;
            public int iType;
            public int nCount;
            public int nRgnSize;
            public NativeMethods.RECT rcBound;
        }

        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int l, int t, int r, int b)
            {
                this.Left = l;
                this.Top = t;
                this.Right = r;
                this.Bottom = b;
            }

            public RECT(Rectangle r)
            {
                this.Left = r.Left;
                this.Top = r.Top;
                this.Right = r.Right;
                this.Bottom = r.Bottom;
            }

            public Rectangle ToRectangle()
            {
                return Rectangle.FromLTRB(this.Left, this.Top, this.Right, this.Bottom);
            }

            public void Inflate(int width, int height)
            {
                this.Left -= width;
                this.Top -= height;
                this.Right += width;
                this.Bottom += height;
            }

            public override string ToString()
            {
                return string.Format("x:{0},y:{1},width:{2},height:{3}", new object[4]
                {
          (object) this.Left,
          (object) this.Top,
          (object) (this.Right - this.Left),
          (object) (this.Bottom - this.Top)
                });
            }
        }

        public enum GetClipBoxReturn
        {
            Error,
            NullRegion,
            SimpleRegion,
            ComplexRegion,
        }

        public struct MINMAXINFO
        {
            public NativeMethods.POINT ptReserved;
            public NativeMethods.POINT ptMaxSize;
            public NativeMethods.POINT ptMaxPosition;
            public NativeMethods.POINT ptMinTrackSize;
            public NativeMethods.POINT ptMaxTrackSize;

            [SecuritySafeCritical]
            public static NativeMethods.MINMAXINFO GetFrom(IntPtr lParam)
            {
                return (NativeMethods.MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(NativeMethods.MINMAXINFO));
            }

            [SecuritySafeCritical]
            public void SetTo(IntPtr lParam)
            {
                Marshal.StructureToPtr((object)this, lParam, false);
            }
        }

        public struct GESTUREINFO
        {
            public int cbSize;
            public int dwFlags;
            public int dwID;
            public IntPtr hwndTarget;
            [MarshalAs(UnmanagedType.Struct)]
            internal NativeMethods.POINTS ptsLocation;
            public int dwInstanceID;
            public int dwSequenceID;
            public long ullArguments;
            public int cbExtraArgs;
        }

        public struct GESTURECONFIG
        {
            public int dwID;
            public int dwWant;
            public int dwBlock;

            public GESTURECONFIG(int dwID, int dwWant, int dwBlock)
            {
                this.dwID = dwID;
                this.dwWant = dwWant;
                this.dwBlock = dwBlock;
            }
        }

        public struct POINTS
        {
            public short x;
            public short y;

            public Point ToPoint()
            {
                return new Point((int)this.x, (int)this.y);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class GESTURENOTIFYSTRUCT
        {
            public int cbSize;
            public int dwFlags;
            public IntPtr hwndTarget;
            [MarshalAs(UnmanagedType.Struct)]
            public NativeMethods.POINTS ptsLocation;
            public int dwInstanceID;
        }

        public class WMSZ
        {
            public const int WMSZ_LEFT = 1;
            public const int WMSZ_RIGHT = 2;
            public const int WMSZ_TOP = 3;
            public const int WMSZ_TOPLEFT = 4;
            public const int WMSZ_TOPRIGHT = 5;
            public const int WMSZ_BOTTOM = 6;
            public const int WMSZ_BOTTOMLEFT = 7;
            public const int WMSZ_BOTTOMRIGHT = 8;
        }

        public class SWP
        {
            public const int SWP_NOSIZE = 1;
            public const int SWP_NOMOVE = 2;
            public const int SWP_NOZORDER = 4;
            public const int SWP_NOREDRAW = 8;
            public const int SWP_NOACTIVATE = 16;
            public const int SWP_FRAMECHANGED = 32;
            public const int SWP_DRAWFRAME = 32;
            public const int SWP_SHOWWINDOW = 64;
            public const int SWP_HIDEWINDOW = 128;
            public const int SWP_NOCOPYBITS = 256;
            public const int SWP_NOOWNERZORDER = 512;
            public const int SWP_NOREPOSITION = 512;
            public const int SWP_NOSENDCHANGING = 1024;
        }

        public class DC
        {
            public const int DCX_WINDOW = 1;
            public const int DCX_CACHE = 2;
            public const int DCX_NORESETATTRS = 4;
            public const int DCX_CLIPCHILDREN = 8;
            public const int DCX_CLIPSIBLINGS = 16;
            public const int DCX_PARENTCLIP = 32;
            public const int DCX_EXCLUDERGN = 64;
            public const int DCX_INTERSECTRGN = 128;
            public const int DCX_EXCLUDEUPDATE = 256;
            public const int DCX_INTERSECTUPDATE = 512;
            public const int DCX_LOCKWINDOWUPDATE = 1024;
            public const int DCX_VALIDATE = 2097152;
        }

        public struct WINDOWPOS
        {
            public IntPtr hWnd;
            public IntPtr hHndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int flags;
        }

        public class SC
        {
            public const int SC_SIZE = 61440;
            public const int SC_MOVE = 61456;
            public const int SC_MINIMIZE = 61472;
            public const int SC_MAXIMIZE = 61488;
            public const int SC_NEXTWINDOW = 61504;
            public const int SC_PREVWINDOW = 61520;
            public const int SC_CLOSE = 61536;
            public const int SC_VSCROLL = 61552;
            public const int SC_HSCROLL = 61568;
            public const int SC_MOUSEMENU = 61584;
            public const int SC_KEYMENU = 61696;
            public const int SC_ARRANGE = 61712;
            public const int SC_RESTORE = 61728;
            public const int SC_TASKLIST = 61744;
            public const int SC_SCREENSAVE = 61760;
            public const int SC_HOTKEY = 61776;
            public const int SC_CONTEXTHELP = 61824;
            public const int SC_DRAGMOVE = 61458;
            public const int SC_SYSMENU = 61587;
        }

        public class HT
        {
            public const int HTERROR = -2;
            public const int HTTRANSPARENT = -1;
            public const int HTNOWHERE = 0;
            public const int HTCLIENT = 1;
            public const int HTCAPTION = 2;
            public const int HTSYSMENU = 3;
            public const int HTGROWBOX = 4;
            public const int HTSIZE = 4;
            public const int HTMENU = 5;
            public const int HTHSCROLL = 6;
            public const int HTVSCROLL = 7;
            public const int HTMINBUTTON = 8;
            public const int HTMAXBUTTON = 9;
            public const int HTLEFT = 10;
            public const int HTRIGHT = 11;
            public const int HTTOP = 12;
            public const int HTTOPLEFT = 13;
            public const int HTTOPRIGHT = 14;
            public const int HTBOTTOM = 15;
            public const int HTBOTTOMLEFT = 16;
            public const int HTBOTTOMRIGHT = 17;
            public const int HTBORDER = 18;
            public const int HTREDUCE = 8;
            public const int HTZOOM = 9;
            public const int HTSIZEFIRST = 10;
            public const int HTSIZELAST = 17;
            public const int HTOBJECT = 19;
            public const int HTCLOSE = 20;
            public const int HTHELP = 21;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class NONCLIENTMETRICS
        {
            public int cbSize = Marshal.SizeOf(typeof(NativeMethods.NONCLIENTMETRICS));
            public int iBorderWidth;
            public int iScrollWidth;
            public int iScrollHeight;
            public int iCaptionWidth;
            public int iCaptionHeight;
            [MarshalAs(UnmanagedType.Struct)]
            public NativeMethods.LOGFONT lfCaptionFont;
            public int iSmCaptionWidth;
            public int iSmCaptionHeight;
            [MarshalAs(UnmanagedType.Struct)]
            public NativeMethods.LOGFONT lfSmCaptionFont;
            public int iMenuWidth;
            public int iMenuHeight;
            [MarshalAs(UnmanagedType.Struct)]
            public NativeMethods.LOGFONT lfMenuFont;
            [MarshalAs(UnmanagedType.Struct)]
            public NativeMethods.LOGFONT lfStatusFont;
            [MarshalAs(UnmanagedType.Struct)]
            public NativeMethods.LOGFONT lfMessageFont;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class LOGFONT
        {
            public int lfHeight;
            public int lfWidth;
            public int lfEscapement;
            public int lfOrientation;
            public int lfWeight;
            public byte lfItalic;
            public byte lfUnderline;
            public byte lfStrikeOut;
            public byte lfCharSet;
            public byte lfOutPrecision;
            public byte lfClipPrecision;
            public byte lfQuality;
            public byte lfPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string lfFaceName;
        }

        public enum ChangeWindowMessageFilterFlags
        {
            Add = 1,
            Remove = 2,
        }

        private static class UnsafeNativeMethods
        {
            [DllImport("version.dll", SetLastError = true)]
            internal static extern bool VerQueryValue(byte[] pBlock, string lpSubBlock, out IntPtr lplpBuffer, out int puLen);

            [DllImport("version.dll", SetLastError = true)]
            internal static extern int GetFileVersionInfoSize(string lptstrFilename, out int dwSize);

            [DllImport("version.dll", SetLastError = true)]
            internal static extern bool GetFileVersionInfo(string lptstrFilename, int dwHandleIgnored, int dwLen, byte[] lpData);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern IntPtr GetFocus();

            [DllImport("ComCtl32.dll", CharSet = CharSet.Auto)]
            internal static extern int SetWindowSubclass(IntPtr hWnd, NativeMethods.Win32SubClassProc newProc, IntPtr uIdSubclass, IntPtr dwRefData);

            [DllImport("ComCtl32.dll", CharSet = CharSet.Auto)]
            internal static extern int RemoveWindowSubclass(IntPtr hWnd, NativeMethods.Win32SubClassProc newProc, IntPtr uIdSubclass);

            [DllImport("ComCtl32.dll", CharSet = CharSet.Auto)]
            internal static extern IntPtr DefSubclassProc(IntPtr hWnd, IntPtr Msg, IntPtr wParam, IntPtr lParam);

            [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int GetTextMetricsA(HandleRef hDC, ref NativeMethods.TEXTMETRICA lptm);

            [DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern int GetTextMetricsW(HandleRef hDC, ref NativeMethods.TEXTMETRIC lptm);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool FlashWindowEx(ref NativeMethods.FLASHWINFO pwfi);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern ushort GlobalAddAtom(string lpString);

            [DllImport("user32.dll")]
            public static extern bool GetClientRect(IntPtr hWnd, out NativeMethods.RECT lpRect);

            [DllImport("SHELL32", CallingConvention = CallingConvention.StdCall)]
            internal static extern int SHAppBarMessage(int dwMessage, ref NativeMethods.APPBARDATA pData);

            [DllImport("user32.dll")]
            internal static extern bool DrawMenuBar(IntPtr hWnd);

            [DllImport("user32.dll")]
            internal static extern IntPtr LoadImage(IntPtr hinst, int iconId, uint uType, int cxDesired, int cyDesired, uint fuLoad);

            [DllImport("user32.dll")]
            internal static extern int DestroyIcon(IntPtr hIcon);

            [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal static extern IntPtr CreateWindowEx(int dwExStyle, IntPtr classAtom, string lpWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool DestroyWindow(IntPtr hwnd);

            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
            internal static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
            internal static extern int RegisterClass(ref NativeMethods.WNDCLASS lpWndClass);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool UnregisterClass(IntPtr classAtom, IntPtr hInstance);

            [DllImport("GDI32.dll")]
            internal static extern int RestoreDC(IntPtr hdc, int savedDC);

            [DllImport("GDI32.dll")]
            internal static extern int SaveDC(IntPtr hdc);

            [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern int BitBlt(HandleRef hDC, int x, int y, int nWidth, int nHeight, HandleRef hSrcDC, int xSrc, int ySrc, int dwRop);

            [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

            [DllImport("gdi32.dll", EntryPoint = "GdiAlphaBlend")]
            internal static extern bool AlphaBlend(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, NativeMethods.BLENDFUNCTION blendFunction);

            [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern IntPtr SelectObject(HandleRef hdc, HandleRef obj);

            [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);

            [DllImport("gdi32.dll")]
            internal static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

            [DllImport("gdi32.dll")]
            internal static extern int GetDIBits(HandleRef hdc, HandleRef hbm, int arg1, int arg2, IntPtr arg3, ref NativeMethods.BITMAPINFO_FLAT bmi, int arg5);

            [DllImport("gdi32.dll")]
            internal static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int width, int height);

            [DllImport("gdi32.dll")]
            internal static extern IntPtr CreateCompatibleBitmap(HandleRef hDC, int width, int height);

            [DllImport("gdi32.dll")]
            internal static extern IntPtr CreateCompatibleDC(HandleRef hDC);

            [DllImport("gdi32.dll")]
            internal static extern IntPtr CreateCompatibleDC(IntPtr hDC);

            [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
            internal static extern bool GetTextMetrics(HandleRef hdc, out NativeMethods.TEXTMETRIC lptm);

            [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
            public static extern int WideCharToMultiByte(int codePage, int flags, [MarshalAs(UnmanagedType.LPWStr)] string wideStr, int chars, [In, Out] byte[] pOutBytes, int bufferBytes, IntPtr defaultChar, IntPtr pDefaultUsed);

            internal static int GetTextExtentPoint32(HandleRef hDC, string text, ref NativeMethods.SIZE size)
            {
                int length = text.Length;
                int num;
                if (Marshal.SystemDefaultCharSize == 1)
                {
                    byte[] numArray = new byte[NativeMethods.UnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, (byte[])null, 0, IntPtr.Zero, IntPtr.Zero)];
                    NativeMethods.UnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, numArray, numArray.Length, IntPtr.Zero, IntPtr.Zero);
                    int byteCount = Math.Min(text.Length, 8192);
                    num = NativeMethods.UnsafeNativeMethods.GetTextExtentPoint32A(hDC, numArray, byteCount, ref size);
                }
                else
                    num = NativeMethods.UnsafeNativeMethods.GetTextExtentPoint32W(hDC, text, text.Length, ref size);
                return num;
            }

            [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
            internal static extern int GetTextExtentPoint32A(HandleRef hDC, byte[] lpszString, int byteCount, ref NativeMethods.SIZE size);

            [DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal static extern int GetTextExtentPoint32W(HandleRef hDC, [MarshalAs(UnmanagedType.LPWStr)] string text, int len, ref NativeMethods.SIZE size);

            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(HandleRef hDC);

            [DllImport("gdi32.dll")]
            internal static extern bool DeleteDC(IntPtr hDC);

            [DllImport("gdi32.dll")]
            internal static extern bool DeleteObject(HandleRef hObject);

            [DllImport("gdi32.dll")]
            internal static extern bool DeleteObject(IntPtr hObject);

            [DllImport("gdi32.dll", SetLastError = true)]
            internal static extern IntPtr CreateDIBSection(HandleRef hdc, ref NativeMethods.BITMAPINFO_FLAT bmi, int iUsage, ref IntPtr ppvBits, IntPtr hSection, int dwOffset);

            [DllImport("gdi32.dll", SetLastError = true)]
            internal static extern IntPtr CreateDIBSection(IntPtr hdc, ref NativeMethods.BITMAPINFO_SMALL bmi, int iUsage, int pvvBits, IntPtr hSection, int dwOffset);

            [DllImport("gdi32.dll")]
            internal static extern int GetPaletteEntries(IntPtr hPal, int startIndex, int entries, byte[] palette);

            [DllImport("comctl32.dll")]
            internal static extern bool _TrackMouseEvent(NativeMethods.TRACKMOUSEEVENTStruct tme);

            [DllImport("user32.dll")]
            internal static extern IntPtr TrackPopupMenu(IntPtr menuHandle, int uFlags, int x, int y, int nReserved, IntPtr hwnd, IntPtr par);

            [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern bool GetViewportOrgEx(IntPtr hDC, ref NativeMethods.POINT point);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern bool ScrollWindowEx(IntPtr hWnd, int nXAmount, int nYAmount, NativeMethods.RECT rectScrollRegion, ref NativeMethods.RECT rectClip, IntPtr hrgnUpdate, ref NativeMethods.RECT prcUpdate, int flags);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern bool ScrollWindowEx(IntPtr hWnd, int nXAmount, int nYAmount, IntPtr rectScrollRegion, ref NativeMethods.RECT rectClip, IntPtr hrgnUpdate, ref NativeMethods.RECT prcUpdate, int flags);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern int ScrollWindowEx(IntPtr hWnd, int dx, int dy, ref NativeMethods.RECT scrollRect, ref NativeMethods.RECT clipRect, IntPtr hrgnUpdate, IntPtr updateRect, int flags);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern bool ScrollWindow(IntPtr hWnd, int nXAmount, int nYAmount, ref NativeMethods.RECT rectScrollRegion, ref NativeMethods.RECT rectClip);

            [DllImport("USER32.dll")]
            internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

            [DllImport("USER32.dll")]
            internal static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, int flags);

            [DllImport("USER32.dll")]
            internal static extern IntPtr GetWindowDC(IntPtr hwnd);

            [DllImport("USER32.dll")]
            internal static extern int GetClassLong(IntPtr hwnd, int flags);

            [DllImport("USER32.dll")]
            internal static extern int GetWindowLong(IntPtr hwnd, int flags);

            [DllImport("USER32.dll")]
            internal static extern int SetWindowLong(IntPtr hwnd, int flags, int val);

            [DllImport("user32.dll")]
            internal static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

            [DllImport("USER32.dll")]
            internal static extern IntPtr GetDesktopWindow();

            [DllImport("USER32.dll")]
            internal static extern bool RedrawWindow(IntPtr hwnd, IntPtr rcUpdate, IntPtr hrgnUpdate, int flags);

            [DllImport("USER32.dll")]
            internal static extern short GetAsyncKeyState(int vKey);

            [DllImport("USER32.dll")]
            internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

            [DllImport("USER32.dll")]
            internal static extern int SetCapture(IntPtr hWnd);

            [DllImport("USER32.dll")]
            internal static extern bool ReleaseCapture();

            [DllImport("USER32.dll")]
            internal static extern bool IsWindowVisible(IntPtr hWnd);

            [DllImport("USER32.dll", CharSet = CharSet.Auto)]
            internal static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

            [DllImport("USER32.dll", CharSet = CharSet.Auto)]
            internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, int lParam);

            [DllImport("USER32.dll", CharSet = CharSet.Auto)]
            internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

            [DllImport("USER32.dll")]
            internal static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll")]
            internal static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref NativeMethods.COPYDATASTRUCT lParam);

            [DllImport("USER32.dll")]
            internal static extern int PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

            [DllImport("USER32.dll")]
            internal static extern bool IsZoomed(IntPtr hwnd);

            [DllImport("USER32.dll")]
            internal static extern bool IsIconic(IntPtr hwnd);

            [DllImport("USER32.dll")]
            internal static extern bool GetWindowRect(IntPtr hWnd, ref NativeMethods.RECT lpRect);

            [DllImport("USER32.dll")]
            internal static extern bool ValidateRect(IntPtr hwnd, ref NativeMethods.RECT lpRect);

            [DllImport("User32.dll")]
            internal static extern int GetUpdateRect(IntPtr hwnd, ref NativeMethods.RECT rect, bool erase);

            [DllImport("USER32.dll")]
            internal static extern IntPtr BeginPaint(IntPtr hWnd, [In, Out] ref NativeMethods.PAINTSTRUCT lpPaint);

            [DllImport("USER32.dll")]
            internal static extern bool EndPaint(IntPtr hWnd, ref NativeMethods.PAINTSTRUCT lpPaint);

            [DllImport("USER32.dll")]
            internal static extern bool LockWindowUpdate(IntPtr hWndLock);

            [DllImport("USER32.dll")]
            internal static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);

            [DllImport("gdi32.dll")]
            internal static extern int GetClipBox(IntPtr hdc, out NativeMethods.RECT lprc);

            [DllImport("GDI32.dll")]
            internal static extern int CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1, IntPtr hrgnSrc2, int fnCombineMode);

            [DllImport("GDI32.dll")]
            internal static extern int ExcludeClipRect(IntPtr hdc, int left, int top, int right, int bottom);

            [DllImport("GDI32.dll")]
            internal static extern int GetClipRgn(IntPtr hdc, IntPtr hrgn);

            [DllImport("GDI32.dll")]
            internal static extern int SelectClipRgn(IntPtr hdc, IntPtr hrgn);

            [DllImport("GDI32.dll")]
            internal static extern int ExtSelectClipRgn(IntPtr hdc, IntPtr hrgn, int mode);

            [DllImport("gdi32.dll")]
            internal static extern bool LPtoDP(IntPtr hdc, [In, Out] NativeMethods.POINT[] lpPoints, int nCount);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern bool GetUpdateRgn(IntPtr hwnd, IntPtr hrgn, bool fErase);

            [DllImport("gdi32.dll")]
            internal static extern int GetRegionData(IntPtr hRgn, int dwCount, IntPtr lpRgnData);

            [DllImport("gdi32.dll")]
            internal static extern int OffsetRgn(IntPtr hrgn, int nXOffset, int nYOffset);

            [DllImport("user32.dll", SetLastError = true)]
            internal static extern int MapWindowPoints(IntPtr hwndFrom, IntPtr hwndTo, ref NativeMethods.POINT lpPoints, [MarshalAs(UnmanagedType.U4)] int cPoints);

            [DllImport("gdi32.dll")]
            internal static extern int GetRandomRgn(IntPtr hdc, IntPtr hrgn, int iNum);

            [DllImport("GDI32.dll")]
            internal static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

            [DllImport("GDI32.dll")]
            internal static extern bool RectVisible(IntPtr hdc, ref NativeMethods.RECT rect);

            [DllImport("User32.dll", CharSet = CharSet.Auto)]
            internal static extern bool DragDetect(IntPtr hwnd, NativeMethods.POINT pt);

            [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
            internal static extern int GetObject(IntPtr hObject, int nSize, [In, Out] NativeMethods.LOGFONT lf);

            [DllImport("gdi32.dll")]
            internal static extern IntPtr SelectPalette(IntPtr hdc, IntPtr hpal, bool bForceBackground);

            [DllImport("gdi32.dll")]
            internal static extern int RealizePalette(IntPtr hdc);

            [DllImport("User32.dll")]
            internal static extern NativeMethods.HDC GetDC(NativeMethods.HWND handle);

            [DllImport("Gdi32.dll")]
            internal static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

            [DllImport("User32.dll")]
            internal static extern IntPtr GetCursor();

            [DllImport("User32.dll")]
            internal static extern bool SetSystemCursor(IntPtr hCursor, int id);

            [DllImport("Gdi32.dll")]
            internal static extern IntPtr CreateSolidBrush(NativeMethods.COLORREF aColorRef);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool FillRect(NativeMethods.HDC hdc, ref NativeMethods.RECT rect, IntPtr hbrush);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool FillRect(IntPtr hdc, ref NativeMethods.RECT rect, IntPtr hbrush);

            [DllImport("GDI32.dll")]
            internal static extern bool FillRgn(IntPtr hdc, IntPtr hrgn, IntPtr brush);

            [DllImport("gdi32.dll")]
            internal static extern int GetPixel(IntPtr hdc, int nXPos, int nYPos);

            [DllImport("gdi32.dll")]
            internal static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, int dwRop);

            [DllImport("User32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool UnhookWindowsHookEx(NativeMethods.HHOOK aHook);

            [DllImport("User32.dll")]
            internal static extern IntPtr CopyIcon(IntPtr hCursor);

            [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern IntPtr SetWindowsHookEx(int idHook, NativeMethods.LowLevelMouseProc lpfn, IntPtr hMod, int dwThreadId);

            [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

            [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern IntPtr GetModuleHandle(string lpModuleName);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref NativeMethods.POINT pptDst, ref NativeMethods.SIZE pSizeDst, IntPtr hdcSrc, ref NativeMethods.POINT pptSrc, int crKey, ref NativeMethods.BLENDFUNCTION pBlend, int dwFlags);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern bool InvalidateRgn(IntPtr hWnd, IntPtr hrgn, bool erase);

            [DllImport("user32.dll")]
            internal static extern bool SetLayeredWindowAttributes(IntPtr hwnd, int crKey, byte bAlpha, uint dwFlags);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern bool AdjustWindowRectEx(ref NativeMethods.RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);

            [DllImport("user32.dll")]
            internal static extern IntPtr FindWindow(string className, string windowText);

            [DllImport("user32.dll")]
            internal static extern int ShowWindow(IntPtr hWnd, int command);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool ShowWindow(IntPtr hWnd, NativeMethods.ShowWindowCommands nCmdShow);

            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool GetWindowPlacement(IntPtr hWnd, out NativeMethods.WINDOWPLACEMENT lpwndpl);

            [DllImport("User32.dll")]
            internal static extern IntPtr WindowFromPoint(Point pt);

            [DllImport("User32.dll")]
            internal static extern IntPtr GetWindow(IntPtr hWnd, uint wCmd);

            [DllImport("User32.dll")]
            internal static extern IntPtr SetActiveWindow(IntPtr hWnd);

            [DllImport("User32.dll")]
            internal static extern bool SetForegroundWindow(IntPtr hWnd);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern bool EnableWindow(IntPtr hWnd, bool enable);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern bool IsWindowEnabled(IntPtr hWnd);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern bool SystemParametersInfo(int uiAction, int uiParam, [In, Out] NativeMethods.NONCLIENTMETRICS pvParam, int fWinIni);

            [DllImport("user32")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool SetGestureConfig(IntPtr hWnd, int dwReserved, int cIDs, [In, Out] NativeMethods.GESTURECONFIG[] pGestureConfig, int cbSize);

            [DllImport("UxTheme")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool BeginPanningFeedback(IntPtr hWnd);

            [DllImport("UxTheme")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool EndPanningFeedback(IntPtr hWnd, bool fAnimateBack);

            [DllImport("UxTheme")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool UpdatePanningFeedback(IntPtr hwnd, int lTotalOverpanOffsetX, int lTotalOverpanOffsetY, bool fInInertia);

            [DllImport("user32")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool CloseGestureInfoHandle(IntPtr hGestureInfo);

            [DllImport("user32")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool GetGestureInfo(IntPtr hGestureInfo, ref NativeMethods.GESTUREINFO pGestureInfo);

            [DllImport("dwmapi.dll")]
            internal static extern int DwmSetIconicThumbnail(IntPtr hwnd, IntPtr hbitmap, uint flags);

            [DllImport("dwmapi.dll")]
            internal static extern int DwmSetIconicLivePreviewBitmap(IntPtr hwnd, IntPtr hbitmap, IntPtr ptClient, uint flags);

            [DllImport("dwmapi.dll")]
            internal static extern int DwmInvalidateIconicBitmaps(IntPtr hwnd);

            [DllImport("dwmapi.dll")]
            internal static extern int DwmSetWindowAttribute(IntPtr hwnd, uint dwAttributeToSet, IntPtr pvAttributeValue, uint cbAttribute);

            [DllImport("user32.dll")]
            internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool ClientToScreen(IntPtr hwnd, ref NativeMethods.POINT point);

            [DllImport("Shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern int ExtractIconEx(string fileName, int iconStartingIndex, IntPtr[] largeIcons, IntPtr[] smallIcons, int iconCount);

            [DllImport("shell32.dll")]
            internal static extern void SHAddToRecentDocs(NativeMethods.ShellAddToRecentDocs flags, [MarshalAs(UnmanagedType.LPWStr)] string path);

            [DllImport("Ole32.dll", PreserveSig = false)]
            internal static extern void PropVariantClear([In, Out] NativeMethods.PropVariant pvar);

            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
            internal static extern uint RegisterWindowMessage(string lpProcName);

            [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal static extern IntPtr RemoveProp(IntPtr hWnd, string lpString);

            [DllImport("user32.dll", SetLastError = true)]
            internal static extern bool SetProp(IntPtr hWnd, string lpString, IntPtr hData);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern IntPtr GetCapture();

            [DllImport("user32.dll")]
            internal static extern void PostQuitMessage(int exitCode);

            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern IntPtr LocalFree(IntPtr p);

            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern IntPtr LocalAlloc(int flag, int size);

            [DllImport("user32.dll")]
            internal static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

            [DllImport("shell32.dll", SetLastError = true)]
            internal static extern void SetCurrentProcessExplicitAppUserModelID([MarshalAs(UnmanagedType.LPWStr)] string AppID);

            [DllImport("gdi32.dll")]
            internal static extern bool SetViewportOrgEx(IntPtr hdc, int X, int Y, out NativeMethods.POINT lpPoint);

            [DllImport("user32")]
            internal static extern bool ChangeWindowMessageFilter(int msg, NativeMethods.ChangeWindowMessageFilterFlags flags);
        }

        internal struct LanguageCodePage
        {
            internal short language;
            internal short codePage;

            internal void SomeMethod()
            {
                this.language = (short)0;
                this.codePage = (short)0;
            }
        }

        public delegate IntPtr Win32SubClassProc(IntPtr hWnd, IntPtr Msg, IntPtr wParam, IntPtr lParam, IntPtr uIdSubclass, IntPtr dwRefData);

        public delegate int EditWordBreakProc(string lpch, int ichCurrent, int cch, int code);

        public delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        public delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        internal struct TEXTMETRICA
        {
            public int tmHeight;
            public int tmAscent;
            public int tmDescent;
            public int tmInternalLeading;
            public int tmExternalLeading;
            public int tmAveCharWidth;
            public int tmMaxCharWidth;
            public int tmWeight;
            public int tmOverhang;
            public int tmDigitizedAspectX;
            public int tmDigitizedAspectY;
            public byte tmFirstChar;
            public byte tmLastChar;
            public byte tmDefaultChar;
            public byte tmBreakChar;
            public byte tmItalic;
            public byte tmUnderlined;
            public byte tmStruckOut;
            public byte tmPitchAndFamily;
            public byte tmCharSet;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct TEXTMETRIC
        {
            public int tmHeight;
            public int tmAscent;
            public int tmDescent;
            public int tmInternalLeading;
            public int tmExternalLeading;
            public int tmAveCharWidth;
            public int tmMaxCharWidth;
            public int tmWeight;
            public int tmOverhang;
            public int tmDigitizedAspectX;
            public int tmDigitizedAspectY;
            public char tmFirstChar;
            public char tmLastChar;
            public char tmDefaultChar;
            public char tmBreakChar;
            public byte tmItalic;
            public byte tmUnderlined;
            public byte tmStruckOut;
            public byte tmPitchAndFamily;
            public byte tmCharSet;
        }
    }
}
