using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Service
{
    public class ModuleService : GenericService<IModule, IModuleRepository>
    {

        #region Constructors
        public ModuleService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }

        public ModuleService(IUnitOfWork unitOfWork) : this(unitOfWork.ModuleRepository)
        {
            _uofw = unitOfWork;
        }

        public ModuleService(IModuleRepository repo): base(repo)
        {
            _uofw = JB2.Settings.Bowtie.UnitOfWork;
        }
        #endregion Constructors

        IModule RetreieveByName(string name)
        {
            return _repo.GetByName(name);
        }


    }
}
