using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using EAP.Framework.Windows.Forms;

namespace Presentation.Forms.FindDialog
{
    public class FindUserDialog : EAP.Framework.Windows.Forms.FindDialog
    {
        public enum eColumn
        {
            Username, FirstName, LastName, Tel, Mobile, Email
        }

        protected override void OnLoad(EventArgs e)
        {
            //base.ShowPage = AppConfig.ConfigPage.MaxShowPage;
            //base.RowPerPage = AppConfig.ConfigPage.RowPerPage;
            base.OnLoad(e);
        }

        public FindUserDialog(IFindDialogDAO findDao, bool bMultiSelection, bool bSearchOnStart) : base("Find User", findDao)
        {
            if (this.DesignMode == false)
            {

                this.m_findDialogDAO = findDao;
                this.Text = "Find User";
                this.MainSQL = "select Username, FirstName, LastName, Email from tbs_User";

                this.OptionalSQL = "ORDER BY Username";
                this.MultiSelection = bMultiSelection;
                this.FindOnStart = bSearchOnStart;

                FindOption option = null;
                // Username
                option = this.FindOptions.Add("User Name", "Username", FindOperator.Like);
                option.EditCheckAble = true;
                // FirstName
                option = this.FindOptions.Add("First Name", "FirstName", FindOperator.Like);
                // LastName
                option = this.FindOptions.Add("Last Name", "LastName", FindOperator.Like);                
                // Email
                option = this.FindOptions.Add("Email", "Email", FindOperator.Like);


                // Result Format
                this.RememberSize = new Size(730, 500);
                //this.ResultFormats.Add((int)eColumn.Username, null, 78 , true, CellHorizontalAlignment.General);
                //this.ResultFormats.Add((int)eColumn.FirstName, null, 96 , true, CellHorizontalAlignment.General);
                //this.ResultFormats.Add((int)eColumn.Name, null, 82 , true, CellHorizontalAlignment.General);
                //this.ResultFormats.Add((int)eColumn.Tel, null, 86 , true, CellHorizontalAlignment.General);
                //this.ResultFormats.Add((int)eColumn.Mobile, null, 86 , true, CellHorizontalAlignment.General);
                //this.ResultFormats.Add((int)eColumn.Email, null, 156 , true, CellHorizontalAlignment.General);
            }
        }

        private void InitializeComponent()
        {
            this.grpFindOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFindOptions
            // 
            this.grpFindOptions.Location = new System.Drawing.Point(0, 24);
            this.grpFindOptions.Size = new System.Drawing.Size(738, 77);
            // 
            // panelFindOptions
            // 
            this.panelFindOptions.Size = new System.Drawing.Size(644, 58);
            // 
            // panelFindOptionsButton
            // 
            this.panelFindOptionsButton.Location = new System.Drawing.Point(647, 16);
            this.panelFindOptionsButton.Size = new System.Drawing.Size(88, 58);
            // 
            // panelResultButton
            // 
            this.panelResultButton.Size = new System.Drawing.Size(738, 32);
            // 
            // panelExtTop
            // 
            this.panelExtTop.Size = new System.Drawing.Size(738, 24);
            // 
            // FindUserDialog
            // 
            this.ClientSize = new System.Drawing.Size(738, 510);
            this.MinimumSize = new System.Drawing.Size(136, 548);
            this.Name = "FindUserDialog";
            this.ShowPageNavigator = true;
            this.grpFindOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
