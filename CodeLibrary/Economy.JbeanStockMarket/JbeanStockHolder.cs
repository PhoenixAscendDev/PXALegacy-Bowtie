using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using JB2.Common;

namespace JB2.Economy
{
    public class JbeanStockHolder : JB2Class, IJbeanStockHolder
    {

        #region Constructor

        protected JbeanStockHolder()
        {

        }

        public JbeanStockHolder(string accountID)
        {
            this.SetProperty<string>("ACCOUNTID", accountID);
        }

        #endregion Constructor



        public string StockExchangeAccountID
        {
            get
            {
                return this.GetProperity<string>("ACCOUNTID");
            }

            set
            {
                throw new NotSupportedException();
            }
        }

        public long GetFundsAvalable()
        {
            throw new NotImplementedException();
        }

        public jBeanAccount GetjBeanAccount()
        {
            throw new NotImplementedException();
        }

        public uint GetShareCount(string stockSymbol)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<JbeanStockShare> GetShares(string stockSymbol)
        {
            throw new NotImplementedException();
        }

        public long GetTotalValue()
        {
            throw new NotImplementedException();
        }

        public static JbeanStockHolder New
        {
            get
            {
                var result = new JbeanStockHolder();


                result.SetProperty<string>("ACCOUNTID", JB2.Helper.JbeanStockMarket.GenerateID<IJbeanStockHolder>());

                return result;

            }
        }
    }
}
