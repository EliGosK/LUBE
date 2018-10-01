using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EAP.Framework.Windows.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormBase : Form, IDisposable
    {
        #region Variables
        private eScreenMode m_screenMode = eScreenMode.Idle; 
        #endregion

        #region Constructor
        public FormBase()
        {
            InitializeComponent();

            IsFormLoading = true;
        } 
        #endregion

        #region Properties

        /// <summary>
        /// Flag indicate Form is processing event OnLoad
        /// </summary>
        [Browsable(false)]
        public bool IsFormLoading { get; private set; }

        #endregion

        #region Virtual Method Must Implement by inherrited form.
        
        public virtual bool DataLoading(object args) { return true; }
        public virtual void SetScreenMode(eScreenMode screenMode) {
            m_screenMode = screenMode;
        }

        /// <summary>
        /// Get/Set current screen mode.
        /// </summary>
        public eScreenMode ScreenMode
        {
            get { return m_screenMode; }
            set { m_screenMode = value; }
        }

        public virtual void PermissionControl() { }

        
        public virtual void OnCommandClose() { }
        public virtual bool OnCommandAdd() { return true; }
        public virtual bool OnCommandEdit() { return true; }
        public virtual bool OnCommandDelete() { return true; }
        public virtual bool OnCommandSave() { return true; }
        public virtual bool OnCommandCancel() { return true; }
        public virtual bool OnCommandPrint() { return true; }
        public virtual bool OnCommandImport() { return true; }
        public virtual bool OnCommandExport() { return true; }
        public virtual bool OnCommandRefresh() { return true; }
        public virtual bool OnCommandFind() { return true; }
        
        #endregion

        #region Override Member

        protected override void OnLoad(EventArgs e)
        {
            IsFormLoading = true;
            try
            {
                PermissionControl();

                base.OnLoad(e);

            }
            catch (Exception ex)
            {
                MessageDialog.ShowSystemErrorMsg(this, ex);
            }
            finally
            {
                IsFormLoading = false;
            }
        }

        protected override void OnClosed(EventArgs e) {            
            base.OnClosed(e);
        }

        #endregion

        #region Event Handler


        #endregion
    }

}