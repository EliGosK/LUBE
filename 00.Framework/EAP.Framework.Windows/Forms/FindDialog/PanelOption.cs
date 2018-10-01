using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;

namespace EAP.Framework.Windows.Forms
{

    [System.ComponentModel.ToolboxItemAttribute(false)]
    public class PanelOption : System.Windows.Forms.Panel
    {
        public event KeyPressEventHandler OnKeyEnterPressed = null;

        private FindOption m_findOption;
        private CheckBox m_chkBox;
        public Label m_lblOptionName;

        private ComboBox m_cboOperators;
        private Control m_inputValue1;
        private Button m_btnListValue1;

        private Label m_lblTo;
        private Control m_inputValue2;
        private Button m_btnListValue2;

        //		private IValueType m_valueType1;
        //		private IValueType m_valueType2;

        

        private CheckBox CreateCheckBox(FindOption fOption)
        {
            CheckBox chkBox = new CheckBox();
            //chkBox.Size = new Size(chkBox.Height + 5, chkBox.Height);
            chkBox.Size = new Size(chkBox.Height - 10, chkBox.Height);
            chkBox.Dock = DockStyle.Left;
            chkBox.Checked = fOption.Checked;
            return chkBox;
        }


        private Label CreateLabelName(FindOption fOption, int iWidth)
        {
            Label lbl = new Label();
            lbl.FlatStyle = FlatStyle.Standard;
            lbl.Text = fOption.Caption;
            lbl.Dock = DockStyle.Left;
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.AutoSize = false;
            lbl.Width = iWidth;
            return lbl;
        }       

        private ComboBox CreateComboBoxOperator(FindOption fOption, int iWidth)
        {
            ComboBox cboCompare = new ComboBox();
            cboCompare.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCompare.Dock = DockStyle.Left;            
            cboCompare.Width = iWidth;            
            //
            foreach (FindOperator oper in fOption.FindOperators)
            {
                //cboCompare.Items.Add(oper.Name);
                cboCompare.Items.Add(oper.Display);
            }

            if (cboCompare.Items.Count > 0)
            {
                int index = cboCompare.Items.IndexOf(fOption.SelectedOperator.Name);
                if (index >= 0)
                {
                    cboCompare.SelectedIndex = index;
                }
                else
                {
                    cboCompare.SelectedIndex = 0;
                }
                return cboCompare;
            }
            else
            {
                return null;
            }
        }

        private void CreateControls(FindOption findOption, int iOptionNameWidth, int iCompareNameWidth)
        {
            // Check Box
            m_chkBox = CreateCheckBox(findOption);
            // Label Name
            m_lblOptionName = CreateLabelName(findOption, iOptionNameWidth);
            if (findOption.FindOperators.Count > 0)
            {
                // create combo box operator
                m_cboOperators = CreateComboBoxOperator(findOption, iCompareNameWidth);
                // create input value1
                findOption.ValueType1.SetValue(findOption.SelectedValue1);
                m_inputValue1 = findOption.ValueType1.ControlObject;

                // create button list value 1
                m_btnListValue1 = new Button();
                m_btnListValue1.FlatStyle = FlatStyle.System;
                m_btnListValue1.Size = new Size(m_inputValue1.Height, m_inputValue1.Height);
                m_btnListValue1.Text = "...";
               
                m_lblTo = null;                

                // If FindOperator is contain "between", so I have to create one InputValue
                if (findOption.FindOperators.Contains(FindOperator.Between))
                {
                    // create to label
                    m_lblTo = new Label();
                    //Fix bug by Wirachai T., Raktai C. 2007/06/07					
                    //m_lblTo.FlatStyle = FlatStyle.System;
                    m_lblTo.FlatStyle = FlatStyle.Standard;
                    m_lblTo.Text = "to";
                    m_lblTo.TextAlign = ContentAlignment.MiddleLeft;
                    m_lblTo.Width = 20;
                    // create input value 2
                    findOption.ValueType2.SetValue(findOption.SelectedValue2);
                    m_inputValue2 = findOption.ValueType2.ControlObject;

                    // create button list value 2
                    m_btnListValue2 = new Button();
                    m_btnListValue2.FlatStyle = FlatStyle.Standard;
                    m_btnListValue2.Size = new Size(m_inputValue2.Height, m_inputValue2.Height);
                    m_btnListValue2.Text = "...";
                }
                // add control to me
                if (findOption.FindOperators.Contains(FindOperator.Between))
                {
                    //Modify By Wirachai T. 2007/07/12
                    //this.Controls.Add(m_btnListValue2);
                    //
                    this.Controls.Add(m_inputValue2);
                    this.Controls.Add(m_lblTo);                    
                    EnabledControl(findOption.EditValueAble, m_btnListValue2, m_inputValue2);
                }
                
                this.Controls.Add(m_inputValue1);
                this.Controls.Add(m_cboOperators);                
                EnabledControl(findOption.EditValueAble, m_btnListValue1, m_inputValue1);
            }
            this.Controls.Add(m_lblOptionName);
            this.Controls.Add(m_chkBox);

            // set control disabled, if EditCheckAble is false
            m_chkBox.Enabled = findOption.EditCheckAble;
        }

