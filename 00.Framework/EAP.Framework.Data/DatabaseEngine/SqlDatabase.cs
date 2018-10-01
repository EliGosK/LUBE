using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace EAP.Framework.Data {

    /// <summary>
    /// �� class ����׺�ʹ�Ҩҡ BaseFactory ��˹�ҷ��㹡�èѴ��á�õԴ�����л����żŤ��������ǡѺ SqlDatabase
    /// </summary>
    public class SqlData : Database
    {

        #region Constant

        /// <summary>
        /// 
        /// </summary>
        public const int SQL_EXECUTE_TIMEOUT = 120; // in second.

        #endregion

        #region Constructor

        private SqlData()
        {
            base.KeepConnection = true;
            base.SqlExecuteTimeout = SQL_EXECUTE_TIMEOUT;
        }

        public SqlData(string strConnectionString, int timeout = SQL_EXECUTE_TIMEOUT) : this()
        {
            m_strConnectionString = strConnectionString;

            base.KeepConnection = true;
            base.SqlExecuteTimeout = timeout;  // Default when create instance at first.
        }
        
        /// <summary>
        /// Constructor �ͧ SqlFactory �¨��Ѻ Connection String ����Ѻ SqlDatabase �� Parameter ������㹡�����ҧ connection �Ѻ SqlDatabase
        /// </summary>
        /// <param name="strConnectionString">Connection string ����Ѻ��õԴ��͡Ѻ SqlDatabase</param>
        public SqlData(string strConnectionString) : this(strConnectionString, SQL_EXECUTE_TIMEOUT) {
            m_strConnectionString = strConnectionString;
            
            base.KeepConnection = true;            
        }

        /// <summary>
        /// ���ҧ SqlData ���� Connection ������ҧ�ҡ��¹͡
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="timeout"></param>
        /// <remarks>���ҧ���� 2017-04-19</remarks>
        public SqlData(IDbConnection conn, int timeout = SQL_EXECUTE_TIMEOUT) : this()
        {
            // Keep own connection.
            base.m_IDBConnection = conn;

            base.SqlExecuteTimeout = timeout;  // Default when create instance at first.
        }

        #endregion

        /// <summary>
        /// ���ҧ connection �Ѻ SqlDatabase ��� connectring string
        /// </summary>
        public override void Open() {
            if (m_IDBConnection == null)
            {
                m_IDBConnection = new SqlConnection(m_strConnectionString);
                m_IDBConnection.Open();
            }
            else // 2017-04-19 Teerayut S.
            {
                if (m_IDBConnection.State == ConnectionState.Closed)
                {
                    m_IDBConnection.Open();
                }
            }
        }

        /// <summary>
        /// �Դ connection �Ѻ SqlDatabase
        /// </summary>
        public override void Close() {
            if (m_IDBConnection != null) {
                m_IDBConnection.Close();
                m_IDBConnection = null;
            }
        }

        /// <summary>
        /// ���ҧ SqlCommand ��� Request ����˹�
        /// </summary>
        /// <param name="request">�� DataRequest ����Ѻ�纤�������ͷӧҹ�Ѻ SqlDatabase</param>
        /// <returns>SqlCommand</returns>
        private SqlCommand CreateSqlCommand(DataRequest request) {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = request.CommandText;
            cmd.CommandType = request.CommandType;
            cmd.CommandTimeout = SqlExecuteTimeout;

            foreach (DataRequest.Parameter parm in request.Parameters) {
                SqlParameter sp = new SqlParameter();
                sp.ParameterName = parm.Name;
                sp.Value = parm.Value;
                sp.Direction = parm.Direction;
                sp.Size = parm.Size;                
                sp.SqlDbType = parm.ParameterType;
                
                cmd.Parameters.Add(sp);
            }
            if (m_IDBTransaction != null) {
                cmd.Transaction = (SqlTransaction)m_IDBTransaction;
            }
            cmd.Connection = (SqlConnection)m_IDBConnection;
            return cmd;
        }

        #region Execution

        /// <summary>
        /// �ӧҹ��������� Request ��Ф׹����� IDataReader
        /// </summary>
        /// <param name="Request">Request : �� DataRequest ����纤��������Ѻ��÷ӧҹ�Ѻ database</param>
        /// <returns>���Ѿ��ҡ��÷ӧҹ�ͧ����觷���к�� Request �� IDataReader</returns>
        public override IDataReader ExecuteDataReader(DataRequest Request) {
            
            if (m_IDBConnection == null || m_IDBConnection.State == ConnectionState.Closed) {
                this.Open();
            }
            SqlCommand cmd = CreateSqlCommand(Request);

            SqlDataAdapter sqlAdapter = new SqlDataAdapter();
            sqlAdapter.SelectCommand = cmd;            

            try {
                IDataReader result = cmd.ExecuteReader();
                CopyParameterValue(cmd.Parameters, Request);
                if (this.KeepConnection == false && !this.HasTransaction) {
                    this.Close();
                }
                return result;
            }
            catch (Exception ex) {
                Trace.WriteLine(ex.Message);

                ReException(ex);
                return null;
            }
            finally
            {
                try
                {
                    cmd.Dispose();
                    sqlAdapter.Dispose();
                }
                catch
                {
                    // ignored
                }
            }
        }

        /// <summary>
        /// �ӧҹ��������� Request �¤���觨��繻�����������׹��Ңͧ��� query ��Ф׹��Ңͧ�ӹǹ record ����ա������¹�ŧ�ҡ��÷ӧҹ�ͧ����� �蹤���觾ǡ insert, delete, update �繵�
        /// </summary>
        /// <param name="Request">Request : �� DataRequest �����纤��������Ѻ��÷ӧҹ</param>
        /// <returns>�͡�ӹǹ record ����ա������¹�ŧ</returns></returns>
        public override int ExecuteNonQuery(DataRequest Request) {
           
            if (m_IDBConnection == null || m_IDBConnection.State == ConnectionState.Closed) {
                this.Open();
            }
            SqlCommand cmd = CreateSqlCommand(Request);
            try {
                int iResult = cmd.ExecuteNonQuery();
                CopyParameterValue(cmd.Parameters, Request);
                if (this.KeepConnection == false && !this.HasTransaction) {
                    this.Close();
                }
                return iResult;
            }
            catch (Exception ex) {
                Trace.WriteLine(ex.Message);

                ReException(ex);
                return -1;
            }
            finally
            {
                try
                {
                    cmd.Dispose();
                }
                catch
                {
                    // ignored
                }
            }
        }

        /// <summary>
        /// �ӧҹ��������� Request �¤���觨��繻��������׹����繤�� scalar �� select count(*) from tb_DPUsers
        /// </summary>
        /// <param name="Request">Request : �� DataRequest ����кؤ��������Ѻ��÷ӧҹ</param>
        /// <returns>���Ѿ��ҡ��÷ӧҹ�ͧ������� object</returns>
        public override object ExecuteScalar(DataRequest Request) {
            
            if (m_IDBConnection == null || m_IDBConnection.State == ConnectionState.Closed) {
                this.Open();
            }
            SqlCommand cmd = CreateSqlCommand(Request);
            try {
                object oResult = cmd.ExecuteScalar();

                CopyParameterValue(cmd.Parameters, Request);

                if (this.KeepConnection == false && !this.HasTransaction) {
                    this.Close();
                }

                return oResult;
            }
            catch (Exception ex) {
                Trace.WriteLine(ex.Message);

                ReException(ex);
                return null;
            }
            finally
            {
                try
                {
                    cmd.Dispose();
                }
                catch
                {
                    // ignored
                }
            }
        }


        public override object ExecuteFunction(DataRequest Request, int iReturnDbType,int iSize = 0 ) {
            throw new Exception("not implemented");
        }

        /// <summary>
        /// �繡�÷ӧҹ�������觷���к� �¨Ф׹����͡���� DataTable
        /// </summary>
        /// <param name="Request">Request : �� DataRequest ����кؤ��������Ѻ��÷ӧҹ</param>
        /// <returns>���Ѿ��ҡ��÷ӧҹ�ͧ������� DataTable</returns>
        public override DataTable ExecuteCommand(DataRequest Request) {
            
            if (m_IDBConnection == null || m_IDBConnection.State == ConnectionState.Closed) {
                this.Open();
            }
            SqlCommand cmd = CreateSqlCommand(Request);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter();
            sqlAdapter.SelectCommand = cmd;
            try {
                DataTable dt = new DataTable();
                sqlAdapter.Fill(dt);
                CopyParameterValue(cmd.Parameters, Request);
                if (this.KeepConnection == false && !this.HasTransaction) {
                    this.Close();
                }
                return dt;
            }
            catch (Exception ex) {
                Trace.WriteLine(ex.Message);

                ReException(ex);
                return null;
            }
            finally
            {
                try
                {
                    cmd.Dispose();
                    sqlAdapter.Dispose();
                }
                catch
                {
                    // ignored
                }
            }
        }

        /// <summary>
        /// �ӧҹ�������觷���к� �¨Ф׹����� DataSet ��� DataSet ����Ҩ�������� � DataTable ���� �������Өӹǹ�ͧ����觷���к�� parameter
        /// </summary>
        /// <param name="Request">Request : �� DataRequest ����кؤ��������Ѻ��÷ӧҹ ����Ҩ�������ҡ����˹��</param>
        /// <returns>���Ѿ��ҡ��÷ӧҹ�ͧ������� DataSet</returns>
        public override DataSet ExecuteDataset(params DataRequest[] Request) {
           
            if (m_IDBConnection == null || m_IDBConnection.State == ConnectionState.Closed) {
                this.Open();
            }
            DataSet dsResult = new DataSet();
            int intPreTableCount = 0;
            foreach (DataRequest req in Request) {
                SqlCommand cmd = CreateSqlCommand(req);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                sqlAdapter.SelectCommand = cmd;
                try {
                    sqlAdapter.Fill(dsResult);
                    CopyParameterValue(cmd.Parameters, req);
                    for (int i = 0; i < dsResult.Tables.Count - intPreTableCount; i++) {
                        if (req.ResultTableNames != null && req.ResultTableNames.Count > i) {
                            dsResult.Tables[intPreTableCount + i].TableName = req.ResultTableNames[i];
                        }
                    }
                    intPreTableCount = dsResult.Tables.Count;
                }
                catch (Exception ex) {
                    Trace.WriteLine(ex.Message);

                    ReException(ex);
                    return null;
                }
                finally
                {
                    try
                    {
                        cmd.Dispose();
                        sqlAdapter.Dispose();
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
            if (this.KeepConnection == false && !this.HasTransaction) {
                this.Close();
            }
            return dsResult;
        }

        public override DataSet GetSchema(params DataRequest[] Request) {
            
            if (m_IDBConnection == null || m_IDBConnection.State == ConnectionState.Closed) {
                this.Open();
            }

            DataSet dsResult = new DataSet();
            int intPreTableCount = 0;
            foreach (DataRequest req in Request) {
                SqlCommand cmd = CreateSqlCommand(req);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                sqlAdapter.SelectCommand = cmd;
                try {
                    sqlAdapter.FillSchema(dsResult, SchemaType.Source);
                    CopyParameterValue(cmd.Parameters, req);

                    for (int i = 0; i < dsResult.Tables.Count - intPreTableCount; i++) {
                        if (req.ResultTableNames != null && req.ResultTableNames.Count > i) {
                            dsResult.Tables[intPreTableCount + i].TableName = req.ResultTableNames[i];
                        }
                    }
                    intPreTableCount = dsResult.Tables.Count;
                }
                catch { }
            }
            if (this.KeepConnection == false && !this.HasTransaction) {
                this.Close();
            }
            return dsResult;
        }
        public override bool ExecuteDataset(DataSet dsDataSet, params DataRequest[] Request) {            
            if (m_IDBConnection == null || m_IDBConnection.State == ConnectionState.Closed) {
                this.Open();
            }
            int intPreTableCount = 0;
            foreach (DataRequest req in Request)
            {
                SqlCommand cmd = CreateSqlCommand(req);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                sqlAdapter.SelectCommand = cmd;
                try {
                    sqlAdapter.Fill(dsDataSet);
                    CopyParameterValue(cmd.Parameters, req);
                    for (int i = 0; i < dsDataSet.Tables.Count - intPreTableCount; i++) {
                        if (req.ResultTableNames != null && req.ResultTableNames.Count > i) {
                            dsDataSet.Tables[intPreTableCount + i].TableName = req.ResultTableNames[i];
                        }
                    }
                    
                    intPreTableCount = dsDataSet.Tables.Count;
                }
                catch (Exception ex) {
                    Trace.WriteLine(ex.Message);

                    ReException(ex);
                    
                }
                finally
                {
                    try
                    {
                        cmd.Dispose();
                        sqlAdapter.Dispose();
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
            if (this.KeepConnection == false && !this.HasTransaction) {
                this.Close();
            }
            return true;
        }

        public override List<T> ExecuteList<T>(DataRequest request)
        {
            using (IDataReader reader = ExecuteDataReader(request))
            {
                return DataUtil.ConvertDataReaderToList<T>(reader);
            }
        }

        #endregion

        public void ReException(Exception ex)
        {
            if (!(ex is SqlException))
            {
                throw ex;
            }

            SqlException sqlEx = (SqlException) ex;
            string sqlMessage = sqlEx.Message;
            if (sqlMessage.Length > 7 && sqlMessage[sqlMessage.Length - 1] == '#')
            {
                string[] strs = sqlMessage.Substring(0, sqlMessage.Length - 1).Split(',');
                string[] strParams = new string[strs.Length - 1];
                if (strs.Length > 1)
                {
                    for (int idx = 1; idx < strs.Length; idx++)
                    {
                        strParams[idx - 1] = strs[idx];
                    }
                }

                throw new BusinessException(strs[0], strParams);
            }
            else
            {
                throw ex;
            }
            
        }
    }//SqlFactory
}

