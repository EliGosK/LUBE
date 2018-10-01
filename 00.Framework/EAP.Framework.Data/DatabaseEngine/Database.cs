using System;
using System.Data;
using System.Collections.Generic;

namespace EAP.Framework.Data {

	/// <summary>
	/// Database เป็น abstract class ของ database provider ต่าง ๆ ซึ่งในการใช้งาน ผู้ใช้สามารถสร้างตัวแปรที่มีชนิดเป็น Database
	/// และเวลาสร้าง instanct ของตัวแปรที่สร้างขึ้นมา สามารถเรียกใช้ผ่าน class DataController ซึ่งต้องการใช้ database provider ชนิดไหน ก็ทำได้
	/// โดยการป้อน parameter ที่ระบุถึง database provider ที่ต้องการ
	/// </summary>
	/// <example>
	/// Database Database = DataController.CreateFactory(EnumData.SqlDatabase, "connection string for SqlDatabase");
	/// baseFactory.Open();
	/// DataTable dt = baseFactory.ExecuteCommand(new DataRequest("select * from tb_Test"));
	/// baseFactory.Close();
	/// </example>
	public abstract class Database
	{	   

		protected IDbConnection m_IDBConnection = null;
		protected IDbTransaction m_IDBTransaction = null;
		protected string m_strConnectionString = string.Empty;        
        protected bool m_bKeepConnection = false; // 15-01-2007 Sunitsa M.
        
        /// <summary>
        /// 
        /// </summary>
        public virtual int SqlExecuteTimeout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool HasTransaction
        {
            get
            {
                if (m_IDBTransaction != null)
                {
                    return true;
                }
                return false;
            }
        }
        public virtual void BeginTrans()
        {
            if (m_IDBConnection == null || m_IDBConnection.State == ConnectionState.Closed)
            {
                this.Open();
            }
            if (m_IDBTransaction != null)
            {
                throw (new Exception("The last transaction has begin and not sucessfuly \"Commit\" or \"Rollback\""));
            }
            m_IDBTransaction = m_IDBConnection.BeginTransaction();

        }

        public virtual void CommitTrans()
        {
			m_IDBTransaction.Commit();            
            m_IDBTransaction.Dispose();

            // =============================
			m_IDBTransaction = null;
            if (!m_bKeepConnection)
            {
                if (m_IDBConnection != null)
                {
                    this.Close();
                }
            }
		}

        public virtual void RollbackTrans()
        {
			m_IDBTransaction.Rollback();            
            m_IDBTransaction.Dispose();
            // =============================
			m_IDBTransaction = null;
            if (!m_bKeepConnection)
            {
                if (m_IDBConnection != null)
                {
                    this.Close();
                }
            }
		}

		/// <summary>
		/// ทำการ query ตามคำสั่งที่กำหนดไว้ใน Request เพื่อคืนค่า IDataReader
		/// </summary>
		/// <param name="Request">Request : เป็นคำสั่งสำหรับการ query</param>
		/// <returns>ผลลัพธ์จากการ query เป็น IDataReader</returns>
		public abstract IDataReader ExecuteDataReader(DataRequest Request);

		/// <summary>
		/// ทำการ query ตามคำสั่งที่กำหนดไว้ใน Request เพื่อคืนค่า DataSet มา
		/// </summary>
		/// <param name="Request">Request : เป็น array ของคำสั่งสำหรับการ query โดยถ้ามีมากกว่าหนึ่งคำสั่ง ผลลัพทธ์ที่ได้ ก็จะเป็น data set ที่มีหลาย ๆ data table</param>
		/// <returns>ผลลัพธ์จากการ query เป็น DataSet</returns>
		public abstract DataSet ExecuteDataset(params DataRequest []Request);

        ///// <summary>
        ///// ทำการ Fill ข้อมูลลง Dataset ตาม Input ที่ส่งมาให้
        ///// </summary>
        ///// <param name="dsDataSet">Dataset : เป็น DataSet ที่ต้องการนำมา Fill ข้อมูล</param>
        ///// <returns>ผลลัพธ์เป็น DataSet ที่ทำการ fill data แล้ว</returns>
        public abstract bool ExecuteDataset(DataSet dsDataSet, params DataRequest[] Request);

		/// <summary>
		/// ทำการ query ตามคำสั่งที่กำหนดไว้ใน Request เพื่อคืนค่า DataTable มา
		/// </summary>
		/// <param name="Request">Request : เป็นคำสั่งสำหรับการ query</param>
		/// <returns>ผลลัพธ์จากการ query เป็น DataTable</returns>
		public abstract DataTable ExecuteCommand(DataRequest Request);

		/// <summary>
		/// ทำการดึงค่า schema จาก request
		/// </summary>
		/// <param name="Request">เป็นคำสั่ง database ทั่ว ๆ ไป อาจจะเป็น การ query ธรรมดาที่ทำให้ได้ผลลัพธ์ออกมาก</param>
		/// <returns>schema ของคำสั่งที่ส่งไป ซึ่งจะคืนค่ามาเป็น DataSet</returns>
		public abstract DataSet GetSchema(params DataRequest []Request);