        private void SetTabIndex()
        {
            // set tab index.
            int iTabIndex = 1;
            m_chkBox.TabIndex = iTabIndex++;
            m_lblOptionName.TabIndex = iTabIndex++;
            if (m_cboOperators != null)
            {
                m_cboOperators.TabIndex = iTabIndex++;
                m_inputValue1.TabIndex = iTabIndex++;
                m_btnListValue1.TabIndex = iTabIndex++;
                if (m_findOption.FindOperators.Contains(FindOperator.Between))
                {
                    m_lblTo.TabIndex = iTabIndex++;
                    m_inputValue2.TabIndex = iTabIndex++;
                    m_btnListValue2.TabIndex = iTabIndex++;
                }
            }
        }

        private void SetControlPosition()
        {
            if (m_findOption.FindOperators.Count > 0)
            {
                //Add by Wirachai T. 2007/07/12
                m_btnListValue1.Width = 0;
                //End Add

                m_inputValue1.Left = m_cboOperators.Right + SystemInformation.BorderSize.Width;
                m_btnListValue1.Left = m_inputValue1.Right + SystemInformation.BorderSize.Width;
                if (m_findOption.FindOperators.Contains(FindOperator.Between))
                {
                    //Add by Wirachai T. 2007/07/12
                    m_btnListValue2.Width = 0;
                    //End Add

                    //Fix by Wirachai T. 2007/06/20
                    //m_lblTo.Left = m_btnListValue1.Right;    
                    //
                    m_lblTo.Left = m_inputValue1.Right;
                    m_inputValue2.Left = m_lblTo.Right;
                    m_btnListValue2.Left = m_inputValue2.Right + SystemInformation.BorderSize.Width;
                }
            }
        }

        private void SetButtonList()
        {
            if (m_findOption.FindOperators.Count > 0)
            {
                bool bBtnListVisible = false;
                if (m_findOption.ValueList1 != null)
                {
                    bBtnListVisible = m_findOption.ValueList1.Count > 0;
                }
                m_btnListValue1.Visible = bBtnListVisible;

                if (m_findOption.FindOperators.Contains(FindOperator.Between))
                {
                    bBtnListVisible = false;
                    if (m_findOption.ValueList2 != null)
                    {
                        bBtnListVisible = m_findOption.ValueList2.Count > 0;
                    }
                    m_btnListValue2.Visible = bBtnListVisible;
                }
            }
        }

