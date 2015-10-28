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

        #region Constructors
        public BingoBall(byte value,string label)
        {
            base.Value = value;
            base.Label = label;           
        }

        #endregion Constructors

        #region Implicit Operators

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

        public static implicit operator string(BingoBall ball)
        {
            return ball.Label;
        }

        #endregion Implicit Operators

        #region Convertors


        #endregion Convertors


    }




    public class BingoBall<T> : IDeckable<BingoBall<T>>
        where T : IComparable
    {

        #region Fields

        private T _value;
        private string _label;
        

        #endregion Fields

        #region Constructors
        public BingoBall()
        {
        }

        public BingoBall(T value, string label)
        {
            this.Value = value;
            this.Label = label;
        }

        #endregion Constructors

        //private string _label
        //private T _value;

        #region Properties

        public string Label
        {
            get { return _label; }
            set { _label = value; }
        }

        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string FromDeckId
        {
            get; set;
            
        }

        internal string CompareString
        {
            get
            {
                return this._value.ToString() + ">*<" + this._label;
            }
        }

        #endregion Properties

        #region Implicit Operators

        public static implicit operator KeyValuePair<string,T>(BingoBall<T> c)
        {
            return new KeyValuePair<string, T>(c.Label, c.Value);
        }

        public static implicit operator T (BingoBall<T> c)
        {
            return c.Value;
        }

        public static bool operator ==(BingoBall<T> x, BingoBall<T> y)
        {
            if ((object)x == null) return (object)y == null;
            return x.CompareString == y.CompareString;
        }

        public static bool operator !=(BingoBall<T> x, BingoBall<T> y)
        {
            return !(x == y);
        }




        public static implicit operator string (BingoBall<T> ball)
        {
            return ball.Label;
        }


        #endregion Implicit Operators

        #region ToString

        public override string ToString()
        {
            return (string)this;
        }

        #endregion ToString

        #region Equals


        #endregion Equals

        #region IDeckable

        public int CompareTo(BingoBall<T> ball)
        {
            if (ball == null) return 1;

            return this.Value.CompareTo(ball.Value);
        }



        #endregion IDeckable



    }
    }
