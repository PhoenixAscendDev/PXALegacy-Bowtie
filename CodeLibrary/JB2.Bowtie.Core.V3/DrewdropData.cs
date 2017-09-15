using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace JB2.Bowtie
{
    public class DrewdropData
    {

        #region Const

        private const int TOTALSIZE = 10200;
        private const int DEWDROPSIZE = 40;
        private const int TICK2_INDEX = 0;
        private const int TICK1_INDEX = 36;
        private const int ID_INDEX = 4;
        private const int VALUE_INDEX = 12;
        private const int INTERNAL_INDEX = 28;
        private const int TOTALROWS = 255;
        private const int DATASETID_INDEX = 10120;
        private const int CHECKSUM_INDEX = 10184;
        private readonly DateTime TICKSTART = new DateTime(1980, 2, 22);
        
        #endregion Const

        #region Fields
        protected BitArray _bitarray;
        #endregion Fields

        #region Constructor

        public DrewdropData(BitArray dataset)
        {
            _bitarray = dataset;
        }

        protected DrewdropData()
        {
            initdataset();
        }

        #endregion Constructor


        #region Properties

        public ulong DewDropDataID
        {
            get
            {
                BitArray a = getSubSet(DATASETID_INDEX, 64);

                return convertToNumber<ulong>(a);

                //if(a.Length > 64)
                //    throw new ArgumentException("Argument length shall be at most 64 bits.");

                //var array = new byte[8];
                //a.CopyTo(array, 0);
                //return BitConverter.ToUInt64(array, 0);


                //var array = new int[2];
                //a.CopyTo(array, 0);
                //return (uint)array[0] + ((ulong)(uint)array[1] << 32);
            }

            internal set
            {

                
                //convert to bool array
                //var array = Convert.ToString((long)value, 2).PadLeft(64,'0').Select(s => s.Equals('1')).ToArray();

                var array = convertToBitArray(value, 64);

                var index = 10120;
                for (int i = 0; i < 64; i++)
                {
                    _bitarray[index + i] = array[i];
                }
                //_bitarray[index + 63] = true;
                //_bitarray[index] = true;
                //_bitarray[index+1] = true;

            }
        }

        public bool isValid
        {
            get
            {
                return Validate();
            }
        }


        public bool Validate()
        {
            bool result = true;

            //validate the rows
            for(int i=0;i < 250;i++)
            {
                if (!validateRow(i + 1))
                    result = false;
            }

            //validate the checksum


            BitArray checksum_bit = this.getSubSet(CHECKSUM_INDEX, 16);

            var testCheckSum = convertToNumber<ushort>(checksum_bit);

            if (testCheckSum != calculateCheckSum())
                result = false;

            return result;
        }

        public BitArray GetDrewDropRow(int rowNum)
        {
            if ((rowNum < 0) || (rowNum > 250))
                throw new ArgumentException("Rowid should be between 1 and 250");
            return getRow(rowNum);
        }


        public int GetDrewDropCount(int drewdropID)
        {
            var rowNumber = getDewdropRowNumber(drewdropID);

            var row = GetDrewDropRow(rowNumber);

            var b = new BitArray(16);

            var index = VALUE_INDEX;

            for(int i = 0; i< 16; i++)
            {
                b[i] = row[index + i];
            }

            return convertToNumber<int>(b);

        }

        public void IncrementDrewDrop(int drewdropID)
        {
            int current = GetDrewDropCount(drewdropID);

            int newValue = current + 1;

            var rowNumber = getDewdropRowNumber(drewdropID);

            var b = convertToBitArray(newValue, 16);


            var index = (40 * (rowNumber-1)) + VALUE_INDEX;

            for (int i = 0; i < 16; i++)
            {
                _bitarray[index + i] = b[i];
            }

            updateRow(rowNumber);

        }

        #endregion Properties

        #region Helpers


        private BitArray getRow(int rowid)
        {
            try
            {
                if ((rowid < 0) || (rowid > TOTALROWS))
                    throw new ArgumentException("Rowid should be between 1 and " + TOTALROWS);

                rowid = rowid - 1;
                int index = rowid * DEWDROPSIZE;
                bool[] data = new bool[DEWDROPSIZE];

                for (int i = 0; i < DEWDROPSIZE; i++)
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

        private BitArray getSubSet(int index, int length)
        {
             bool[] result = new bool[length];

            for(int i=0;i < length;i++)
            {
                result[i] = _bitarray[index + i];
            }
            return new BitArray(result);
        }

        private BitArray fillData(int length, bool fill)
        {
            BitArray b = new BitArray(length);
            for (int i = 0; i < length; i++)
            {
                b[i] = fill;
            }

            return b;

        }





        private BitArray convertToBitArray(int number, int size = 32)
        {
            //string s = Convert.ToString(number, 2);

            bool[] bits = Convert.ToString((int)number, 2).PadLeft(size, '0').Select(s => s.Equals('1')).ToArray();

            Array.Reverse(bits);

            BitArray result = new BitArray(bits);

            return result;
        }

        private BitArray convertToBitArray(byte number, int size = 8)
        {
            //string s = Convert.ToString(number, 2);

            bool[] bits = Convert.ToString((byte)number, 2).PadLeft(size, '0').Select(s => s.Equals('1')).ToArray();

            Array.Reverse(bits);

            BitArray result = new BitArray(bits);

            return result;
        }

        private BitArray convertToBitArray(short number, int size = 16)
        {
            //string s = Convert.ToString(number, 2);

            bool[] bits = Convert.ToString((short)number, 2).PadLeft(size, '0').Select(s => s.Equals('1')).ToArray();

            Array.Reverse(bits);

            BitArray result = new BitArray(bits);

            return result;
        }

        private BitArray convertToBitArray(ushort number, int size = 16)
        {
            return convertToBitArray((short)number, size);
        }

        private BitArray convertToBitArray(long number, int size = 64)
        {
            //string s = Convert.ToString(number, 2);

            bool[] bits = Convert.ToString((long)number, 2).PadLeft(size, '0').Select(s => s.Equals('1')).ToArray();

            Array.Reverse(bits);

            BitArray result = new BitArray(bits);

            return result;
        }

        private BitArray convertToBitArray(ulong number, int size = 16)
        {
            return convertToBitArray((long)number, size);
        }


        private int convertToInt(BitArray b)
        {
            var array = new byte[4];
            b.CopyTo(array, 0);

            return BitConverter.ToInt32(array, 0);
        }

        private T convertToNumber<T>(BitArray b)
        {

            object result = 0;

            if (typeof(T) == typeof(ulong))
            {
                var array = new byte[8];
                b.CopyTo(array, 0);

                result = BitConverter.ToUInt64(array, 0);
            }

            else if (typeof(T) == typeof(long))
            {
                var array = new byte[8];
                b.CopyTo(array, 0);

                result = BitConverter.ToInt64(array, 0);
            }

            else if (typeof(T) == typeof(uint))
            {
                var array = new byte[4];
                b.CopyTo(array, 0);

                result = BitConverter.ToUInt32(array, 0);
            }



            if (typeof(T) == typeof(int))
            {
                var array = new byte[4];
                b.CopyTo(array, 0);

                result = BitConverter.ToInt32(array, 0);
            }


            else if (typeof(T) == typeof(ushort))
            {
                var array = new byte[2];
                b.CopyTo(array, 0);

                result = BitConverter.ToUInt16(array, 0);
            }

            else if (typeof(T) == typeof(short))
            {
                var array = new byte[2];
                b.CopyTo(array, 0);

                result = BitConverter.ToInt16(array, 0);
            }

            return (T)Convert.ChangeType(result, typeof(T));


        }



        private int getDewdropRowNumber(int dewdropID)
        {
            return dewdropID;

        }


        private int calculateRowTick(int rowNumber)
        {         
            var b = getRow(rowNumber);

            //get the value and use it to seed the RNG
            var bvalue = new BitArray(16);
            var index = VALUE_INDEX;
            for (int i = 0; i < 16; i++)
            {
                bvalue[i] = b[index + i];
            }

            var value =  convertToNumber<ushort>(bvalue);

            //get the seed
            ushort seed = 0;
            for(int i=0;i < rowNumber;i++)
            {
                seed = JB2.Common.RNG.Plumber(seed);
            }

            ushort rng = 0;

            rng = JB2.Common.RNG.Plumber(seed, value == 0 ? (ushort)1 : value).LastOrDefault();


            int tick = (rng >> (8 * 0)) & 0xff;

            return tick;

        }

        private ushort calculateCheckSum()
        {
            //add up all row ticks (even taking tick2 odd tick1)

            long sum = 0;
            for(int i=1;i<=250;i++)
            {
               
                var tick_bit = new BitArray(8);
                var index = 0;
                if (i % 2 == 0)
                    index = 0;
                else
                    index = 1;

                for (int j = 0; j < 4; j++)
                {
                    tick_bit[j] = _bitarray[(DEWDROPSIZE * (i - 1)) + TICK1_INDEX + j];
                }

                for (int j = 0; j < 4; j++)
                {
                    tick_bit[j + 4] = _bitarray[(DEWDROPSIZE * (i - 1)) + TICK2_INDEX + j];
                }

                var rowTickValue = convertToNumber<ushort>(tick_bit);

                int tick = (rowTickValue >> (4 * index)) & 0xf;

                sum = sum + tick;
            }

            //determine the seed for the rng
            BitArray seedbit = getSubSet(DATASETID_INDEX, 16);




            ushort rng = JB2.Common.RNG.Plumber(0, convertToNumber<ushort>(seedbit)).LastOrDefault();

            for(int i=0;i<sum;i++)
            {
                rng = JB2.Common.RNG.Plumber(rng);
            }

            return rng;



        }



        private bool validateRow(int rowNumber)
        {
            var tick_bit = new BitArray(8);

            for (int i = 0; i < 4; i++)
            {
                tick_bit[i] = _bitarray[(DEWDROPSIZE * (rowNumber - 1)) + TICK1_INDEX + i];
            }

            for (int i = 0; i < 4; i++)
            {
                tick_bit[i + 4] = _bitarray[(DEWDROPSIZE * (rowNumber - 1)) + TICK2_INDEX + i];
            }

            var testValue = convertToNumber<int>(tick_bit);

            return testValue == calculateRowTick(rowNumber);


        }

        private void updateRow(int rowNumber)
        {

            var tick = calculateRowTick(rowNumber);


            //update the row ticks
            BitArray tick_bit = convertToBitArray(tick, 8);

            for(int i = 0; i< 4;i++)
            {
                _bitarray[(DEWDROPSIZE * (rowNumber - 1)) + TICK1_INDEX + i] = tick_bit[i];
            }

            for (int i = 0; i < 4; i++)
            {
                _bitarray[(DEWDROPSIZE * (rowNumber - 1)) + TICK2_INDEX + i] = tick_bit[i + 4];
            }


            //reapply the dataset Checksum
            var checksum = calculateCheckSum();

            var checksum_bit = convertToBitArray(checksum, 16);

            for(int i=0;i < checksum_bit.Length; i++)
            {
                _bitarray[CHECKSUM_INDEX + i] = checksum_bit[i];
            }



        }





        private void initdataset()
        {
            _bitarray = new BitArray(10200);


            //set the drewdrop IDs
            for (int i = 0; i < 250; i++)
            {
                var rowNumber = i + 1;

                BitArray b = convertToBitArray(rowNumber, 16);

                for (int j = 0; j < b.Length; j++)
                {
                    _bitarray[(40 * i) + 4 + j] = b[j];
                }
            }


            //set the dataset uniqueID
            var timediff = DateTime.UtcNow - TICKSTART;

            var ticks = timediff.Ticks + JB2.Common.RNG.Randy;

            this.DewDropDataID = (ulong)ticks;


            for (int i = 0; i < 250; i++)
            {
                updateRow(i+1);
            }
        }





        #endregion Helpers







        public static DrewdropData Empty
        {
            get
            {
                return new DrewdropData();
            }
        }


        public static implicit operator BitArray(DrewdropData d)
        {
            return d._bitarray;
        }
    }
}
