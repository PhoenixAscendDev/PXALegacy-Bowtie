using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie.Service
{
    public class ApplicationService : JB2.Common.Singleton<ApplicationService>
    {
        #region Fields

        protected IUnitOfWork _uofw;

        #endregion Fields

        #region Constructors
        public ApplicationService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }

        public ApplicationService(IUnitOfWork unitofwork)
        {
            _uofw = unitofwork;
        }

        #endregion Constructors


        public ServiceResult<IApplication> RetrieveApplicationById(string id)
        {
            try
            {
                var app = _uofw.ApplicationRepository.GetById(id);
                return new ServiceResult<IApplication>(app);
            }
            catch(Exception ex)
            {
                return new ServiceResult<IApplication>(ex);
            }
        }
    }
}
