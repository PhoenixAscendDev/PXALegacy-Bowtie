using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common.Data;
using Microsoft.WindowsAzure.Storage.Table;

namespace JB2.Bowtie.Data.Azure
{
    public abstract class BaseRepository<T,Tentity> : JB2.Common.IRepository<T, string>
        where Tentity : AzureTableEntity, new()
    {
        protected JB2.Common.Data.AzureTableRepository _table;
        protected JB2.Common.Data.AzureBlobRepository _blob;
        protected string _defaultPartitionKey;

        #region IRepository
        public virtual void Insert(T obj)
        {
            saveEntity(convertToEntity(obj), true);         
        }

        public virtual void Delete(T obj)
        {
            deleteAll(convertToEntity(obj));          
        }

        public virtual T[] SearchFor()
        {
            throw new NotImplementedException();
        }

        public virtual T[] GetAll()
        {
            var list_e = _table.GetByRowKeyStartWith<Tentity> (_defaultPartitionKey, "id:",1000);
            return convertToObject(list_e).ToArray();
        }

        public virtual T GetById(string id)
        {
            var e = _table.GetEntity<Tentity>(_defaultPartitionKey, "id:" + id);

            return convertToObject(e);
        }

        #endregion IRepository

        protected abstract Tentity convertToEntity(T o);

        protected abstract T convertToObject(Tentity e);

        protected abstract IEnumerable<T> convertToObject(IEnumerable<Tentity> list);

        protected abstract void saveEntity(Tentity e, bool replace);

        protected abstract void deleteAll(Tentity e);

    }
}
