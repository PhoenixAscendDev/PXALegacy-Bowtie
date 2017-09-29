using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;

namespace JB2.Bowtie
{
    public interface IBitDataSet
    {

        ulong DataSetID { get; }

        BitArray ToBitArray();

        string ToBase64String();

        byte[] ToByteArray();

        string ToBitString();

    }
}
