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

        object GetRepository(Enum.RepositoryType respository);

        object GetRepository(Enum.RepositoryType respository, Enum.RepoDataSource datasource);

        IApplicationRepository GetApplicationRepository();

        IApplicationRepository GetApplicationRepository(Enum.RepoDataSource datasource);

    }
}
