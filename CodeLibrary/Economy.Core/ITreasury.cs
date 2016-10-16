using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Economy
{
    public interface ITreasury : IIDNamePair<string,string>
    {

        #region Events
        event Action<ITreasury, ITreasuryNote, ITreasuryRequest> NoteIssued;
        event Action<ITreasury, ITreasuryNote> NoteCancelled;
        #endregion Events


        ITreasuryNote IssueNote(ITreasuryRequest request);

        long GetAmountIssued();

        void CancelNote(ITreasuryNote treasuryNote);

        bool IsValidNote(ITreasuryNote treasuryNote);

        bool IsValidRequest(ITreasuryRequest request);

        




        

             
    }
}
