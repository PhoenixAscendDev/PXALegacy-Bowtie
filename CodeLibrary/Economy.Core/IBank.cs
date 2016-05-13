using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public interface IBank<TAccountHolder,TAccountStatus,TRequest,TRequestor,TID> :  JB2.Common.IIDNamePair<string,string>
        where TID : IComparable
        where TRequestor : IRequestor<TID>
        where TRequest : ITreasuryRequest<TID,TRequestor>
    {
        IBankAccount<TAccountHolder,TAccountStatus> GetBankAccount(TAccountHolder accountHolder);
        IBankAccount<TAccountHolder,TAccountStatus> OpenNewBankAccount(TAccountHolder accountHolder);

        IBankAccount<TAccountHolder, TAccountStatus> ChangeAccountStatus(IBankAccount<TAccountHolder, TAccountStatus> account, TAccountStatus newStatus);

        string GenerateNewAccountNumber();
        long TotalCapital();
        float InterestRate(DateTime dt);
        IBankTransactionReceipt Deposit(IBankAccount<TAccountHolder,TAccountStatus> account, ITreasuryNote treasuryNote);
        IBankTransactionReceipt Withdrawn(IBankAccount<TAccountHolder,TAccountStatus> account, TRequest request);

        float CheckBalance(IBankAccount<TAccountHolder,TAccountStatus> account);

    }
}
