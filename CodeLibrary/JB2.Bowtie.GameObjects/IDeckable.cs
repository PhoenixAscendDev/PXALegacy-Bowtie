using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.GameObjects
{
    public interface  IDeckable<T> : IComparable<T>
    {
        string FromDeckId { get; set; }
    }
}
