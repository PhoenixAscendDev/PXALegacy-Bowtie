using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class GenericService<Tobject> : JB2.Common.IObjectService<Tobject, bool, string> where Tobject : IBowtieObject
    {
        private JB2.Common.IRepository<Tobject, string> _repo;


        public GenericService()
        {
            _repo = null;
        }

        public GenericService(JB2.Common.IRepository<Tobject, string> repository)
        {
            _repo = repository;
        }


        public bool Remove(Tobject entity)
        {
            throw new NotImplementedException();
        }

        public List<Tobject> Retrieve(string request)
        {
            throw new NotImplementedException();
        }

        public List<Tobject> Retrieve(bool isActive)
        {
            throw new NotImplementedException();
        }

        public List<Tobject> Retrieve()
        {
            throw new NotImplementedException();
        }

        public Tobject RetrieveById(string id)
        {
            throw new NotImplementedException();
        }

        public Tobject RetrieveByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool Save(Tobject entity)
        {
            throw new NotImplementedException();
        }
    }
}
