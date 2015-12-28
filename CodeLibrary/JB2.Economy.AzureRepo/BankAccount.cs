using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common.Data;

namespace JB2.Economy.Data
{
    public class BankAccountEntity : JB2.Common.Data.AzureTableEntity, IBankAccount
    {
        public BankAccountEntity(string partitionKey, string rowKey) : base(partitionKey,rowKey)
        {

        }

        public BankAccountEntity(): base()
        {
           
        }
        public string AccountNumber { get; set; }
        public string Treasury { get; set; }
        public string RoutingNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public string AccountStatus { get; set; }
        public float Balance { get; set; }
        public string PlayerID { get; set; }
        public DateTime LastTransactionDate { get; set; }
    }
}
