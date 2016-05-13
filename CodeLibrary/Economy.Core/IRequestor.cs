using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public interface IRequestor: IRequestor<string>
    {

    }
    public interface IRequestor<T> : JB2.Common.IIDNamePair<T,string>
        where T : IComparable
    {

    }
}
