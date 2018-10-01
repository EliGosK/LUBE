using System;
using System.Collections;

namespace EAP.Framework.Windows.Forms
{
    public class FindOption {
        private bool m_bAllowMultiValue = false;
        private bool m_bChecked = false;
        private FindOperator m_selectedOperator = FindOperator.Like;
        private string m_strName = string.Empty;
        private string m_strCaption = string.Empty;
        private string m_strFieldMap = string.Empty;
        private FindOperatorCollection m_findOperators = new FindOperatorCollection();
        private IValueType m_valueType1 = new TextValueType();
        private IValueType m_valueType2 = new TextValueType();
        private bool m_bEditCheckAble = true;	// Can Edit CheckBox
        private bool m_bEditValue = true;
        private ArrayList m_arValueList1 = new ArrayList();
        private ArrayList m_arValueList2 = new ArrayList();
        private IValuePopup m_popupValue1 = null;
        private IValuePopup m_popupValue2 = null;

        public FindOption(string strName, string strFieldMap, params FindOperator[] findOperators) {
            m_strCaption = strName;
            m_strName = strName;
            m_strFieldMap = strFieldMap;
            for (int i = 0; i < findOperators.Length; ++i) {
                m_findOperators.Add(findOperators[i]);
                if (i == 0) {
                    this.m_selectedOperator = findOperators[0];
                }
            }
            if (findOperators.Length > 0) {
                m_popupValue1 = new DefaultValuePopup();
                m_popupValue2 = new DefaultValuePopup();
                m_popupValue1.AllowMultiValue = m_bAllowMultiValue;
                m_popupValue2.AllowMultiValue = m_bAllowMultiValue;

            }
        }
        //Apiwat add 2007-10-02
        public FindOption(string strName, string strFieldMap, IValueType valueType1, params FindOperator[] findOperators) {
            m_strCaption = strName;
            m_strName = strName;
            m_strFieldMap = strFieldMap;
            m_valueType1 = valueType1;
            for (int i = 0; i < findOperators.Length; ++i) {
                m_findOperators.Add(findOperators[i]);
                if (i == 0) {
                    this.m_selectedOperator = findOperators[0];
                }
            }
            m_popupValue1 = new DefaultValuePopup();
            m_popupValue1.AllowMultiValue = m_bAllowMultiValue;
        }
        //End add
        public FindOption(string strName, string strFieldMap, IValueType valueType1, IValueType valueType2, params FindOperator[] findOperators) {
            m_strCaption = strName;
            m_strName = strName;
            m_strFieldMap = strFieldMap;
            m_valueType1 = valueType1;
            m_valueType2 = valueType2;
            for (int i = 0; i < findOperators.Length; ++i) {
                m_findOperators.Add(findOperators[i]);
                if (i == 0) {
                    this.m_selectedOperator = findOperators[0];
                }
            }
            m_popupValue1 = new DefaultValuePopup();
            m_popupValue2 = new DefaultValuePopup();
            m_popupValue1.AllowMultiValue = m_bAllowMultiValue;
            m_popupValue2.AllowMultiValue = m_bAllowMultiValue;
        }

        public bool AllowMultiValue {
            get { return m_bAllowMultiValue; }
            set {
                m_bAllowMultiValue = value;
                m_popupValue1.AllowMultiValue = m_bAllowMultiValue;
                m_popupValue2.AllowMultiValue = m_bAllowMultiValue;
            }
        }

        public bool Checked {
            get { return m_bChecked; }
            set { m_bChecked = value; }
        }

        public FindOperator SelectedOperator {
            get { return m_selectedOperator; }
            set { m_selectedOperator = value; }
        }

        public IValuePopup ValuePopup1 {
            get { return m_popupValue1; }
            set {
                m_popupValue1 = value;
                m_popupValue1.AllowMultiValue = m_bAllowMultiValue;
            }
        }

