using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.GameObjects
{
    public interface IBingoCard : IBingoCard<byte>
    {

    }

    public interface IBingoCard<T> : JB2.Common.IIDNamePair<string,string>, JB2.API.IBaseObject
    {
        Enum.BingoType BingoType { get;}
        T[,] Cells { get; set; }
        byte[,] CellMarks { get; set; }
        Enum.BingoCardSize CardSize { get; }
        string MarkString { get; }
        System.Collections.BitArray GetMarks();
        
    }
}
