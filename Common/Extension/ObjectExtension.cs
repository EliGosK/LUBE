using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EAP.Framework.Data;

namespace Common.Extension
{
    public static class ObjectExtension
    {
        public static string ToNullableString(this object obj)
        {
            if (Util.IsNullOrEmpty(obj))
                return null;

            string str = Convert.ToString(obj);
            if (str != null)
                return str.Trim();

            return str;
        }

        public static int? ToNullableInt32(this object obj)
        {
            if (Util.IsNullOrEmpty(obj))
                return null;

            return Util.ConvertObjectToInteger(obj);
        }

        public static decimal? ToNullableDecimal(this object obj)
        {
            if (Util.IsNullOrEmpty(obj))
                return null;

            return Util.ConvertObjectToDecimal(obj);
        }

        public static bool? ToNullableBoolean(this object obj)
        {
            if (Util.IsNullOrEmpty(obj))
                return null;

            return Util.ConvertObjectToBoolean(obj);
        }

        public static DateTime? ToNullableDateTime(this object obj)
        {
            if (Util.IsNullOrEmpty(obj))
                return null;

            DateTime? dateTime = (DateTime)obj;
            return dateTime;
        }

        public static object GetPropertyValue(this object obj, string propertyName)
        {
            return DataUtil.GetPropertyValue(obj, propertyName);
        }
    }
}
