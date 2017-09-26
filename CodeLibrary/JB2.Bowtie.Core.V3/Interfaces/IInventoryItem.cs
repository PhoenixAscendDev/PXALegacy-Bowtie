using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IInventoryItem : JB2.Common.IIDNamePair<string, string>
    {
        //int Quanity { get; set; }

        string ApplicationID { get; set; }

        string IconUrl { get; set; }

        string InventoryCategory { get; set; }

        string PuralName { get; set; }

        string StorageSlot { get; set; }
    }
}
