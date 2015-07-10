using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public static class ApplicationManager
    {

        public static string GetPublicKey(int applicationID)
        {
            switch(applicationID)
            {
                case 1:
                    return "174B863C17";
                default:
                    return string.Empty;

            }
        }

        public static string GetPublicKeyByPassPhrase(string phrase)
        {


            var appKey = JB2.Common.Utility.GenerateKey(Common.Enum.KeyBitSize.keybit64, phrase);

            return appKey;
        }

    }
}
