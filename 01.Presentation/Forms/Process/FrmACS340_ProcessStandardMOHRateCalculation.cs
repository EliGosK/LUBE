using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessService;
using Common;
using DataObject;
using EAP.Framework;
using EAP.Framework.Data;
using EAP.Framework.Windows;

namespace Presentation.Forms.Process
{
    [ScreenPermission(Permission.OpenScreen,"Process")]
    public partial class FrmACS340_ProcessStandardMOHRateCalculation : LUBEFormDev
    {
        #region Enum

        private enum cGridCol
        {
            Month,
            ActualMOH,
            ActualCapacity,
            ActualMOHRate,
        }

        #endregion

        #region Variables

        private ACS340_ProcessBIZ m_bizProcess = new ACS340_ProcessBIZ();
        private bool chkDataExist = false;

        #endregion

        #region Constructor

        public FrmACS340_ProcessStandardMOHRateCalculation()
        {
            InitializeComponent();

            ControlUtil.VisibleControl(false, m_toolBarFind, m_toolBarFind, m_toolBarAdd, m_toolBarEdit, m_toolBarDelete,
                m_toolBarSave, m_toolBarCancel, m_toolBarRefresh,
                m_toolBarPrint, m_toolBarImport, m_toolBarExport);

            UpdateToolbarSeparator();
        }

        #endregion

        #region General Method

        private void InitialScreen()
        {
            grdView.AutoGenerateColumns = false;

            ClearScreen();

            bool bProcess = AppEnvironment.Permission.AllowPermission(AppEnvironment.UserLogin, this.GetType().FullName, "Process");
            if (bProcess)
                btnProcess.Enabled = true;
            else
                btnProcess.Enabled = false;

            txtYear.Text = (DateTime.Now.Year).ToString();

            // Binding Column-Field
            grcMonth.DataPropertyName = "Period";
            grcActualMOH.DataPropertyName = "ActMOH";
            grcActualCapacity.DataPropertyName = "ActCapaUsed";
            grcActualMOHRate.DataPropertyName = "MOHRate";
        }

        private void ClearScreen()
        {
            txtActualMOH.Text = string.Empty;
            txtActualCapacity.Text = string.Empty;
            txtActualMOHRate.Text = string.Empty;

            grdView.DataSource = null;
        }

        #endregion

        #region Validation

        private bool ValidateRequire()
        {
            errorProvider.Clear();

            bool bValid = true;

            if (Util.IsNullOrEmptyOrZero(txtYear.IntValue))
            {
                errorProvider.SetError(txtYear, "Please entry: Year");
                bValid = false;
            }

            if (!bValid)
            {
                MessageDialog.ShowBusinessErrorMsg(this, "Please input criteria");
            }

            return bValid;
        }

        #endregion

        #region Retrieve Data

        private void RetrieveData()
        {
            if (!ValidateRequire())
                return;

            int year = txtYear.IntValue;
            DataTable dtbView = m_bizProcess.RetrieveData(year);
            if (!Util.IsNullOrEmptyOrZero(dtbView.Rows.Count))
            {
                grdView.Columns[3].DefaultCellStyle.Format = "N4";
                grdView.DataSource = dtbView;

                //3. Check data existing
                chkDataExist = true;
            }
            else
            {
                MessageBox.Show("Data not Found.", "ACS340", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Process Data

        private void ProcessData()
        {
            int Year = txtYear.IntValue;
            tb_NewStandardMOH result = m_bizProcess.ProcessData(Year, AppEnvironment.UserLogin);

            if (result == null)
            {
                txtActualMOH.DecimalValue = 0;
                txtActualCapacity.DecimalValue = 0;
                txtActualMOHRate.DecimalValue = 0;
            }
            else
            {
                txtActualMOH.DecimalValue = result.TotalMOH.GetValueOrDefault(0);
                txtActualCapacity.DecimalValue = result.TotalCapacity.GetValueOrDefault(0);
                txtActualMOHRate.DecimalValue = result.MOHRate.GetValueOrDefault(0);
            }

            MessageDialog.ShowInformationMsg("Process Completed.");
        }

        private void TransferData()
        {
            int Year = txtYear.IntValue;

            bool bExistTransfer = m_bizProcess.CheckExistTransfer(Year);
            if (bExistTransfer)
            {
                if (MessageDialog.ShowConfirmationMsg(this, "This Standard MOH of selected year had been transferred, Do you want to continue?") == DialogButton.No)
                {
                    return;
                }
            }

            m_bizProcess.TransferData(Year, AppEnvironment.UserLogin);
            MessageDialog.ShowInformationMsg("Transfer Completed.");

        }
        #endregion

        #region Control Events

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {                
                ClearScreen();
                txtYear.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
        }

        private void btnRetrieveData_Click(object sender, EventArgs e)
        {
            try
            {
                RetrieveData();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                //1. Check mandatory data
                if (!ValidateRequire())
                    return;

                //2. Show confirmation message to process data.
                if (MessageDialog.ShowConfirmationMsg(this, "Do you want to process data?") == DialogButton.Yes)
                {
                    //3. Check data existing
                    if (chkDataExist)
                    {
                        
                        //20180531 May Remove
                        ////4. Check data transferring
                        //int Year = txtYear.IntValue;
                        //bool bExistTransfer = m_bizProcess.CheckExistTransfer(Year);
                        //if (bExistTransfer)
                        //{
                        //    if (MessageDialog.ShowConfirmationMsg(this, "This Standard MOH of selected year had been transferred, Do you want to continue?") == DialogButton.No)
                        //    {
                        //        return;
                        //    }
                        //}

                        ProcessData();
                    }
                    else
                    {
                        MessageBox.Show("There is no data to perform operation." + "\n" + "Please search data first.", "ACS340", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                //5. Check mandatory data
                if (!ValidateRequire())
                    return;

                //6. Show confirmation message to process data.
                if (MessageDialog.ShowConfirmationMsg(this, "Do you want to transfer data ?") == DialogButton.Yes)
                {
                    //7. Check data existing
                    if (chkDataExist)
                    {
                        //4. Check data transferring
                        int Year = txtYear.IntValue;
                        string bExistTransfer = m_bizProcess.CheckExistProcess(Year);
                        if (bExistTransfer == "F")
                        {
                            if (MessageDialog.ShowConfirmationMsg(this, "There is no data to perform operation. Please process data first.") == DialogButton.No)
                            {
                                return;
                            }
                        }

                        TransferData();
                    }
                    else
                    {
                        MessageBox.Show("There is no data to perform operation." + "\n" + "Please search data first.", "ACS340", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {

                ExceptionManager.ManageException(this, ex);
            }
        }
        #endregion

        #region Form Events

        private void FrmACS340_ProcessStandardMOHRateCalculation_Load(object sender, EventArgs e)
        {
            InitialScreen();
        }

        private void FrmACS340_ProcessStandardMOHRateCalculation_Shown(object sender, EventArgs e)
        {
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


    }
}
