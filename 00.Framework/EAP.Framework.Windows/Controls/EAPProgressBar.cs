using System;
using System.Drawing;

namespace EAP.Framework.Windows.Controls
{
	/// <summary>
	/// 
	/// </summary>
	public class EAPProgressBar : System.Windows.Forms.Label {

		private int m_iMinValue = 0;
		private int m_iMaxValue = 100;
		private float m_iValue = 0;
		private string m_strDisplayFormat = "##0";
		private string m_strPrefix = string.Empty;
		private string m_strSuffix = "%";
		private bool m_bEnabledGraphics = true;
		private Color m_clComplete = Color.Green;
		private Color m_clEmpty = Color.White;

		public EAPProgressBar() : base(){
			this.TextAlign = ContentAlignment.MiddleCenter;
		}

		public bool EnabledGraphics{
			get{	return m_bEnabledGraphics;}
			set{
				m_bEnabledGraphics = value;
				this.Invalidate();
			}
		}

		public int MinValue{
			get{
				return m_iMinValue;
			}
			set{
				m_iMinValue = value;
				this.Invalidate();
			}
		}

		public int MaxValue{
			get{
				return m_iMaxValue;
			}
			set{
				m_iMaxValue = value;
				this.Invalidate();
			}
		}

		
		public string Prefix{
			get{
				return m_strPrefix;
			}
			set{
				m_strPrefix = value;
				this.Invalidate();
			}
		}

		public string Suffix{
			get{
				return m_strSuffix;
			}
			set{
				m_strSuffix = value;
				this.Invalidate();
			}
		}

		public string DisplayFormat{
			get{
				return m_strDisplayFormat;
			}
			set{
				string realFormat = string.Empty;
				try{
					string format = "{0:" + value + "}";
					string test = string.Format(format, 100.00);
					realFormat = value;
				}catch{
					realFormat = "##0";
				}
				m_strDisplayFormat = realFormat;
				this.Invalidate();
			}
		}


		public float Value{
			get{
				return m_iValue;
			}
			set{
				if (value > m_iMaxValue){
					value = m_iMaxValue;
				}else if (value < m_iMinValue){
					value = m_iMinValue;
				}
				m_iValue = value;
				this.Invalidate();
			}
		}

		public Color CompletedColor{
			get{
				return m_clComplete;
			}
			set{
				m_clComplete = value;
				this.Invalidate();
			}
		}

		public Color EmptyColor{
			get{
				return m_clEmpty;
			}
			set{
				m_clEmpty = value;
				this.Invalidate();
			}
		}

		private StringFormat GetStringFormat(){
			StringFormat format = new StringFormat();
			switch (this.TextAlign){
				case ContentAlignment.BottomCenter:
					format.Alignment = StringAlignment.Center;
					format.LineAlignment = StringAlignment.Far;
					break;
				case ContentAlignment.BottomLeft:
					format.Alignment = StringAlignment.Near;
					format.LineAlignment = StringAlignment.Far;
					break;
				case ContentAlignment.BottomRight:
					format.Alignment = StringAlignment.Far;
					format.LineAlignment = StringAlignment.Far;
					break;
				case ContentAlignment.MiddleCenter:
					format.Alignment = StringAlignment.Center;
					format.LineAlignment = StringAlignment.Center;
					break;
				case ContentAlignment.MiddleLeft:
					format.Alignment = StringAlignment.Near;
					format.LineAlignment = StringAlignment.Center;
					break;
				case ContentAlignment.MiddleRight:
					format.Alignment = StringAlignment.Far;
					format.LineAlignment = StringAlignment.Center;
					break;
				case ContentAlignment.TopCenter:
					format.Alignment = StringAlignment.Center;
					format.LineAlignment = StringAlignment.Near;
					break;
				case ContentAlignment.TopLeft:
					format.Alignment = StringAlignment.Near;
					format.LineAlignment = StringAlignment.Near;
					break;
				case ContentAlignment.TopRight:
					format.Alignment = StringAlignment.Far;
					format.LineAlignment = StringAlignment.Near;
					break;
			}
			return format;
		}

		public override string Text {
			get {
				return base.Text;
			}
//			set {
//				base.Text = value;
//			}
		}


		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
			base.OnPaint (e);
			float fPercent = (float)m_iValue * (float)100.0 / ((float)(m_iMaxValue - m_iMinValue));
			if (m_bEnabledGraphics){
				Bitmap bmp = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
				Graphics g = Graphics.FromImage(bmp);
				
				string strPercent = m_strPrefix + string.Format("{0:"+m_strDisplayFormat+"}"+m_strSuffix, fPercent);

				float xOffSet = ((float)m_iValue * (float)this.ClientRectangle.Width) / (float)(m_iMaxValue - m_iMinValue);
				RectangleF rcFill = new RectangleF(this.ClientRectangle.X, this.ClientRectangle.Y, xOffSet, this.ClientRectangle.Height);
				RectangleF rcEmpty = new RectangleF(xOffSet, this.ClientRectangle.Y, this.ClientRectangle.Width - xOffSet, this.ClientRectangle.Height);
				g.FillRectangle(new SolidBrush(m_clComplete), rcFill);
				g.FillRectangle(new SolidBrush(m_clEmpty), rcEmpty);
				g.DrawString(strPercent, this.Font, new SolidBrush(this.ForeColor), this.ClientRectangle, GetStringFormat());

				


				e.Graphics.DrawImage(bmp, this.ClientRectangle);
				bmp.Dispose();
				g.Dispose();

			}else{
				this.Text = m_strPrefix + string.Format("{0:"+m_strDisplayFormat+"}"+m_strSuffix, fPercent);
			}
		}

	}
}
