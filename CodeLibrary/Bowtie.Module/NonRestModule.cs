using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
