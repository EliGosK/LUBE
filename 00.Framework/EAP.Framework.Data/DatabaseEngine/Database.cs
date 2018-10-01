using System;
using System.Data;
using System.Collections.Generic;

namespace EAP.Framework.Data {

	/// <summary>
	/// Database �� abstract class �ͧ database provider ��ҧ � ���㹡����ҹ ���������ö���ҧ����÷���ժ�Դ�� Database
	/// ����������ҧ instanct �ͧ����÷�����ҧ����� ����ö���¡���ҹ class DataController ��觵�ͧ����� database provider ��Դ�˹ �����
	/// �¡�û�͹ parameter ����кض֧ database provider ����ͧ���
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
		/// �ӡ�� query �������觷���˹����� Request ���ͤ׹��� IDataReader
		/// </summary>
		/// <param name="Request">Request : �繤��������Ѻ��� query</param>
		/// <returns>���Ѿ��ҡ��� query �� IDataReader</returns>
		public abstract IDataReader ExecuteDataReader(DataRequest Request);

		/// <summary>
		/// �ӡ�� query �������觷���˹����� Request ���ͤ׹��� DataSet ��
		/// </summary>
		/// <param name="Request">Request : �� array �ͧ���������Ѻ��� query �¶�����ҡ����˹�觤���� ���Ѿ������� ����� data set ��������� � data table</param>
		/// <returns>���Ѿ��ҡ��� query �� DataSet</returns>
		public abstract DataSet ExecuteDataset(params DataRequest []Request);

        ///// <summary>
        ///// �ӡ�� Fill ������ŧ Dataset ��� Input ����������
        ///// </summary>
        ///// <param name="dsDataSet">Dataset : �� DataSet ����ͧ��ù��� Fill ������</param>
        ///// <returns>���Ѿ���� DataSet ���ӡ�� fill data ����</returns>
        public abstract bool ExecuteDataset(DataSet dsDataSet, params DataRequest[] Request);

		/// <summary>
		/// �ӡ�� query �������觷���˹����� Request ���ͤ׹��� DataTable ��
		/// </summary>
		/// <param name="Request">Request : �繤��������Ѻ��� query</param>
		/// <returns>���Ѿ��ҡ��� query �� DataTable</returns>
		public abstract DataTable ExecuteCommand(DataRequest Request);

		/// <summary>
		/// �ӡ�ô֧��� schema �ҡ request
		/// </summary>
		/// <param name="Request">�繤���� database ���� � � �Ҩ���� ��� query �����ҷ����������Ѿ���͡�ҡ</param>
		/// <returns>schema �ͧ����觷����� ��觨Ф׹������� DataSet</returns>
		public abstract DataSet GetSchema(params DataRequest []Request);

		/// <summary>
		/// �ӡ�� query �������觷���˹����� Request �¤����� Request ���� query ��� return ����� scalar
		/// </summary>
		/// <param name="Request">Request : �繤��������Ѻ��� query ���繤���觷�������� query return ����͡���� scalar</param>
		/// <returns>���Ѿ��ҡ��� query �� scalar</returns>
		public abstract object ExecuteScalar(DataRequest Request);

        /// <summary>
        /// �ӡ�� query �������觷���˹����� Request �¤����� Request ���� query ��� return ����� scalar
        /// </summary>
        /// <param name="Request">Request : �繤��������Ѻ��� query ���繤���觷�������� query return ����͡���� scalar</param>
        /// <param name="iReturnDbType">Return Type</param>
        /// <returns></returns>
        public abstract object ExecuteFunction(DataRequest Request, int iReturnDbType, int iSize =0);

		/// <summary>
		/// �ӡ�� query �������觷���˹����� Request ������ return �Ũҡ��� query �¨� return �ӹǹ record �������¹�ŧ�ҡ�����㹡�� query
		/// </summary>
		/// <param name="Request">Request : �繤��������Ѻ��� query �µ��仨��繻����� insert, update, delete �繵�</param>
		/// <returns>���Ѿ��ҡ��� query �� integer</returns>
		public abstract int ExecuteNonQuery(DataRequest Request);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
	    public abstract List<T> ExecuteList<T>(DataRequest request);

        /// <summary>
        /// ���ҧ connection �Ѻ database provider 
        /// </summary>
        public abstract void Open();

		/// <summary>
		/// �Դ connection �Ѻ database provider 
		/// </summary>
		public abstract void Close();
		
		/// <summary>
		/// �繡���к���ҡ�÷ӧҹ�ͧ BaseFactory ��Ẻ�˹ ��觤�Ңͧ IsConnectionLess ���ͧẺ���
		///	1. ����� true : ���¶֧��� connection �Ѻ database provider �����١���ҧ��ҧ��� ����Ǥ�� ������ա����觤���� query ����Ф��駹�� 
		///		���ա�����ҧ connection ��������� �������� query �������� connection ��� ��ж١�Դ���
		///		BaseFactory baseFactory = DataController.CreateFactory(EnumData.SqlDatabase, "connection string for SqlDatabase");
		///		baseFactory.IsConnectionLess = true;
		///		baseFactory.ExecuteCommand(request1);	// open connection -> query -> close connection
		///		baseFactory.ExecuteCommand(request2);	// open connection -> query -> close connection
		///	2. ����� false : ���¶֧��� connection �Ѻ database provider �ж١���ҧ����Ҥ������� ��� connection ���ж١���ʹ�����ҹ �С��Ҩ����¡
		///		����� close
		///		BaseFactory baseFactory = DataController.CreateFactory(EnumData.SqlDatabase, "connection string for SqlDatabase");
		///		baseFactory.Open();	//	 open connection
		///		baseFactory.ExecuteCommand(request1);	// query
		///		baseFactory.ExecuteCommand(request2);	// query
		///		baseFactory.Close();	// close connection
		///�»������� ��ҹ����� false
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
		/// ����� Query �������� ��� parameter �� direction ��� output ����դ�������� value �ͧ parameter ��� � ��� parameter ���Т������Ѻ��Դ�ͧ provider ���� �ѧ��� ��ͧ copy ���
		/// �ҡ parameter ��� � ����� parameter �ͧ datarequst ���ͷӡ�� return ��� user ����ö��ҹ�����
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
