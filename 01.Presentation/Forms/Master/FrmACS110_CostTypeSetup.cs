#region Using namespace
using BusinessService;
using Common;
using DataObject;
using EAP.Framework;
using EAP.Framework.Data;
using EAP.Framework.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Presentation.Forms.Master
{
    [ScreenPermission(Permission.OpenScreen, Permission.Add, Permission.Edit, Permission.Delete)]
    public partial class FrmACS110_CostTypeSetup : LUBEFormDev
    {
        //Search
        string searchCostType;
        string searchDesc;
        string searchCostGrp;
        bool isSearch = false;

        //Add
        string costTypeCost;
        string costDesc;
        string costGrp;
        int processID;
        bool status;

        bool isAddMode = false;
        DataTable dtbProcess = new DataTable();
        DataTable dtbCostGroup = new DataTable();

        private ACS110_MasterBIZ m_bizMaster = new ACS110_MasterBIZ();

        public FrmACS110_CostTypeSetup()
        {
            InitializeComponent();
            InitialScreen();
        }

        private void InitialScreen()
        {
            EnableControl(eScreenMode.View);
            ControlUtil.VisibleControl(false, m_toolBarFind, m_toolBarPrint, m_toolBarExport, m_toolBarImport, m_toolBarSave, m_toolBarCancel);
            ControlUtil.EnabledControl(false, m_toolBarEdit, m_toolBarDelete);
            UpdateToolbarSeparator();

            dgvCostType.AutoGenerateColumns = false;
            BindSearchCostGroupCombobox();
        }

        #region Bind Data

        private void BindCostGroupCombobox()
        {
            if (cmbAddCostGroup.DataSource == null)
            {
                dtbCostGroup.Columns.Add("CostGrpValue", typeof(string));
                dtbCostGroup.Columns.Add("CostGrpDisplay", typeof(string));

                dtbCostGroup.Rows.Add("LB", "Fixed Cost – LB");
                dtbCostGroup.Rows.Add("OH", "Fixed Cost – OH");

                cmbAddCostGroup.DataSource = dtbCostGroup;
                cmbAddCostGroup.ValueMember = "CostGrpValue";
                cmbAddCostGroup.DisplayMember = "CostGrpDisplay";
            }
            if (isAddMode)
                cmbAddCostGroup.SelectedIndex = 0;
        }

        private void BindSearchCostGroupCombobox()
        {
            DataTable dtbSearchCostGroup = new DataTable();
            dtbSearchCostGroup.Columns.Add("CostGrpValue", typeof(string));
            dtbSearchCostGroup.Columns.Add("CostGrpDisplay", typeof(string));

            dtbSearchCostGroup.Rows.Add("", "All");
            dtbSearchCostGroup.Rows.Add("LB", "Fixed Cost – LB");
            dtbSearchCostGroup.Rows.Add("OH", "Fixed Cost – OH");

            cmbSearchCostGroup.DataSource = dtbSearchCostGroup;
            cmbSearchCostGroup.ValueMember = "CostGrpValue";
            cmbSearchCostGroup.DisplayMember = "CostGrpDisplay";
        }

        private void BindProcessCombobox()
        {
            if (cmbAddProcess.DataSource == null)
            {
                dtbProcess = m_bizMaster.GetItemGroupCombobox();

                cmbAddProcess.DataSource = dtbProcess;
                cmbAddProcess.ValueMember = "ItmsGrpCod";
                cmbAddProcess.DisplayMember = "ItmsGrpNam";
            }
            if (isAddMode)
                cmbAddProcess.SelectedIndex = 0;
        }

        private void BindDataForEdit()
        {
            int indexRow = dgvCostType.CurrentCell.RowIndex;
            foreach (DataGridViewRow row in dgvCostType.SelectedRows)
            {
                txtAddCostType.Text = row.Cells[0].Value.ToString();
                txtAddDescription.Text = row.Cells[1].Value.ToString();

                //Bind Cost Group
                BindCostGroupCombobox();
                int index = 0;
                for (int i = 0; i < dtbCostGroup.Rows.Count; i++)
                {
                    if (row.Cells[2].Value.ToString() == dtbCostGroup.Rows[i]["CostGrpDisplay"].ToString())
                    {
                        index = i;
                        break;
                    }
                }
                cmbAddCostGroup.SelectedIndex = index;

                //Bind Process
                BindProcessCombobox();
                index = 0; //reset index
                for (int i = 0; i < dtbProcess.Rows.Count; i++)
                {
                    if(row.Cells[3].Value.ToString() == dtbProcess.Rows[i]["ItmsGrpNam"].ToString())
                    {
                        index = i;
                        break;
                    }
                }
                cmbAddProcess.SelectedIndex = index;

                if (row.Cells[4].Value.ToString() == "Y")
                    radioBtnType1.Checked = true;
                else
                    radioBtnType2.Checked = true;
            }

        }

        #endregion

        #region Control

        private void EnableControl(eScreenMode eScreenMode)
        {
            switch (eScreenMode)
            {
                case eScreenMode.View:
                    txtAddCostType.Enabled = false;
                    txtAddDescription.Enabled = false;
                    cmbAddCostGroup.Enabled = false;
                    cmbAddProcess.Enabled = false;
                    radioBtnType1.Enabled = false;
                    radioBtnType2.Enabled = false;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnSearch.Enabled = true;
                    btnClear.Enabled = true;
                    txtSearchCostType.Enabled = true;
                    txtSearchDescription.Enabled = true;
                    cmbSearchCostGroup.Enabled = true;

                    break;
                case eScreenMode.Add:
                    ControlUtil.EnabledControl(false, m_toolBarAdd, m_toolBarRefresh);
                    txtAddCostType.Enabled = true;
                    txtAddDescription.Enabled = true;
                    cmbAddCostGroup.Enabled = true;
                    cmbAddProcess.Enabled = true;
                    radioBtnType1.Enabled = true;
                    radioBtnType2.Enabled = true;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;

                    txtSearchCostType.Enabled = false;
                    txtSearchDescription.Enabled = false;
                    cmbSearchCostGroup.Enabled = false;
                    btnSearch.Enabled = false;
                    btnClear.Enabled = false;

                    BindCostGroupCombobox();
                    BindProcessCombobox();
                    radioBtnType1.Checked = true;
                    break;
                case eScreenMode.Edit:
                    txtSearchCostType.Enabled = false;
                    txtSearchDescription.Enabled = false;
                    cmbSearchCostGroup.Enabled = false;
                    btnSearch.Enabled = false;
                    btnClear.Enabled = false;

                    txtAddCostType.Enabled = false;
                    txtAddDescription.Enabled = true;
                    cmbAddCostGroup.Enabled = true;
                    cmbAddProcess.Enabled = true;
                    radioBtnType1.Enabled = true;
                    radioBtnType2.Enabled = true;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    break;
            }
        }

        private void clearControl()
        {
            txtAddCostType.Clear();
            txtAddDescription.Clear();
            cmbAddCostGroup.SelectedIndex = -1;
            cmbAddProcess.SelectedIndex = -1;
            radioBtnType1.Checked = false;
            radioBtnType2.Checked = false;
        }

        private void ClearScreen()
        {
            txtSearchCostType.Text = string.Empty;
            txtSearchDescription.Text = string.Empty;
            cmbSearchCostGroup.SelectedIndex = 0;

            dgvCostType.DataSource = null;
            isSearch = false;
            ControlUtil.EnabledControl(false, m_toolBarEdit, m_toolBarDelete);
        }

        private void Cancel()
        {
            EnableControl(eScreenMode.View);
            clearControl();
            errorProvider.Clear();
            txtSearchCostType.Focus();

            isAddMode = false;
            ControlUtil.EnabledControl(true, m_toolBarAdd, m_toolBarRefresh);

            if (isSearch)
                ControlUtil.EnabledControl(true, m_toolBarEdit, m_toolBarDelete);
        }

        #endregion

        #region Validate

        private void BindSearchData()
        {
            if (Util.IsNullOrEmpty(txtSearchCostType.Text))
                searchCostType = null;
            else
                searchCostType = txtSearchCostType.Text;

            if (Util.IsNullOrEmpty(txtSearchDescription.Text))
                searchDesc = null;
            else
                searchDesc = txtSearchDescription.Text;

            if (cmbSearchCostGroup.Text == "All") //meaning select ALL
                searchCostGrp = null;
            else
                searchCostGrp = cmbSearchCostGroup.SelectedValue.ToString();
        }

        private bool ValidateSaveRequire()
        {
            errorProvider.Clear();

            bool bValid = true;

            if (Util.IsNullOrEmpty(txtAddCostType.Text))
            {
                errorProvider.SetError(txtAddCostType, "Please input: Cost Type Code");
                bValid = false;
            }
            else
                costTypeCost = txtAddCostType.Text;

            if (Util.IsNullOrEmpty(txtAddDescription.Text))
            {
                errorProvider.SetError(txtAddDescription, "Please input: Description");
                bValid = false;
            }
            else
                costDesc = txtAddDescription.Text;

            if (Util.IsNullOrEmpty(cmbAddCostGroup.SelectedValue))
            {
                errorProvider.SetError(cmbAddCostGroup, "Please input: Cost Group");
                bValid = false;
            }
            else
                costGrp = cmbAddCostGroup.SelectedValue.ToString();

            if (Util.IsNullOrEmptyOrZero(cmbAddProcess.SelectedValue))
            {
                errorProvider.SetError(cmbAddProcess, "Please input: Process");
                bValid = false;
            }
            else
                processID = Convert.ToInt16(cmbAddProcess.SelectedValue);

            if (radioBtnType1.Checked)
                status = true;
            else
                status = false;

            return bValid;

        }

        #endregion

        #region Process

        private void RetrieveData()
        {
            try
            {
                BindSearchData();

                DataTable dtbView = m_bizMaster.RetrieveData(searchCostType, searchDesc, searchCostGrp);
                if (!Util.IsNullOrEmptyOrZero(dtbView.Rows.Count))
                {
                    dgvCostType.DataSource = dtbView;
                    ControlUtil.EnabledControl(true, m_toolBarEdit, m_toolBarDelete);
                    isSearch = true; //flag refresh grid
                }
                else
                {
                    dgvCostType.DataSource = null;
                    ControlUtil.EnabledControl(false, m_toolBarEdit, m_toolBarDelete);
                    //MessageBox.Show("Data not Found.", "ACS110", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
        }

        private void InsertData()
        {
            try
            {
                if (!ValidateSaveRequire())
                    return;

                if (MessageDialog.ShowConfirmationMsg(this, "Do you want to save data?") == DialogButton.Yes)
                {
                    string isUse = m_bizMaster.CheckExistCostType(costTypeCost);
                    if(isUse == "F")
                    {
                        m_bizMaster.InsertCostType(costTypeCost, costDesc, costGrp, processID, status, AppEnvironment.UserLogin);
                        MessageDialog.ShowInformationMsg("Save completed");
                        Cancel();
                    }
                    else
                    {
                        MessageBox.Show("This Code is already in system.", "ACS110", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       // MessageDialog.ShowInformationMsg("This Code is already in system.");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
        }

        private void UpdateData()
        {
            try
            {
                if (!ValidateSaveRequire())
                    return;

                if (MessageDialog.ShowConfirmationMsg(this, "Do you want to save data?") == DialogButton.Yes)
                {
                    string isUse = m_bizMaster.CheckUseCostType(costTypeCost);
                    if (isUse == "F")
                    {
                        m_bizMaster.UpdateCostType(costTypeCost, costDesc, costGrp, processID, status, AppEnvironment.UserLogin);
                        MessageDialog.ShowInformationMsg("Save completed");
                        Cancel();
                    }
                    else
                    {
                        MessageBox.Show("Not allowed to edit Cost type code which already mapped to any account code.", "ACS110", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //MessageDialog.ShowInformationMsg("Not allowed to edit Cost type code which already mapped to any account code.");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
        }

        private void DeleteData()
        {
            try
            {
                foreach (DataGridViewRow row in dgvCostType.SelectedRows)
                {
                    costTypeCost = row.Cells[0].Value.ToString();
                }
                    
                string isUse = m_bizMaster.CheckUseCostType(costTypeCost);
                if (isUse == "F")
                {
                    if (MessageDialog.ShowConfirmationMsg(this, "Do you want to delete data?") == DialogButton.Yes)
                    {
                        m_bizMaster.DeleteCostType(costTypeCost);
                        MessageDialog.ShowInformationMsg("Delete completed");
                    }  
                }
                else
                {
                    MessageDialog.ShowInformationMsg("Not allowed to delete cost type which already mapped to any account code.");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
        }

        #endregion

        #region Event

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isAddMode)
                InsertData();
            else
                UpdateData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RetrieveData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearScreen();
        }

        public override bool OnCommandAdd()
        {
            isAddMode = true;
            EnableControl(eScreenMode.Add);
            txtAddCostType.Select();
            return true;
        }

        public override bool OnCommandEdit()
        {
            EnableControl(eScreenMode.Edit);
            ControlUtil.EnabledControl(false, m_toolBarAdd, m_toolBarDelete);
            BindDataForEdit();
            return true;
        }

        public override bool OnCommandDelete()
        {
            DeleteData();
            RetrieveData();
            return true;
        }

        public override bool OnCommandRefresh()
        {
            RetrieveData();
            return true;
        }
        #endregion


    }
}
