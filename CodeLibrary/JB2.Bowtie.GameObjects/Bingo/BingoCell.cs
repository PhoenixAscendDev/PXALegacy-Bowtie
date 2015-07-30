using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;




namespace JB2.Bowtie.GameObjects
{

    public class BingoBall : BingoBall<byte>
    {
        public BingoBall(byte value,string label)
        {
            base.Value = value;
            base.Label = label;           
        }

        public static implicit operator BingoBall(byte b)
        {
            string label = string.Empty;
            if (b <= 15)
                label = "B";
            else if (b <= 30)
                label = "I";
            else if (b <= 45)
                label = "N";
            else if (b <= 50)
                label = "G";
            else if (b <= 75)
                label = "O";
            return new BingoBall(b, label+"-"+b.ToString());
            //Type gType = typeof(T);
            //switch(gType)
            //{
            //    case typeof(byte):
            //    case typeof(int):
            //    case typeof(short):
            //    case typeof(long):
            //        string label = string.Empty;
            //        var b = (byte)T;
            //        if (b <= 15)
            //            label = "B";
            //        else if (b <= 30)
            //            label = "I";
            //        else if (b <= 45)
            //            label = "N";
            //        else if (b <= 50)
            //            label = "G";
            //        else if (b <= 75)
            //            label = "O";
            //        return new BingoBall<T>(v, label);
            //    default:
            //        return new BingoBall<T>(v, string.Empty);


            ////switch(T.GetType())
            //string label = string.Empty;
            //if (b <= 15)
            //    label = "B";
            //else if (b <= 30)
            //    label = "I";
            //else if (b <= 45)
            //    label = "N";
            //else if (b <= 50)
            //    label = "G";
            //else if (b <= 75)
            //    label = "O";

            // return new BingoBall<T>(b, string.Empty);
        }
        

    }
     



    public class BingoBall<T>
    {

        public BingoBall()
        {
        }

        public BingoBall(T value, string label)
        {
            this.Value = value;
            this.Label = label;
        }
        //private string _label
        //private T _value;

        public string Label
        {
            get;
            set;
        }

        public T Value
        {
            get;
            set;
        }

        public static implicit operator KeyValuePair<string,T>(BingoBall<T> c)
        {
            return new KeyValuePair<string, T>(c.Label, c.Value);
        }

        public static implicit operator T (BingoBall<T> c)
        {
            return c.Value;

        }

        

    }
}
