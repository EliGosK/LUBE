using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

/*
 * Update Date : 10-03-2005 02:36PM
 *				Add new property for resize the last column to fit with the width of control if the view is "details"
 *				Property Name is "AutoSizeLastColumn"					
 * 
 * 
 * */



namespace EAP.Framework.Windows.Controls
{
	public delegate void QueryToolTipText(EAPListView sender, int itemIndex, int subItemIndex, ref string tooltipText);
	/// <summary>
	/// Class EAPListView เป็น class ที่ inherits มาจาก System.Window.Forms.ListView โดยหน้าที่ของ class นี้ ก็จะเหมือนกับ ListView ธรรมดา เพียงแต่ว่า
	/// 1. จะเพิ่มความสามารถในการแสดงผลให้เร็วขึ้น
	/// </summary>
	public class EAPListView : System.Windows.Forms.ListView{

		#region WINDOW MESSAGE CONSTANT
		private const int WM_ERASEBKGND = 0x0014;
		private const int WM_PAINT = 0x000F;
		private const int WM_NOTIFY = 0x4E;
		#endregion

		#region List View Constant
		private const int LVM_FIRST = 0x1000;
		private const int LVM_GETITEMRECT = LVM_FIRST + 14;
		private const int LVM_GETCOLUMNWIDTH = LVM_FIRST + 29;
		private const int LVM_SUBITEMHITTEST = LVM_FIRST + 57; 
		private const int LVM_GETSTRINGWIDTHW =	LVM_FIRST + 87;
		private const int LVIR_LABEL = 2;
		#endregion

		#region Constant
		// tooltip
		private const int TTN_FIRST = -520;
		private const int TTN_NEEDTEXT = (TTN_FIRST - 10);
		#endregion

		#region RECT
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
			public struct RECT {
			public int left;
			public int top;
			public int right;
			public int bottom;
		}
		#endregion

		#region LVHITTESTINFO
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
			private struct LVHITTESTINFO {
			public Point pt;
			public int flags;
			public int iItem;
			public int iSubItem;
		}
		#endregion

		#region NMHDR
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
			private struct NMHDR {
			public IntPtr hwndFrom;
			public int idFrom;
			public int code;
		}
		#endregion

