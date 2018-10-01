using	System;
using System.Data;
using System.Collections;
using System.Collections.Generic;


namespace EAP.Framework.Data {

	/// <summary>
	/// DataRequest �� class ����˹����������Ѻ��� query �Ѻ database provider �� CommandText, CommandType, Parameters �繵�
	/// </summary>
	public class DataRequest : IDisposable {

	    #region Member

	    private string m_strCommandText = string.Empty;
	    private List<string> m_strResultTableNames = new List<string>();
	    private CommandType m_commandType = CommandType.Text;
	    private ParameterCollection m_parameters = new ParameterCollection();

	    #endregion


	    #region Inner Class

	    /// <summary>
	    /// �� class ������� parameter ����Ѻ��� query �¨��纪��ͧ͢ parameter, ��Ңͧ parameter ��Ъ�Դ�ͧ parameter
	    /// </summary>
	    public class Parameter : IDisposable
	    {
	        private string m_strName = string.Empty;
	        private object m_oValue;
	        private SqlDbType m_iParameterType = SqlDbType.Variant;
	        private ParameterDirection m_parmDirection = ParameterDirection.Input;
	        private int m_iSize = 0;

	        /// <summary>
	        /// Default constructor.
	        /// </summary>
	        public Parameter()
	        {
	        }

	        /// <summary>
	        /// Constructor �ͧ Parameter �¨��Ѻ���ͧ͢ paramter ��Ф�Ңͧ parameter
	        /// </summary>
	        /// <param name="strParamName">���ͧ͢ parameter</param>
	        /// <param name="oValue">��Ңͧ parameter</param>
	        public Parameter(string strParamName, object oValue)
	        {
	            if (oValue is string)
	            {
	                m_iParameterType = SqlDbType.VarChar;
	            }
                else if (oValue is Int16)
                {
                    m_iParameterType = SqlDbType.SmallInt;
                }
                else if (oValue is Int32)
	            {
                    m_iParameterType = SqlDbType.Int;
                }
                else if (oValue is Int64)
                {
                    m_iParameterType = SqlDbType.BigInt;
                }
                else if (oValue is Decimal)
                {
                    m_iParameterType = SqlDbType.Decimal;
                }
                else if (oValue is DateTime)
                {
                    m_iParameterType = SqlDbType.DateTime;
                }

                m_strName = strParamName;
	            m_oValue = oValue;
	        }

	        /// <summary>
	        /// Constructor �ͧ Parameter �¨��Ѻ���ͧ͢ parameter, ��Դ ��Ф�Ңͧ parameter
	        /// ���㹡�ú͡��Դ�ͧ parameter ����դ������繵�ͧ��㹺ҧ����
	        /// </summary>
	        /// <param name="strParamName">���ͧ͢ parameter</param>
	        /// <param name="iParameterType">��Դ�ͧ parameter</param>
	        /// <param name="oValue">��Ңͧ parameter</param>
	        public Parameter(string strParamName, SqlDbType iParameterType, object oValue)
	        {
	            m_strName = strParamName;
	            m_oValue = oValue;
	            m_iParameterType = iParameterType;
	        }

	        public string UdtTypeName { get; set; }

	        /// <summary>
	        /// �кط�ȷҧ�ͧ parameter
	        /// </summary>
	        public ParameterDirection Direction
	        {
	            get { return m_parmDirection; }
	            set { m_parmDirection = value; }
	        }

	        /// <summary>
	        /// get ���� set ���ͧ͢ parameter
	        /// </summary>
	        public string Name
	        {
	            get { return m_strName; }
	            set { m_strName = value; }
	        }

	        /// <summary>
	        /// get ���� set ��Ңͧ parameter
	        /// </summary>
	        public object Value
	        {
	            get { return m_oValue; }
	            set { m_oValue = value; }
	        }

	        public int Size
	        {
	            get { return m_iSize; }
	            set { m_iSize = value; }
	        }

	        /// <summary>
	        /// get ���� set ��Դ�ͧ parameter
	        /// </summary>
	        public SqlDbType ParameterType
	        {
	            get { return m_iParameterType; }
	            set { m_iParameterType = value; }
	        }


	        /// <summary>
	        /// ���º��º parameter �����ҡѹ������� �����º��º�ҡ���ͧ͢ parameter
	        /// </summary>
	        /// <param name="obj">parameter ����ͧ������º��º</param>
	        /// <returns>true ����� parameter �������, false ����� parameter ���е�ǡѹ</returns>
	        public override bool Equals(object obj)
	        {
	            return this.Name == ((Parameter) obj).Name;
	        }

	        public override int GetHashCode()
	        {
	            return base.GetHashCode();
	        }

