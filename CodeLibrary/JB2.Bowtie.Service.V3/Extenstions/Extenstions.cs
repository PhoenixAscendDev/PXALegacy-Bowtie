using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Extensions
{
    public static class ServicePlayerExtenstions
    {
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
    }


    public static class ServiceExceptionExtenstions
    {

        public static void BowtieLogIt(this Exception ex, string message = "", string logcode="")
        {
            var logger = JB2.Settings.Bowtie.Logger;

            var ls = JB2.Bowtie.Service.LogService.Instance;

            ls.LogError(ex, message, logcode);
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
}
