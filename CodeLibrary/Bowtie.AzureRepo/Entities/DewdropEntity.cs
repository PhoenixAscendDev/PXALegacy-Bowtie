using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.Azure
{
    public class DewdropEntity : JB2.Common.Data.AzureTableEntity
    {
        #region Constructors
        public DewdropEntity() : base()
        {

        }

        public DewdropEntity(string partitionKey, string rowKey) : base(partitionKey, rowKey)
        {

        }
        #endregion Constructors

        #region Properties

        public string ApplicationID { get; set; }
        public string GraphID { get; set; }

        public string Description { get; set; }

        #endregion Properties
    }
}
