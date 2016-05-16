using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Economy;
using JB2.Common; 
namespace JB2.Economy
{
    public class JbeanTreasury : JB2.Common.IDNamePair, ITreasury
    {
        #region Fields
        private IJBeanRepository _repo;
        #endregion Fields

        #region Constructor

        public JbeanTreasury() : base(string.Empty,string.Empty)
        {
           
        }

        public JbeanTreasury(IJBeanRepository repo)
        {
            _repo = repo;
        }

        #endregion Constructor

        public void CancelNote(ITreasuryNote treasuryNote)
        {
            var code = _repo.GetTreasuryNoteStatus(treasuryNote);
            switch(code)
            {
                case Enum.jBeanTreasureNoteStatus.Issued:
                case Enum.jBeanTreasureNoteStatus.Unknown:
                    _repo.SaveTreasuryNote(treasuryNote, Enum.jBeanTreasureNoteStatus.Cancelled);
                    break;
            }          
        }

        public long GetAmountIssued()
        {
            var totals = _repo.GetStats();
            return totals.AmountIssued;
        }

        public ITreasuryNote IssueNote(ITreasuryRequest request)
        {
            ITreasuryNote result = null;
            _repo.SaveRequest(request);

            if (IsValidRequest(request))
            {
                //JbeanTreasuryNote.NewNote(request.Amount, request.Requestor as JB2.Identity.IApplication);
                //marks the note status as "Issued"
                result = JbeanTreasuryNote.NewNote(request.Amount, request.Requestor as IIDNamePair<string,string>);
                _repo.SaveTreasuryNote(result, Enum.jBeanTreasureNoteStatus.Issued);

                jBeanTotals stats = _repo.GetStats();
                stats.AmountIssued = stats.AmountIssued + request.Amount;
                _repo.SaveStats(stats);        
            }
            else
            {
                result = JbeanTreasuryNote.NewNote(0, request.Requestor as IIDNamePair<string,string>);
            }

            return result;
        }
        public bool IsValidRequest(ITreasuryRequest request)
        {

            //first lets make sure the requestor is jbean application 

            var appsettings = _repo.GetApplicationSettingsByID(request.Requestor.GetID());

            if (appsettings == null)
                return false;

            try
            {
                return ((appsettings.CanRequest) && (appsettings.RequestValidationKey == request.VerificationKey));
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool IsValidNote(ITreasuryNote note)
        {
            //get note from repository to verify
            var verifyNote = _repo.GetTreasuryNoteById(note.ID);

            if (verifyNote == null)
                return false;

            if ((note.ID == verifyNote.ID) && (note.Amount == verifyNote.Amount))
                return true;
            else
                return false;

        }

    }  
}
