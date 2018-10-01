using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Data;
using EAP.Framework.Windows.MultiLanguage;


namespace EAP.Framework.Windows
{
    /// <summary>
    /// Summary description for MessageDialog.
    /// </summary>
    public class MessageDialog : Form
    {
        #region Variables

        private Splitter splitter1;
        private PictureBox picIcon;
        private Panel panelButtons;
        private TextBox txtDetail;
        private Panel panelMessage;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;
        private Panel panelDetail;
        private DialogButton m_selectedButton = DialogButton.No;
        private Label lblMessage;
        private int m_iLastPanelDetailHeight = -1;
        private int DEFAULT_MESSAGE_HEIGHT = -1;

        private string str_EmailFrom = string.Empty;
        private string str_EmailTo = string.Empty;
        private string str_SMTPServer = string.Empty;
        private string str_UserDomain = string.Empty;
        private string str_PassDomain = string.Empty;
        private DataGridView dgvDetail;
        private Button button1;
        private bool b_EnableSSL = false;

        #endregion

        [DllImport("winmm.dll", EntryPoint = "PlaySound", CharSet = CharSet.Auto)]
        private static extern int PlaySound(String pszSound, int hmod, int falgs);

        private enum SND
        {
            SND_SYNC = 0x0000,/* play synchronously (default) */
            SND_ASYNC = 0x0001, /* play asynchronously */
            SND_NODEFAULT = 0x0002, /* silence (!default) if sound not found */
            SND_MEMORY = 0x0004, /* pszSound points to a memory file */
            SND_LOOP = 0x0008, /* loop the sound until next sndPlaySound */
            SND_NOSTOP = 0x0010, /* don't stop any currently playing sound */
            SND_NOWAIT = 0x00002000, /* don't wait if the driver is busy */
            SND_ALIAS = 0x00010000,/* name is a registry alias */
            SND_ALIAS_ID = 0x00110000, /* alias is a pre d ID */
            SND_FILENAME = 0x00020000, /* name is file name */
            SND_RESOURCE = 0x00040004, /* name is resource name or atom */
            SND_PURGE = 0x0040,  /* purge non-static events for task */
            SND_APPLICATION = 0x0080 /* look for application specific association */
        }

        #region Constructor

        public MessageDialog(
            string strCaption
            , string strMessage
            , string strDetail
            , MessageBoxIcon icon
            , DialogButton acceptButton
            , DialogButton cancelButton
            , DialogButton DlgButtonDefault
            , params DialogButton[] buttons)
        {

            InitializeComponent();
            DEFAULT_MESSAGE_HEIGHT = panelButtons.Bottom;

            // show text;
            this.Text = strCaption;
            this.lblMessage.Text = strMessage;

            if (string.IsNullOrEmpty(strDetail))
                strDetail = "";

            this.txtDetail.Lines = strDetail.Split('\n');
            // show icon picture
            Icon icoDisplay = null;
            string strSoundFileName = string.Empty;
            switch (icon)
            {
                case MessageBoxIcon.Information:	// MessageBoxIcon.Information = MessageBoxIcon.Asterisk
                    icoDisplay = SystemIcons.Information;
                    strSoundFileName = "SystemAsterisk";
                    break;
                case MessageBoxIcon.Error:	// MessageBoxIcon.Error = MessageBoxIcon.Hand = MessageBoxIcon.Stop
                    icoDisplay = SystemIcons.Error;
                    strSoundFileName = "SystemHand";
                    break;
                case MessageBoxIcon.Warning:	// MessageBoxIcon.Warning = MessageBoxIcon.Exclamation
                    icoDisplay = SystemIcons.Warning;
                    strSoundFileName = "SystemExclamation";
                    break;
                case MessageBoxIcon.Question:
                    icoDisplay = SystemIcons.Question;
                    strSoundFileName = "SystemAsterisk";
                    break;
                case MessageBoxIcon.None:
                    icoDisplay = SystemIcons.Application;
                    break;
            }
            if (icoDisplay != null)
            {
                picIcon.Image = icoDisplay.ToBitmap();
                this.Icon = icoDisplay;
            }
            else
            {
                picIcon.Image = SystemIcons.Application.ToBitmap();
                this.Icon = SystemIcons.Application;
            }
            m_iLastPanelDetailHeight = panelDetail.Height;
            // make new button to be showed on screen
            if (buttons.Length <= 0)
            {	// not have button specify, so use OK button as default.
                buttons = new DialogButton[] { DialogButton.OK };
            }
            CreateButton(buttons, strDetail.Trim().Length > 0, acceptButton, cancelButton);
            ShowDetail(false, false, false);
            //	Set Default SimpleButton
            foreach (Button btn in panelButtons.Controls)
            {
                if (btn.Name == DlgButtonDefault.ToString())
                    btn.Select();
            }
            if (strSoundFileName != string.Empty)
            {
                try
                {
                    PlaySound(strSoundFileName, 0, (int)(SND.SND_ASYNC | SND.SND_ALIAS | SND.SND_NOWAIT));
                }
                catch { }
            }
        }

