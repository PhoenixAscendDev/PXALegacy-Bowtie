using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.Local
{
    public class LeaderboardRepo : JB2.Common.Singleton<LeaderboardRepo>, ILeaderboardRepository
    {
        #region Fields

        protected Dictionary<string, ILeaderboard> _items;      
        protected IUnitOfWork _uofw;
        #endregion Fields


        #region Constructors

        public LeaderboardRepo() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }

        public LeaderboardRepo(IUnitOfWork unitofWork)
        {
            _uofw = unitofWork;
            _items = new Dictionary<string, ILeaderboard>();
        }

        #endregion Constructors

        public void Delete(ILeaderboard entity)
        {
            if (_items.ContainsKey(entity.ID))
                _items.Remove(entity.ID);

        }

        public ILeaderboard[] GetAll()
        {
            return getall().ToArray();
        }

        public ILeaderboard[] GetAll(int? maxRecordCount)
        {
            if (maxRecordCount != null || maxRecordCount == 0)
            {
                return getall().ToArray();
            }
            else
                return getall().Take((int)maxRecordCount).ToArray();
        }

        public IEnumerable<ILeaderboard> GetByApplicationID(string applicationID)
        {
            return getall().Where(x => x.ApplicationID == applicationID);
        }

        public ILeaderboard GetById(string id)
        {
            return getall().Where(x => x.ID == id).FirstOrDefault();
        }

        public void Insert(ILeaderboard entity)
        {
            if (_items.ContainsKey(entity.ID))
                _items[entity.ID] = entity;
            else
                _items.Add(entity.ID, entity);


        }

        public ILeaderboard[] SearchFor()
        {
            throw new NotImplementedException();
        }

        public ILeaderboard[] SearchFor(string filter)
        {
            throw new NotImplementedException();
        }


        #region Helpers

        private IEnumerable<ILeaderboard> getall()
        {
            return _items.Values;
        }
        #endregion Helpers



    }
}
