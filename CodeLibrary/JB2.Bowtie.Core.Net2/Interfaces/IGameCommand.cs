using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface IGameCommand : IBowtieObject, JB2.Common.IIDNamePair<string, string>
    {
        string IssuedPlayerID { get; set; }
        string AffectedPlayerID { get; set; }
        string CommandCode { get; set; }
        string AppID { get; set; }
        string GameID { get; set; }

        string ToPacket();
    }
}
