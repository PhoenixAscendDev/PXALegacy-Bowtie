using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Interfaces
{
    public interface IModule : JB2.Common.IIDNamePair<string,string>
    {
        BowtieAPI API { get; set; }

        BowtieMetadata GetPlayerData { get; set; }
    }
}
