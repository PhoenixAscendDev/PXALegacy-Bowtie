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
        //protected JB2.Identity.IPlayer _player;
        protected new Name _name;
        protected MetaDataCollection _metadata;
        
        protected Dictionary<string, MetaDataCollection> _modules;
        #endregion Fields


        #region Constructors
        public Player()
        {
            _metadata = new MetaDataCollection();
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

        public int Age
        {
            get
            {
                return _metadata["Age"].GetValue().IntValue;
            }

            set
            {
                _metadata["Age"].UpdateValue(value);
            }
        }

        public string Gender
        {
            get
            {
                return _metadata["Gender"].GetValue().StringValue;
            }

            set
            {
                _metadata["Gender"].UpdateValue(value);
            }
        }

        public string AuthProvider
        {
            get
            {
                return _metadata["Auth"].GetValue().StringValue;
            }

            set
            {
                _metadata["Auth"].UpdateValue(value);
            }
        }

        #region IPlayer
        public virtual IMetaData GetModuleMetaData(string module, string propertyName)
        {
            //MetaDataCollection collection = _modules[module];
            if (_modules.ContainsKey(module))
                return _modules[module][propertyName];
            else
                return null;
        }

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

        //public IMetaData GetModuleAttribute(string module, string propertyName)
        //{
        //    return _player.GetModuleAttribute(module, propertyName);
        //}

        //public void SetModuleAttribute(string module, IMetaData metadata)
        //{
        //    _player.SetModuleAttribute(module, metadata);
        //}

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
            throw new NotImplementedException();
        }

        #endregion IBowtiePlayer

    }
}
