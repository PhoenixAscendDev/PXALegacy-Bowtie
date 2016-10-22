using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Economy;

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

        protected ITreasuryNote retrieveJBeanTokens(JB2.Economy.Enum.JBeanTokenType type,int quantity)
        {
            JB2.Economy.ITreasury treasury = JB2.Settings.Jbean.Factory.Treasury;

            JBeanBag beanbag = new JBeanBag(type, quantity);
            var requestor = JB2.Settings.Bowtie.CurrentApplication;

            TreasuryRequest request = new TreasuryRequest() { Amount = beanbag, Requestor = requestor, RequestDate = DateTime.Now, VerificationKey = "verifyit" };
            ITreasuryNote treasuryNote = treasury.IssueNote(request);
            return treasuryNote;        
        }

        public virtual bool Remove(Tobject entity)
        {
            _repo.Delete(entity);
            return true;
        }

        public virtual List<Tobject> Retrieve(string request)
        {
            throw new NotImplementedException();
        }

        public virtual List<Tobject> Retrieve(bool isActive)
        {
            throw new NotImplementedException();
        }

        public virtual List<Tobject> Retrieve()
        {
            try
            {
                return _repo.GetAll().ToList();
            }
            catch(Exception ex)
            {
                ex.BowtieLog();
                return new List<Tobject>();   
            }
            
        }

        public virtual Tobject RetrieveById(string id)
        {
            return _repo.GetById(id);
        }

        public virtual Tobject RetrieveByName(string name)
        {
            throw new NotImplementedException();
        }

        public virtual bool Save(Tobject entity)
        {
           _repo.Insert(entity);
           return true;
        }
    }
}
