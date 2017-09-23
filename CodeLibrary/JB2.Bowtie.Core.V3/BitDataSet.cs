using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;

using JB2.Common;

namespace JB2.Bowtie
{
    public abstract class BitDataSet
    {
        #region Fields
        protected BitArray _bitarray;
        #endregion Fields

        #region Const

        private const int TOTALSIZE = 10240;
        protected static readonly  int ROWSIZE = 80;
        protected static readonly int TOTALROWS = 95;

        protected static readonly int TICK1_INDEX = 72;
        protected static readonly int TICK2_INDEX = 76;
        protected static readonly int VALUE_INDEX = 0;
        protected static readonly int ID_INDEX = 48;

        protected static readonly int DATASETID_INDEX = 7920;
        protected static readonly int CHECKSUM_INDEX = 7984;
        protected static readonly DateTime TICKSTART = new DateTime(2017, 9, 20, 0, 0, 0, DateTimeKind.Utc);

        #endregion Const

        #region Constructor

        public BitDataSet(BitArray dataset)
        {
            _bitarray = dataset;
        }

        protected BitDataSet()
        {
            initdataset();
        }

        #endregion Constructor


        #region Properties
        public virtual ulong DataSetID
        {
            get
            {
                BitArray a = _bitarray.GetSubSet(DATASETID_INDEX, 64);
                return a.ToNumber<ulong>(); 

            }

            internal set
            {
                var array = value.ToBitArray(64);
                var index = DATASETID_INDEX;
                for (int i = 0; i < 64; i++)
                {
                    _bitarray[index + i] = array[i];
                }
            }
        }

        #endregion Properties


        public virtual BitArray ToBitArray()
        {
            return (BitArray)this;
        }

        public virtual string ToBase64String()
        {
            var bitstr = Convert.ToBase64String((byte[])this);

            return bitstr;
        }

        public virtual byte[] ToByteArray()
        {
            return (byte[])this;
        }

        public virtual string ToBitString()
        {
            return this._bitarray.ToBitString();
        }


        #region Helpers

        protected virtual void initdataset()
        {
            _bitarray = new BitArray(TOTALSIZE);


            //set the drewdrop IDs
            for (int i = 0; i < TOTALROWS; i++)
            {
                var rowNumber = i + 1;

                BitArray b = rowNumber.ToBitArray(16); // convertToBitArray(rowNumber, 16);

                for (int j = 0; j < b.Length; j++)
                {
                    _bitarray[(ROWSIZE * i) + ID_INDEX + j] = b[j];
                }
            }


            //set the dataset uniqueID
            var timediff = DateTime.UtcNow - TICKSTART;

            var ticks = timediff.Ticks + JB2.Helper.Bowtie.NewRNG();

            this.DataSetID = (ulong)ticks;


            for (int i = 0; i < TOTALROWS; i++)
            {
                updateRow(i + 1);
            }



        }

        protected virtual void updateRow(int rowNumber)
        {

            var tick = calculateRowTick(rowNumber);


            //update the row ticks
            BitArray tick_bit = tick.ToBitArray(8);// convertToBitArray(tick, 8);

            for (int i = 0; i < 4; i++)
            {
                _bitarray[(ROWSIZE * (rowNumber - 1)) + TICK1_INDEX + i] = tick_bit[i];
            }

            for (int i = 0; i < 4; i++)
            {
                _bitarray[(ROWSIZE * (rowNumber - 1)) + TICK2_INDEX + i] = tick_bit[i + 4];
            }


            //reapply the dataset Checksum
            var checksum = calculateCheckSum();

            var checksum_bit = checksum.ToBitArray(16); // convertToBitArray(checksum, 16);

            for (int i = 0; i < checksum_bit.Length; i++)
            {
                _bitarray[CHECKSUM_INDEX + i] = checksum_bit[i];
            }
        }

        protected virtual int calculateRowTick(int rowNumber)
        {
            var b = getRow(rowNumber);

            //get the value and use it to seed the RNG
            var bvalue = new BitArray(16);
            var index = VALUE_INDEX;
            for (int i = 0; i < 16; i++)
            {
                bvalue[i] = b[index + i];
            }

            var value = bvalue.ToNumber<ushort>();

            //get the seed
            ushort seed = 0;
            for (int i = 0; i < rowNumber; i++)
            {
                seed = JB2.Common.RNG.Plumber(seed);
            }

            ushort rng = 0;

            rng = JB2.Common.RNG.Plumber(seed, value == 0 ? (ushort)1 : value).LastOrDefault();


            int tick = (rng >> (8 * 0)) & 0xff;

            return tick;

        }


        protected virtual BitArray getRow(int rowid)
        {
            try
            {
                if ((rowid < 0) || (rowid > TOTALROWS))
                    throw new ArgumentException("Rowid should be between 1 and " + TOTALROWS);

                rowid = rowid - 1;
                int index = rowid * ROWSIZE;
                bool[] data = new bool[ROWSIZE];

                for (int i = 0; i < ROWSIZE; i++)
                {
                    data[i] = _bitarray[index + i];
                }

                return new BitArray(data);
            }
            catch (Exception ex)
            {
                return new BitArray(0);
            }


        }

        protected virtual ushort calculateCheckSum()
        {
            //add up all row ticks (even taking tick2 odd tick1)

            long sum = 0;
            for (int i = 1; i <= TOTALROWS; i++)
            {

                var tick_bit = new BitArray(8);
                var index = 0;
                if (i % 2 == 0)
                    index = 0;
                else
                    index = 1;

                for (int j = 0; j < 4; j++)
                {
                    tick_bit[j] = _bitarray[(ROWSIZE * (i - 1)) + TICK1_INDEX + j];
                }

                for (int j = 0; j < 4; j++)
                {
                    tick_bit[j + 4] = _bitarray[(ROWSIZE * (i - 1)) + TICK2_INDEX + j];
                }

                var rowTickValue = tick_bit.ToNumber<ushort>(); // convertToNumber<ushort>(tick_bit);

                int tick = (rowTickValue >> (4 * index)) & 0xf;

                sum = sum + tick;
            }

            //determine the seed for the rng
            BitArray seedbit = _bitarray.GetSubSet(DATASETID_INDEX, 16);




            ushort rng = JB2.Common.RNG.Plumber(0, seedbit.ToNumber<ushort>()).LastOrDefault();

            for (int i = 0; i < sum; i++)
            {
                rng = JB2.Common.RNG.Plumber(rng);
            }

            return rng;



        }



        #endregion Helpers


        #region Static

        public static implicit operator BitArray(BitDataSet d)
        {
            return d._bitarray;
        }

        public static implicit operator byte[](BitDataSet d)
        {
            byte[] ret = new byte[(d._bitarray.Length - 1) / 8 + 1];
            d._bitarray.CopyTo(ret, 0);

            return ret;
        }

        #endregion Static


    }
}
