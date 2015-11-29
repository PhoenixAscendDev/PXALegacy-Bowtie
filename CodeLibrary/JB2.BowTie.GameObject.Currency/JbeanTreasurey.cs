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

        public void Cancel(ITreasuryNote treasuryNote)
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

        public ITreasuryNote IssueDeomination(ITreasuryRequest request)
        {
            throw new NotImplementedException();
        }

        private bool IsRequestApproved(ITreasuryRequest request)
        {
            bool result = true;

            //Make sure the requestor is a JB2 Identity Application
            if (request.Requestor.GetType() != typeof(JB2.Identity.IApplication))
                return false;

            try
            {
                var app = request.Requestor as JB2.Identity.IApplication;
                var appSettings = _repo.GetApplicationSettings(app);
                return appSettings.canRequest;
            }
            catch(Exception ex)
            {
                return true;
            }

            









        }
    }



   
}
