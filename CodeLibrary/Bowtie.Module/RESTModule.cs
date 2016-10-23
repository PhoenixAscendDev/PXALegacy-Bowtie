using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;

using JB2.Common;


namespace JB2.Bowtie
{
    public class RESTModule : Module
    {

        #region Constructor
        public RESTModule(string id) : base()
        {
            this.SetProperty<string>("ID", id);
        }

        #endregion Constructor

        public override BowtieMetadata GetPlayerData(string playerID, ApiKeySecretPair accesskey)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IPlayerInventoryItem> GetPlayerInventory(string playerID, ApiKeySecretPair accesskey)
        {
            throw new NotImplementedException();
        }

        public override ServiceResult SetPlayerData(string playerID, ApiKeySecretPair accesskey, IMetaData data)
        {
            throw new NotImplementedException();
        }

        public override ServiceResult SetPlayerInventory(string playerID, ApiKeySecretPair accesskey, IPlayerInventoryItem item)
        {
            throw new NotImplementedException();
        }
    }
}
