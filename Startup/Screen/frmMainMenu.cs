using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using BusinessService;
using Common;
using EAP.Framework.Windows;
using Presentation.Forms.Admin;
using Presentation.Forms.Report;
using Presentation.Forms.Master;
using Presentation.Forms.Process;

namespace Startup.Screen
{
    public partial class frmMainMenu : Form
    {
        #region Enums

        public enum ExitModes
        {
            Terminate, Logout
        }

        #endregion

        #region Members

        private SecurityBIZ m_bizSecurity = new SecurityBIZ();        

        private ExitModes m_exitMode = ExitModes.Terminate;


        /// <summary>
        /// รายการเมนูที่เป็นหน้าจอ  ใช้สำหรับ Mapping กับ Permission
        /// </summary>
        private Dictionary<ToolStripMenuItem, string> _lstMenuRegister = new Dictionary<ToolStripMenuItem, string>();

        #endregion
        
        #region Constructor

        public frmMainMenu()
        {
            InitializeComponent();

            RegisterMenuItem();            

            
        }

        #endregion

        #region Properties

        /// <summary>
        /// Keep reference to frmMainLogin instance
        /// </summary>
        public frmMainLogin MainLogin { get; set; }


        #endregion

        #region Register Menu - Screen

        /// <summary>
        /// ลงทะเบียน MenuItem ที่ใช้สำหรับเปิดหน้าจอ
        /// </summary>
        private void RegisterMenuItem()
        {
            // กรณีมี MenuItem ใหม่ ที่ต้องการผูกกับ Permission จะต้องรวบรวมไว้ในนี้           

            // Master
            _lstMenuRegister.Add(miACS110, typeof(FrmACS110_CostTypeSetup).FullName);
            _lstMenuRegister.Add(miACS120, typeof(FrmACS120_MappingAccountCodeAndCostType).FullName);
            //_lstMenuRegister.Add(miACS120_addeditCost, typeof(FrmACS120_AddEditCostType).FullName);


            // Security
            _lstMenuRegister.Add(miADM010_UserMaintenance, typeof(FrmADM010_UserMaintenance).FullName);
            _lstMenuRegister.Add(miADM030_UserGroupMaintenance, typeof(FrmADM030_UserGroupMaintenance).FullName);
            _lstMenuRegister.Add(miADM040_SecurityMapping, typeof(FrmADM040_SecurityMapping).FullName);

            // Process
            _lstMenuRegister.Add(miACS310, typeof(FrmACS310_ProcessRetrieveData).FullName);
            _lstMenuRegister.Add(miACS320, typeof(FrmACS320_ProcessActualCostCalculation).FullName);
            _lstMenuRegister.Add(miACS330, typeof(FrmACS330_ProcessTransferData).FullName);
            _lstMenuRegister.Add(miACS340, typeof(FrmACS340_ProcessStandardMOHRateCalculation).FullName);
            _lstMenuRegister.Add(miACS350, typeof(FrmACS350_ProcessInventoryRevolutionImport).FullName);


            // Report
            _lstMenuRegister.Add(miRPT010, typeof(FrmRPT010_ManufacturingOverheadSummaryReport).FullName);
            _lstMenuRegister.Add(miRPT020, typeof(FrmRPT020_CostReport).FullName);
            _lstMenuRegister.Add(miRPT030, typeof(FrmRPT030_CostofGoodsManufacturedReport).FullName);
            _lstMenuRegister.Add(miRPT040, typeof(FrmRPT040_ClosingItemReport).FullName);
            _lstMenuRegister.Add(miRPT050, typeof(FrmRPT050_VarianceReport).FullName);
        }        

        #endregion

        #region General Method

