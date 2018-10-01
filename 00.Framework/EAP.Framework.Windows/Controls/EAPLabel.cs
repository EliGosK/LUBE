using System;
using System.Drawing;

namespace EAP.Framework.Windows.Controls
{

	/// <summary>
	/// Kitisak Sangthong, If some error occor please let me know.. T_T
	/// </summary>
	public class EAPLabel : System.Windows.Forms.Label{

		private const int ALPHA = 255;

		private bool m_bEnableOverlayText = false;
		private bool m_bAddAlpha = true;
		private int m_iAlphaValue = ALPHA;
		private bool m_bAddShadow = true;
		private Bitmap m_offScreenBMP = null;
		private Graphics m_offScreenDC = null;

		public EAPLabel() : base(){
			CreateOffScreenDC();
		}


		private void CreateOffScreenDC(){
			Size sz = this.ClientRectangle.Size;
			if (sz.Width <= 0 || sz.Height <= 0){
				m_offScreenBMP = null;
				m_offScreenDC = null;
			}else{
				m_offScreenBMP = new Bitmap(sz.Width, sz.Height);
				m_offScreenDC = Graphics.FromImage(m_offScreenBMP);
			}
			GC.Collect();
		}

		private void DrawImage(Graphics g){
			if (this.Image == null)
				return;

			int x=0, y=0;
			switch (this.ImageAlign){
				case ContentAlignment.TopLeft:
					x = 0;
					y = 0;
					break;
				case ContentAlignment.TopCenter:
					x = this.ClientRectangle.Width/2 - this.Image.Width/2;
					y = 0;
					break;
				case ContentAlignment.TopRight:
					x = this.ClientRectangle.Width - this.Image.Width;
					break;
				case ContentAlignment.MiddleLeft:
					x = 0;
					y = this.ClientRectangle.Height/2 - this.Image.Height/2;
					break;
				case ContentAlignment.MiddleCenter:
					x = this.ClientRectangle.Width/2 - this.Image.Width/2;
					y = this.ClientRectangle.Height/2 - this.Image.Height/2;
					break;
				case ContentAlignment.MiddleRight:
					x = this.ClientRectangle.Width - this.Image.Width;
					y = this.ClientRectangle.Height/2 - this.Image.Height/2;
					break;
				case ContentAlignment.BottomLeft:
					x = 0;
					y = this.ClientRectangle.Height - this.Image.Height;
					break;
				case ContentAlignment.BottomCenter:
					x = this.ClientRectangle.Width/2 - this.Image.Width/2;
					y = this.ClientRectangle.Height - this.Image.Height;
					break;
				case ContentAlignment.BottomRight:
					x = this.ClientRectangle.Width - this.Image.Width;
					y = this.ClientRectangle.Height - this.Image.Height;
					break;
			}

			g.DrawImage(this.Image, x, y);
		}



		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			
			if (m_bEnableOverlayText && m_offScreenDC != null && this.Text != null && this.Text.Length > 0){

				// Clear backgroud to default color
				m_offScreenDC.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);
				
				// Draw Image
				DrawImage(m_offScreenDC);

				// Calculate to add adpha
				int alpha = ALPHA;
				if (m_bAddAlpha){
					alpha = m_iAlphaValue;
				}
//				int alpha = ALPHA;
//				if ( m_bAddAlpha ) {
//					alpha = 90 + (this.Text.Length * 2);
//					if ( alpha >= ALPHA )  alpha = ALPHA;
//				}
				// Create brush with alpha
				SolidBrush  b = new SolidBrush(Color.FromArgb(alpha, this.ForeColor));

				StringFormat strFormat = new StringFormat();//	 StringFormat.GenericTypographic;

				switch (this.TextAlign){
					case ContentAlignment.BottomCenter:
						strFormat.Alignment = StringAlignment.Center;
						strFormat.LineAlignment = StringAlignment.Far;
						break;
					case ContentAlignment.BottomLeft:
						strFormat.Alignment = StringAlignment.Near;
						strFormat.LineAlignment = StringAlignment.Far;
						break;
					case ContentAlignment.BottomRight:
						strFormat.Alignment = StringAlignment.Far;
						strFormat.LineAlignment = StringAlignment.Far;
						break;
					case ContentAlignment.MiddleCenter:
						strFormat.Alignment = StringAlignment.Center;
						strFormat.LineAlignment = StringAlignment.Center;
						break;
					case ContentAlignment.MiddleLeft:
						strFormat.Alignment = StringAlignment.Near;
						strFormat.LineAlignment = StringAlignment.Center;
						break;
					case ContentAlignment.MiddleRight:
						strFormat.Alignment = StringAlignment.Far;
						strFormat.LineAlignment = StringAlignment.Center;
						break;
					case ContentAlignment.TopCenter:
						strFormat.Alignment = StringAlignment.Center;
						strFormat.LineAlignment = StringAlignment.Near;
						break;
					case ContentAlignment.TopLeft:
						strFormat.Alignment = StringAlignment.Near;
						strFormat.LineAlignment = StringAlignment.Near;
						break;
					case ContentAlignment.TopRight:
						strFormat.Alignment = StringAlignment.Far;
						strFormat.LineAlignment = StringAlignment.Near;
						break;
				}


				m_offScreenDC.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

				RectangleF rect = this.ClientRectangle;

				if ( m_bAddShadow ) {
					SolidBrush  shadow = new SolidBrush(Color.FromArgb((int)(alpha / 2.0), this.ForeColor));
					RectangleF  sRect = new RectangleF((float)rect.X + (this.Font.Size * 0.1F), (float)rect.Y + (this.Font.Size * 0.1F), rect.Width, rect.Height);
					m_offScreenDC.DrawString(this.Text, this.Font, shadow, sRect, strFormat);
				}

				m_offScreenDC.DrawString(this.Text, this.Font, b, rect, strFormat);

				
				e.Graphics.DrawImage(m_offScreenBMP, 0, 0);
				//				this.CreateGraphics().DrawImage(m_offScreenBMP, 0, 0);
				GC.Collect();
			}else{
				base.OnPaint (e);
			}
		}

		protected override void OnSizeChanged(EventArgs e) {
			CreateOffScreenDC();
			base.OnSizeChanged (e);
		}

		public int AlphaValue{
			get{	return m_iAlphaValue;}
			set{
				if (value < 0){
					m_iAlphaValue = 0;
				}else if (value > ALPHA){
					m_iAlphaValue = ALPHA;
				}else{
					m_iAlphaValue = value;
				}
				this.Invalidate();
			}
		}

		public bool EnableOverlayText{
			get{	return m_bEnableOverlayText;}
			set{	
				m_bEnableOverlayText = value;
				this.Invalidate();
			}
		}

		public bool AddAlpha{
			get{	return m_bAddAlpha;}
			set{
				m_bAddAlpha = value;
				this.Invalidate();
			}
		}

		public bool AddShadow{
			get{	return m_bAddShadow;}
			set{
				m_bAddShadow = value;
				this.Invalidate();
			}
		}
	}
}