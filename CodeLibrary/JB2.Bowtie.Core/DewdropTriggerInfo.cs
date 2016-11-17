using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public struct DewdropTriggerInfo
    {

        #region Fields
        private string _id;
        #endregion Fields
        public string ID
        {
            get
            {
                if (string.IsNullOrEmpty(_id))
                    _id = "t" + JB2.Common.NewID.TickHash();
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public string DewdropID { get; set; }

        public string Namespace { get; set; }

        public string Classname { get; set; }

        public string ParamaterString1 { get; set; }

        public string ParameterString2 { get; set; }

        public string TriggerType { get; set; }


        public static DewdropTriggerInfo New
        {
            get
            {
                DewdropTriggerInfo info = new DewdropTriggerInfo();
                info.ID = JB2.Common.NewID.TickHash();

                return info;
            }
        }
    }
}
