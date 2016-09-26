using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public struct CurrencyAmountPair
    {
        #region Fields
        private ICurrency _currency;
        private double _amount;
        private JB2.Common.DoubleRange _range;
        #endregion Fields

        #region Constructors
        public CurrencyAmountPair(ICurrency currency, double amount): this(currency,amount,0.00,double.MaxValue)
        {
            
        }
        public CurrencyAmountPair(ICurrency currency, double amount, double minAllowed, double maxAllowed)
        {
            _currency = currency;
            _amount = amount;
            _range = new Common.DoubleRange(minAllowed, maxAllowed);
        }

        #endregion Constructors

        #region Properties

        public ICurrency Currency
        {
            get
            {
                return _currency;
            }
        }

        public double Amount
        {
            get
            {
                return _amount;
            }
        }
        #endregion Properties


        #region Methods

        public void IncreaseAmount(double amount)
        {
            _amount = _amount + Amount;
            if (_amount < _range.Min)
                _amount = _range.Min;
            if (_amount > _range.Max)
                _amount = _range.Max;
        }

        public void IncreaseAmount(int amount)
        {
            IncreaseAmount(Convert.ToDouble(amount));
        }

        public void DecreaseAmount(double amount)
        {
            IncreaseAmount(amount * -1);
        }

        public void DecreaseAmount(int amount)
        {
            DecreaseAmount(Convert.ToDouble(amount));
        }

        #endregion Methods

        #region Implicit Operators

        public static implicit operator double(CurrencyAmountPair cap)
        {
            return cap.Amount;
        }

        public static implicit operator long(CurrencyAmountPair cap)
        {
            return (long)cap.Amount;
        }


        public static CurrencyAmountPair operator +(CurrencyAmountPair a, CurrencyAmountPair b)
        {
            if (a.Currency.ID == b.Currency.ID)
                return new CurrencyAmountPair(a.Currency, a.Amount + b.Amount);
            else
                return a;
        }

        #endregion Implicit Operators
    }
}
