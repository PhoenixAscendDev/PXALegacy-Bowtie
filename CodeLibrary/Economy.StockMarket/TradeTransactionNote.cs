using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public struct TradeTransactionNote<TKey>
    {
        public string TransactionID { get; set; }

        public TradeType TradeType { get; set; }

        public string AccountID { get; set; }

        public TKey ExchangeID { get; set; }

        public string CompanyID { get; set; }

        public string CheckSum { get; set; }

        public int ShareCount { get; set; }

        public CurrencyAmountPair SharePrice { get; set; }

        public CurrencyAmountPair GetTotalCost()
        {
            double total = (double)SharePrice * ShareCount;

            return new CurrencyAmountPair(SharePrice.Currency, total);

        }

        public static implicit operator double(TradeTransactionNote<TKey> note)
        {
            return (double)note.GetTotalCost();
        }

    }
}
