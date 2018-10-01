using System;
using System.Drawing;

namespace EAP.Framework.Windows.Controls.ComboResource
{
    /// <summary>
    /// 
    /// </summary>
    public interface IComboForm {
        void InitializeData(System.Data.DataTable table);
        void SetLocation(Point point);
        void SetSize(Size size);

        /** debug Mr.Fuangwith Sopharath @ 05/06/2006 **/
        void Expand(int iSelected);
        void SetNotify(SelectedItemHandler listener);
        String SearchString(String message, int colIndex, ref int iSelected);
        void SetFont(System.Drawing.Font font);

        object GetObjectInArea();


        /** for Table **/
        void SetColumnWidth(String colName, int width);
        void SetColumnWidth(int colIndex, int width);
        void SetAlignment(int colIndex, System.Windows.Forms.HorizontalAlignment align);
        /** for Tree **/
    }
}
