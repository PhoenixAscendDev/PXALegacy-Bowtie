using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.Extensions;

namespace JB2.Bowtie
{
    public class Engine : JB2.Common.Singleton<Engine>
    {

        #region fields

        internal Dictionary<string,List<IAchievement>> _dewdropTriggers;
        internal System.Timers.Timer _timer;
        internal Dictionary<string,DateTime> _stopwatch;
        //internal Dictionary<string,DateTime> _timelastupdate;
        private const string GAMETIME_GDID = "898E1697";

        #endregion Fields
        public Engine()
        {
            _dewdropTriggers = new Dictionary<string, List<IAchievement>>();
            _stopwatch = new Dictionary<string, DateTime>();
            //_timelastupdate = new Dictionary<string, TimeSpan>();


        }

        public IApplication CurrentApplication { get; internal set; }

        public IEnumerable<string> DewdropTriggers
        {
            get
            {
                return _dewdropTriggers.Keys;
            }
        }

        public void StartTimer(IPlayer p)
        {
            if (!_stopwatch.ContainsKey(p.ID))
            {
                //var sw = new System.Diagnostics.Stopwatch();
                //sw
                _stopwatch.Add(p.ID, JB2.Helper.Bowtie.Now() );
            }

            _stopwatch[p.ID] = JB2.Helper.Bowtie.Now();
        }
        public void StopTimer(IPlayer p)
        {
            if (_stopwatch[p.ID] != DateTime.MinValue)
            {
                TimeSpan ts = JB2.Helper.Bowtie.Now() - _stopwatch[p.ID];
                updateGameTime(p, ts);
            }

            _stopwatch[p.ID] = DateTime.MinValue;
        }


        public ushort MinutesPlayed(IPlayer p)
        {
            var v = p.GetDewdropValue(this.CurrentApplication, GAMETIME_GDID);

            return v >= ushort.MaxValue ? ushort.MaxValue : Convert.ToUInt16(v);
        }


        protected virtual void updateGameTime(IPlayer p,TimeSpan ts)
        {


            var lasttime = ts;


            var current = p.GetDewdropValue(this.CurrentApplication, GAMETIME_GDID);

            var dservice = JB2.Bowtie.Service.DewdropService.Instance;

            ushort value = (current + lasttime.Minutes) >= 65535 ? (ushort)65535 : Convert.ToUInt16((current + lasttime.Minutes));

            dservice.AddDewdropEntry(p, this.CurrentApplication, value, "from Engine", GAMETIME_GDID);
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


                    var steps = dataset.GetStepValue(a.StorageSlot);
                    aentry.AchievementID = a.ID;
                    aentry.ApplicationID = application.ID;
                    aentry.PlayerID = player.ID;
                    aentry.PercentComplete = steps != 0 ? (a.StepsRequired / steps) * 100 : 0;
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
