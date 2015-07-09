using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JB2.Bowtie;

namespace Bowtie.WebAPI.Service
{
    public class ApplicationService : GenericService<IApplication>
    {

        public ApplicationService()
        {

        }

        public ApplicationService(IApplicationRepository repo): base( repo)
        {
        }


    }
}