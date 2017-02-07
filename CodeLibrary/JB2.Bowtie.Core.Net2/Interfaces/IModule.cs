using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using JB2.Common;

namespace JB2.Bowtie
{
    public interface IModule : JB2.Common.IIDNamePair<string, string>, IBowtieObject
    {
        BowtieAPI API { get; set; }

        BowtieMetadata GetPlayerData(string playerID, ApiKeySecretPair AccessKey);

        Enum.ModuleStatusType Status { get; set; }

        Enum.ModuleType ModuleType { get; set; }

        IEnumerable<IPlayerInventoryItem> GetPlayerInventory(string playerID, ApiKeySecretPair AccessKey);

        IEnumerable<IInventoryItem> InventoryItems { get; set; }

        IEnumerable<string> PlayerDataNames { get; set; }

        ServiceResult SetPlayerData(string playerID, ApiKeySecretPair AccessKey, IMetaData data);

        ServiceResult SetPlayerInventory(string playerID, ApiKeySecretPair AccessKey, IPlayerInventoryItem item);


        



    }
}
