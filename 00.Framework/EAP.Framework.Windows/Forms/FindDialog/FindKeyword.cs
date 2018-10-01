using System;
using System.Collections;


namespace EAP.Framework.Windows.Forms
{
	public class FindKeyword {
		private string m_strKeyName = string.Empty;
		private bool m_bChecked = false;
		private string m_strFieldMap = string.Empty;
		private FindOperator m_selectedOperator = FindOperator.Like;
		private object []m_oSelectedValue1;
		private object []m_oSelectedValue2;
		public FindKeyword(string strKeyName, bool bChecked, string strFieldMap, FindOperator selectedOperator, object []oSelectedValue1, object []oSelectedValue2) {
			m_strKeyName = strKeyName;
			m_bChecked = bChecked;
			m_strFieldMap = strFieldMap;
			m_selectedOperator = selectedOperator;
			m_oSelectedValue1 = oSelectedValue1;
			m_oSelectedValue2 = oSelectedValue2;
		}
		public string KeyName{
			get{	return m_strKeyName;}
		}
		public bool Checked{
			get{	return m_bChecked;}
		}
		public string FieldMap{
			get{	return m_strFieldMap;}
		}
		public string FieldMapDbVariable{
			get{
                // Wirachai T. 2008 12 03
                // Modified to support Regular Expression
				//char []extras = new char[]{'[', ']', '.', '+', '\'', ':', '(', ')', '!',' '};
                char[] extras = new char[] { '[', ']', '.', '+', '\'', ':', '(', ')', '!', ' ','^','$','{','}','|',','};
                // End
				string tmp = m_strFieldMap;
				foreach (char c in extras){
					tmp = tmp.Replace(c.ToString(), string.Empty);
				}
                // Wirachai T. 2008 12 03
				if (tmp.Length > 20)
                {
                    tmp = tmp.Substring(0, 20);
                }
                // end

                return tmp;
			}
		}
		public FindOperator SelectedOperator{
			get{	return m_selectedOperator;}
		}
		public object []SelectedValue1{
			get{	return m_oSelectedValue1;}
		}
		public object []SelectedValue2{
			get{	return m_oSelectedValue2;}
		}
		public override bool Equals(object obj) {
			return this.FieldMap == ((FindKeyword)obj).FieldMap;
		}
		public override int GetHashCode() {
			return base.GetHashCode ();
		}
		public override string ToString() {
			return this.FieldMap;
		}

        //####################
        //# 2013-12-12
        //# Add by Teerayut S
        //####################
        public object GetDbValue_SelectedValue1()
        {
            if (!Checked)
                return DBNull.Value;

            return SelectedValue1[0];
        }

        public object GetDbValue_SelectedValue2()
        {
            if (!Checked)
                return DBNull.Value;

            return SelectedValue2[0];
        }


	}

	public class FindKeywordCollection : CollectionBase{
		public FindKeyword this[int index]{
			get{	return (FindKeyword)List[index];}
			set{	List[index] = value;}
		}
		public FindKeyword this[string keyName]{
			get{
				FindKeyword selectKkey = null;
				foreach (FindKeyword key in this.List){
					if (key.KeyName == keyName){
						selectKkey = key;
						break;
					}
				}
				return selectKkey;
			}
		}

        public FindKeyword GetByFieldMap(string fieldMap)
        {
            FindKeyword selectKkey = null;
            foreach (FindKeyword key in this.List)
            {
                if (key.FieldMap == fieldMap)
                {
                    selectKkey = key;
                    break;
                }
            }
            return selectKkey;
        }

        public FindKeyword GetByFielMapDb(string fieldMapDb)
        {
            FindKeyword selectKkey = null;
            foreach (FindKeyword key in this.List)
            {
                if (key.FieldMapDbVariable == fieldMapDb)
                {
                    selectKkey = key;
                    break;
                }
            }
            return selectKkey;
        }

		public int Add(FindKeyword value){
			return List.Add(value);
		}

		public int IndexOf(FindKeyword value){
			return List.IndexOf(value);
		}

		public void Insert(int index, FindKeyword value){
			List.Insert(index, value);
		}

		public void Remove(FindKeyword value){
			List.Remove(value);
		}

		public bool Contains(FindKeyword value){
			return List.Contains(value);
		}
	}
}