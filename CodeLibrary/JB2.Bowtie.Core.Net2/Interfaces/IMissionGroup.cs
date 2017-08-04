using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface IMissionGroup : IDewdrop, IPointGiver
    {
        IEnumerable<IMission> GetMissions();
    }
}
