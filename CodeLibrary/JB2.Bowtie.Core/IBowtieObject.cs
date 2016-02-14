using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IBowtieObject : JB2.Common.IObject<Enum.BowtieObjectType,string,JB2.Common.ObjectTag>
    {
       string UniqueToken{ get; }





    }
}
