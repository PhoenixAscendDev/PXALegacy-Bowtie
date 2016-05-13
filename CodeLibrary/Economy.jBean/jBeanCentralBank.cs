using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Economy.Enum;

namespace JB2.Economy
{
    public class jBeanCentralBank : JB2.Common.IDNamePair, IBank<JB2.Common.IIDProp<string>,JB2.Economy.Enum.jBeanAccountStatus,ITreasuryRequest,IRequestor,string>
    {
        #region Fields
        private IJBeanRepository _repo;
        private ITreasury _treasury;
        #endregion Fields

        #region Constructors
        public jBeanCentralBank(ITreasury treasury, IJBeanRepository repo)
        {
            _repo = repo;
            _treasury = treasury;          
        }

        #endregion Constructors

        public string GenerateNewAccountNumber()
        {
            return JB2.Common.NewID.Guid();
        }
        public IBankAccount<IIDProp<string>,jBeanAccountStatus> GetBankAccount(IIDProp<string> accountHolder)
        {
            var playerID = accountHolder.GetID();
            return _repo.GetBankAccountByPlayerID(accountHolder.GetID());
        }

        public IBankAccount<IIDProp<string>, jBeanAccountStatus> ChangeAccountStatus(IBankAccount<IIDProp<string>, jBeanAccountStatus> account,jBeanAccountStatus newStatus)
        {
            var e = _repo.GetBankAccount(account.AccountNumber);
            e.Status = newStatus;

            _repo.SaveBankAccount(e, e.AccountHolder.GetID());

            return e;
        }

        public IBankAccount<IIDProp<string>, jBeanAccountStatus> OpenNewBankAccount(IIDProp<string> accountHolder)
        {
            //check to make sure player doesn't already have account
            if( string.IsNullOrEmpty(this.GetBankAccount(accountHolder).AccountNumber))
            {
                string accountNumber = this.GenerateNewAccountNumber();
                var newAccount = new jBeanAccount(accountNumber);
                _repo.SaveBankAccount(newAccount, accountHolder.GetID());

                return _repo.GetBankAccount(accountNumber);
            }

            return _repo.GetBankAccountByPlayerID(accountHolder.GetID());
           
        }

        public float CheckBalance(IBankAccount<IIDProp<string>, jBeanAccountStatus> account)
        {
            return _repo.GetBalance(account.AccountNumber);
        }

        public IBankTransactionReceipt Deposit(IBankAccount<IIDProp<string>, jBeanAccountStatus> account, ITreasuryNote treasuryNote)
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
                    JB2.Common.ServiceResult bankTransaction = _repo.AddFundsToAccount(treasuryNote, account.AccountNumber);

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

        public IBankTransactionReceipt Withdrawn(IBankAccount<IIDProp<string>, jBeanAccountStatus> account, ITreasuryRequest request)
        {
            IBankTransactionReceipt receipt = null;
            string transNumber = JB2.Common.NewID.Guid();
            var isValid = _treasury.IsValidRequest(request);
            if(!isValid)
                receipt = new jBeanReceipt(transNumber, "Withdraw request is not valid", false);
            else
            {
                var balance = _repo.GetBalance(account.AccountNumber);
                if (balance >= request.Amount)
                {
                    _repo.RemoveFundsFromAccount(request, account.AccountNumber);
                    receipt = new jBeanReceipt(transNumber, "Withdraw from account has been successful", true);
                }
                else
                {
                    receipt = new jBeanReceipt(transNumber, "Withdraw from account cancelled: Insufficient Funds", false);
                }
            }

            _repo.SaveBankReceipt(receipt);

            return receipt;
            
        }
    }
}
