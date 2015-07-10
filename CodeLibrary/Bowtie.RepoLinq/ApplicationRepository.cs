using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;

namespace JB2.Bowtie.Data
{
    public class ApplicationRepository :  LinqRepository<IApplication>, IApplicationRepository
    {
        public ApplicationRepository(BowtieDataContext context): base(context)
        {

        }


        public IApplication[] GetAPIAllowedApps()
        {
            throw new NotImplementedException();
        }
    }
}
