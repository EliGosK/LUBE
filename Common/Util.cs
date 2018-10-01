using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Extension;
using EAP.Framework;
using EAP.Framework.Data;
using EAP.Framework.Windows.MultiLanguage;

namespace Common
{
    public class Util
    {
        private static CultureInfo m_culture = null;
        private static NumberFormatInfo m_numInfo = null;

        #region Static Constructor
        static Util()
        {
            //Date time and region
            m_culture = new CultureInfo("en-US", true);
            m_culture.DateTimeFormat.ShortDatePattern = "dd'/'MM'/'yyyy";

            //number
            m_numInfo = m_culture.NumberFormat;
        }
        #endregion

        #region constant value

        #endregion

        #region Properties
        #endregion

        #region Common Function
        public static bool IsNull(object data)
        {
            if (data == null || data == DBNull.Value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string IsNull(DataRow dr, int col, string value)
        {
            if (dr.IsNull(col) || dr.HasErrors)
                return value;
            else
                return dr[col].ToString();

        }

        public static object IsNull(object data, object value)
        {
            if (IsNull(data))
                return value;
            else
                return data;
        }

        public static bool IsNullOrEmpty(object obj)
        {
            if (IsNull(obj) || obj.ToString().Trim().Equals(String.Empty))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsNullOrEmptyOrNeg(object obj)
        {
            if (IsNullOrEmpty(obj))
            {
                return true;
            }
            if (ConvertTextToDecimal(obj.ToString()) < 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsNullOrEmptyOrZero(object obj)
        {
            if (IsNullOrEmpty(obj))
            {
                return true;
            }
            if (ConvertTextToDecimal(obj.ToString()) == 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsNullOrEmptyOrZeroOrNeg(object obj)
        {
            if (IsNullOrEmpty(obj))
            {
                return true;
            }
            if (ConvertTextToDecimal(obj.ToString()) <= 0)
            {
                return true;
            }
            return false;
        }

        public static DateTime FirstDayOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }
        #endregion
      
        #region Permission
        public static void UpdateToolbarButtonPermission(Type typeForm, string strPermName, MenuItem mItem)
        {           
            bool bEnabled = false;
            if (mItem.Enabled)
            {
                bEnabled = AppEnvironment.Permission.AllowPermission(AppEnvironment.UserLogin, typeForm.FullName, strPermName);
            }
            mItem.Enabled = bEnabled;
        }

        public static void UpdateToolbarButtonPermission(Type typeForm, string strPermName, Control control)
        {
            bool bEnabled = false;            

            bEnabled = AppEnvironment.Permission.AllowPermission(AppEnvironment.UserLogin, typeForm.FullName, strPermName);

            if (bEnabled)
            {
                //control.Tag = string.Empty;                
            }
            else
            {
                control.Enabled = false;
                control.Tag = "no control";

                control.BackColor = Color.AliceBlue;
            }
        }
        
        public static void UpdateToolbarButtonPermission(Type typeForm, string strPermName, ToolStripButton toolbarButton)
        {
            bool bEnabled = false;
            
            bEnabled = AppEnvironment.Permission.AllowPermission(AppEnvironment.UserLogin, typeForm.FullName, strPermName);

            
            if (bEnabled)
            {
                toolbarButton.Visible = true;
            }
            else
            {
                toolbarButton.Visible = false;
                //ControlUtil.EnabledControl(false, toolbarButton);

                // Change Image Icon
                // toolbarButton.ImageKey = "NoPermission";
                // toolbarButton.Image = EAP.Framework.Windows.Properties.Resources.ToolbarPermiss;
                // End

                toolbarButton.Tag = "no control";
            }
            // End
        }

        //public static void UpdateToolbarButtonPermission(Type typeForm, string strPermName, DevExpress.XtraBars.BarButtonItem bItem)
        //{
        //    bool bEnabled = false;
        //    bEnabled = AppEnvironment.DAOSecurity.AllowPermission(AppEnvironment.UserID, typeForm.Namespace + "." + typeForm.Name, strPermName);

        //    if (bEnabled)
        //    {
        //        bItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        //    }
        //    else
        //    {
        //        bItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        //    }

        //    //bItem.Tag = "no control";
        //}
        #endregion

        #region Utils - Conversion DataType

        public static Nullable<T> Convert<T>(object input)
            where T : struct
        {
            if (input == null)
                return null;
            if (input is Nullable<T> || input is T)
                return (Nullable<T>)input;


            try
            {
                Type typeParameterType = typeof(T);
                if (typeParameterType == typeof(int))
                {
                    int result;
                    if (!Int32.TryParse(input.ToString(), out result))
                        return null;
                }
                else if (typeParameterType == typeof(decimal))
                {
                    decimal result;
                    if (!Decimal.TryParse(input.ToString(), out result))
                        return null;
                }

                T test = (T)System.Convert.ChangeType(input, typeParameterType);
                return (test);
            }
            catch
            {
                throw;
            }
        }

        #region Number
        public static object ConvertObject(object obj, Type type)
        {
            return ConvertObject(obj, type, m_culture);
        }
        public static object ConvertObject(object obj, Type type, CultureInfo cInfo)
        {
            if (type == typeof(string))
            {
                return obj.ToString();
            }
            else if (type == typeof(Double))
            {
                return ConvertTextToDouble(obj.ToString());
            }
            else if (type == typeof(decimal))
            {
                return ConvertTextToDecimal(obj.ToString());
            }
            else if (type == typeof(int))
            {
                return ConvertTextToInteger(obj.ToString());
            }
            else if (type == typeof(DateTime))
            {
                return DateTime.Parse(obj.ToString(), cInfo, DateTimeStyles.None);
            }
            else if (type == typeof(bool))
            {
                return ConvertTextToBoolean(obj.ToNullableString());
            }
            else
            {
                return System.Convert.ChangeType(obj, type);
            }

        }
        public static double ConvertObjectToDouble(object obj)
        {
            if (IsNull(obj)) return 0;
            return (Double)ConvertObject(obj, typeof(Double));
        }
        public static decimal ConvertObjectToDecimal(object obj)
        {
            if (IsNull(obj)) return 0;
            return (decimal)ConvertObject(obj, typeof(decimal));
        }
        public static int ConvertObjectToInteger(object obj)
        {
            if (IsNull(obj)) return 0;
            return (int)ConvertObject(obj, typeof(int));
        }
        public static long ConvertObjectToLong(object obj)
        {
            if (IsNull(obj)) return 0;
            return (long)ConvertObject(obj, typeof(long));
        }
        public static bool ConvertObjectToBoolean(object obj)
        {
            if (IsNull(obj)) return false;
            return (bool)ConvertObject(obj, typeof(bool));
        }
        public static double ConvertTextToDouble(string data)
        {
            double dbl = 0;
            if (!Double.TryParse(data, NumberStyles.Currency | NumberStyles.AllowTrailingSign, m_numInfo, out dbl))
                return 0;
            else
                return dbl;
        }
        public static decimal ConvertTextToDecimal(string data)
        {
            //int len = GetStringLength(data.Trim());
            decimal dec;
            if (!Decimal.TryParse(data, NumberStyles.Currency | NumberStyles.AllowTrailingSign, m_numInfo, out dec))
                return 0;
            else
                return dec;
        }
        public static int ConvertTextToInteger(string data)
        {
            int intTmp;
            if (!Int32.TryParse(data, NumberStyles.Currency | NumberStyles.AllowTrailingSign, m_numInfo, out intTmp))
                return 0;
            else
                return intTmp;
        }
        public static int ConvertTextToInteger(string data, int ireturn)
        {
            int intTmp;
            if (!Int32.TryParse(data, NumberStyles.Currency | NumberStyles.AllowTrailingSign, m_numInfo, out intTmp))
                return ireturn;
            else
                return intTmp;
        }
        public static DateTime ConvertTextToDate(string data)
        {
            return ConvertTextToDate(data, m_culture);
        }
        public static DateTime ConvertTextToDate(string data, CultureInfo culture)
        {
            DateTime dt = new DateTime(0);
            if (!DateTime.TryParse(data, culture.DateTimeFormat, DateTimeStyles.None, out dt))
                return DateTime.MinValue;
            else
                return dt;
        }

        public static bool ConvertTextToBoolean(string data)
        {

            if (data == "T" || data == "t" || data == bool.TrueString)
                return true;
            else if (data == "F" || data == "f" || data == bool.FalseString)
                return false;
            else
            {
                bool result = false;
                if (!bool.TryParse(data, out result))
                    return false;

                return result;
            }

        }

        public static string RepeatDecimalPlace(decimal source, int iDecimal)
        {
            return RepeatDecimalPlace(source.ToString(), iDecimal);
        }

        public static string RepeatDecimalPlace(string source, int iDecimal)
        {
            try
            {
                if (IsNullOrEmpty(source))
                    return source;

                int iDec = source.Trim().Split('.')[1].Length;
                for (int i = 0; i < iDecimal - iDec; i++)
                {
                    source = source.Insert(source.Length, "0");
                }
                return source;
            }
            catch
            {
                return source;
            }
        }
        #endregion

        #endregion


        /// <summary>
        /// Clone object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException(@"The type must be serializable.", "source");
            }
            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new MemoryStream())
            {
                using (stream)
                {
                    formatter.Serialize(stream, source);
                    stream.Seek(0, SeekOrigin.Begin);
                    return (T)formatter.Deserialize(stream);
                }
            }
        }

        #region Multilanguage

        #region Get Message Text

       

        public static string GetMessageText(string msgId, params object[] placeholder)
        {
            string msgText = MessageManager.GetMessageText(msgId);

            // Replace newline
            msgText = msgText.Replace("\\r\\n", Environment.NewLine);
            msgText = msgText.Replace("\\n", Environment.NewLine);

            try
            {
                if (placeholder == null)
                    return msgText;

                return String.Format(msgText, placeholder);
            }
            catch (Exception ex)
            {
                throw new Exception(msgText + "\nMessage does not match with Message parameters");
            }
        }

        public static string GetMessageText(string msgId)
        {
            return GetMessageText(msgId, "");
        }

        public static string GetMessageText(eMsgId msgId)
        {
            return GetMessageText(msgId.ToString());
        }

        public static string GetMessageText(eMsgId msgId, params object[] placeholder)
        {
            return GetMessageText(msgId.ToString(), placeholder);
        }
        #endregion

        #endregion
        
        #region Email Util

        public static void SendMail(string[] emailAddressTo, string[] emailAddressCC, string subject, string message, string senderName = "", bool isBodyHtml = true)
        {

            try
            {
                string emailSender = AppEnvironment.EmailSenderName;
                if (!String.IsNullOrEmpty(senderName))
                {
                    emailSender = senderName;
                }

                SmtpInfo smtpInfo = AppEnvironment.SmtpInfo;
                {
                    SmtpClient client = new SmtpClient(smtpInfo.Host, smtpInfo.Port);
                    client.EnableSsl = smtpInfo.EnableSSL;
                    client.UseDefaultCredentials = smtpInfo.UseDefaultCredential;
                    client.Credentials = new NetworkCredential(smtpInfo.CredentialAccount, smtpInfo.CredentialPassword);

                    MailAddress mailFrom = new MailAddress(smtpInfo.FromEmail, emailSender);

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = mailFrom;
                    if (emailAddressTo != null)
                    {
                        foreach (string strAddress in emailAddressTo)
                        {
                            if (mailMessage.To.All(d => d.Address != strAddress))
                            {
                                MailAddress ToEmailAddress = new MailAddress(strAddress);
                                mailMessage.To.Add(ToEmailAddress);
                            }
                        }
                    }
                    if (emailAddressCC != null)
                    {
                        foreach (string strAddress in emailAddressCC)
                        {
                            MailAddress mailCC = new MailAddress(strAddress);
                            mailMessage.CC.Add(mailCC);
                        }
                    }
                    mailMessage.Subject = subject;
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    mailMessage.Body = message;
                    mailMessage.IsBodyHtml = isBodyHtml;
                    client.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        #endregion
    }
}
