using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessService;
using Common;
using EAP.Framework;
using EAP.Framework.Data;
using EAP.Framework.Windows;

namespace Presentation.Forms.Process
{
    [ScreenPermission(Permission.OpenScreen, "Process")]
    public partial class FrmACS310_ProcessRetrieveData : LUBEFormDev
    {
        private class ProcessArgument
        {
            public int Year;
            public int Period;
            public int System;
            public string ByUser;
        }

        #region Variables

        private ACS310_ProcessBIZ m_bizProcess = new ACS310_ProcessBIZ();

        #endregion

        #region Constructor

        public FrmACS310_ProcessRetrieveData()
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
            BindSystemCombobox();

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

        private void BindSystemCombobox()
        {
            DataTable dtbSystem = m_bizProcess.GetSystemCombobox();

            cboSystem.DataSource = dtbSystem;
            cboSystem.ValueMember = "SystemID";
            cboSystem.DisplayMember = "SystemName";
        }

        private void SetControlDefaultValue()
        {
            txtYear.Text = (DateTime.Now.Year).ToString();
            cboMonth.SelectedValue = 1; // January
            cboSystem.SelectedValue = eSystem.All;
                                    
            progressBar.Value = 0;
        }



        private void StartProcess(ProcessArgument arg)
        {
            try
            {
                SafeUpdateProgressValue(0);

                m_bizProcess.RetrieveData(arg.Year, arg.Period, arg.System, arg.ByUser, ConnOnInfoMessage);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
        }
        
        private void SafeUpdateProgressValue(int percent)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action) (() =>
                {
                    SafeUpdateProgressValue(percent);
                    
                }));

                return;
            }

            progressBar.Value = percent;
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

        #region Background Worker Events - Processing

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
                    string[] tokens = sqlInfoMessageEventArgs.Message.Split(new[] {":"}, StringSplitOptions.RemoveEmptyEntries);
                    int percent = Util.ConvertObjectToInteger(tokens[1]);

                    bgWorker.ReportProgress(percent);
                }
            }
            else
            {                
                MessageDialog.ShowSystemErrorMsg(this, new Exception(sqlInfoMessageEventArgs.Errors[0].Message));
            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Disable Screen
            SafeEnableControl(false, txtYear, cboMonth, cboSystem, btnClear, btnProcess);

            // Start process. 
            ProcessArgument arg = (ProcessArgument) e.Argument;
            StartProcess(arg);
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {            
            SafeUpdateProgressValue(e.ProgressPercentage);
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Enable screen.
            SafeEnableControl(true, txtYear, cboMonth, cboSystem, btnClear, btnProcess);

            if (progressBar.Value == 100)
            {
                AppEnvironment.CloseWaitForm();
                MessageDialog.ShowInformationMsg("Process completed 100%");
                getRevision();
            }
            else
            {
                AppEnvironment.CloseWaitForm();
                MessageDialog.ShowInformationMsg("Process not completed.");
            }
        }

        private void getRevision()
        {
            try
            {
                if (txtYear.Text.Length == 4 && !Util.IsNullOrEmptyOrZero(cboSystem.SelectedValue))
                {
                    DataTable dtbRev = m_bizProcess.GetRevision(txtYear.IntValue, Convert.ToInt16(cboMonth.SelectedValue));
                    if (!Util.IsNullOrEmptyOrZero(dtbRev.Rows.Count))
                    {
                        foreach (DataRow row in dtbRev.Rows)
                        {
                            if (!Util.IsNullOrEmptyOrZero(row["Financial"]) || !Util.IsNullOrEmptyOrZero(row["Production"]))
                            {
                                numericTextBox1.IntValue = Convert.ToInt32(row["Financial"]);
                                numericTextBox2.IntValue = Convert.ToInt32(row["Production"]);
                            }
                            else
                            {
                                numericTextBox1.Clear();
                                numericTextBox2.Clear();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
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

            if (Util.IsNullOrEmptyOrZero(cboSystem.SelectedValue))
            {
                errorProvider.SetError(cboSystem, "Please input: System");
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
            int System = Util.ConvertObjectToInteger(cboSystem.SelectedValue);

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
            //# 3. Check Repeat Process
            //##################################
            bool bRepeatProcess = m_bizProcess.CheckRepeatProcess(Year, Period, System);
            if (bRepeatProcess)
            {
                if (MessageDialog.ShowConfirmationMsg(this, "Do you want to do repeat process?") == DialogButton.No)
                {
                    return;
                }
            }

            //##################################
            //# 4. Retrieve Data (Async)
            //##################################
            AppEnvironment.ShowWaitForm("Please Wait", "Processing");
            ProcessArgument argument = new ProcessArgument();
            argument.Year = Year;
            argument.Period = Period;
            argument.System = System;
            argument.ByUser = AppEnvironment.UserLogin;

            bgWorker.RunWorkerAsync(argument);                          
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
                if (MessageDialog.ShowConfirmationMsg(this, "Do you want to pocess retrieve data?") == DialogButton.Yes)
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

        #endregion

        #region Form Events

        private void FrmACS310_ProcessRetrieveData_Load(object sender, EventArgs e)
        {
            InitialScreen();
            SetControlDefaultValue();
            getRevision();
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            getRevision();
        }

        private void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            getRevision();
        }

        #endregion


    }
}