		/// <summary>
		/// ทำการ query ตามคำสั่งที่กำหนดไว้ใน Request โดยคำสั่งใน Request จะเป็น query ที่ return ค่าเป็น scalar
		/// </summary>
		/// <param name="Request">Request : เป็นคำสั่งสำหรับการ query โดยเป็นคำสั่งที่ทำให้การ query return ค่าออกมาเป็น scalar</param>
		/// <returns>ผลลัพธ์จากการ query เป็น scalar</returns>
		public abstract object ExecuteScalar(DataRequest Request);

        /// <summary>
        /// ทำการ query ตามคำสั่งที่กำหนดไว้ใน Request โดยคำสั่งใน Request จะเป็น query ที่ return ค่าเป็น scalar
        /// </summary>
        /// <param name="Request">Request : เป็นคำสั่งสำหรับการ query โดยเป็นคำสั่งที่ทำให้การ query return ค่าออกมาเป็น scalar</param>
        /// <param name="iReturnDbType">Return Type</param>
        /// <returns></returns>
        public abstract object ExecuteFunction(DataRequest Request, int iReturnDbType, int iSize =0);

		/// <summary>
		/// ทำการ query ตามคำสั่งที่กำหนดไว้ใน Request แต่จะไม่ return ผลจากการ query โดยจะ return จำนวน record ที่เปลี่ยนแปลงจากคำสั่งในการ query
		/// </summary>
		/// <param name="Request">Request : เป็นคำสั่งสำหรับการ query โดยตัวไปจะเป็นประเภท insert, update, delete เป็นต้น</param>
		/// <returns>ผลลัพธ์จากการ query เป็น integer</returns>
		public abstract int ExecuteNonQuery(DataRequest Request);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
	    public abstract List<T> ExecuteList<T>(DataRequest request);

        /// <summary>
        /// สร้าง connection กับ database provider 
        /// </summary>
        public abstract void Open();

		/// <summary>
		/// ปิด connection กับ database provider 
		/// </summary>
		public abstract void Close();
		
		/// <summary>
		/// เป็นการระบุว่าการทำงานของ BaseFactory เป็นแบบไหน ซึ่งค่าของ IsConnectionLess มีสองแบบคือ
		///	1. ถ้าเป็น true : หมายถึงการ connection กับ database provider จะไม่ถูกสร้างค้างไว้ กล่าวคือ เมื่อมีการสั่งคำสั่ง query ในแต่ละครั้งนั้น 
		///		จะมีการสร้าง connection ขึ้นมาใหม่ และเมื่อ query เสร็จแล้ว connection นั้น ก็จะถูกปิดไปเช่น
		///		BaseFactory baseFactory = DataController.CreateFactory(EnumData.SqlDatabase, "connection string for SqlDatabase");
		///		baseFactory.IsConnectionLess = true;
		///		baseFactory.ExecuteCommand(request1);	// open connection -> query -> close connection
		///		baseFactory.ExecuteCommand(request2);	// open connection -> query -> close connection
		///	2. ถ้าเป็น false : หมายถึงการ connection กับ database provider จะถูกสร้างขึ้นมาครั้งเดียว และ connection นี้จะถูกใช้ตลอดการใช้งาน จะกว่าจะเรียก
		///		คำสั่ง close
		///		BaseFactory baseFactory = DataController.CreateFactory(EnumData.SqlDatabase, "connection string for SqlDatabase");
		///		baseFactory.Open();	//	 open connection
		///		baseFactory.ExecuteCommand(request1);	// query
		///		baseFactory.ExecuteCommand(request2);	// query
		///		baseFactory.Close();	// close connection
		///โดยปกติแล้ว ค่านี้จะเป็น false
		/// </summary>
        
        // 15-01-2007 Sunitsa M.
        public virtual bool KeepConnection
        {
            get { return m_bKeepConnection; }
            set { m_bKeepConnection = value; }
        }

		public virtual IDbConnection Connection{
			get{	return m_IDBConnection;}
		}

		/// <summary>
		/// เมื่อ Query เสร็จแล้ว ถ้า parameter มี direction เป็ฯ output ก็จะมีค่าเก็บไว้ใน value ของ parameter นั้น ๆ ซึ่ง parameter นี้จะขึ้นอยู่กับชนิดของ provider ด้วย ดังนั้น ต้อง copy ค่า
		/// จาก parameter นั้น ๆ มาเก็บใน parameter ของ datarequst เพื่อทำการ return ให้ user สามรรถใช้งานได้ต่อไป
		/// </summary>
		/// <param name="parmSource"></param>
		/// <param name="dataRequest"></param>
		protected void CopyParameterValue(IDataParameterCollection parmSource, DataRequest dataRequest){
			if (parmSource == null || dataRequest == null)
				return;
			for (int i=0; i<parmSource.Count;++i){
				IDataParameter iParm = (IDataParameter)parmSource[i];
				foreach (DataRequest.Parameter p in dataRequest.Parameters){
					if (iParm.ParameterName == p.Name){
						p.Value = iParm.Value;
						break;
					}
				}
			}
		}


	}
}
