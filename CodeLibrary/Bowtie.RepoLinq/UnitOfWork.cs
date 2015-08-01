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

        public JB2.Bowtie.IGameCommandRepository GameCommandRepository
        {
            get
            {
                return (IGameCommandRepository)GetRepository(RepositoryType.GameCommand);
            }
        }

        public JB2.Bowtie.ILeaderboardRepository LeaderboardRepository
        {
            get
            {
                return (ILeaderboardRepository)GetRepository(RepositoryType.Leaderboard);
            }
        }
      

        public object GetRepository(JB2.Bowtie.Enum.RepositoryType repository)
        {
            return GetRepository(repository, Enum.RepoDataSource.Standard);
        }



        private object GetRepository(JB2.Bowtie.Enum.RepositoryType repository, JB2.Bowtie.Enum.RepoDataSource datasource)
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
                            case RepositoryType.Achievement:
                                _repos.Add(repository, new JB2.Bowtie.Data.Linq.AchievementRepository(_dataContext));
                                break;
                            case RepositoryType.GameCommand:
                                _repos.Add(repository, new JB2.Bowtie.Data.Linq.GameCommandRepository(_dataContext));
                                break;
                            case RepositoryType.Leaderboard:
                                _repos.Add(repository, new JB2.Bowtie.Data.Linq.LeaderboardRepository(_dataContext));
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