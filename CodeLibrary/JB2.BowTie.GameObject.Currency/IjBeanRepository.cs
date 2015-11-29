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

        ITreasuryNote CreateTreasuryNote(long amount, object requestor);

        ServiceResult SaveTreasuryNote(ITreasuryNote note, string status);
        Enum.jBeanTreasureNoteStatus GetTreasuryNoteStatus(ITreasuryNote note);

        ServiceResult CancelTreasureNote(ITreasuryNote note);
        IEnumerable<ITreasuryNote> GetTreasuryNotes();
        IEnumerable<ITreasuryNote> GetTreasuryNotesByStatus(Enum.jBeanTreasureNoteStatus status);

        jBeanTotals GetStats();

        jBeanAppSettings GetApplicationSettings(Identity.IApplication app);
    }
}
    
