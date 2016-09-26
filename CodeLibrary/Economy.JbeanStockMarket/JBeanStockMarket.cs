using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;

namespace JB2.Economy
{
    public class JbeanStockExchange : StockExchange<JbeanStockCompany, IJbeanStockHolder, JbeanStockShare, string, long>
    {
        #region Fields
        

        #endregion Fields

        #region Constuctor
        public JbeanStockExchange(string exchangeID)
        {
            ID = exchangeID;
            
        }

        #endregion Constructor

        public override bool IsOpen
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override JbeanStockCompany GetCompany(string stockSymbol)
        {
            return JB2.Settings.JbeanStockMarket.Repository.RetrieveCompanyBySymbol(stockSymbol);
        }

        public override DateTime GetLastOpenDate()
        {

            throw new NotImplementedException();
        }

        public override DateTime GetLastTradeTime()
        {
            throw new NotImplementedException();
        }

        public override DateTime GetNextCloseTime()
        {
            throw new NotImplementedException();
        }

        public override JbeanStockShare GetShare(string transactionID)
        {
            return JB2.Settings.JbeanStockMarket.Repository.RetrieveShareByTranID(transactionID);
        }

        public override IJbeanStockHolder GetShareHolder(string accountID)
        {
            return JB2.Settings.JbeanStockMarket.Repository.RetrieveShareHolder(accountID);
        }

        public override TradeTransactionNote<string> Trade(string holderAccountID, string stockSymbol, int quantity, TradeType tradeType, long? askPrice = null)
        {
            TradeTransactionNote<string> result = new TradeTransactionNote<string>();
            result.AccountID = holderAccountID;

            JbeanStockCompany company = JB2.Settings.JbeanStockMarket.Repository.RetrieveCompanyBySymbol(stockSymbol);
            if (askPrice == null)
                askPrice = company.GetCurrentStockValue();

            result.CompanyID = company.StockExchangeCompanyID;
            result.SharePrice = new CurrencyAmountPair(JB2.Settings.JbeanStockMarket.DefaultCurrency, (double)askPrice);
            result.ShareCount = quantity;
            result.TradeType = tradeType;

            string checksum = GenerateTradeCheckSum(result);

            result.CheckSum = checksum;

            if (ValidateTrade(result))
            {
                switch(result.TradeType)
                {
                    case TradeType.Sell:
                        Sell(result);
                        break;
                    case TradeType.Buy:
                        Buy(result);
                }
                
                return result;
            }
            else
            {
                throw new Exception();
            }

        }

        private bool Sell(TradeTransactionNote<string> note)
        {
            JB2.Settings.JbeanStockMarket.Repository.Save(note);
            return true;
        }

        private bool Buy(TradeTransactionNote<string> note)
        {
            JB2.Settings.JbeanStockMarket.Repository.Save(note);
            return true;
        }

        public override ServiceResult ValidateTrade(TradeTransactionNote<string> note)
        {
            ServiceResult result = true;

            var checksum = GenerateTradeCheckSum(note);
            if (checksum != note.CheckSum)
                return new ServiceResult(new InvalidChecksumException());



            long fundsNeeded = note.SharePrice * note.ShareCount;
            IJbeanStockHolder holder = JB2.Settings.JbeanStockMarket.Repository.RetrieveShareHolder(note.AccountID);
            JbeanStockCompany  company = JB2.Settings.JbeanStockMarket.Repository.RetrieveCompany(note.CompanyID);
            switch (note.TradeType)
            {
                
                case TradeType.Buy:
                    //check to make sure holder as the funds                                    
                    if (holder.GetFundsAvalable() < fundsNeeded)
                        result = new ServiceResult(new InsufficientFundsException(fundsNeeded));
                    break;
                case TradeType.Sell:
                    //check to make sure holder has the shares to sell
                    var shares = holder.GetShareCount(company.StockExchangeCompanyID);
                    if (shares < note.ShareCount)
                        result = new ServiceResult(new InsufficientStockSharesException(note.ShareCount));
                    break;
            }

            return result;
        }

        public string GenerateTradeCheckSum(TradeTransactionNote<string> note)
        {
            string urlkey = "http://www.jbsquared.com?TranID={0}&AID={1}&type={2}&c={3}&count={4}&price={5}";
            string url = string.Format(urlkey,  note.TransactionID, 
                                                note.AccountID, 
                                                note.TradeType.ToString(), 
                                                note.CompanyID,
                                                note.ShareCount.ToString(), 
                                                note.SharePrice.Amount.ToString());
            return JB2.Common.NewID.UriHash(new Uri(url));
        }

        
    }
}
