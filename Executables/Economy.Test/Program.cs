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

            JB2.Common.BaseSetting s = new JB2.Common.BaseSetting()
            {
                ID = JB2.Economy.JbeanStockMarketSettingName.StockExchange,
                Name = JB2.Economy.JbeanStockMarketSettingName.StockExchange,
                Value = new JbeanStockExchange(JB2.Configuration.GetjBeanStockMarketID, jBeanStockRepo);

            };



        }




        static void Main(string[] args)
        {
            int value = 58;
            Console.WriteLine(JB2.Info.Project.ID);
            for(int i=0; i<10; i++)
            {
                value = JB2.Economy.JbeanStockExchange.CalculateNewStockValue(value);
                Console.WriteLine(value);
            }

            Console.ReadLine();
        }
    }
}
