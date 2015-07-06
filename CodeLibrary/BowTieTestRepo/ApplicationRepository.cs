using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.NoDBData
{
    public class ApplicationRepository : JB2.Bowtie.IApplicationRepository
    {
        public void Delete(Application entity)
        {
            throw new NotImplementedException();
        }

        public Application[] GetAll()
        {
            throw new NotImplementedException();
        }

        public Application GetById(int id)
        {
            return new Application("4d53bce03ec34c0a911182d4c228ee6c", "A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc9");
        }

        public void Insert(Application entity)
        {
            throw new NotImplementedException();
        }

        public Application[] SearchFor()
        {
            throw new NotImplementedException();
        }

        public Application[] GetAPIAllowedApps()
        {
            List<Application> result = new List<Application>();

            result.Add(this.GetById(1));

            return result.ToArray();
        }
    }
}
