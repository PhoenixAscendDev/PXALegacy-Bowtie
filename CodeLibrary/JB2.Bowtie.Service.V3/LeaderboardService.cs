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

                var data = _uofw.LeaderboardRepository.GetByApplicationID(application.ID);

                return new JB2.Common.ServiceResult<IEnumerable<ILeaderboard>>(data);
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return ex.ToServiceResult();
            }
        }

        public JB2.Common.ServiceResult<IEnumerable<ILeaderboardRankedScore>> RetrieveScores(ILeaderboard leaderboard)
        {
            try
            {
                var repo = _uofw.LogRepository;

                var list = repo.GetScoresByLeaderboard(leaderboard.ID);

                var result = convertToRankedScore(leaderboard, list);

                return new JB2.Common.ServiceResult<IEnumerable<ILeaderboardRankedScore>>(result);
            }
            catch(Exception ex)
            {
                return ex.ToServiceResult< IEnumerable<ILeaderboardRankedScore>>();
            }
            

            

        }

        public JB2.Common.ServiceResult<ILeaderboardRankedScore> RetrieveMaxScoreByPlayer(ILeaderboard leaderboard, IPlayer player)
        {
            try
            {
                var repo = _uofw.LogRepository;

                var list = repo.GetScoresByLeaderboard(leaderboard.ID);

                var rankedList = convertToRankedScore(leaderboard, list);

                var result = rankedList.Where(x => x.GetPlayerID() == player.GetPlayerID()).OrderBy(x => x.Ranked);



                return new JB2.Common.ServiceResult<ILeaderboardRankedScore>(result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return ex.ToServiceResult<ILeaderboardRankedScore>();
            }
        }

        public JB2.Common.ServiceResult<IEnumerable<ILeaderboardRankedScore>> RetrieveAllScoresByPlayer(ILeaderboard leaderboard, IPlayer player)
        {
            try
            {
                var repo = _uofw.LogRepository;

                var list = repo.GetScoresByLeaderboard(leaderboard.ID);

                var rankedList = convertToRankedScore(leaderboard, list);

                var result = rankedList.Where(x => x.GetPlayerID() == player.GetPlayerID()).OrderBy(x => x.Ranked);



                return new JB2.Common.ServiceResult<IEnumerable<ILeaderboardRankedScore>>(result);
            }
            catch (Exception ex)
            {
                return ex.ToServiceResult<IEnumerable<ILeaderboardRankedScore>>();
            }

           
        }
        public JB2.Common.ServiceResult<ILeaderboardRankedScore> RetrieveScoreByRank(ILeaderboard leaderboard, int rank)
        {
            try
            {
                var repo = _uofw.LogRepository;

                var list = repo.GetScoresByLeaderboard(leaderboard.ID);

                var rankedList = convertToRankedScore(leaderboard, list);

                var result = rankedList.Where(x => x.Ranked == rank).OrderBy(x => x.Ranked);

                return new JB2.Common.ServiceResult<ILeaderboardRankedScore>(result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return ex.ToServiceResult<ILeaderboardRankedScore>();
            }

        }

        public JB2.Common.ServiceResult<IEnumerable<ILeaderboardRankedScore>> RetrieveScoreByRankRange(ILeaderboard leaderboard, int rankMin, int rankMax)
        {
            try
            {
                var repo = _uofw.LogRepository;

                var list = repo.GetScoresByLeaderboard(leaderboard.ID);

                var rankedList = convertToRankedScore(leaderboard, list);

                var result = rankedList.Where(x => x.Ranked >= rankMin && x.Ranked <= rankMax).OrderBy(x => x.Ranked);



                return new JB2.Common.ServiceResult<IEnumerable<ILeaderboardRankedScore>>(result);
            }
            catch (Exception ex)
            {
                return ex.ToServiceResult<IEnumerable<ILeaderboardRankedScore>>();
            }
        }

        public JB2.Common.ServiceResult<int> CalculateScoreRank(ILeaderboard leaderboard, ILeaderboardEntry entry)
        {
            try
            {
                var repo = _uofw.LogRepository;

                var list = repo.GetScoresByLeaderboard(leaderboard.ID);

                list.ToList().Add(entry);

                var result = convertToRankedScore(leaderboard, list);

                result = result.Where(x => x.Score == entry.Score).OrderBy(x => x.Ranked).ToList();

                return new JB2.Common.ServiceResult<int>(result.FirstOrDefault().Ranked);
            }
            catch (Exception ex)
            {
                return ex.ToServiceResult<int>();
            }
        }


        public JB2.Common.ServiceResult SubmitScore(ILeaderboard leaderboard, ILeaderboardEntry entry)
        {
            try
            {
                _uofw.LogRepository.Insert(entry);

                return true;
            }
            catch (Exception ex)
            {
                return ex.ToServiceResult();
            }
        }

        #region Helpers

        private IEnumerable<ILeaderboardRankedScore> convertToRankedScore(ILeaderboard leaderboard, IEnumerable<ILeaderboardEntry> list, bool ignoreDateRange = false)
        {
            ILeaderboardEntry[] rankedList = list.ToArray();

            if(!ignoreDateRange)
                list = list.Where(x => x.ScoreDate >= leaderboard.DateRangeStart && x.ScoreDate <= leaderboard.DateRangeEnd);

            switch (leaderboard.ScoreOrderType)
            {
                case Enum.ScoreOrderType.LargeOnTop:
                    rankedList = list.ToList().OrderByDescending(x => x.Score).ToArray();
                    break;
                case Enum.ScoreOrderType.SmallOnTop:
                    rankedList = list.ToList().OrderBy(x => x.Score).ToArray();
                    break;
            }

            var result = new List<ILeaderboardRankedScore>();

            for (int i = 0; i < rankedList.Length; i++)
            {
                var rankedScore = new LeaderboardRankedScore(rankedList[i], i+1);
                result.Add(rankedScore);
            }

            return result.ToArray();
        }

        #endregion Helpers


    }
}