	        #region IDisposable Members

	        public void Dispose()
	        {
	            GC.SuppressFinalize(this);
	        }

	        #endregion
	    } //Parameter

	    #endregion


		/// <summary>
		/// �� class ����˹�ҷ��㹡���� collection �ͧ parameter
		/// </summary>
		public class ParameterCollection : CollectionBase 
		{
			/// <summary>
			/// Default constructor
			/// </summary>
			public Parameter this[ int index ]  
			{
				get  {	return( (Parameter) List[index] );	}
				set  {	List[index] = value;	}
			}

			/// <summary>
			/// Add parameter ������������ collection.
			/// </summary>
			/// <param name="value"></param>
			/// <returns></returns>
			public int Add( Parameter value )  
			{
				return( List.Add( value ) );
			}

			/// <summary>
			/// Add parameter ������������ collection
			/// </summary>
			/// <param name="strParamName">���ͧ͢ parameter</param>
			/// <param name="value">��Ңͧ parameter</param>
			/// <returns>parameter ��� add</returns>
			public Parameter Add(string strParamName, object value)
			{
				Parameter parm =new Parameter(strParamName, value ?? DBNull.Value);
				List.Add(parm);
				return parm;
			}

			public Parameter Add(string strParamName, SqlDbType sqlDbType, object value){
                Parameter parm = new Parameter(strParamName, sqlDbType, value ?? DBNull.Value);
				List.Add(parm);
				return parm;
			}

			/// <summary>
			/// �ҵ��˹觢ͧ parameter � collection
			/// </summary>
			/// <param name="value">Parameter ����ͧ����ҵ��˹�</param>
			/// <returns>����Ţ���͡���˹觢ͧ parameter � collection</returns>
			public int IndexOf( Parameter value )  
			{
				return( List.IndexOf( value ) );
			}

			/// <summary>
			/// �á parameter 㹵��˹觷���ͧ���
			/// </summary>
			/// <param name="index">���˹觷���ͧ����á parameter</param>
			/// <param name="value">��Ңͧ parameter ����ͧ����á</param>
			public void Insert( int index, Parameter value )  
			{
				List.Insert( index, value );
			}

			/// <summary>
			/// ��� parameter ��Ƿ���ͧ����͡�ҡ collection
			/// </summary>
			/// <param name="value">parameter ����ͧ�������͡�ҡ collection</param>
			public void Remove( Parameter value )  
			{
				List.Remove( value );
			}

			/// <summary>
			/// ��Ǩ�ͺ����� parameter ����ͧ�������� collection �������
			/// </summary>
			/// <param name="value">parameter ����ͧ��õ�Ǩ�ͺ</param>
			/// <returns>true ��� parameter ����� collection, false ��� parameter �������� collection</returns>
			public bool Contains( Parameter value )  
			{
				return( List.Contains( value ) );
			}
		} //ParameterCollection

		public DataRequest(){
		}

		/// <summary>
		/// Construct �ͧ DataRequest �¨��Ѻ command text(query)
		/// </summary>
		/// <param name="strCommandText">CommandText ����Ѻ��� query</param>
		public DataRequest(string strCommandText) 
		{
			m_strCommandText = strCommandText;
		}

		/// <summary>
		/// Constructor �ͧ DataRequest �¨��Ѻ command text ��� command type
		/// </summary>
		/// <param name="strCommandText">Command Text ����Ѻ��� query</param>
		/// <param name="commandType">��Դ�ͧ Command</param>
		public DataRequest(string strCommandText, CommandType commandType) : this(strCommandText) {
			m_commandType = commandType;
		}

		/// <summary>
		/// get ���� set command type
		/// </summary>
		public CommandType CommandType {
			get{	return m_commandType;}
			set{	m_commandType = value;}
		}

		/// <summary>
		/// get ���� set command text
		/// </summary>
		public string CommandText {
			get{	return m_strCommandText;}
			set{	m_strCommandText = value;}
		}
		
		/// <summary>
		/// get ���� set ResultTableName
		/// </summary>
		public List<string> ResultTableNames{
			get{	return m_strResultTableNames;}
			set{	m_strResultTableNames = value;}
		}

		/// <summary>
		/// get ���� set parameter
		/// </summary>
		public ParameterCollection Parameters {
			get{	return m_parameters;}
			set{	m_parameters = value;}
		}        

		/// <summary>
		/// deconstructor
		/// </summary>
		~DataRequest() {
			m_parameters.Clear();
			((IDisposable)this).Dispose();
		}

		#region IDisposable Members

		public void Dispose() {
			GC.SuppressFinalize(this);
		}

		#endregion
	} //DataRequest
}
