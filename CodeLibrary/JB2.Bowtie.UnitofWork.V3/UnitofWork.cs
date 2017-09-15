using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class UnitofWork : IUnitOfWork
    {
        public IApplicationRepository ApplicationRepository => throw new NotImplementedException();

        public IDewdropRepository DewdropRepository => throw new NotImplementedException();

        public IAuthorizeRepository AuthorizeRepository => throw new NotImplementedException();

        public IDewdropQueueRepo DewdropQueue => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
