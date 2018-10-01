using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace EAP.Framework.Windows.Controls
{

    public class DateTextBoxWithCalendar : System.Windows.Forms.UserControl
    {
        [DllImport("USER32.DLL", EntryPoint = "SendMessageW", SetLastError = true,
             CharSet = CharSet.Unicode, ExactSpelling = true,
             CallingConvention = CallingConvention.StdCall)]
        //		public static extern long SendMessage(IntPtr hwnd , long wMsg, IntPtr wParam, IntPtr lParam);
        private static extern long SendMessage(IntPtr Handle, Int32 msg, IntPtr wParam, IntPtr lParam);

        private const int WM_KEYDOWN = 0x0100;

        // Constant
        private const string YEAR = "yyyy";
        private const string MONTH = "MM";
        private const string DAY = "dd";
        private const string HOUR = "HH";
        private const string MINUTE = "mm";
        private const string SECOND = "ss";
        private const string MILLISECOND = "fff";
        private static Calendar frmCalendar;
        const int SPACE_BEFORE_BUTTON = 2;

        private bool m_bShowButton = true;

        private System.Windows.Forms.Button btnCallCalendar;
        public DateTextBox dtb;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public event System.EventHandler ValueChanged;
        public event System.EventHandler CalendarDateChanged;

        //		public event System.EventHandler DateTextChanged;

        public DateTextBoxWithCalendar()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            dtb.Location = new Point(0, 0);
            btnCallCalendar.Location = new Point(dtb.Width + SPACE_BEFORE_BUTTON, 0);
            this.Width = dtb.Width + SPACE_BEFORE_BUTTON + btnCallCalendar.Width;

            btnCallCalendar.Height = dtb.Height;

            dtb.DateValue = System.DateTime.Now;
            btnCallCalendar.Visible = m_bShowButton;

            //Add By : Mr. Boonlert F. 1/12/2005
            dtb.KeyDown += new KeyEventHandler(this.On_KeyDown);
            dtb.KeyUp += new KeyEventHandler(this.On_KeyUp);
            dtb.KeyPress += new KeyPressEventHandler(this.On_KeyPress);

            RePositionControls();

        }

        //Add By : Mr. Boonlert F. 1/12/2005
        private void On_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            base.OnKeyDown(e);

            // Added by KWS 2008 08 04
            //if (e.KeyData == Keys.Down)   // - button
            //{
            //    this.Value = this.Value.AddDays(-1);
            //}
            //else if(e.KeyData == Keys.Up) // + button
            //{
            //    this.Value = this.Value.AddDays(1);
            //}
            //else if (e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.F)
            //{
            //    btnCallCalendar.PerformClick();
            //}
            // End

            if (!e.Alt && !e.Shift && e.Control && e.KeyCode == Keys.O)
            {
                btnCallCalendar.PerformClick();
            }
        }

        private void On_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }

        private void On_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }

        /// <summary>
        /// For set visible button.
        /// </summary>
        public bool ShowButton
        {
            get { return m_bShowButton; }
            set
            {
                m_bShowButton = value;
                btnCallCalendar.Visible = m_bShowButton;
                if (!m_bShowButton)
                    dtb.Width = this.Width;
                else
                {
                    dtb.Width = this.Width - btnCallCalendar.Width - 1;
                }
            }
        }


        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
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
        private void InitializeComponent()
        {
            this.dtb = new EAP.Framework.Windows.Controls.DateTextBox();
            this.btnCallCalendar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtb
            // 
            this.dtb.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                     | System.Windows.Forms.AnchorStyles.Right)));
            this.dtb.DateValue = null;
            this.dtb.Format = "dd/MM/yyyy";
            this.dtb.Location = new System.Drawing.Point(0, 0);
            this.dtb.Mask = "00/00/0000";
            this.dtb.MaxDateTime = new System.DateTime(9998, 12, 31, 23, 59, 59, 999);
            this.dtb.MinDateTime = new System.DateTime(((long) (0)));
            this.dtb.Name = "dtb";
            this.dtb.Size = new System.Drawing.Size(276, 20);
            this.dtb.TabIndex = 0;
            this.dtb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtb.ValueChanged += new System.EventHandler(this.dtb_ValueChanged);
            // 
            // btnCallCalendar
            // 
            this.btnCallCalendar.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCallCalendar.Location = new System.Drawing.Point(279, 0);
            this.btnCallCalendar.Name = "btnCallCalendar";
            this.btnCallCalendar.Size = new System.Drawing.Size(17, 20);
            this.btnCallCalendar.TabIndex = 1;
            this.btnCallCalendar.TabStop = false;
            this.btnCallCalendar.Click += new System.EventHandler(this.btnCallCalendar_Click);
            // 
            // CSIDateTextBoxWithCalendar
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.btnCallCalendar);
            this.Controls.Add(this.dtb);
            this.Name = "CSIDateTextBoxWithCalendar";
            this.Size = new System.Drawing.Size(296, 22);
            this.Resize += new System.EventHandler(this.DateTextBoxWithCalendar_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion



        private void btnCallCalendar_Click(object sender, System.EventArgs e)
        {
            if (dtb.Text.Trim() == "")
            {
                if (frmCalendar == null) frmCalendar = new Calendar(System.DateTime.Now);
                frmCalendar.Date = System.DateTime.Now;

            }
            else if (dtb.Text.Trim() == "/  /")
            {
                if (frmCalendar == null) frmCalendar = new Calendar(System.DateTime.Now);
                frmCalendar.Date = System.DateTime.Now;

            }
            else
            {
                DateTime dtDate = Convert.ToDateTime(dtb.DateValue);
                if (frmCalendar == null) frmCalendar = new Calendar(dtDate);
                frmCalendar.Date = dtDate;
            }
            frmCalendar.dHandle += new Calendar.MyHandler(this.frmCalendar_handle1);

            Point p;
            int X = 0, Y = 0;
            if (MousePosition.Y + frmCalendar.Size.Height <= Screen.PrimaryScreen.WorkingArea.Height)
                Y = 0 + this.Size.Height;
            else
                Y = 0 - frmCalendar.Size.Height;

            if (MousePosition.X + frmCalendar.Size.Width > Screen.PrimaryScreen.WorkingArea.Width)
            {
                X = 0 - (Screen.PrimaryScreen.WorkingArea.Width - MousePosition.X);

            }

            p = this.PointToScreen(new Point(X, Y));
            frmCalendar.StartPosition = FormStartPosition.Manual;
            frmCalendar.Location = p;
            frmCalendar.IsSelect = false;
            frmCalendar.Show();
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
            {
                // Invokes the delegates. 
                ValueChanged(this, e);
            }
        }

        protected virtual void OnCalendarDateChanged(EventArgs e)
        {
            if (CalendarDateChanged != null)
            {
                // Invokes the delegates. 
                CalendarDateChanged(this, e);
            }
        }


        private void frmCalendar_handle1()
        {
            bool bChange = false;
            if (frmCalendar.IsSelect == true)
            {
                if (this.Text.Trim() == string.Empty)
                {
                    bChange = true;
                }
                else
                {
                    if (this.Value != frmCalendar.GetDate)
                        bChange = true;

                }

                dtb.DateValue = frmCalendar.GetDate;
                // Modified by KWS 2008 10 14
                //dtb.Focus();
                try
                {
                    //Form form = this.FindForm();
                    //form.SelectNextControl(form.ActiveControl, true, true, true, true);
                    this.Parent.SelectNextControl(this, true, true, true, true);
                }
                catch
                {
                    dtb.Focus();
                }
                //

                if (bChange)
                {
                    OnValueChanged(new EventArgs());
                    OnCalendarDateChanged(new EventArgs());
                }
            }
            frmCalendar.dHandle -= new Calendar.MyHandler(this.frmCalendar_handle1);
        }

        /// <summary>
        /// กำหมดขนาดของ control โดยให้ขยายได้เฉพาะทางยาวเท่านั้น
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTextBoxWithCalendar_Resize(object sender, System.EventArgs e)
        {
            this.Height = dtb.Height;
            btnCallCalendar.Height = dtb.Height;

            //			dtb.Width = this.Width - btnCallCalendar.Width - SPACE_BEFORE_BUTTON;
            //			btnCallCalendar.Location = new Point(dtb.Width + SPACE_BEFORE_BUTTON,0);
        }

        private void dtb_ValueChanged(object sender, System.EventArgs e)
        {
            OnValueChanged(new EventArgs());
        }

        //		protected virtual void OnDateTextChanged(EventArgs e)
        //		{
        //			if (DateTextChanged != null) 
        //			{
        //				// Invokes the delegates. 
        //				DateTextChanged(this, e);
        //			}
        //		}

        //		private void dtb_TextChanged(object sender, System.EventArgs e)
        //		{
        //			OnDateTextChanged(new EventArgs());
        //			
        //		}

        public string Format
        {
            get { return this.dtb.Format; }
            set
            {
                string strTempFormat = value;
                if (strTempFormat == YEAR)
                {
                    this.dtb.Format = strTempFormat;
                }
                else if (strTempFormat == YEAR + "/" + MONTH + "/" + DAY + " " + HOUR + ":" + MINUTE)
                {
                    this.dtb.Format = strTempFormat;
                }
                else if (strTempFormat == YEAR + "/" + MONTH + "/" + DAY)
                {
                    this.dtb.Format = strTempFormat;
                }
                else if (strTempFormat == YEAR + "/" + MONTH)
                {
                    this.dtb.Format = strTempFormat;
                }
                else if (strTempFormat == MONTH + "/" + YEAR)
                {
                    this.dtb.Format = strTempFormat;
                }
                else if (strTempFormat == DAY + "/" + MONTH + "/" + YEAR + " " + HOUR + ":" + MINUTE + ":" + SECOND)
                {
                    this.dtb.Format = strTempFormat;
                }
                else if (strTempFormat == DAY + "/" + MONTH + "/" + YEAR + " " + HOUR + ":" + MINUTE + ":" + SECOND + "." + MILLISECOND)
                {
                    this.dtb.Format = strTempFormat;
                }
                else if (strTempFormat == HOUR + ":" + MINUTE)
                {
                    this.dtb.Format = strTempFormat;
                }
                else
                {
                    this.dtb.Format = DAY + "/" + MONTH + "/" + YEAR;
                }
            }
        }


        /// <summary>
        /// สร้างขึ้นมาเพื่อ เป้น property ใช้กำหนดค่า และ Assign ค่าให้กับ control
        /// </summary>
        public DateTime Value
        {
            get
            {
                if (dtb.DateValue == null)
                    return dtb.MinDateTime;
                else
                    return Convert.ToDateTime(this.dtb.DateValue);
            }
            set { this.dtb.DateValue = value; }
        }

        public DateTime MinDateTime
        {
            get { return dtb.MinDateTime; }
            set { dtb.MinDateTime = value; }
        }

        public DateTime MaxDateTime
        {
            get { return dtb.MaxDateTime; }
            set { dtb.MaxDateTime = value; }
        }


        /// <summary>
        ///  สร้างขึ้นมาเพื่อเป็น property ใช้ดูค่าที่ key เข้ามา
        /// </summary>
        public override string Text
        {
            get { return this.dtb.Text; }
            set { this.dtb.Text = value; }
        }


        //RTS 20080513
        //Empty date?
        public bool IsEmpty()
        {
            return dtb.IsEmpty();
        }

        //End RTS 20080513

        /// <summary>
        /// Get/set width of button calendar
        /// </summary>
        public int ButtonWidth
        {
            get { return btnCallCalendar.Width; }
            set
            {
                if (btnCallCalendar.Width == value)
                    return;

                btnCallCalendar.Width = value;

                // Re-position controls
                RePositionControls();
            }
        }

        [Browsable(true)]
        public Image ButtonImage
        {
            get { return btnCallCalendar.Image; }
            set { btnCallCalendar.Image = value; }
        }


        private void RePositionControls()
        {
            // Re-position controls
            int btnWidth = (ShowButton) ? btnCallCalendar.Width : 0;
            dtb.Width = this.Width - btnWidth;
            btnCallCalendar.Left = this.Width - btnWidth;
        }

    }
}
