using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;

namespace JB2.Bowtie.Data.Linq
{
    public class ApplicationRepository :  LinqRepository<IApplication>, IApplicationRepository
    {
        public ApplicationRepository(BowtieDataContext context): base(context,Enum.BowtieObjectType.bowtie_application)
        {

        }


        public IApplication[] GetAPIAllowedApps()
        {
            throw new NotImplementedException();
        }


        public IApplication[] GetApplicationsByClientID()
        {
            throw new NotImplementedException();
        }
    }
}
