using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace JB2.Bowtie.Data
{
    public class LocalApplication : BasicApplication
    {
        [JsonConverter(typeof(ConcreteConverter<JB2.Common.Business>))]
        public new JB2.Common.IBusiness Company
        {
            get
            {
                return base.Company;
            }
            set
            {
                base.Company = value;
            }
        }
    }
}
