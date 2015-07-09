using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class Manager
    {
        public static bool Initialize(string publicKey, string secretKey)
        {

            Application app = new Application(publicKey, secretKey);
            JB2.Bowtie.Settings._application = app;
            return true;

        }

        public static bool Initialize(BowtieConfig config)
        {
            JB2.Bowtie.Settings.LoadByConfig(config);

            return true;
        }

        public static bool Initialize(string bowtieConfigFile)
        {
            JB2.Bowtie.Settings.SettingsFilename = bowtieConfigFile;

            return true;
        }
    }
}
