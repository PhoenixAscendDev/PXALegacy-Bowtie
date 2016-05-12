using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using JB2.Economy.Enum;
using Microsoft.WindowsAzure.Storage.Table;
using JB2.Common;

namespace JB2.Economy.Data
{
    public class jBeanRespostory : JB2.Economy.IJBeanRepository
    {
        #region Fields
        private JB2.Common.Data.StorageAccount _storage;
        private JB2.Common.Data.AzureTableRepository _jbeanTable;
        private JB2.Common.Data.AzureTableRepository _tranlogTable;
        private JB2.Common.Data.AzureTableRepository _tokenTable;
        #endregion Fields

        #region Constructor
        public jBeanRespostory(JB2.Common.Data.StorageAccount storageAccount)
        {
            _storage = storageAccount;
            _jbeanTable = _storage.GetTable("economy");
            _tranlogTable = _storage.GetTable("economyLog");
            _tokenTable = _storage.GetTable("economyTokens");
        }
        #endregion Constructor


        #region Token

        public jBeanToken GetTokenById(string id)
        {
            var e = _tokenTable.GetEntity<TokenEntity>("token:jbean", "id:" + id);
            return convertTokenFromentity(e);
        }

        public IEnumerable<jBeanToken> GetTokensByTreasuryNote(ITreasuryNote note)
        {
            var e = _tokenTable.GetByRowKeyStartWith<TokenEntity>("token:jbean_" + note.ID, "id:", 1000);

            var result = new List<jBeanToken>(e.Count());
            foreach(TokenEntity token in e )
            {
                result.Add(convertTokenFromentity(token));
            }
            return result;
        }

        public JB2.Common.ServiceResult SaveToken(jBeanToken token)
        {
            var e = _tokenTable.GetEntity<TokenEntity>("token:jbean", "id:" + token.GetID());
            if(string.IsNullOrEmpty(e.GetID()) )
            {
                e = new TokenEntity();
                e.DateCreated = DateTime.Now;              
                e.Treasury = "jBean";
                
            }
            e.ID = token.ID;
            e.Name = token.Name;           
            e.TreasuryNoteID = token.TreasuryNoteId;
            e.Value = Convert.ToInt32(token.Value);

            saveTokenEntity(e);

            return true;




        }

        #endregion Token

        public string GenerateTokenID()
        {
            return JB2.Common.NewID.Guid();
        }

        public jBeanAccount GetBankAccountByPlayerID(string playerid)
        {
            var accountNumber = this.GetAccountNumberByPlayerID(playerid);

            return GetBankAccount(accountNumber);
        }

        public jBeanAccount GetBankAccount(string accountNumber)
        {
            var e = _jbeanTable.GetEntity<BankAccountEntity>("account:jbean", "accountNumber:" + accountNumber);

            if (e != null)
            {
                var result = new jBeanAccount(accountNumber);
                result.AccountHolder = new JB2.Common.IDNamePair(e.PlayerID, string.Empty);
                result.AccountNumber = e.AccountNumber;
                result.Name = e.Name;
                result.RoutingNumber = e.RoutingNumber;
                result.Status = (Enum.jBeanAccountStatus)System.Enum.Parse(typeof(Enum.jBeanAccountStatus), e.AccountStatus);
                return result;
            }
            return new jBeanAccount(string.Empty);

            
        }

        public JBeanBag GetBalance(string accountNumber)
        {
            var e = _jbeanTable.GetEntity<BankAccountEntity>("account:jbean", "accountNumber:" + accountNumber);
            return e.Balance;

        }


        private bool createTokensAsync(ITreasuryNote note,string accountNumber)
        {
            for (int i = 1; i <= note.Amount; i++)
            {
                TokenEntity t = new TokenEntity();
                t.Value = 1;
                t.Treasury = "jBean";
                t.TreasuryNoteID = note.ID;
                t.ID = GenerateTokenID();
                t.Name = "Kidney jBean";
                t.BankAccountNumber = accountNumber;
                t.DateCreated = DateTime.Now;
                saveTokenEntity(t);
            }

            return true;
        }


