/**
 * PageNavigator Control
 * Purpose : Create a page navigator look like in web page
 * Author : Kitisak Sangthong 
 * Create Date : 17 Jan 2007
 * */
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using EAP.Framework.Windows.Forms;


namespace EAP.Framework.Windows.Controls
{
	public class PageNavigator : Control{
		public delegate void PageClickHandler(int pageNumber);

		public event PageClickHandler OnPageClick = null;
		
		private const int MIN_PAGE = 1;
		private const int SPACE = 3;
		private const string CHAR_FIRST = "<<";
		private const string CHAR_PREV = "<";
		private const string CHAR_NEXT = ">";
		private const string CHAR_LAST = ">>";

		private Color m_clBordor;
		private Color m_clText;
		private Color m_clBgNormal;
		private Color m_clBgActive;
		private Color m_clBgHilight;
		private Color m_clBgDisable;

		private Pen m_penBorder;

		private Brush m_brBgNormal;
		private Brush m_brBgActive;
		private Brush m_brBgHilight;
		private Brush m_brBgDisable;
		private Brush m_brText;

		private Graphics m_gOff = null;
		private Bitmap m_bmpOff = null;

		private int m_iMaxPage = 10;
		private int m_iCurrentPage = 1;
		private int m_iPagePerScreen = 5;
		private string m_strPageText = "page";
		private StringAlignment m_alignment = StringAlignment.Near;

		private ArrayList m_arPageButton;

		public PageNavigator() {
			this.Size = new Size(300, 20);
//			this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint  | ControlStyles.ResizeRedraw, true);
			m_clBordor = Color.FromArgb(7,42,102);
			m_clText = Color.FromArgb(34,34,34);
			m_clBgNormal = Color.FromArgb(240,245,250);
			m_clBgActive = Color.FromArgb(255,201,165);
			m_clBgHilight = Color.FromArgb(255,201,255);
			m_clBgDisable = Color.FromArgb(236, 233, 216);

			m_penBorder = new Pen(m_clBordor, 1);

			m_brBgNormal = new SolidBrush(m_clBgNormal);
			m_brBgActive = new SolidBrush(m_clBgActive);
			m_brBgDisable = new SolidBrush(m_clBgDisable);
			m_brText = new SolidBrush(m_clText);
			m_brBgHilight = new SolidBrush(m_clBgHilight);

			m_arPageButton = new ArrayList();
		}

		protected override void OnFontChanged(EventArgs e) {
			CalculateLayout(m_gOff);
			this.Invalidate();
			base.OnFontChanged (e);
		}


		private float GetMax(float fCurrent, float fNewValue){
			return fNewValue > fCurrent ? fNewValue : fCurrent;
		}

		private void ReCreateOffScreenObject() {
			if (m_gOff != null) {
				m_gOff.Dispose();
				m_gOff = null;
				GC.Collect();
			}
			if (m_bmpOff != null) {
				m_bmpOff.Dispose();
				m_bmpOff = null;
				GC.Collect();
			}

			if (this.ClientRectangle.Width > 0 && this.ClientRectangle.Height > 0) {
				m_bmpOff = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
				m_gOff = Graphics.FromImage(m_bmpOff);
				m_gOff.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
//				m_gOff.FillRectangle(SystemBrushes.Control, this.ClientRectangle);
			}
			if (m_gOff != null){
				CalculateLayout(m_gOff);
			}
		}

		public int MaxPage{
			get{return m_iMaxPage;}
			set{
				if (m_iMaxPage != value){
					m_iMaxPage = value;
					if (m_iCurrentPage > m_iMaxPage)
						m_iCurrentPage = m_iMaxPage;
					RefreshInvalidate();
				}
			}
		}
		public int CurrentPage{
			get{return m_iCurrentPage;}
			set{
				if (m_iCurrentPage != value){
					m_iCurrentPage = value;
					if (m_iCurrentPage < MIN_PAGE){
						m_iCurrentPage = MIN_PAGE;
					}else if (m_iCurrentPage > m_iMaxPage){
						m_iCurrentPage = m_iMaxPage;
					}

					RefreshInvalidate();
				}
			}
		}
		public int PagePerScreen{
			get{return m_iPagePerScreen;}
			set{
				if (m_iPagePerScreen != value){
					m_iPagePerScreen = value;
					RefreshInvalidate();
				}
			}
		}
		public string PageText{
			get{return m_strPageText;}
			set{
				if (m_strPageText != value){
					m_strPageText =value;
					RefreshInvalidate();
				}
			}
		}

