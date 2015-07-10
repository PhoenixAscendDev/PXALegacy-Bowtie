using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface ILeaderboard : IBowtieObject, JB2.Common.IIDNamePair<string, string>
    {
        Enum.ScoreOrderType OrderType { get; set; }

    }
}
