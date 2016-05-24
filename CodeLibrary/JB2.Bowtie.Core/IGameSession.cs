using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IGameSession
    {
        string GetSessionID();
        DateTime StartTime();
        DateTime EndTime();
        IDictionary<int, IBowtiePlayer> GetPlayers();
    }
}
