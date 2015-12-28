using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public class jBeanCentralBank : JB2.Common.IDNamePair, IBank
    {
        #region Fields
        private IJBeanRepository _repo;
        private ITreasury _treasury;
        #endregion Fields

        public IBankAccount GetBankAccount(object accountHolder)
        {
            string playerID = "0";
            Type type = accountHolder.GetType();

            if (type == typeof(string))
                playerID = accountHolder.ToString();

            return new jBeanAccount(_repo.GetAccountNumberByPlayerID(playerID));          
        }

        public float CheckBalance(IBankAccount account)
        {
            return new JBeanBag(50, 0, 0);
        }

        public jBeanCentralBank(ITreasury treasury, IJBeanRepository repo)
        {
            _repo = repo;
            _treasury = treasury;          
        }

        public IBankTransactionReceipt Deposit(IBankAccount account, ITreasuryNote treasuryNote)
        {

            IBankTransactionReceipt receipt = null;
            string transNumber = JB2.Common.NewID.Guid();
            //make sure the note is valid and hasn't already been deposite
            bool isValid  = _treasury.IsValidNote(treasuryNote);

            if(!isValid)
            {
                receipt = new jBeanReceipt(transNumber, string.Format("Treasury Note {0} is not valid", treasuryNote.ID), false);
                _repo.SaveBankReceipt(receipt);
                return receipt;
            }

            Enum.jBeanTreasureNoteStatus noteStatus = _repo.GetTreasuryNoteStatus(treasuryNote);
            switch(noteStatus)
            {
                case Enum.jBeanTreasureNoteStatus.Issued:
                    //do the deposit
                    JB2.Common.ServiceResult bankTransaction = _repo.AddFundsToAccount(treasuryNote.Amount, account.AccountNumber);
                    //print receipt
                    string message = (bankTransaction == true) ? string.Format("jBeans have successfully been deposited for the amount of {0}", treasuryNote.Amount.ToString())
                                                               : string.Format("jBeans were not desposited for the amount of {0}", treasuryNote.Amount.ToString());
                    receipt = new jBeanReceipt(transNumber, message, bankTransaction);

                    //mark note as deposited
                    _repo.SaveTreasuryNote(treasuryNote, Enum.jBeanTreasureNoteStatus.Deposited);
                    break;
                case Enum.jBeanTreasureNoteStatus.Deposited:
                    receipt = new jBeanReceipt(transNumber, string.Format("Treasury Note ({0}) has already been deposited",treasuryNote.ID),false);
                    break;
                case Enum.jBeanTreasureNoteStatus.Cancelled:
                    receipt = new jBeanReceipt(transNumber, string.Format("Treasury Note ({0}) has previously been cancelled",treasuryNote.ID), false);
                    break;
                case Enum.jBeanTreasureNoteStatus.NotApproved:
                    receipt = new jBeanReceipt(transNumber, string.Format("Treasury Note ({0}) is not approved Note",treasuryNote.ID), false);
                    break;
                case Enum.jBeanTreasureNoteStatus.Unknown:
                    receipt = new jBeanReceipt(transNumber, string.Format("Treasury Note ({0}) is not approved Note",treasuryNote.ID), false);
                    break;
            }
            _repo.SaveBankReceipt(receipt);         
            return receipt;
        }

        public float InterestRate(DateTime dt)
        {
            return 0;
        }

        public long TotalCapital()
        {
            throw new NotImplementedException();
        }

        public IBankTransactionReceipt Withdrawn(IBankAccount account, ITreasuryRequest request)
        {
            IBankTransactionReceipt receipt = null;
            string transNumber = JB2.Common.NewID.Guid();
            var isValid = _treasury.IsValidRequest(request);
            if(!isValid)
                receipt = new jBeanReceipt(transNumber, "Withdraw request is not valid", false);
            else
            {
                _repo.RemoveFundsFromAccount(request.Amount, account.AccountNumber);
                receipt = new jBeanReceipt(transNumber, "Withdraw from account has been successful", true);
            }

            _repo.SaveBankReceipt(receipt);

            return receipt;
            
        }
    }
}
