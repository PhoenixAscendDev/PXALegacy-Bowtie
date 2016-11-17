using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie
{
    public interface IPlayerStory : IPlayerable<string>, IIDNamePair<string, string>, IApplicationable<string>
    {
        string ActionName { get; set; }

        string ObjectName { get; set; }

        DateTime CreateDate { get; set; }

        IEnumerable<IMetaData> ObjectData { get; set; }

        IEnumerable<IMetaData> ActionData { get; set; }

        IEnumerable<IMetaData> PlayerData { get; set; }

        int ActionWeight { get; set; }

    }
}
