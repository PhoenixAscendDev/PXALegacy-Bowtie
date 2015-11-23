using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Economy
{
    public struct CurrencyAmountPair
    {
        #region Fields
        private ICurrency _currency;
        private double _amount;
        #endregion Fields

        #region Constructors
        public CurrencyAmountPair(ICurrency currency, double amount)
        {
            _currency = currency;
            _amount = amount;
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
