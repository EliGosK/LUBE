using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using EAP.Framework;
using EAP.Framework.Windows;
using EAP.Framework.Windows.Forms;

namespace Presentation.Forms
{
    public partial class LUBEFormDev : FormDev, IFormClosable
    {
        #region Constructor

        public LUBEFormDev()
        {
            InitializeComponent();

        }

        #endregion

        #region Properties

       
        #endregion

        #region IFormClosable Implementation

        /// <summary>
        /// Override method นี้เพื่อกำหนด logic สำหรับอนุญาติให้ปิดหน้าจอได้
        /// </summary>
        /// <returns>True: อนุญาติให้ปิดจอ, False: ไม่อนุญาติให้ปิดจอ</returns>
        public virtual bool GetAllowClose()
        {
            // ถ้าอยู่ในโหมด Add, Edit จะไม่ยอมให้ปิดหน้าจอ  ต้องทำการ Save หรือ Cancel เสียก่อน
            if (ScreenMode == eScreenMode.Add || ScreenMode == eScreenMode.Edit)
            {
                return false;
            }

            return true;
        }

        #endregion



        #region Form Events

        private void LUBEFormDev_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            this.Icon = AppEnvironment.AppIcon;
        }

        private void LUBEFormDev_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!GetAllowClose())
                {                    
                    this.Activate();
                    this.BringToFront();

                    MessageDialog.ShowBusinessErrorMsg(this, Util.GetMessageText(eMsgId.COM0004));
                                        
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
        }

        #endregion
    }
}
