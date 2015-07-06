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


        public Application[] GetAPIAllowedApps()
        {
            throw new NotImplementedException();
        }

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
            throw new NotImplementedException();
        }

        public void Insert(Application entity)
        {
            throw new NotImplementedException();
        }

        public Application[] SearchFor()
        {
            throw new NotImplementedException();
        }
    }
}
