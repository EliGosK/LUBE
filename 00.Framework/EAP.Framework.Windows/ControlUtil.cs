using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using EAP.Framework.Windows.Forms;

namespace EAP.Framework
{
    public static class ControlUtil
    {

        #region Default Font

        public static Font DefaultFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
        public static Font DefaultFontBold = new Font("Tahoma", 8.25f, FontStyle.Bold);

        #endregion


        #region VisibleControl
        private static void _VisibleControl(bool bVisible, Control control)
        {
            control.Visible = bVisible;
        }
        public static void VisibleControl(bool bVisible, params Control[] controls)
        {
            foreach (Control c in controls)
            {
                if (c == null)
                    continue;

                _VisibleControl(bVisible, c);
            }
        }
        public static void VisibleControl(bool bVisible, Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                _VisibleControl(bVisible, c);
            }
        }
        public static void VisibleControl(bool bVisible, params ToolBarButton[] toolbarButtons)
        {
            foreach (ToolBarButton b in toolbarButtons)
            {
                b.Visible = bVisible;
            }
        }
        
        public static void VisibleControl(bool bVisible, params ToolStripItem[] toolbarStrips)
        {
            for (int i = 0; i < toolbarStrips.Length; i++)
            {
                toolbarStrips[i].Visible = bVisible;
            }
        }

        #endregion

        #region EnabledControl

        private static void _EnabledControl(bool bEnabled, Control ctl)
        {
            if (ctl == null)
                return;

            if (ctl is System.Windows.Forms.TextBox)
            {
                ((System.Windows.Forms.TextBox)ctl).ReadOnly = !bEnabled;
                ((System.Windows.Forms.TextBox)ctl).TabStop = bEnabled;
                if (bEnabled)
                {
                    ((System.Windows.Forms.TextBox)ctl).BackColor = System.Drawing.SystemColors.Window;
                    ((System.Windows.Forms.TextBox)ctl).ForeColor = System.Drawing.SystemColors.ControlText;
                }
                else {
                    ((System.Windows.Forms.TextBox)ctl).BackColor = System.Drawing.SystemColors.Control;
                    ((System.Windows.Forms.TextBox)ctl).ForeColor = System.Drawing.SystemColors.GrayText;
                }
            }                        
            else if (ctl is Label == false
               && ctl is ToolBar == false
               && ctl is ListView == false
               && ctl is Splitter == false
               && ctl is TreeView == false)
            {

                ctl.Enabled = bEnabled; ;
            }            
        }                
                
        public static void EnabledControl(bool bEnabled, params Control[] Controls)
        {
            foreach (Control c in Controls)
            {
                if (c == null)
                    continue;

                if (c.Tag != null && c.Tag.ToString().Contains("no control"))
                {

                }
                else if (c is ToolStrip)
                {
                    continue;
                }
                else if (c is GroupBox
                    || c is Panel)
                {
                    EnabledControl(bEnabled, c.Controls);
                }               
                else if (c is TabControl)
                {
                    foreach (TabPage tp in ((TabControl)c).TabPages)
                    {
                        EnabledControl(bEnabled, tp.Controls);
                    }
                }
                else {
                    _EnabledControl(bEnabled, c);
                }
            }
        }
        public static void EnabledControl(bool bEnabled, Control.ControlCollection Controls)
        {
            foreach (Control c in Controls)
            {
                if (c.Tag != null && c.Tag.ToString().Contains("no control"))
                {

                }
                else if (c is ToolStrip
                    || c is Label)
                {
                    continue;
                }
                else if (c is Panel 
                    || c is GroupBox)
                {
                    EnabledControl(bEnabled, c.Controls);
                }
                else if (c is TabControl)
                {
                    foreach (TabPage tp in ((TabControl)c).TabPages)
                    {
                        EnabledControl(bEnabled, tp.Controls);
                    }
                }
                else {
                    _EnabledControl(bEnabled, c);
                }
            }
        }
        public static void EnabledControl(bool bEnabled, params ToolBarButton[] tbButtons)
        {
            foreach (ToolBarButton tbButton in tbButtons)
            {
                if (tbButton.Tag != null && tbButton.Tag.ToString().Contains("no control"))
                {

                }
                else {                    
                    tbButton.Enabled = bEnabled;                    
                }
            }
        }
        public static void EnabledControl(bool bEnabled, ToolStrip toolStrips)
        {
            for (int i = 0; i < toolStrips.Items.Count; i++)
            {
                EnabledControl(bEnabled, toolStrips.Items[i]);
            }
        }
        public static void EnabledControl(bool bEnabled, params ToolStripItem[] toolStrips)
        {
            for (int i = 0; i < toolStrips.Length; i++)
            {
                ToolStripItem t = toolStrips[i];
                if (t.Tag != null && t.Tag.ToString().Contains("no control"))
                {

                }
                else
                {
                    t.Enabled = bEnabled;

                }
            }
        }

        
        //public static void EnableControlByScreenMode(EXP.Framework.ScreenMode eScreenMode, System.Windows.Forms.Control.ControlCollection cntrlList)
        //{
        //    //Pattern:      [A1E0D1I1]  A=Mode, 1/0 = Enable/Disable
        //    string str_mode = string.Empty;

