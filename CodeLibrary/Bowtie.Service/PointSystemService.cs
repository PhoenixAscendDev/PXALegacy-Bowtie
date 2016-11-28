using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie.Service
{
    public class PointSystemService : JB2.Common.IObjectService<IPointSystem,bool, string>
    {
        protected IPointSystemRepository _repo;
        protected IUnitOfWork _uofw;


        #region Constructors
        public PointSystemService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }

        public PointSystemService(IUnitOfWork unitOfWork) : this(unitOfWork.PointSystemRepository)
        {
            _uofw = unitOfWork;
        }

        public PointSystemService(IPointSystemRepository repo)
        {
            _repo = repo;

        }

        #endregion Constructors


        public List<PointSystemConfig> RetrieveConfigs()
        {
            return _repo.GetAllConfigs().ToList();
        }

        public PointSystemConfig RetrieveConfigById(string id)
        {
            return _repo.GetAllConfigs().ToList().Find(x => x.ID == id);
        }


        public virtual bool Remove(IPointSystem entity)
        {
            _repo.Delete(entity);
            return true;
        }

        public virtual List<IPointSystem> Retrieve(string request)
        {
            throw new NotImplementedException();
        }

        public virtual List<IPointSystem> Retrieve(bool isActive)
        {
            throw new NotImplementedException();
        }

        public virtual List<IPointSystem> Retrieve()
        {
            try
            {
                return _repo.GetAll().ToList();
            }
            catch (Exception ex)
            {
                ex.BowtieLog();
                return new List<IPointSystem>();
            }

        }

        public virtual IPointSystem RetrieveById(string id)
        {
            return _repo.GetById(id);
        }

        public virtual IPointSystem RetrieveByName(string name)
        {
            throw new NotImplementedException();
        }

        public virtual bool Save(IPointSystem entity)
        {
            _repo.Insert(entity);
            return true;
        }


    }
}
