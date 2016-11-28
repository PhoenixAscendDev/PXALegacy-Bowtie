using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common.Log;
using JB2.Economy;

namespace JB2.Bowtie.Data.NoDB
{
    public class UnitofWork : JB2.Bowtie.IUnitOfWork
    {
        public IApplicationRepository ApplicationRepository
        {
            get 
            {
                return (IApplicationRepository)GetRepository(Enum.RepositoryType.Application);

            }
        }

        public IAchievementRepository AchievementRepository
        {
            get
            {
                return (IAchievementRepository)GetRepository(Enum.RepositoryType.Achievement);
            
            }
        }

        public IGameCommandRepository GameCommandRepository
        {
            get
            {
                return (IGameCommandRepository)GetRepository(Enum.RepositoryType.GameCommand);

            }
        }

        public ILeaderboardRepository LeaderboardRepository
        {
            get
            {
                return (ILeaderboardRepository)GetRepository(Enum.RepositoryType.Leaderboard);

            }
        }

        public JB2.Common.IPlayerRepo PlayerRepository
        {
            get
            {
                return (JB2.Common.IPlayerRepo)GetRepository(Enum.RepositoryType.Player);
            }
        }

        public Bowtie.IGraphRepository GraphRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IDewdropRepository DewdropRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IJBeanRepository JbeanRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IWalletRepository WalletRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        IBowtiePlayerRespository IUnitOfWork.PlayerRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IModuleRepository ModuleRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IAuthorizeRepository AuthorizeRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ILogRepo LogRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IDewdropQueueRepo DewdropQueue
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IMaintenanceQueueRepo MaintenanceQueue
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IAuthProviderRepository AuthProviderRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IAchievementQueueRepo AchievementQueue
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IPointSystemRepository PointSystemRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public object GetRepository(Enum.RepositoryType respository)
        {
            switch(respository)
            {
                case Enum.RepositoryType.Achievement:
                    return new AchievementRepository();
                case Enum.RepositoryType.Application:
                    return new ApplicationRepository();
                case Enum.RepositoryType.GameCommand:
                    return new GameCommandRepository();
                case Enum.RepositoryType.Leaderboard:
                    return new LeaderboardRepository();
            }

            return null;
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
