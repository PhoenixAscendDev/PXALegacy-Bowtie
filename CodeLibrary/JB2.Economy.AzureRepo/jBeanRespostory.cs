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
        #endregion Fields

        public jBeanRespostory(JB2.Common.Data.StorageAccount storageAccount)
        {
            _storage = storageAccount;
        }
   
        public JB2.Common.ServiceResult AddFundsToAccount(ITreasuryNote note, IBankAccount account)
        {
            throw new NotImplementedException();
        }

        public JB2.Common.ServiceResult CancelTreasureNote(ITreasuryNote note)
        {
            throw new NotImplementedException();
        }

        public string GetAccountNumberByPlayerID(string playerid)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IEnumerable<ITreasuryNote> GetTreasuryNotesByStatus(jBeanTreasureNoteStatus status)
        {
            throw new NotImplementedException();
        }

        public jBeanTreasureNoteStatus GetTreasuryNoteStatus(ITreasuryNote note)
        {
            throw new NotImplementedException();
        }

        public ITreasuryNote RemoveFundsFromAccount(ITreasuryRequest request, IBankAccount account)
        {
            throw new NotImplementedException();
        }

        public JB2.Common.ServiceResult SaveBankReceipt(IBankTransactionReceipt receipt)
        {
            throw new NotImplementedException();
        }

        public JB2.Common.ServiceResult SaveRequest(ITreasuryRequest request)
        {
            throw new NotImplementedException();
        }

        public JB2.Common.ServiceResult SaveTreasuryNote(ITreasuryNote note, jBeanTreasureNoteStatus statu)
        {
            throw new NotImplementedException();
        }
    }
}
