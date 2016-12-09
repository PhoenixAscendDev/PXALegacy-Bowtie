using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Service
{
    public class SystemService
    {

        #region Fields
        protected IUnitOfWork _uofw;
        protected ISystemRepository _repo;

        #endregion Fields

        public SystemService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }

        public SystemService(IUnitOfWork unitOfWork) : this(unitOfWork.SystemRepository)
        {
            _uofw = unitOfWork;
        }

        public SystemService(ISystemRepository repo)
        {
            _repo = repo;
        }

        public List<ISystemConfig<IPointSystem>> RetrievePointConfigs()
        {
            return _repo.GetPointSystems().ToList();
        }

        public List<ISystemConfig<ICurrencySystem>> RetrieveCurrencyConfigs()
        {
            return _repo.GetCurrencySystems().ToList();
        }
    }
}
