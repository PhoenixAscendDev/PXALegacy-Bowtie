using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;

namespace JB2.Common
{
    public class JB2AppSettings<T> where T : new()
    {
        private const string DEFAULT_FILENAME = "jb2app_settings.jsn";

        public void Save(string fileName = DEFAULT_FILENAME)
        {
            File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(this));
        }

        public static void Save(T pSettings, string fileName = DEFAULT_FILENAME)
        {
            File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(pSettings));
        }

        public static T Load(string fileName = DEFAULT_FILENAME)
        {
            T t = new T();
            if (File.Exists(fileName))
                t = (new JavaScriptSerializer()).Deserialize<T>(File.ReadAllText(fileName));
            return t;
        }

    }
}