        private bool removeTokenFromAccount(int amount,string accountNumber)
        {
            var tokens = getTokensbyAccountNumber(accountNumber, amount);
            foreach (TokenEntity e in tokens)
            {
                try
                {
                    _tokenTable.Delete<TokenEntity>("token:jbean:bankaccount_" + accountNumber, "id:" + e.ID);
                }
                catch (Exception ex)
                {
                    ///TODO: 
                }
                e.BankAccountNumber = string.Empty;
                this.saveTokenEntity(e);
            }
            return true;

        }
        public JB2.Common.ServiceResult AddFundsToAccount(ITreasuryNote note, string accountNumber)
        {

            Task.Factory.StartNew(() => createTokensAsync(note, accountNumber));
            //Thread thread = new Thread(createTokensAsync(note, accountNumber));
            //thread.Start();
            //Task<bool> tokensCreated = createTokensAsync(note, accountNumber);
            //create jBeanTokens and associate them with the account
            //for (int i = 1; i <= note.Amount; i++)
            //{
            //    TokenEntity t = new TokenEntity();
            //    t.Value = 1;
            //    t.Treasury = "jBean";
            //    t.TreasuryNoteID = note.ID;
            //    t.ID = GenerateTokenID();
            //    t.Name = "Kidney jBean";
            //    t.BankAccountNumber = accountNumber;
            //    t.DateCreated = DateTime.Now;
            //    saveTokenEntity(t);
            //}

            //modify the balance
            var e = _jbeanTable.GetEntity<BankAccountEntity>("account:jbean", "accountNumber:" + accountNumber);
            if (e != null && !string.IsNullOrEmpty(e.AccountNumber))
            {
                e.Balance = e.Balance + (int)note.Amount;
                e.LastTransactionDate = DateTime.Now;
                this.saveBankAccount(e);
            }
            else
            {
                return false;
            }
            return true;
        }

        public JB2.Common.ServiceResult SaveBankAccount(jBeanAccount account, string playerid)
        {
            var e = _jbeanTable.GetEntity<BankAccountEntity>("account:jbean", "accountnumber:" + account.AccountNumber);

            if(e == null)
            {
                e = new BankAccountEntity("account:jbean", "accountnumber:" + account.AccountNumber);
                e.AccountStatus = Enum.jBeanAccountStatus.Open.ToString();
                e.Balance = 0;
                e.DateCreated = DateTime.Now;
                e.ID = account.AccountNumber;
                e.AccountNumber = account.AccountNumber;
                e.PlayerID = playerid;
                e.RoutingNumber = account.RoutingNumber;
                e.Treasury = "jBean";
                e.Name = "jBean Bank Account";
                e.LastTransactionDate = DateTime.Now;
            }
            else
            {
                e.AccountNumber = account.AccountNumber;
                e.RoutingNumber = account.RoutingNumber;
            }

            var result = saveBankAccount(e);
            return true;

        }

        public JB2.Common.ServiceResult RemoveFundsFromAccount(ITreasuryRequest request, string accountNumber)
        {
            //first get the jBean Tokens and remove them from the account
            Task.Factory.StartNew(() => removeTokenFromAccount((int)request.Amount, accountNumber));
            //update the balance
            var accountEntity = getBankAccountByAccountNumber(accountNumber);
            //var accountEntity = _jbeanTable.Get(new BankAccountEntity("account:jbean", "accountNumber:" + accountNumber);



            
             var newBalance = (accountEntity.Balance - request.Amount);
            accountEntity.Balance = newBalance < 0 ? 0 : (int)newBalance;
            accountEntity.LastTransactionDate = DateTime.Now;
            saveBankAccount(accountEntity);
            return true;
           
        }      
        public string GetAccountNumberByPlayerID(string playerid)
        {
            var entity = _jbeanTable.GetEntity<PlayerjBeanAccount>("account:jbean", "player:" + playerid);
            return entity != null ? entity.AccountNumber : string.Empty;
        }

        public jBeanAppSettings GetApplicationSettings(JB2.Identity.IApplication app)
        {
            var e = _jbeanTable.GetEntity<AppSettingEntity>("application:jbean", "id:" + app.ID);

            if(e != null)
            {
                return new jBeanAppSettings()
                {
                    CanRequest = e.CanRequest,
                    RequestValidationKey = e.RequestValidationKey
                };
            }
            else
            {
                return jBeanAppSettings.Default();
            }
            
        }

        public JB2.Common.ServiceResult SaveApplicationSettings(string appId, jBeanAppSettings settings)
        {
            var e = _jbeanTable.GetEntity<AppSettingEntity>("application:jbean", "id:" + appId);

            if(e == null)
            {
                e = new AppSettingEntity("application:jbean", "id:" + appId);
                var d = jBeanAppSettings.Default();
                e.CanRequest = d.CanRequest;
                e.ID = appId;
                e.Name = "jBean Application Setting";
                e.ApplicationID = appId;
                e.RequestValidationKey = JB2.Common.NewID.Guid();

            }

            e.CanRequest = settings.CanRequest;
            e.RequestValidationKey = settings.RequestValidationKey;

            _jbeanTable.Insert<AppSettingEntity>(e, true);

            return true;
        }
        public jBeanTotals GetStats()
        {
            var e = _jbeanTable.GetEntity<TreasuryStats>("treasury", "jbean");

            if (e != null)
            {
                return new jBeanTotals()
                {
                    AmountIssued = e.AmountIssued
                };
            }
            else
            {
                return new jBeanTotals() { AmountIssued = 0 };
            }
        }
        public JB2.Common.ServiceResult  SaveStats(jBeanTotals totals)
        {
            //var e = _jbeanRepo.GetEntity<TreasuryStats>("treasury","jbean")
            return true;
        }

