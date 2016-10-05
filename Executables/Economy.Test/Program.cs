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

        static void InitStockMarket()
        {

            //configure jBean
            var jBeanRepo = new JB2.Economy.Data.jBeanRespostory(JB2.Infrastructure.Storage.BowtieAccount);
            JB2.Common.BaseSetting s = new JB2.Common.BaseSetting()
            {
                ID = JB2.Economy.JbeanSettingName.CurrencyID,
                Name = "CurrencyID",
                Value = JB2.Configuration.GetjBeanCurrencyID()
            };

            JB2.Settings.Jbean.Configure(new JB2.Common.BaseSetting[1] { s }, jBeanRepo);

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




        static void Main(string[] args)
        {

            InitStockMarket();

            var market = JB2.Settings.JbeanStockMarket.StockExchange;


            JB2.Settings.JbeanStockMarket.Repository.Save(market);

            Console.WriteLine(JB2.Settings.JbeanStockMarket.StockExchange.GetID());

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
