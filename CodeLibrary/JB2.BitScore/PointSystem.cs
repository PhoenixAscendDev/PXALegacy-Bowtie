using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie;
using JB2.Common;

namespace JB2.BitScore
{
    public class BitScoreSystem : JB2.Bowtie.IPointSystem
    {

        #region Constructor

        public BitScoreSystem()
        {

        }

        #endregion Constructor
        public string ID
        {
            get
            {
                return "jb2-bitscore";
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
                return "BitScore";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Plural
        {
            get
            {
                return "Points";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public WordTense ReceiveTense
        {
            get
            {
                return new WordTense()
                {
                    ImperativeTense = "",
                    Past = "",
                    PluralPast = "scored",
                    PluralPresent = "",
                    Present = "is scoring"
                };
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Single
        {
            get
            {
                return "Point";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public ServiceResult AddPointsToPlayer(int points, IPlayerable<string> player, string description)
        {
            throw new NotImplementedException();
        }

        public JB2Image GetIcon(int point)
        {
            return new JB2Image();
        }

        public string GetID()
        {
            return ID;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