        public MessageDialog(
            string strCaption
            , string strMessage
            , string strDetail
            , MessageBoxIcon icon
            , DialogButton acceptButton
            , DialogButton cancelButton
            , DialogButton DlgButtonDefault
            , DataTable dtDetail
            , params DialogButton[] buttons)
        {

            InitializeComponent();
            DEFAULT_MESSAGE_HEIGHT = panelButtons.Bottom;

            // show text;
            this.Text = strCaption;
            this.lblMessage.Text = strMessage;
            this.txtDetail.Lines = strDetail.Split('\n');
            // show icon picture
            Icon icoDisplay = null;
            string strSoundFileName = string.Empty;
            switch (icon)
            {
                case MessageBoxIcon.Information:	// MessageBoxIcon.Information = MessageBoxIcon.Asterisk
                    icoDisplay = SystemIcons.Information;
                    strSoundFileName = "SystemAsterisk";
                    break;
                case MessageBoxIcon.Error:	// MessageBoxIcon.Error = MessageBoxIcon.Hand = MessageBoxIcon.Stop
                    icoDisplay = SystemIcons.Error;
                    strSoundFileName = "SystemHand";
                    break;
                case MessageBoxIcon.Warning:	// MessageBoxIcon.Warning = MessageBoxIcon.Exclamation
                    icoDisplay = SystemIcons.Warning;
                    strSoundFileName = "SystemExclamation";
                    break;
                case MessageBoxIcon.Question:
                    icoDisplay = SystemIcons.Question;
                    strSoundFileName = "SystemAsterisk";
                    break;
                case MessageBoxIcon.None:
                    icoDisplay = SystemIcons.Application;
                    break;
            }
            if (icoDisplay != null)
            {
                picIcon.Image = icoDisplay.ToBitmap();
                this.Icon = icoDisplay;
            }
            else
            {
                picIcon.Image = SystemIcons.Application.ToBitmap();
                this.Icon = SystemIcons.Application;
            }
            m_iLastPanelDetailHeight = panelDetail.Height;
            // make new button to be showed on screen
            if (buttons.Length <= 0)
            {	// not have button specify, so use OK button as default.
                buttons = new DialogButton[] { DialogButton.OK };
            }
            CreateButton(buttons, strDetail.Trim().Length > 0, acceptButton, cancelButton);

            this.dgvDetail.DataSource = dtDetail;
            ShowDetail(true, false, true);
            //	Set Default SimpleButton
            foreach (Button btn in panelButtons.Controls)
            {
                if (btn.Name == DlgButtonDefault.ToString())
                    btn.Select();
            }
            if (strSoundFileName != string.Empty)
            {
                try
                {
                    PlaySound(strSoundFileName, 0, (int)(SND.SND_ASYNC | SND.SND_ALIAS | SND.SND_NOWAIT));
                }
                catch { }
            }
        }


