using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using JB2.Common;

namespace JB2.Bowtie
{
    public class AchievementData
    {
        #region Const
        private const int TOTALSIZE = 10240;
        private const int ACHIEVEMENTSIZE = 80;
        private const int STEP_INDEX = 0;
        private const int TIME_INDEX = 16;
        private const int DAY_INDEX = 33;
        private const int ID_INDEX = 48;
        private const int STATUS_INDEX = 56;
        private const int TYPE_INDEX = 60;
        private const int POINTS_INDEX = 64;

        private const int TICK1_INDEX = 72;
        private const int TICK2_INDEX = 76;
        private const int TOTALROWS = 95;

        
        private const int DATASETID_INDEX = 7920;
        private const int CHECKSUM_INDEX = 7984;
        private readonly DateTime TICKSTART = new DateTime(2017, 9, 22).ToUniversalTime();

        #endregion Const


        #region Fields
        protected BitArray _bitarray;
        #endregion Fields

        #region Constructor

        public AchievementData(BitArray dataset)
        {
            _bitarray = dataset;
        }

        protected AchievementData()
        {
            initdataset();
        }

        #endregion Constructor

        #region Properties

        public ulong AchievementDataID
        {
            get
            {
                BitArray a = _bitarray.GetSubSet(DATASETID_INDEX, 64);

                return a.ToNumber<ulong>(); //.convertToNumber<ulong>(a);

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

                var array = value.ToBitArray(64);// convertToBitArray(value, 64);

                var index = DATASETID_INDEX;
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
            for (int i = 0; i < TOTALROWS; i++)
            {
                if (!validateRow(i + 1))
                    result = false;
            }

            //validate the checksum


            BitArray checksum_bit = _bitarray.GetSubSet(CHECKSUM_INDEX, 16);

            var testCheckSum = checksum_bit.ToNumber<ushort>();

            if (testCheckSum != calculateCheckSum())
                result = false;

            return result;
        }

        public int GetStepValue(int storageID)
        {
            var a_bit = GetStorage(storageID);

            var value_bit = a_bit.GetSubSet(STEP_INDEX, 16);

            return value_bit.ToNumber<int>();
        }

        public ServiceResult SetStepValue(int storageID, int step)
        {
            var step_bit = step.ToBitArray(16);


            var index = (ACHIEVEMENTSIZE * (storageID - 1)) + STEP_INDEX;

            for (int i = 0; i < 16; i++)
            {
                _bitarray[index + i] = step_bit[i];
            }

            updateRow(storageID);

            return true;
        }

        public int GetPoints(int storageID)
        {
            var a_bit = GetStorage(storageID);

            var point_bit = a_bit.GetSubSet(POINTS_INDEX, 8);

            return point_bit.ToNumber<int>();
        }

        public ServiceResult SetPoints(int storageID, byte points)
        {
            var points_bit = points.ToBitArray(8);

            var index = (ACHIEVEMENTSIZE * (storageID - 1)) + POINTS_INDEX;

            for (int i = 0; i < 8; i++)
            {
                _bitarray[index + i] = points_bit[i];
            }

            updateRow(storageID);

            return true;
        }

        public Enum.AchievementStatusType GetStatus(int storageID)
        {
            var a_bit = GetStorage(storageID);

            var status_bit = a_bit.GetSubSet(STATUS_INDEX, 4);

            int status_int = status_bit.ToNumber<int>();

            return (Enum.AchievementStatusType)status_int;
        }

        public ServiceResult SetStatus(int storageID, Enum.AchievementStatusType status)
        {
            var a_bit = GetStorage(storageID);

            var status_bit = ((int)status).ToBitArray(4);

            var index = (ACHIEVEMENTSIZE * (storageID - 1)) + STATUS_INDEX;

            for (int i = 0; i < 4; i++)
            {
                _bitarray[index + i] = status_bit[i];
            }

            updateRow(storageID);

            return true;


        }

        public DateTime GetDateAchieved(int storageID)
        {
            var a_bit = GetStorage(storageID);

            var timeHR_bit = a_bit.GetSubSet(TIME_INDEX, 5);
            var timeMIN_bit = a_bit.GetSubSet(TIME_INDEX + 5, 6);
            var timeSEC_bit = a_bit.GetSubSet(TIME_INDEX + 11, 6);

            var day_bit = a_bit.GetSubSet(DAY_INDEX, 15);

            var hr = timeHR_bit.ToNumber<int>();
            var min = timeMIN_bit.ToNumber<int>();
            var sec = timeSEC_bit.ToNumber<int>();

            var day = day_bit.ToNumber<int>();

            DateTime start = new DateTime(TICKSTART.Year, TICKSTART.Month, TICKSTART.Day, hr, min, sec, DateTimeKind.Utc);
            start = start.AddDays(day);

            return start;
        }

        public ServiceResult SetDateAcheived(int storageID, DateTime dt)
        {
            var utc = dt.ToUniversalTime();

            var hr_bit = utc.Hour.ToBitArray(5);
            var min_bit = utc.Minute.ToBitArray(6);
            var sec_bit = utc.Second.ToBitArray(6);

            TimeSpan span = utc - TICKSTART;

            var day_bit = span.Days.ToBitArray(15);

            var index = (ACHIEVEMENTSIZE * (storageID - 1)) + TIME_INDEX;
            for (int i = 0; i < 5; i++)
            {
                _bitarray[index + i] = hr_bit[i];
            }
            for (int i = 0; i < 6; i++)
            {
                _bitarray[index + 5 + i] = min_bit[i];
            }
            for (int i = 0; i < 6; i++)
            {
                _bitarray[index + 15 + i] = sec_bit[i];
            }

             index = (ACHIEVEMENTSIZE * (storageID - 1)) + DAY_INDEX;
            for (int i = 0; i < 15; i++)
            {
                _bitarray[index + i] = day_bit[i];
            }

            updateRow(storageID);

            return true;



        }


        public BitArray GetStorage(int storageID)
        {
            int rowNum = storageID;
            if ((rowNum < 0) || (rowNum > TOTALROWS))
                throw new ArgumentException("StorageID should be between 1 and " + TOTALROWS);
            return getRow(rowNum);
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
                int index = rowid * ACHIEVEMENTSIZE;
                bool[] data = new bool[ACHIEVEMENTSIZE];

                for (int i = 0; i < ACHIEVEMENTSIZE; i++)
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

        private void initdataset()
        {
            _bitarray = new BitArray(8000);


            //set the drewdrop IDs
            for (int i = 0; i < TOTALROWS; i++)
            {
                var rowNumber = i + 1;

                BitArray b = convertToBitArray(rowNumber, 16);

                for (int j = 0; j < b.Length; j++)
                {
                    _bitarray[(ACHIEVEMENTSIZE * i) + ID_INDEX + j] = b[j];
                }
            }


            //set the dataset uniqueID
            var timediff = DateTime.UtcNow - TICKSTART;

            var ticks = timediff.Ticks + JB2.Common.RNG.Randy;

            this.AchievementDataID = (ulong)ticks;


            for (int i = 0; i < TOTALROWS; i++)
            {
                updateRow(i + 1);
            }



        }

        private int calculateRowTick(int rowNumber)
        {
            var b = getRow(rowNumber);

            //get the value and use it to seed the RNG
            var bvalue = new BitArray(16);
            var index = STEP_INDEX;
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


        private ushort calculateCheckSum()
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
                    tick_bit[j] = _bitarray[(ACHIEVEMENTSIZE * (i - 1)) + TICK1_INDEX + j];
                }

                for (int j = 0; j < 4; j++)
                {
                    tick_bit[j + 4] = _bitarray[(ACHIEVEMENTSIZE * (i - 1)) + TICK2_INDEX + j];
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


        private void updateRow(int rowNumber)
        {

            var tick = calculateRowTick(rowNumber);


            //update the row ticks
            BitArray tick_bit = convertToBitArray(tick, 8);

            for (int i = 0; i < 4; i++)
            {
                _bitarray[(ACHIEVEMENTSIZE * (rowNumber - 1)) + TICK1_INDEX + i] = tick_bit[i];
            }

            for (int i = 0; i < 4; i++)
            {
                _bitarray[(ACHIEVEMENTSIZE * (rowNumber - 1)) + TICK2_INDEX + i] = tick_bit[i + 4];
            }


            //reapply the dataset Checksum
            var checksum = calculateCheckSum();

            var checksum_bit = convertToBitArray(checksum, 16);

            for (int i = 0; i < checksum_bit.Length; i++)
            {
                _bitarray[CHECKSUM_INDEX + i] = checksum_bit[i];
            }
        }

        private bool validateRow(int rowNumber)
        {
            var tick_bit = new BitArray(8);

            for (int i = 0; i < 4; i++)
            {
                tick_bit[i] = _bitarray[(ACHIEVEMENTSIZE * (rowNumber - 1)) + TICK1_INDEX + i];
            }

            for (int i = 0; i < 4; i++)
            {
                tick_bit[i + 4] = _bitarray[(ACHIEVEMENTSIZE * (rowNumber - 1)) + TICK2_INDEX + i];
            }

            var testValue = tick_bit.ToNumber<int>(); // convertToNumber<int>(tick_bit);

            return testValue == calculateRowTick(rowNumber);


        }

        private BitArray convertToBitArray(int number, int size = 32)
        {
            //string s = Convert.ToString(number, 2);

            bool[] bits = Convert.ToString((int)number, 2).PadLeft(size, '0').Select(s => s.Equals('1')).ToArray();

            Array.Reverse(bits);

            BitArray result = new BitArray(bits);

            return result;
        }


        #endregion Helpers

        #region Static
        public static  AchievementData Empty
        {
            get
            {
                return new AchievementData();
            }
        }


        public static implicit operator BitArray(AchievementData d)
        {
            return d._bitarray;
        }


        #endregion Static

    }
}
