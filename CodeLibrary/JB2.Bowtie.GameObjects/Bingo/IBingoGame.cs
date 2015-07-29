using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.GameObjects
{
    public interface IBingoGame<T> : JB2.API.IBaseObject, JB2.Common.IIDNamePair<string, string>
    {
        BingoBall<T>[] CallOrder { get; set; }
        BingoBall<T>[] PreviousCalls { get; }
        T CallCount { get; }
        Enum.BingoType BingoType { get; }
        BingoPatternType PatternType { get; set; }
    }



}
