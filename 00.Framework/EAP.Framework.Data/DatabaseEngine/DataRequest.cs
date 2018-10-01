using	System;
using System.Data;
using System.Collections;
using System.Collections.Generic;


namespace EAP.Framework.Data {

	/// <summary>
	/// DataRequest เป็น class ที่กำหนดคำสั่งสำหรับการ query กับ database provider เช่น CommandText, CommandType, Parameters เป็นต้น
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
	    /// เป็น class ที่ใช้เก็บ parameter สำหรับการ query โดยจะเก็บชื่อของ parameter, ค่าของ parameter และชนิดของ parameter
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
	        /// Constructor ของ Parameter โดยจะรับชื่อของ paramter และค่าของ parameter
	        /// </summary>
	        /// <param name="strParamName">ชื่อของ parameter</param>
	        /// <param name="oValue">ค่าของ parameter</param>
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
	        /// Constructor ของ Parameter โดยจะรับชื่อของ parameter, ชนิด และค่าของ parameter
	        /// ซึ่งในการบอกชนิดของ parameter นั้นมีความจำเป็นต้องใช้ในบางครั้ง
	        /// </summary>
	        /// <param name="strParamName">ชื่อของ parameter</param>
	        /// <param name="iParameterType">ชนิดของ parameter</param>
	        /// <param name="oValue">ค่าของ parameter</param>
	        public Parameter(string strParamName, SqlDbType iParameterType, object oValue)
	        {
	            m_strName = strParamName;
	            m_oValue = oValue;
	            m_iParameterType = iParameterType;
	        }

	        public string UdtTypeName { get; set; }

	        /// <summary>
	        /// ระบุทิศทางของ parameter
	        /// </summary>
	        public ParameterDirection Direction
	        {
	            get { return m_parmDirection; }
	            set { m_parmDirection = value; }
	        }

	        /// <summary>
	        /// get หรือ set ชื่อของ parameter
	        /// </summary>
	        public string Name
	        {
	            get { return m_strName; }
	            set { m_strName = value; }
	        }

	        /// <summary>
	        /// get หรือ set ค่าของ parameter
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
	        /// get หรือ set ชนิดของ parameter
	        /// </summary>
	        public SqlDbType ParameterType
	        {
	            get { return m_iParameterType; }
	            set { m_iParameterType = value; }
	        }


	        /// <summary>
	        /// เปรียบเทียบ parameter ว่าเท่ากันหรือไม่ โดยเปรียบเทียบจากชื่อของ parameter
	        /// </summary>
	        /// <param name="obj">parameter ที่ต้องการเปรียบเทียบ</param>
	        /// <returns>true ถ้าเป็น parameter ตัวเดียว, false ถ้าเป็น parameter คนละตัวกัน</returns>
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
		/// เป็น class ที่ทำหน้าที่ในการเก็บ collection ของ parameter
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
			/// Add parameter ตัวใหม่เข้าไปใน collection.
			/// </summary>
			/// <param name="value"></param>
			/// <returns></returns>
			public int Add( Parameter value )  
			{
				return( List.Add( value ) );
			}

			/// <summary>
			/// Add parameter ตัวใหม่เข้าไปใน collection
			/// </summary>
			/// <param name="strParamName">ชื่อของ parameter</param>
			/// <param name="value">ค่าของ parameter</param>
			/// <returns>parameter ที่ add</returns>
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
			/// หาตำแหน่งของ parameter ใน collection
			/// </summary>
			/// <param name="value">Parameter ที่ต้องการหาตำแหน่ง</param>
			/// <returns>ตัวเลขที่บอกตำแหน่งของ parameter ใน collection</returns>
			public int IndexOf( Parameter value )  
			{
				return( List.IndexOf( value ) );
			}

			/// <summary>
			/// แทรก parameter ในตำแหน่งที่ต้องการ
			/// </summary>
			/// <param name="index">ตำแหน่งที่ต้องการแทรก parameter</param>
			/// <param name="value">ค่าของ parameter ที่ต้องการแทรก</param>
			public void Insert( int index, Parameter value )  
			{
				List.Insert( index, value );
			}

			/// <summary>
			/// เอา parameter ตัวที่ต้องการออกจาก collection
			/// </summary>
			/// <param name="value">parameter ที่ต้องการเอาออกจาก collection</param>
			public void Remove( Parameter value )  
			{
				List.Remove( value );
			}

			/// <summary>
			/// ตรวจสอบว่ามี parameter ที่ต้องการอยู่ใน collection หรือไม่
			/// </summary>
			/// <param name="value">parameter ที่ต้องการตรวจสอบ</param>
			/// <returns>true ถ้า parameter อยู่ใน collection, false ถ้า parameter ไม่อยู่ใน collection</returns>
			public bool Contains( Parameter value )  
			{
				return( List.Contains( value ) );
			}
		} //ParameterCollection

		public DataRequest(){
		}

		/// <summary>
		/// Construct ของ DataRequest โดยจะรับ command text(query)
		/// </summary>
		/// <param name="strCommandText">CommandText สำหรับการ query</param>
		public DataRequest(string strCommandText) 
		{
			m_strCommandText = strCommandText;
		}

		/// <summary>
		/// Constructor ของ DataRequest โดยจะรับ command text และ command type
		/// </summary>
		/// <param name="strCommandText">Command Text สำหรับการ query</param>
		/// <param name="commandType">ชนิดของ Command</param>
		public DataRequest(string strCommandText, CommandType commandType) : this(strCommandText) {
			m_commandType = commandType;
		}

		/// <summary>
		/// get หรือ set command type
		/// </summary>
		public CommandType CommandType {
			get{	return m_commandType;}
			set{	m_commandType = value;}
		}

		/// <summary>
		/// get หรือ set command text
		/// </summary>
		public string CommandText {
			get{	return m_strCommandText;}
			set{	m_strCommandText = value;}
		}
		
		/// <summary>
		/// get หรือ set ResultTableName
		/// </summary>
		public List<string> ResultTableNames{
			get{	return m_strResultTableNames;}
			set{	m_strResultTableNames = value;}
		}

		/// <summary>
		/// get หรือ set parameter
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
