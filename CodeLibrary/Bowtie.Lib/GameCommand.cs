using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class GameCommand : BowtieObject,IGameCommand,IBowtieObject
    {
        private IPlayer _issuedPlayer;
        private IPlayer _affectedPlayer;
        private string _commandCode;
        private string _applicationID;
        private string _gameID;


        public GameCommand() : this(null)
        {

        }

        public GameCommand(string id) : base(Enum.BowtieObjectType.bowtie_command,id)
        {

        }
        public IPlayer IssuedPlayer
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

        public IPlayer AffectedPlayer
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
            var cmdCode = "1223344444";

            System.Text.StringBuilder finalCmd = new System.Text.StringBuilder();
            finalCmd.Append(cmdCode);

            //System.Text.StringBuilder keyAndCmd = new System.Text.StringBuilder();

            //for (int i = 0; i < appKey.Length; i++)
            //{
            //    keyAndCmd.Append(appKey[i]);
            //    keyAndCmd.Append(cmdCode[i]);
            //}
            //var checksum = JB2.Bowtie.Utility.GetChecksum(keyAndCmd.ToString(), 16);
            //var byte[] test2 = 23;


            //char[] reverseChecksum = checksum.ToString().ToCharArray();
            //Array.Reverse(reverseChecksum);

            //string strchecksum = new string(reverseChecksum);


           

            //finalCmd.Append(strchecksum.Substring(0, 3));
            //finalCmd.Append(keyAndCmd.ToString());
            //finalCmd.Append(strchecksum.Substring(3, 2));

            return finalCmd.ToString();
        }
    }
}
