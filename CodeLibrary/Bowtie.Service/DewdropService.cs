using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using JB2.Common.Data;

namespace JB2.Bowtie.Service
{
    public class DewdropService
    {
        #region Fields
        protected IDewdropRepository _repo;
        protected IUnitOfWork _uofw;
        #endregion Fields

        #region Constructors
        public DewdropService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }
        public DewdropService(IUnitOfWork uofw)
        {
            _uofw = uofw;
            _repo = (IDewdropRepository)uofw.GetRepository(Enum.RepositoryType.Dewdrop);
        }

        public DewdropService(IDewdropRepository repo)
        {
            _repo = repo;
            _uofw = JB2.Settings.Bowtie.UnitOfWork;
        }
        #endregion Constructors

        public JB2.Common.ServiceResult Validate(IBowtiePlayer player, IDewdrop dewdrop)
        {
            JB2.Common.ServiceResult result = false;

            if (dewdrop.GetjBeanCost() <= 0)
                return true;

            // make sure the person has enough jbeans in wallet
            var wallet = player.GetWallet();

            if (wallet != null && wallet.JBeanTotal >= dewdrop.GetjBeanCost())
                return true;
            else
                return false;
        }

        #region Retrieve 
        public IEnumerable<Dewdrop> Retrieve(string request, OnErrorReturnType errorReturntype = OnErrorReturnType.ThrowException)
        {
            return _repo.GetAll();
        }

        public IEnumerable<Dewdrop> Retrieve(bool isActive, OnErrorReturnType errorReturntype = OnErrorReturnType.ThrowException)
        {
            return _repo.GetAll();
        }

        public IEnumerable<Dewdrop> Retrieve(OnErrorReturnType errorReturntype = OnErrorReturnType.ThrowException)
        {
            return _repo.GetAll();
        }

        public IEnumerable<Dewdrop> RetrieveByApplication(Application app, OnErrorReturnType errorReturntype = OnErrorReturnType.ThrowException)
        {
            return _repo.GetByApplicationID(app.GetID());
        }

        public Dewdrop RetrieveById(string id, OnErrorReturnType errorReturntype = OnErrorReturnType.ThrowException)
        {
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match("^dew_?");
            if(!match.Success)
            {
                id = "dew_" + id;
            }
            try
            {
                return _repo.GetById(id);
            }
            catch(NullReferenceException nullEx)
            {
                switch(errorReturntype)
                {
                    case OnErrorReturnType.ThrowException:
                        throw new ObjectNotFoundInRepositoryException(nullEx, entityId: id, respository: _repo);
                    case OnErrorReturnType.Null:
                        return null;
                    case OnErrorReturnType.EmptyObject:
                    default:
                        return Dewdrop.Empty();
                }                                
            }
        }

        public Dewdrop RetrieveByName(string name)
        {
            throw new NotImplementedException();
        }

        #endregion Retrieve

        public bool Save(Dewdrop entity)
        {
            _repo.Insert(entity);
            return true;
        }

        #region PlayerDewdrop

        public IEnumerable<IPlayerDewdrop> RetrievePlayerDewdropByPlayerID(string id)
        {
            return _repo.GetPlayerDewsByPlayerID(id);
        }

        
        public bool Save(IPlayerDewdrop playerdewdrop)
        {
            _repo.InsertPlayerDew(playerdewdrop);
            return true;
        }

        #endregion PlayerDewdrop
    }
}
