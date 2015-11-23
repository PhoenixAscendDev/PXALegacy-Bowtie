using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;


namespace JB2.Bowtie
{
    public class Player : JB2.Common.Player, JB2.Common.IPlayer
    {
        #region Fields
        private MetaDataCollection _metadata;
        #endregion Fields

        public override IMetaData MetaData(string propertyName)
        {
            return _metadata[propertyName];
        }
    }
}
