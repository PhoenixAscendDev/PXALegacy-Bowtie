using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;


namespace JB2.Bowtie
{
    public abstract class Player  : BowtieObject, IBowtiePlayer
    {

        #region Fields
        //protected JB2.Identity.IPlayer _player;
        protected new Name _name;
        protected MetaDataCollection _metadata;
        
        protected Dictionary<string,IModule> _modules;

        protected IDictionary<string, int> _dewdrops;

        
        #endregion Fields

        #region Constructors
        public Player()
        {
            _metadata = new MetaDataCollection();
            _dewdrops = new Dictionary<string, int>();
            _modules = new Dictionary<string, IModule>();        
        }
        public Player(string id, IEnumerable<IModule> modules)
        {
            _metadata = new MetaDataCollection();
            _dewdrops = new Dictionary<string, int>();
            _modules = new Dictionary<string, IModule>();
            foreach(var m in modules)
            {
                _modules.Add(m.GetID(), m);
            }
        }

        #endregion Constructors

        public abstract string DisplayName { get; set; }
        public new virtual Name Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public virtual JB2.Common.IRange<byte> AgeRange
        {
            get
            {
                return _props.GetProperty<IRange<byte>>("AgeRange");
            }
            set
            {
                _props.SetProperty<IRange<byte>>("AgeRange", value);
            }
        }

        public virtual short BirthMonth
        {
            get
            {
                return _props.GetProperty<short>("BirthMonth");
            }
            set
            {
                _props.SetProperty<short>("BirthMonth", value);
            }
        }
        public virtual short BirthDayOfMonth
        {
            get
            {
                return _props.GetProperty<short>("BirthDayOfMonth");
            }
            set
            {
                _props.SetProperty<short>("BirthDayOfMonth", value);
            }
        }

        public AuthInfo AuthInfo
        {
            get
            {
                return _metadata.GetProperty<AuthInfo>("AuthInfo", new AuthInfo());
            }

            set
            {
                _metadata.SetProperty<AuthInfo>("AuthInfo", value);
            }
        }

        public abstract Backpack GetBackpack();

        #region IPlayer
        public virtual IEnumerable<IModule> GetModules()
        {
            return _modules.Values;
        }

        public virtual BowtieMetadata GetModuleData(string moduleid)
        {
            if (_modules.ContainsKey(moduleid))
            {
                var accesskey = JB2.Settings.Bowtie.CurrentApplication.GetModulePermission(moduleid).AccessKey;
                return _modules[moduleid].GetPlayerData(this.GetPlayerID(),accesskey);
            }             
            else
                return new BowtieMetadata(this.GetPlayerID());
        }

        public virtual AuthInfo GetAuthInfo()
        {
            return this.AuthInfo;
        }

        public virtual IEnumerable<IPlayerInventoryItem> GetModuleInventory(string moduleid)
        {
            if (_modules.ContainsKey(moduleid))
            {
                var accesskey = JB2.Settings.Bowtie.CurrentApplication.GetModulePermission(moduleid).AccessKey;
                return _modules[moduleid].GetPlayerInventory(this.GetPlayerID(), accesskey);
            }
            else
                return new List<IPlayerInventoryItem>();

        }
        public virtual IMetaData GetModuleData(string moduleID, string propertyName)
        {
            //MetaDataCollection collection = _modules[module];
            if (_modules.ContainsKey(moduleID))
            {
                var accesskey = JB2.Settings.Bowtie.CurrentApplication.GetModulePermission(moduleID).AccessKey;
                var data = _modules[moduleID].GetPlayerData(this.GetPlayerID(),accesskey);
                return data[propertyName];
            }
            
            else
                return null;
        }

        public virtual IMetaData MetaData(string propertyName)
        {
            return _metadata[propertyName];
        }
        #endregion IPlayer

        #region IBowtiePlayer
        public virtual IWallet GetWallet()
        {
            return JB2.Settings.Bowtie.UnitOfWork.WalletRepository.GetByPlayerAndApplication(this.GetPlayerID(), JB2.Settings.Bowtie.CurrentApplication.ID);
        }

        public virtual IDictionary<string, int> DewdropCounts
        {
            get
            {
                return _dewdrops;
            }
            set
            {
                _dewdrops = value;
            }
        }

        public virtual void AddDewDrop(IDewdrop dewdrop)
        {
            if (!_dewdrops.ContainsKey(dewdrop.GetID()))
                _dewdrops.Add(dewdrop.GetID(), 1);
            else
                _dewdrops[dewdrop.GetID()]++;
        }

        public string GetPlayerID()
        {
            return _id;
        }

        public IMetaData GetMetaData(string propertyName)
        {
           return  _metadata[propertyName];
        }

        public string GetCounterName()
        {
            return "bowtie_player";
        }

        public int GetCounterIndex()
        {
            return -1;
        }

        Name INameProp<Name>.GetName()
        {
            return _name;
        }

        #endregion IBowtiePlayer

    }
}
