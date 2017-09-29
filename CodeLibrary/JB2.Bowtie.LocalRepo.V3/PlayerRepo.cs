using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.Local
{
    public class PlayerRepo : JB2.Common.Singleton<PlayerRepo>, IPlayerRepository
    {

        #region Fields

        protected Dictionary<string, IPlayer> _items;
        protected IUnitOfWork _uofw;

        #endregion Fields


        #region Constructors

        public PlayerRepo() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }

        public PlayerRepo(IUnitOfWork unitofWork)
        {
            _uofw = unitofWork;

            _items = new Dictionary<string, IPlayer>();

            //if (_items == null)
            //{
            //    _apps = new Dictionary<string, IApplication>();
            //    var json = "[{\"Website\":\"http://linkfence.io\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{\"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"a600dcba\",\"Name\":\"Link Fence\"},{\"Website\":\"http://fivetwo.io\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{\"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"a4cc70f2\",\"Name\":\"FiveTwo\"},{\"Website\":\"http://bluffstreet.fun/thisthat\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{\"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"9F0199E\",\"Name\":\"ThisThat\"}]";
            //    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LocalApplication>>(json);

            //    foreach (var a in list)
            //    {
            //        _apps.Add(a.ID, a);
            //    }

            //}

        }

        #endregion Constructors

        public void Delete(IPlayer entity)
        {
            if (_items.ContainsKey(entity.ID))
                _items.Remove(entity.ID);
        }

        public IPlayer[] GetAll()
        {
            throw new NotImplementedException();
        }

        public IPlayer[] GetAll(int? maxRecordCount)
        {
            if (maxRecordCount == null || maxRecordCount <= 0)
                return getall().ToArray();
            else
                return getall().Take((int)maxRecordCount).ToArray();

            
        }

        public IPlayer GetById(string id)
        {
            return getall().Where(x => x.ID == id).FirstOrDefault();
        }

        public void Insert(IPlayer entity)
        {
            if (_items.ContainsKey(entity.ID))
                _items[entity.ID] = entity;
            else
                _items.Add(entity.ID, entity);
        }

        public IPlayer[] SearchFor()
        {
            throw new NotImplementedException();
        }

        public IPlayer[] SearchFor(string filter)
        {
            throw new NotImplementedException();
        }


        #region Helpers
        private IEnumerable<IPlayer> getall()
        {
            return _items.Values;
        }

        #endregion Helpers




    }
}
