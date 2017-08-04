using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;

namespace JB2.Bowtie
{
    public class GenericSystem : JB2.Common.IDNamePair<string, string>, ISystem
    {

        public GenericSystem()
        {

        }

        public GenericSystem(Enum.SystemType type)
        {
            SystemType = type;
        }
        public virtual SystemType SystemType { get; set; }      
    }
}
