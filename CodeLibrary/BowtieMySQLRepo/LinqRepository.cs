using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Linq;

namespace JB2.Bowtie.Data
{
    public class LinqRepository<Tobject> : JB2.Common.IRepository<Tobject, string> where Tobject : IBowtieObject
    {
        protected DataContext _dbcontext;

        public LinqRepository(DataContext dbc)
        {
            this._dbcontext = dbc;
        }

        public LinqRepository()
        {
            this._dbcontext = new BowtieDataContext();
           
        }



        public void Delete(Tobject entity)
        {
            throw new NotImplementedException();
        }

        public Tobject[] GetAll()
        {
            throw new NotImplementedException();
        }

        public Tobject GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Tobject entity)
        {
            throw new NotImplementedException();
        }

        public Tobject[] SearchFor()
        {
            throw new NotImplementedException();
        }
    }
}
