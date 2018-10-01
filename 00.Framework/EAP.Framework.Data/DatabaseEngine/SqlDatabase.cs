using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace EAP.Framework.Data {

    /// <summary>
    /// เป็น class ที่สืบทอดมาจาก BaseFactory มีหน้าที่ในการจัดการการติดต่อและประมวลผลคำสั่งเกี่ยวกับ SqlDatabase
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
        /// Constructor ของ SqlFactory โดยจะรับ Connection String สำหรับ SqlDatabase เป็น Parameter เพื่อใช้ในการสร้าง connection กับ SqlDatabase
        /// </summary>
        /// <param name="strConnectionString">Connection string สำหรับการติดต่อกับ SqlDatabase</param>
        public SqlData(string strConnectionString) : this(strConnectionString, SQL_EXECUTE_TIMEOUT) {
            m_strConnectionString = strConnectionString;
            
            base.KeepConnection = true;            
        }

        /// <summary>
        /// สร้าง SqlData ด้วย Connection ที่สร้างจากภายนอก
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="timeout"></param>
        /// <remarks>สร้างเพิ่ม 2017-04-19</remarks>
        public SqlData(IDbConnection conn, int timeout = SQL_EXECUTE_TIMEOUT) : this()
        {
            // Keep own connection.
            base.m_IDBConnection = conn;

            base.SqlExecuteTimeout = timeout;  // Default when create instance at first.
        }

        #endregion

        /// <summary>
        /// สร้าง connection กับ SqlDatabase ตาม connectring string
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
        /// ปิด connection กับ SqlDatabase
        /// </summary>
        public override void Close() {
            if (m_IDBConnection != null) {
                m_IDBConnection.Close();
                m_IDBConnection = null;
            }
        }

        /// <summary>
        /// สร้าง SqlCommand ตาม Request ที่กำหนด
        /// </summary>
        /// <param name="request">เป็น DataRequest สำหรับเก็บคำสั่งเพื่อทำงานกับ SqlDatabase</param>
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
        /// ทำงานตามคำสั่งใน Request และคืนค่าเป็น IDataReader
        /// </summary>
        /// <param name="Request">Request : เป็น DataRequest ที่เก็บคำสั่งสำหรับการทำงานกับ database</param>
        /// <returns>ผลลัพธ์จากการทำงานของคำสั่งที่ระบุใน Request เป็น IDataReader</returns>
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
        /// ทำงานตามคำสั่งใน Request โดยคำสั่งจะเป็นประเภทที่ไม่คืนค่าของการ query แต่จะคืนค่าของจำนวน record ที่มีการเปลี่ยนแปลงจากการทำงานของคำสั่ง เช่นคำสั่งพวก insert, delete, update เป็นต้น
        /// </summary>
        /// <param name="Request">Request : เป็น DataRequest เพื่อเก็บคำสั่งสำหรับการทำงาน</param>
        /// <returns>บอกจำนวน record ที่มีการเปลี่ยนแปลง</returns></returns>
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
        /// ทำงานตามคำสั่งใน Request โดยคำสั่งจะเป็นประเภทที่คืนค่าเป็นค่า scalar เช่น select count(*) from tb_DPUsers
        /// </summary>
        /// <param name="Request">Request : เป็น DataRequest ที่ระบุคำสั่งสำหรับการทำงาน</param>
        /// <returns>ผลลัพธ์จากการทำงานของคำสั่งเป็น object</returns>
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
        /// เป็นการทำงานตามคำสั่งที่ระบุ โดยจะคืนค่าออกมาเป็น DataTable
        /// </summary>
        /// <param name="Request">Request : เป็น DataRequest ที่ระบุคำสั่งสำหรับการทำงาน</param>
        /// <returns>ผลลัพธ์จากการทำงานของคำสั่งเป็น DataTable</returns>
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
        /// ทำงานตามคำสั่งที่ระบุ โดยจะคืนค่าเป็น DataSet โดยใน DataSet นี้อาจจะมีหลาย ๆ DataTable ก็ได้ ขึ้นอยู่กำจำนวนของคำสั่งที่ระบุใน parameter
        /// </summary>
        /// <param name="Request">Request : เป็น DataRequest ที่ระบุคำสั่งสำหรับการทำงาน ซึ่งอาจจะมีได้มากกว่าหนึ่ง</param>
        /// <returns>ผลลัพธ์จากการทำงานของคำสั่งเป็น DataSet</returns>
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

