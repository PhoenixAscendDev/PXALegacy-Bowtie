using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Economy
{
    public interface IJBeanRepository
    {
        #region Bank

        jBeanAccount GetBankAccountByPlayerID(string id);
        string GetAccountNumberByPlayerID(string playerid);

        ServiceResult AddFundsToAccount(long amount, string accountNumber);

        ServiceResult RemoveFundsFromAccount(long amount, string accountNumber);

        ServiceResult SaveBankAccount(jBeanAccount account, string playerID);

        ServiceResult SaveBankReceipt(IBankTransactionReceipt receipt);

        #endregion Bank

        #region TreasuryNote
        ServiceResult SaveTreasuryNote(ITreasuryNote note, Enum.jBeanTreasureNoteStatus statu);

        //ServiceResult UpdateTreasuryNoteStatus(ITreasuryNote note, Enum.jBeanTreasureNoteStatus status);
        Enum.jBeanTreasureNoteStatus GetTreasuryNoteStatus(ITreasuryNote note);

        IEnumerable<ITreasuryNote> GetTreasuryNotes();
        IEnumerable<ITreasuryNote> GetTreasuryNotesByStatus(Enum.jBeanTreasureNoteStatus status);
        ITreasuryNote GetTreasuryNoteById(string id);
        #endregion TeasuryNote



        jBeanTotals GetStats();

        jBeanAppSettings GetApplicationSettings(Identity.IApplication app);

        ServiceResult SaveRequest(ITreasuryRequest request);
    }
}
    
