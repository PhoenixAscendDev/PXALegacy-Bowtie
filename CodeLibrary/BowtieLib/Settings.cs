using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public static class Settings
    {
        internal static Application _application = null;

        internal static string _sigFormat = "{0}{1}{2}{3}{4}";

        internal static IUnitOfWork _uofw;

        internal const string _headerDelimiter = ":";


        public static Application CurrentApplication
        {
            get
            {
                if (_application == null)
                    return new Application("4d53bce03ec34c0a911182d4c228ee6c", "A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc=");
                else
                    return _application;
            }
        }

        public static string SignatureFormat
        {
            get
            {
                StringBuilder result = new StringBuilder(JB2.Bowtie.Settings.CurrentApplication.ID);
                result.Append(">*<");
                result.Append(_sigFormat);
                return result.ToString();
            }
        }

        public static string HeaderDelimiter
        {
            get
            {
                return _headerDelimiter;
            }
        }

        

        
    }
}
