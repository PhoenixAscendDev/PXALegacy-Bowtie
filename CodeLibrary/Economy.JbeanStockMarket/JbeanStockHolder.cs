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


        public string BankAccountID
        {
            get
            {
               return  this.GetProperity<string>("BANKACCOUNTID");
            }
            set
            {
                this.SetProperty<string>("BANKACCOUNTID", value);
            }
        }



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
            try
            {
                var bankAccount = this.GetjBeanAccount();
                long balance = JB2.Settings.Jbean.Factory.CentralBank.CheckBalance(bankAccount).ToJBean().ToInt();

                return balance;
            }
            catch(Exception ex)
            {
                return 0;
            }


        }

        public jBeanAccount GetjBeanAccount()
        {
            try
            {
                var account = (jBeanAccount)JB2.Settings.Jbean.Factory.CentralBank.GetBankAccount(new JB2.Common.IDNamePair(this.BankAccountID, "jBean Bank Account"));

                return account;

            }
            catch(Exception ex)
            {
                return new jBeanAccount(this.BankAccountID);
            }
           
        }

        public uint GetShareCount(string stockSymbol)
        {
            try
            {
                var repo = JB2.Settings.JbeanStockMarket.Repository;

                var shares = repo.GetStockPositionByAccountID(this.StockExchangeAccountID);

                foreach (var s in shares)
                {
                    var company = repo.GetCompanyByStockSymbol(s.Share.StockSymbol);
                    if (company.StockSymbol == stockSymbol)
                        return (uint)s.Quantity;
                }

                return 0;
            }
            catch(Exception ex)
            {
                return 0;
            }



        }

        public Position<string,long> GetShares(string stockSymbol)
        {
            //try
            //{
            //    var repo = JB2.Settings.JbeanStockMarket.Repository;

            //    var shares = repo.GetStockSharesByAccountID(this.StockExchangeAccountID);

            //    foreach (var s in shares)
            //    {
            //        if (s.Company.StockSymbol == stockSymbol)
            //            return s;
            //    }

            //    return null;
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
            return new Position<string, long>();

        }

        public IEnumerable<Position<string, long>> GetShares()
        {
            try
            {
                var repo = JB2.Settings.JbeanStockMarket.Repository;

                var shares = repo.GetStockPositionByAccountID(this.StockExchangeAccountID);

                return shares;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public long GetTotalValue()
        {
            try
            {
                var repo = JB2.Settings.JbeanStockMarket.Repository;

                var shares = repo.GetStockPositionByAccountID(this.StockExchangeAccountID);

                long totalValue = 0;

                foreach (var s in shares)
                {
                    var company = repo.GetCompanyByStockSymbol(s.Share.StockSymbol);
                    totalValue += totalValue + (company.GetCurrentStockValue() * s.Quantity);
                }

                return totalValue;
            }
            catch (Exception ex)
            {
                return 0;
            }

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
