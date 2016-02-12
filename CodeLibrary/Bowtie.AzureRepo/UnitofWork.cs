using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.Enum;

namespace JB2.Bowtie.Data.Azure
{
    public class UnitOfWork : JB2.Bowtie.IUnitOfWork, IDisposable
    {
        private Dictionary<JB2.Bowtie.Enum.RepositoryType, object> _repos;
        //private BowtieDataContext _dataContext;
        //private JB2.Bowtie.Enum.RepoDataSource _defaultSource;


        public UnitOfWork()
        {


        }

        public JB2.Bowtie.IGraphRepository GraphRepository
        {
            get
            {
                return (JB2.Bowtie.IGraphRepository)GetRepository(RepositoryType.Graph);
            }
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

        //public JB2.Common.IPlayerRepo PlayerRepository
        //{
        //    get
        //    {
        //        return (JB2.Common.IPlayerRepo)GetRepository(Enum.RepositoryType.Player);
        //    }
        //}

        public object GetRepository(JB2.Bowtie.Enum.RepositoryType repository)
        {
            return GetRepository(repository, Enum.RepoDataSource.Standard);
        }



        private object GetRepository(JB2.Bowtie.Enum.RepositoryType repository, JB2.Bowtie.Enum.RepoDataSource datasource)
        {
            switch (datasource)
            {
                case RepoDataSource.Standard:
                    //if (!_repos.ContainsKey(repository))
                    //{
                    //    switch (repository)
                    //    {
                    //        case RepositoryType.Application:
                    //            _repos.Add(repository, new JB2.Bowtie.Data.Linq.ApplicationRepository(_dataContext));
                    //            break;
                    //        case RepositoryType.Achievement:
                    //            _repos.Add(repository, new JB2.Bowtie.Data.Linq.AchievementRepository(_dataContext));
                    //            break;
                    //        case RepositoryType.GameCommand:
                    //            _repos.Add(repository, new JB2.Bowtie.Data.Linq.GameCommandRepository(_dataContext));
                    //            break;
                    //        case RepositoryType.Leaderboard:
                    //            _repos.Add(repository, new JB2.Bowtie.Data.Linq.LeaderboardRepository(_dataContext));
                    //            break;
                    //    }
                    //}
                    return _repos[repository];
            }
            return null;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                //if (disposing)
                //{
                //    _repos.Di
                //}
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
