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

        jBeanAccount GetBankAccount(string accountNumber);
        string GetAccountNumberByPlayerID(string playerid);

        JBeanBag GetBalance(string accountNumber);
       

        ServiceResult AddFundsToAccount(ITreasuryNote treasuryNote, string accountNumber);

        ServiceResult RemoveFundsFromAccount(ITreasuryRequest treasuryNote, string accountNumber);

        ServiceResult SaveBankAccount(jBeanAccount account, string playerID);

        ServiceResult SaveBankReceipt(IBankTransactionReceipt receipt);

        #endregion Bank

        #region TreasuryNote

        ServiceResult SaveRequest(ITreasuryRequest request);
        ServiceResult SaveTreasuryNote(ITreasuryNote note, Enum.jBeanTreasureNoteStatus statu);

        //ServiceResult UpdateTreasuryNoteStatus(ITreasuryNote note, Enum.jBeanTreasureNoteStatus status);
        Enum.jBeanTreasureNoteStatus GetTreasuryNoteStatus(ITreasuryNote note);

        IEnumerable<ITreasuryNote> GetTreasuryNotes();
        IEnumerable<ITreasuryNote> GetTreasuryNotesByStatus(Enum.jBeanTreasureNoteStatus status);
        ITreasuryNote GetTreasuryNoteById(string id);
        #endregion TeasuryNote

        #region Token

        ServiceResult SaveToken(jBeanToken token);
        jBeanToken GetTokenById(string id);
        IEnumerable<jBeanToken> GetTokensByTreasuryNote(ITreasuryNote note);

        #endregion Token

        #region Treasury

        IEnumerable<ISetting> GetFactorySettings(string currencyID);
        

        #endregion Treasury

        jBeanTotals GetStats();

        ServiceResult SaveStats(jBeanTotals totals);

        jBeanAppSettings GetApplicationSettings(Identity.IApplication app);

        jBeanAppSettings GetApplicationSettingsByID(string applicationID);


    }
}
    
