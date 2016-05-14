using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy.Data
{
    public class TokenEntity : JB2.Common.Data.AzureTableEntity
    {
        public TokenEntity(string partitionKey, string rowKey) : base(partitionKey, rowKey)
        {

        }

        public TokenEntity() : base()
        {

        }
        public string Treasury { get; set; }
        public int Value { get; set; }
        public string TreasuryNoteID { get; set; }
        public string BankAccountNumber { get; set; }
        public DateTime DateCreated { get; set; }

        public string WalletID { get; set; }
    }
}
