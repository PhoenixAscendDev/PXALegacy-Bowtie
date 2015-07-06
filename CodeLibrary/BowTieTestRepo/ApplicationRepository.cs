using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Test
{
    public class ApplicationRepository : JB2.Bowtie.IApplicationRepository
    {
        public void Delete(Application entity)
        {
            throw new NotImplementedException();
        }

        public Application[] GetAll()
        {
            throw new NotImplementedException();
        }

        public Application GetById(int id)
        {
            return new Application("174B863C17", "39c11f9ccc0d48349918-093505a8df32.battlesim");
        }

        public void Insert(Application entity)
        {
            throw new NotImplementedException();
        }

        public Application[] SearchFor()
        {
            throw new NotImplementedException();
        }
    }
}
