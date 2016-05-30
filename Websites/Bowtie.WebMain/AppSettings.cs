using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JB2.Bowtie;
namespace JB2.Bowtie.Web
{
    public static class Helper
    {

        public static IUnitOfWork UnitofWork
        {
            get
            {
                switch (JB2.Settings.Bowtie.Mode)
                {
                    case Enum.APIMode.Debug:
                    case Enum.APIMode.Production:
                        return GetUnitofWork(Enum.RepoDataSource.Standard);
                    default:
                        return GetUnitofWork(Enum.RepoDataSource.NoDB);
                }
            }
        }

        public static IUnitOfWork GetUnitofWork(Enum.RepoDataSource source)
        {
            switch (source)
            {
                case Enum.RepoDataSource.Standard:
                    return new JB2.Bowtie.Data.Azure.UnitOfWork();
                default:
                    return new JB2.Bowtie.Data.NoDB.UnitofWork();
            }
        }

    }
}