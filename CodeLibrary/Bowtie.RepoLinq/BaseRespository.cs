using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace JB2.Bowtie.Data
{
    public class BaseRespository
    {
        public static Decimal getDecimal<T>(T record, string property) where T : class
        {
            PropertyInfo prop = record.GetType().GetProperty(property);
            return prop == null ? 0 : Convert.ToDecimal(prop.GetValue(record, null));
        }

        public static int getInt<T>(T record, string property) where T : class
        {
            PropertyInfo prop = record.GetType().GetProperty(property);
            return prop == null ? 0 : Convert.ToInt32(prop.GetValue(record, null));
        }
        public static DateTime getDate<T>(T record, string property) where T : class
        {
            PropertyInfo prop = record.GetType().GetProperty(property);
            return prop == null ? DateTime.MinValue : Convert.ToDateTime(prop.GetValue(record, null));
        }

        public static string getString<T>(T record, string property) where T : class
        {
            PropertyInfo prop = record.GetType().GetProperty(property);
            return prop == null ? string.Empty : Convert.ToString(prop.GetValue(record, null));
            //string[] props = new string[] { "Ssn", "FirstName", "LastName"};
            //string value = prop == null ? string.Empty : Convert.ToString(prop.GetValue(record, null));
            //return props.Contains(property) ? decrypt(value) : value;
        }

        public static byte[] getByteArray<T>(T record, string property) where T : class
        {
            PropertyInfo prop = record.GetType().GetProperty(property);
            return prop == null ? new byte[32768] : ((System.Data.Linq.Binary)prop.GetValue(record, null)).ToArray();
            //string[] props = new string[] { "Ssn", "FirstName", "LastName"};
            //string value = prop == null ? string.Empty : Convert.ToString(prop.GetValue(record, null));
            //return props.Contains(property) ? decrypt(value) : value;
 

        }


        public static Boolean getBoolean<T>(T record, string property) where T : class
        {
            PropertyInfo prop = record.GetType().GetProperty(property);
            return prop == null ? new Boolean() : Convert.ToBoolean(prop.GetValue(record, null));
        }



        public static List<T> makeList<T>(T itemOftype)
        {
            List<T> newList = new List<T>();
            return newList;
        }


        public static List<T> bitwiseToList<T>(int bitwise)
        {

            //var returnList = makeList(record);
            Type enumType = typeof(T);

            Array enumValArray = System.Enum.GetValues(enumType);

            List<T> enumValList = new List<T>(enumValArray.Length);
            if (enumType.BaseType != typeof(System.Enum))
                throw new ArgumentException("T must be of type System.Enum");

            foreach (int value in enumValArray)
            {
                if ((value & bitwise) == value)
                    enumValList.Add((T)System.Enum.Parse(enumType, value.ToString()));

            }
            return enumValList;
        }
    }
}
