using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.Extensions;

namespace JB2.Bowtie.Service
{
    public class LeaderboardService : JB2.Common.Singleton<LeaderboardService>
    {
        #region Fields

        protected IUnitOfWork _uofw;

        #endregion Fields

        #region Constructors
        public LeaderboardService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }

        public LeaderboardService(IUnitOfWork unitofwork)
        {
            _uofw = unitofwork;
        }



        #endregion Constructors


        public JB2.Common.ServiceResult<IEnumerable<ILeaderboard>> RetrieveByApplication(IApplication application)
        {
            try
            {

                var data =  _uofw.LeaderboardRepository.GetByApplicationID(application.ID);

                return new JB2.Common.ServiceResult<IEnumerable<ILeaderboard>>(data);
            }
            catch(Exception ex)
            {
                return ex.ToServiceResult<IEnumerable<ILeaderboard>>();
            }
        }

        public JB2.Common.ServiceResult<ILeaderboard> RetrieveByID(string id)
        {
            try
            {

                var data = _uofw.LeaderboardRepository.GetById(id);

                return new JB2.Common.ServiceResult<ILeaderboard>(data);
            }
            catch (Exception ex)
            {
                return ex.ToServiceResult<ILeaderboard>();
            }
        }

        public JB2.Common.ServiceResult Save(ILeaderboard entity)
        {
            try
            {
                _uofw.LeaderboardRepository.Insert(entity);

                return true;
            }
            catch(Exception ex)
            {
                return ex.ToServiceResult();
            }
        }
    }
}
