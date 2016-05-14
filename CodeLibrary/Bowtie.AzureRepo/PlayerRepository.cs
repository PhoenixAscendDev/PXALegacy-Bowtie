using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;
using JB2.Identity;
using Microsoft.WindowsAzure.Storage.Table;

namespace JB2.Bowtie.Data.Azure
{
    public class PlayerRepository : BowtieRepository<IBowtiePlayer>, IBowtiePlayer
    {
        public string DisplayName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ID
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Name Name
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int GetCounterIndex()
        {
            throw new NotImplementedException();
        }

        public string GetCounterName()
        {
            throw new NotImplementedException();
        }

        public PlayerProfilePacket GetDefaultProfile()
        {
            throw new NotImplementedException();
        }

        public string GetFamilyID()
        {
            throw new NotImplementedException();
        }

        public string GetID()
        {
            throw new NotImplementedException();
        }

        public string GetIdentityAuthID()
        {
            throw new NotImplementedException();
        }

        public string GetjBeanAccountNumber()
        {
            throw new NotImplementedException();
        }

        public string GetMasterEmail()
        {
            throw new NotImplementedException();
        }

        public string GetMasterUsername()
        {
            throw new NotImplementedException();
        }

        public IMetaData GetMetaData(string propertyName)
        {
            throw new NotImplementedException();
        }

        public IMetaData GetModuleAttribute(string module, string propertyName)
        {
            throw new NotImplementedException();
        }

        public Name GetName()
        {
            throw new NotImplementedException();
        }

        public string GetPlayerID()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlayerProfilePacket> GetProfiles()
        {
            throw new NotImplementedException();
        }

        public IWallet GetWallet()
        {
            throw new NotImplementedException();
        }

        public IMetaData MetaData(string propertyName)
        {
            throw new NotImplementedException();
        }

        public void SetModuleAttribute(string module, IMetaData metadata)
        {
            throw new NotImplementedException();
        }

        protected override DynamicTableEntity convertToEntity(IBowtiePlayer o)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<IBowtiePlayer> convertToObject(IEnumerable<DynamicTableEntity> list)
        {
            throw new NotImplementedException();
        }

        protected override IBowtiePlayer convertToObject(DynamicTableEntity e)
        {
            throw new NotImplementedException();
        }

        protected override void deleteAll(DynamicTableEntity e)
        {
            throw new NotImplementedException();
        }

        protected override void saveEntity(DynamicTableEntity e, bool replace)
        {
            throw new NotImplementedException();
        }
    }
}
