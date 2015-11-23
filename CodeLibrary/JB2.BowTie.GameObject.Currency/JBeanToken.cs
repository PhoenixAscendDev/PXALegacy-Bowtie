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


        #region Constructor

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

        #endregion Constructor




        public string ImageFrontUri
        {
            get
            {
                return JB2.Bowtie.Economy.Settings.Jbean.GetTokenImageFront(this._type);
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public string ImageBackUri
        {
            get
            {
                return JB2.Bowtie.Economy.Settings.Jbean.GetTokenImageBack(this._type);
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
                return JB2.Bowtie.Economy.Settings.Jbean.GetSetting(JbeanSettingName.CurrencyID).ToString();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public int UnitMultiplier
        {
            get
            {
                return JB2.Bowtie.Economy.Settings.Jbean.GetTokenValue(this._type);
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
