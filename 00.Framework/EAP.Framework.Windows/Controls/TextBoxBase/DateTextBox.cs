using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;

namespace EAP.Framework.Windows.Controls
{
	/// <summary>
	/// Summary description for DateTextBox.
	/// </summary>
	public class DateTextBox : System.Windows.Forms.MaskedTextBox {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		// Constant
		private const int WM_KEYDOWN = 0x100;
		private const int WM_CHAR = 0x102;
		private const int WM_KEYUP = 0x101;
		private const string YEAR = "yyyy";
		private const string MONTH = "MM";
		private const string DAY = "dd";
		private const string HOUR = "HH";
		private const string MINUTE = "mm";
		private const string SECOND = "ss";
		private const string MILLISECOND = "fff";
		

		// Option
        private DateTime m_dtMinDateTime = DateTime.MinValue;//new DateTime(1753, 1, 1, 0, 0, 0, 0);
		private DateTime m_dtMaxDateTime = DateTime.MaxValue;//new DateTime(9998, 12, 31, 23, 59, 59, 999);
		//		private bool m_IsNull = true;
		private bool m_bAccess = true;

		// Property
		private object m_Value = DateTime.Today;
		private object oldValue = DateTime.Today;
		private string m_strFormat = "dd/MM/yyyy";

		public event System.EventHandler ValueChanged;
        
        //

		//		public bool IsNull {
		//			get {
		//				return this.m_IsNull;
		//			}
		//			set {
		//				this.m_IsNull = value;
		//			}
		//		}

		public override string Text {
			get {
				if (m_bAccess) {
					return base.Text;
				} else {
					return base.Text;
				}
			}
			set {
				object objTemp = null;
				string strTemp = value;

				base.Text = strTemp;
				CheckTextFormat(ref strTemp, ref objTemp);
				this.m_Value = objTemp;
			}
		}
		private object GetDateValue() {

			if (m_bAccess) {
				// Check Null Value
				m_bAccess = false;


				//					if (this.Text.Length != this.m_strFormat.Length) {
				//						this.m_Value = null;
				//						return;
				//					}
				//					object objTemp = this.m_Value;
				//					m_bAccess = true;
				//					string strTemp = this.Text;
				//					if (this.m_IsNull && objTemp == null) {
				//						//this.Text = string.Empty;
				//						return objTemp;
				//					} else if (!this.m_IsNull && objTemp == null) {
				//						objTemp = m_dtMinDateTime;
				//						CheckDateFormat(ref strTemp, ref objTemp);
				//						//this.Text = strTemp;
				//						return objTemp;
				//					}

				object objTemp = null;
				string strTemp = this.Text; 

				CheckTextFormat(ref strTemp, ref objTemp);
				//check != null to allow blank text if ,not check program will not allow blank
				if (objTemp != null) {
					// Check Range Value
					if (Convert.ToDateTime(objTemp) < m_dtMinDateTime || Convert.ToDateTime(objTemp) > m_dtMaxDateTime) {
						objTemp = m_dtMinDateTime;
						CheckDateFormat(ref strTemp, ref objTemp);
						this.Text = strTemp;
					}
				}
				return objTemp;

			} else {
				return this.m_Value;
			}


		}
		public object DateValue {
			get {
				object dtValue = GetDateValue();
				if (dtValue != null)
					if (Convert.ToDateTime(dtValue) < m_dtMinDateTime || Convert.ToDateTime(dtValue) > m_dtMaxDateTime) 
						return null;
					else
						return dtValue;
				else
					return null;
			}
			set {
				object objTemp = value;
				string strTemp = string.Empty;
				this.m_Value = objTemp;
				CheckDateFormat(ref strTemp, ref objTemp);
				this.Text = strTemp;
			}
		}

		public DateTime MinDateTime {
			get {
				return this.m_dtMinDateTime;
			}
			set {
				try {
					DateTime dtTemp = Convert.ToDateTime(value);
					if (dtTemp > this.m_dtMaxDateTime) {
						this.m_dtMinDateTime = this.m_dtMaxDateTime;
					} else {
						this.m_dtMinDateTime = dtTemp;
					}
				} catch (Exception) {
					//this.m_dtMinDateTime = new DateTime(1753, 1, 1, 0, 0, 0, 0);
                    this.m_dtMinDateTime = DateTime.MinValue;
				}
			}
		}

