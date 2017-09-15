using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;

namespace JB2.Bowtie.ConsoleTest.V3
{
    class Program
    {
        static private BitArray convertInt8(int number)
        {
            //string s = Convert.ToString(number, 2);

            bool[] bits = Convert.ToString((int)number, 2).PadLeft(8, '0').Select(s => s.Equals('1')).ToArray();

            Array.Reverse(bits);

            BitArray result = new BitArray(bits);

            return result;
        }


        static private BitArray convertToBitArray(long number, int size = 64)
        {
            //string s = Convert.ToString(number, 2);

            bool[] bits = Convert.ToString((long)number, 2).PadLeft(size, '0').Select(s => s.Equals('1')).ToArray();

            Array.Reverse(bits);

            BitArray result = new BitArray(bits);

            return result;
        }

        static private BitArray convertToBitArray(ulong number, int size = 16)
        {
            return convertToBitArray((long)number, size);
        }



        static private T convertToNumber<T>(BitArray b)
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




        static void Main(string[] args)
        {

            var JB2 = new JB2.Common.Business();
            JB2.ID = "jb2-centreville";
            JB2.Name = "JBsquared LLC";

            var dataset = DrewdropData.Empty;

            Application a = new Application();

            a.ID = "a600dcba";
            a.Name = "Link Fence";
            a.Website = "http://linkfence.io";
            a.Company = JB2;

            Application a2 = new Application();
            a2.ID = "a4cc70f2";
            a2.Name = "FiveTwo";
            a2.Website = "http://fivetwo.io";
            a2.Company = JB2;


            List<Application> apps = new List<Application>();

            apps.Add(a);
            apps.Add(a2);
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(apps));



            var uofw = new JB2.Bowtie.UnitofWork();


            var a3 = uofw.ApplicationRepository.GetById("a600dcba");

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(a3));




            //dataset.DewDropDataID = 5660304; // ulong.MinValue;

            // var test = BitConverter.GetBytes(ulong.MaxValue);

            // ulong foo = ulong.MinValue;
            // Console.WriteLine(Convert.ToString((long)foo, 2));



            // BitArray ba = (BitArray)dataset;

            // for (int i = 0; i < ba.Length;i++)
            // {
            //     Console.Write(ba[i] ? 1 : 0);
            // }

            // Stopwatch sw = new Stopwatch();
            // sw.Start();
            // dataset.IncrementDrewDrop(1);
            // sw.Stop();
            // Console.WriteLine(sw.Elapsed);
            // dataset.IncrementDrewDrop(2);

            // Console.WriteLine("ID -> " + dataset.DewDropDataID);
            // Console.WriteLine("Count -> " + dataset.GetDrewDropCount(1));


            // for (int i = 1; i <= 250; i++)
            // {
            //     ba = dataset.GetDrewDropRow(i);
            //     Console.Write("row " + i.ToString().PadLeft(3, '0') + " -> ");
            //     foreach (bool b in ba)
            //     {
            //         Console.Write(b ? 1 : 0);
            //     }
            //     Console.Write("\n");
            // }


            // BitArray hacked = (BitArray)dataset;

            //// hacked[] = true;

            // dataset = new DrewdropData(hacked);

            // Console.Write(dataset.Validate());





            //ushort rng = 0256;

            //int x = (rng >> ( * 3)) & 0xf;

            //Console.WriteLine(x);




            //BitArray t = new BitArray(64);

            //string s = Convert.ToString(3, 2);

            //int[] bits = s.PadLeft(8, '0') // Add 0's from left
            // .Select(c => int.Parse(c.ToString())) // convert each char to int
            // .ToArray(); // Convert IEnumerable from select to Array

            //BitArray t2 = new BitArray(8);


            //Array.Reverse(bits);

            //for (int i=0;i < 8; i++)
            //{
            //    t[10+i] = bits[i] == 1 ? true : false;


            //}




            //for (int i = 0; i < 8; i++)
            //{
            //    t2[i] = t[10+i];


            //}



            //foreach (var b in t)
            //{
            //    Console.Write( (bool)b == false ? 0 : 1);

            //}
            //Console.WriteLine();
            //foreach (var b in t2)
            //{
            //    Console.Write((bool)b == false ? 0 : 1);

            //}
            //Console.WriteLine();

            //bool[] b2 = new bool[t2.Count];
            //t2.CopyTo(b2, 0);

            //foreach (var b in b2)
            //{
            //    Console.Write((bool)b == false ? 0 : 1);

            //}

            //Console.WriteLine();

            //int[] final = new int[1];

            //t2.CopyTo(final, 0);
            //Console.WriteLine(final[0]);

            //BitArray smallInt = convertToBitArray(short.MaxValue);


            //foreach (var b in smallInt)
            //{
            //    Console.Write((bool)b == false ? 0 : 1);

            //}
            //Console.WriteLine();

            //var array = new byte[8];
            //smallInt.CopyTo(array, 0);
            //Console.WriteLine(BitConverter.ToUInt64(array, 0));

            //Console.WriteLine(convertToNumber<ulong>(smallInt));





            Console.ReadLine();



           





        }
    }
}
