using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public class WalletReceipt
    {
        public string CurrencyID { get; set; }
        public WalletTransationType TransactionType { get; set; }

        public string TransationID { get; set; }
        public double Amount { get; set; }

        public string  Description { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
