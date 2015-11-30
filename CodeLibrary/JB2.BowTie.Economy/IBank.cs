using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public interface IBank :  JB2.Common.IIDNamePair<string,string>
    {
        IBankAccount GetBankAccount(object accountHolder);
        long TotalCapital();
        float InterestRate(DateTime dt);
        IBankTransactionReceipt Deposit(IBankAccount account, ITreasuryNote treasuryNote);
        IBankTransactionReceipt Withdrawn(IBankAccount account, ITreasuryRequest request);

    }
}
