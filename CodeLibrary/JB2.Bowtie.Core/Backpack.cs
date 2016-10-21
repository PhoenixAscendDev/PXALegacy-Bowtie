using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie
{
    public class Backpack : JB2.Common.JB2Class, IPlayerable, IEnumerable<IPlayerInventoryItem>, IObjectCollection<IPlayerInventoryItem>
    {

        #region Fields
        JB2.Common.BaseCollection<IPlayerInventoryItem> _collection;

        public IPlayerInventoryItem this[int index]
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
        #endregion Fields


        #region Constructor

        public Backpack(string playerid ) : this(playerid, new List<IPlayerInventoryItem>())
        {
            
        }

        public Backpack(string playerid, IEnumerable<IPlayerInventoryItem> items) : base()
        {
            this.GetProperity<string>("PlayerID", string.Empty);
            _collection = new BaseCollection<IPlayerInventoryItem>(items);
        }

        #endregion Constructor

        #region IPlayerable
        public string GetPlayerID()
        {
            return this.GetProperity<string>("PlayerID", string.Empty);
        }

        #endregion IPlayerable

        #region EnumPlayerInventoryItem

        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerable<IPlayerInventoryItem> list = (IEnumerable<IPlayerInventoryItem>)_collection;
            return list.GetEnumerator();
        }

        public IEnumerator<IPlayerInventoryItem> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        #endregion EnumPlayerInventoryItem

        public IPlayerInventoryItem Find(Func<IPlayerInventoryItem, bool> predicate)
        {
            return _collection.Find(predicate);
        }

        public int Count()
        {
            return _collection.Count();
        }

        public ServiceResult Add(IPlayerInventoryItem item)
        {
            return _collection.Add(item);
        }

        public ServiceResult Remove(IPlayerInventoryItem item)
        {
            return _collection.Remove(item);
        }

    }
}
