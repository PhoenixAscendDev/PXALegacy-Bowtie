using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Linq;

namespace JB2.Bowtie.Data
{
    public class LinqRepository<Tobject> : BaseRespository, JB2.Common.IRepository<Tobject, string> where Tobject : IBowtieObject
    {
        protected BowtieDataContext _dbcontext;
        private const string DB_PREFIX = "jb2bt_";

        public LinqRepository(BowtieDataContext dbc)
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
