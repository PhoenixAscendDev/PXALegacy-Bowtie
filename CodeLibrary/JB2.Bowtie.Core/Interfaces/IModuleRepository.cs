using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IModuleRepository : JB2.Common.IRepository<IModule,string>
    {
       IModule GetByName(string name);


        IEnumerable<IPlayerInventoryItem> GetInventoryByPlayerID(string moduleid, string playerid);

        BowtieMetadata GetDataByPlayerID(string moduleid, string playerid);

    }
}
