using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie
{
    public class BowtieMetadata : IEnumerable<IMetaData>, IEnumerable, IObjectCollection<IMetaData>
    {

        #region Fields
        protected MetaDataCollection _metadata;
        protected string _playerid;
        #endregion Fields


        #region Constructors
        public BowtieMetadata(string playerid)
        {
            _metadata = new MetaDataCollection();
            _playerid = playerid;
        }

        public BowtieMetadata(string playerid, IEnumerable<IMetaData> data)
        {
            _metadata = new MetaDataCollection(data);
            _playerid = playerid;
        }

        public IMetaData this[int index]
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

        #endregion Constructors

        #region This

        public IMetaData this[string propertyName]
        {
            get
            {
                return _metadata[propertyName];
            }
        }

        #endregion This

        #region Properties
        public string PlayerID
        {
            get { return _playerid; }
        }

        #endregion Properties


        public IEnumerator<IMetaData> GetEnumerator()
        {
            return _metadata.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _metadata.GetEnumerator();
        }

        public ServiceResult Add(IMetaData item)
        {
            return _metadata.Add(item);
        }

        public int Count()
        {
            return _metadata.Count();
        }

        public IMetaData Find(Func<IMetaData, bool> predicate)
        {
            return _metadata.Find(predicate);
        }

        public ServiceResult Remove(IMetaData item)
        {
            return _metadata.Remove(item);
        }

        public static implicit operator MetaDataCollection(BowtieMetadata md)
        {
            return md._metadata;
        }

        public static implicit operator List<IMetaData>(BowtieMetadata md)
        {
            return (List<IMetaData>)md._metadata.ToList();
        }



    }
}