        //    switch (eScreenMode)
        //    {
        //        case ScreenMode.Idle:
        //            str_mode = "I";
        //            break;
        //        case ScreenMode.View:
        //            str_mode = "V";
        //            break;
        //        case ScreenMode.Add:
        //            str_mode = "A";
        //            break;
        //        case ScreenMode.Edit:
        //            str_mode = "E";
        //            break;
        //        case ScreenMode.Custom:
        //            str_mode = "C";
        //            break;
        //    }

        //    if (cntrlList != null && cntrlList.Count > 0)
        //    {
        //        foreach (System.Windows.Forms.Control cntrl in cntrlList)
        //        {
        //            if (cntrl.Tag != null && !string.IsNullOrEmpty(cntrl.Tag.ToString()))
        //            {
        //                foreach (Match m in System.Text.RegularExpressions.Regex.Matches(cntrl.Tag.ToString(), @"\[\w+\]", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
        //                {
        //                    foreach (Match m2 in Regex.Matches(m.Value, str_mode + ".", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
        //                    {
        //                        ControlUtil.EnabledControl(m2.Value.Substring(1, 1).Equals("1"), cntrl);
        //                        //cntrl.Enabled = m2.Value.Substring(1, 1).Equals("1");
        //                    }
        //                }
        //            }
        //            EnabledControlByScreenMode(eScreenMode, cntrl.Controls);
        //        }
        //    }
        //}

        #endregion

        #region IsEnabled
        /// <summary>
        /// for checking Enabled Control (include readonly and preview condition)
        /// </summary>
        /// <param name="ctl"></param>
        /// <returns></returns>
        public static bool IsEnabled(Control ctl)
        {
            bool bResult = false;
            if (ctl == null)
                return bResult;

            if (ctl is System.Windows.Forms.TextBox)
            {
                bResult = ctl.Visible && ctl.Enabled && !((System.Windows.Forms.TextBox) ctl).ReadOnly;
            }
            else
                bResult = ctl.Visible && ctl.Enabled;

            return bResult;
        }

        #endregion

        #region ClearControlData
        private static void _ClearControlData(Control ctl)
        {
            if (ctl == null)
                return;
            if (ctl is System.Windows.Forms.TextBox)
            {
                ctl.Text = string.Empty;               
            }                                   
            else if (ctl is CheckBox)
            {
                ((CheckBox)ctl).Checked = false;
            }                             
            else if (ctl is System.Windows.Forms.ComboBox && ((System.Windows.Forms.ComboBox)ctl).DropDownStyle == ComboBoxStyle.DropDownList)
            {
                if (((System.Windows.Forms.ComboBox)ctl).Items.Count > 0)
                    ((System.Windows.Forms.ComboBox)ctl).SelectedIndex = 0;
                else
                    ((System.Windows.Forms.ComboBox)ctl).SelectedIndex = -1;
            }
        }
        public static void ClearControlData(params Control[] Controls)
        {
            foreach (Control c in Controls)
            {
                if (c is GroupBox
                    || c is Panel
                    || c is TabControl)
                {
                    ClearControlData(c.Controls);
                }                                               
                else {
                    _ClearControlData(c);
                }
            }
        }
        public static void ClearControlDataExcept(System.Windows.Forms.Control.ControlCollection ControlCollection, Control control1, Control control2)
        {
            foreach (Control c in ControlCollection)
            {
                if (c.Equals(control1) || c.Equals(control2))
                {
                    continue;
                }

                if (c is GroupBox
                    || c is Panel
                    || c is TabControl)
                {
                    ClearControlDataExcept(c.Controls, control1, control2);
                }                
                else {
                    _ClearControlData(c);
                }
            }
        }
        public static void ClearControlData(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c is GroupBox
                    || c is Panel
                    || c is TabControl)
                {
                    ClearControlData(c.Controls);
                }                
                else
                {
                    _ClearControlData(c);
                }
            }
        }
        #endregion        
    }
}
