using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.NoDBData
{
    public class ApplicationRepository : JB2.Bowtie.IApplicationRepository
    {
        public void Delete(IApplication entity)
        {
            throw new NotImplementedException();
        }

        public IApplication[] GetAll()
        {
            throw new NotImplementedException();
        }

        public IApplication GetById(string id)
        {
            return new Application("4d53bce03ec34c0a911182d4c228ee6c", "A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc=");
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
            List<IApplication> result = new List<IApplication>();

            result.Add(this.GetById("1"));
            result.Add(new Application("4d53bce03ec34c0a911182d4c228ee6a", "A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabca"));
            result.Add(new Application("F79676FF52F9018B4FC1BEE5E0.battlesim", "HuCSuRTYGyJhMJbTjsCU4O6YemrZLOrwoGJLMe5CnSE="));
            result.Add(new Application("4d53bce03ec34c0a911182d4c228ee6d", "A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc="));
            
            

            return result.ToArray();
        }

        
    }
}
