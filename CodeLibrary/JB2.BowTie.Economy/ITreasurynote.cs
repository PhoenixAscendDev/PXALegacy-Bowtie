using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Economy
{
    public interface ITreasurynote
    {
        string ID { get;}
        long Amount { get;}
        
        string Requestor { get; }

        void Cancel();
        void Deposit(IBankAccount account);

        
    }
}
