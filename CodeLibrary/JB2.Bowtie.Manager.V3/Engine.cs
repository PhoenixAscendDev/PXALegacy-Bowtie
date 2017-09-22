using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class Engine : JB2.Common.Singleton<Engine>
    {

        #region fields

       internal Dictionary<string,List<IAchievement>> _dewdropTriggers;

        #endregion Fields
        public Engine()
        {
            _dewdropTriggers = new Dictionary<string, List<IAchievement>>();

        }


        public IApplication CurrentApplication { get; internal set; }

        public IEnumerable<string> DewdropTriggers
        {
            get
            {
                return _dewdropTriggers.Keys;
            }

        }


        internal void CheckAchievement(JB2.Bowtie.DewdropData data, JB2.Bowtie.IDewdropEntry entry)
        {
            var dservice = JB2.Bowtie.Service.DewdropService.Instance;
            var aservice = JB2.Bowtie.Service.AchievementService.Instance;
            var appservice = JB2.Bowtie.Service.ApplicationService.Instance;

            
            var player = new BasicPlayer() { ID = entry.PlayerID };

            //only care if this app has a trigger on the dewdrop
            if (_dewdropTriggers.ContainsKey(entry.GDID))
            {
                foreach(var a in _dewdropTriggers[entry.GDID])
                {
                    var status = aservice.EvaluateAchievement(a,player );
                    var application = appservice.RetrieveApplicationById(a.ApplicationID).ToObject();


                    var dataset = aservice.RetrieveAchievementData(player, application).ToObject();
                    var aentry = new AchievementEntry();

                    aentry.AchievementID = a.ID;
                    aentry.ApplicationID = application.ID;
                    aentry.PlayerID = player.ID;
                    aentry.PercentComplete = (dataset.GetStepValue(a.StorageSlot) / a.StepsRequired) * 100;
                    aentry.DateEarned = DateTime.MinValue;
                    aentry.PointsEarned = 0;

                    if ( (status) && (status.ToObject() == Enum.AchievementStatusType.Achieved))
                    {
                        var points = dataset.GetPoints(a.StorageSlot);
                        var dt = dataset.GetDateAchieved(a.StorageSlot);

                        aentry.DateEarned = dt;
                        aentry.PointsEarned = points;

                        JB2.Events.Bowtie.OnAchievementAchieved(a, aentry);

                    }
                }
            }


        }


    }
}
