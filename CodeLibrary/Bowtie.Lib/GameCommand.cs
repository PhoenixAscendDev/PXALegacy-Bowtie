using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class GameCommand : BowtieObject,IGameCommand,IBowtieObject
    {
        private string _issuedPlayer;
        private string _affectedPlayer;
        private string _commandCode;
        private string _applicationID;
        private string _gameID;


        public GameCommand() : this(null)
        {

        }

        public GameCommand(string id) : base(Enum.BowtieObjectType.bowtie_command,id)
        {

        }
        public string IssuedPlayerID
        {
            get
            {
                return _issuedPlayer;
            }
            set
            {
                _issuedPlayer = value;
            }
        }

        public string AffectedPlayerID
        {
            get
            {
                return _affectedPlayer;
            }
            set
            {
                _affectedPlayer = value;
            }
        }

        public string CommandCode
        {
            get
            {
                return _commandCode;
            }
            set
            {
                _commandCode = value;
            }
        }

        public string AppID
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

        public string GameID
        {
            get
            {
                return _gameID;
            }
            set
            {
                _gameID = value;
            }
        }

        public string ToPacket()
        {
            //var cmdCode = "1223344444";


            string cmdFormat = "{0}>*<{1}>*<{2}>*<{3}";


            return string.Format(cmdFormat, this._applicationID, this._issuedPlayer, this._affectedPlayer, this._commandCode);

        }
    }
}
