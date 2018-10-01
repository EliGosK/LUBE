using BusinessService;
using Common;
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

namespace Presentation.Forms.Master
{
    [ScreenPermission(Permission.OpenScreen, Permission.Add, Permission.Edit, Permission.Delete)]
    public partial class FrmACS120_MappingAccountCodeAndCostType : LUBEFormDev
    {
        string srchAccCode;
        string srchAccName;

        string mapAccCode;
        string mapAccName;
        string mapCostType;

        private ACS120_MasterBIZ m_bizMaster = new ACS120_MasterBIZ();

        public FrmACS120_MappingAccountCodeAndCostType()
        {
            InitializeComponent();
            InitialScreen();
        }

        private void InitialScreen()
        {
            ControlUtil.VisibleControl(false, m_toolBarFind, m_toolBarPrint, m_toolBarExport, m_toolBarImport, m_toolBarSave, m_toolBarCancel);
            UpdateToolbarSeparator();

            dgvProductionCost.AutoGenerateColumns = false;
            dgvMappingCost.AutoGenerateColumns = false;
            RetrieveProductionCostData();
            //RetrieveMappingCostData(null);
        }

        private void ClearControl()
        {
            txtAccountCode.Text = string.Empty;
            txtAccountName.Text = string.Empty;
            RetrieveProductionCostData();
        }

        #region validate

        private void BindSearchData()
        {
            if (Util.IsNullOrEmpty(txtAccountCode.Text))
                srchAccCode = null;
            else
                srchAccCode = txtAccountCode.Text;

            if (Util.IsNullOrEmpty(txtAccountName.Text))
                srchAccName = null;
            else
                srchAccName = txtAccountName.Text;
        }

        #endregion

        #region Process

        private void RetrieveProductionCostData()
        {
            try
            {
                BindSearchData();

                DataTable dtbProCostView = m_bizMaster.RetrievePrdData(srchAccCode, srchAccName);
                if (!Util.IsNullOrEmptyOrZero(dtbProCostView.Rows.Count))
                {
                    dgvProductionCost.DataSource = dtbProCostView;
                }
                else
                {
                    MessageBox.Show("Data not Found.", "ACS120", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
        }

        private void RetrieveMappingCostData(string mapAccCode)
        {
            try
            {
                DataTable dtbMapCostView = m_bizMaster.RetrieveMapData(mapAccCode);
                if (!Util.IsNullOrEmptyOrZero(dtbMapCostView.Rows.Count))
                {
                    dgvMappingCost.DataSource = dtbMapCostView;
                }
                else
                {
                    dgvMappingCost.DataSource = null;
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
                foreach (DataGridViewRow row in dgvMappingCost.SelectedRows)
                {
                    mapAccCode = row.Cells["AccCode"].Value.ToString(); 
                    mapCostType = row.Cells["CostType"].Value.ToString();
                }

                if (MessageDialog.ShowConfirmationMsg(this, "Do you want to delete data?") == DialogButton.Yes)
                {
                    m_bizMaster.DeleteMappingData(mapAccCode, mapCostType);
                    MessageDialog.ShowInformationMsg("Delete completed");
                    RetrieveMappingCostData(mapAccCode);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
        }

        #endregion

        #region event control

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RetrieveProductionCostData();
        }

        private void dgvProductionCost_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductionCost.SelectedRows.Count > 0)
            {
                ControlUtil.EnabledControl(false, m_toolBarDelete, m_toolBarEdit);
                ControlUtil.EnabledControl(true, m_toolBarAdd);

                foreach (DataGridViewRow row in dgvProductionCost.SelectedRows)
                {
                    mapAccCode = row.Cells[0].Value.ToString();
                }

                RetrieveMappingCostData(mapAccCode);
                //dgvMappingCost.ClearSelection();
            }
        }

        private void dgvMappingCost_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMappingCost.SelectedRows.Count > 0)
            {
                ControlUtil.EnabledControl(true, m_toolBarDelete, m_toolBarAdd, m_toolBarEdit);
                //dgvProductionCost.ClearSelection();
            }
        }

        public override bool OnCommandAdd()
        {
            foreach (DataGridViewRow row in dgvProductionCost.SelectedRows)
            {
                mapAccCode = row.Cells[0].Value.ToString();
                mapAccName = row.Cells[1].Value.ToString();
            }

            FrmACS120_AddEditCostType dialog = new FrmACS120_AddEditCostType();
            dialog.diaAccCode = mapAccCode;
            dialog.diaAccName = mapAccName;

            decimal sumPercent = 0;
            foreach (DataGridViewRow item in dgvMappingCost.Rows)
            {
                sumPercent += Convert.ToDecimal(item.Cells["PercentAllocation"].Value);
            }
            dialog.diaSumPercent = sumPercent;

            dialog.isMode = true;
            dialog.ShowDialog(this);

            RetrieveMappingCostData(mapAccCode);
            return true;
        }

        public override bool OnCommandEdit()
        {
            FrmACS120_AddEditCostType dialog = new FrmACS120_AddEditCostType();
            foreach (DataGridViewRow row in dgvMappingCost.SelectedRows)
            {
                dialog.diaAccCode = row.Cells["AccCode"].Value.ToString();
                dialog.diaAccName = row.Cells["AccName"].Value.ToString();
                dialog.diaCostType = row.Cells["CostType"].Value.ToString();
                dialog.diaPerAllow = Convert.ToDecimal(row.Cells["PercentAllocation"].Value);
            }

            decimal sumPercent = 0;
            foreach (DataGridViewRow item in dgvMappingCost.Rows)
            {
                sumPercent += Convert.ToDecimal(item.Cells["PercentAllocation"].Value);
            }
            dialog.diaSumPercent = sumPercent;
            dialog.isMode = false;
            dialog.ShowDialog(this);

            RetrieveMappingCostData(dialog.diaAccCode);
            return true;
        }

        public override bool OnCommandDelete()
        {
            DeleteData();

            return true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        #endregion

    }
}
