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
            

            //only care if this app has a trigger on the dewdrop
            if(_dewdropTriggers.ContainsKey(entry.GDID))
            {
                foreach(var a in _dewdropTriggers[entry.GDID])
                {
                    aservice.EvaluateAchievement(a, new BasicPlayer() { ID = entry.PlayerID });
                }
            }


        }


    }
}