        public MessageDialog(
            string strCaption
            , string strMessage
            , string strDetail
            , MessageBoxIcon icon
            , DialogButton acceptButton
            , DialogButton cancelButton
            , string strEMailFrom
            , string strEmailTo
            , string strPasswordDomain
            , string strUserDomain
            , string strSMTPServer
            , bool bEnableSSL
            , params DialogButton[] buttons)
        {

            InitializeComponent();
            str_EmailFrom = strEMailFrom;
            str_EmailTo = strEmailTo;
            str_PassDomain = strPasswordDomain;
            str_UserDomain = strUserDomain;
            str_SMTPServer = strSMTPServer;
            b_EnableSSL = bEnableSSL;
            DEFAULT_MESSAGE_HEIGHT = panelButtons.Bottom;

            // show text;
            this.Text = strCaption;
            this.lblMessage.Text = strMessage;
            this.txtDetail.Lines = strDetail.Split('\n');
            // show icon picture
            Icon icoDisplay = null;
            string strSoundFileName = string.Empty;
            switch (icon)
            {
                case MessageBoxIcon.Information:	// MessageBoxIcon.Information = MessageBoxIcon.Asterisk
                    icoDisplay = SystemIcons.Information;
                    strSoundFileName = "SystemAsterisk";
                    break;
                case MessageBoxIcon.Error:	// MessageBoxIcon.Error = MessageBoxIcon.Hand = MessageBoxIcon.Stop
                    icoDisplay = SystemIcons.Error;
                    strSoundFileName = "SystemHand";
                    break;
                case MessageBoxIcon.Warning:	// MessageBoxIcon.Warning = MessageBoxIcon.Exclamation
                    icoDisplay = SystemIcons.Warning;
                    strSoundFileName = "SystemExclamation";
                    break;
                case MessageBoxIcon.Question:
                    icoDisplay = SystemIcons.Question;
                    strSoundFileName = "SystemAsterisk";
                    break;
                case MessageBoxIcon.None:
                    icoDisplay = SystemIcons.Application;
                    break;
            }
            if (icoDisplay != null)
            {
                picIcon.Image = icoDisplay.ToBitmap();
                this.Icon = icoDisplay;
            }
            else
            {
                picIcon.Image = SystemIcons.Application.ToBitmap();
                this.Icon = SystemIcons.Application;
            }
            m_iLastPanelDetailHeight = panelDetail.Height;
            // make new button to be showed on screen
            if (buttons.Length <= 0)
            {	// not have button specify, so use OK button as default.
                buttons = new DialogButton[] { DialogButton.OK };
            }
            CreateButton(buttons, strDetail.Trim().Length > 0, acceptButton, cancelButton);
            ShowDetail(false, false, false);
            if (strSoundFileName != string.Empty)
            {
                try
                {
                    PlaySound(strSoundFileName, 0, (int)(SND.SND_ASYNC | SND.SND_ALIAS | SND.SND_NOWAIT));
                }
                catch { }
            }
        }
        

        #endregion

        private void ShowDetail(bool bShow, bool bMessageMode, bool bTableMode)
        {
            //ถ้า messagemode กับ table mode เป็น false ก็ถือว่าไม่ต้อง show detail
            if (bMessageMode == false && bTableMode == false)
            {
                bShow = false;
            }

            if (bShow)
            {
                this.Height = 400;//panelMessage.Height + splitter1.Height + SystemInformation.CaptionHeight + SystemInformation.BorderSize.Height * 2 + panelDetail.Height + 2;
                panelMessage.Dock = DockStyle.Top;
                splitter1.Visible = true;
                panelDetail.Visible = true;

                if (this.dgvDetail.DataSource == null)
                {
                    bTableMode = false;
                    dgvDetail.Visible = false;
                }



                //ถ้า มีตัวไหนตัวหนึ่งเป็น true 
                if ((bMessageMode ^ bTableMode))
                {
                    if (bMessageMode)
                    {
                        txtDetail.Dock = DockStyle.Fill;
                    }
                    else if (bTableMode)
                    {
                        dgvDetail.Dock = DockStyle.Fill;
                        dgvDetail.Visible = true;
                    }
                }
                else
                {


                    txtDetail.Dock = DockStyle.Top;
                    dgvDetail.Dock = DockStyle.Bottom;

                }
            }
            else
            {
                splitter1.Visible = false;
                panelDetail.Visible = false;
                this.Height = panelMessage.Height + SystemInformation.CaptionHeight + SystemInformation.BorderSize.Height * 2 + 5;
                panelMessage.Dock = DockStyle.Fill;
            }
            //if (txtDetail.Text.Trim().Length > 0)
            //{
            //    SimpleButton btnDetail = (SimpleButton)panelButtons.Controls[panelButtons.Controls.Count - 1];
            //    btnDetail.Text = bShow ? "Details <<" : "Details >>";
            //    //btnDetail.FlatStyle = FlatStyle.System;
            //}
        }

