using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class GenericService<Tobject,Trepo> : JB2.Common.IObjectService<Tobject, bool, string> 
          where Tobject : IBowtieObject
          where Trepo : JB2.Common.IRepository<Tobject, string>
    {
        protected Trepo _repo;
        protected IUnitOfWork _uofw;






       
        public GenericService()
        {
            _repo = default(Trepo);
        }

        public GenericService(Trepo repository)
        {
            _repo = repository;
        }


        public bool Remove(Tobject entity)
        {
            _repo.Delete(entity);
            return true;
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
            return _repo.GetAll().ToList();
        }

        public Tobject RetrieveById(string id)
        {
            return _repo.GetById(id);
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
