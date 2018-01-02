using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Helper
{
    public class Bowtie
    {

        private const string _dewdropSprogJson = "{\"Key\":\"JB2-dewdrop\",\"Name\":\"Dewdrop\",\"Definition\":{\"Version\":{\"Major\":1,\"Minor\":0,\"Build\":0,\"Revision\":0,\"isPreRelease\":false},\"Name\":\"Dewdrop\",\"ID\":\"JB2-dewdrop\",\"Owner\":\"JBsquared LLC\",\"Properties\":[{\"PropertyName\":\"APPLICATIONID\",\"DataType\":0,\"IsIndex\":false},{\"PropertyName\":\"GDID\",\"DataType\":0,\"IsIndex\":false},{\"PropertyName\":\"PARENTGDID\",\"DataType\":0,\"IsIndex\":false},{\"PropertyName\":\"ISACTIVE\",\"DataType\":9,\"IsIndex\":false},{\"PropertyName\":\"ID\",\"DataType\":1,\"IsIndex\":false},{\"PropertyName\":\"NAME\",\"DataType\":0,\"IsIndex\":false},{\"PropertyName\":\"VALUETYPE\",\"DataType\":1,\"IsIndex\":false}]}}";
        private static Sprog.ISprogType _dewdropSprog { get; set; }

        internal static Sprog.ISprogType DewDropSprog
        {
            get
            {
                if (_dewdropSprog != null)
                    _dewdropSprog = Sprog.SprogItemType.FromJson(_dewdropSprogJson);

                return _dewdropSprog;
            }
        }
         

        public static string GenerateID<T>()
            where T : class
        {
            var guid = JB2.Common.NewID.Guid();
            var result = JB2.Common.NewID.UriHash(new Uri("http://bowtie.io/?=" + guid + typeof(T).ToString()));
            Type type = typeof(T);

            if (type is JB2.Bowtie.IApplication)
                result = "001-" + result;
            if (type is JB2.Bowtie.IDewdrop)
                result = guid;

            return result;
        }

        public static ushort NewRNG()
        {
            var result = (ushort)0;

            if (JB2.Settings.Bowtie.RNGMethod != null)
                result = JB2.Settings.Bowtie.RNGMethod();
            else
                result = JB2.Common.RNG.Randy;


            return result;
        }


        public static string ConvertToJsonString(object obj)
        {
            var result = obj.ToString();

            if (JB2.Settings.Bowtie.JsonSerializerMethod != null)
            {
                result = JB2.Settings.Bowtie.JsonSerializerMethod(obj);
            }

            return result;
        }

        public static T ConvertToObjectFromJsonString<T>(string json)
            where T : class
        {
            var result = default(T);

            if (JB2.Settings.Bowtie.JsonDeserializerMethod != null)
            {
                var obj = JB2.Settings.Bowtie.JsonDeserializerMethod(json, typeof(T));

                result = (T)obj;
                //result = (T)Convert.ChangeType(obj, typeof(T));
            }

            return result;
        }

        public static DateTime Now()
        {
            if (JB2.Settings.Bowtie.NowMethod != null)
                return JB2.Settings.Bowtie.NowMethod();
            else
                return DateTime.UtcNow;
        }
    }
}
