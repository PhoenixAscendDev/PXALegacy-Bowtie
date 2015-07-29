using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace JB2.Bowtie.GameObjects
{
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

    }
}
