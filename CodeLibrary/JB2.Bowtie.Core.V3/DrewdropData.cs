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
            _bitarray = new BitArray(10200);
        }

        #endregion Constructor


        #region Properties

        public ulong DewDropDataID
        {
            get
            {
                BitArray a = getSubSet(10120, 64);

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

            set
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

        public BitArray GetDrewDropRow(int rowNum)
        {
            if ((rowNum < 0) || (rowNum > 250))
                throw new ArgumentException("Rowid should be between 1 and 250");
            return getRow(rowNum);
        }

        #endregion Properties

        #region Helpers

        private BitArray getRow(int rowid)
        {
            try
            {
                if ((rowid < 0) || (rowid > 255))
                    throw new ArgumentException("Rowid should be between 1 and 255");

                rowid = rowid - 1;
                int index = rowid * 40;
                bool[] data = new bool[40];

                for (int i = 0; i < 40; i++)
                {
                    data[i] = _bitarray[index + i];
                }

                return new BitArray(data);
            }
            catch (Exception ex)
            {
                return new BitArray(0);
            }

            #endregion Helpers

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
