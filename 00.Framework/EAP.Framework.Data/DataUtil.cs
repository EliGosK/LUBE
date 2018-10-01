using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Management;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace EAP.Framework.Data
{
    public class DataUtil
    {
        #region Member
        private static TripleDES alg;
        private static byte[] key; 
        #endregion

        #region Member - Crypto

        private const int KEY_SIZE = 128;
        private const string PRIVATE_KEY = "999";

        private const int BLOCK_SIZE = 256;
        private const string INITIAL_VECTOR = "zzz";

        #endregion


        static DataUtil()
        {
            key = new byte[] { 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48 };
            alg = TripleDES.Create();
            alg.Key = key;
            alg.Mode = CipherMode.ECB;
        }
        
        /// <summary>
        /// Combine Date + Time
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime CombineDateAndTime(DateTime date, DateTime time)
        {
            DateTime combinedDatetime = date.Date;
            return combinedDatetime.Add(time.TimeOfDay);
        }
        
        public static string ToHtmlColorCode(Color color)
        {
            return string.Format("{0}{1}{2}", color.R.ToString("X2"), color.G.ToString("X2"), color.B.ToString("X2"));
        }

        public static string SHA1Encrypt(string password)
        {
            System.Security.Cryptography.SHA1 enc = System.Security.Cryptography.SHA1.Create();
            byte[] hashVal = enc.ComputeHash(Encoding.UTF8.GetBytes(password));
            return System.Convert.ToBase64String(hashVal);
        }

        public static bool IsValidDecimal(string decimalString, int dbPrecision, int dbScale)
        {
            decimal result;
            if (Decimal.TryParse(decimalString, out result))
            {
                uint[] bits = (uint[])(object)decimal.GetBits(result);


                decimal mantissa =
                    (bits[2] * 4294967296m * 4294967296m) +
                    (bits[1] * 4294967296m) +
                    bits[0];

                uint scale = (bits[3] >> 16) & 31;
                uint precision = 0;
                if (result != 0m)
                {
                    for (decimal tmp = mantissa; tmp >= 1; tmp /= 10)
                    {
                        precision++;
                    }
                }
                else
                {
                    precision = scale + 1;
                }

                uint trailingZeros = 0;
                for (decimal tmp = mantissa;
                     tmp % 10m == 0 && trailingZeros < scale;
                     tmp /= 10)
                {
                    trailingZeros++;
                }

                if (precision - trailingZeros - (scale - trailingZeros) + 3 <= dbPrecision && scale - trailingZeros <= dbScale)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public static bool IsTime(string timeString)
        {
            const int HOUR = 0;
            const int MINUTE = 0;
            string[] timePart = timeString.Split(':');
            int result;
            if (Int32.TryParse(timePart[HOUR], out result))
            {
                if (result < 0 || result > 23)
                    return false;
            }
            else
                return false;
            if (Int32.TryParse(timePart[MINUTE], out result))
            {
                if (result < 0 || result > 59)
                    return false;
            }
            else
                return false;

            return true;

        }

        public static bool IsDate(string DateString)
        {
            bool isdate = true;
            try
            {
                string[] arrDate = DateString.Split('/');
                DateTime dt = DateTime.Parse(string.Format("{2}/{1}/{0}", arrDate[0], arrDate[1], arrDate[2]));
            }
            catch (Exception ex)
            {
                isdate = false;
            }

            return isdate;
        }

        //public static DateTime ConvertToDate(string DateString)
        //{
        //    try
        //    {
        //        string[] arrDate = DateString.Split('/');
        //        return DateTime.Parse(string.Format("{2}/{1}/{0}", arrDate[0], arrDate[1], arrDate[2]));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}


        #region Symmetric Cryptography (Triple DES)

        public static string SymmetricDecrypt(string input)
        {
#if DEBUG
            try
            {
#endif
                byte[] clearData;
                string result;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(System.Convert.FromBase64String(input), 0, System.Convert.FromBase64String(input).Length);
                        cs.FlushFinalBlock();
                        clearData = ms.ToArray();
                        ms.Close();
                        cs.Close();
                        result = Encoding.UTF8.GetString(clearData);
                    }
                }

                return result;
#if DEBUG
            }
            catch
            {
                return input;
            }
#endif                
        }

        public static string SymmetricEncrypt(string input)
        {
            byte[] clearData = Encoding.UTF8.GetBytes(input);
            string result;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearData, 0, clearData.Length);
                    cs.FlushFinalBlock();
                    byte[] CipherBytes = ms.ToArray();
                    ms.Close();
                    cs.Close();
                    result = System.Convert.ToBase64String(CipherBytes);
                }
            }
            return result;
        }


        /// <summary>
        /// เข้ารหัสด้วย TripleDES
        /// </summary>
        /// <param name="keyHash">Key ที่ใช้เข้ารหัส</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string SymmetricEncrypt(string keyHash, string input)
        {
            byte[] byteInput = Encoding.ASCII.GetBytes(input);
            string result;
            using (MemoryStream ms = new MemoryStream())
            {
                using (TripleDES tripleDES = TripleDES.Create())
                {
                    tripleDES.Key = Encoding.ASCII.GetBytes((keyHash + "!@#$%^&*()_+|").PadRight(24, '฿').Substring(0, 24));
                    tripleDES.Mode = CipherMode.ECB;

                    using (CryptoStream cs = new CryptoStream(ms, tripleDES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(byteInput, 0, byteInput.Length);
                        cs.FlushFinalBlock();

                        byte[] CipherBytes = ms.ToArray();
                        ms.Close();
                        cs.Close();
                        result = System.Convert.ToBase64String(CipherBytes);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// ถอดรหัสด้วย TripleDEC
        /// </summary>
        /// <param name="keyHash">Key ที่ใช้ถอดรหัส</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string SymmetricDecrypt(string keyHash, string input)
        {
            byte[] clearData;
            string result;
            using (MemoryStream ms = new MemoryStream())
            {
                using (TripleDES tripleDES = TripleDES.Create())
                {
                    tripleDES.Key = Encoding.ASCII.GetBytes((keyHash + "!@#$%^&*()_+|").PadRight(24, '฿').Substring(0, 24));
                    tripleDES.Mode = CipherMode.ECB;

                    using (CryptoStream cs = new CryptoStream(ms, tripleDES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        byte[] byteInput = System.Convert.FromBase64String(input);
                        cs.Write(byteInput, 0, byteInput.Length);
                        cs.FlushFinalBlock();
                        clearData = ms.ToArray();

                        ms.Close();
                        cs.Close();
                        result = Encoding.ASCII.GetString(clearData);
                    }
                }
            }
            return result;
        }

        #endregion

        #region Symmetric Cryptography (Rijndael)

        private static byte[] GetKey
        {
            get
            {
                int thisSize = (KEY_SIZE / 8) - 1;
                int temp;
                byte[] thisKey = new byte[thisSize + 1];

                if (PRIVATE_KEY.Length < 1)
                {
                    return thisKey;
                }
                int lastBound = PRIVATE_KEY.Length;
                if (lastBound > thisSize)
                {
                    lastBound = thisSize;
                }
                for (temp = 0; temp < lastBound; ++temp)
                {
                    thisKey[temp] = Convert.ToByte(PRIVATE_KEY[temp]);
                }
                return thisKey;
            }
        }

        private static byte[] GetIV
        {
            get
            {
                // Convert Bits to Bytes
                int thisSize = (BLOCK_SIZE / 8) - 1;
                byte[] thisIV = new byte[thisSize + 1];
                if (INITIAL_VECTOR.Length < 1)
                {
                    return thisIV;
                }
                int lastBound = INITIAL_VECTOR.Length;
                if (lastBound > thisSize)
                {
                    lastBound = thisSize;
                }
                for (int i = 0; i < lastBound; ++i)
                {
                    thisIV[i] = Convert.ToByte(INITIAL_VECTOR[i]);
                }
                return thisIV;
            }
        }

        public static string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            try
            {
                ICryptoTransform encryptor = null;
                using (SymmetricAlgorithm crypt = SymmetricAlgorithm.Create("Rijndael"))
                {
                    crypt.BlockSize = BLOCK_SIZE;
                    crypt.KeySize = KEY_SIZE;
                    encryptor = crypt.CreateEncryptor(GetKey, GetIV);
                }

                byte[] mBytes = null;
                using (MemoryStream mem = new MemoryStream())
                {
                    using (encryptor)
                    {
                        using (CryptoStream cryptStr = new CryptoStream(mem, encryptor, CryptoStreamMode.Write))
                        {
                            byte[] valueBytes = Encoding.ASCII.GetBytes(value);
                            cryptStr.Write(valueBytes, 0, valueBytes.Length);                            
                            cryptStr.FlushFinalBlock();
                            
                            mem.Position = 0;                            
                            mBytes = mem.ToArray();
                        }
                    }
                }                
                
                string result = Convert.ToBase64String(mBytes);                
                return result;

            }
            catch
            {
                // If cannot encrypt will return original text.
                return value;
            }

        }

        public static string Decrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            try
            {
                string result = "";

                ICryptoTransform mDecrypt = null;

                using (SymmetricAlgorithm crypt = SymmetricAlgorithm.Create("Rijndael"))
                {
                    crypt.BlockSize = BLOCK_SIZE;
                    crypt.KeySize = KEY_SIZE;
                    mDecrypt = crypt.CreateDecryptor(GetKey, GetIV);
                }

                using (mDecrypt)
                {
                    byte[] buff = Convert.FromBase64String(value);
                    using (MemoryStream mem = new MemoryStream(buff))
                    {
                        mem.Position = 0;

                        using (CryptoStream mCSReader = new CryptoStream(mem, mDecrypt, CryptoStreamMode.Read))
                        {

                            using (StreamReader sr = new StreamReader(mCSReader))
                            {
                                result = value;
                                try
                                {
                                    result = sr.ReadToEnd();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.ToString());
                                }
                            }
                        }
                    }
                }                                

                return result;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.ToString());
                return value;
#else
                throw;
#endif
            }
        }

#endregion


        /// <summary>
        /// Encrypt MD5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string input)
        {
            try
            {
                byte[] data = null;
                using (MD5 enc = System.Security.Cryptography.MD5.Create())
                {
                    data = enc.ComputeHash(Encoding.UTF8.GetBytes(input));
                }


                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sb.AppendFormat("{0:X2}", data[i]);
                }
                return sb.ToString();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get mainboard serial number
        /// </summary>
        /// <returns></returns>
        public static string GetMainboardSerialNumber()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_BaseBoard");
            foreach (ManagementObject share in searcher.Get())
            {
                if (share.Properties.Count > 0)
                {
                    foreach (PropertyData PC in share.Properties)
                    {
                        if (PC.Name.ToUpper() == "SERIALNUMBER")
                        {
                            if (PC.Value != null && PC.Value.ToString() != "")
                            {
                                return PC.Value.ToString();
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }

        public static bool CheckValidRegisterSerialNumber()
        {
            try
            {
                string mainboard_ssid = GetMainboardSerialNumber();

                // อ่านข้อความจาก Registry ซึ่งจะเป็นข้อความที่เข้ารหัสด้วย TripleDES
                string CipherText = ReadRegisterSerialNumber();

                // แยกข้อมูลของ MainboardSerial และ Serial (ข้อความทั้งสองได้เข้ารหัสด้วย MD5 ไว้แล้ว)
                string[] arrPlainText = SymmetricDecrypt(mainboard_ssid, CipherText).Split('|');

                // Compare between data in Registry and MainboardSerial
                if (arrPlainText[0] == DataUtil.MD5Encrypt(mainboard_ssid))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

#region Registry : Serial

        public static void WriteRegisterSerialNumber(string Value)
        {
            RegistryKey r = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft", true);
            //string strRegistryKey = (string)r.GetValue("SER_V");
            //if (strRegistryKey == null)
            {
                /* Write information into registry contains data following:
                 * 
                 * 
                 * +---------------------------+--------------+
                 * |   Mainboard Serial (MD5)  | Serial (MD5) |
                 * +---------------------------+--------------+
                 *   ||                               ||
                 *   \/                               \/
                 *  Encrypt all data with TripleDES by Key: Mainboard Serial
                 * 
                 */
                string mainboard_ssid = GetMainboardSerialNumber();

                string md5Mainboard = DataUtil.MD5Encrypt(mainboard_ssid);
                string md5Company = DataUtil.MD5Encrypt(Value);

                string regText = string.Format("{0}|{1}", md5Mainboard, md5Company);
                r.SetValue("SER_V", SymmetricEncrypt(mainboard_ssid, regText));
            }
        }

        /// <summary>
        /// อ่านข้อความ SerialNumber จาก Registry ซึ่งจะเป็นข้อความที่เข้ารหัส TripleDES ไว้อยู่
        /// </summary>
        /// <returns></returns>
        public static string ReadRegisterSerialNumber()
        {
            //HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft
            RegistryKey r = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft", true);
            string strRegistryKey = (string)r.GetValue("SER_V");

            if (strRegistryKey != null)
            {
                return strRegistryKey;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// ดึงข้อมูล Serial และ MainboardSerial จากที่เก็บใน Registry
        /// </summary>
        /// <param name="Serial">ข้อมูล MD5</param>
        /// <param name="MainboardSerial">ข้อมูล MD5</param>
        /// <returns>หากอ่านข้อมูลได้สมบูรณ์จะคืนค่า True, แต่ถ้าไม่ใช่จะคืนค่า False</returns>
        public static bool GetDataMD5FromRegistry(out string Serial, out string MainboardSerial)
        {
            Serial = null;
            MainboardSerial = null;

            try
            {
                string mainboard_ssid = GetMainboardSerialNumber();
                string strRegSerialNumber = ReadRegisterSerialNumber();

                if (string.IsNullOrEmpty(strRegSerialNumber))
                    return false;

                // แยกข้อมูลของ MainboardSerial และ Serial (ข้อความทั้งสองได้เข้ารหัสด้วย MD5 ไว้แล้ว)
                string[] arrData = SymmetricDecrypt(mainboard_ssid, strRegSerialNumber).Split('|');

                MainboardSerial = arrData[0];
                Serial = arrData[1];
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


#endregion

#region Conversion Data

        /// <summary>
        /// Create empty DataTable object with schema.
        /// </summary>
        /// <returns>True if create DataTable finish. Otherwise return false.</returns>
        public static bool CreateDataTableSchema(Type typeObject, out DataTable dataTable)
        {
            Type type = typeObject;
            if (type == null)
                throw new Exception("Type not found");

            DataTable dtb = new DataTable();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (pi != null && pi.CanRead && pi.CanWrite)
                {
                    // new column
                    DataColumn dc = null;
                    if (pi.PropertyType.GenericTypeArguments.Length > 0)
                    {
                        dc = new DataColumn(pi.Name, pi.PropertyType.GenericTypeArguments[0]);
                    }
                    else
                    {
                        dc = new DataColumn(pi.Name, pi.PropertyType);
                    }
                    dc.AllowDBNull = true;

                    // add column
                    dtb.Columns.Add(dc);
                }
            }
            dataTable = dtb;
            return true;
        }

#region " Parse / Format between DataTable and DTO "

        /// <summary>
        /// Convert DataTransferObject to DataTable. It will copy value in property that declare FieldAttribute to new DataTable.
        /// </summary>
        /// <typeparam name="T">Type of IDataTransferObject</typeparam>
        /// <param name="list">List of DTO which will convert.</param>
        /// <returns>Return null if cannot convert.</returns>
        public static DataTable ConvertListToDataTable<T>(List<T> list) where T : class
        {
            // Create object given DTO.
            Type typeOfDTO = typeof(T);            
            T dto = default(T);
            if (list.Count == 0)
            {
                
                object instance = Activator.CreateInstance(typeOfDTO);
                dto = (T) instance;
            }
            else
            {
                dto = list[0];
            }

            // Create schema of DataTable
            DataTable dataTable = null;
            bool bResult = CreateDataTableSchema(typeOfDTO, out dataTable);
            if (bResult == false || dataTable == null)
            {
                return null;
            }

            // Dump rows.
            if (list != null)
            {

                string[] propDTONames = GetListPropertyNames(typeOfDTO);
                string[] names = new string[dataTable.Columns.Count];
                for (int i = 0; i < names.Length; i++)
                {
                    names[i] = dataTable.Columns[i].ColumnName;
                }

                for (int i = 0; i < list.Count; i++)
                {
                    DataRow row = dataTable.NewRow();

                    // Copy column. 
                    // ยึดการวนจำนวน Column ตามจำนวนของ Property ใน DTO เป็นหลัก  เพราะเป็นการคัดลอกข้อมูลจาก DTO->DataTable                        
                    for (int iCol = 0; iCol < propDTONames.Length; iCol++)
                    {
                        string colName = propDTONames[iCol];
                        //Check column name exists on DataTable.
                        if (names.Contains(colName))
                        {
                            row[colName] = GetPropertyValue(list[i], colName) ?? DBNull.Value;
                        }
                    }
                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }

        /// <summary>
        /// Convert DataTable to DataTransferObject. It will copy value on DataTable to each field on DTO.
        /// </summary>
        /// <typeparam name="T">Type of IDataTransferObject which convert to.</typeparam>
        /// <param name="dataTable">Source DataTable.</param>
        /// <returns>List of DTO.</returns>
        public static List<T> ConvertDataTableToList<T>(DataTable dataTable) where T : class
        {
            List<T> list = new List<T>();

            //DTOList<T> list = new DTOList<T>();
            if (dataTable == null)
                return list;

            DataColumnCollection columns = dataTable.Columns;
            for (int iRow = 0; iRow < dataTable.Rows.Count; iRow++)
            {
                DataRow drRow = dataTable.Rows[iRow];
                T dtoInstance = (T) Activator.CreateInstance(typeof (T));

                for (int i = 0; i < columns.Count; i++)
                {
                    try
                    {
                        string colName = columns[i].ColumnName;

                        PropertyInfo propInfo = FindProperty(dtoInstance, colName);
                        if (propInfo != null)
                        {
                            DataRowVersion dataRowVersion = DataRowVersion.Default;
                            if (drRow.RowState == DataRowState.Deleted)
                                dataRowVersion = DataRowVersion.Original;
                            else
                            {
                                dataRowVersion = DataRowVersion.Current;
                            }

                            object objVal = null;
                            if (propInfo.PropertyType.IsGenericType)
                            {
                                objVal = System.Convert.ChangeType(dataTable.Rows[iRow][i, dataRowVersion], propInfo.PropertyType.GenericTypeArguments[0]);
                            }
                            else
                            {
                                objVal = System.Convert.ChangeType(dataTable.Rows[iRow][i, dataRowVersion], propInfo.PropertyType);    
                            }
                                
                            propInfo.SetValue(dtoInstance, objVal);
                            
                        }
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine(">> ERROR FROM DataUtil.ConvertDataTableToList: " + err.Message + ", " + err.Source);

#if DEBUG
                        throw;
#endif
                    }
                }

                list.Add((T) dtoInstance);
            }
            return list;
        }

        /// <summary>
        /// Convert DataRow object to DTO object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T ConvertDataRowToDTO<T>(DataRow row) where T : class
        {
            DataColumnCollection columns = row.Table.Columns;
            T dtoInstance = (T) Activator.CreateInstance(typeof (T));
            for (int i = 0; i < columns.Count; i++)
            {
                try
                {
                    PropertyInfo propInfo = FindProperty(dtoInstance, columns[i].ColumnName);
                    if (propInfo != null)
                    {
                        // Copy value from dataTable to DTO.
                        propInfo.SetValue(dtoInstance, row[i]);
                    }
                }
                catch (Exception err)
                {
                    Trace.WriteLine(err.Message + ", Column: " + columns[i].ColumnName);

#if DEBUG
                    throw;
#endif
                }
            }

            return (T) dtoInstance;
        }

        /// <summary>
        /// Convert SqlDataReader into List of Type and close SqlDataReader automatically.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<T> ConvertDataReaderToList<T>(IDataReader reader)
        {
            List<T> lstResult = new List<T>();

            T dtoInstance = (T)Activator.CreateInstance(typeof(T));
            List<string> lstFieldName = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                PropertyInfo propInfo = FindProperty(dtoInstance, reader.GetName(i));
                if (propInfo != null)
                {
                    lstFieldName.Add(reader.GetName(i));
                }
            }

            while (reader.Read())
            {
                dtoInstance = (T)Activator.CreateInstance(typeof(T));
                for (int iCol = 0; iCol < lstFieldName.Count; iCol++)
                {
                    string fieldName = lstFieldName[iCol];
                    try
                    {
                        PropertyInfo propInfo = FindProperty(dtoInstance, fieldName);
                        if (propInfo != null)
                        {
                            object objVal = reader[fieldName];

                            if (objVal == DBNull.Value)
                                objVal = null;

                            propInfo.SetValue(dtoInstance, objVal);
                        }
                    }
                    catch (Exception err)
                    {
                        Trace.WriteLine(err.Message + ", Column: " + fieldName);

#if DEBUG
                        throw;
#endif
                    }

                }

                lstResult.Add(dtoInstance);

            }

            reader.Close();

            return lstResult;
        }

#endregion

#region Reflection

        /// <summary>
        /// Get property's value of given owner object instance at runtime.
        /// </summary>
        /// <param name="ownerObj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue(object ownerObj, string propertyName)
        {
            PropertyInfo propInfo = FindProperty(ownerObj, propertyName);
            if (propInfo == null)
                return null;

            return propInfo.GetValue(ownerObj, null);
        }

        public static void SetPropertyValue(object ownerObj, string propertyName, object value)
        {
            PropertyInfo propInfo = FindProperty(ownerObj, propertyName);
            if (propInfo != null)
            {
                propInfo.SetValue(ownerObj, value, null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ownerObj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static PropertyInfo FindProperty(object ownerObj, string propertyName)
        {
            Type type = ownerObj.GetType();
            return type.GetProperty(propertyName);
        }

        public static string[] GetListPropertyName(object ownerObj)
        {
            return GetListPropertyNames(ownerObj.GetType());
        }

        public static string[] GetListPropertyNames(Type type)
        {
            PropertyInfo[] propInfos = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance);

            string[] strNames = new string[propInfos.Length];
            for (int i = 0; i < strNames.Length; i++)
            {
                strNames[i] = propInfos[i].Name;
            }

            return strNames;
        }

#endregion

#endregion


    }
}