        private void AttachControlEvent()
        {
            // attach event
            m_chkBox.CheckedChanged += new EventHandler(m_chkBox_CheckedChanged);
            //// Added by Wirachai T. 2008 10 07
            //m_chkBox.KeyDown += new KeyEventHandler(this.Control_KeyDown);
            //// End
            m_lblOptionName.MouseDown += new MouseEventHandler(m_lblOptionName_MouseDown);
            if (m_findOption.FindOperators.Count > 0)
            {
                m_cboOperators.SelectedIndexChanged += new EventHandler(m_cboOperators_SelectedIndexChanged);
                //// Added by Wirachai T. 2008 10 07
                //m_cboOperators.KeyDown += new KeyEventHandler(this.Control_KeyDown);
                //// End
            }

            OnCheckBoxChanged();
            if (m_findOption.FindOperators.Count > 0)
            {
                OnFindOperatorChanged();
            }            

            m_chkBox.KeyPress += new KeyPressEventHandler(this.Control_KeyPress);
            if (m_findOption.FindOperators.Count > 0)
            {
                // attach event for RequestFind Operation
                if (m_findOption.FindOperators.Contains(FindOperator.Between))
                {
                    m_btnListValue2.KeyPress += new KeyPressEventHandler(this.Control_KeyPress);
                    m_inputValue2.KeyPress += new KeyPressEventHandler(this.Control_KeyPress);
                    //// Added by Wirachai T. 2008 10 07
                    //m_inputValue2.KeyDown += new KeyEventHandler(this.Control_KeyDown);
                    //// End
                    m_inputValue2.TextChanged += new EventHandler(m_inputValue2_TextChanged);
                    m_lblTo.KeyPress += new KeyPressEventHandler(this.Control_KeyPress);
                }
                m_btnListValue1.KeyPress += new KeyPressEventHandler(this.Control_KeyPress);
                m_inputValue1.KeyPress += new KeyPressEventHandler(this.Control_KeyPress);
                //// Added by Wirachai T. 2008 10 07
                //m_inputValue1.KeyDown += new KeyEventHandler(this.Control_KeyDown);
                //// End
                m_inputValue1.TextChanged += new EventHandler(m_inputValue1_TextChanged);
                m_cboOperators.KeyPress += new KeyPressEventHandler(this.Control_KeyPress);
                m_lblOptionName.KeyPress += new KeyPressEventHandler(this.Control_KeyPress);


                m_btnListValue1.Click += new EventHandler(m_btnListValue1_Click);
                if (m_btnListValue2 != null)
                {
                    m_btnListValue2.Click += new EventHandler(m_btnListValue2_Click);
                }
            }
        }


        private void AssignValuePopup()
        {
            if (m_findOption.ValuePopup1 != null)
            {
                m_findOption.ValuePopup1.SetListValues(m_findOption.ValueList1);
            }
            if (m_findOption.ValuePopup2 != null)
            {
                m_findOption.ValuePopup2.SetListValues(m_findOption.ValueList2);
            }
        }

        public PanelOption(FindOption findOption, int iOptionNameWidth, int iCompareNameWidth)
            : base()
        {
            m_findOption = findOption;
            //			 m_valueType1 = findOption.ValueType1;
            //			m_valueType2 = findOption.ValueType2;
            CreateControls(findOption, iOptionNameWidth, iCompareNameWidth);
            AssignValuePopup();
            SetTabIndex();
            SetControlPosition();
            SetButtonList();
            AttachControlEvent();
        }

        private int CalculateWidth()
        {
            int iNewWidth = 0;
            iNewWidth += m_chkBox.Width;
            iNewWidth += m_lblOptionName.Width;
            if (m_findOption.FindOperators.Count > 0)
            {
                iNewWidth += m_cboOperators.Width;
                iNewWidth += m_inputValue1.Width;
                //Modify By Wirachai T. 2007/07/12
                //iNewWidth += m_btnListValue1.Width;
                //
                iNewWidth += (m_lblTo == null) ? 0 : m_lblTo.Width;
                iNewWidth += (m_inputValue2 == null) ? 0 : m_inputValue2.Width;
                //Modify By Wirachai T. 2007/07/12
                //iNewWidth += (m_btnListValue2 == null) ? 0 : m_btnListValue2.Width;                
                //
            }
            iNewWidth += 10;	// Offset;
            return iNewWidth;
        }

        private void EnabledControl(bool bEnabled, params Control[] Controls)
        {
            foreach (Control c in Controls)
            {
                if (c != null)
                {
                    c.Enabled = bEnabled;
                }
            }
        }
        private void EnabledControl(bool bEnabled, params IValueType[] valueType)
        {
            foreach (IValueType vType in valueType)
            {
                if (vType != null)
                {
                    vType.Enabled = bEnabled;
                }
            }
        }