        private string GetButtonCaption(DialogButton btn)
        {
            switch (btn)
            {
                case DialogButton.Abort: return "Abort";
                case DialogButton.Cancel: return "Cancel";
                case DialogButton.Ignore: return "Ignore";
                case DialogButton.No: return "No";
                case DialogButton.NoToAll: return "No to all";
                case DialogButton.OK: return "OK";
                case DialogButton.Retry: return "Retry";
                case DialogButton.Yes: return "Yes";
                case DialogButton.YesToAll: return "Yes to all";
                case DialogButton.SendMail: return "Send Error";
                case DialogButton.Detail: return "Detail";

                default: return string.Empty;
            }
        }

        private void CreateButton(DialogButton[] buttons, bool bHaveDetail, DialogButton acceptButton, DialogButton cancelButton)
        {
            const int OFFSET = 8;	// Pixel
            panelButtons.Controls.Clear();
            int iTabIndex = 0;
            for (int i = 0; i < buttons.Length; ++i)
            {
                Button btn = new Button();
                //btn.FlatStyle = FlatStyle.System;
                btn.Location = new System.Drawing.Point(OFFSET, OFFSET + (OFFSET + btn.Height) * i);
                btn.Name = buttons[i].ToString();
                btn.TabIndex = iTabIndex++;
                btn.Text = GetButtonCaption(buttons[i]);
                btn.Tag = buttons[i];
                btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                btn.Click += this.Button_Click;

                ////////////////////
                if (buttons[i] == DialogButton.OK)
                {
                    btn.Image = Properties.Resources.OK;
                }
                else if (buttons[i] == DialogButton.Yes)
                {
                    btn.Image = Properties.Resources.Yes;
                }
                else if (buttons[i] == DialogButton.No)
                {
                    btn.Image = Properties.Resources.No;
                }
                ////////////////////
                if (buttons[i] == acceptButton)
                {
                    this.AcceptButton = btn;
                }
                else if (buttons[i] == cancelButton)
                {
                    this.AcceptButton = btn;
                }
                panelButtons.Controls.Add(btn);
            }
            // create more detail
            Button btnDetail = null;
            if (bHaveDetail)
            {
                btnDetail = new Button();
                //btnDetail.FlatStyle = FlatStyle.System;
                btnDetail.Location = new Point(OFFSET, panelButtons.Bottom - btnDetail.Height - OFFSET);// (OFFSET + btnDetail.Height) *buttons.Length);

                btnDetail.Name = "Detail";
                btnDetail.TabIndex = iTabIndex++;
                btnDetail.Text = GetButtonCaption(DialogButton.Detail);
                btnDetail.Image = Properties.Resources.Detail;
                btnDetail.TextImageRelation = TextImageRelation.ImageBeforeText;
                btnDetail.Click += new EventHandler(Button_Click);
                panelButtons.Controls.Add(btnDetail);
            }
            int height = ((Button)panelButtons.Controls[panelButtons.Controls.Count - 1]).Bottom + OFFSET;
            if (height > DEFAULT_MESSAGE_HEIGHT)
            {
                panelMessage.Height = ((Button)panelButtons.Controls[panelButtons.Controls.Count - 1]).Bottom + OFFSET;
            }
            else
            {
                if (btnDetail != null)
                {
                    btnDetail.Top = DEFAULT_MESSAGE_HEIGHT - OFFSET - btnDetail.Height;
                }
            }
            if (btnDetail != null)
            {
                btnDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            }
            txtDetail.Focus();
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Tag != null)
            {
                ButtonResult = (DialogButton)Enum.Parse(typeof(DialogButton), Convert.ToString(b.Tag));
                if (ButtonResult == DialogButton.SendMail)
                {
                    ///////SendError();
                }
                this.Close();
            }
            else
            {	// Detail button
                ShowDetail(!txtDetail.Visible, !txtDetail.Visible, false);
            }
        }

