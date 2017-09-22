using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.Extensions;

namespace JB2.Bowtie
{
    public  class Manager
    {
        public static JB2.Common.ServiceResult LoadApplication(string appconfig)
        {
            try
            {
                var aservice = JB2.Bowtie.Service.ApplicationService.Instance;

                var app = aservice.ImportApplication(appconfig);

                if (!app)
                    throw app.Validation[0].ToException();

                JB2.Bowtie.Engine.Instance.CurrentApplication = app.ToObject();
                

                foreach(var a in app.ToObject().GetAchievements())
                {
                    foreach (var s in a.GetDewdropTriggers())
                    {

                        if (!Engine.Instance._dewdropTriggers.ContainsKey(s))
                            Engine.Instance._dewdropTriggers[s] = new List<IAchievement>();

                        Engine.Instance._dewdropTriggers[s].Add(a);

                    }
                }

                JB2.Events.Bowtie.Instance.DewdropDataUpdated += Engine.Instance.CheckAchievement;

                return true;
            }
            catch(Exception ex)
            {
                return ex.ToServiceResult();
            }
        }

    }
}
