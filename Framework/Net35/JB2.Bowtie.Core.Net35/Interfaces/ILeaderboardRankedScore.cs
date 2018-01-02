using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface  ILeaderboardRankedScore: ILeaderboardEntry
    {
        int Ranked { get; set; }
    }
}