        private void VisibledControl(bool bVisibled, params Control[] Controls)
        {
            foreach (Control c in Controls)
            {
                if (c != null)
                {
                    c.Visible = bVisibled;
                }
            }
        }

        private void OnCheckBoxChanged()
        {
            EnabledControl(m_chkBox.Checked/* && m_chkBox.Enabled*/, m_cboOperators, m_btnListValue1, m_lblTo, m_btnListValue2);
            EnabledControl(m_chkBox.Checked/* && m_chkBox.Enabled*/, m_findOption.ValueType1, m_findOption.ValueType2);

            // Added by Wirachai T. 2008 10 08
            // ถ้า combobox operator มีเพียงแค่ 1 อันให้ disable ไม่ต้องให้เลือก
            if (m_cboOperators.Items.Count == 1)
            {
                m_cboOperators.Enabled = false;
            }
            // End

            if (m_chkBox.Checked && m_chkBox.Enabled)
            {
                m_findOption.ValueType1.HilightControl();
            }
            if (m_findOption.EditValueAble == false)
            {
                EnabledControl(false, m_findOption.ValueType1, m_findOption.ValueType2);
            }
            m_findOption.Checked = m_chkBox.Checked;
        }

        private void OnFindOperatorChanged()
        {
            bool bVisible = FindOperator.GetOperator(m_cboOperators.Text).ToString() == FindOperator.Between.ToString();
            VisibledControl(bVisible, m_lblTo, m_inputValue2);
            //			m_lblTo.Visible = bVisible;
            //			m_inputValue2.Visible = bVisible;
            if (bVisible)
            {
                if (m_findOption.ValueList2 != null && m_btnListValue2 != null)
                {
                    m_btnListValue2.Visible = m_findOption.ValueList2.Count > 0;
                }
                else
                {
                    m_btnListValue2.Visible = false;
                }
            }
            foreach (FindOperator oper in m_findOption.FindOperators)
            {
                if (oper.Name == m_cboOperators.Text)
                {
                    m_findOption.SelectedOperator = oper;
                    break;
                }
            }
        }

        private void m_chkBox_CheckedChanged(object sender, EventArgs e)
        {
            OnCheckBoxChanged();
        }

