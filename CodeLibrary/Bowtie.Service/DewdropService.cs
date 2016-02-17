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

        #region Retrieve 
        public IEnumerable<Dewdrop> Retrieve(string request, bool returnNullOrEmptyOnError = false)
        {
            return _repo.GetAll();
        }

        public IEnumerable<Dewdrop> Retrieve(bool isActive, bool returnNullOrEmptyOnError = false)
        {
            return _repo.GetAll();
        }

        public IEnumerable<Dewdrop> Retrieve( bool returnNullOrEmptyOnError = false)
        {
            return _repo.GetAll();
        }

        public IEnumerable<Dewdrop> RetrieveByApplication(Application app, bool returnNullOrEmptyOnError = false)
        {
            return _repo.GetByApplicationID(app.GetID());
        }

        public Dewdrop RetrieveById(string id, bool returnNullOrEmptyOnError = false)
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
                if (!returnNullOrEmptyOnError)
                    throw new ObjectNotFoundInRepositoryException(nullEx, entityId: id, respository: _repo);
                else
                    return Dewdrop.Empty();
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
    }
}
