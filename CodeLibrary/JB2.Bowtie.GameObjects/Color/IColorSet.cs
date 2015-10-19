using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.GameObjects
{
    public interface IColorSet : JB2.Bowtie.IGameObject
    {
        IColor[] Colors { get;}

        int Count { get; }

        JB2.Bowtie.Enum.ColorSetType Type { get; set; }

        IColor FindColorByName(string name);

        IColor FindColorByHex(string hexString);

        JB2.Common.ServiceResult AddColor(IColor c);
        JB2.Common.ServiceResult RemoveColor(IColor c);
    }
}
