using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.BitScore
{
    public class PlayerPoint : JB2.Bowtie.IPlayerable<string>
    {
        #region Fields

        protected string _playerid;
        protected int _points;

        #endregion Fields


        #region Constructors

        public PlayerPoint(string playerID, int points)
        {
            _playerid = playerID;
            _points = points;
        }

        #endregion Constructors


        #region Properties
        public string PlayerID
        {
            get
            {
                return _playerid;
            }
            set
            {
                _playerid = value;
            }
        }

        public int Points
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
            }
        }

        #endregion Properties

        public string GetPlayerID()
        {
            return _playerid;
        }
        #region Static Implicit
        public static implicit operator int(PlayerPoint pp)
        {
            return pp.Points;
        }

        #endregion Static Implicit


    }
}
