using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Identity;

namespace JB2.Bowtie
{
    public abstract class Player  : BowtieObject, IBowtiePlayer
    {
        #region Fields
        protected JB2.Identity.IPlayer _player;
        protected new Name _name;
        
        protected MetaDataCollection _metadata;
        
        protected Dictionary<string, MetaDataCollection> _modules;              
        #endregion Fields

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

        #region IPlayer
        public abstract int GetBitScore();
        public abstract string GetFamilyID();
        public abstract string GetjBeanAccountNumber();
        public abstract string GetMasterEmail();
        public abstract string GetMasterUsername();
        public virtual IMetaData GetModuleMetaData(string module, string propertyName)
        {
            //MetaDataCollection collection = _modules[module];
            if (_modules.ContainsKey(module))
                return _modules[module][propertyName];
            else
                return null;
        }

        public abstract Name GetName();
        public abstract IEnumerable<PlayerProfilePacket> GetProfiles();

        public virtual IMetaData MetaData(string propertyName)
        {
            return _metadata[propertyName];
        }
        public virtual void SetModuleMetaData(string module, IMetaData metadata)
        {
            if (!_modules.ContainsKey(module))
                _modules.Add(module, new MetaDataCollection(metadata));
            else
                _modules[module].Add(metadata);
        }
        #endregion IPlayer

        #region IBowtiePlayer
        public virtual IWallet GetWallet()
        {
            return JB2.Settings.Bowtie.UnitOfWork.WalletRepository.GetByPlayerAndApplication(this.GetPlayerID(), JB2.Settings.Bowtie.CurrentApplication.ID);
        }

        public abstract string GetIdentityAuthID();

        public IMetaData GetModuleAttribute(string module, string propertyName)
        {
            return _player.GetModuleAttribute(module, propertyName);
        }

        public void SetModuleAttribute(string module, IMetaData metadata)
        {
            _player.SetModuleAttribute(module, metadata);
        }

        public string GetPlayerID()
        {
            return _player.GetPlayerID();
        }


        public PlayerProfilePacket GetDefaultProfile()
        {
            return _player.GetDefaultProfile();
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

        #endregion IBowtiePlayer

    }
}
