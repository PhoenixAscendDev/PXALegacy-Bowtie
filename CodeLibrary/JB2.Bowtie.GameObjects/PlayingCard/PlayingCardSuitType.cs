using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common.Attributes;

namespace JB2.Bowtie.Enum
{
    
    public enum PlayingCardSuitType
    {
        [ColorHex("FFFFFF")]
        NoSuit = 0,
        [ColorHex("ff0000")]
        Heart,
        [ColorHex("ff0000")]
        Diamond,
        [ColorHex("000000")]
        Spade,
        [ColorHex("000000")]
        Club,
        

    }
}
