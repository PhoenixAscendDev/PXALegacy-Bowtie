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

        internal static DateTime _lastAPIAuthCheck;

        internal static string _sigFormat = "{0}>*<{1}{2}{3}{4}";

        internal const string _headerDelimiter = ":";

        public static Application CurrentApplication
        {
            get
            {
                return _application;
            }
        }

        public static string SignatureFormat
        {
            get
            {
                //StringBuilder result = new StringBuilder(JB2.Bowtie.Settings.CurrentApplication.ID);
               //result.Append(">*<");
                StringBuilder result = new StringBuilder(_sigFormat);
                //result.Append(_sigFormat);
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
