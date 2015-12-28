using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common.Data;

namespace JB2.Economy.Data
{
    public class TreasuryNote: AzureTableEntity, ITreasuryNote
    {
        public TreasuryNote(string partitionKey, string rowKey) : base(partitionKey, rowKey)
        {

        }

        public TreasuryNote()
        {

        }

        public long Amount { get; set; }
        public string Treasury { get; set; }
        public string Status { get; set;}
        public DateTime DateCreated { get; set; }
        public string IssuedBy { get; set; }

        public JB2.Common.IIDNamePair<string,string> GetRequestor()
        {
            return new JB2.Common.IDNamePair<string, string>(this.IssuedBy,string.Empty);
        }

    }
}
