using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data
{
    public class ApplicationRepo : IApplicationRepository
    {
        public void Delete(IApplication entity)
        {
            throw new NotImplementedException();
        }

        public IApplication[] GetAll()
        {
            throw new NotImplementedException();
        }

        public IApplication[] GetAll(int? maxRecordCount)
        {
            throw new NotImplementedException();
        }

        public IApplication[] GetAPIAllowedApps()
        {
            throw new NotImplementedException();
        }

        public IApplication GetApplicationByAPIKey(string publicKey)
        {
            throw new NotImplementedException();
        }

        public IApplication[] GetApplicationsByClientID(string clientID)
        {
            throw new NotImplementedException();
        }

        public IApplication GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(IApplication entity)
        {
            throw new NotImplementedException();
        }

        public IApplication[] SearchFor()
        {
            throw new NotImplementedException();
        }

        public IApplication[] SearchFor(string filter)
        {
            throw new NotImplementedException();
        }
    }
}
