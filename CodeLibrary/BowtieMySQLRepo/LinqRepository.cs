using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Linq;

namespace JB2.Bowtie.Data
{
    public class LinqRepository
    {
        protected DataContext _dbcontext;

        public LinqRepository(DataContext dbc)
        {
            this._dbcontext = dbc;
        }

        public LinqRepository()
        {
            this._dbcontext = new BowtieDataContext();
           
        }


    }
}
