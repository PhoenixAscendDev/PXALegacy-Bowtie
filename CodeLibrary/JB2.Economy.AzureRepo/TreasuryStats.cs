using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common.Data;

namespace JB2.Economy.Data
{
    public class TreasuryStats : AzureTableEntity
    {

        public TreasuryStats(string partitionKey, string rowKey) : base(partitionKey, rowKey)
        {

        }

        public TreasuryStats() : base()
        {

        }

        public long AmountIssued { get; set; }
        public string Treasury { get; set; }
        public DateTime DateUpdated { get; set; }



    }
}
