using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Bowtie;
using JB2.Bowtie.Extensions;

namespace JB2.Bowtie.Service
{
    public class AchievementService : JB2.Common.Singleton<AchievementService>
    {
        #region Fields

        protected IUnitOfWork _uofw;

        #endregion Fields

        #region Constructors
        public AchievementService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }

        public AchievementService(IUnitOfWork unitofwork)
        {
            _uofw = unitofwork;
        }

        #endregion Constructors


        public ServiceResult<IEnumerable<IAchievement>> RetrieveByApplication(IApplication application)
        {
            try

            {
                var result = _uofw.AchievementRepository.GetByApplicationID(application.ID);

                return new ServiceResult<IEnumerable<IAchievement>>(result);
            }
            catch(Exception ex)
            {
                return ex.ToServiceResult<IEnumerable<IAchievement>>();
            }
        }


        public ServiceResult<IEnumerable<AchievementStepRule>> RetrieveStepRules(IAchievement achievement)
        {
            try

            {
                var result = _uofw.AchievementRepository.GetStepsByAchievementID(achievement.ID);

                return new ServiceResult<IEnumerable<AchievementStepRule>>(result);
            }
            catch (Exception ex)
            {
                return ex.ToServiceResult<IEnumerable<AchievementStepRule>>();
            }
        }


        public ServiceResult<IAchievement> GenerateNew(string name, int points, IApplication application,byte storageSlot, Enum.AchievementType type = Enum.AchievementType.Standard, string category = "")
        {

            try
            {
                var app = new BasicAchievement();
                app.Name = name;
                app.ApplicationID = application.ID;
                app.AchievementType = type;
                app.ID = JB2.Helper.Bowtie.GenerateID<BasicAchievement>();
                app.Category = category;
                app.StorageSlot = storageSlot;
                app.Points = points;
               
                _uofw.AchievementRepository.Insert(app);

                return new ServiceResult<IAchievement>(app);
            }
            catch(Exception ex)
            {
                return ex.ToServiceResult<IAchievement>();
            }


        }

        public ServiceResult Save(IAchievement a)
        {
            try

            {
                _uofw.AchievementRepository.Insert(a);

                return true;
            }
            catch(Exception ex)
            {
                return ex.ToServiceResult();
            }

        }

        public ServiceResult Save(AchievementStepRule a)
        {
            try

            {
                _uofw.AchievementRepository.Insert(a);

                return true;
            }
            catch (Exception ex)
            {
                return ex.ToServiceResult();
            }

        }




        public ServiceResult<Enum.AchievementStatusType> EvaluateAchievement(IAchievement achievement, IPlayer player)
        {
            try
            {

                Enum.AchievementStatusType result = Enum.AchievementStatusType.NotAcheived;
                var rules = achievement.GetStepRules();

                foreach(var rule in rules)
                {
                    switch(rule.StepType)
                    {
                        case Enum.StepFxType.DewdropIncrement:
                            string[] parts = new string[2];

                            var GDID = rule.StepFx.TrySplit('|', 0).Trim();
                            var mult = rule.StepFx.TrySplit('|', 1).Trim();
                            var dewdrop = _uofw.DewdropRepository.GetByGDID(GDID);
                            var ddata = player.GetDewdropData(achievement.ApplicationID);
                            ddata.IncrementDewDrop(dewdrop.ID);
                            var newValue = ddata.GetDewDropValue(dewdrop.ID);
                            break;


                    }
                }

                return result;
            }
            catch(Exception ex)
            {
                ex.ToServiceResult<int>();
            }




            return true;
        }
    }
}
