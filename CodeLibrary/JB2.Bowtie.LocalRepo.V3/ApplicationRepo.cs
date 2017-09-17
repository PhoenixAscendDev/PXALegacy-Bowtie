using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.Local
{
    public class ApplicationRepo : JB2.Common.Singleton<ApplicationRepo>, IApplicationRepository
    {

        #region Fields

        protected Dictionary<string, IApplication> _apps;
        protected IUnitOfWork _uofw;

        #endregion Fields


        #region Constructors

        public ApplicationRepo() : this(JB2.Settings.Bowtie.UnitOfWork)
        {
            
        }

        public ApplicationRepo(IUnitOfWork unitofWork)
        {
            _uofw = unitofWork;

            if(_apps == null)
            {
                _apps = new Dictionary<string, IApplication>();
                var json = "[{\"Website\":\"http://linkfence.io\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{\"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"a600dcba\",\"Name\":\"Link Fence\"},{\"Website\":\"http://fivetwo.io\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{\"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"a4cc70f2\",\"Name\":\"FiveTwo\"},{\"Website\":\"http://bluffstreet.fun/thisthat\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{\"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"9F0199E\",\"Name\":\"ThisThat\"}]";
                var list =  Newtonsoft.Json.JsonConvert.DeserializeObject<List<LocalApplication>>(json);

                foreach(var a in list)
                {
                    _apps.Add(a.ID, a);
                }

            }

        }



        #endregion Constructors


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
            _apps.Add(entity.ID, entity);
          
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

            return _apps.Values.ToList();

            //var json = "[{ \"Website\":\"http://linkfence.io\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{ \"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"a600dcba\",\"Name\":\"Link Fence\"},{ \"Website\":\"http://fivetwo.io\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{ \"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"a4cc70f2\",\"Name\":\"FiveTwo\"},null]";

            //return Newtonsoft.Json.JsonConvert.DeserializeObject<List<LocalApplication>>(json);


        }




        #endregion Helpers
    }
}
