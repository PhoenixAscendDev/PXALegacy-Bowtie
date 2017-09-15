using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Enum
{
    public enum APIAuthorizeState
    {
        Unknown = 0,
        TemporaryBlocked = 1,
        LifelongBan = 2,
        Authorized = 3
    }
}
