using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace JB2.Economy.Data
{
    public class TransLogEntry : JB2.Common.Log.LogTableEntry
    {
        public TransLogEntry(string partitionKey, string rowKey) : base(partitionKey, rowKey)
        {

        }

        public TransLogEntry() : base()
        {

        }
        public string Treasury { get; set; }
        public string TransType { get; set; }
        public string AccountNumber { get; set; }
        public string Success { get; set; }



    }
}
