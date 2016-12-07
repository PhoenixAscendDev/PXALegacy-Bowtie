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
            _repos = new Dictionary<RepositoryType, object>();
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

        public JB2.Bowtie.IDewdropRepository DewdropRepository
        {
            get
            {
                return (IDewdropRepository)GetRepository(RepositoryType.Dewdrop);
            }
        }

        public JB2.Economy.IJBeanRepository JbeanRepository
        {
            get
            {
                return (JB2.Economy.IJBeanRepository)GetRepository(RepositoryType.Jbean);
            }
        }

        public JB2.Bowtie.IBowtiePlayerRespository PlayerRepository
        {
            get
            {
                return (JB2.Bowtie.IBowtiePlayerRespository)GetRepository(RepositoryType.Player);
            }
        }

        public JB2.Bowtie.IWalletRepository WalletRepository
        {
            get
            {
                return (JB2.Bowtie.IWalletRepository)GetRepository(RepositoryType.Wallet);
            }
        }

        public JB2.Bowtie.IModuleRepository ModuleRepository
        {
            get
            {
                return (JB2.Bowtie.IModuleRepository)GetRepository(RepositoryType.Module);
            }
        }

        public JB2.Bowtie.IAuthorizeRepository AuthorizeRepository
        {
            get
            {
                return (JB2.Bowtie.IAuthorizeRepository)GetRepository(RepositoryType.Authorize);
            }
        }

        public JB2.Common.Log.ILogRepo LogRepository
        {
            get
            {
                return (JB2.Common.Log.ILogRepo)GetRepository(RepositoryType.Log);
            }
        }

        public IAuthProviderRepository AuthProviderRepository
        {
            get
            {
                return (JB2.Bowtie.IAuthProviderRepository)GetRepository(RepositoryType.AuthProvider);
            }

        }

        public IPointSystemRepository PointSystemRepository
        {
            get
            {
                return (JB2.Bowtie.IPointSystemRepository)GetRepository(RepositoryType.PointSystem);
            }
        }

        public IMissionRespository MissionRepository
        {
            get
            {
                return (JB2.Bowtie.IMissionRespository)GetRepository(RepositoryType.Mission);
            }
        }

        public IDewdropQueueRepo DewdropQueue
        {
            get
            {
                return new JB2.Bowtie.Queue.Azure.DewdropQueue(AzureStorage.DewdropQueue);
            }
        }

        public IAchievementQueueRepo AchievementQueue 
        {
            get
            {
                return new JB2.Bowtie.Queue.Azure.AchievementQueue(AzureStorage.AchievementQueue);
            }
        }




        public IMaintenanceQueueRepo MaintenanceQueue
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IPointQueueRepository PointQueue
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IExternalClassRepository ExternalClassRepository
        {
            get
            {
                return (JB2.Bowtie.IExternalClassRepository)GetRepository(RepositoryType.ClassConfig);
            }
        }


        public object GetRepository(JB2.Bowtie.Enum.RepositoryType repository)
        {
            return GetRepository(repository, Enum.RepoDataSource.Standard);
        }



        private object GetRepository(JB2.Bowtie.Enum.RepositoryType repository, JB2.Bowtie.Enum.RepoDataSource datasource)
        {
            switch (datasource)
            {
                case RepoDataSource.Standard:

                    if (!_repos.ContainsKey(repository))
                    {
                        switch (repository)
                        {
                            case RepositoryType.Graph:
                                _repos.Add(repository, new JB2.Bowtie.Data.Azure.GraphRepository());
                                break;
                            case RepositoryType.Dewdrop:
                                _repos.Add(repository, new JB2.Bowtie.Data.Azure.DewdropRepository());
                                break;
                            case RepositoryType.Application:
                                _repos.Add(repository, new JB2.Bowtie.Data.Azure.ApplicationRepository());
                                break;
                            case RepositoryType.Jbean:
                                var storage = JB2.Infrastructure.Storage.EconomyAccount;
                                _repos.Add(repository, new JB2.Economy.Data.jBeanRespostory(storage));
                                break;
                            case RepositoryType.Player:
                                _repos.Add(repository, new JB2.Bowtie.Data.Azure.PlayerRepository());
                                break;
                            case RepositoryType.Wallet:
                                _repos.Add(repository, new JB2.Bowtie.Data.Azure.WalletRepository());
                                break;
                            case RepositoryType.Achievement:
                                _repos.Add(repository, new JB2.Bowtie.Data.Azure.AchievementRepository());
                                break;
                            case RepositoryType.GameCommand:
                                _repos.Add(repository, new JB2.Bowtie.Data.Azure.GameCommandRepository());
                                break;
                            case RepositoryType.Module:
                                _repos.Add(repository, new JB2.Bowtie.Data.Azure.ModuleRepository());
                                break;
                            case RepositoryType.Authorize:
                                _repos.Add(repository, new JB2.Bowtie.Data.Azure.AuthorizeRepository());
                                break;
                            case RepositoryType.Log:
                                _repos.Add(repository, new JB2.Common.Log.AzureRepo(AzureStorage.LogTable));
                                break;
                            case RepositoryType.AuthProvider:
                                _repos.Add(repository, new JB2.Bowtie.Data.Azure.AuthProviderRepository());
                                break;
                            case RepositoryType.PointSystem:
                                _repos.Add(repository, new JB2.Bowtie.Data.Azure.PointSysteRepository());
                                break;
                            case RepositoryType.Mission:
                                _repos.Add(repository, new JB2.Bowtie.Data.Azure.MissionRespository());
                                break;
                            case RepositoryType.ClassConfig:
                                _repos.Add(repository, new JB2.Bowtie.Data.Azure.ExternalClassRepository());
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
