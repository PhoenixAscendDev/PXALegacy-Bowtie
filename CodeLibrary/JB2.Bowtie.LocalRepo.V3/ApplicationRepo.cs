using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.Local
{
    public class ApplicationRepo : JB2.Common.Singleton<ApplicationRepo>, IApplicationRepository
    {
        public void Delete(IApplication entity)
        {
            throw new NotImplementedException();
        }

        public IApplication[] GetAll()
        {
            return GetAll(0);
        }

        public IApplication[] GetAll(int? maxRecordCount)
        {
            return getall().ToArray();
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
            return getall().Where(x => x.ID == id).FirstOrDefault();
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


        #region Helpers

        public IEnumerable<IApplication> getall()
        {
            var json = "[{\"Website\":\"http://linkfence.io\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{\"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"a600dcba\",\"Name\":\"Link Fence\"},{\"Website\":\"http://fivetwo.io\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{\"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"a4cc70f2\",\"Name\":\"FiveTwo\"}]";

            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<LocalApplication>>(json);


        }




        #endregion Helpers
    }
}