        public ITreasuryNote GetTreasuryNoteById(string id)
        {
            return _jbeanTable.GetEntity<TreasuryNoteEntity>("treasuryNote:jbean", "id:" + id);
                      
        }

        public IEnumerable<ITreasuryNote> GetTreasuryNotes()
        {
            var entity = _jbeanTable.GetByPartitionKey<TreasuryNoteEntity>("treasuryNote:jbean",1000);
            return entity;
        }

        public IEnumerable<ITreasuryNote> GetTreasuryNotesByStatus(jBeanTreasureNoteStatus status)
        {
            var query = new TableQuery<TreasuryNoteEntity>();
            query.Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "treasuryNote:jbean"),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("Status", QueryComparisons.Equal, status.ToString())
                )
            );
            return _jbeanTable.ExecuteQuery<TreasuryNoteEntity>(query);
        }

        public jBeanTreasureNoteStatus GetTreasuryNoteStatus(ITreasuryNote note)
        {
            var e = _jbeanTable.GetEntity<TreasuryNoteEntity>("treasuryNote:jbean", "id:" + note.ID);

            if (e != null)
            {
                return (jBeanTreasureNoteStatus)System.Enum.Parse(typeof(jBeanTreasureNoteStatus), e.Status);
            }
            else
                return jBeanTreasureNoteStatus.Unknown;
        }


        public JB2.Common.ServiceResult SaveBankReceipt(IBankTransactionReceipt receipt)
        {
            return true;
        }
        public JB2.Common.ServiceResult SaveRequest(ITreasuryRequest request)
        {
            var e = new TreasuryRequestEntity();
            e.ID = JB2.Common.NewID.Guid();
            e.Name = "jBean Treasury Request";
            e.RequestDate = request.RequestDate;
            e.RequestorID = request.Requestor.ToString();
            e.Treasury = "jBean";
            e.VerificationKey = string.Empty;
            saveTreasuryRequest(e);

            return true;
        }

        public JB2.Common.ServiceResult SaveTreasuryNote(ITreasuryNote note, jBeanTreasureNoteStatus status)
        {
            var e = new TreasuryNoteEntity();
            e.Treasury = "jBean";
            e.Status = status.ToString();
            e.DateCreated = DateTime.Now;
            e.Amount = note.Amount;
            e.ID = note.ID;
            e.Name = "jBean Treasury Note";
            e.IssuedBy = note.GetRequestor().GetID();
            

            var result = saveTreasuryNote(e);

            return true;
        }

        private BankAccountEntity getBankAccountByAccountNumber(string accountNumber)
        {
            return _jbeanTable.GetEntity<BankAccountEntity>("account:jbean", "accountNumber:" + accountNumber);
        }

        private BankAccountEntity saveBankAccount(BankAccountEntity e)
        {
            e.PartitionKey = "account:jbean";
            e.RowKey = "accountNumber:" + e.AccountNumber;
            _jbeanTable.Insert<BankAccountEntity>(e, true);

            e.PartitionKey = "account:jbean_" + e.AccountNumber.Substring(0, 2);
            e.RowKey = "accountNumber:" + e.AccountNumber;
            _jbeanTable.Insert<BankAccountEntity>(e, true);

            e.PartitionKey = "account:jbean";
            e.RowKey = "player:" + e.PlayerID;
            _jbeanTable.Insert<BankAccountEntity>(e, true);

            return e;
        }

        private TreasuryNoteEntity saveTreasuryNote(TreasuryNoteEntity e)
        {
            e.PartitionKey = "treasuryNote:jbean";
            e.RowKey = "id:" + e.ID;
            _jbeanTable.Insert<TreasuryNoteEntity>(e, true);

            return e;
        }

        private ITreasuryRequest saveTreasuryRequest(TreasuryRequestEntity e)
        {
            e.PartitionKey = "treasuryRequest:jbean";
            e.RowKey = "id:" + e.ID;
            _jbeanTable.Insert<TreasuryRequestEntity>(e, true);

            return e;
        }

        private TokenEntity saveTokenEntity(TokenEntity e)
        {
            e.PartitionKey = "token:jbean";
            e.RowKey = "id:" + e.ID;
            _tokenTable.Insert<TokenEntity>(e, true);

            e.PartitionKey = "token:jbean_" + e.TreasuryNoteID;
            e.RowKey = "id:" + e.ID;
            _tokenTable.Insert<TokenEntity>(e, true);

            if (!string.IsNullOrEmpty(e.BankAccountNumber))
            {
                e.PartitionKey = "token:jbean:bankaccount_" + e.BankAccountNumber;
                e.RowKey = "id:" + e.ID;
                _tokenTable.Insert<TokenEntity>(e, true);
            }
            return e;
        }

        private IEnumerable<TokenEntity> getTokensbyAccountNumber(string accountNumber,int numOfRecords)
        {
            return _tokenTable.GetByRowKeyStartWith<TokenEntity>("token:jbean:bankaccount_" + accountNumber, "id:", numOfRecords);
        }

        private JbeanTreasuryNote convertNoteFromEntity(TreasuryNoteEntity e)
        {
            var result = new JbeanTreasuryNote(e.ID, e.Amount, new JB2.Common.IDNamePair<string,string>(e.IssuedBy,string.Empty));

            return result;

        }

        private jBeanToken convertTokenFromentity(TokenEntity e)
        {
            var result = new jBeanToken();
            result.Value = e.Value;
            result.ID = e.ID;
            result.Name = e.Name;
            result.TokenType = Enum.JBeanTokenType.Kidney;
            result.TreasuryNoteId = e.TreasuryNoteID;

            return result;

        }

        private JB2.Common.ServiceResult IncreaseAmountIssuedStat(long amount)
        {
            var e = _jbeanTable.GetEntity<TreasuryStats>("treasury", "jbean");

            if(e == null)
            {
                e = new TreasuryStats("treasury", "jbean");
                e.AmountIssued = 0;
            }

            e.DateUpdated = DateTime.Now;
            e.AmountIssued = e.AmountIssued + amount;

            _jbeanTable.Insert<TreasuryStats>(e, true);

            return true;
        }

        public IEnumerable<ISetting> GetFactorySettings(string currencyID)
        {
            var settings = new List<ISetting>();



            var ce = _jbeanTable.GetEntity<DynamicTableEntity>("currency", "id:" + currencyID);

            if (ce != null)
            {
                settings.Add(new BaseSetting() { ID = JB2.Economy.JbeanSettingName.CurrencyID, Value = ce["ID"].StringValue });
                settings.Add(new BaseSetting() { ID = JB2.Economy.JbeanSettingName.KidneyFrontImage, Value = ce["Demo1_FrontImage"].StringValue });
                settings.Add(new BaseSetting() { ID = JB2.Economy.JbeanSettingName.KidneyBackImage, Value = ce["Demo1_BackImage"].StringValue });
                settings.Add(new BaseSetting() { ID = JB2.Economy.JbeanSettingName.KidneyValue, Value = ce["Demo1_Value"].Int32Value });
                settings.Add(new BaseSetting() { ID = JB2.Economy.JbeanSettingName.NavyFrontImage, Value = ce["Demo2_FrontImage"].StringValue });
                settings.Add(new BaseSetting() { ID = JB2.Economy.JbeanSettingName.NavyBackImage, Value = ce["Demo2_BackImage"].StringValue });
                settings.Add(new BaseSetting() { ID = JB2.Economy.JbeanSettingName.NavyValue, Value = ce["Demo2_Value"].Int32Value });
                settings.Add(new BaseSetting() { ID = JB2.Economy.JbeanSettingName.PintoFrontImage, Value = ce["Demo3_FrontImage"].StringValue });
                settings.Add(new BaseSetting() { ID = JB2.Economy.JbeanSettingName.PintoBackImage, Value = ce["Demo3_BackImage"].StringValue });
                settings.Add(new BaseSetting() { ID = JB2.Economy.JbeanSettingName.PintoValue, Value = ce["Demo3_Value"].Int32Value });
            }

            var te = _jbeanTable.GetEntity<DynamicTableEntity>("treasury", "id:jBean");

            if (te != null)
            {

                settings.Add(new BaseSetting() { ID = JB2.Economy.JbeanSettingName.AutoFillMinBalance, Value = te["AutoFill_MinBalance"].Int32Value });
                settings.Add(new BaseSetting() { ID = JB2.Economy.JbeanSettingName.AutoFillEnable, Value = te["AutoFill_Enable"].BooleanValue });
                settings.Add(new BaseSetting() { ID = JB2.Economy.JbeanSettingName.AutoFillTime, Value = te["AutoFill_TimeMinutes"].Int32Value });

                settings.Add(new BaseSetting() { ID = JB2.Economy.JbeanSettingName.TreasuryRequestLimit, Value = te["RequestLimit"].Int32Value });
                settings.Add(new BaseSetting() { ID = JB2.Economy.JbeanSettingName.TreasuryRequestLimitCoolDown, Value = te["RequestLimit_CoolDownMinutes"].Int32Value });
            }

            return settings;
        }
    }
}
