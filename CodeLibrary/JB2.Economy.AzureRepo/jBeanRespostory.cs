using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Economy.Enum;
using Microsoft.WindowsAzure.Storage.Table;

namespace JB2.Economy.Data
{
    public class jBeanRespostory : JB2.Economy.IJBeanRepository
    {
        #region Fields
        private JB2.Common.Data.StorageAccount _storage;
        private JB2.Common.Data.AzureTableRepository _jbeanRepo;
        private JB2.Common.Data.AzureTableRepository _tranlogRepo;
        #endregion Fields


        #region Token

        public jBeanToken GetTokenById(string id)
        {
            var e = _jbeanRepo.GetEntity<TokenEntity>("token:jbean", "id:" + id);
            return convertTokenFromentity(e);
        }

        public IEnumerable<jBeanToken> GetTokensByTreasuryNote(ITreasuryNote note)
        {
            var e = _jbeanRepo.GetByRowKeyStartWith<TokenEntity>("token:jbean_" + note.ID, "id:", 1000);

            var result = new List<jBeanToken>(e.Count());
            foreach(TokenEntity token in e )
            {
                result.Add(convertTokenFromentity(token));
            }
            return result;
        }

        public JB2.Common.ServiceResult SaveToken(jBeanToken token)
        {
            var e = _jbeanRepo.GetEntity<TokenEntity>("token:jbean", "id:" + token.GetID());
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

        public jBeanAccount GetBankAccountByPlayerID(string playerid)
        {
            var accountNumber = this.GetAccountNumberByPlayerID(playerid);

            return GetBankAccount(accountNumber);
        }

        public jBeanAccount GetBankAccount(string accountNumber)
        {
            var e = _jbeanRepo.GetEntity<BankAccountEntity>("account:jbean", "accountNumber:" + accountNumber);

            var result = new jBeanAccount(accountNumber);
            result.AccountHolder = new JB2.Common.IDNamePair(e.PlayerID, string.Empty);
            result.AccountNumber = e.AccountNumber;
            result.Name = e.Name;
            result.RoutingNumber = e.RoutingNumber;
            result.Status = (Enum.jBeanAccountStatus)System.Enum.Parse(typeof(Enum.jBeanAccountStatus), e.AccountStatus);

            return result;
        }

        public JBeanBag GetBalance(string accountNumber)
        {
            var e = _jbeanRepo.GetEntity<BankAccountEntity>("account:jbean", "accountNumber:" + accountNumber);
            return e.Balance;

        }

        public jBeanRespostory(JB2.Common.Data.StorageAccount storageAccount)
        {
            _storage = storageAccount;
            _jbeanRepo = _storage.GetTable("bankAccounts");
            _tranlogRepo = _storage.GetTable("transLog");
        }
   
        public JB2.Common.ServiceResult AddFundsToAccount(long amount, string accountNumber)
        {
            var e = _jbeanRepo.GetEntity<BankAccountEntity>("account:jbean", "accountnumber:" + accountNumber);

            if(e != null && string.IsNullOrEmpty(e.AccountNumber))
            {
                e.Balance = e.Balance + amount;
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
            var e = _jbeanRepo.GetEntity<BankAccountEntity>("account:jbean", "accountnumber:" + account.AccountNumber);

            if(e == null)
            {
                e = new BankAccountEntity("account:jbean", "accountnumber:" + account.AccountNumber);
                e.AccountStatus = "Active";
                e.Balance = 0;
                e.DateCreated = DateTime.Now;
                e.ID = account.AccountNumber;
                e.PlayerID = playerid;
                e.RoutingNumber = account.RoutingNumber;
                e.Treasury = "jBean";
            }
            else
            {
                e.AccountNumber = account.AccountNumber;
                e.RoutingNumber = account.RoutingNumber;
            }

            var result = saveBankAccount(e);
            return true;

        }

        public JB2.Common.ServiceResult RemoveFundsFromAccount(long amount, string accountNumber)
        {
            if (amount > 0)
                amount = amount * -1;
            return AddFundsToAccount(amount, accountNumber);
        }

        
        public string GetAccountNumberByPlayerID(string playerid)
        {
            var entity = _jbeanRepo.GetEntity<PlayerjBeanAccount>("account:jbean", "player:" + playerid);
            return entity.AccountNumber;
        }

        public jBeanAppSettings GetApplicationSettings(JB2.Identity.IApplication app)
        {
            var e = _jbeanRepo.GetEntity<AppSettingEntity>("applicationSetting:jbean", "id:" + app.ID);

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
            var e = _jbeanRepo.GetEntity<AppSettingEntity>("applicationSetting:jbean", "id:" + appId);

            if(e == null)
            {
                e = new AppSettingEntity("applicationSetting:jbean", "id:" + appId);
                var d = jBeanAppSettings.Default();
                e.CanRequest = d.CanRequest;
                e.ID = appId;
                e.Name = "jBean Application Setting";
                e.ApplicationID = appId;
                e.RequestValidationKey = JB2.Common.NewID.Guid();

            }

            e.CanRequest = settings.CanRequest;
            e.RequestValidationKey = settings.RequestValidationKey;

            _jbeanRepo.Insert<AppSettingEntity>(e, true);

            return true;
        }
        public jBeanTotals GetStats()
        {
            var e = _jbeanRepo.GetEntity<TreasuryStats>("treasury", "jbean");

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
            return _jbeanRepo.GetEntity<TreasuryNoteEntity>("treasuryNote:jbean", "id:" + id);
                      
        }

        public IEnumerable<ITreasuryNote> GetTreasuryNotes()
        {
            var entity = _jbeanRepo.GetByPartitionKey<TreasuryNoteEntity>("treasuryNote:jbean",1000);
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
            return _jbeanRepo.ExecuteQuery<TreasuryNoteEntity>(query);
        }

        public jBeanTreasureNoteStatus GetTreasuryNoteStatus(ITreasuryNote note)
        {
            var e = _jbeanRepo.GetEntity<TreasuryNoteEntity>("treasuryNote:jbean", "id:" + note.ID);

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

        private BankAccountEntity saveBankAccount(BankAccountEntity e)
        {
            e.PartitionKey = "account:jbean";
            e.RowKey = "accountNumber:" + e.AccountNumber;
            _jbeanRepo.Insert<BankAccountEntity>(e, true);

            e.PartitionKey = "account:jbean_" + e.AccountNumber.Substring(0, 2);
            e.RowKey = "accountNumber:" + e.AccountNumber;
            _jbeanRepo.Insert<BankAccountEntity>(e, true);

            e.PartitionKey = "account:jbean_" + e.AccountNumber.Substring(0, 2); ;
            e.RowKey = "player:" + e.PlayerID;
            _jbeanRepo.Insert<BankAccountEntity>(e, true);

            return e;
        }

        private TreasuryNoteEntity saveTreasuryNote(TreasuryNoteEntity e)
        {
            e.PartitionKey = "treasuryNote:jbean";
            e.RowKey = "id:" + e.ID;
            _jbeanRepo.Insert<TreasuryNoteEntity>(e, true);

            return e;
        }

        private ITreasuryRequest saveTreasuryRequest(TreasuryRequestEntity e)
        {
            e.PartitionKey = "treasuryRequest:jbean";
            e.RowKey = "id:" + e.ID;
            _jbeanRepo.Insert<TreasuryRequestEntity>(e, true);

            return e;
        }

        private TokenEntity saveTokenEntity(TokenEntity e)
        {
            e.PartitionKey = "token:jbean";
            e.RowKey = "id:" + e.ID;
            _jbeanRepo.Insert<TokenEntity>(e, true);

            e.PartitionKey = "token:jbean_" + e.TreasuryNoteID;
            e.RowKey = "id:" + e.ID;
            _jbeanRepo.Insert<TokenEntity>(e, true);
            return e;
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
            var e = _jbeanRepo.GetEntity<TreasuryStats>("treasury", "jbean");

            if(e == null)
            {
                e = new TreasuryStats("treasury", "jbean");
                e.AmountIssued = 0;
            }

            e.DateUpdated = DateTime.Now;
            e.AmountIssued = e.AmountIssued + amount;

            _jbeanRepo.Insert<TreasuryStats>(e, true);

            return true;
        }  





    }
}
