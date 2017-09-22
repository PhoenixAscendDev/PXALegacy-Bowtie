using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Extensions
{
    public static class ServicePlayerExtenstions
    {

        public static string ToJson(this IBowtieObject obj)
        {
            return JB2.Helper.Bowtie.ConvertToJsonString(obj);
        }


        public static JB2.Bowtie.DewdropData GetDewdropData(this IPlayer player, string applicationID)
        {

            BasicApplication app = new BasicApplication();
            app.ID = applicationID;

            return player.GetDewdropData(app);
        }


        public static JB2.Bowtie.DewdropData GetDewdropData(this IPlayer player, IApplication application)
        {
            var dds = JB2.Bowtie.Service.DewdropService.Instance;

            var data = dds.RetrieveDewdropData(player, application);

            if ((data) && (data.ToObject() == null))
            {
                data = dds.GenerateDewdropData(player, application);
            }

            return data;
        }

        public static JB2.Bowtie.AchievementData GetAchievementData(this IPlayer player, IApplication application)
        {
            var aservice = JB2.Bowtie.Service.AchievementService.Instance;

            var data = aservice.RetrieveAchievementData(player, application);

            if ((data) && (data.ToObject() == null))
            {
                data = aservice.GenerateAchievementData(player, application);
            }

            return data.ToObject();
        }

        public static IEnumerable<JB2.Bowtie.IAchievementEntry> GetAchievements(this IPlayer player, IApplication application)
        {
            var aservice = JB2.Bowtie.Service.AchievementService.Instance;

            var data = aservice.RetrievePlayerAcheivements(player,application );

            return data.ToObject();
        }

        public static JB2.Common.ServiceResult SetDewdropData(this IPlayer player, IApplication application, DewdropData data)
        {
            var dds = JB2.Bowtie.Service.DewdropService.Instance;

            var result = dds.SaveDewdropData(player, application, data);

            return result;
        }

        public static IEnumerable<IDewdropEntry> GetDewdropLog(this IPlayer player)
        {
            var dds = JB2.Bowtie.Service.DewdropService.Instance;
            var result = dds.RetrieveDewdropLogByPlayer(player);

            if (result)
                return result.ToObject();
            else
                return new IDewdropEntry[0];
        }

        public static IEnumerable<IDewdropEntry> GetDewdropLog(this IPlayer player, IApplication application)
        {
            var dds = JB2.Bowtie.Service.DewdropService.Instance;
            var result = dds.RetrieveDewdropLog(player, application);

            if (result)
                return result.ToObject();
            else
                return new IDewdropEntry[0];
        }

        public static IDewdropEntry AddDewdrop(this IPlayer player, IApplication application, string GDID, ushort value = 0, string message = "" )
        {
            var dds = JB2.Bowtie.Service.DewdropService.Instance;

            var result = dds.AddDewdropEntry(player, application, value, message, GDID);

            if (result)
                return result.ToObject();
            else
                return null;
        }

        public static int GetDewdropValue(this IPlayer player, IApplication application, string GDID)
        {
            var dds = JB2.Bowtie.Service.DewdropService.Instance;

            var result = dds.RetrieveDewdropValue(player, application, GDID);

            if (result)
                return result.ToObject();
            else
                return 0;
        }

    }

    public static class ServiceExceptionExtenstions
    {

        public static void BowtieLogIt(this Exception ex, string message = "", string logcode="")
        {
            var logger = JB2.Settings.Bowtie.Logger;

            var ls = JB2.Bowtie.Service.LogService.Instance;

            ls.LogError(ex, message, logcode);
        }

        public static JB2.Common.ServiceResult ToServiceResult(this Exception ex, string message = "", string logcode = "", bool logit = true)
        {
            var r = new JB2.Common.ServiceResult(ex);
            message = String.IsNullOrEmpty(message) ? ex.Message : message;
            r.Validation[0].Message = message;

            if (logit)
            {
                ex.BowtieLogIt(message, logcode);
            }

            return r;
        }

        public static JB2.Common.ServiceResult<T> ToServiceResult<T>(this Exception ex, string message = "", string logcode = "", bool logit = true)
        {
            var r = new JB2.Common.ServiceResult<T>(ex);
            message = String.IsNullOrEmpty(message) ? ex.Message : message;
            r.Validation[0].Message = message;

            if(logit)
            {
                ex.BowtieLogIt(message, logcode);
            }

            return r;
        }


        public static void BowtieLogIt(this JB2.Common.JB2Exception ex, string message = "")
        {
            var logger = JB2.Settings.Bowtie.Logger;

            var ls = JB2.Bowtie.Service.LogService.Instance;

            ls.LogError(ex, message, ex.Code);
        }

        public static JB2.Common.ServiceResult<T> ToServiceResult<T>(this JB2.Common.JB2Exception ex, string message = "", bool logit = true)
        {
            var r = new JB2.Common.ServiceResult<T>(ex);
            message = String.IsNullOrEmpty(message) ? ex.Message : message;
            r.Validation[0].Message = message;

            if (logit)
            {
                ex.BowtieLogIt(message, ex.Code);
            }

            return r;
        }


    }


    public static class ServiceApplicationExtenstions
    {
        public static IEnumerable<IAchievement> GetAchievements(this IApplication application)
        {
            var dds = JB2.Bowtie.Service.AchievementService.Instance;

            var data = dds.RetrieveByApplication(application);

            if ((data) && (data.ToObject() == null))
            {
                return new IAchievement[0];
            }

            return data.ToObject();
        }

        public static IEnumerable<IDewdrop> GetDewdrops(this IApplication application)
        {
            var dds = JB2.Bowtie.Service.DewdropService.Instance;

            var data = dds.RetrieveByApplication(application);

            if ((data) && (data.ToObject() == null))
            {
                return new IDewdrop[0];
            }

            return data.ToObject();
        }


    }


    public static class ServiceAchievementExtenstions
    {
        public static IEnumerable<AchievementStepRule> GetStepRules(this IAchievement achievement)
        {
            var dds = JB2.Bowtie.Service.AchievementService.Instance;

            var data = dds.RetrieveStepRules(achievement);

            if ((data) && (data.ToObject() == null))
            {
                return new AchievementStepRule[0];
            }

            return data.ToObject();
        }

        public static IEnumerable<string> GetDewdropTriggers(this IAchievement achievement)
        {
            var dds = JB2.Bowtie.Service.AchievementService.Instance;

            var rules = achievement.GetStepRules();

            List<string> list = new List<string>();

            foreach(var r in rules)
            {
                switch(r.StepType)
                {
                    case Enum.StepFxType.DewdropIncrement:
                    case Enum.StepFxType.DewdropValue:
                        string[] parts = r.StepFx.Split('|');
                        string GDID = parts[0].Trim();
                        list.Add(GDID);
                        break;
                }
            }

            return list;
        }
    }
}
