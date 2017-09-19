using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Helper
{
    public class Bowtie
    {
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

            if(JB2.Settings.Bowtie.JsonSerializerMethod != null)
            {
                result = JB2.Settings.Bowtie.JsonSerializerMethod(obj);
            }

            return result;
        }
    }
}