        public IValuePopup ValuePopup2 {
            get { return m_popupValue2; }
            set {
                m_popupValue2 = value;
                m_popupValue1.AllowMultiValue = m_bAllowMultiValue;
            }
        }

        public string Caption {
            get { return m_strCaption; }
            set { m_strCaption = value; }
        }
        public string Name {
            get { return m_strName; }
            set { m_strName = value; }
        }
        public FindOperatorCollection FindOperators {
            get { return m_findOperators; }
            set { m_findOperators = value; }
        }

        public string FieldMap {
            get { return m_strFieldMap; }
            set { m_strFieldMap = value; }
        }
        public IValueType ValueType1 {
            get { return m_valueType1; }
            set { m_valueType1 = value; }
        }

        public IValueType ValueType2 {
            get { return m_valueType2; }
            set { m_valueType2 = value; }
        }

        public bool EditCheckAble {
            get { return m_bEditCheckAble; }
            set { m_bEditCheckAble = value; }
        }

        public bool EditValueAble {
            get { return this.m_bEditValue; }
            set { m_bEditValue = value; }
        }

        public object[] SelectedValue1 {
            get { return m_valueType1.GetValue(); }
            set { m_valueType1.SetValue(value); }
        }

        public object[] SelectedValue2 {
            get { return m_valueType2.GetValue(); }
            set { m_valueType2.SetValue(value); }
        }       

        public ArrayList ValueList1 {
            get { return m_arValueList1; }
            set { m_arValueList1 = value; }
        }

        public ArrayList ValueList2 {
            get { return m_arValueList2; }
            set { m_arValueList2 = value; }
        }

        public override string ToString() {
            return m_strName;
        }


    }


    public enum FindOptionDirection {
        Down, CrossDown
    }

    public class FindOptionCollection : CollectionBase {

        public FindOption this[int index] {
            get { return (FindOption)List[index]; }
            set { List[index] = value; }
        }

        public FindOption this[string strOptionName] {
            get { return this.Search(strOptionName); }
            set {
                FindOption opt = this.Search(strOptionName);
                opt = value;
            }
        }

        private FindOption Search(string strOptionName) {
            foreach (FindOption opt in List) {
                if (opt.Caption == strOptionName) {
                    return opt;
                }
            }
            return null;
        }

        public int Add(FindOption value) {
            return List.Add(value);
        }

        public FindOption Add(string strName, string strFieldMap) {
            FindOption option = new FindOption(strName, strFieldMap);
            this.Add(option);
            return option;
        }

        public FindOption Add(string strName, string strFieldMap, params FindOperator[] findOperators) {
            FindOption option = new FindOption(strName, strFieldMap, findOperators);
            this.Add(option);
            return option;
        }

        //Apiwat add 2007-10-02
        public FindOption Add(string strName, string strFieldMap, IValueType valueType1, params FindOperator[] findOperators) {
            FindOption option = new FindOption(strName, strFieldMap, valueType1, findOperators);
            this.Add(option);
            return option;
        }
        //End add

        public FindOption Add(string strName, string strFieldMap, IValueType valueType1, IValueType valueType2, params FindOperator[] findOperators) {
            FindOption option = new FindOption(strName, strFieldMap, valueType1, valueType2, findOperators);
            this.Add(option);
            return option;
        }

        public int IndexOf(FindOption value) {
            return List.IndexOf(value);
        }

        public void Insert(int index, FindOption value) {
            List.Insert(index, value);
        }

        public void Remove(FindOption value) {
            List.Remove(value);
        }

        public bool Contains(FindOption value) {
            return List.Contains(value);
        }

        // Add by Teerayut S. on 2012-08-09
        public FindOption FindByFieldMap(string fieldMap)
        {
            FindOption option = null;
            for (int i=0; i<this.Count; i++)
            {
                if (Equals(this[i].FieldMap, fieldMap))
                {
                    option = this[i];
                    break;
                }                
            }

            return option;
        }
    }
}
