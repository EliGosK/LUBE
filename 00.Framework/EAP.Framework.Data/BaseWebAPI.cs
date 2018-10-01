using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace EAP.Framework.Data
{
    /// <summary>
    /// Class สำหรับจัดการติดต่อกับ WebAPI
    /// ทุกครั้งที่ใช้เสร็จ จะต้องเรียกคำสั่ง Close() เสมอ
    /// </summary>
    public class BaseWebAPI : IDisposable
    {
        #region Member
        protected WebClient _client = null;
        protected string _token = null;

        protected static string AUTErrorMessage = "Your login session has expired.";
        protected static string AUTToString = string.Format("{0}" +
                                                          "- The login session will get expired if user is idle for 24 hrs continuously.{0}" +
                                                          "- This Username already be logged on to another computer.{0}{0}{0}", Environment.NewLine);

        

        #endregion      

        #region Constructor

        public BaseWebAPI() : this(null)
        {
            
        }
        public BaseWebAPI(string token)
        {
            _token = token;
            _client = CreateWebClient();
        }

        #endregion       

        #region Method

        /// <summary>
        /// Create new instance of WebClient
        /// </summary>
        private WebClient CreateWebClient()
        {
            WebClient client = new WebClient();

            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            client.Encoding = Encoding.UTF8;

            IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
            defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;
            client.Proxy = defaultWebProxy;

            //_client.Headers.Add("COMPUTER_NAME", System.Environment.MachineName + "-" + System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            if (!string.IsNullOrEmpty(_token))
                client.Headers.Add("TOKEN", _token);

            client.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);

            return client;
        }               

        /// <summary>
        /// Get flag to indicate current WebClient is closed
        /// </summary>
        public bool IsClose
        {
            get { return _client == null; }
        }

        /// <summary>
        /// Force to release WebClient resource.
        /// </summary>
        public void Close()
        {
            if (_client != null)
            {
                try
                {
                    _client.Dispose();
                }
                catch
                {

                }
                finally
                {
                    _client = null;
                }
            }
        }


        #endregion

        #region Implementation IDisposable

        public void Dispose()
        {
            this.Close();
        }

        #endregion

        #region Private Method

        /// <summary>
        /// Generate query parameter string
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        protected string GenerateQueryStringParameter(string prefix, Hashtable hs)
        {
            if (hs == null)
                return String.Empty;

            StringBuilder sb = new StringBuilder();
            foreach (String item in hs.Keys)
            {
                string strValue = "";

                if (hs[item] is DateTime)
                {
                    strValue = HttpUtility.UrlEncode(Convert.ToDateTime(hs[item]).ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US")));
                }
                else
                {
                    strValue = HttpUtility.UrlEncode(Convert.ToString(hs[item]));
                }

                if (sb.Length > 0)
                    sb.Append("&");

                sb.Append(string.Format("{0}={1}", item, strValue));
            }


            return prefix + sb.ToString();
        }

        protected string GenerateParameter(Hashtable hs)
        {
            return GenerateQueryStringParameter("?", hs);
        }

        private void ThrownException(Exception exception)
        {
            if (exception is WebException)
            {
                WebException ex = (WebException) exception;
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (((HttpWebResponse) ex.Response).StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new ApplicationException(AUTErrorMessage, new Exception(AUTToString));
                    }

                    if (((HttpWebResponse) ex.Response).StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new ApplicationException("Not found API Service");
                    }

                    if (((HttpWebResponse) ex.Response).StatusCode == HttpStatusCode.ExpectationFailed)
                    {
                        // Read Stream from Reponse
                        string strMessage = "";

                        Stream stream = ex.Response.GetResponseStream();
                        if (stream != null)
                        {
                            using (StreamReader sr = new StreamReader(stream))
                            {
                                strMessage = sr.ReadToEnd();
                                strMessage = JsonConvert.DeserializeObject<string>(strMessage);
                            }

                        }

                        throw new ApplicationException(strMessage);
                    }

                    throw exception;

                }

                throw new Exception(ex.Message);
            }
            else
            {
                if (exception.Message.ToUpper().Contains("UNAUTHORIZED"))
                {
                    throw new ApplicationException(AUTErrorMessage, new Exception(AUTToString));
                }

                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host">host address. Such as "htt://www.myweb.com/EAP-WebAPI/"</param>
        /// <param name="module">Controller to request. Such as "Security/GetToken"</param>
        /// <param name="parameters">Query string</param>
        /// <param name="formData">Forms Data. It contain Key and Value </param>
        /// <returns></returns>
        public string ExecuteUploadValues(string host, string module, Hashtable parameters, NameValueCollection formData)
        {
            try
            {
                if (formData == null)
                {
                    formData = new NameValueCollection();
                }

                _client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string strQueryString = CombineURL(host, module) + GenerateParameter(parameters);
                byte[] byteResponse = _client.UploadValues(strQueryString, "POST", formData);

                this.Close();

                string strResult = "";
                if (byteResponse != null)
                {
                    strResult = Encoding.UTF8.GetString(byteResponse);
                }

                /* If result is Exception */
                if (strResult.Contains("Exception"))
                {
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(strResult);
                    throw new Exception(dt.Rows[0]["Message"].ToString(), new Exception(dt.Rows[0]["ToString"].ToString()));
                }
                //-----------------------------------------

                return strResult;
            }
            catch (Exception ex)
            {
                ThrownException(ex);
                return "";
            }
        }

        public string ExecuteUploadJsonData(string host, string module, Hashtable parameters, object objData)
        {            
            try
            {                                          
                string strQueryString = CombineURL(host, module) + GenerateParameter(parameters);
                string strJson = JsonConvert.SerializeObject(objData);

                _client.Headers[HttpRequestHeader.ContentType] = "application/json";
                string strResult = _client.UploadString(strQueryString, "POST", strJson);

                this.Close();

                /* If result is Exception */
                if (strResult.Contains("Exception"))
                {
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(strResult);
                    throw new Exception(dt.Rows[0]["Message"].ToString(), new Exception(dt.Rows[0]["ToString"].ToString()));
                }
                //-----------------------------------------

                return strResult;
            }
            catch (Exception ex)
            {
                ThrownException(ex);
                return "";
            }
        }
        #endregion

        #region Internal Method
        internal static string CombineURL(string url1, string url2)
        {
            if (url1 == null)
                throw new ArgumentNullException("url1");

            if (url2 == null)
                throw new ArgumentNullException("url2");

            string newUrl = url1;

            if (url1.Length > 0)
            {
                char lastChar = url1[url1.Length - 1];
                if (lastChar == '\\' || lastChar == '/')
                    newUrl = url1.Substring(0, url1.Length - 1);
            }

            if (url2.Length > 0)
            {
                newUrl += "/";

                char firstChar = url2[0];
                if (firstChar == '/' || firstChar == '\\')
                    newUrl += url2.Substring(1);
                else
                {
                    newUrl += url2;
                }
            }


            return newUrl;
        }

        #endregion

    }
}
