using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IGameCommand
    {
        IPlayer IssuedPlayer { get; set; }
        IPlayer AffectedPlayer { get; set; }
        string CommandCode { get; set; }
        string AppID { get; set; }
        string GameID { get; set; }

        string ToPacket();
    }
}
