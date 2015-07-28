using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;

namespace JB2.Bowtie.Economy
{
    public class JBeanToken : IDenomination<Enum.JBeanTokenType>
    {
        private Enum.JBeanTokenType _type;
        private string _name;
        private string _serialNumber;
        
        

        public JBeanToken(): this(Enum.JBeanTokenType.Kidney)
        {

        }

        public JBeanToken(Enum.JBeanTokenType type): this(null,type)
        {

        }

        public JBeanToken(string serialNumber,Enum.JBeanTokenType type)
        {
            _type = type;
            _serialNumber = String.IsNullOrEmpty(serialNumber) ? string.Empty : serialNumber;
        }




        public string ImageFrontUrl
        {
            get
            {
                   return JB2.Bowtie.Economy.Settings.JBean.GetFrontImage(this._type);
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public string ImageBackUrl
        {
            get
            {
                return JB2.Bowtie.Economy.Settings.JBean.GetBackImage(this._type);
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public string CurrencyID
        {
            get
            {
                return JB2.Bowtie.Economy.Settings.JBean.CurrencyID;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public byte UnitMultiplier
        {
            get
            {
                return JB2.Bowtie.Economy.Settings.JBean.GetMultiplier(this._type);
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public bool isSubUnit
        {
            get
            {
                return false;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public string SerialNumber
        {
            get
            {
                return _serialNumber;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public string ID
        {
            get
            {
                return _serialNumber;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                    return System.Enum.GetName(typeof(Enum.JBeanTokenType), this._type);
                else
                    return _name;
            }
            set
            {
                _name = value;
            }
        }

        public Enum.JBeanTokenType DenominationType
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }


        public static implicit operator int(JBeanToken r)
        {
            return (int)r.UnitMultiplier;
        }


    }
}
