using System;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Drawing;
using System.Runtime.InteropServices;

namespace EAP.Framework.Windows.Controls
{

	#region EAPStatusBarPanelType
	public enum EAPStatusBarPanelType{
		Normal,
		DateTime,
		ProgressBar
	}
	#endregion

	#region EAPStatusBarPanelConverter

	public class EAPStatusBarPanelConverter : ExpandableObjectConverter{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
			if (destinationType == typeof(InstanceDescriptor)){
				return true;
			}
			return base.CanConvertTo (context, destinationType);
		}
		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType) {
			if (destinationType == typeof(InstanceDescriptor)){
				return new InstanceDescriptor(typeof(EAPStatusBarPanel).GetConstructor(new Type[0]), null, false);
			}
			return base.ConvertTo (context, culture, value, destinationType);
		}
	}

	#endregion

	#region EAPStatusBarPanelCollectionEditor
	[ToolboxItemAttribute(false)]
	public class EAPStatusBarPanelCollectionEditor : CollectionEditor{
		private Type []m_types;
		public EAPStatusBarPanelCollectionEditor(Type type) : base(type){
			m_types = new Type[]{typeof(EAPStatusBarPanel), typeof(EAPStatusBarDateTimePanel), typeof(EAPStatusBarProgressPanel), typeof(EAPStatusBarKeyPanel)};
		}
		protected override Type[] CreateNewItemTypes() {
			return m_types;
		}

	}
	#endregion

	#region EAPStatusBar

	public class EAPStatusBar : System.Windows.Forms.StatusBar{
		private Timer m_tm = null;
		public EAPStatusBar() : base(){
			m_tm = new Timer();
			m_tm.Interval = (int)(new TimeSpan(0, 0, 1).TotalMilliseconds);
			m_tm.Enabled = true;
			m_tm.Tick += new EventHandler(m_tm_Tick);
		}


		[Category ("Collections")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(EAPStatusBarPanelCollectionEditor),typeof(System.Drawing.Design.UITypeEditor))]
		public new StatusBar.StatusBarPanelCollection Panels{
			get{
				return base.Panels;
			}
		}


		private void m_tm_Tick(object sender, EventArgs e) {
			if (this.DesignMode == false){
				foreach (StatusBarPanel p in base.Panels){
					if (p is EAPStatusBarDateTimePanel){
						((EAPStatusBarDateTimePanel)p).RefreshText();
					}
				}
			}
		}

		protected override void OnDrawItem(StatusBarDrawItemEventArgs e) {
			if (e.Panel is EAPStatusBarProgressPanel){
				Rectangle rect = new Rectangle(0, 0, e.Bounds.Width-4, e.Bounds.Height-3);
				EAPStatusBarProgressPanel panel = (EAPStatusBarProgressPanel)e.Panel;
				float fPercent = (float)panel.Value * (float)100.0 / ((float)(panel.MaxValue - panel.MinValue));
				Bitmap bmp = new Bitmap(e.Bounds.Width, e.Bounds.Height);
				Graphics g = Graphics.FromImage(bmp);
				
				string strPercent = panel.Prefix + string.Format("{0:"+ panel.DisplayFormat+"}"+panel.Suffix, fPercent);
				
				float xOffSet = ((float)panel.Value * (float)rect.Width) / (float)(panel.MaxValue - panel.MinValue);
				RectangleF rcFill = new RectangleF(rect.X, rect.Y, xOffSet, rect.Height);
				RectangleF rcEmpty = new RectangleF(xOffSet, rect.Y, rect.Width - xOffSet, rect.Height);
				
				g.FillRectangle(new SolidBrush(panel.CompletedColor), rcFill);
				g.FillRectangle(new SolidBrush(panel.EmptyColor), rcEmpty);
				g.DrawLines(new Pen(panel.BorderColor),
					new Point[]{
								   new Point(rect.Left, rect.Top),
								   new Point(rect.Right, rect.Top),
								   new Point(rect.Right, rect.Bottom),
								   new Point(rect.Left, rect.Bottom),
								   new Point(rect.Left, rect.Top)});

				g.DrawString(strPercent, this.Font, new SolidBrush(panel.ForeColor), rect, GetStringFormat(panel.TextAlign));

				e.Graphics.DrawImage(bmp, e.Bounds.Left+1, e.Bounds.Top+1);
				bmp.Dispose();
				g.Dispose();
			}else{
				base.OnDrawItem(e);
			}
		}


		private StringFormat GetStringFormat(ContentAlignment align){
			StringFormat format = new StringFormat();
			switch (align){
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




	}

	#endregion

	#region EAPStatusBarPanel

	[TypeConverter("EAPStatusBarPanelConverter")]
	[ToolboxItemAttribute(false)]
	public class EAPStatusBarPanel : System.Windows.Forms.StatusBarPanel{

		protected EAPStatusBarPanelType m_type = EAPStatusBarPanelType.Normal;

		public EAPStatusBarPanel() : base(){
		}

		public EAPStatusBarPanelType PanelType{
			get{
				return m_type;
			}
		}
	}

	#endregion

	public enum CalendarType{
		Local,
		EnglishUnitedStates
	}


	#region EAPStatusBarDateTimePanel
	[TypeConverter("EAPStatusBarPanelConverter")]
	[ToolboxItemAttribute(false)]
	public class EAPStatusBarDateTimePanel : EAPStatusBarPanel{
		private string m_strDateTimeFormat = "dd/MM/yyyy";
		private CalendarType m_calendarType;
		private DateTimeFormatInfo m_dInfo = null;
		public EAPStatusBarDateTimePanel() : base(){
			m_calendarType = CalendarType.Local;
			m_type = EAPStatusBarPanelType.DateTime;
			base.Style = StatusBarPanelStyle.Text;
		}

		[Browsable(false)]
		public new StatusBarPanelStyle Style{
			get{	return base.Style;	}
		}
		public string DateTimeFormat{
			get{	return m_strDateTimeFormat;}
			set{	
				m_strDateTimeFormat = value;
				base.Text = DateTimeText;
			}
		}
		public CalendarType FormatType{
			get{	return m_calendarType;}
			set{	
				m_calendarType = value;
				if (m_calendarType == CalendarType.EnglishUnitedStates){
					m_dInfo = new CultureInfo("en-US", false).DateTimeFormat;
				}else{
					m_dInfo = null;
				}
				base.Text = DateTimeText;
			}
		}
		private string DateTimeText{
			get{
				if (m_dInfo != null){
					return string.Format(m_dInfo, "{0:" + m_strDateTimeFormat + "}", DateTime.Now);
				}else{
					return string.Format("{0:" + m_strDateTimeFormat + "}", DateTime.Now);
				}
			}
		}
		public void RefreshText(){
			base.Text = DateTimeText;
		}

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string Text{
			get{	return DateTimeText; }
		}

	}
	#endregion

	#region EAPStatusBarProgressPanel
	[TypeConverter("EAPStatusBarPanelConverter")]
	[ToolboxItemAttribute(false)]
	public class EAPStatusBarProgressPanel : EAPStatusBarPanel{

		private int m_iMinValue = 0;
		private int m_iMaxValue = 100;
		private float m_iValue = 0;
		private string m_strDisplayFormat = "##0";
		private string m_strPrefix = string.Empty;
		private string m_strSuffix = "%";
//		private bool m_bEnabledGraphics = true;
		private Color m_clFore = SystemColors.WindowText;
		private Color m_clComplete = Color.Green;
		private Color m_clEmpty = Color.White;
		private Color m_clBoder = Color.Black;
		private ContentAlignment m_textAlignment;

		public EAPStatusBarProgressPanel() : base(){
			m_textAlignment = ContentAlignment.MiddleCenter;
			base.Style = StatusBarPanelStyle.OwnerDraw;
			m_type = EAPStatusBarPanelType.ProgressBar;
		}

		[Browsable(false)]
		public new HorizontalAlignment Alignment{	get{	return base.Alignment;}	set{base.Alignment = value;}}

//		[Browsable(false)]
//		public new StatusBarPanelAutoSize AutoSize {get{return base.AutoSize;} set{base.AutoSize = value;}}

		[Browsable(false)]
		public new Icon Icon {get{return base.Icon;} set{base.Icon = value;}}

		[Browsable(false)]
		public new StatusBarPanelStyle Style {get{return base.Style;} set{base.Style = value;}}


		private void _Invalidate(){
			if (this.DesignMode == false){
				this.Parent.Invalidate();
			}
		}

		public ContentAlignment TextAlign{
			get{	return m_textAlignment;}
			set{	
				m_textAlignment = value;
				_Invalidate();
			}
		}
		public Color ForeColor{
			get{	return m_clFore;}
			set{	
				m_clFore = value;
				_Invalidate();
			}
		}
		public int MinValue{
			get{
				return m_iMinValue;
			}
			set{
				m_iMinValue = value;
				_Invalidate();
			}
		}
		public int MaxValue{
			get{
				return m_iMaxValue;
			}
			set{
				m_iMaxValue = value;
				_Invalidate();
			}
		}
		public string Prefix{
			get{
				return m_strPrefix;
			}
			set{
				m_strPrefix = value;
				_Invalidate();
			}
		}
		public string Suffix{
			get{
				return m_strSuffix;
			}
			set{
				m_strSuffix = value;
				_Invalidate();
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
				_Invalidate();
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
				_Invalidate();
			}
		}
		public Color BorderColor{
			get{	return m_clBoder;}
			set{
				m_clBoder = value;
				_Invalidate();
			}
		}
		public Color CompletedColor{
			get{
				return m_clComplete;
			}
			set{
				m_clComplete = value;
				_Invalidate();
			}
		}

		public Color EmptyColor{
			get{
				return m_clEmpty;
			}
			set{
				m_clEmpty = value;
				_Invalidate();
			}
		}
		[Browsable(false)]
		public new string Text {
			get {
				return base.Text;
			}
			set {
				base.Text = value;
			}
		}
	}
	#endregion

	public enum VirtualKeys{
		CapLock = 0x14, 
		NumLock = 0x90, 
		ScrollLock = 0x91, 
		Insert = 0x2D
	}

	#region EAPStatusBarKeyPanel
	[ToolboxItemAttribute(false)]
	public class EAPStatusBarKeyPanel : EAPStatusBarPanel{

		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true, CallingConvention=CallingConvention.Winapi)] 
		public static extern short GetKeyState(int keyCode);


		private string m_strTextOn = string.Empty;
		private string m_strTextOff = string.Empty;
		private VirtualKeys m_key;
		public EAPStatusBarKeyPanel() : base(){
			Application.Idle += new EventHandler(Application_Idle);
			m_key = VirtualKeys.Insert;
		}

		private void Application_Idle(object sender, EventArgs e) {
			if (this.DesignMode == false){
				RefreshKeyState();
			}
		}

		private void RefreshKeyState(){
			int iKeyCode = (int)m_key;
			bool bOn = (((ushort)GetKeyState(iKeyCode)) & 0xffff) != 0;
			if (bOn){
				this.Text = m_strTextOn;
			}else{
				this.Text = m_strTextOff;
			}		
		}


		public VirtualKeys KeyState{
			get{	return m_key;}
			set{
				m_key = value;
				switch (m_key){
					case VirtualKeys.CapLock:
						m_strTextOn = "CAP";
						m_strTextOff = string.Empty;
						break;
					case VirtualKeys.NumLock:
						m_strTextOn = "NUM";
						m_strTextOff = string.Empty;
						break;
					case VirtualKeys.ScrollLock:
						m_strTextOn = "SCR";
						m_strTextOff = string.Empty;
						break;
					case VirtualKeys.Insert:
						m_strTextOn = "INS";
						m_strTextOff = "OVR";
						break;
				}
				RefreshKeyState();
			}
		}

		public string TextOn{
			get{	return m_strTextOn;}
			set{
				m_strTextOn = value;
				RefreshKeyState();
			}
		}

		public string TextOff{
			get{	return m_strTextOff;}
			set{
				m_strTextOff = value;
				RefreshKeyState();
			}
		}

	}
	#endregion
	
}
