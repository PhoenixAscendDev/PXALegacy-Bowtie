using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;

namespace JB2.Bowtie.Data.Local
{
    public class LogRepo : JB2.Common.Singleton<LogRepo>, ILogRepository
    {
        #region Fields

        protected List<ILogEntry> _items;
        protected IUnitOfWork _uofw;
        protected Dictionary<string, List<IPlayerActivity>> _playeractivities;


        #endregion Fields


        #region Constructors

        public LogRepo() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }

        public LogRepo(IUnitOfWork unitofWork)
        {
            _uofw = unitofWork;


            _items = new List<ILogEntry>();

            _playeractivities = new Dictionary<string, List<IPlayerActivity>>();


        }


        #endregion Constructors


        public void Delete(ILogEntry entity)
        {
            _items.Remove(entity);
        }

        public ILogEntry[] GetAll()
        {
            return GetAll(null);
        }

        public ILogEntry[] GetAll(int? maxRecordCount)
        {
            if (maxRecordCount == null || maxRecordCount < 0)
                return getall().ToArray();
            else
                return getall().Take((int)maxRecordCount).ToArray();
        }

        public ILogEntry GetById(string id)
        {
            return getall().Where(x => x.ID == id).FirstOrDefault();
        }

        public void Insert(ILogEntry entity)
        {
            _items.Add(entity);
        }

        public ILogEntry[] SearchFor()
        {
            throw new NotImplementedException();
        }

        public ILogEntry[] SearchFor(string filter)
        {
            throw new NotImplementedException();
        }

        public void Insert(IPlayerActivity pa)
        {
            string pakey = pa.GetApplicationID() + "<*>" + pa.GetPlayerID();


            if(!_playeractivities.ContainsKey(pakey))
                _playeractivities.Add(pakey, new List<IPlayerActivity>());

            _playeractivities[pakey].Add(pa);
        }

        public IEnumerable<IPlayerActivity> GetPlayerActivityByApplicationID(string applicationid, string playerid)
        {
            string pakey = applicationid + "<*>" + playerid;

            if (_playeractivities.ContainsKey(pakey))
                return _playeractivities[pakey];

            else
                return new IPlayerActivity[0];
        }


        #region Helpers

        public IEnumerable<ILogEntry> getall()
        {
            return _items;
        }

        

        #endregion Helpers
    }
}