		public DateTime MaxDateTime {
			get {
				return this.m_dtMaxDateTime;
			}
			set {
				try {
					DateTime dtTemp = Convert.ToDateTime(value);
					if (dtTemp < this.m_dtMinDateTime) {
						this.m_dtMaxDateTime = this.m_dtMinDateTime;
					} else {
						this.m_dtMaxDateTime = dtTemp;
					}
				} catch (Exception) {
					//this.m_dtMaxDateTime = new DateTime(9998, 12, 31, 23, 59, 59, 999);
                    this.m_dtMaxDateTime = DateTime.MaxValue;
				}
			}
		}

		public string Format {
			get {
				return this.m_strFormat;
			}
			set {
				string strTempFormat = value;
				if (strTempFormat == YEAR) {
					this.m_strFormat = strTempFormat;
                    this.Mask = "0000";
                }
                else if (strTempFormat == YEAR + "/" + MONTH + "/" + DAY + " " + HOUR + ":" + MINUTE)
                {
                    this.m_strFormat = strTempFormat;
                    this.Mask = "0000/00/00 00:00";
                }
                else if (strTempFormat == YEAR + "/" + MONTH + "/" + DAY)
                {
                    this.m_strFormat = strTempFormat;
                    this.Mask = "0000/00/00";
                }
                else if (strTempFormat == YEAR + "/" + MONTH)
                {
                    this.m_strFormat = strTempFormat;
                    this.Mask = "0000/00";
                }
                else if (strTempFormat == MONTH + "/" + YEAR)
                {
                    this.m_strFormat = strTempFormat;
                    this.Mask = "00/0000";
                }
                else if (strTempFormat == DAY + "/" + MONTH + "/" + YEAR + " " + HOUR + ":" + MINUTE + ":" + SECOND)
                {
                    this.m_strFormat = strTempFormat;
                    this.Mask = "";
                }
                else if (strTempFormat == DAY + "/" + MONTH + "/" + YEAR + " " + HOUR + ":" + MINUTE + ":" + SECOND + "." + MILLISECOND)
                {
                    this.m_strFormat = strTempFormat;
                    this.Mask = "";
                }
                else if (strTempFormat == HOUR + ":" + MINUTE)
                {
                    this.m_strFormat = strTempFormat;
                    this.Mask = "00:00";
                }
                else
                {
                    this.m_strFormat = DAY + "/" + MONTH + "/" + YEAR;
                    this.Mask = "00/00/0000";
                }
				this.DateValue = this.m_Value;
			}
		}

