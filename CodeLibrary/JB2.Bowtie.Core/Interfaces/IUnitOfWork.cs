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
        IGameCommandRepository GameCommandRepository { get; }
        ILeaderboardRepository LeaderboardRepository { get; }
        IGraphRepository GraphRepository { get; }

        IDewdropRepository DewdropRepository { get;}

        JB2.Economy.IJBeanRepository JbeanRepository { get;}
        
        //IPlayerRepo PlayerRepository { get; }
        object GetRepository(Enum.RepositoryType respository);

      
    }
}
