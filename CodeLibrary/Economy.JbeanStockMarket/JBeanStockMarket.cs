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
        protected IJbeanStockMarketRepository _repo;


        #endregion Fields

        #region Constuctor

        public JbeanStockExchange(string exchangeID) : this(exchangeID, JB2.Settings.JbeanStockMarket.Repository)
        {

        }

        public JbeanStockExchange(string exchangeID, IJbeanStockMarketRepository repo)
        {
            this._props = new MetaDataCollection();
            this._lastupdate = System.DateTime.Now;
            this._defaultchangeLastUpdate = true;

            ID = exchangeID;
            _repo = repo;
        }

        #endregion Constructor

        public override bool IsOpen
        {
            get
            {
                return this.GetProperity<bool>("ISOPEN");
            }
        }

        public void SetOpen(bool open)
        {
            this.SetProperty<bool>("ISOPEN", open);
        }

        public override JbeanStockCompany GetCompany(string stockSymbol)
        {
            return _repo.GetCompanyByStockSymbol(stockSymbol);
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
            return _repo.GetStockShareByTranID(transactionID);
        }

        public override IJbeanStockHolder GetShareHolder(string accountID)
        {
            return _repo.GetShareholderByID(accountID);
        }

        public override TradeTransactionNote<string> Trade(string holderAccountID, string stockSymbol, int quantity, TradeType tradeType, long? askPrice = null)
        {
            TradeTransactionNote<string> result = new TradeTransactionNote<string>();
            result.AccountID = holderAccountID;

            JbeanStockCompany company = _repo.GetCompanyByStockSymbol(stockSymbol);
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
                switch (result.TradeType)
                {
                    case TradeType.Sell:
                        sell(result);
                        break;
                    case TradeType.Buy:
                        buy(result);
                        break;
                }

                return result;
            }
            else
            {
                throw new Exception();
            }

        }

        private void sell(TradeTransactionNote<string> note)
        {
             try
            {
                //get jBean Bank Account info
               

            //get jBean Bank Account info
            IJbeanStockHolder holder = _repo.GetShareholderByID(note.AccountID);
                var bankAccount = holder.GetjBeanAccount();

                //get jBean Central Bank
                var bank = JB2.Settings.Jbean.Factory.CentralBank;

                //create jBean Treasury Note
                var treasuryNote = JbeanTreasuryNote.NewNote(note.GetTotalCost(), JB2.Settings.JbeanStockMarket.JBeanTreasuryRequestor);

                var result = bank.Deposit(bankAccount, treasuryNote);
            }
            catch (Exception ex)
            {
                note.IsComplete = new ServiceResult(new TradeTransationException(TradeType.Sell));
                note.Message = "Error:" + ex.Message;
            }
            finally
            {
                JB2.Settings.JbeanStockMarket.Repository.Save(note);
            }

        }

        private void buy(TradeTransactionNote<string> note)
        {
            try
            {
                //get jBean Central Bank
                var bank = JB2.Settings.Jbean.Factory.CentralBank;

                //get jBean Bank Account info
                IJbeanStockHolder holder = JB2.Settings.JbeanStockMarket.Repository.GetShareholderByID(note.AccountID);
                var bankAccount = holder.GetjBeanAccount();

                //create a TreasuryRequest
                var request = new TreasuryRequest();
                request.Amount = note.GetTotalCost();
                request.RequestDate = DateTime.Now;
                request.Requestor = JB2.Settings.JbeanStockMarket.JBeanTreasuryRequestor;
                request.VerificationKey = JB2.Settings.JbeanStockMarket.JBeanTreasuryVerificationKey;

                var result = bank.Withdrawn(bankAccount, request);
            }
            catch (Exception ex)
            {
                note.IsComplete = new ServiceResult(new TradeTransationException(TradeType.Sell));
                note.Message = "Error:" + ex.Message;
            }
            finally
            {
                JB2.Settings.JbeanStockMarket.Repository.Save(note);
            }
        }

        public override ServiceResult ValidateTrade(TradeTransactionNote<string> note)
        {
            ServiceResult result = true;

            var checksum = GenerateTradeCheckSum(note);
            if (checksum != note.CheckSum)
                return new ServiceResult(new InvalidChecksumException());



            long fundsNeeded = note.SharePrice * note.ShareCount;
            IJbeanStockHolder holder = _repo.GetShareholderByID(note.AccountID);
            JbeanStockCompany company = _repo.GetCompanyByID(note.CompanyID);
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
            string url = string.Format(urlkey, note.TransactionID,
                                                note.AccountID,
                                                note.TradeType.ToString(),
                                                note.CompanyID,
                                                note.ShareCount.ToString(),
                                                note.SharePrice.Amount.ToString());
            return JB2.Common.NewID.UriHash(new Uri(url));
        }

        public static ushort RNG
        {
            get
            {
                return JB2.Info.Project.GetRNG();
            }
        }
        public static int CalculateNewStockValue(int currentValue)
        {

            int newValue = currentValue;
            //dice1 used to determine size of change
            byte dice1 = JB2.Common.RNG.Dice(6, JbeanStockExchange.RNG);
            //dice2 used to determine pos/neg change
            byte dice2 = JB2.Common.RNG.Dice(6, JbeanStockExchange.RNG);
            //sizefaces is used to determine the number of faces on dice for change
            byte sizefaces = 0;
            int percentChange = 0;


            bool neg = false;


            switch (dice1)
            {
                case 2:
                case 3:
                case 4:
                default:
                    sizefaces = 25;
                    sizefaces = 25;
                    break;
                case 5:
                case 6:
                    sizefaces = 75;
                    break;
            }

            if (sizefaces > 0)
                percentChange = JB2.Common.RNG.Dice(sizefaces, JbeanStockExchange.RNG);

            switch (dice2)
            {
                case 1:
                case 3:
                case 5:
                    neg = true;
                    break;
                case 2:
                case 4:
                case 6:
                default:
                    neg = false;
                    break;
            }



            int change = (int)Math.Round((double)(percentChange / 100.00) * (double)Math.Abs(currentValue));

            newValue = neg == true ? (currentValue + (-1 * change)) : (currentValue + change);

            return newValue;

        }

        public IJbeanStockHolder CreateNewShareHolder(string bankAccountID)
        {
            JbeanStockHolder holder = JbeanStockHolder.New;
            holder.BankAccountID = bankAccountID;

            try
            {
                var repo = JB2.Settings.JbeanStockMarket.Repository;

                repo.Save(holder);

                var testkey = holder.StockExchangeAccountID;

                var holder2 = repo.GetShareholderByID(testkey);

                if (holder2.StockExchangeAccountID != holder.StockExchangeAccountID)
                    throw new Exception("Account unable to save");

                return holder;


            }
            catch (Exception ex)
            {
                return null;
            }




        }
    }
}
