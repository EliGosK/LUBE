using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Threading;
using System.Globalization;
using EAP.Framework.Windows.Controls;

namespace EAP.Framework.Windows.Forms
{
    public interface IValueType {

        object[] GetValue();        
        void SetValue(params object[] value);
        Control ControlObject { get; }
        int GetWidth(Graphics g, Font font);
        void HilightControl();
        bool Enabled { get; set; }
    }

    public class NumberValueType : IValueType
    {
        private NumericTextBox m_control;
        public NumberValueType(int iMaxDecimalPlaces)
        {
            m_control = new NumericTextBox();
            m_control.MaxDecimalPlaces = iMaxDecimalPlaces;
        }
        public object[] GetValue()
        {
            return new object[] { m_control.DoubleValue };
        }

        public void SetValue(params object[] value)
        {
            if (value != null)
            {
                if (value.Length == 1)
                {
                    m_control.Text = value[0].ToString();
                }
                else
                {
                    m_control.Text = string.Empty;
                }
            }
            else
            {
                m_control.Text = string.Empty;
            }
        }

        public Control ControlObject
        {
            get
            {
                return m_control;
            }
        }

        public int GetWidth(Graphics g, Font font)
        {
            return m_control.Width;
        }

        public void HilightControl()
        {
            m_control.Focus();
            m_control.SelectAll();
        }

        public bool Enabled
        {
            get
            {
                return !m_control.ReadOnly;
            }
            set
            {
                m_control.ReadOnly = !value;
            }
        }
    }

    public class TextValueType : IValueType
    {
        private TextBox m_control;
        public TextValueType()
        {
            m_control = new TextBox();
        }
        //Apiwat add
        public TextValueType(string strDefault)
        {
            m_control = new TextBox();
            m_control.Text = strDefault;
        }
        //End add
        public int GetWidth(Graphics g, Font font)
        {
            return m_control.Width;
        }
        public object[] GetValue()
        {
            string[] values = m_control.Text.Split(FindDialog.MultiValueSeperator);
            for (int i = 0; i < values.Length; ++i)
            {
                values[i] = values[i].Trim();
            }
            return values;
        }
        public void HilightControl()
        {
            m_control.Focus();
            m_control.SelectAll();
        }
        public bool Enabled
        {
            get { return !m_control.ReadOnly; }
            set { m_control.ReadOnly = !value; }
        }
        public void SetValue(params object[] value)
        {
            if (value != null)
            {
                string strValue = string.Empty;
                for (int i = 0; i < value.Length; ++i)
                {
                    strValue += (value[i] == null || value[i] == DBNull.Value) ? string.Empty : value[i].ToString();
                    if (i < value.Length - 1)
                    {
                        strValue += FindDialog.MultiValueSeperator;
                    }
                }
                m_control.Text = strValue;
            }
        }
        public Control ControlObject
        {
            get { return m_control; }
        }
    }

    public class DateTimeValueType : IValueType
    {
        private DateTextBoxWithCalendar m_control;

        private string str_default = string.Empty;

        public DateTimeValueType()
        {
            m_control = new DateTextBoxWithCalendar();

            m_control.BackColor = System.Drawing.SystemColors.Control;
            m_control.Format = "dd/MM/yyyy";
            //m_control.dtpStartDate.Name = "dtpStartDate";
            m_control.Value = new System.DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, System.DateTime.Today.Day, 0, 0, 0, 0);
        }
        public DateTimeValueType(DateTime dtDefaultDate)
        {
            m_control = new DateTextBoxWithCalendar();

            m_control.BackColor = System.Drawing.SystemColors.Control;
            m_control.Format = "dd/MM/yyyy";
            //m_control.dtpStartDate.Name = "dtpStartDate";
            m_control.Value = dtDefaultDate;
        }

