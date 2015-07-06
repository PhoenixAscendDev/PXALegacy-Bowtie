using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class GameCommand : IGameCommand
    {
        public IPlayer IssuedPlayer
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IPlayer AffectedPlayer
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string CommandCode
        {
            get
            {
                return "1223344444";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string AppID
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string GameID
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotImplementedException();
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
