using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class LeaderboardScore : IComparable
    {
        #region Fields
        private object _score;
        private Enum.NumberFormatType _type;

        #endregion Fields

        #region Constructors

        private LeaderboardScore(int score)
        {
            _score = score;
            _type = Enum.NumberFormatType.Number;
        }

        private LeaderboardScore(decimal score)
        {
            _score = score;
            _type = Enum.NumberFormatType.Decimal;
        }

        private LeaderboardScore(float score)
        {
            _score = score;
            _type = Enum.NumberFormatType.Decimal;
        }

        private LeaderboardScore(double score)
        {
            _score = score;
            _type = Enum.NumberFormatType.Decimal;
        }

        private LeaderboardScore(TimeSpan score)
        {
            _score = score.Ticks;
            _type = Enum.NumberFormatType.Time;
        }

        private LeaderboardScore(long score)
        {
            _score = score;
            _type = Enum.NumberFormatType.Number;
        }

        private LeaderboardScore(uint score)
        {
            _score = score;
            _type = Enum.NumberFormatType.Number;
        }

        private LeaderboardScore(ulong score)
        {
            _score = score;
            _type = Enum.NumberFormatType.Number;
        }

        private LeaderboardScore(short score)
        {
            _score = score;
            _type = Enum.NumberFormatType.Number;
        }

        private LeaderboardScore(ushort score)
        {
            _score = score;
            _type = Enum.NumberFormatType.Number;
        }

        #endregion Constructors

        #region IComparable

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            LeaderboardScore other = obj as LeaderboardScore;
            if (other == null)
                throw new ArgumentException("Object is not a LeaderboardScore");

            try
            {
                switch (this._type)
                {
                    case Enum.NumberFormatType.Decimal:
                        return Convert.ToDouble(this._score).CompareTo(Convert.ToDouble(other._score));
                    case Enum.NumberFormatType.Number:
                        return Convert.ToInt64(this._score).CompareTo(Convert.ToInt64(other._score));
                    case Enum.NumberFormatType.Currency:
                        return Convert.ToDecimal(this._score).CompareTo(Convert.ToDecimal(other._score));
                    case Enum.NumberFormatType.Time:
                        var thisTicks = Convert.ToInt64(this._score);
                        var otherTicks = Convert.ToInt64(other._score);
                        return thisTicks.CompareTo(otherTicks);
                    default:
                        return 1;
                }
            }
            catch(Exception ex)
            {
                throw new ArgumentException(ex.Message,ex);
            }

            

        }

        #endregion IComparable

        #region Helpers



        #endregion Helpers

        #region Static Implicit

        public static implicit operator int(LeaderboardScore score)
        {
            return Convert.ToInt32(score._score);
        }

        public static implicit operator LeaderboardScore(int score)
        {
            return new LeaderboardScore(score);
        }

        public static implicit operator decimal(LeaderboardScore score)
        {
            return Convert.ToDecimal(score._score);
        }

        public static implicit operator LeaderboardScore(decimal score)
        {
            return new LeaderboardScore(score);
        }

        public static implicit operator double(LeaderboardScore score)
        {
            return Convert.ToDouble(score);
        }

        public static implicit operator LeaderboardScore(double score)
        {
            return new LeaderboardScore(score);
        }

        public static implicit operator TimeSpan(LeaderboardScore score)
        {
            if (score._type == Enum.NumberFormatType.Time)
                return new TimeSpan(Convert.ToInt64(score._score));
            else
            {
                var num = Convert.ToInt64(score._score);
                return new TimeSpan(num);
            }

        }

        public static implicit operator LeaderboardScore(TimeSpan score)
        {
            return new LeaderboardScore(score);
        }

        #endregion Static Implicit

        #region To

        public long ToNumber()
        {
            return (long)this;
        }

        public decimal ToCurrency()
        {
            return (decimal)this;
        }

        public double ToDecimal()
        {
            return (double)this;
        }

        public TimeSpan ToTime()
        {
            return (TimeSpan)this;
        }

        #endregion To

    }
}
