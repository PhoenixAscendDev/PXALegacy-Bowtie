using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Test
{
    public class GenericRepository<Tobject> : JB2.Common.IRepository<Tobject, string> where Tobject : IBowtieObject
    {


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
