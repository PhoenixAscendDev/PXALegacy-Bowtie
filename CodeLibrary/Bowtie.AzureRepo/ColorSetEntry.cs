using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.Azure
{
    public class ColorSetEntry : Microsoft.WindowsAzure.Storage.Table.TableEntity
    {

        public ColorSetEntry(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }

        public ColorSetEntry()
        {

        }

        public string ID { get; set; }
        public string ColorSetType { get; set; }
        public string Name { get; set; }
        public int ColorCount { get; set; }



    }
}
