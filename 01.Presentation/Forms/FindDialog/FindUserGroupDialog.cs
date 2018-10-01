using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EAP.Framework.Windows.Forms;

namespace Presentation.Forms.FindDialog
{
    public class FindUserGroupDialog : EAP.Framework.Windows.Forms.FindDialog
    {
        public enum eColumns
        {
            GroupID, GroupName, Description, CreateDate, UpdateDate, CreateUser, UpdateUser
        }       

        public FindUserGroupDialog(IFindDialogDAO findDao, bool bMultiSelection, bool bSearchOnStart) : base("Find User Group", findDao)
        {
            if (this.DesignMode == false)
            {
                this.Text = "Find User Group";
                this.MainSQL = "select GroupID, GroupName, Description, CreateDate, UpdateDate, CreateUser, UpdateUser from tbs_UserGroups ";
                this.OptionalSQL = "order by GroupName";
                this.MultiSelection = bMultiSelection;
                this.OptionDirection = FindOptionDirection.Down;
                this.FindOnStart = bSearchOnStart;

                FindOption option = null;
                // Group Name
                option = this.FindOptions.Add("Group Name", "GroupName", FindOperator.Like);
                // Description
                option = this.FindOptions.Add("Description", "Description", FindOperator.Like);

                this.RememberSize = new Size(730, 500);

                //this.ResultFormats.Add((int)eColumns.GroupID, null, 65 , false, CellHorizontalAlignment.General);
                //this.ResultFormats.Add((int)eColumns.GroupName, null, 117 , true, CellHorizontalAlignment.General);
                //this.ResultFormats.Add((int)eColumns.Description, null, 164 , true, CellHorizontalAlignment.General);
                //this.ResultFormats.Add((int)eColumns.CreateDate, Util.GetDateTimeCellType(), 120 , true, CellHorizontalAlignment.General);
                //this.ResultFormats.Add((int)eColumns.UpdateDate, Util.GetDateTimeCellType(), 116, true, CellHorizontalAlignment.General);
                //this.ResultFormats.Add((int)eColumns.CreateUser, null, 93 , true, CellHorizontalAlignment.General);
                //this.ResultFormats.Add((int)eColumns.UpdateUser, null, 94 , true, CellHorizontalAlignment.General);
            }
        }
    }
}
