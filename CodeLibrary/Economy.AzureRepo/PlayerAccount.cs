using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common.Data;

namespace JB2.Economy.Data
{
    public class PlayerjBeanAccount : AzureTableEntity
    {
        public PlayerjBeanAccount(string partitionKey, string rowKey) : base(partitionKey, rowKey)
        {

        }

        public PlayerjBeanAccount(): base()
        {

        }

        public string PlayerID { get; set; }
        public string AccountNumber { get; set; }
    }
}
