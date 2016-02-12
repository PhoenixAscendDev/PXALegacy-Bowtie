using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;
using JB2.Common;

namespace JB2.Bowtie.Data.Azure
{
    public class GraphElementEntity : JB2.Common.Data.AzureTableEntity
    {

        #region Constructors

        public GraphElementEntity(): base()
        {

        }


        public GraphElementEntity(string partitionKey, string rowKey) : base(partitionKey,rowKey)
        {

        }
        #endregion Constructors

        public string ApplicationID
        {
            get; set;
        }

        public string ElementType
        {
            get; set;
        }

        public string ParentID
        {
            get; set;
        }

        public string PropertyName
        {
            get; set;
        }

        public string Value
        {
            get; set;
        }

    }
}
