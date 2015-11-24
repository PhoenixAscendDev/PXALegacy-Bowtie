using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public interface ITreasuryNote
    {
        string ID { get;}
        long Amount { get;}
        
        object Requestor { get; }          
    }
}
