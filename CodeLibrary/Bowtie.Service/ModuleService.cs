using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Service
{
    public class ModuleService : GenericService<IModule, IModuleRepository>
    {
        IModule RetreieveByName(string name)
        {
            return _repo.GetByName(name);
        }

    }
}
