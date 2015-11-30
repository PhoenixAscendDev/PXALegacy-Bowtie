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
        string GetAccountNumberByPlayerID(string playerid);

        ServiceResult AddFundsToAccount(ITreasuryNote note, string accountNumber);

        ITreasuryNote RemoveFundsFromAccount(ITreasuryRequest request, string accountNumber);

        ServiceResult SaveRequest(ITreasuryRequest request);

        ServiceResult SaveTreasuryNote(ITreasuryNote note, Enum.jBeanTreasureNoteStatus statu);

        //ServiceResult UpdateTreasuryNoteStatus(ITreasuryNote note, Enum.jBeanTreasureNoteStatus status);
        Enum.jBeanTreasureNoteStatus GetTreasuryNoteStatus(ITreasuryNote note);

        ServiceResult CancelTreasureNote(ITreasuryNote note);
        IEnumerable<ITreasuryNote> GetTreasuryNotes();
        IEnumerable<ITreasuryNote> GetTreasuryNotesByStatus(Enum.jBeanTreasureNoteStatus status);
        ITreasuryNote GetTreasuryNoteById(string id);


        jBeanTotals GetStats();

        jBeanAppSettings GetApplicationSettings(Identity.IApplication app);
    }
}
    
