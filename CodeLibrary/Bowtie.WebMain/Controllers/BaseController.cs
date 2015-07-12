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
                    _unitOfWork = new JB2.Bowtie.Data.Linq.UnitOfWork();
                    break;
                default:
                    _unitOfWork = new JB2.Bowtie.Data.NoDB.UnitofWork();
                    break;
            }
            _repoSource = source;
            return true;
        }
    }
}