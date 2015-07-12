using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;

using JB2.Bowtie;
using JB2.Bowtie.Enum;

namespace JB2.Bowtie.Data.Linq
{
    public class UnitOfWork : JB2.Bowtie.IUnitOfWork, IDisposable
    {
        private Dictionary<JB2.Bowtie.Enum.RepositoryType, object> _repos;
        private BowtieDataContext _dataContext;
        //private JB2.Bowtie.Enum.RepoDataSource _defaultSource;


        public UnitOfWork() : this( new BowtieDataContext())
        {

        }

        

        public UnitOfWork(BowtieDataContext dataContext)
            : this()
        {
            _repos = new Dictionary<RepositoryType, object>();
            _dataContext = dataContext;
        }

        public JB2.Bowtie.IApplicationRepository ApplicationRepository
        {
            get
            {
                return (IApplicationRepository)GetRepository(RepositoryType.Application);
            }
        }

        public JB2.Bowtie.IAchievementRepository AchievementRepository
        {
            get
            {
                return (IAchievementRepository)GetRepository(RepositoryType.Achievement);
            }
        }
      

        public object GetRepository(JB2.Bowtie.Enum.RepositoryType repository)
        {
            return GetRepository(repository, Enum.RepoDataSource.Standard);
        }



        public object GetRepository(JB2.Bowtie.Enum.RepositoryType repository, JB2.Bowtie.Enum.RepoDataSource datasource)
        {
            switch(datasource)
            {
                case RepoDataSource.Standard:
                    if (!_repos.ContainsKey(repository))
                    {
                        switch(repository)
                        {
                            case RepositoryType.Application:
                                _repos.Add(repository, new JB2.Bowtie.Data.Linq.ApplicationRepository(_dataContext));
                                break;
                        
                        }
                    }
                    return _repos[repository];
            }
            return null;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dataContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}