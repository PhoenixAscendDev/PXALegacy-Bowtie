using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Collections;

namespace JB2.Bowtie
{
    public class ActivityDataset : BitDataSet
    {

        #region Constructor

        public ActivityDataset(BitArray dataset)
        {
            _bitarray = dataset;
        }

        public ActivityDataset(byte[] data)
        {
            _bitarray = new BitArray(data);
        }

        protected ActivityDataset()
        {

            initdataset();
        }



        #endregion Constructor

        public static ActivityDataset FromByte(byte[] data)
        {
            return new ActivityDataset(data);
        }

        public static ActivityDataset FromString(string data)
        {
            var ba = Encoding.ASCII.GetBytes(data);

            return new ActivityDataset(ba);
        }
    }
}
