/*	Created By		:	Tanin Uaraksakul
 *	Created Date	:	23/09/2003 
 *	Function			:	AFDComboBox
 *	Examples			:	1. เปลี่ยน type ของ ComboBox ที่ประกาศให้มาใช้ที่ TSD.AFD.Windows.Controls.AFDComboBox();
 *								2. แต่ถ้าไม่อยากให้ show error ก็สามารถเช็คจากค่า combobox.SelectedIndex=-1 ถือว่า select ไม่ได้
 *								3. ใน .NET จะมีทั้ง Event Leave, LostFocus โดยลำดับการเกิดจะเป็น GotFocus->Enter->Leave->Validate->LostFocus
 *									ดังนั้นห้ามใช้ event Leave ในการเช็คว่าค่าอยู่ใน combo หรือไม่ เพราะยังไม่ผ่านการ validate แต่ event LostFocus
 *									จะไม่มีให้เห็นในหน้าต่าง properties (Bug) ให้ Add Event อื่นไปก่อน แล้ว rename เป็น combobox.LostFocus
 * Update Date		: 10/10/2003 - เพิ่ม Property IsInList ไว้เช็คว่าค่า Text นั้นมีอยู่ใน list หรือไม่ (บางทีเช็คจาก SelectedIndex อย่างเดียวอาจจะไม่ถูกเสมอไป (เป็น bug ของ combobox)
 */
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace EAP.Framework.Windows.Controls
{
    public class EAPComboBox : System.Windows.Forms.ComboBox
    {
        [DllImport("USER32.DLL", EntryPoint = "SendMessageW", SetLastError = true,
             CharSet = CharSet.Unicode, ExactSpelling = true,
             CallingConvention = CallingConvention.StdCall)]
        //		public static extern long SendMessage(IntPtr hwnd , long wMsg, IntPtr wParam, IntPtr lParam);
        private static extern long SendMessage(IntPtr Handle, Int32 msg, IntPtr wParam, IntPtr lParam);

        private const int WM_KEYDOWN = 0x0100;

        public event System.ComponentModel.CancelEventHandler NotInList;

        private bool _inEditMode = false;
        private bool _autoSearch = true;
        //private bool _autoDroppedDown =false ;
        //private bool _autoExpandDropDown = false;
        private bool _ClearText = true;
        private bool _IsGotFocus = false;//แก้ bug ของ combobox .NET ที่จะเกิด event LostFocus ก่อน Event GotFocus

        //Add By Boonlert F. 23/11/2005
        private bool _isValidating = false;//debug when use event SelectedIndexChanged then text on control will had selection.

        //add by Mr.Fuangwith Sopharath @ 07/06/2006
        /** for disable sendmessage after user press control key **/
        //private bool m_bControlKeyCapture = true;

        public EAPComboBox()
            : base()
        {
            InitializeComponent();
        }

        [DefaultValue(true)]
        public bool AutoSearch//true=มี droppeddown ให้ตอนที่ key
        {
            get { return _autoSearch; }
            set { _autoSearch = value; }
        }

        //		[DefaultValue(false)]
        //		public bool AutoDroppedDown //true=มี droppeddown ให้ตอนที่ key
        //		{
        //			get { return _autoDroppedDown;}
        //			set { _autoDroppedDown = value;}
        //		}

        //		[DefaultValue(false)]
        //		public bool AutoExpandDropDown //true=ยืด width ของ dropped ให้พอกับความยาวของ string ที่ load เข้ามาใน combo
        //		{
        //			get { return _autoExpandDropDown;}
        //			set { _autoExpandDropDown= value;}
        //		}

        public bool IsInList
        {
            get
            {
                //เอา text ไปเช็คหาจากใน list เลย
                if (this.FindStringExact(this.Text) >= 0)
                {
                    this.SelectedIndex = this.FindStringExact(this.Text);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [DefaultValue(true)]
        public bool ClearText
        {
            get { return _ClearText; }
            set { _ClearText = value; }
        }

        protected virtual void OnNotInList(System.ComponentModel.CancelEventArgs e)
        {
            if (_ClearText)
            {
                this.SelectedIndex = -1;
                this.Text = "";
            }

            if (NotInList != null)
            {
                NotInList(this, e);
            }
        }

        /// <summary>
        /// แก้ bug ว่า ถ้า set selectedindex=-1 แล้วค่า text มักจะไม่เปลี่ยนตาม
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if (SelectedIndex == -1 & _inEditMode == false & Text != "")
            {
                this.Text = "";
            }
            base.OnSelectedIndexChanged(e);
        }

        protected override void OnTextChanged(System.EventArgs e)
        {
            if (_autoSearch)
            {
                if (_inEditMode)
                {
                    if (this.Text != "") //ถ้าเป็นค่าว่างไม่ต้อง search
                    {
                        string input = this.Text;
                        int index = FindString(input);

                        if (index >= 0)
                        {
                            _inEditMode = false;
                            this.SelectedIndex = index; //this line will call SelectedIndexChanged
                            _inEditMode = true;
                            //					DroppedDown=_autoDroppedDown;

                            if (!_isValidating) //Add By Boonlert F. 23/11/2005
                                Select(input.Length, this.Text.Length); //สั่ง select ตัวอักษร
                        }
                    }
                    else
                    {
                        _inEditMode = false;
                    }
                }
            }
            base.OnTextChanged(e);
        }

        //		protected override void OnDropDown(System.EventArgs e)
        //		{
        //			if (_autoExpandDropDown & _inEditMode)
        //			{
        ////				bool oldEditMode=_inEditMode;
        ////				_inEditMode=false;
        //				int oldindex=SelectedIndex;
        //				int maxlength=0;
        //				int i=0;
        //				for (i=0;i<Items.Count;i++)
        //				{
        //					if (Items[i].ToString().Length>maxlength)
        //					{
        //						maxlength=Items[i].ToString().Length;
        //					}
        //				}
        //				DropDownWidth=Math.Max(Width,Convert.ToInt32((double)maxlength*7));//ต้องหาว่าความยาวของ string ใน droppeddown เท่าไหร่
        //				SelectedIndex=oldindex;
        ////				_inEditMode=oldEditMode;
        //			}
        //		}

        //		protected override void OnLeave(EventArgs e)
        //		{
        //			base.OnLeave (e);
        //		}

        protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
        {
            _inEditMode = false;
            //comment by Fuangwith Sopharath @ 02/06/2006
            //int pos = this.FindStringExact(this.Text); //เช็คหาค่าที่ตรงกับ text จริงๆ แล้ว return ตำแหน่งที่เจอใน Text ออกมา

            // Fix by KWS 2007/06/15 : เนื่องจากมีปัญหากับข้อมูลที่ไม่ได้มีการ sorting ทำให้ค้นหาข้อมูลผิดพลาด
            int pos = this.FindStringExact(this.Text);
            // ---------------------------------

            if (pos == -1) //ถ้าไม่พบ
            {
                OnNotInList(e); //สั่งให้ call event NotInList	
                //					_IsInList=false;
            }
            else //ถ้าพบ ให้ select ค่านั้น
            {
                this.SelectedIndex = pos;
                //					_IsInList=true;
                _isValidating = true; //Add By Boonlert F. 23/11/2005
            }
            base.OnValidating(e);
        }

        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            // YMS 21/09/2009 Comment e.Control for enable auto complete in Main Report
            //if (e.Control) {
            //RTS 2007/06/21 ----- 
            IntPtr pHandle = Parent.FindForm().Handle;
            //RTS 2007/09/10 ---- remove below sendmessage for progress screen
            //SendMessage(pHandle, WM_KEYDOWN, new IntPtr((int)e.KeyCode), IntPtr.Zero);

            _inEditMode = (e.KeyCode != Keys.Back && e.KeyCode != Keys.Delete && e.KeyCode != Keys.Home && e.KeyCode != Keys.End);

            //Add By Boonlert F. 23/11/2005
            _isValidating = false;
            //MessageBox.Show("ControlKeyCapture == true");
            //}
            base.OnKeyDown(e);

            // add by keng
            if (e.KeyCode == Keys.Enter)
            {
                base.OnKeyPress(new KeyPressEventArgs('\r'));
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            _IsGotFocus = true;
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (_IsGotFocus) //ต้องผ่านgotfocus ก่อน ถึงจะให้ lostfocus ได้
            {
                base.OnLostFocus(e);
                _IsGotFocus = false;
            }
        }

        public void Clear()
        {
            this.SelectedIndex = -1;
            this.Text = "";
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AFDComboBox
            // 
            this.ResumeLayout(false);

        }

        ///// <summary>
        ///// Get or Set AFDComboBox capture when user press Control Key
        ///// </summary>
        ///// <remark> 
        ///// This property enable/disable capture when user press Control Key
        ///// </remark>
        //public bool ControlKeyCapture {
        //    get {
        //        return this.m_bControlKeyCapture;
        //    }
        //    set {
        //        this.m_bControlKeyCapture = value;
        //    }
        //}
    }
}