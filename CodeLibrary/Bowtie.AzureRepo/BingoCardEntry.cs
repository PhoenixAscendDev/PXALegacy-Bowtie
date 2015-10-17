using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.Azure
{
    public class BingoCardEntry : Microsoft.WindowsAzure.Storage.Table.TableEntity
    {
        public BingoCardEntry(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }

        public BingoCardEntry()
        {

        }


        public string CheckSum { get; set; }
        public string CardID { get; set; }

        public string Spaces { get; set; }

        public string BingoType { get; set; }

        public int Rows { get; set; }
        public int Columns { get; set; }

        public string Size { get; set; }

        public string DateCreated { get; set; }

        public string UniqueToken { get; set; }
    }
}