        /// <summary>
        /// เริ่มสร้างรายการเมนูที่มีสิทธิ์
        /// </summary>
        private void InitialMenu()
        {
            List<string> lstScreenClass = m_bizSecurity.LoadAllMenuByUser(AppEnvironment.UserLogin);

            //#########################################
            // Remove menu which don't have Open permission
            //#########################################
            var enumMenuItem = _lstMenuRegister.GetEnumerator();
            while (enumMenuItem.MoveNext())
            {
                // If not found ScreenClass on reistered MenuItem List will remove MenuItem out from screen.
                if (!lstScreenClass.Contains(enumMenuItem.Current.Value))
                {
                    ToolStripMenuItem menuItem = enumMenuItem.Current.Key;
                    menuItem.GetCurrentParent().Items.Remove(menuItem);
                }
                else
                {
                    // Bind event click to MenuItem
                    enumMenuItem.Current.Key.Click += miMenuItem_Click;
                }
                
            }          

            //#########################################
            // Remove seperator menu item
            //#########################################
            // 1. ถ้าพบ Separator เป็นลำดับแรกในกลุ่ม จะลบทันที
            // 2. ถ้าพบ Separator ที่ไม่ใช่ลำดับแรกในกลุ่ม และพบว่าซ้ำกัน จะลบทันที
            // 3. ถ้าพบ Separator เป็นลำดับสุดท้าย จะลบทันที
            for (int i = 0; i < MainMenuStrip.Items.Count; i++)
            {
                ToolStripMenuItem menuGroup = MainMenuStrip.Items[i] as ToolStripMenuItem;
                if (menuGroup == null)
                    continue;

                RemoveSeparatorUnused(menuGroup);
            }

            //#########################################
            // Remove MenuGroup if don't have child.
            //#########################################
            List<ToolStripMenuItem> lstRemoveMenuItems = new List<ToolStripMenuItem>();
            for (int i = 0; i < MainMenuStrip.Items.Count; i++)
            {                
                ToolStripMenuItem menuGroup = MainMenuStrip.Items[i] as ToolStripMenuItem;

                if (menuGroup == mgWindows)
                    continue;


                if (menuGroup == null)
                    continue;
                if (!menuGroup.HasDropDownItems)
                    lstRemoveMenuItems.Add(menuGroup);
            }

            if (lstRemoveMenuItems.Count > 0)
            {
                for (int i = 0; i < lstRemoveMenuItems.Count; i++)
                {
                    MainMenuStrip.Items.Remove(lstRemoveMenuItems[i]);
                }
            }
        }

        /// <summary>
        /// ลบเมนู Separator ที่ไม่ใช้งานออก
        /// </summary>
        /// <param name="menuGroup"></param>
        private void RemoveSeparatorUnused(ToolStripMenuItem menuGroup)
        {            
            if (!menuGroup.HasDropDownItems)
                return;

            List<ToolStripItem> lstMenuItems = new List<ToolStripItem>();

            ToolStripItem lastItem = null;
            for (int i = 0; i < menuGroup.DropDownItems.Count; i++)
            {                
                ToolStripItem item = menuGroup.DropDownItems[i];
                if (!item.Available)
                    continue;                

                if (item is ToolStripMenuItem)
                {
                    lastItem = item;

                    // ถ้ามี Sub Menu จะทำการเข้าไปลบ Separator ด้วย
                    ToolStripMenuItem menuItem = (ToolStripMenuItem)item;
                    if (menuItem.HasDropDownItems)
                        RemoveSeparatorUnused(menuItem);
                }
                else if (item is ToolStripSeparator)
                {
                    if (lastItem == null)
                    {
                        // ถ้าพบเป็นรายการแรก
                        lstMenuItems.Add(item);
                    }
                    else if (lastItem is ToolStripSeparator)
                    {
                        // ถ้าพบในรายการถัดมา แต่พบว่าซ้ำกัน
                        lstMenuItems.Add(item);
                    }
                    else
                    {
                        lastItem = item;
                    }
                }
                else
                {
                    lastItem = item;
                }
            }
                        
            // กรณีที่พบว่าตัวสุดท้ายก็เป็น Separator 
            if (lastItem is ToolStripSeparator)
            {
                lstMenuItems.Add(lastItem);
            }


            //######################
            //# ลบรายการ Menu Separator ที่เป็นเมนูย่อย
            //######################
            if (lstMenuItems.Count > 0)
            {
                for (int i = 0; i < lstMenuItems.Count; i++)
                {
                    menuGroup.DropDownItems.Remove(lstMenuItems[i]);
                }
            }
        }

