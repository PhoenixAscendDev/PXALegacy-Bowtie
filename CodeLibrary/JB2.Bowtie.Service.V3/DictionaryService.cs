using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Bowtie;
using JB2.Bowtie.Extensions;

namespace JB2.Bowtie.Service
{
    public class DictionaryService : JB2.Common.Singleton<DictionaryService>
    {
        #region Fields

        protected IUnitOfWork _uofw;

        #endregion Fields

        #region Constructors
        public DictionaryService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }

        public DictionaryService(IUnitOfWork unitofwork)
        {
            _uofw = unitofwork;
        }


        

        #endregion Constructors



        public ServiceResult<Dictionary<string,string>> RetrieveApplicationActivities(IApplication application)
        {
            try
            {

                var data = _uofw.DictionaryRepository.GetApplicationActivities(application.ID);

                return new JB2.Common.ServiceResult<Dictionary<string, string>>(data);
            }
            catch (Exception ex)
            {
                return ex.ToServiceResult<Dictionary<string, string>>();
            }
        }

        public ServiceResult InsertActivities(IApplication application, Dictionary<string,string> activities)
        {
            try
            {

                _uofw.DictionaryRepository.InsertActivities(application.ID, activities);

                return true;
            }
            catch (Exception ex)
            {
                return ex.ToServiceResult();
            }
        }

        
    }
}
