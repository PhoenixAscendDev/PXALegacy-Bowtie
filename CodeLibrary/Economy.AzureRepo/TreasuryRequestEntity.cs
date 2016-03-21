using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common.Data;

namespace JB2.Economy.Data
{
    public class TreasuryRequestEntity : AzureTableEntity,  ITreasuryRequest
    {

        #region Constructor
        public TreasuryRequestEntity(string partitionKey, string rowKey) : base(partitionKey, rowKey)
        {

        }

        public TreasuryRequestEntity() : base()
        {

        }

        #endregion Constructor

        #region Properties
        public object Requestor { get; set; }
        public DateTime RequestDate { get; set; }
        public long Amount { get; set; }
        public string VerificationKey { get; set; }
        public string Treasury { get; set; }
        public string RequestorID { get; set; }
        #endregion Properties
    }
}
