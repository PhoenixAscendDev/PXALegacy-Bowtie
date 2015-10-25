using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.Azure
{
    public class BingoPatternEntry : Microsoft.WindowsAzure.Storage.Table.TableEntity
    {

        public BingoPatternEntry() : base()
        {

        }

        public BingoPatternEntry(string partitionKey, string rowKey) : base(partitionKey,rowKey)
        {

        }

        public string ID { get; set; }

        public string BingoType { get; set; }

        public string Name { get; set; }

        public string WinningPatternsCombined { get; set; }


    }
}