        public virtual void DoLogout()
        {
            //Check Open Form

            if (this.MdiChildren.Length > 0)
            {
                //Please close all screen before
                MessageDialog.ShowBusinessErrorMsg(this, Util.GetMessageText(eMsgId.COM0033));
            }
            else
            {
                //Confirm: Do you want to logout?
                if (MessageDialog.ShowConfirmationMsg(this, Util.GetMessageText(eMsgId.COM0032)) == DialogButton.Yes)
                {
                    m_exitMode = ExitModes.Logout;                    
                    this.Close();

                    MainLogin.IsLogout = true;
                    MainLogin.Show();
                    MainLogin.PerformLogin();
                }
            }
        }

        private Form GetExistingForm(Type frmType)
        {
            //if (frmType.FullName == null)
            //    return null;

            foreach (Form form in this.MdiChildren)
            {
                if (form.GetType().FullName == frmType.FullName)
                {
                    return form;
                }

                //if (form.Name.Contains(frmType.Name))
                //{
                //    return form;
                //}
            }
            return null;

        }
        private void LoadChildForm(System.Type frmType, params object[] args)
        {
            Form frmChild = null;
            try
            {
                // Get opened form
                frmChild = GetExistingForm(frmType);

                // if not found exist form, so create new.
                if (frmChild == null)
                {
                    frmChild = (Form)Activator.CreateInstance(frmType, args);
                                        
                    frmChild.FormClosed += FrmChild_FormClosed;
                    
                    if (!(frmChild.Tag != null && frmChild.Tag.ToString().ToUpper().Contains("NOMDI")))
                    {
                        frmChild.MdiParent = this;
                    }

                    frmChild.Show();
                    frmChild.Activate();
                    frmChild.BringToFront();

                    //  Add Tab screen.
                    AddTabScreen(frmChild);
                }
                else
                {
                    frmChild.Activate();
                    frmChild.BringToFront();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);                
            }
        }

        private void FrmChild_FormClosed(object sender, FormClosedEventArgs e)
        {

            Form frmChild = sender as Form;
            RemoveTabScreen(frmChild);            
        }
        #endregion

        #region MenuItem - Click 

        private void miMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
                if (menuItem == null)
                    return;

                if (!_lstMenuRegister.ContainsKey(menuItem))
                    return;


                string screenClassName = null;
                if (!_lstMenuRegister.TryGetValue(menuItem, out screenClassName))
                {
                    return;
                }

                Assembly asm = Assembly.LoadFrom(Application.StartupPath + @"\Presentation.dll");
                Type t = asm.GetType(Convert.ToString(screenClassName));
                if (t != null)
                {                    
                    LoadChildForm(t);                 
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void miLogout_Click(object sender, EventArgs e)
        {
            DoLogout();
        }

        #endregion

        #region Form Events

        private void frmMainMenu_Load(object sender, EventArgs e)
        {

        }

        private void frmMainForm_Shown(object sender, EventArgs e)
        {
            // Status Bar
            stsUser.Text = AppEnvironment.UserLogin;
            stsScreenName.Text = @"-";
            stsDatabaseName.Text = AppEnvironment.DatabaseName;
            stsVersion.Text = @"Version: " + Application.ProductVersion;

            menuMain.MdiWindowListItem = mgWindows;
            InitialMenu();
        }

        private void frmMainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool bClosable = true;

            foreach (Form childForm in this.MdiChildren)
            {
                IFormClosable formClosable = childForm as IFormClosable;
                if (formClosable == null)
                    continue;

                
                if (!formClosable.GetAllowClose())
                {
                    bClosable = false;
                    break;
                }
            }

            if (!bClosable)
            {
                e.Cancel = true;
                return;
            }

            //=========================================


            if (m_exitMode != ExitModes.Logout)
            {
                // Request to terminate application.
                if (MessageDialog.ShowConfirmationMsg(this, Util.GetMessageText(eMsgId.COM0038)) == DialogButton.Yes)
                {
                    // Force to exit application.
                    m_exitMode = ExitModes.Terminate;
                    e.Cancel = false;
                }
                else
                {
                    // Cancel exit application.
                    e.Cancel = true;
                }
            }
        }

