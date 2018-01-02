using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public class BasicApplicationPlayerPair<Tplayer, Tapplication> : IApplicationPlayerPair<Tplayer, Tapplication>
    {
        #region Fields

        protected Tplayer _playerID;
        protected Tapplication _applicationID;

        #endregion Fields

        #region Constructors
        public BasicApplicationPlayerPair()
        {

        }

        public BasicApplicationPlayerPair(Tapplication applicationID, Tplayer playerID)
        {
            _playerID = playerID;
            _applicationID = applicationID;
        }

        #endregion Constructors

        public Tplayer PlayerID
        {
            get
            {
                return _playerID;
            }
            set
            {
                _playerID = value;
            }
        }
        public virtual Tapplication ApplicationID
        {
            get
            {
                return _applicationID;
            }
            set
            {
                _applicationID = value;
            }
        }

        public virtual Tapplication GetApplicationID()
        {
            return _applicationID;
        }

        public virtual string GetKey()
        {
            return _applicationID + ">*<" + _playerID;
        }

        public override string ToString()
        {
            return GetKey();
        }

        public virtual Tplayer GetPlayerID()
        {
            return _playerID;
        }
    }
}
