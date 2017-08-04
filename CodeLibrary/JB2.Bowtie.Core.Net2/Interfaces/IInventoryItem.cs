using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using JB2.Common;

namespace JB2.Bowtie
{
    public interface IInventoryItem : IIDNamePair<string, string>
    {
        //int Quanity { get; set; }

        JB2Image Icon { get; set; }

        string InventoryCategory { get; set; }

        string PuralName { get; set; }
    }
}
