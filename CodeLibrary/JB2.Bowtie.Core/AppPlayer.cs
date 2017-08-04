using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;
using JB2.Identity;

namespace JB2.Bowtie
{
    public class ApplicationPlayer : Player, IApplicationable<string>
    {
        #region Fields

        protected string _applicationID;
        protected IDictionary<string, Backpack> _modulebackpack;
        protected IDictionary<string, BowtieMetadata> _moduleMetadata;
        protected IEnumerable<IPlayerInventoryItem> _inventoryItems;

        #endregion Fields

        #region Constructors
        public ApplicationPlayer(string id, string applicationID) : base()
        {
            _id = id;
            _applicationID = applicationID;
            _inventoryItems = new List<IPlayerInventoryItem>();
        }



        #endregion Constructors

        #region IApplicationable
        public string GetApplicationID()
        {
            return _applicationID;
        }

        #endregion IApplicationable


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

        public DateTime DateRegistered
        {
            get
            {
                return _metadata["DateRegistered"].GetValue().DateTimeValue;
            }

            set
            {
                _metadata["DateRegistered"].UpdateValue(value);
            }
        }

        public override Backpack GetBackpack()
        {
            Backpack bp = new Backpack(this.GetPlayerID());

            foreach( var mdp in _modulebackpack)
            {
                foreach( var i in mdp.Value)
                {
                    i.ID = mdp.Key + ":" + i.ID;
                    bp.Add(i);
                }
            }

            foreach( var i in _inventoryItems)
            {
                bp.Add(i);
            }

            return bp;
        }


        #region Static Methods
        public static ApplicationPlayer FromPlayer(IBowtiePlayer player, IApplication app)
        {
            ApplicationPlayer ap = new ApplicationPlayer(player.GetID(), app.GetID());
            //ap.Age = player.Age;
            ap.AuthInfo = player.GetAuthInfo();
            ap.DisplayName = player.DisplayName;
            //ap.Gender = player.Gender;
            
            foreach(var m in app.GetModules())
            {
                var items = player.GetModuleInventory(m.GetID());
                var data = player.GetModuleData(m.GetID());

                if (items != null)
                    ap._modulebackpack.Add(m.GetID(), new Backpack(player.GetID(), items));

                if (data != null)
                    ap._moduleMetadata.Add(m.GetID(), data);
            }
            
            return ap;
        }

        #endregion Static Methods
    }
}