		private void RefreshInvalidate(){
			CalculateLayout(m_gOff);
			if (m_gOff != null){
				m_gOff.FillRectangle(SystemBrushes.Control, this.ClientRectangle);
			}
			this.Invalidate();
		}


		public StringAlignment Alignment{
			get{
				return m_alignment;
			}
			set{
				if (m_alignment != value){
					m_alignment = value;
					RefreshInvalidate();
				}
			}
		}

		private Pen GetButtonPen(PageButton pb){
			if (pb.Active && pb.State != ButtonState.Hilight){
				return m_penBorder;
			}else{
				switch (pb.State){
					case ButtonState.Hilight:
						return m_penBorder;
					case ButtonState.Normal:
						return m_penBorder;
					case ButtonState.Disable:
						return SystemPens.ControlDark;
					default:
						return m_penBorder;
				}
			}
		}

		private Brush GetButtonTextBrush(PageButton pb){
			if (pb.Active && pb.State != ButtonState.Hilight){
				return m_brText;
			}else{
				switch (pb.State){
					case ButtonState.Hilight:
						return m_brText;
					case ButtonState.Normal:
						return m_brText;
					case ButtonState.Disable:
						return SystemBrushes.ControlDark;
					default:
						return m_brText;
				}
			}
		}

		private Brush GetButtonBrush(PageButton pb){
			if (pb.Active && pb.State != ButtonState.Hilight){
				return m_brBgActive;
			}else{
				switch (pb.State){
					case ButtonState.Hilight:
						return m_brBgHilight;
					case ButtonState.Normal:
						return m_brBgNormal;
					case ButtonState.Disable:
						return m_brBgDisable;
					default:
						return m_brBgNormal;
				}
			}
		}




		protected override void OnMouseDown(MouseEventArgs e) {
			bool bValidate = false;
			foreach (PageButton pb in m_arPageButton){
				if (pb.State != ButtonState.Disable){
					if (pb.HitTest(new PointF(e.X, e.Y))){
						if (pb.PageValue == -1){
							string strValue = m_iCurrentPage.ToString();
							if (InputDialog.Show(this, "Go To Page", ref strValue) == DialogResult.OK){
								double num = m_iCurrentPage;
								if (double.TryParse(strValue, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num)){
									m_iCurrentPage = Convert.ToInt32(num);
									if (m_iCurrentPage < MIN_PAGE){
										m_iCurrentPage = MIN_PAGE;
									}else if (m_iCurrentPage > m_iMaxPage){
										m_iCurrentPage = m_iMaxPage;
									}
									CalculateLayout(m_gOff);
									bValidate = true;
								}else{
									MessageDialog.ShowBusinessErrorMsg(this, "Invalid page number");
								}
							}
						}else{
							m_iCurrentPage = pb.PageValue;
							CalculateLayout(m_gOff);
							bValidate = true;
						}
						break;
					}
				}
			}
			if (bValidate){
				if (OnPageClick != null){
					OnPageClick(m_iCurrentPage);
				}
				if (m_gOff !=null){
					m_gOff.FillRectangle(SystemBrushes.Control, this.ClientRectangle);
				}
				this.Invalidate();
			}
			base.OnMouseDown (e);
		}
		protected override void OnMouseLeave(EventArgs e) {
			foreach (PageButton pb in m_arPageButton){
				if (pb.State == ButtonState.Hilight){
					Rectangle rc = new Rectangle((int)pb.Rect.X+1, (int)pb.Rect.Y+1, (int)pb.Rect.Width+1, (int)pb.Rect.Height+1);
					pb.State = ButtonState.Normal;
					this.Invalidate(rc);
				}
			}
			base.OnMouseLeave (e);
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			if (m_arPageButton.Count > 0){
				foreach (PageButton pb in m_arPageButton){
					Rectangle rc = new Rectangle((int)pb.Rect.X+1, (int)pb.Rect.Y+1, (int)pb.Rect.Width+1, (int)pb.Rect.Height+1);
					if (pb.State != ButtonState.Disable){
						if (pb.HitTest(new PointF(e.X, e.Y))){
							pb.State = ButtonState.Hilight;
							this.Invalidate(rc);
						}else{
							if (pb.State == ButtonState.Hilight){
								pb.State = ButtonState.Normal;
								this.Invalidate(rc);
							}
						}
					}
				} 
			}
			base.OnMouseMove (e);
		}


