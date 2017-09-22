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

        public AchievementData GenerateAchievementData(IPlayer player, IApplication application)
        {
            var playerID = player.ID;
            var applicationID = application.ID;
            var dd = AchievementData.Empty;

            _uofw.AchievementRepository.InsertAchievementData(applicationID, playerID, dd);

            return dd;

        }

        public ServiceResult<AchievementData> RetrieveAchievementData(IPlayer player, IApplication application)
        {
            try
            {
                var result = _uofw.AchievementRepository.GetDataByPlayer(application.ID, player.ID);

                if(result == null)
                {
                    result = GenerateAchievementData(player, application);
                }

                return new ServiceResult<AchievementData>(result);
            }
            catch(Exception ex)
            {
                return ex.ToServiceResult<AchievementData>();
            }
        }


        public ServiceResult<Enum.AchievementStatusType> EvaluateAchievement(IAchievement achievement, IPlayer player)
        {
            try
            {

                Enum.AchievementStatusType result = Enum.AchievementStatusType.NotAcheived;
                var rules = achievement.GetStepRules();

                var dataset = _uofw.AchievementRepository.GetDataByPlayer(achievement.ApplicationID, player.ID);
                var application = _uofw.ApplicationRepository.GetById(achievement.ApplicationID);

                if (dataset == null)
                {                  
                    dataset = this.GenerateAchievementData(player, application);
                }


                var status = dataset.GetStatus(achievement.StorageSlot);

                if(status != Enum.AchievementStatusType.NotAcheived)
                {
                    return new ServiceResult<Enum.AchievementStatusType>(Enum.AchievementStatusType.AlreadyAchieved);
                }



                foreach (var rule in rules)
                {
                    int newValue = 0;
                    byte slot = achievement.StorageSlot;
                    string GDID = string.Empty;
                    var oldValue = dataset.GetStepValue(slot);
                    var stepRequested = achievement.StepsRequired;
                    switch (rule.StepType)
                    {
                        case Enum.StepFxType.DewdropIncrement:
                            string[] parts = rule.StepFx.Split('|');
                            GDID = parts[0].Trim();
                            var mult = parts[1].Trim();                       
                            newValue = oldValue + (1 * Convert.ToInt32(mult));
                            break;

                        case Enum.StepFxType.DewdropValue:
                            GDID = rule.StepFx.TrySplit('|', 0).Trim();
                            var dewdropValue = player.GetDewdropValue(application, GDID);
                            newValue = dewdropValue;
                            break;
                    }

                    dataset.SetStepValue(slot,newValue);

                    _uofw.AchievementRepository.InsertAchievementData(application.ID, player.ID, dataset);

                    if (newValue >= stepRequested)
                    {
                        dataset.SetDateAcheived(slot, DateTime.UtcNow);
                        dataset.SetPoints(slot, Convert.ToByte(achievement.Points));
                        dataset.SetStatus(slot, Enum.AchievementStatusType.Achieved);
                        dataset.SetStepValue(slot, newValue);

                        _uofw.AchievementRepository.InsertAchievementData(application.ID, player.ID, dataset);

                        return new ServiceResult<Enum.AchievementStatusType>(Enum.AchievementStatusType.Achieved);


                    }


                }

                return new ServiceResult<Enum.AchievementStatusType>(result);
            }
            catch(Exception ex)
            {
                ex.ToServiceResult<int>();
            }




            return true;
        }
    }
}
