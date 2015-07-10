using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;

using JB2.Bowtie;
using JB2.Bowtie.Enum;

namespace Bowtie.WebAPI.Models
{
    public class UnitOfWork : JB2.Bowtie.IUnitOfWork
    {
        private Dictionary<JB2.Bowtie.Enum.RepositoryType, object> _repos;
        private JB2.Bowtie.Data.BowtieDataContext _dataContext;
        private JB2.Bowtie.Enum.RepoDataSource _defaultSource;


        public UnitOfWork()
        {
            _repos = new Dictionary<RepositoryType, object>();
            _defaultSource = RepoDataSource.NoDB;
        }

        public UnitOfWork(RepoDataSource defaultSource) : this()
        {
            _defaultSource = defaultSource;
        }

        public UnitOfWork(JB2.Bowtie.Data.BowtieDataContext dataContext)
            : this()
        {
            _dataContext = dataContext;
            _defaultSource = RepoDataSource.Standard;
        }

        public JB2.Bowtie.IApplicationRepository ApplicationRepository
        {
            get
            {
                return GetApplicationRepository();
            }

        }

        public JB2.Bowtie.IApplicationRepository GetApplicationRepository(JB2.Bowtie.Enum.RepoDataSource repoType)
        {
            return (IApplicationRepository)GetRepository(RepositoryType.Application);
        }


        public IApplicationRepository GetApplicationRepository()
        {
            return (IApplicationRepository)GetRepository(RepositoryType.Application);
        }

        public object GetRepository(JB2.Bowtie.Enum.RepositoryType repository)
        {
            return GetRepository(repository, _defaultSource);
        }



        public object GetRepository(JB2.Bowtie.Enum.RepositoryType repository, JB2.Bowtie.Enum.RepoDataSource datasource)
        {
            switch(datasource)
            {
                case RepoDataSource.Standard:
                    if (_repos[repository] == null)
                    {
                        switch(repository)
                        {
                            case RepositoryType.Application:
                                _repos.Add(repository, new JB2.Bowtie.Data.ApplicationRepository(_dataContext));
                                break;
                        
                        }
                    }
                    return _repos[repository];
                case RepoDataSource.NoDB:
                    switch(repository)
                    {
                        case RepositoryType.Application:
                            return new JB2.Bowtie.NoDBData.ApplicationRepository();
                        default:
                            return null;
                    }
            }
            return null;
        }



        public void Dispose()
        {
            _dataContext.Dispose();
            //throw new NotImplementedException();
        }

    }
}