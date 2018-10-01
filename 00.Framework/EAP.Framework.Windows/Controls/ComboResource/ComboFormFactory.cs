using System;
using System.Drawing;


namespace EAP.Framework.Windows.Controls.ComboResource
{
    /// <summary>
    /// 
    /// </summary>
    public class ComboFormFactory {
        public ComboFormFactory() {
        }

        public static IComboForm CreateForm(ComboFormMode mode) {
            IComboForm cmbForm = null;
            if (ComboFormMode.COLUMN == mode) {
                cmbForm = new ListViewForm();
            } else if (ComboFormMode.TREE == mode) {
                cmbForm = new TreeForm();
            }
            return cmbForm;
        }
    }
}
