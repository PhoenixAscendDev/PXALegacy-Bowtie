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
