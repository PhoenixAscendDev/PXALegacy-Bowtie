using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Linq;

namespace JB2.Bowtie.Data
{
    public class LinqRepository<Tobject> : BaseRespository, JB2.Common.IRepository<Tobject, string> where Tobject : IBowtieObject
    {
        protected BowtieDataContext _dbcontext;
        private const string DB_PREFIX = "jb2bt_";
        protected JB2.Bowtie.Enum.BowtieObjectType _objType;

        public LinqRepository(BowtieDataContext dbc, JB2.Bowtie.Enum.BowtieObjectType type)
        {
            this._dbcontext = dbc;
            this._objType = type;

        }

        public LinqRepository()
        {
            this._dbcontext = new BowtieDataContext();
           
        }

        public void Delete(Tobject entity)
        {
            throw new NotImplementedException();
        }

        public Tobject[] GetAll()
        {
            
            switch(_objType)
            {
                case Enum.BowtieObjectType.bowtie_application:
                    var query = from i in _dbcontext.jb2bt_Application_Get(null, null)
                        select (Tobject)getApplication(i);
                    return query.ToArray();
                case Enum.BowtieObjectType.bowtie_client:
                    var query2 = from i in _dbcontext.jb2bt_Client_Get(null, null)
                                select (Tobject)getClient(i);
                    return query2.ToArray();
            }
            return default(Tobject[]);
        }

        public Tobject GetById(string id)
        {
            switch(_objType)
            {
                case Enum.BowtieObjectType.bowtie_application:
                    var query = from i in _dbcontext.jb2bt_Application_Get(null,id)
                        select getApplication(i);
                    return (Tobject)query.ToArray().FirstOrDefault();
                case Enum.BowtieObjectType.bowtie_client:
                    var query2 = from i in _dbcontext.jb2bt_Client_Get(null, null)
                                 select (Tobject)getClient(i);
                    return query2.ToArray().FirstOrDefault();
            }
            return default(Tobject);
            //throw new NotImplementedException();
        }

        public void Insert(Tobject entity)
        {
            throw new NotImplementedException();
        }

        public Tobject[] SearchFor()
        {
            throw new NotImplementedException();
        }

        internal static IApplication getApplication<T>(T r) where T : class
        {
            Application result = new Application(getString(r, "PublicKey"), getString(r, "Secret"))
            {
                ID = getString(r, "ID"),
                Name = getString(r, "Name"),
                ClientID = getString(r,"Client_Key")
            };
            return result;
        }

        internal static IClient getClient<T>(T r) where T : class
        {
            Client result = new Client(getString(r, "Key"))
            {
                Name = getString(r, "Name"),
            };
            return result;
        }
    }
}
