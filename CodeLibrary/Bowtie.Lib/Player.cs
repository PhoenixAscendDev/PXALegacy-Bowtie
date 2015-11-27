using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Identity;

namespace JB2.Bowtie
{
    public abstract class Player  : JB2.Common.ShortGuidID, IBowtiePlayer
    {
        #region Fields
        protected JB2.Identity.IPlayer _player;
        protected Name _name;
        protected JB2.Economy.IWallet _wallet;
        protected MetaDataCollection _metadata;
        protected Dictionary<string, MetaDataCollection> _modules;                
        #endregion Fields


        public abstract string DisplayName { get; set; }
        public virtual Name Name
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
        public virtual JB2.Economy.IWallet GetWallet()
        {
            return _wallet;
        }

        public abstract string GetIdentityAuthID();

        #endregion IBowtiePlayer



    }
}
