using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Bowtie.Extensions;

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
                return ex.ToServiceResult<IApplication>();
            }
        }

        public ServiceResult<IApplication> GenerateNewApplication(string name, string website,JB2.Common.IBusiness company)
        {
            try
            {

                var a = BasicApplication.New;
                a.Name = name;
                a.Website = website;
                a.Company = company;

                _uofw.ApplicationRepository.Insert(a);

                return new ServiceResult<IApplication>(a);
            }
            catch(Exception ex)
            {
                return ex.ToServiceResult<IApplication>();
            }

        }


        public ServiceResult<string> ExportApplicationToJson(IApplication application)
        {
            try
            {
                var str = _uofw.ApplicationRepository.ExportApplicationToJson(application);

                return new ServiceResult<string>(str);
            }
            catch(Exception ex)
            {
                return ex.ToServiceResult<string>();
            }
        }


        public ServiceResult<IApplication> ImportApplication(string json)
        {
           try
            {
                var a = _uofw.ApplicationRepository.ImportApplicationFromJson(json);

                return new ServiceResult<IApplication>(a);
            }
            catch(Exception ex)
            {
                return ex.ToServiceResult<IApplication>();
            }
        }
    }
}
