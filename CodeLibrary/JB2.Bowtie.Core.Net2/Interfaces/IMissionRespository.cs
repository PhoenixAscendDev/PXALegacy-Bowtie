using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface IMissionRespository : JB2.Common.IRepository<JB2.Bowtie.IMission, string>
    {
        IEnumerable<IMission> GetMissionsByGroupID(string id);
    }
}
