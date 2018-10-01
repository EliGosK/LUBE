using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EAP.Framework.Windows.Forms;

namespace Presentation.Forms.FindDialog
{
    public class FindScreenDialog : EAP.Framework.Windows.Forms.FindDialog
    {
        public enum eColumns
        {
            ScreenID, ClassName, DisplayName, Description
        }

        protected override void OnLoad(EventArgs e)
        {
            //base.ShowPage = AppConfig.ConfigPage.MaxShowPage;
            //base.RowPerPage = AppConfig.ConfigPage.RowPerPage;
            base.OnLoad(e);
        }


        public FindScreenDialog(IFindDialogDAO findDao, bool bMultiSelection, bool bSearchOnStart) : base("Find Screens", findDao)
        {
            if (this.DesignMode == false)
            {                
                this.MainSQL = "select ScreenID, ClassName, DisplayName, Description from tbs_Screen";
                this.OptionalSQL = " ORDER BY ClassName";
                this.MultiSelection = bMultiSelection;
                this.FindOnStart = bSearchOnStart;
                this.OptionDirection = FindOptionDirection.Down;

                FindOption option = null;
                // Class Name
                option = this.FindOptions.Add("Class Name", "ClassName", FindOperator.Like);
                // Display Name
                option = this.FindOptions.Add("Display Name", "DisplayName", FindOperator.Like);
                // Description
                option = this.FindOptions.Add("Description", "Description", FindOperator.Like);


                this.RememberSize = new Size(800, 515);
                //this.ResultFormats.Add((int)eColumns.ScreenID, null, 2, false, CellHorizontalAlignment.General);
                //this.ResultFormats.Add((int)eColumns.ClassName, null, 395 , true, CellHorizontalAlignment.General);
                //this.ResultFormats.Add((int)eColumns.DisplayName, null, 230 , true, CellHorizontalAlignment.General);
                //this.ResultFormats.Add((int)eColumns.Description, null, 329 , true, CellHorizontalAlignment.General);
            }
        }
    }
}
