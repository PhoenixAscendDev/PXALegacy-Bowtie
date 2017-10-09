using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Bowtie.Extensions;

namespace JB2.Bowtie.Service
{
    public class PointService : JB2.Common.Singleton<PointService>
    {
        #region Fields

        protected IUnitOfWork _uofw;

        #endregion Fields

        #region Constructors
        public PointService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }

        public PointService(IUnitOfWork unitofwork)
        {
            _uofw = unitofwork;
        }

        #endregion Constructors

        public ServiceResult<int> RetrieveTotalPoints(IPlayer player, IApplication application)
        {
            try
            {
                var lr = _uofw.LogRepository;

                var elist = lr.GetPointEntryByApplicationID(application.ID, player.ID);

                var result = elist.Sum(x => x.PointsEarned);

                return new ServiceResult<int>(result);
            }
            catch(Exception ex)
            {
               return ex.ToServiceResult<int>();
            }
            
        }

    }
}
