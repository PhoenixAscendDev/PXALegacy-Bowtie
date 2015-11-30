using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public class jBeanCentralBank : JB2.Common.IDNamePair, IBank
    {
       

        public IBankTransactionReceipt Deposit(IBankAccount account, ITreasuryNote treasuryNote)
        {
            throw new NotImplementedException();
        }

       

        public float InterestRate(DateTime dt)
        {
            throw new NotImplementedException();
        }

        public long TotalCapital()
        {
            throw new NotImplementedException();
        }

        public IBankTransactionReceipt Withdrawn(IBankAccount account, ITreasuryRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
