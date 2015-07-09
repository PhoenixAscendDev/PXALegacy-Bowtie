using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;

namespace JB2.Bowtie.Data
{
    public class ApplicationRepository :  LinqRepository<IApplication>, IApplicationRepository
    {


        public ApplicationRepository() : base()
        {

        }

        public ApplicationRepository(BowtieDataContext dbc)
            : base(dbc)
        {

        }


        public IApplication[] GetAPIAllowedApps()
        {
            throw new NotImplementedException();
        }

        public void Delete(IApplication entity)
        {
            throw new NotImplementedException();
        }

        public IApplication[] GetAll()
        {
            var query = from i in _dbcontext.jb2bt_Application_Get(null, null)
                        select getApplication(i);
            return query.ToArray();
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


        internal static IApplication getApplication<T>(T r) where T : class
        {
            //int id = -1;
            //int.TryParse(getString(r, "Id"), out id);
            Application result = new Application( getString(r,"PublicKey"), getString(r,"Secret"))
            {
                ID = getString(r, "ID"),
                Name = getString(r, "Name")
            };




            return result;
        }





    }
}
