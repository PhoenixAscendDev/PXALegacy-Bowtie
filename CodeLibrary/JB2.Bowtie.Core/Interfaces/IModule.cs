using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IModule : JB2.Common.IIDNamePair<string,string>, IBowtieObject
    {
        BowtieAPI API { get; set; }

        BowtieMetadata GetPlayerData(string playerID);

        Enum.ModuleStatusType Status { get; set; }

        Enum.ModuleType ModuleType { get; set; }

        IEnumerable<IPlayerInventoryItem> GetPlayerInventory(string playerID);

        
    }
}
