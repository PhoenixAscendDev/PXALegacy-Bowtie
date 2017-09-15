using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.Local
{
    public class DewDropRepo : JB2.Common.Singleton<DewDropRepo>, IDewdropRepository
    {
        public void Delete(IDewdrop entity)
        {
            throw new NotImplementedException();
        }

        public IDewdrop[] GetAll()
        {
            throw new NotImplementedException();
        }

        public IDewdrop[] GetAll(int? maxRecordCount)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDewdrop> GetByApplicationID(string appID)
        {
            throw new NotImplementedException();
        }

        public IDewdrop GetByGDID(string gdid)
        {
            throw new NotImplementedException();
        }

        public IDewdrop GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(IDewdrop entity)
        {
            throw new NotImplementedException();
        }

        public IDewdrop[] SearchFor()
        {
            throw new NotImplementedException();
        }

        public IDewdrop[] SearchFor(string filter)
        {
            throw new NotImplementedException();
        }
    }
}
