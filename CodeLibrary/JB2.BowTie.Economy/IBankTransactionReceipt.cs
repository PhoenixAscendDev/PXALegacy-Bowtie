using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Economy
{
    public interface IBankTransactionReceipt
    {
        string BankID { get; set; }
        string TransactionNumber { get;}
        string Message { get;}
        bool WasSuccess { get; }

    }
}
