using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie
{
    public class PlayerDewdrop : JB2.Common.ObjectWithMetadata<byte,string,string>,IPlayerDewdrop
    {
        #region Fields
        public string _value;
        #endregion Fields

        public PlayerDewdrop()
        {
            _id = JB2.Common.NewID.ShortGuid();

        }
        public PlayerDewdrop( IEnumerable<IMetaData> metadataList, string value)
        {
            _metadata = new MetaDataCollection(metadataList);
            _id = JB2.Common.NewID.ShortGuid();
        }

        #region IPlayerDewdrop
        public string GetApplicationID()
        {
            return _metadata["ApplicationID"].GetValue().StringValue;

        }

        public DateTime GetDewDate()
        {
            return _metadata["DewDate"].GetValue().DateTimeValue;
        }

        public string GetDewdropID()
        {
            return _metadata["DewdropID"].GetValue().StringValue;
        }

        public override byte GetKind()
        {
            return 0;
        }

        public override DateTime GetLastUpdate()
        {
            return System.DateTime.Now;
        }

        public string GetPlayerID()
        {
            return _metadata["PlayerID"].GetValue().StringValue;
        }

        public string GetValue()
        {
            return _value;
        }

        #endregion IPlayerDewdrop
    }
}