		#region DLL Import to used
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern bool ValidateRect(IntPtr handle, ref RECT rect);
		[DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
		private static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, IntPtr lParam);
		// overloaded for wParam type
		[DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
		private static extern IntPtr SendMessage(IntPtr hWnd, int uMsg, IntPtr wParam, IntPtr lParam);

		#endregion

		private bool m_bIsUpdating;
		private int m_iItemNumber;
		private ToolTip m_toolTip = new ToolTip();
		private bool m_bAutoSizeLastColumn;

		public event QueryToolTipText OnQueryToolTipText = null;

		public EAPListView() {
			
			SetStyle(ControlStyles.EnableNotifyMessage, true);

			m_bAutoSizeLastColumn = true;
		}

		
		protected override void OnNotifyMessage(Message m) {
			if ( m.Msg == WM_NOTIFY ) {
				NMHDR n = (NMHDR)Marshal.PtrToStructure( m.LParam, typeof(NMHDR) );

				if( n.code == TTN_NEEDTEXT ) {
					NeedText();
				}
			}
		}

		private void ListView_SubItemHitTest(ref LVHITTESTINFO lvhi) {
			IntPtr ptr = Marshal.AllocHGlobal( Marshal.SizeOf(lvhi) );
			Marshal.StructureToPtr(lvhi, ptr, true);
								
			SendMessage(Handle, LVM_SUBITEMHITTEST, IntPtr.Zero, ptr);
								
			lvhi = (LVHITTESTINFO)Marshal.PtrToStructure(ptr, typeof(LVHITTESTINFO));
			Marshal.FreeHGlobal(ptr);
			
		}

		private void NeedText() {
			LVHITTESTINFO lvh = new LVHITTESTINFO();
			lvh.pt = PointToClient(Control.MousePosition);
			ListView_SubItemHitTest(ref lvh);

			if (IsItemTextHidden(lvh)){
				string str = string.Empty;
				if (OnQueryToolTipText != null){
					OnQueryToolTipText(this, lvh.iItem, lvh.iSubItem, ref str);
					m_toolTip.SetToolTip(this, str);
				}
			}else{
				m_toolTip.SetToolTip(this, string.Empty);
			}
		}

		/// <summary>
		/// Finds whether the listview item text is completely visible or 
		/// contains a trailing ellipsis "...".
		/// </summary>
		/// <param name="lvhi">List view hit test information structure</param>
		/// <returns>True if text is hidden, false otherwise</returns>
		private bool IsItemTextHidden(LVHITTESTINFO lvhi) {
			if (lvhi.iItem <= -1)
				return false;
			Rectangle rect = Rectangle.Empty;
			int stringWidth, colWidth;

			if( lvhi.iSubItem > 0 ) {
				// MSDN : ListView_GetStringWidth() talks something about padding.
				// for subitem: The text is padded with 6 pixels on either sides

				stringWidth = ListView_GetStringWidth(Items[lvhi.iItem].SubItems[lvhi.iSubItem].Text);
				colWidth = ListView_GetColumnWidth(lvhi.iSubItem);
				return ((stringWidth + 12) > colWidth);

			}
			else {
				// MSDN : ListView_GetStringWidth() talks something about padding.
				// for item: The text is padded with 2 pixel on either sides

				stringWidth = ListView_GetStringWidth(Items[lvhi.iItem].Text);
				colWidth = ListView_GetColumnWidth(0);
				ListView_GetItemRect(lvhi.iItem, LVIR_LABEL, ref rect);
				rect = Rectangle.Inflate(rect, -2, -2);
				return ((rect.Left + stringWidth + 4) > colWidth);

			}

		}
		private bool ListView_GetItemRect(int iItem, int code, ref Rectangle lpRect) {
			Rectangle rct = new Rectangle();
			IntPtr pRct = Marshal.AllocHGlobal(Marshal.SizeOf(rct));
			Marshal.StructureToPtr(rct, pRct, true);

			SendMessage(Handle, LVM_GETITEMRECT, iItem, pRct);
			
			lpRect = (Rectangle)Marshal.PtrToStructure(pRct, typeof(Rectangle));
			Marshal.FreeHGlobal(pRct);

			return true;

		}
		private int ListView_GetColumnWidth(int iCol) {
			return SendMessage(Handle, LVM_GETCOLUMNWIDTH, iCol, IntPtr.Zero);
		}

		private int ListView_GetStringWidth(string psz) {
			IntPtr ptr = Marshal.StringToHGlobalAuto(psz);
			int ret = SendMessage(Handle, LVM_GETSTRINGWIDTHW, 0, ptr);
			Marshal.FreeHGlobal(ptr);

			return ret;

		}

		public void UpdateItem(int iIndex) {
			m_bIsUpdating = true;
			m_iItemNumber = iIndex;
			this.Update();
			m_bIsUpdating = false;
		}

		public bool AutoSizeLastColumn{
			get{	return m_bAutoSizeLastColumn;}
			set{	
				m_bAutoSizeLastColumn = value;
				if (m_bAutoSizeLastColumn && this.Columns.Count > 0 && this.View == View.Details){
					this.Columns[this.Columns.Count-1].Width = -2;
				}
			}
		}

		protected override void WndProc(ref System.Windows.Forms.Message m) {
			if (m_bIsUpdating){
				switch (m.Msg){
					case WM_ERASEBKGND:
						m.Msg = 0;	// nothing
						break;
					case WM_PAINT:
						RECT vrect = this.GetWindowRECT();
						ValidateRect(this.Handle, ref vrect);
						Invalidate(this.Items[m_iItemNumber].Bounds);
						break;
				}
			}
			// check for auto size last column
			if (m.Msg == WM_PAINT){
				if (m_bAutoSizeLastColumn && this.Columns.Count > 0 && this.View == View.Details){
					this.Columns[this.Columns.Count-1].Width = -2;
				}
			}
			base.WndProc (ref m);
		}

		private RECT GetWindowRECT() {
			RECT rect = new RECT();
			rect.left = this.Left;
			rect.right = this.Right;
			rect.top = this.Top;
			rect.bottom = this.Bottom;
			return rect;
		}

		/// <summary>
		/// Provides data about the ItemHover event.
		/// </summary>
//		public class ItemHoverEventArgs : EventArgs {
//			// ref to listview item and sub items
//			protected int m_item;
//			protected int m_subitem;
//			protected bool m_itemTextVisible;
//
//			/// <summary>
//			/// The zero based index of a Listview item.
//			/// </summary>
//			public int Item {
//				get {
//					return m_item;
//				}
//				set {
//					m_item = value;
//				}
//
//			}
//
//			/// <summary>
//			/// The 1 based index of a ListviewSubitem item.
//			/// </summary>
//			public int SubItem {
//				get {
//					return m_subitem;
//				}
//				set {
//					m_subitem = value;
//				}
//			}
//
//			/// <summary>
//			/// The item or subitem has text which is currently 
//			/// TRUE = Invisible, FALSE = Visible.
//			/// </summary>
//			public bool ItemTextInVisible {
//				get {
//					return m_itemTextVisible;
//				}
//				set {
//					m_itemTextVisible = value;
//				}
//			}
//
//		}

//		public delegate void ItemHoverEventHandler(object sender, ItemHoverEventArgs e);

//		protected event ItemHoverEventHandler m_itemHover;
//		public event ItemHoverEventHandler ItemHover {
//			add {
//				m_itemHover += value;				
//			}
//			remove {
//				m_itemHover -= value;
//			}
//		}

	}


}
