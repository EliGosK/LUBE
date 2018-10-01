using System;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using System.Threading;
using System.ComponentModel;
using System.Collections;

namespace EAP.Framework.Windows.Forms
{


    public partial class FormDev : FormBase, IDisposable
    {        
        #region Member Declaration                      

        public object LoadingParam = null;

        //public event ToolBarButtonClickEventHandler OnCommandCustom = null;
        #endregion

        #region Constructor
        protected FormDev()
        {
            InitializeComponent();               
        }
        #endregion        

        #region Virtual Method Must Implement by inherrited form.
        public override bool DataLoading(object args)
        {
            return base.DataLoading(args);
        }

        public override void SetScreenMode(eScreenMode eScreenMode)
        {            
            base.SetScreenMode(eScreenMode);

            switch (eScreenMode)
            {
                case eScreenMode.Idle:
                    ControlUtil.EnabledControl(true, m_toolBarClose, m_toolBarAdd, m_toolBarFind);
                    ControlUtil.EnabledControl(false, m_toolBarEdit, m_toolBarDelete, m_toolBarSave, m_toolBarCancel, m_toolBarRefresh, m_toolBarPrint, m_toolBarImport, m_toolBarExport);
                    break;
                case eScreenMode.View:
                    ControlUtil.EnabledControl(true, m_toolBarClose, m_toolBarAdd, m_toolBarEdit, m_toolBarDelete, m_toolBarRefresh, m_toolBarPrint, m_toolBarImport, m_toolBarExport, m_toolBarFind);
                    ControlUtil.EnabledControl(false, m_toolBarSave, m_toolBarCancel);
                    break;
                case eScreenMode.Add:
                    ControlUtil.EnabledControl(false, m_toolBarClose, m_toolBarAdd, m_toolBarEdit, m_toolBarDelete, m_toolBarRefresh, m_toolBarPrint, m_toolBarImport, m_toolBarExport, m_toolBarFind);
                    ControlUtil.EnabledControl(true, m_toolBarSave, m_toolBarCancel);
                    break;
                case eScreenMode.Edit:
                    ControlUtil.EnabledControl(false, m_toolBarClose, m_toolBarAdd, m_toolBarEdit, m_toolBarDelete, m_toolBarRefresh, m_toolBarPrint, m_toolBarImport, m_toolBarExport, m_toolBarFind);
                    ControlUtil.EnabledControl(true, m_toolBarSave, m_toolBarCancel);
                    break;
                case eScreenMode.Custom:
                    break;
            }
        }
        
        public override void OnCommandClose() { this.Close(); }
        public override bool OnCommandAdd() { return true; }
        public override bool OnCommandEdit() { return true; }
        public override bool OnCommandDelete() { return true; }
        public override bool OnCommandSave() { return true; }
        public override bool OnCommandCancel() { return true; }
        public override bool OnCommandPrint() { return true; }
        public override bool OnCommandImport() { return true; }
        public override bool OnCommandExport() { return true; }
        public override bool OnCommandRefresh() { return true; }
        public override bool OnCommandFind() { return true; }
        #endregion

        #region Override Member

        protected override void OnKeyUp(KeyEventArgs e)
        {
            //Raktai add 20070824
            // check short cut for toolbar ใช้ แทน KeyDown เพราะว่า ไม่สามารถจับ Ctrl+E ได้
            if (e.Control && !e.Alt && !e.Shift)
            {
                switch (e.KeyCode)
                {
                    case Keys.E:	// Edit
                        if (m_toolBarEdit.Enabled && m_toolBarEdit.Visible)
                        {
                            OnCommandEdit();
                        }
                        break;
                }
            }
            //Raktai end add
            base.OnKeyUp(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            // check short cut for toolbar 
            if (e.Control && !e.Alt && !e.Shift)
            {
                switch (e.KeyCode)
                {                   
                    case Keys.A:	// Add
                        if (m_toolBarAdd.Enabled && m_toolBarAdd.Visible)
                        {
                            OnCommandAdd();
                        }
                        break;                    
                    case Keys.D:	// Delete
                        if (m_toolBarDelete.Enabled && m_toolBarDelete.Visible)
                        {
                            OnCommandDelete();
                        }
                        break;
                    case Keys.S:	// Save
                        if (m_toolBarSave.Enabled && m_toolBarSave.Visible)
                        {
                            OnCommandSave();
                        }
                        break;

                    case Keys.P:	// Print
                        if (m_toolBarPrint.Enabled && m_toolBarPrint.Visible)
                        {
                            OnCommandPrint();
                        }
                        break;
                    case Keys.B:	// Export
                        if (m_toolBarExport.Enabled && m_toolBarExport.Visible)
                        {
                            OnCommandExport();
                        }
                        break;
                    //Add by Raktai 2007/05/28
                    case Keys.F:	// Find
                        if (m_toolBarFind.Enabled && m_toolBarFind.Visible)
                        {
                            OnCommandFind();
                        }
                        break;                   
                }
            }
            else if (!e.Control && !e.Shift && !e.Alt)
            {
                switch (e.KeyCode)
                {

                    case Keys.F5:	// refresh
                        if (m_toolBarRefresh.Enabled && m_toolBarRefresh.Visible)
                        {
                            OnCommandRefresh();
                        }
                        break;
                    case Keys.Escape:	// cancel
                        if (m_toolBarCancel.Enabled && m_toolBarCancel.Visible)
                        {
                            OnCommandCancel();
                            break;
                        } //exit
                        if (m_toolBarClose.Enabled && m_toolBarClose.Visible)
                        {
                            OnCommandClose();
                            break;
                        }
                        break;
                    //end---------------------------------------------------------------
                }
            }

            base.OnKeyDown(e);
        }

        #endregion

        #region Event Handler
        private void m_toolBarClose_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                OnCommandClose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void m_toolBarFind_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                OnCommandFind();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void m_toolBarAdd_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                OnCommandAdd();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void m_toolBarEdit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                OnCommandEdit();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void m_toolBarDelete_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                OnCommandDelete();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void m_toolBarSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                OnCommandSave();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void m_toolBarCancel_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                OnCommandCancel();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void m_toolBarRefresh_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                OnCommandRefresh();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void m_toolBarPrint_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                OnCommandPrint();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void m_toolBarImport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                OnCommandImport();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void m_toolBarExport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                OnCommandExport();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        /// <summary>
        /// Update display separator on toolbar.
        /// </summary>
        protected void UpdateToolbarSeparator()
        {
            //m_toolBar.Controls

            ToolStripSeparator sepLast = null;
            bool bStateVisible = false;
            for (int i = 0; i < m_toolBar.Items.Count; i++)
            {
                ToolStripSeparator sepCurrent = m_toolBar.Items[i] as ToolStripSeparator;
                if (sepCurrent == null)
                {
                    // Assume button toolstrip
                    if (m_toolBar.Items[i].Visible)
                        bStateVisible = true;
                }
                else
                {
                    if (sepLast != null && !bStateVisible)
                    {
                        sepLast.Visible = false;
                    }

                    if (sepLast != sepCurrent)
                    {
                        sepLast = sepCurrent;
                        bStateVisible = false;
                    }
                }
            }

            if (sepLast != null && !bStateVisible)
            {
                sepLast.Visible = false;
            }
        }               

        private void FormDev_Shown(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            this.WindowState = FormWindowState.Maximized;
        }
    }



}



