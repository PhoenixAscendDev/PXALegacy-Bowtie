using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class Application : JB2.Common.IIDNamePair<string, string>
    {
        private string _id;
        private string _name;
        private string _publickey;
        private string _secret;

        

        public Application(string publickey, string secretKey )
        {

        }

        #region Public Properies

        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        
        public string Secret
        {
            get
            {
                return _secret;
            }
        }

       #endregion Public Properies
    }
}
