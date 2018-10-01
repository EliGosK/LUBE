using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
    [ScreenPermission(Permission.OpenScreen, "Process", "ReCalculate")]
    public partial class FrmACS320_ProcessActualCostCalculation : LUBEFormDev
    {
        private class ProcessArgument
        {
            public int Year;
            public int Period;            
            public decimal pActMOH;
            public decimal pActCapaUsed;
            public decimal pSoldLiter;
            public decimal pEndingLiter;
            public decimal pSoldLiterOEM;
            public decimal pEndingLiterOEM;
            public string ByUser;
        }

        #region Variables

        private ACS320_ProcessBIZ m_bizProcess = new ACS320_ProcessBIZ();

        #endregion

        #region Constructor

        public FrmACS320_ProcessActualCostCalculation()
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
            BindPeriodCombobox();

            bool bProcess = AppEnvironment.Permission.AllowPermission(AppEnvironment.UserLogin, this.GetType().FullName, "Process");
            if (bProcess)
                btnProcess.Enabled = true;
            else
                btnProcess.Enabled = false;
        }

        private void BindPeriodCombobox()
        {
            DataTable dtbPeriod = m_bizProcess.GetPeriodCombobox();

            cboMonth.DataSource = dtbPeriod;
            cboMonth.ValueMember = "PeriodID";
            cboMonth.DisplayMember = "PeriodName";
        }

        private void SetControlDefaultValue()
        {
            txtYear.Text = (DateTime.Now.Year).ToString();
            cboMonth.SelectedValue = 1; // January
            progressBar.Value = 0;

            txtActualMOH.Text = string.Empty;
            txtActualCapacity.Text = string.Empty;
            txtActualMOHRate.Text = string.Empty;
            txtSoldLiter.Text = string.Empty;
            txtEndLiter.Text = string.Empty;
            txtSoldLiterOEM.Text = string.Empty;
            txtEndLiterOEM.Text = string.Empty;
            txtSoldLiterSum.Text = string.Empty;
            txtEndLiterSum.Text = string.Empty;

            txtActualMOH.Enabled = false;
            txtActualCapacity.Enabled = false;
            txtSoldLiter.Enabled = false;
            txtEndLiter.Enabled = false;
            txtSoldLiterOEM.Enabled = false;
            txtEndLiterOEM.Enabled = false;

            btnRecal.Enabled = false;
        }

        #region SafeUpdate Control on Screen

        private void SafeUpdateProgressValue(int percent)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() =>
                {
                    SafeUpdateProgressValue(percent);

                }));

                return;
            }

            progressBar.Value = percent;
        }

        private void UpdateProgressValue(ACS320_Process resultDo)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() =>
                {
                    txtActualMOH.DecimalValue = resultDo.ActMOH;
                    txtActualCapacity.DecimalValue = resultDo.ActCapaUsed;
                    txtActualMOHRate.DecimalValue = resultDo.ActCostRate;
                    txtSoldLiter.DecimalValue = resultDo.SoldLiter;
                    txtEndLiter.DecimalValue = resultDo.EndingLiter;
                    txtSoldLiterOEM.DecimalValue = resultDo.SoldLiterOEM;
                    txtEndLiterOEM.DecimalValue = resultDo.EndingLiterOEM;

                    txtActualMOH.Enabled = true;
                    txtActualCapacity.Enabled = true;
                    txtSoldLiter.Enabled = true;
                    txtEndLiter.Enabled = true;
                    txtSoldLiterOEM.Enabled = true;
                    txtEndLiterOEM.Enabled = true;


                    bool bRecal = AppEnvironment.Permission.AllowPermission(AppEnvironment.UserLogin, this.GetType().FullName, "ReCalculate");
                    if (bRecal)
                        btnRecal.Enabled = true;
                    else
                        btnRecal.Enabled = false;

                }));

                return;
            }
        }

        private void UpdateReCalValue(ACS320_Process resultDo)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() =>
                {
                    txtActualMOH.DecimalValue = resultDo.ActMOH;
                    txtActualCapacity.DecimalValue = resultDo.ActCapaUsed;
                    txtActualMOHRate.DecimalValue = resultDo.ActCostRate;
                    txtSoldLiter.DecimalValue = resultDo.SoldLiter;
                    txtEndLiter.DecimalValue = resultDo.EndingLiter;
                    txtSoldLiterOEM.DecimalValue = resultDo.SoldLiterOEM;
                    txtEndLiterOEM.DecimalValue = resultDo.EndingLiterOEM;
                }));

                return;
            }
        }

        private void SafeEnableControl(bool bEnabled, params Control[] controls)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() =>
                {
                    SafeEnableControl(bEnabled, controls);
                }));

                return;
            }

            ControlUtil.EnabledControl(bEnabled, controls);
        }

        #endregion

        #endregion

        #region Background Worker - Process

        private void StartProcess(ProcessArgument arg)
        {
            try
            {
                SafeUpdateProgressValue(0);

                var resultDo = m_bizProcess.CalculateData(arg.Year, arg.Period, arg.ByUser, ConnOnInfoMessage);
                UpdateProgressValue(resultDo);

                //this.Invoke(new Action(() =>
                //{
                //    txtActualMOH.DecimalValue = resultDo.ActMOH;

                //}));
                //txtActualMOH.DecimalValue = resultDo.ActMOH;
                //txtActualCapacity.DecimalValue = resultDo.ActCapaUsed;
                //txtActualMOHRate.DecimalValue = resultDo.ActCostRate;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
        }

        /// <summary>
        /// Method called by SQLServer events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="sqlInfoMessageEventArgs"></param>
        private void ConnOnInfoMessage(object sender, SqlInfoMessageEventArgs sqlInfoMessageEventArgs)
        {
            if (sqlInfoMessageEventArgs.Errors.Count == 0)
                return;

            if (sqlInfoMessageEventArgs.Errors[0].Number == 50000)
            {
                if (sqlInfoMessageEventArgs.Message.StartsWith("PROGRESS:"))
                {
                    string[] tokens = sqlInfoMessageEventArgs.Message.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    int percent = Util.ConvertObjectToInteger(tokens[1]);

                    bgWorkerProcess.ReportProgress(percent);
                }
            }
            else
            {
                MessageDialog.ShowSystemErrorMsg(this, new Exception(sqlInfoMessageEventArgs.Errors[0].Message));
            }
        }

        private void bgWorkerProcess_DoWork(object sender, DoWorkEventArgs e)
        {
            // Disable Screen
            SafeEnableControl(false, txtYear, cboMonth, btnClear, btnProcess);

            // Start process. 
            ProcessArgument arg = (ProcessArgument)e.Argument;
            StartProcess(arg);
        }

        private void bgWorkerProcess_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {            
            SafeUpdateProgressValue(e.ProgressPercentage);
        }

        private void bgWorkerProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Enable screen.
            SafeEnableControl(true, txtYear, cboMonth, btnClear, btnProcess);            
        }

        #endregion

        private bool ValidateRequire()
        {
            errorProvider.Clear();

            bool bValid = true;

            if (Util.IsNullOrEmptyOrZero(txtYear.IntValue))
            {
                errorProvider.SetError(txtYear, "Please input: Year");
                bValid = false;
            }

            if (Util.IsNullOrEmptyOrZero(cboMonth.SelectedValue))
            {
                errorProvider.SetError(cboMonth, "Please input: Month");
                bValid = false;
            }           

            if (!bValid)
            {
                MessageDialog.ShowBusinessErrorMsg(this, "Please input criteria");
            }

            return bValid;

        }

        private bool ValidateReCalRequire()
        {
            errorProvider.Clear();

            bool bValid = true;

            if (Util.IsNullOrEmptyOrZero(txtYear.IntValue))
            {
                errorProvider.SetError(txtYear, "Please input: Year");
                bValid = false;
            }

            if (Util.IsNullOrEmptyOrZero(cboMonth.SelectedValue))
            {
                errorProvider.SetError(cboMonth, "Please input: Month");
                bValid = false;
            }

            if (Util.IsNullOrEmptyOrZero(txtActualMOH.DecimalValue))
            {
                errorProvider.SetError(txtActualMOH, "Please input: Actual MOH");
                bValid = false;
            }

            if (Util.IsNullOrEmptyOrZero(txtActualCapacity.DecimalValue))
            {
                errorProvider.SetError(txtActualCapacity, "Please input: Actual Capacity");
                bValid = false;
            }

            if (Util.IsNullOrEmptyOrZero(txtSoldLiter.DecimalValue))
            {
                errorProvider.SetError(txtSoldLiter, "Please input: Solid Liter LT");
                bValid = false;
            }

            if (Util.IsNullOrEmptyOrZero(txtEndLiter.DecimalValue))
            {
                errorProvider.SetError(txtEndLiter, "Please input: End Liter LT");
                bValid = false;
            }

            if (Util.IsNullOrEmptyOrZero(txtSoldLiterOEM.DecimalValue))
            {
                errorProvider.SetError(txtSoldLiterOEM, "Please input: Solid Liter OEM");
                bValid = false;
            }

            if (Util.IsNullOrEmptyOrZero(txtEndLiterOEM.DecimalValue))
            {
                errorProvider.SetError(txtEndLiterOEM, "Please input: End Liter OEM");
                bValid = false;
            }

            if (!bValid)
            {
                MessageDialog.ShowBusinessErrorMsg(this, "Please input criteria");
            }

            return bValid;

        }

        private void ProcessData()
        {
            //##################################
            // 1. Check mandatory data           
            //   Year, Month, System
            //##################################
            if (!ValidateRequire())
                return;

            int Year = txtYear.IntValue;
            int Period = Util.ConvertObjectToInteger(cboMonth.SelectedValue);

            //##################################
            //# 2. Check data transfer to SAP B1
            //##################################
            bool bExistTransfer = m_bizProcess.CheckExistTransfer(Year, Period);
            if (bExistTransfer)
            {
                MessageDialog.ShowBusinessErrorMsg(this, "This month can't be processed");
                return;
            }

            //##################################
            //# 3. Check retrieve data from SAP B1
            //##################################
            bool bRetrieveData = m_bizProcess.CheckRetrieve(Year, Period);
            if (!bRetrieveData)
            {
                MessageDialog.ShowBusinessErrorMsg(this, "Please Retrieve data before Calculation");
                return;
            }

            //##################################
            //# 4. Check repeat process
            //##################################
            bool bRepeat = m_bizProcess.CheckRepeatProcess(Year, Period);
            if (bRepeat)
            {
                if (MessageDialog.ShowConfirmationMsg(this, "Do you want to do repeat process?") == DialogButton.No)
                {
                    return;
                }
            }

            //##################################
            //# 5. Start Process Calculation (Async)
            //##################################
            AppEnvironment.ShowWaitForm("Please Wait", "Processing");
            ProcessArgument argument = new ProcessArgument();
            argument.Year = Year;
            argument.Period = Period;            
            argument.ByUser = AppEnvironment.UserLogin;
            
            bgWorkerProcess.RunWorkerAsync(argument);

            while (bgWorkerProcess.IsBusy)
            {
                Thread.Sleep(200);
                Application.DoEvents();
            }

            //##################################
            //# 7. Show information message after process complete.
            //##################################
            tb_Transfer transferResult = null;
            if (progressBar.Value == 100)
            {
                AppEnvironment.CloseWaitForm();
                MessageDialog.ShowInformationMsg("Process completed 100%");
                //transferResult = m_bizProcess.GetTransferData(Year, Period);
            }
            else
            {
                AppEnvironment.CloseWaitForm();
                MessageDialog.ShowInformationMsg("Process not completed.");
            }

            //##################################
            //# 8. Display output after process complete.
            //##################################
            //if (transferResult == null)
            //{
            //    txtActualMOH.Text = string.Empty;
            //    txtActualCapacity.Text = string.Empty;
            //    txtActualMOHRate.Text = string.Empty;
            //}
            //else
            //{
            //    txtActualMOH.DecimalValue = transferResult.ActualMOH.GetValueOrDefault(0);
            //    txtActualCapacity.DecimalValue = transferResult.ActualCapacity.GetValueOrDefault(0);
            //    txtActualMOHRate.DecimalValue = transferResult.ActualMOHRate.GetValueOrDefault(0);
            //}
        }

        private void ProcessReCalculate()
        {
            if (!ValidateReCalRequire())
                return;

            //##################################
            //#Check process
            //##################################
            bool bRepeat = m_bizProcess.CheckRepeatProcess(txtYear.IntValue, Util.ConvertObjectToInteger(cboMonth.SelectedValue));
            if (!bRepeat)
            {
                MessageDialog.ShowBusinessErrorMsg(this, "Please Process Data First.");
                return;
               
            }

            if (Util.ConvertObjectToDecimal(txtActualCapacity.DecimalValue) != (Util.ConvertObjectToDecimal(txtSoldLiterSum.DecimalValue) + Util.ConvertObjectToDecimal(txtEndLiterSum.DecimalValue)))
            {
                MessageDialog.ShowBusinessErrorMsg(this, "Total FG Production is not equal to Total of Sold Liter and Ending Liter.");
                return;

            }

            AppEnvironment.ShowWaitForm("Please Wait", "Calculating");
            ProcessArgument arg = new ProcessArgument();
            arg.Year = txtYear.IntValue;
            arg.Period = Util.ConvertObjectToInteger(cboMonth.SelectedValue);
            arg.pActMOH = Util.ConvertObjectToDecimal(txtActualMOH.DecimalValue);
            arg.pActCapaUsed = Util.ConvertObjectToDecimal(txtActualCapacity.DecimalValue);
            arg.pSoldLiter = Util.ConvertObjectToDecimal(txtSoldLiter.DecimalValue);
            arg.pEndingLiter = Util.ConvertObjectToDecimal(txtEndLiter.DecimalValue);
            arg.pSoldLiterOEM = Util.ConvertObjectToDecimal(txtSoldLiterOEM.DecimalValue);
            arg.pEndingLiterOEM = Util.ConvertObjectToDecimal(txtEndLiterOEM.DecimalValue);

            arg.ByUser = AppEnvironment.UserLogin;

            var resultDo = m_bizProcess.ReCalculateData(arg.Year, arg.Period, arg.pActMOH, arg.pActCapaUsed, arg.pSoldLiter, arg.pEndingLiter, arg.pSoldLiterOEM, arg.pEndingLiterOEM, arg.ByUser);
            UpdateReCalValue(resultDo);
            AppEnvironment.CloseWaitForm();
            MessageDialog.ShowInformationMsg("Calculate Completed");
        }

        #region Control Events

        private void btnClear_Click(object sender, EventArgs e)
        {
            SetControlDefaultValue();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageDialog.ShowConfirmationMsg(this, "Do you want to calculate actual cost?") == DialogButton.Yes)
                {
                    ProcessData();
                }
            }
            catch (Exception ex)
            {
                AppEnvironment.CloseWaitForm();
                ExceptionManager.ManageException(this, ex);
            }
        }

        private void btnRecal_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageDialog.ShowConfirmationMsg(this, "Do you want to re-calculate actual cost?") == DialogButton.Yes)
                {
                    ProcessReCalculate();
                }
            }
            catch (Exception ex)
            {
                AppEnvironment.CloseWaitForm();
                ExceptionManager.ManageException(this, ex);
            }
        }

        #endregion

        #region Form Events

        private void FrmACS320_ProcessActualCostCalculation_Load(object sender, EventArgs e)
        {
            InitialScreen();
            SetControlDefaultValue();
        }

        #endregion

        private void txtSoldLiter_Leave(object sender, EventArgs e)
        {
            txtSoldLiterSum.DecimalValue = Util.ConvertObjectToDecimal(txtSoldLiter.DecimalValue) + Util.ConvertObjectToDecimal(txtSoldLiterOEM.DecimalValue);
        }

        private void txtSoldLiterOEM_Leave(object sender, EventArgs e)
        {
            txtSoldLiterSum.DecimalValue = Util.ConvertObjectToDecimal(txtSoldLiter.DecimalValue) + Util.ConvertObjectToDecimal(txtSoldLiterOEM.DecimalValue);
        }

        private void txtSoldLiter_TextChanged(object sender, EventArgs e)
        {
            txtSoldLiterSum.DecimalValue = Util.ConvertObjectToDecimal(txtSoldLiter.DecimalValue) + Util.ConvertObjectToDecimal(txtSoldLiterOEM.DecimalValue);
        }

        private void txtSoldLiterOEM_TextChanged(object sender, EventArgs e)
        {
            txtSoldLiterSum.DecimalValue = Util.ConvertObjectToDecimal(txtSoldLiter.DecimalValue) + Util.ConvertObjectToDecimal(txtSoldLiterOEM.DecimalValue);
        }

        private void txtEndLiter_Leave(object sender, EventArgs e)
        {
            txtEndLiterSum.DecimalValue = Util.ConvertObjectToDecimal(txtEndLiter.DecimalValue) + Util.ConvertObjectToDecimal(txtEndLiterOEM.DecimalValue);
        }

        private void txtEndLiterOEM_Leave(object sender, EventArgs e)
        {
            txtEndLiterSum.DecimalValue = Util.ConvertObjectToDecimal(txtEndLiter.DecimalValue) + Util.ConvertObjectToDecimal(txtEndLiterOEM.DecimalValue);
        }

        private void txtEndLiter_TextChanged(object sender, EventArgs e)
        {
            txtEndLiterSum.DecimalValue = Util.ConvertObjectToDecimal(txtEndLiter.DecimalValue) + Util.ConvertObjectToDecimal(txtEndLiterOEM.DecimalValue);
        }

        private void txtEndLiterOEM_TextChanged(object sender, EventArgs e)
        {
            txtEndLiterSum.DecimalValue = Util.ConvertObjectToDecimal(txtEndLiter.DecimalValue) + Util.ConvertObjectToDecimal(txtEndLiterOEM.DecimalValue);
        }

        
    }
}
