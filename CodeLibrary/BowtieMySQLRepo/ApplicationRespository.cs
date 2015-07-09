using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;

namespace JB2.Bowtie.Data
{
    public class ApplicationRespository :  LinqRepository, IApplicationRepository
    {


        public ApplicationRespository() : base()
        {

        }

        public ApplicationRespository(System.Data.Linq.DataContext dbc) : base(dbc)
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




    }
}
