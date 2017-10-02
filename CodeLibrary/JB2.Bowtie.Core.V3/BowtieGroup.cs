using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;

namespace JB2.Bowtie
{
    public abstract class BasicBowtieGroup<Tplayer, Tkey, Tsearch> : BowtieObject, IBowtieGroup<Tplayer, Tkey, Tsearch>
        where Tplayer : IPlayerable<Tkey>
        where Tkey : IComparable
    {
        #region Fields
        protected BaseCollection<Tplayer> _collection;
        protected Tkey _appID;

        #endregion Fields

        #region Constructor

        public BasicBowtieGroup()
        {
            _collection = new BaseCollection<Tplayer>();
        }
        #endregion Constructor

        #region IBowtieGroup
        public virtual Tplayer this[int index]
        {
            get
            {
                return _collection[index];
            }

            set
            {
                _collection[index] = value;
            }
        }

        public virtual ServiceResult Add(Tplayer item)
        {
            return _collection.Add(item);
        }

        public virtual int Count()
        {
            return _collection.Count();
        }

        public abstract bool DoesExist(Tplayer player);

        public virtual Tplayer Find(Func<Tplayer, bool> predicate)
        {
            return _collection.Find(predicate);
        }

        public Tkey GetApplicationID()
        {
            return _appID;
        }

        public ServiceResult Remove(Tplayer item)
        {
            return _collection.Remove(item);
        }

        public abstract IEnumerable<Tplayer> Search(Tsearch search);
        

        public IList<Tplayer> ToList()
        {
            return _collection.ToList();
        }

        #endregion IBowtieObject
    }
}
