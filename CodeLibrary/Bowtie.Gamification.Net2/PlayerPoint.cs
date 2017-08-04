using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public struct PlayerPoint : IPlayerPoint
    {

        public string PlayerID { get; set; }
        public int Points { get; set; }


        public string PointSystem { get; set; }


        public string GetPlayerID()
        {
            return PlayerID;
        }

        public override string ToString()
        {
            return this.PlayerID + "_" + this.PointSystem + "_" + this.Points;
        }
    }
}
