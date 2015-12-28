using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public interface IBank<TAccountHolder,TAccountStatus> :  JB2.Common.IIDNamePair<string,string>
    {
        IBankAccount<TAccountHolder,TAccountStatus> GetBankAccount(TAccountHolder accountHolder);
        IBankAccount<TAccountHolder,TAccountStatus> OpenNewBankAccount(TAccountHolder accountHolder);

        IBankAccount<TAccountHolder, TAccountStatus> ChangeAccountStatus(IBankAccount<TAccountHolder, TAccountStatus> account, TAccountStatus newStatus);

        string GenerateNewAccountNumber();
        long TotalCapital();
        float InterestRate(DateTime dt);
        IBankTransactionReceipt Deposit(IBankAccount<TAccountHolder,TAccountStatus> account, ITreasuryNote treasuryNote);
        IBankTransactionReceipt Withdrawn(IBankAccount<TAccountHolder,TAccountStatus> account, ITreasuryRequest request);

        float CheckBalance(IBankAccount<TAccountHolder,TAccountStatus> account);

    }
}
