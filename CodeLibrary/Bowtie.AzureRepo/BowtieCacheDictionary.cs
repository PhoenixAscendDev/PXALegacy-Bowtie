using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.Azure
{
    public class BowtieCacheDictionary<Tentity> : JB2.Common.ExpirableDictionary<string,Tentity>
    {
    }
}
