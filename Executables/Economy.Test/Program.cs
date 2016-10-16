using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Economy;

namespace Economy.Test
{
    class Program
    {
        static void InitjBean()
        {

            
                //configure jBean
            var jBeanRepo = new JB2.Economy.Data.jBeanRespostory(JB2.Infrastructure.Storage.EconomyAccount);
            JB2.Common.BaseSetting s = new JB2.Common.BaseSetting()
            {
                ID = JB2.Economy.JbeanSettingName.CurrencyID,
                Name = "CurrencyID",
                Value = JB2.Configuration.GetjBeanCurrencyID()
            };

            JB2.Settings.Jbean.Configure(new JB2.Common.BaseSetting[1] { s }, jBeanRepo);

            JB2.Settings.Jbean.Factory.CentralBank.AccountOpened += CentralBank_AccountOpened;

            //configure jBeanStockMarket

            var jBeanStockRepo = new JB2.Economy.Data.JBeanStockRepository(JB2.Infrastructure.Storage.EconomyAccount);

            JB2.Common.BaseSetting s1 = new JB2.Common.BaseSetting()
            {
                ID = JB2.Economy.JbeanStockMarketSettingName.StockExchange,
                Name = JB2.Economy.JbeanStockMarketSettingName.StockExchange,
                Value = new JbeanStockExchange(JB2.Configuration.GetjBeanStockMarketID(), jBeanStockRepo)
            };
            JB2.Common.BaseSetting s2 = new JB2.Common.BaseSetting()
            {
                ID = JB2.Economy.JbeanStockMarketSettingName.Currency,
                Name = JB2.Economy.JbeanStockMarketSettingName.Currency,
                Value = JB2.Settings.Jbean.Factory.Currencies[0]
            };
            JB2.Common.BaseSetting s3 = new JB2.Common.BaseSetting()
            {
                ID = JB2.Economy.JbeanStockMarketSettingName.TreasuryRequestor,
                Name = JB2.Economy.JbeanStockMarketSettingName.TreasuryRequestor,
                Value = null
            };

            JB2.Settings.JbeanStockMarket.Configure(new JB2.Common.BaseSetting[3] { s1, s2, s3 }, jBeanStockRepo);
    }

        private static void CentralBank_AccountOpened(IBank<JB2.Common.IIDProp<string>, JB2.Economy.Enum.jBeanAccountStatus, ITreasuryRequest, IRequestor, string> bank, IBankAccount<JB2.Common.IIDProp<string>, JB2.Economy.Enum.jBeanAccountStatus> account)
        {
            var market = JB2.Settings.JbeanStockMarket.StockExchange;

            var holder = market.OpenNewAccount(account.AccountNumber);

            Console.WriteLine("New Stock Holder:" + holder.StockExchangeAccountID);
        }

        static void SetupCompanies()
        {
            JbeanStockCompany c1 = JbeanStockCompany.New;
            c1.StockSymbol = "WOOD03";
            c1.Name = "Wooden Fellow";

            StockPrice<string, long> p1 = new StockPrice<string, long>();
            p1.PriceDate = System.DateTime.Now;
            p1.StockExchangeCompanyID = c1.StockExchangeCompanyID;
            p1.Value = 28;

            JB2.Settings.JbeanStockMarket.Repository.Save(p1);
            JB2.Settings.JbeanStockMarket.Repository.Save(c1);
           

            JbeanStockCompany c2 = JbeanStockCompany.New;
            c2.StockSymbol = "SHEL06";
            c2.Name = "Hard Shell";

            StockPrice<string, long> p2 = new StockPrice<string, long>();
            p2.PriceDate = System.DateTime.Now;
            p2.StockExchangeCompanyID = c2.StockExchangeCompanyID;
            p2.Value = 105;

            JB2.Settings.JbeanStockMarket.Repository.Save(p2);
            JB2.Settings.JbeanStockMarket.Repository.Save(c2);

            JbeanStockCompany c3 = JbeanStockCompany.New;
            c3.StockSymbol = "SOLE02";
            c3.Name = "Helpful Soles";

            StockPrice<string, long> p3 = new StockPrice<string, long>();
            p3.PriceDate = System.DateTime.Now;
            p3.StockExchangeCompanyID = c3.StockExchangeCompanyID;
            p3.Value = 05;

            JB2.Settings.JbeanStockMarket.Repository.Save(p3);

            JB2.Settings.JbeanStockMarket.Repository.Save(c3);

            JbeanStockCompany c4 = JbeanStockCompany.New;
            c4.StockSymbol = "ECHM09";
            c4.Name = "Echo Media";

            StockPrice<string, long> p4 = new StockPrice<string, long>();
            p4.PriceDate = System.DateTime.Now;
            p4.StockExchangeCompanyID = c4.StockExchangeCompanyID;
            p4.Value = 76;

            JB2.Settings.JbeanStockMarket.Repository.Save(p4);

            JB2.Settings.JbeanStockMarket.Repository.Save(c4);
        }


        static void SetupAccounts()
        {
            var bank = JB2.Settings.Jbean.Factory.CentralBank;

            List<jBeanAccount> accounts = new List<jBeanAccount>();

            for(var i=0;i <10; i++)
            {
                var account = (jBeanAccount)bank.OpenNewBankAccount(new JB2.Common.IDNamePair("BluffStreetChar" + i.ToString("D3"), string.Empty));
               
                accounts.Add(account);
                Console.WriteLine(i);
            }
        }


        static void Main(string[] args)
        {

            InitjBean();

            var market = JB2.Settings.JbeanStockMarket.StockExchange;


            JB2.Settings.JbeanStockMarket.Repository.Save(market);

            //SetupCompanies();

            // var company = market.GetCompany("SOLE02");

            SetupAccounts();





            Console.WriteLine(JB2.Settings.JbeanStockMarket.StockExchange.GetID());
            //Console.WriteLine(company.GetCurrentStockValue().ToString());

            //int value = 58;
            //Console.WriteLine(JB2.Info.Project.ID);
            //for(int i=0; i<10; i++)
            //{
            //    value = JB2.Economy.JbeanStockExchange.CalculateNewStockValue(value);
            //    Console.WriteLine(value);
            //}

            Console.ReadLine();
        }
    }
}
