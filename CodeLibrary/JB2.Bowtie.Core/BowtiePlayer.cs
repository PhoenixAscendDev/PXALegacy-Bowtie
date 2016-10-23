using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class BowtiePlayer : Player, IBowtiePlayer
    {
        #region Fields
        protected Backpack _backpack;

        #endregion Fields

        #region Constructors
        public BowtiePlayer() : base()
        {

        }

        public BowtiePlayer(string id, IEnumerable<IModule> modules) : base(id,modules)
        {
            
        }


        #endregion Constructors


        public override string DisplayName
        {
            get
            {
                return _metadata["DisplayName"].GetValue().StringValue;
            }

            set
            {
                _metadata["DisplayName"].UpdateValue(value);
            }
        }

        public override Backpack GetBackpack()
        {
            Backpack bp = new Backpack(this.GetPlayerID());
            
            foreach(var m in _modules.Values)
            {
                var accesskey = JB2.Settings.Bowtie.CurrentApplication.GetModulePermission(m.GetID()).AccessKey;

                var moduleInventory = m.GetPlayerInventory(this.GetPlayerID(), accesskey);
                foreach(var i in moduleInventory)
                {
                    i.ID = m.GetID() + ":" + i.GetID();
                    bp.Add(i);
                }
            }

            return bp;
        }

    }
}
