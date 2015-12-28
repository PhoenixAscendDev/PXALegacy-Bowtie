using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Economy.Enum;

namespace JB2.Economy.Data
{
    public class jBeanRespostory : JB2.Economy.IJBeanRepository
    {
        #region Fields
        private JB2.Common.Data.StorageAccount _storage;
        private JB2.Common.Data.AzureTableRepository _jbeanRepo;
        private JB2.Common.Data.AzureTableRepository _tranlogRepo;
        #endregion Fields

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

        public JB2.Common.ServiceResult RemoveFundsFromAccount(long amount, string accountNumber)
        {
            if (amount > 0)
                amount = amount * -1;
            return AddFundsToAccount(amount, accountNumber);
        }

        public JB2.Common.ServiceResult CancelTreasureNote(ITreasuryNote note)
        {
            throw new NotImplementedException();
        }

        public string GetAccountNumberByPlayerID(string playerid)
        {
            var entity = _jbeanRepo.GetEntity<PlayerjBeanAccount>("account:jbean", "player:" + playerid);
            return entity.AccountNumber;
        }

        public jBeanAppSettings GetApplicationSettings(JB2.Identity.IApplication app)
        {
            throw new NotImplementedException();
        }

        public jBeanTotals GetStats()
        {
            throw new NotImplementedException();
        }

        public ITreasuryNote GetTreasuryNoteById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITreasuryNote> GetTreasuryNotes()
        {
            var entity = _jbeanRepo.GetByPartitionKey<TreasuryNoteEntity>("treasuryNote:jbean",1000);
            return entity;
        }

        public IEnumerable<ITreasuryNote> GetTreasuryNotesByStatus(jBeanTreasureNoteStatus status)
        {
            throw new NotImplementedException();
        }

        public jBeanTreasureNoteStatus GetTreasuryNoteStatus(ITreasuryNote note)
        {
            throw new NotImplementedException();
        }


        public JB2.Common.ServiceResult SaveBankReceipt(IBankTransactionReceipt receipt)
        {
            return true;
        }

        public JB2.Common.ServiceResult SaveRequest(ITreasuryRequest request)
        {
            throw new NotImplementedException();
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





    }
}