        public DateTimeValueType(string strDateTimeFormat)
            : this()
        {
            m_control.Format = strDateTimeFormat;
            if (strDateTimeFormat.Equals("HH:mm"))
            {
                m_control.ShowButton = false;
            }
        }
        public bool Enabled
        {
            get { return m_control.Enabled; }
            set { m_control.Enabled = value; }
        }
        public object[] GetValue()
        {
            return new object[] { m_control.Value };
        }
        public object Getdefault()
        {
            return str_default;
        }
        public void HilightControl()
        {
            m_control.Focus();
        }
        public int GetWidth(Graphics g, Font font)
        {
            return (int)g.MeasureString(m_control.Text + "-", font).Width + SystemInformation.CaptionButtonSize.Width * 2;
        }
        public void SetValue(params object[] value)
        {
            if (value != null)
            {
                if (value.Length > 0)
                {
                    object oValue = value[0];
                    if (oValue != null && oValue != DBNull.Value)
                    {
                        if (oValue is DateTime)
                        {
                            DateTime dt = (DateTime)value[0];                            
                            m_control.Value = dt;
                        }
                    }
                }
            }
        }
        public Control ControlObject
        {
            get { return m_control; }
        }
    }

    public class ComboValueType : IValueType
    {
        #region IValueType Members
        private EAPComboBox m_control;
        private string str_default = string.Empty;

        public ComboValueType(params string[] values)
        {
            m_control = new EAPComboBox();
            m_control.AutoSearch = true;

            m_control.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            m_control.Items.AddRange(values);

            if (m_control.Items.Count > 0)
            {
                m_control.SelectedIndex = 0;
            }
        }

        public ComboValueType(ArrayList values)
        {
            m_control = new EAPComboBox();

            m_control.AutoSearch = true;

            m_control.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            foreach (string data in values)
            {
                m_control.Items.Add(data);
            }
            if (m_control.Items.Count > 0)
            {
                m_control.SelectedIndex = 0;
            }
        }

        public ComboValueType(DataTable dt)
        {
            m_control = new EAPComboBox();
            m_control.AutoSearch = true;
            m_control.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dat = dt.Rows[i];
                    if (!dat.IsNull(0) && dat[0] != DBNull.Value && dat[0].ToString() != "")
                    {
                        m_control.Items.Add(dat[0]);
                    }
                }

                if (m_control.Items.Count > 0)
                {
                    m_control.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageDialog.ShowSystemErrorMsg(null, ex);
            }
        }

        public ComboValueType(DataTable dt, string strSelItem)
        {
            m_control = new EAPComboBox();
            m_control.AutoSearch = true;

            m_control.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dat = dt.Rows[i];
                    if (!dat.IsNull(0) && dat[0] != DBNull.Value && dat[0].ToString() != "")
                    {
                        m_control.Items.Add(dat[0]);
                    }
                }

                if (m_control.Items.Count > 0)
                {
                    m_control.SelectedIndex = m_control.FindString(strSelItem);
                }
            }
            catch (Exception ex)
            {
                MessageDialog.ShowSystemErrorMsg(null, ex);
            }
        }

        object[] IValueType.GetValue()
        {
            if (m_control.SelectedIndex >= 0)
            {
                return new Object[] { m_control.SelectedItem.ToString() }; //return an array of object
            }
            else
            {
                if (!m_control.Text.Equals(string.Empty))
                {
                    return new Object[] { m_control.Text.Trim() }; //return an array of object
                }
            }
            return new Object[] { string.Empty };
        }

        public Object GetDefault()
        {
            return str_default;
        }

        void IValueType.SetValue(params object[] value)
        {
            if (value == null || value.Length <= 0 || value[0] == null)
            {
                return;
            }
            int index = m_control.FindStringExact(value[0].ToString());
            if (index >= 0)
            {
                m_control.Text = m_control.Items[index].ToString();
            }
        }

        public int SelectedIndex
        {
            set { m_control.SelectedIndex = value; }
            get { return m_control.SelectedIndex; }
        }

        Control IValueType.ControlObject
        {
            get { return m_control; }
        }

        int IValueType.GetWidth(Graphics g, Font font)
        {
            return m_control.Width;
        }

        void IValueType.HilightControl()
        {
            m_control.Focus();
        }

        bool IValueType.Enabled
        {
            get
            {
                return m_control.Enabled;
            }
            set
            {
                m_control.Enabled = value;
            }
        }

        #endregion
    };

    public class ComboValueTypeReturnIndex : IValueType
    {
        #region IValueType Members
        private EAPComboBox m_control;
        private string str_default = "";
        private int iStartIndex = 0;

        public ComboValueTypeReturnIndex(int iStartIndex, params string[] values)
        {
            m_control = new EAPComboBox();
            m_control.AutoSearch = true;

            m_control.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            m_control.Items.AddRange(values);

            if (m_control.Items.Count > 0)
            {
                m_control.SelectedIndex = 0;
            }

            this.iStartIndex = iStartIndex;
        }

        public ComboValueTypeReturnIndex(int iStartIndex, ArrayList values)
        {
            m_control = new EAPComboBox();
            m_control.AutoSearch = true;

            m_control.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            foreach (string data in values)
            {
                m_control.Items.Add(data);
            }
            if (m_control.Items.Count > 0)
            {
                m_control.SelectedIndex = 0;
            }

            this.iStartIndex = iStartIndex;
        }

        public ComboValueTypeReturnIndex(int iStartIndex, DataTable dt)
        {
            m_control = new EAPComboBox();
            m_control.AutoSearch = true;

            m_control.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.iStartIndex = iStartIndex;

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dat = dt.Rows[i];
                    if (!dat.IsNull(0) && dat[0] != DBNull.Value && dat[0].ToString() != "")
                    {
                        m_control.Items.Add(dat[0]);
                    }
                }

                if (m_control.Items.Count > 0)
                {
                    m_control.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageDialog.ShowSystemErrorMsg(null, ex);
            }
        }

        object[] IValueType.GetValue()
        {
            if (m_control.SelectedIndex >= 0)
            {
                return new Object[] { (m_control.SelectedIndex + this.iStartIndex) }; //return an array of object
            }
            else
            {
                if (m_control.Text != null && m_control.IsInList)
                {
                    if (m_control.Items.Count > 0 || m_control.DataBindings.Count > 0)
                    {
                        return new Object[] { 0 }; //return an array of object
                    }
                    else
                    {
                        return new Object[] { -1 };
                    }
                    //---------------------------------
                }
            }
            return new Object[] { };
        }

        public int SelectedIndex
        {
            set
            {
                if (m_control.Items.Count > value || m_control.DataBindings.Count > value)
                {
                    m_control.SelectedIndex = value;
                }
                else
                {
                    m_control.SelectedIndex = -1;
                }
            }
            get { return m_control.SelectedIndex; }
        }

        public Object GetDefault()
        {
            return str_default;
        }

        void IValueType.SetValue(params object[] value)
        {
            if (value == null || value.Length <= 0 || value[0] == null)
            {
                return;
            }
            int index = m_control.FindStringExact(value[0].ToString());
            if (index >= 0)
            {
                m_control.Text = m_control.Items[index].ToString();
            }
        }

        Control IValueType.ControlObject
        {
            get { return m_control; }
        }

        int IValueType.GetWidth(Graphics g, Font font)
        {
            return m_control.Width;
        }

        void IValueType.HilightControl()
        {
            m_control.Focus();
        }

        bool IValueType.Enabled
        {
            get
            {
                return m_control.Enabled;
            }
            set
            {
                m_control.Enabled = value;
            }
        }

        #endregion
    };

    public class YesNoComboValueType : IValueType
    {
        #region IValueType Members
        private EAPComboBox m_control;
        private string str_default = "";

        public YesNoComboValueType()
        {
            m_control = new EAPComboBox();
            m_control.AutoSearch = true;

            m_control.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            m_control.Items.AddRange(new object[] { "YES", "NO" });

            // Add by Wirachai T. 2007/06/18
            if (m_control.Items.Count > 0)
            {
                m_control.SelectedIndex = 0;
            }
        }

        public object[] GetValue()
        {
            if (m_control.SelectedIndex >= 0)
            {
                if (m_control.SelectedIndex == 0)
                {
                    return new Object[] { 1 };
                }
                else
                {
                    return new Object[] { 0 };
                }

            }
            else
            {
                if (m_control.Text != null)
                {
                    return new Object[] { m_control.Text.ToString() }; //return an array of object
                }
            }
            return new Object[] { };
        }

        public int SelectedIndex
        {
            set { m_control.SelectedIndex = value; }
            get { return m_control.SelectedIndex; }
        }

        public Object GetDefault()
        {
            return str_default;
        }

        void IValueType.SetValue(params object[] value)
        {
            if (value == null || value.Length <= 0 || value[0] == null)
            {
                return;
            }
            int index = m_control.FindStringExact(value[0].ToString());
            if (index >= 0)
            {
                m_control.Text = m_control.Items[index].ToString();
            }
        }

        Control IValueType.ControlObject
        {
            get { return m_control; }
        }

        int IValueType.GetWidth(Graphics g, Font font)
        {
            return m_control.Width;
        }

        void IValueType.HilightControl()
        {
            m_control.Focus();
        }

        bool IValueType.Enabled
        {
            get
            {
                return m_control.Enabled;
            }
            set
            {
                m_control.Enabled = value;
            }
        }
        #endregion
    };

   
}

