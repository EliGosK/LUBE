using BusinessService;
using Common;
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

namespace Presentation.Forms.Master
{
    public partial class FrmACS120_AddEditCostType : Form
    {
        public string diaAccCode;
        public string diaAccName;
        public string diaCostType;
        public decimal diaPerAllow;
        public decimal diaSumPercent;
        public bool isMode; //true is Insert, false is Update

        DataTable dtbCostType = new DataTable();
        private ACS120_MasterBIZ m_bizMaster = new ACS120_MasterBIZ();

        public FrmACS120_AddEditCostType()
        {
            InitializeComponent();
        }

        private void FrmACS120_AddEditCostType_Load(object sender, EventArgs e)
        {
            txtAccountCode.Text = diaAccCode;
            txtAccountName.Text = diaAccName;
            BindCostTypeCombobox();

            if (!Util.IsNullOrEmpty(diaCostType))
            {
                int index = 0;
                for (int i = 0; i < dtbCostType.Rows.Count; i++)
                {
                    if (diaCostType == dtbCostType.Rows[i]["CostType"].ToString())
                    {
                        index = i;
                        break;
                    }
                }

                cmbCostType.SelectedIndex = index;
            }

            if (!Util.IsNullOrEmptyOrZero(diaPerAllow))
            {
                txtAllocation.DecimalValue = diaPerAllow;
            }

            if (!isMode)
                cmbCostType.Enabled = false;
        }

        private void BindCostTypeCombobox()
        {
            dtbCostType = m_bizMaster.GetCostTypeCombo(null);

            cmbCostType.DataSource = dtbCostType;
            cmbCostType.ValueMember = "CostType";
            cmbCostType.DisplayMember = "CostDisplay";
        }

        private bool ValidateRequire()
        {
            //errorProvider.Clear();

            bool bValid = true;

            if (Util.IsNullOrEmpty(cmbCostType.SelectedValue))
            {
                MessageBox.Show("Please input Cost Type.", "ACS120", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bValid = false;
            }
            else
                diaCostType = cmbCostType.SelectedValue.ToString();

            if (Util.IsNullOrEmptyOrZero(txtAllocation.DecimalValue))
            {
                MessageBox.Show("Please input % Allocation.", "ACS120", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bValid = false;
            }
            else
            {
                if(isMode)
                {
                    if (diaSumPercent + txtAllocation.DecimalValue > 100)
                    {
                        MessageBox.Show("Please enter a value for Sum % Allocation not more than 100%", "ACS120", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bValid = false;
                    }
                    else
                    {
                        diaPerAllow = txtAllocation.DecimalValue;
                    }
                }
                else
                {
                    if ((diaSumPercent- diaPerAllow) + txtAllocation.DecimalValue > 100)
                    {
                        MessageBox.Show("Please enter a value for Sum % Allocation not more than 100%", "ACS120", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bValid = false;
                    }
                    else
                    {
                        diaPerAllow = txtAllocation.DecimalValue;
                    }
                }    
            }
                

            return bValid;  
        }

        private void InsertData()
        {
            try
            {
                if (!ValidateRequire())
                    return;

                if (MessageDialog.ShowConfirmationMsg(this, "Do you want to save data?") == DialogButton.Yes)
                {
                    m_bizMaster.InsertMappingData(diaAccCode, diaCostType, diaPerAllow, AppEnvironment.UserLogin);
                    MessageDialog.ShowInformationMsg("Save completed");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("This Mapping is already exists.", "ACS120", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //ExceptionManager.ManageException(this, ex);
            }
        }

        private void UpdateData()
        {
            try
            {
                if (!ValidateRequire())
                    return;

                if (MessageDialog.ShowConfirmationMsg(this, "Do you want to save data?") == DialogButton.Yes)
                {
                    m_bizMaster.UpdateMappingData(diaAccCode, diaCostType, diaPerAllow, AppEnvironment.UserLogin);
                    MessageDialog.ShowInformationMsg("Save completed");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("This Mapping is already exists.", "ACS120", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //ExceptionManager.ManageException(this, ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isMode)
                InsertData();
            else
                UpdateData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAllocation_TextChanged(object sender, EventArgs e)
        {
            if (txtAllocation.DoubleValue > 100)
            {
                txtAllocation.DoubleValue = 100.00;
            }
        }
    }
}
