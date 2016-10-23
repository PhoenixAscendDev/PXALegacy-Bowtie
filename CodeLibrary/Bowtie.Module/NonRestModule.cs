using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;

namespace JB2.Bowtie
{
    public class NonRestModule : Module
    {

        #region Fields
        protected IModuleRepository _repo;

        #endregion Fields

        #region Constructor
        public NonRestModule(string id, IModuleRepository repo) : base()
        {
            this.SetProperty<string>("ID", id);
            _repo = repo;
        }

        #endregion Constructor
        public override BowtieMetadata GetPlayerData(string playerID)
        {
            return _repo.GetDataByPlayerID(this.GetID(), playerID);
        }

        public override IEnumerable<IPlayerInventoryItem> GetPlayerInventory(string playerID)
        {
            return _repo.GetInventoryByPlayerID(this.GetID(), playerID);          
        }

        public override ServiceResult SetPlayerInventory(string playerID, IPlayerInventoryItem item)
        {
            return _repo.SavePlayerInventory(this.GetID(), playerID, item);
            
        }

        public override ServiceResult SetPlayerData(string playerID, IMetaData data)
        {
            return _repo.SavePlayerData(this.GetID(),playerID, data);
        }

        #region Static
        public static NonRestModule New
        {
            get
            {
                string id = "m." + JB2.Common.NewID.ShortGuid();
                NonRestModule result = new NonRestModule(id, JB2.Settings.Bowtie.UnitOfWork.ModuleRepository);

                return result;
            }
        }
        #endregion Static
    }
}
