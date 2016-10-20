using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie
{
    public class BowtieMetadata
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

        #endregion Constructors

        #region Properties
        public string PlayerID
        {
            get { return _playerid; }
        }

        //public string jBeanAccountNumber
        //{
        //    get
        //    {
        //        if (_metadata.ContainsKey("jbeanAccountNumber"))
        //            return _metadata["jbeanAccountNumber"].GetValue().ToString();
        //        else
        //            return string.Empty;
        //    }
        //    set
        //    {
        //        StringMetaData md = new StringMetaData("jbeanAccountNumber", value);
        //        _metadata["jbeanAccountNumber"] = md;
        //    }
        //}

        //public string Title
        //{
        //    get
        //    {
        //        if (_metadata.ContainsKey("title"))
        //            return _metadata["title"].GetValue().ToString();
        //        else
        //            return string.Empty;
        //    }

        //    set
        //    {
        //        StringMetaData md = new StringMetaData("title", value);
        //        _metadata["title"] = md;
        //    }
        //}

        //public long BitScore
        //{
        //    get
        //    {
        //        if (_metadata.ContainsKey("bitscore"))
        //            return _metadata["bitscore"].GetValue().LongValue;
        //        else
        //            return 0;
        //    }

        //    set
        //    {
        //        MetaData<long> md = new MetaData<long>("bitscore", value);
        //        _metadata["bitscore"] = md;
        //    }
        //}
        

        //public Kenshin Kenshin
        //{
        //    get
        //    {
        //        if (_metadata.ContainsKey("kenshin"))
        //            return (Kenshin)_metadata["kenshin"].GetValue();
        //        else
        //            return new Kenshin();
        //    }

        //    set
        //    {
        //        MetaData<Kenshin> md = new MetaData<Kenshin>("kenshin", value);
        //        _metadata["kenshin"] = md;
        //    }

        //}

        #endregion Properties


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
