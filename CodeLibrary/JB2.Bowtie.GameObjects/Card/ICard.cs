using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.GameObjects
{
    public interface ICard<Tcode,TCategory>
    {
        Tcode Code { get; }
        string Label { get; }
        TCategory Category { get; }

        int Value { get; set; }
        
    }
}