        public DialogButton ButtonResult
        {
            get
            {
                return m_selectedButton;
            }
            private set { m_selectedButton = value; }
        }

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

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageDialog));
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.panelMessage = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.txtDetail = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.panelMessage.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // picIcon
            // 
            this.picIcon.Image = ((System.Drawing.Image)(resources.GetObject("picIcon.Image")));
            this.picIcon.Location = new System.Drawing.Point(8, 8);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(32, 32);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picIcon.TabIndex = 0;
            this.picIcon.TabStop = false;
            // 
            // panelMessage
            // 
            this.panelMessage.Controls.Add(this.lblMessage);
            this.panelMessage.Controls.Add(this.panelButtons);
            this.panelMessage.Controls.Add(this.picIcon);
            this.panelMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMessage.Location = new System.Drawing.Point(0, 0);
            this.panelMessage.Name = "panelMessage";
            this.panelMessage.Size = new System.Drawing.Size(379, 96);
            this.panelMessage.TabIndex = 0;
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblMessage.Location = new System.Drawing.Point(48, 8);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(235, 80);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "Message";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.button1);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(291, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(88, 96);
            this.panelButtons.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button1.Location = new System.Drawing.Point(6, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.Control;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 96);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(379, 5);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panelDetail
            // 
            this.panelDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetail.Controls.Add(this.dgvDetail);
            this.panelDetail.Controls.Add(this.txtDetail);
            this.panelDetail.Location = new System.Drawing.Point(0, 101);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(377, 245);
            this.panelDetail.TabIndex = 2;
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.AllowUserToDeleteRows = false;
            this.dgvDetail.AllowUserToResizeRows = false;
            this.dgvDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Location = new System.Drawing.Point(8, 125);
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.ReadOnly = true;
            this.dgvDetail.RowHeadersVisible = false;
            this.dgvDetail.Size = new System.Drawing.Size(361, 108);
            this.dgvDetail.TabIndex = 1;
            // 
            // txtDetail
            // 
            this.txtDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDetail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtDetail.Location = new System.Drawing.Point(8, 7);
            this.txtDetail.Multiline = true;
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.ReadOnly = true;
            this.txtDetail.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDetail.Size = new System.Drawing.Size(361, 105);
            this.txtDetail.TabIndex = 0;
            // 
            // MessageDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(379, 348);
            this.ControlBox = false;
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panelMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(387, 123);
            this.Name = "MessageDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MessageDialog";
            this.Load += new System.EventHandler(this.MessageDialog_Load);
            this.Shown += new System.EventHandler(this.MessageDialog_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.panelMessage.ResumeLayout(false);
            this.panelMessage.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelDetail.ResumeLayout(false);
            this.panelDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        public static bool ShowInTaskBar = false;

        #region MessageCaptions
        public static string MessageCaptions_Information
        {
            get
            {
                return "Information";
                //return "ข้อมูล";
            }
        }
        public static string MessageCaptions_Confirmation
        {
            get
            {
                return "Confirmation";
                //return "ยืนยัน";
            }
        }
        public static string MessageCaptions_BusinessError
        {
            get
            {
                return "Warning";
                //return "คำเตือน";
            }
        }
        public static string MessageCaptions_SystemError
        {
            get
            {
                return "Error";
                //return "ข้อผิดพลาด";
            }
        }
        #endregion

        #region ShowConfirmationMsg
        public static DialogButton ShowConfirmationMsg(IWin32Window owner, string strMessage)
        {
            return MessageDialog.Show(owner, MessageCaptions_Confirmation, strMessage, string.Empty, MessageBoxIcon.Question);
        }

        public static DialogButton ShowConfirmationMsg(IWin32Window owner, string strMessage, DataTable dtDetail)
        {
            return MessageDialog.Show(owner, MessageCaptions_Confirmation, strMessage, string.Empty, MessageBoxIcon.Question, dtDetail);
        }

        public static DialogButton ShowConfirmationMsg(IWin32Window owner, string strMessage, DialogButton DlgButtonDefault)
        {
            return MessageDialog.Show(owner, MessageCaptions_Confirmation, strMessage, string.Empty, MessageBoxIcon.Question, DlgButtonDefault);
        }
        #endregion

        #region ShowBusinessErrorMsg

        public static DialogButton ShowBusinessErrorMsg(IWin32Window owner, ApplicationException ex)
        {
            if (ex is BusinessException)
                return ShowBusinessErrorMsg(owner, (BusinessException) ex);
            else
            {
                string innerMsg = ex.InnerException == null ? null : ex.InnerException.Message;
                return ShowBusinessErrorMsg(owner, ex.Message, innerMsg);
            }
        }

        public static DialogButton ShowBusinessErrorMsg(IWin32Window owner, BusinessException ex)
        {
            string msgText = MessageManager.GetMessageText(ex.MsgId);            
            if (String.IsNullOrEmpty(msgText))
            {
                msgText = "ไม่พบข้อความรหัส: " + ex.MsgId;
            }
            else
            {
                msgText = string.Format("{0}: {1}", ex.MsgId, msgText);
            }

            return ShowBusinessErrorMsg(owner, msgText);
        }

        public static DialogButton ShowBusinessErrorMsg(IWin32Window owner, string strMessage, string strDetail)
        {            
            return MessageDialog.Show(owner, MessageCaptions_BusinessError, strMessage, strDetail, MessageBoxIcon.Warning);
        }

        public static DialogButton ShowBusinessErrorMsg(IWin32Window owner, string strMessage, string strDetail, MessageBoxIcon msgIcon)
        {
            return MessageDialog.Show(owner, MessageCaptions_BusinessError, strMessage, strDetail, msgIcon);
        }

        public static DialogButton ShowBusinessErrorMsg(IWin32Window owner, string strMessage)
        {
            string strMsg = strMessage;
            string strDetail = string.Empty;
            //if (!string.IsNullOrEmpty(strMessage) && strMessage.Split('\n').Length > 1)
            //{
            //    strMsg = strMessage.Split('\n')[0];
            //    strDetail = strMessage;
            //}
            return MessageDialog.Show(owner, MessageCaptions_BusinessError, strMsg, strDetail, MessageBoxIcon.Warning);
        }

        public static DialogButton ShowBusinessErrorMsg(IWin32Window owner, string strMessage, DataTable dtDetail)
        {
            return MessageDialog.Show(owner, MessageCaptions_BusinessError, strMessage, strMessage, MessageBoxIcon.Warning, dtDetail);
        }

        public static DialogButton ShowBusinessErrorMsg(IWin32Window owner, string strMessage, string strDetail, bool bSendMail, string strEmailFrom, string strEmailTo, string strSmtpServer, string strUserDomain, string strPasswordDomain, bool bEnableSSL)
        {
            DialogButton btnAccept = DialogButton.OK;
            DialogButton btnCancel = DialogButton.SendMail;
            DialogButton[] buttons;
            buttons = new DialogButton[] { DialogButton.OK, DialogButton.SendMail };
            return MessageDialog.Show(owner, MessageCaptions_BusinessError, strMessage, strDetail, MessageBoxIcon.Warning, btnAccept, btnCancel, strEmailFrom, strEmailTo, strPasswordDomain, strUserDomain, strSmtpServer, bEnableSSL, buttons);
        }

        #endregion

        #region ShowSystemErrorMsg
        public static DialogButton ShowSystemErrorMsg(IWin32Window owner, Exception ex)
        {
            // Write Error Log for critical exception.
            //TODO: Write Error Log for critical exception.
            
            return MessageDialog.Show(owner, MessageCaptions_SystemError, ex.Message, ex.ToString(), MessageBoxIcon.Error);
        }

        public static DialogButton ShowSystemErrorMsg(IWin32Window owner, Exception ex, bool bSendMail, string strEmailFrom, string strEmailTo, string strSmtpServer, string strUserDomain, string strPasswordDomain, bool bEnableSSL)
        {
            // Write Error Log for critical exception.
            //TODO: Write Error Log for critical exception.


            DialogButton btnAccept = DialogButton.OK;
            DialogButton btnCancel = DialogButton.SendMail;
            DialogButton[] buttons;
            buttons = new DialogButton[] { DialogButton.OK, DialogButton.SendMail };
            return MessageDialog.Show(owner, MessageCaptions_SystemError, ex.Message, ex.ToString(), MessageBoxIcon.Error, btnAccept, btnCancel, strEmailFrom, strEmailTo, strPasswordDomain, strUserDomain, strSmtpServer, bEnableSSL, buttons);
        }

        #endregion

        #region ShowInformationMsg
        public static void ShowInformationMsg(string text)
        {
            InformationMessage inf = new InformationMessage();
            inf.Title = MessageCaptions_Information;
            inf.Message = text;
            inf.Show();
        }
        #endregion

        #region Static Show method

        public static DialogButton Show(IWin32Window owner, string strCaption, string strMessage, string strDetail, MessageBoxIcon icon)
        {
            DialogButton dlgButtonResult;
            switch (icon)
            {
                case MessageBoxIcon.Information:	// MessageBoxIcon.Information = MessageBoxIcon.Asterisk
                case MessageBoxIcon.Error:	// MessageBoxIcon.Error = MessageBoxIcon.Hand = MessageBoxIcon.Stop
                case MessageBoxIcon.Warning:	// MessageBoxIcon.Warning = MessageBoxIcon.Exclamation
                    dlgButtonResult = DialogButton.OK;
                    break;
                case MessageBoxIcon.Question:
                    dlgButtonResult = DialogButton.Yes;
                    break;
                default:
                    dlgButtonResult = DialogButton.None;
                    break;
            }
            return Show(owner, strCaption, strMessage, strDetail, icon, dlgButtonResult);
        }

        public static DialogButton Show(IWin32Window owner, string strCaption, string strMessage, string strDetail, MessageBoxIcon icon, DataTable dtDetail)
        {
            DialogButton dlgButtonResult;
            switch (icon)
            {
                case MessageBoxIcon.Information:	// MessageBoxIcon.Information = MessageBoxIcon.Asterisk
                case MessageBoxIcon.Error:	// MessageBoxIcon.Error = MessageBoxIcon.Hand = MessageBoxIcon.Stop
                case MessageBoxIcon.Warning:	// MessageBoxIcon.Warning = MessageBoxIcon.Exclamation
                    dlgButtonResult = DialogButton.OK;
                    break;
                case MessageBoxIcon.Question:
                    dlgButtonResult = DialogButton.Yes;
                    break;
                default:
                    dlgButtonResult = DialogButton.None;
                    break;
            }
            return Show(owner, strCaption, strMessage, strDetail, icon, dlgButtonResult, dtDetail);
        }

        public static DialogButton Show(IWin32Window owner, string strCaption, string strMessage, string strDetail, MessageBoxIcon icon, DialogButton DlgButtonDefault)
        {
            DialogButton btnAccept = DialogButton.None;
            DialogButton btnCancel = DialogButton.None;
            DialogButton[] buttons;
            switch (icon)
            {
                case MessageBoxIcon.Information:	// MessageBoxIcon.Information = MessageBoxIcon.Asterisk
                case MessageBoxIcon.Error:	// MessageBoxIcon.Error = MessageBoxIcon.Hand = MessageBoxIcon.Stop
                case MessageBoxIcon.Warning:	// MessageBoxIcon.Warning = MessageBoxIcon.Exclamation
                    btnAccept = DialogButton.OK;
                    btnCancel = DialogButton.None;
                    buttons = new DialogButton[] { DialogButton.OK };
                    break;
                case MessageBoxIcon.Question:
                    btnAccept = DialogButton.Yes;
                    btnCancel = DialogButton.None;
                    buttons = new DialogButton[] { DialogButton.Yes, DialogButton.No };
                    break;
                default:
                    btnAccept = DialogButton.None;
                    btnCancel = DialogButton.None;
                    buttons = new DialogButton[] { DialogButton.None };
                    break;
            }
            return Show(owner, strCaption, strMessage, strDetail, icon, btnAccept, btnCancel, DlgButtonDefault, buttons);
        }

        public static DialogButton Show(IWin32Window owner, string strCaption, string strMessage, string strDetail, MessageBoxIcon icon, DialogButton DlgButtonDefault, DataTable dtDetail)
        {
            DialogButton btnAccept = DialogButton.None;
            DialogButton btnCancel = DialogButton.None;
            DialogButton[] buttons;
            switch (icon)
            {
                case MessageBoxIcon.Information:	// MessageBoxIcon.Information = MessageBoxIcon.Asterisk
                case MessageBoxIcon.Error:	// MessageBoxIcon.Error = MessageBoxIcon.Hand = MessageBoxIcon.Stop
                case MessageBoxIcon.Warning:	// MessageBoxIcon.Warning = MessageBoxIcon.Exclamation
                    btnAccept = DialogButton.OK;
                    btnCancel = DialogButton.None;
                    buttons = new DialogButton[] { DialogButton.OK };
                    break;
                case MessageBoxIcon.Question:
                    btnAccept = DialogButton.Yes;
                    btnCancel = DialogButton.None;
                    buttons = new DialogButton[] { DialogButton.Yes, DialogButton.No };
                    break;
                default:
                    btnAccept = DialogButton.None;
                    btnCancel = DialogButton.None;
                    buttons = new DialogButton[] { DialogButton.None };
                    break;
            }
            return Show(owner, strCaption, strMessage, strDetail, icon, btnAccept, btnCancel, DlgButtonDefault, dtDetail, buttons);
        }

        public static DialogButton Show(
            IWin32Window owner
            , string strCaption
            , string strMessage
            , string strDetail
            , MessageBoxIcon icon
            , DialogButton acceptButton
            , DialogButton cancelButton
            , DialogButton dlgButtonDefault
            , params DialogButton[] buttons)
        {

            MessageDialog dlg = new MessageDialog(strCaption, strMessage, strDetail, icon, acceptButton, cancelButton, dlgButtonDefault, buttons);
            
            if (owner == null)
            {
                dlg.ShowInTaskbar = true;
                dlg.StartPosition = FormStartPosition.CenterScreen;
                dlg.ShowDialog();                
            }
            else
            {
                dlg.ShowInTaskbar = ShowInTaskBar;
                dlg.StartPosition = FormStartPosition.CenterParent;

                Form mainForm = owner as Form;
                if (mainForm != null)
                {
                    // 2016-06-25 Teerayut S. Support ThreadSafe
                    if (mainForm.InvokeRequired)
                    {
                        return (DialogButton) mainForm.Invoke(new Func<DialogButton>(() =>
                        {
                            dlg.ShowDialog(owner);
                            return dlg.ButtonResult;
                        }));
                    }
                    else
                    {
                        dlg.ShowDialog(owner);
                    }
                }
                else
                {
                    dlg.ShowDialog();                
                }

            }
           

            return dlg.ButtonResult;

        }

        public static DialogButton Show(
            IWin32Window owner
            , string strCaption
            , string strMessage
            , string strDetail
            , MessageBoxIcon icon
            , DialogButton acceptButton
            , DialogButton cancelButton
            , DialogButton dlgButtonDefault
            , DataTable dtDetail
            , params DialogButton[] buttons)
        {

            MessageDialog dlg = new MessageDialog(strCaption, strMessage, strDetail, icon, acceptButton, cancelButton, dlgButtonDefault, dtDetail, buttons);
            if (owner == null)
            {
                dlg.ShowInTaskbar = true;
                dlg.ShowDialog();
                dlg.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                dlg.ShowInTaskbar = ShowInTaskBar;
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.ShowDialog(owner);
            }
            return dlg.ButtonResult;

        }


        public static DialogButton Show(
            IWin32Window owner
            , string strCaption
            , string strMessage
            , string strDetail
            , MessageBoxIcon icon
            , DialogButton acceptButton
            , DialogButton cancelButton
            , string strEMailFrom
            , string strEMailTo
            , string strPasswordDomain
            , string strUserDomain
            , string strSMTPServer
            , bool bEnableSSL
            , params DialogButton[] buttons)
        {

            MessageDialog dlg = new MessageDialog(strCaption, strMessage, strDetail, icon, acceptButton, cancelButton, strEMailFrom, strEMailTo, strPasswordDomain, strUserDomain, strSMTPServer, bEnableSSL, buttons);
            if (owner == null)
            {
                dlg.ShowInTaskbar = true;
                dlg.ShowDialog();
                dlg.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                dlg.ShowInTaskbar = ShowInTaskBar;
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.ShowDialog(owner);
            }
            return dlg.ButtonResult;

        }

        #endregion

        #region Form Events

        private void MessageDialog_Load(object sender, EventArgs e)
        {
            if (this.AcceptButton == null)
            {
                this.ActiveControl = txtDetail;
                txtDetail.Focus();
            }
        }

        private void MessageDialog_Shown(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Activate();
        }

        #endregion
    }

    #region DialogButton
    public enum DialogButton
    {
        None,
        OK,
        Yes,
        Cancel,
        No,
        YesToAll,
        NoToAll,
        Abort,
        Retry,
        Ignore,
        SendMail,
        Detail
    }
    #endregion
}
