using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using JB2.Bowtie;


namespace JB2.Bowtie.Web.Controllers
{
    public class BaseController : Controller
    {
        protected Enum.RepoDataSource _repoSource;
        protected JB2.Bowtie.IUnitOfWork _unitOfWork;

        public BaseController()
        {
            switch(JB2.Settings.Bowtie.Mode)
            {
                case Enum.APIMode.Debug:
                    ChangeDataSource(Enum.RepoDataSource.Standard);
                    break;
                default:
                    ChangeDataSource(Enum.RepoDataSource.NoDB);
                    break;
            }


        }

        public BaseController(bool useTestData)
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
            switch(source)
            {
                case Enum.RepoDataSource.Standard:
                    _unitOfWork = new JB2.Bowtie.Data.Azure.UnitOfWork();
                    break;
                default:
                    _unitOfWork = new JB2.Bowtie.Data.NoDB.UnitofWork();
                    break;
            }
            _repoSource = source;
            return true;
        }

        #region BowtieServices


        #region Achievement
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

        #endregion Achievement

        #region DewDrop

        private JB2.Bowtie.Service.DewdropService GetDewdropService(int? testCount)
        {
            return new Service.DewdropService(_unitOfWork);
        }

        private JB2.Bowtie.Service.DewdropService GetDewdropService()
        {
            return this.GetDewdropService(null);
        }

        protected JB2.Bowtie.Service.DewdropService DewdropService
        {
            get
            {
                return this.GetDewdropService();
            }
        }

        #endregion DewDrop

        #region Application
        protected JB2.Bowtie.Service.ApplicationService ApplicationService
        {
            get
            {
                return this.GetApplicationService();
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

        #endregion Application

        #region Graph

        private JB2.Bowtie.Service.GraphService GetGraphService(int? testCount)
        {
            return new Service.GraphService(_unitOfWork);
        }

        private JB2.Bowtie.Service.GraphService GetGraphService()
        {
            return this.GetGraphService(null);
        }

        protected JB2.Bowtie.Service.GraphService GraphService
        {
            get
            {
                return this.GetGraphService();
            }
        }

        #endregion Graph

        protected List<SelectListItem> applicationSelectList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            
            var appsAll = this.ApplicationService.Retrieve();

            foreach(IApplication a in appsAll)
            {
                items.Add(new SelectListItem { Text = a.GetName(), Value = a.GetID() });
            }
            return items;
        }

        protected List<SelectListItem> graphactionsSelectList()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var list = this.GraphService.RetrieveActions();

            foreach(IGraphElement a in list)
            {
                items.Add(new SelectListItem { Text = a.GetName(), Value = a.GetID() });
            }
            return items;

        }

        #endregion
    }
}