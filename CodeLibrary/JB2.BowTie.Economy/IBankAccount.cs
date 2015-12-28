using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Economy
{
    
    public interface IBankAccount<THolder,TStatus>
    {
        string AccountNumber { get; set; }
        string RoutingNumber { get; set; }
        string Name { get; set; }
        TStatus Status { get; set; }
        THolder AccountHolder { get; set; }
    }
}
