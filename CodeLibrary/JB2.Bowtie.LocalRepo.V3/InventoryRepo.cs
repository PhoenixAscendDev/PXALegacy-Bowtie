using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.Local
{
    public class InventoryRepo : JB2.Common.Singleton<InventoryRepo>, IInventoryRepository
    {
        #region Fields

        protected Dictionary<string, IInventoryItem> _items;
        protected IUnitOfWork _uofw;
        #endregion Fields


        #region Constructors

        public InventoryRepo() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }

        public InventoryRepo(IUnitOfWork unitofWork)
        {
            _uofw = unitofWork;
            _items = new Dictionary<string, IInventoryItem>();
        }

        #endregion Constructors

        public void Delete(IInventoryItem entity)
        {
            if (_items.ContainsKey(entity.ID))
                _items.Remove(entity.ID);
        }

        public IInventoryItem[] GetAll()
        {
            return GetAll(null);
        }

        public IInventoryItem[] GetAll(int? maxRecordCount)
        {
            if (maxRecordCount == null || maxRecordCount == 0)
                return this.getall().ToArray(); // throw new NotImplementedException();
            else
                return this.getall().Take((int)maxRecordCount).ToArray();
        }

        public IEnumerable<IInventoryItem> GetByApplicationID(string applicationID)
        {
            return getall().Where(x => x.ApplicationID == applicationID);
        }

        public IInventoryItem GetById(string id)
        {
            return getall().Where(x => x.ID == id).FirstOrDefault();
        }

        public void Insert(IInventoryItem entity)
        {
            if (_items.ContainsKey(entity.ID))
                _items[entity.ID] = entity;
        }

        public IInventoryItem[] SearchFor()
        {
            throw new NotImplementedException();
        }

        public IInventoryItem[] SearchFor(string filter)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<IInventoryItem> getall()
        {
            return _items.Values;
        }

    }
}
