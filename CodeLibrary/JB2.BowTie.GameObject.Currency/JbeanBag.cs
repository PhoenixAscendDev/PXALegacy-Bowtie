using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Economy
{
    public struct JBeanBag
    {
        #region Fields
        private int _kidney;
        private int _navy;
        private int _pinto;

        #endregion Fields

        public JBeanBag(int kidney, int navy, int pinto)
        {
            _kidney = kidney;
            _navy = navy;
            _pinto = pinto;
        }

        public int Kidney { get { return _kidney; } }
        public int Navy { get { return _navy; } }
        public int Pinto { get { return _pinto; } }


        #region Implicit Operator

        public static implicit operator JBeanBag(int num)
        {
            int kidneyValue = JB2.Bowtie.Jbean.GetTokenValue(Enum.JBeanTokenType.Kidney);
            int navyValue = JB2.Bowtie.Jbean.GetTokenValue(Enum.JBeanTokenType.Navy);
            int pintoValue = JB2.Bowtie.Jbean.GetTokenValue(Enum.JBeanTokenType.Pinto);

            int pintoCount = num / pintoValue;

            num = num % pintoValue;

            int navyCount = num / navyValue;

            int kidneyCount = num % navyValue;

            return new JBeanBag(kidneyCount, navyCount, pintoCount);
        }

        public static implicit operator int(JBeanBag bag)
        {
            int kidneyValue = JB2.Bowtie.Jbean.GetTokenValue(Enum.JBeanTokenType.Kidney);
            int navyValue = JB2.Bowtie.Jbean.GetTokenValue(Enum.JBeanTokenType.Navy);
            int pintoValue = JB2.Bowtie.Jbean.GetTokenValue(Enum.JBeanTokenType.Pinto);

            int result = (bag.Pinto * pintoValue) + (bag.Navy * navyValue) + (bag.Kidney & kidneyValue);

            return result;
        }

        public static implicit operator string(JBeanBag bag)
        {
            return string.Format("Pinto:{2}: Navy:{1}; Kidney:{0}; ", bag.Kidney.ToString(), bag.Navy.ToString(), bag.Pinto.ToString());
        }

        #endregion Implicit Operator

        #region ToString

        public override string ToString()
        {
            return (string)this;
        }

        #endregion ToString
    }
}
