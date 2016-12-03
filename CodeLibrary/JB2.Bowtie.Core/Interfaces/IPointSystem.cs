using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie
{
    public interface IPointSystem : JB2.Common.IIDNamePair<string, string>
    {
        string Single { get; set; }
        string Plural { get; set; }

        JB2.Common.WordTense ReceiveTense { get; set; }

        JB2.Common.JB2Image GetIcon(int point);

        ServiceResult AddPointsToPlayer(int points, IPlayerable<string> player);

        ServiceResult Process(PointTransaction tran);

    }
}
