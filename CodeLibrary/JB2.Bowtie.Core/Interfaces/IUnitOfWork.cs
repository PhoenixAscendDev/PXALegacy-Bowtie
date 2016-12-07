using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie
{
    public interface IUnitOfWork: IDisposable
    {
        IApplicationRepository ApplicationRepository { get; }
        IAchievementRepository AchievementRepository { get; }

        IAuthProviderRepository AuthProviderRepository { get; }

        IGameCommandRepository GameCommandRepository { get; }
        ILeaderboardRepository LeaderboardRepository { get; }
        IGraphRepository GraphRepository { get; }

        IDewdropRepository DewdropRepository { get;}

        IModuleRepository ModuleRepository { get; }

        JB2.Economy.IJBeanRepository JbeanRepository { get;}

        IWalletRepository WalletRepository { get; }

        IBowtiePlayerRespository PlayerRepository { get; }

        IAuthorizeRepository AuthorizeRepository { get; }

        IPointSystemRepository PointSystemRepository { get; }

        IMissionRespository MissionRepository { get; }

        IExternalClassRepository ExternalClassRepository { get; }
        
        //IPlayerRepo PlayerRepository { get; }
        object GetRepository(Enum.RepositoryType respository);

        JB2.Common.Log.ILogRepo LogRepository { get; }




        #region Queues

        IDewdropQueueRepo DewdropQueue { get; }
        IMaintenanceQueueRepo MaintenanceQueue { get; }

        IAchievementQueueRepo AchievementQueue { get; }

        IPointQueueRepository PointQueue { get; }

        #endregion Queues

    }
}
