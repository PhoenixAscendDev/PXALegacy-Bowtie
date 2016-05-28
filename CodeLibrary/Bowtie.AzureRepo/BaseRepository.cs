using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common.Data;
using Microsoft.WindowsAzure.Storage.Table;

namespace JB2.Bowtie.Data.Azure
{

    public abstract class BowtieRepository<T> : BowtieRepository<T,DynamicTableEntity>
        where T : JB2.Common.IIDProp<string>
    {

    }

    public abstract class BowtieRepository<T,Tentity> :
        JB2.Common.Data.AzureRepositoryWithCache<T, string, string, BowtieCacheDictionary<T>, string>,
        JB2.Common.IRepositoryWithCache<T,string,string>
        where Tentity : class, ITableEntity, new()
        where T : JB2.Common.IIDProp<string>
    {
        protected string _defaultPartitionKey;

        #region Fields
        protected bool _useCache;

        #endregion Fields


        #region Constructors

        public BowtieRepository() : this(true)
        {

        }

        public BowtieRepository(bool useCache) : base()
        {
            _useCache = useCache;
        }

        #endregion Constructors

        #region IRepository
        public override void Insert(T obj)
        {
            saveEntity(convertToEntity(obj), true);         
        }

        public override void Delete(T obj)
        {
            deleteAll(convertToEntity(obj));          
        }

        public override T[] SearchFor()
        {
            throw new NotImplementedException();
        }

        public override T[] GetAll()
        {
            return GetAll(_useCache);
        }

        public override T[] GetAll(bool usecache)
        {
            var list = _table.GetByRowKeyStartWith<Tentity>(_defaultPartitionKey, "id:", 1000);

            if (list != null)
            {
                //lets go ahead and store the players in cache
                var Tlist = convertToObject(list).ToArray();
                _cache.Clear();
                foreach (T p in Tlist)
                {
                    if (_cache.ContainsKey(p.GetID()))
                        _cache[p.GetID()] = p;
                    else
                        _cache.Add(p.GetID(), p);
                }
                return convertToObject(list).ToArray();
            }
            else
                return null;
        }

        public override T GetById(string id)
        {
            return GetById(id, _useCache);
        }

        public override T GetById(string id, bool useCache = true)
        {
            if (useCache)
            {
                if (_cache.ContainsKey(id))
                    return _cache[id];
            }
            var e = _table.GetEntity<Tentity>(_defaultPartitionKey, "id:" + id);

            if (e != null)
            {
                //go ahead and update cache in case the next request wants to use
                if (_cache.ContainsKey(id))
                    _cache[id] = convertToObject(e);
                else
                    _cache.Add(id, convertToObject(e));

                return convertToObject(e);
            }
            else
                return default(T);
        }

        public override T[] SearchFor(string filter, bool useCache)
        {
            throw new NotImplementedException();
        }

        public override T[] SearchFor(bool useCache)
        {
            return SearchFor(string.Empty, useCache);
        }

        public override T[] SearchFor(string filter)
        {
            return SearchFor(filter, _useCache);
        }

        #endregion IRepository

        protected abstract Tentity convertToEntity(T o);

        protected abstract T convertToObject(Tentity e);

        protected abstract IEnumerable<T> convertToObject(IEnumerable<Tentity> list);

        protected abstract void saveEntity(Tentity e, bool replace);

        protected abstract void deleteAll(Tentity e);

       
    }
}
