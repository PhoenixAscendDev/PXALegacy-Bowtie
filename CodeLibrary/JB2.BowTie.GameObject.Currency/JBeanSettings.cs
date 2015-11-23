using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie.Economy.Settings
{
    public static class Jbean
    {
        #region Fields

        private static SettingCollection<string> _settings;
        #endregion Fields


        public static void Configure(IEnumerable<ISetting> settings)
        {
            _settings = new SettingCollection<string>();
            foreach(ISetting s in settings)
            {
                _settings.Add(s);
            }
           
        }

        public static ISetting GetSetting(string settingName)
        {
            if (_settings == null)
                return null;
            else
                return _settings[settingName];
        }

        

    }
}
