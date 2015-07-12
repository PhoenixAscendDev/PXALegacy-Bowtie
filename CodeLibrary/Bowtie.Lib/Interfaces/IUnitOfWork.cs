using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IUnitOfWork: IDisposable
    {
        IApplicationRepository ApplicationRepository { get; }
        IAchievementRepository AchievementRepository { get; }

        object GetRepository(Enum.RepositoryType respository);

      
    }
}
