using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using JB2.Bowtie;

namespace JB2.Bowtie.WebAPI.Controllers
{
    public class BaseAPIController : ApiController
    {
        protected Enum.RepoDataSource _repoSource;
        protected IUnitOfWork _unitOfWork;

        public BaseAPIController()
        {
            switch(JB2.Bowtie.Settings.Mode)
            {
                case Enum.APIMode.Debug:
                    ChangeDataSource(Enum.RepoDataSource.Standard);
                    break;
                default:
                    ChangeDataSource(Enum.RepoDataSource.NoDB);
                    break;
            }


        }

        public BaseAPIController(bool useTestData)
        {
            ChangeDataSource(Enum.RepoDataSource.NoDB);
        }

        public virtual bool useTestData
        {
            get
            {
                return (_repoSource == Enum.RepoDataSource.NoDB);
            }
            set
            {
                if (value)
                    this.ChangeDataSource(Enum.RepoDataSource.NoDB);
            }
        }


        protected bool ChangeDataSource(Enum.RepoDataSource source)
        {
            _unitOfWork = JB2.Bowtie.Web.Helper.GetUnitofWork(source);
            
            _repoSource = source;
            return true;
        }


        #region BowtieServices

        protected JB2.Bowtie.Service.AchievementService AchievementService
        {
            get
            {
                return this.GetAchievementService();
            }
        }

        private JB2.Bowtie.Service.AchievementService GetAchievementService()
        {
            return this.GetAchievementService(null);
        }

        private JB2.Bowtie.Service.AchievementService GetAchievementService(int? testCount)
        {
            return new JB2.Bowtie.Service.AchievementService(_unitOfWork);
        }


        protected JB2.Bowtie.Service.ApplicationService ApplicationService
        {
            get
            {
                return this.GetApplicationService();
            }
        }

        protected JB2.Bowtie.Service.GameCommandService GameCommandService
        {
            get
            {
                return this.GetGameCommandService();
            }
        }

        private JB2.Bowtie.Service.ApplicationService GetApplicationService()
        {
            return this.GetApplicationService(null);
        }

        private JB2.Bowtie.Service.ApplicationService GetApplicationService(int? testCount)
        {
            return new JB2.Bowtie.Service.ApplicationService(_unitOfWork);
        }

        private JB2.Bowtie.Service.GameCommandService GetGameCommandService()
        {
            return this.GetGameCommandService(null);
        }

        private JB2.Bowtie.Service.GameCommandService GetGameCommandService(int? testCount)
        {
            return new JB2.Bowtie.Service.GameCommandService(_unitOfWork);
        }





        #endregion

    }
}