		protected override void OnSizeChanged(EventArgs e) {
			ReCreateOffScreenObject();
			base.OnSizeChanged (e);
		}

		private void Draw(RectangleF rcClip){
			if (m_gOff != null){
				if (m_arPageButton.Count <= 0){
					CalculateLayout(m_gOff);
				}
				StringFormat sf = new StringFormat();
				sf.LineAlignment = StringAlignment.Center;
				sf.Alignment = StringAlignment.Center;
				for (int i=0;i<m_arPageButton.Count;++i){
					PageButton pb = (PageButton)m_arPageButton[i];
					if (pb.Rect.IntersectsWith(rcClip)){
						// Draw Background
						m_gOff.FillRectangle(GetButtonBrush(pb), pb.Rect);
						// Draw Border
						m_gOff.DrawLines(GetButtonPen(pb), pb.RoundRect);
						// Draw Text
						if (pb.Active){
							m_gOff.DrawString(pb.Text, new Font(this.Font, FontStyle.Bold), GetButtonTextBrush(pb), pb.Rect, sf);
						}else{
							m_gOff.DrawString(pb.Text, this.Font, GetButtonTextBrush(pb), pb.Rect, sf);
						}
					}
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e) {
			Draw(e.ClipRectangle);
			if (m_bmpOff != null) {
				e.Graphics.DrawImage(m_bmpOff, new Point(0, 0));
			}
		}

		internal enum ButtonState{
			Normal, Hilight, Disable
		}

		internal class PageButton{
			private const int TEXT_OFFST = 4;
			private RectangleF m_rcClient = RectangleF.Empty;
			private string m_strText;
			private Graphics m_g;
			private Font m_font;
			private ButtonState m_buttonState = ButtonState.Normal;
			private bool m_bActive = false;
			private int m_iPageValue;
			public PageButton(Graphics g, Font f, float height, int iPageValue){
				m_rcClient.Height = height-1;
				m_g = g;
				m_font = f;
				m_iPageValue = iPageValue;
			}
			public PageButton(Graphics g, Font f, float height, int iPageValue, string text) : this(g, f, height, iPageValue){
				this.Text = text;
			}

			public int PageValue{get{return m_iPageValue;}}

			public string Text{
				get{return m_strText;}
				set{
					m_strText = value;
					m_rcClient.Width = m_g.MeasureString(m_strText, m_font).Width+TEXT_OFFST*2;
                    //Console.WriteLine(string.Format("{0} {1} {2}", m_strText, m_font, m_rcClient.Width));
				}
			}

			public bool Active{
				get{return m_bActive;}
				set{
					m_bActive = value;
				}
			}

			public ButtonState State{
				get{return m_buttonState;}
				set{
					m_buttonState = value;
				}
			}

			public PointF Location{
				get{return m_rcClient.Location;}
				set{
					m_rcClient.Location = value;
				}
			}

			public float Width{
				get{return m_rcClient.Width;}
				set{
					m_rcClient.Width = value;
				}
			}

			
			public PointF []RoundRect{
				get{
					return new PointF[]{
											new PointF(m_rcClient.X, m_rcClient.Y),
											new PointF(m_rcClient.Right, m_rcClient.Y),
											new PointF(m_rcClient.Right, m_rcClient.Bottom),
											new PointF(m_rcClient.X, m_rcClient.Bottom),
											new PointF(m_rcClient.X, m_rcClient.Y)
									   };
				}
			}

			public RectangleF Rect{
				get{return m_rcClient;}
			}

			public bool HitTest(PointF point){
				return m_rcClient.Contains(point);
			}
		}


		private void CalculateLayout(Graphics g){
			m_arPageButton = new ArrayList();
			float fMaxWidth = -1;
			int iStartPage = MIN_PAGE;
			int iEndPage = m_iMaxPage;
			if (m_iMaxPage > m_iPagePerScreen){
				iStartPage = m_iCurrentPage - (m_iPagePerScreen / 2);
				iEndPage = iStartPage + m_iPagePerScreen-1;
				if (iStartPage < 1){
					iEndPage += (1 - iStartPage);
					iStartPage = 1;
				}else if (iEndPage > m_iMaxPage){
					iStartPage -= (iEndPage - m_iMaxPage);
					iEndPage = m_iMaxPage;
				}
			}
			// Total Page
			PageButton pTotal = new PageButton(g, this.Font, this.ClientRectangle.Height, -1, string.Format("{0:#,##0} {1}", m_iMaxPage, m_strPageText));
			pTotal.Location = new PointF(0, 0);
			m_arPageButton.Add(pTotal);
			// First Button
			PageButton pFirst = new PageButton(g, this.Font, this.ClientRectangle.Height, MIN_PAGE, CHAR_FIRST);
			fMaxWidth = GetMax(fMaxWidth, pFirst.Rect.Width);
			pFirst.State = (iStartPage > MIN_PAGE) ? ButtonState.Normal : ButtonState.Disable;
			m_arPageButton.Add(pFirst);
			// Prev Button
			PageButton pPrev = new PageButton(g, this.Font, this.ClientRectangle.Height, m_iCurrentPage-1, CHAR_PREV);
			fMaxWidth = GetMax(fMaxWidth, pPrev.Rect.Width);
			pPrev.State = (m_iCurrentPage > MIN_PAGE) ? ButtonState.Normal : ButtonState.Disable;
			m_arPageButton.Add(pPrev);
			// Page Navigator
			for (int i=iStartPage;i<=iEndPage;++i){
				PageButton pButton = new PageButton(g, this.Font, this.ClientRectangle.Height, i, string.Format("{0:#,##0}", i));
				fMaxWidth = GetMax(fMaxWidth, pButton.Rect.Width);
				pButton.Active = (m_iCurrentPage == i);
				m_arPageButton.Add(pButton);
			}
			// Next Button
			PageButton pNext = new PageButton(g, this.Font, this.ClientRectangle.Height, m_iCurrentPage+1, CHAR_NEXT);
			fMaxWidth = GetMax(fMaxWidth, pNext.Rect.Width);
			pNext.State = (m_iCurrentPage < m_iMaxPage) ? ButtonState.Normal : ButtonState.Disable;
			m_arPageButton.Add(pNext);
			// Last Button
			PageButton pLast = new PageButton(g, this.Font, this.ClientRectangle.Height, m_iMaxPage, CHAR_LAST);
			fMaxWidth = GetMax(fMaxWidth, pLast.Rect.Width);
			pLast.State = (iEndPage < m_iMaxPage) ? ButtonState.Normal : ButtonState.Disable;
			m_arPageButton.Add(pLast);

			// Update Width & Location
			float fx = 0;
			switch (m_alignment){
				case StringAlignment.Near:
					pTotal.Location = new PointF(0, 0);
					fx += pTotal.Width + SPACE;
					for (int i=1;i<m_arPageButton.Count;++i){
						((PageButton)m_arPageButton[i]).Width = fMaxWidth;
						((PageButton)m_arPageButton[i]).Location = new PointF(fx, 0);
						fx += fMaxWidth + SPACE;
					}
					break;
				case StringAlignment.Center:
					float fTotalWidth = pTotal.Width + SPACE;
					fTotalWidth += (m_arPageButton.Count-1) * (fMaxWidth + SPACE);
					fx = (this.ClientRectangle.Width - fTotalWidth) / 2;
					pTotal.Location = new PointF(fx, 0);
					fx += pTotal.Width + SPACE;
					for (int i=1;i<m_arPageButton.Count;++i){
						((PageButton)m_arPageButton[i]).Width = fMaxWidth;
						((PageButton)m_arPageButton[i]).Location = new PointF(fx, 0);
						fx += fMaxWidth + SPACE;
					}
					break;
				case StringAlignment.Far:
					fx = this.ClientRectangle.Width;
					pTotal.Location = new PointF(fx - pTotal.Width-1, 0);
					fx -= pTotal.Width + SPACE;
					for (int i=m_arPageButton.Count-1;i>0;--i){
						((PageButton)m_arPageButton[i]).Width = fMaxWidth;
						fx -= ((PageButton)m_arPageButton[i]).Width + SPACE;
						((PageButton)m_arPageButton[i]).Location = new PointF(fx, 0);
						fx -= SPACE;
					}
					break;
			}


		}
	}
}
