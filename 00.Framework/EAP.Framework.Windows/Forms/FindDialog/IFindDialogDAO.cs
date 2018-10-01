using System;
using System.Data;
using EAP.Framework.Data;

namespace EAP.Framework.Windows.Forms {

	#region IFindDialogDAO
	public interface IFindDialogDAO {
		/// <summary>
		/// สร้าง DataRequest จาก Keywords ในลักษณะของ Stored Procedure
		/// </summary>
		/// <param name="keywords"></param>
		/// <returns></returns>
		DataRequest CreateRequestStoreCommand(string strMainSql, FindKeywordCollection keywords);

		/// <summary>
		/// สร้าง DataRequest จาก Keywords ในลักษณะของ Query Text
		/// </summary>
		/// <param name="keywords"></param>
		/// <returns></returns>
		DataRequest CreateRequestTextCommand(string strMainSql,  bool bAlreadyHaveWhereStatement, string strOptionalSql, FindKeywordCollection keywords);

		DataSet GetDataSet(DataRequest req);
	}
	#endregion	

	#region FindDialogSqlDAO
	public class FindDialogSqlDAO : IFindDialogDAO {
		private Database m_db;
		private const string PARM_PREFIX = "@";
		public FindDialogSqlDAO(Database db){
			m_db = db;
		}

		private string GetValueStringFromArray(object []array){
			string strResult = string.Empty;
			for (int i=0;i<array.Length;++i){
				if (array[i] != null){
					strResult += array[i].ToString() + FindDialog.MultiValueSeperator;
				}
			}
			while (strResult.EndsWith(Convert.ToString(FindDialog.MultiValueSeperator))){
				strResult = strResult.Substring(0, strResult.Length-1);
			}
			return strResult;
		}

		public DataRequest CreateRequestStoreCommand(string strMainSql,  FindKeywordCollection keywords) {
			DataRequest req = new DataRequest(strMainSql, CommandType.StoredProcedure);

            if (keywords != null)
            {
                foreach (FindKeyword key in keywords)
                {
                    object value1;
                    object value2;
                    if (key.SelectedOperator == FindOperator.Between)
                    {
// if between operator is selected so multi value can't be used.
                        value1 = DBNull.Value;
                        value2 = DBNull.Value;
                        if (key.Checked)
                        {
                            if (key.SelectedValue1.Length > 0)
                            {
                                value1 = key.SelectedValue1[0];
                            }
                            if (key.SelectedValue2.Length > 0)
                            {
                                value2 = key.SelectedValue2[0];
                            }
                        }

                        req.Parameters.Add(string.Format("{0}{1}", key.FieldMapDbVariable, 1), value1);
                        req.Parameters.Add(string.Format("{0}{1}", key.FieldMapDbVariable, 2), value2);
                    }
                    else
                    {
                        // If user not selected between operator
                        // if type is storeprocedue, i will not care for FineOperator user selected. Because this operation will be operate by stoprocedure in side.
                        // check for multi value
                        if (key.SelectedValue1.Length == 1)
                        {

                            if (key.Checked)
                            {
                                value1 = key.SelectedValue1[0] == null ? DBNull.Value : key.SelectedValue1[0];
                                req.Parameters.Add(key.FieldMapDbVariable, value1);
                            }
                            else
                            {
                                req.Parameters.Add(key.FieldMapDbVariable, DBNull.Value);
                            }
                        }
                        else
                        {
                            if (key.Checked)
                            {
                                req.Parameters.Add(key.FieldMapDbVariable, GetValueStringFromArray(key.SelectedValue1));
                            }
                            else
                            {
                                req.Parameters.Add(key.FieldMapDbVariable, DBNull.Value);
                            }
                        }
                    }
                }
            }
		    return req;
		}

		public DataRequest CreateRequestTextCommand(string strMainSql,  bool bAlreadyHaveWhereStatement, string strOptionalSql, FindKeywordCollection keywords) {
			DataRequest req = new DataRequest(strMainSql, CommandType.Text);
			string result = string.Empty;
			string con;// = " where ";
			if (bAlreadyHaveWhereStatement){
				con = " and ";
			}else{
				con = " where ";
			}

            if (keywords != null)
            {
                foreach (FindKeyword key in keywords)
                {
                    string k1, k2;
                    if (key.Checked == false)
                        continue;
                    if (key.SelectedOperator == FindOperator.Between)
                    {
                        k1 = PARM_PREFIX + key.FieldMapDbVariable + "1";
                        k2 = PARM_PREFIX + key.FieldMapDbVariable + "2";
                        result += con + key.FieldMap + key.SelectedOperator.Symbol + k1 + " and " + k2;



                        req.Parameters.Add(k1, key.SelectedValue1[0]);
                        req.Parameters.Add(k2, key.SelectedValue2[0]);
                        con = " and ";
                    }
                    else
                    {
                        // end if (key.SelectedOperator == FindOperator.Between){
                        if (key.SelectedValue1.Length == 1)
                        {
                            object oValue = key.SelectedValue1[0];
                            // Calculate for Like Operator
                            if (oValue != null)
                            {
                                if (oValue is string && key.SelectedOperator == FindOperator.Like)
                                {
                                    string tmp = Convert.ToString(oValue).Trim();
                                    if (tmp.StartsWith("%") == false && tmp.EndsWith("%") == false)
                                    {
                                        tmp = "%" + tmp + "%";
                                        oValue = tmp;
                                    }
                                }
                            }
                            if (oValue == null)
                            {
                                result += con + key.FieldMap + " is null ";
                                con = " and ";
                            }
                            else
                            {
                                result += con + key.FieldMap + key.SelectedOperator.Symbol + PARM_PREFIX + key.FieldMapDbVariable;
                                req.Parameters.Add(PARM_PREFIX + key.FieldMapDbVariable, oValue);
                                con = " and ";
                            }
                        }
                        else
                        {
                            //if (key.SelectedValue1.Length == 1), In this case user select muliple value, So must use "in" operator.
                            string strFields = string.Empty;
                            for (int i = 0; i < key.SelectedValue1.Length; ++i)
                            {
                                if (key.SelectedValue1[i] != null)
                                {
                                    string field = PARM_PREFIX + key.FieldMapDbVariable + i.ToString();
                                    strFields += field;
                                    req.Parameters.Add(field, key.SelectedValue1[i]);
                                    if (i < key.SelectedValue1.Length - 1)
                                    {
                                        strFields += ", ";
                                    }
                                }
                            }
                            if (key.SelectedOperator == FindOperator.NotEqualTo)
                            {
                                result += con + key.FieldMap + " not in (" + strFields + ")";
                            }
                            else
                            {
                                result += con + key.FieldMap + " in (" + strFields + ")";
                            }

                            con = " and ";
                        }
                    }
                } // end foreach
            }

		    req.CommandText += result + " " + strOptionalSql + " ";
			return req;
		}
		public DataSet GetDataSet(DataRequest req){
			return m_db.ExecuteDataset(req);
		}
	}
	#endregion

}