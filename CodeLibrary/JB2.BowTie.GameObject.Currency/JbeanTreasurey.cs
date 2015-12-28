using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;
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
                    _repo.CancelTreasureNote(treasuryNote);
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
            }
            else
            {
                result = JbeanTreasuryNote.NewNote(0, request.Requestor as IIDNamePair<string,string>);
            }

            return result;
        }

        public bool IsValidRequest(ITreasuryRequest request)
        {

            //Make sure the requestor is a JB2 Identity Application
            if (request.Requestor.GetType() != typeof(JB2.Identity.IApplication))
                return false;
            try
            {
                //Now make sure the request's validation key matches that of the application and can request
                var app = request.Requestor as JB2.Identity.IApplication;
                var appSettings = _repo.GetApplicationSettings(app);
                return ((appSettings.CanRequest) && (appSettings.RequestValidationKey == request.VerificationKey));
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