		public DateTextBox() {
			///
			/// Required for Windows.Forms Class Composition Designer support
			///
			InitializeComponent();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
            this.TextAlign = HorizontalAlignment.Center;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			components = new System.ComponentModel.Container();
		}
		#endregion
		// ใช้แปลง text ให้เป็น string and datetime ที่ผ่าน by ref มา
		private void CheckTextFormat(ref string strText, ref object dtDateTime) {
			try {
				m_bAccess = false;
				int iYear = 1;
				int iMonth = 1;
				int iDay = 1;
				int iHour = 0;
				int iMinute = 0;
				int iSecond = 0;
				int iMilliSecond = 0;
				string strTempText = string.Empty;
				string [] strFormat = this.m_strFormat.Split('/', ' ', ':','.','-');// YMS Add '-' 28/09/2009
                string[] strData = strText.Split('/', ' ', ':', '.', '-');// YMS Add '-' 28/09/2009
				if (strFormat.GetUpperBound(0) == strData.GetUpperBound(0)) {
					for (int iIndex = strFormat.GetUpperBound(0);iIndex > -1;iIndex--) {
						if (Convert.ToString(strFormat[iIndex]) == YEAR) {
							iYear = Convert.ToInt32(strData[iIndex]);
						} else if (Convert.ToString(strFormat[iIndex]) == MONTH) {
							iMonth = Convert.ToInt32(strData[iIndex]);
						} else if (Convert.ToString(strFormat[iIndex]) == DAY) {
							iDay = Convert.ToInt32(strData[iIndex]);
						} else if (Convert.ToString(strFormat[iIndex]) == HOUR) {
							iHour = Convert.ToInt32(strData[iIndex]);
						} else if (Convert.ToString(strFormat[iIndex]) == MINUTE) {
							iMinute = Convert.ToInt32(strData[iIndex]);
						} else if (Convert.ToString(strFormat[iIndex]) == SECOND) {
							iSecond = Convert.ToInt32(strData[iIndex]);
						} else if (Convert.ToString(strFormat[iIndex]) == MILLISECOND) {
							iMilliSecond = Convert.ToInt32(strData[iIndex]);
						}
					}
				} else {
					strText = string.Empty;
					dtDateTime = null;
					return;
				}

				if (iYear < m_dtMinDateTime.Year)
					iYear = m_dtMinDateTime.Year;

				dtDateTime = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond, iMilliSecond);
				strTempText = Convert.ToString(iDay).PadLeft(2, '0')
					+ "/" + Convert.ToString(iMonth).PadLeft(2, '0')
					+ "/" + Convert.ToString(iYear).PadLeft(4, '0')
					+ " " + Convert.ToString(iHour).PadLeft(2, '0')
					+ ":" + Convert.ToString(iMinute).PadLeft(2, '0')
					+ ":" + Convert.ToString(iSecond).PadLeft(2, '0')
					+ "." + Convert.ToString(iMilliSecond).PadLeft(3, '0');

                //KMS add 20071202 to prevent year minimum
                if ((DateTime)dtDateTime < m_dtMinDateTime || (DateTime)dtDateTime > m_dtMaxDateTime) {
                    throw new Exception("Invalid date range");
                }
                //end -----------------------------------

				if (this.m_strFormat == YEAR) {
					strText = strTempText.Substring(6, this.m_strFormat.Length);
                }
                else if (this.m_strFormat == YEAR + "/" + MONTH + "/" + DAY){
                    strText = Convert.ToString(iYear).PadLeft(4, '0')
                    + "/" + Convert.ToString(iMonth).PadLeft(2, '0')
                    + "/" + Convert.ToString(iDay).PadLeft(2, '0');
                }
                else if (this.m_strFormat == YEAR + "/" + MONTH + "/" + DAY + " " + HOUR + ":" + MINUTE)
                {
                    strText = Convert.ToString(iYear).PadLeft(4, '0')
                    + "/" + Convert.ToString(iMonth).PadLeft(2, '0')
                    + "/" + Convert.ToString(iDay).PadLeft(2, '0')
                    + " " + strTempText.Substring(11, 5);
                }
                else if (this.m_strFormat == YEAR + "/" + MONTH)
                {
                    strText = Convert.ToString(iYear).PadLeft(4, '0')
                    + "/" + Convert.ToString(iMonth).PadLeft(2, '0');
                }
                else if (this.m_strFormat == MONTH + "/" + YEAR)
                {
					strText = strTempText.Substring(3, this.m_strFormat.Length);
				} else if (this.m_strFormat == HOUR + ":" + MINUTE){
					strText = strTempText.Substring(11, this.m_strFormat.Length);
				} else{
					strText = strTempText.Substring(0, this.m_strFormat.Length);
				}

			} catch (Exception) {
				strText = string.Empty;
				dtDateTime = null;
			} finally {
				m_bAccess = true;
			}
		}
		// ใช้แปลง object date ให้เป็น Text ตาม format
		private void CheckDateFormat(ref string strText, ref object dtDateTime) {
			try {
				m_bAccess = false;
				DateTime dtDataInput;
				string strTempInput;

				dtDataInput = (DateTime)dtDateTime;
				strTempInput = Convert.ToString(dtDataInput.Day).PadLeft(2, '0')
					+ "/" + Convert.ToString(dtDataInput.Month).PadLeft(2, '0')
					+ "/" + Convert.ToString(dtDataInput.Year).PadLeft(4, '0')
					+ " " + Convert.ToString(dtDataInput.Hour).PadLeft(2, '0')
					+ ":" + Convert.ToString(dtDataInput.Minute).PadLeft(2, '0')
					+ ":" + Convert.ToString(dtDataInput.Second).PadLeft(2, '0')
					+ "." + Convert.ToString(dtDataInput.Millisecond).PadLeft(3, '0');
				if (this.m_strFormat == YEAR) {
					strText = strTempInput.Substring(6, this.m_strFormat.Length);
                }
                else if (this.m_strFormat == YEAR + "/" + MONTH + "/" + DAY){
                    strText = Convert.ToString(dtDataInput.Year).PadLeft(4, '0')
                    + "/" + Convert.ToString(dtDataInput.Month).PadLeft(2, '0')
                    + "/" + Convert.ToString(dtDataInput.Day).PadLeft(2, '0');
                }
                else if (this.m_strFormat == YEAR + "/" + MONTH + "/" + DAY + " " + HOUR + ":" + MINUTE)
                {
                    strText = Convert.ToString(dtDataInput.Year).PadLeft(4, '0')
                    + "/" + Convert.ToString(dtDataInput.Month).PadLeft(2, '0')
                    + "/" + Convert.ToString(dtDataInput.Day).PadLeft(2, '0')
                    + " " + Convert.ToString(dtDataInput.Hour).PadLeft(2, '0')
                    + ":" + Convert.ToString(dtDataInput.Minute).PadLeft(2, '0');
                }
                else if (this.m_strFormat == YEAR + "/" + MONTH)
                {
                    strText = Convert.ToString(dtDataInput.Year).PadLeft(4, '0')
                    + "/" + Convert.ToString(dtDataInput.Month).PadLeft(2, '0');
                }
                else if (this.m_strFormat == MONTH + "/" + YEAR)
                {
					strText = strTempInput.Substring(3, this.m_strFormat.Length);
				} 
				else if (this.m_strFormat == HOUR + ":" + MINUTE) {
					strText = strTempInput.Substring(11, this.m_strFormat.Length);
				} 
				else {
					strText = strTempInput.Substring(0, this.m_strFormat.Length);
				}
				dtDateTime = dtDataInput;
			} catch (Exception) {
				strText = string.Empty;
				dtDateTime = null;
			} finally {
				m_bAccess = true;
			}
		}