        private void frmMainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_exitMode == ExitModes.Terminate)
            {
                // Exit MainApp (Process)
                MainLogin.Close();
            }
        }

        private void frmMainMenu_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
            {
                stsScreenName.Text = "";
            }
            else
            {
                stsScreenName.Text = this.ActiveMdiChild.Text;

                // Set active tabScreenList match to active MDI Child.
                SetActiveTabScreenByForm(this.ActiveMdiChild);

            }
        }

        #endregion

        #region Tab Screen List - Controller

        #region Events

        private void tabScreenList_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage selectedPage = tabScreenList.SelectedTab;
            Type typeOfScreen = selectedPage.Tag as Type;
            if (typeOfScreen != null)
            {
                SetActiveScreenByTabPage(typeOfScreen);
            }
        }

        #endregion

        /// <summary>
        /// Bind event to TabControl
        /// </summary>
        private void SubscribeTabScreenList()
        {
            tabScreenList.SelectedIndexChanged += tabScreenList_SelectedIndexChanged;
        }

        /// <summary>
        /// Unbind event from TabControl
        /// </summary>
        private void UnsubscribeTabScreenList()
        {
            tabScreenList.SelectedIndexChanged -= tabScreenList_SelectedIndexChanged;
        }

        /// <summary>
        /// Update visible TabControl
        /// </summary>
        private void UpdateTabScreenListVisible()
        {
            tabScreenList.Visible = (tabScreenList.TabPages.Count != 0);            
        }

        /// <summary>
        /// Get TabPage by specific type of Child Form
        /// </summary>
        /// <param name="typeOfScreen"></param>
        /// <returns></returns>
        private TabPage GetTabScreen(Type typeOfScreen)
        {
            foreach (TabPage tp in tabScreenList.TabPages)
            {
                Type tabScreenType = tp.Tag as Type;
                if (tabScreenType == null)
                    continue;

                if (tabScreenType == typeOfScreen)
                    return tp;
            }

            return null;
        }

        /// <summary>
        /// Set active tabpage by specific child form
        /// No affect TabControl's event
        /// </summary>
        /// <param name="frmChild"></param>
        private void SetActiveTabScreenByForm(Form frmChild)
        {
            UnsubscribeTabScreenList();

            TabPage tpScreen = GetTabScreen(frmChild.GetType());
            if (tpScreen != null)
            {
                tabScreenList.SelectedTab = tpScreen;
            }

            SubscribeTabScreenList();
        }

        /// <summary>
        /// Activate screen by specific tabPage
        /// </summary>
        /// <param name="typeOfScreen"></param>
        private void SetActiveScreenByTabPage(Type typeOfScreen)
        {
            Form frmChild = GetExistingForm(typeOfScreen);
            if (frmChild != null)
            {                
                frmChild.Activate();
                frmChild.BringToFront();                                
            }
        }

        private TabPage AddTabScreen(Form frmChild)
        {
            TabPage tpScreen = GetTabScreen(frmChild.GetType());
            if (tpScreen != null)
                return null;

            TabPage tabPage = new TabPage(frmChild.Text);
            tabPage.Name = "tp" + frmChild.Name;
            tabPage.Tag = frmChild.GetType();

            UnsubscribeTabScreenList();
            {
                tabScreenList.TabPages.Add(tabPage);
                tabScreenList.SelectedTab = tabPage;
            }
            SubscribeTabScreenList();

            UpdateTabScreenListVisible();

            return tabPage;

        }

        private void RemoveTabScreen(Form frmChild)
        {
            try
            {
                TabPage tpScreen = GetTabScreen(frmChild.GetType());
                if (tpScreen == null)
                    return;

                UnsubscribeTabScreenList();
                tabScreenList.TabPages.Remove(tpScreen);
                SubscribeTabScreenList();


                UpdateTabScreenListVisible();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
        }





        #endregion
    }    
}
