using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.NoDB
{
    public class ApplicationRepository : JB2.Bowtie.IApplicationRepository
    {
        private IApplication[] _apps;

        public ApplicationRepository() : this(10)
        {

        }

        public ApplicationRepository(int recordCount)
        {
            List<IApplication> result = new List<IApplication>();

            
            result.Add(new Application("4d53bce03ec34c0a911182d4c228ee6a", "A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabca"));
            result.Add(new Application("F79676FF52F9018B4FC1BEE5E0.battlesim", "HuCSuRTYGyJhMJbTjsCU4O6YemrZLOrwoGJLMe5CnSE="));
            result.Add(new Application("4d53bce03ec34c0a911182d4c228ee6d", "A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc="));

            _apps = result.ToArray();
        }




        public void Delete(IApplication entity)
        {
            throw new NotImplementedException();
        }

        public IApplication[] GetAll()
        {
            return _apps;
        }

        public IApplication GetById(string id)
        {
            return _apps.FirstOrDefault();
        }

        public void Insert(IApplication entity)
        {
            throw new NotImplementedException();
        }

        public IApplication[] SearchFor()
        {
            throw new NotImplementedException();
        }

        public IApplication[] GetAPIAllowedApps()
        {
            return _apps;
        }




        public IApplication[] GetApplicationsByClientID()
        {
            throw new NotImplementedException();
        }

        public IApplication[] SearchFor(string filter)
        {
            throw new NotImplementedException();
        }

        public IApplication[] GetApplicationsByClientID(string clientID)
        {
            throw new NotImplementedException();
        }

        public IApplication GetApplicationByAPIKey(string publicKey)
        {
            throw new NotImplementedException();
        }
    }
}