        private void m_lblOptionName_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_chkBox.Enabled)
            {
                m_chkBox.Checked = !m_chkBox.Checked;
                
            }
        }

        private void m_cboOperators_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnFindOperatorChanged();
        }

        public IValueType ValueType1
        {
            get { return m_findOption.ValueType1; }
            set { m_findOption.ValueType1 = value; }
        }
        public IValueType ValueType2
        {
            get { return m_findOption.ValueType2; }
        }
        //Add by Wirachai T. 2007/06/20
        public Label LabelTo
        {
            get { return m_lblTo; }
        }
        public bool EnableOperator
        {
            set { m_cboOperators.Enabled = value; }
        }
        public FindOption Option
        {
            get { return this.m_findOption; }
        }
        //-----------------------------------

        public int SetInputWidth(int input1Width, int input2Width)
        {
            if (m_findOption.FindOperators.Count > 0)
            {
                m_inputValue1.Width = input1Width;
                m_btnListValue1.Left = m_inputValue1.Right + SystemInformation.BorderSize.Width;
                m_btnListValue1.Size = new Size(0, 0);
                //m_btnListValue1.Size = new Size(m_inputValue1.Height, m_inputValue1.Height);
                if (m_findOption.FindOperators.Contains(FindOperator.Between))
                {
                    m_lblTo.Left = m_btnListValue1.Right;
                    m_inputValue2.Left = m_lblTo.Right;
                    m_inputValue2.Width = input2Width;
                    m_btnListValue2.Left = m_inputValue2.Right + SystemInformation.BorderSize.Width;
                    m_btnListValue2.Size = new Size(0, 0);
                    //m_btnListValue2.Size = new Size(m_inputValue2.Height, m_inputValue2.Height);
                }
            }
            return CalculateWidth();
        }

        public void ResetState()
        {
            //Raktai Fix for use disable chkbox for custom finddialog 2007/06/22
            if (!m_chkBox.Enabled)
            {
                return;
            }
            //------------------------------------------------------------------
            m_chkBox.Checked = false;
            if (this.m_findOption.ValueType1 != null)
            {
                this.m_findOption.ValueType1.SetValue(string.Empty);
            }
            if (this.m_findOption.ValueType2 != null)
            {
                this.m_findOption.ValueType2.SetValue(string.Empty);
            }
        }

        public bool Checked
        {
            get { return m_chkBox.Checked; }
            //Add by Wirachai T. 2007/06/18
            set { m_chkBox.Checked = value; }
            //--------------------------------
        }
        public string FieldMap
        {
            get { return m_findOption.FieldMap; }
        }
        public string KeyName
        {
            get { return m_findOption.Name; }
        }
        public FindOperator SelectedOperator
        {
            get
            {
                if (m_findOption.FindOperators.Count > 0)
                {
                    return new FindOperator(FindOperator.GetOperator(m_cboOperators.Text));

                    //return new FindOperator(
                    //    (FindOperator.Operator)Enum.Parse(typeof(FindOperator.Operator),
                    //     m_cboOperators.Text)
                    //    );
                }
                else
                {
                    return FindOperator.EqualTo;
                }
            }
        }
        public object[] SelectedValue1
        {
            get
            {
                if (m_findOption.FindOperators.Count > 0)
                {
                    return m_findOption.ValueType1.GetValue();
                }
                else
                {
                    return new object[] { m_chkBox.Checked };
                }
            }
        }
        public object[] SelectedValue2
        {
            get
            {
                if (m_findOption.FindOperators.Count > 0)
                {
                    return m_findOption.ValueType2.GetValue();
                }
                else
                {
                    return new object[] { m_chkBox.Checked };
                }
            }
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (this.OnKeyEnterPressed != null)
                {
                    this.OnKeyEnterPressed(sender, e);
                }
            }
        }

        // Added by Wirachai T. 2008 10 07
        //private void Control_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        // ถ้าเป็น find dialog จะมีการผูก event นี้ไว้เพื่อให้กด enter แล้ว search ข้อมูลทันที
        //        if (OnKeyEnterPressed == null)
        //        {
        //            Control ctrl = (Control)sender;
        //            Form form = ctrl.FindForm();

        //            if (e.KeyCode == Keys.Enter)
        //            {
        //                form.SelectNextControl(form.ActiveControl, true, true, true, true);
        //            }
        //            else if (e.KeyCode == Keys.Back)
        //            {
        //                if ((sender is DateTextBoxWithCalendar) && ((DateTextBoxWithCalendar)sender).IsEmpty())
        //                {
        //                    form.SelectNextControl(form.ActiveControl, false, true, true, false);
        //                }
        //                else if (sender is ComboBox)
        //                {
        //                    form.SelectNextControl(form.ActiveControl, false, true, true, false);
        //                }
        //                else if (((Control)sender).Text.Trim().Equals(""))
        //                {
        //                    form.SelectNextControl(form.ActiveControl, false, true, true, false);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
                
        //    }
        //}
        // End

        private void ShowPopupValue(IValuePopup valuePopup, IValueType valueType)
        {
            if (valuePopup != null)
            {
                valuePopup.SetValues(valueType.GetValue());
                BaseValuePopup dlg = (BaseValuePopup)valuePopup;
                dlg.Location = Cursor.Position;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    valueType.SetValue(valuePopup.GetValues());
                }
            }
        }

        private void m_btnListValue1_Click(object sender, EventArgs e)
        {
            ShowPopupValue(m_findOption.ValuePopup1, m_findOption.ValueType1);
        }

        private void m_btnListValue2_Click(object sender, EventArgs e)
        {
            ShowPopupValue(m_findOption.ValuePopup2, m_findOption.ValueType2);
        }

        private void m_inputValue2_TextChanged(object sender, EventArgs e)
        {
            if (m_findOption.ValueType2 != null)
            {
                m_findOption.ValueType2.SetValue(m_findOption.ValueType2.GetValue());
            }
        }

        private void m_inputValue1_TextChanged(object sender, EventArgs e)
        {
            //if (m_findOption.ValueType1 != null){
            //    m_findOption.ValueType1.SetValue(m_findOption.ValueType1.GetValue());
            //}
        }
    }
}
