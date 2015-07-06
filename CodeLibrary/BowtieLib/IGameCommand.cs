using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IGameCommand
    {
        public IPlayer IssuedPlayer { get; set; }
        public IPlayer AffectedPlayer { get; set; }
        public string CommandCode { get; set; }
        public string AppID { get; set; }
        public string GameID { get; set; }

        public string ToPacket();
    }
}
