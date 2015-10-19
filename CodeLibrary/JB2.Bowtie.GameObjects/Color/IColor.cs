using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.Enum;

namespace JB2.Bowtie.GameObjects
{
    public interface IColor : JB2.Bowtie.IGameObject
    {
        ColorSetType ColorSet { get; set; }
        string HexValue { get;}
        JB2.Common.JB2Color Color { get; }
    }
}