		protected override void OnLostFocus(EventArgs e) {
			object otemp = null;
			string strtemp = this.Text;

			//Trace.WriteLine("1 this.Text : " + this.Text);

			CheckTextFormat(ref strtemp, ref otemp);
			
			//Trace.WriteLine("2 strtemp : " + (strtemp == null ? " null " : strtemp) + " otemp : " + (otemp == null ? " Null"  : otemp.ToString()));


			if (otemp == null  ){
				//Trace.WriteLine("3  otemp == null ");
				this.Text = "";
				this.m_Value = null;
			}
			else if ( Convert.ToDateTime(otemp) < m_dtMinDateTime ){
				//Trace.WriteLine("4  Convert.ToDateTime(otemp) < m_dtMinDateTime ");
				otemp = m_dtMinDateTime;
				CheckDateFormat(ref strtemp, ref otemp);
			}


			this.Text = strtemp;
			this.m_Value = otemp;
			base.OnLeave (e);
		}

		protected override void OnGotFocus(EventArgs e) {
			base.OnGotFocus (e);
			if (this.Text.Length != 0) {
				this.SelectionStart = 0;
			}
		}

		protected override void OnTextChanged(EventArgs e) {
			if (this.Text.Length == m_strFormat.Length || this.Text == string.Empty) {
				//this.Text = this.Text;
				base.OnTextChanged (e);
			}
		}

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            
            // KWS 2008 12 : Add Copy & Paste event
            if (e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.C)
            {
                //DateTextBox tb = (DateTextBox)sender;
                //string text = tb.Text;
                //Clipboard.SetText(text);
                this.Copy();
                e.Handled = true;
                e.SuppressKeyPress = true;
                //CheckValueChanged();
            }
            else if (e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.V)
            {
                //DateTextBox tb = (DateTextBox)sender;
                //string textToPaste = Clipboard.GetText();
                this.Paste();
                e.Handled = true;
                e.SuppressKeyPress = true;
                //CheckValueChanged();
            }
            // End
        }

		protected virtual void OnValueChanged(EventArgs e) {
			if (ValueChanged != null) {
				// Invokes the delegates. 
				ValueChanged(this, e);
			}
		}

		private void CheckValueChanged(){

			// เช็คว่ามีการ change ของ datevalue หรือไม่
			object objCurrent = this.DateValue;
			if (objCurrent == null ) objCurrent = string.Empty;

			if (oldValue.ToString().Trim() == string.Empty) {
				if (objCurrent.ToString().Trim() != string.Empty)
					OnValueChanged(new EventArgs());
			}
			else {
				if (objCurrent.ToString() != string.Empty) {
					if (Convert.ToDateTime(objCurrent) != Convert.ToDateTime(oldValue))
						if (Convert.ToDateTime(objCurrent) >= m_dtMinDateTime && Convert.ToDateTime(objCurrent) <= m_dtMaxDateTime )
							OnValueChanged(new EventArgs());
				}
				else {
					OnValueChanged(new EventArgs());
				}
			}

			//OnValueChanged(new EventArgs());

		}

		protected override void WndProc(ref Message m) {
			base.WndProc (ref m);

			if (m.Msg == WM_KEYDOWN){
				if( (int)m.WParam == (int)Keys.Delete || (int)m.WParam == (int)Keys.F9 ){
					CheckValueChanged();
				}
			}

			if (m.Msg == WM_CHAR){
				
				CheckValueChanged();

				object dtValue = GetDateValue();
				if (dtValue != null)
					if (Convert.ToDateTime(dtValue) < m_dtMinDateTime || Convert.ToDateTime(dtValue) > m_dtMaxDateTime) {
						m_Value = null;
					}
					else
						m_Value = dtValue;
				else{
					m_Value = null;
				}


			} //	if (m.Msg == WM_CHAR){

		}

		public override bool PreProcessMessage(ref System.Windows.Forms.Message msg) {
			try {
				oldValue = this.DateValue;
				if (oldValue == null) oldValue = string.Empty;

				m_bAccess = false;
				//Trace.WriteLine(msg.ToString());

				if (msg.Msg == WM_CHAR) {

					int iChar = (int)msg.WParam;
					this.SelectionLength = 0;

					if (iChar == (int)Keys.Enter || iChar == (int)Keys.Back || iChar == (int)Keys.Delete ) {
						// Skip
					} else if (iChar >= (int)'0' && iChar <= (int)'9') {

						if (this.m_strFormat.Length > this.SelectionStart) {
							string strSeparator = this.m_strFormat.Substring(this.SelectionStart, 1);

							if (strSeparator == "/" || strSeparator == " " || strSeparator == ":" || strSeparator == "."){

								if (this.SelectionStart < this.Text.Length) {
									int itemp = this.SelectionStart;
									string strStart = this.Text.Substring(0, this.SelectionStart);
									string strEnd = this.Text.Substring(this.SelectionStart + 1, this.Text.Length - this.SelectionStart - 1);
									this.Text = strStart + strSeparator + strEnd;
									this.SelectionStart = this.SelectionStart == itemp ? this.SelectionStart : itemp;
									this.Select(this.SelectionStart, 1);
									//Trace.WriteLine("aa " + strStart + " : " + strEnd + " + " + this.SelectionStart.ToString());
									this.SelectionStart++;
								} else {
									//Trace.WriteLine("bb ");
									this.Text = this.Text + strSeparator;
									this.SelectionStart = this.Text.Length;
								}


							} else {
								//Trace.WriteLine("cc ");
								this.Select(this.SelectionStart, 1);
							}
						} else {
							msg.WParam = IntPtr.Zero;
						}


					} else {
						msg.WParam = IntPtr.Zero;
					}


				}	//if (msg.Msg == WM_CHAR)


			} catch (Exception Err) {
				Trace.WriteLine(Err.ToString());
			} finally {
				m_bAccess = true;
			}
			return base.PreProcessMessage (ref msg);

		} //public override bool PreProcessMessage(ref System.Windows.Forms.Message msg) 

        //RTS 20080513
        //Empty date?
        public bool IsEmpty()
        {
            return this.Text.Replace('/',' ').Trim().Equals("");
        }

        //End RTS 20080513

	}	// class
}
