using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using JB2.Bowtie.Service;

namespace JB2.Bowtie.WebAPI.Controllers
{
    [RoutePrefix("api/v1/dewdrop")]
    public class DewdropController : JB2.Common.WebAPI.BaseApiController
    {
        public DewdropController()
            : base()
        {

        }

        [HttpGet]
        public IHttpActionResult Add(string id, string playerid, string value)
        {
            try
            {
                var service = new DewdropService(JB2.Settings.Bowtie.UnitOfWork);
                var metadata = new List<JB2.Common.IMetaData>();
                metadata.Add(new JB2.Common.StringMetaData("PlayerID", playerid));
                metadata.Add(new JB2.Common.StringMetaData("DewdropID", id));
                metadata.Add(new JB2.Common.DateTimeMetaData("DewDate", DateTime.Now));

                var newdew = new PlayerDewdrop(metadata, value);
                var success = service.Save(newdew);
                if (success)
                    return Ok();
                else
                    throw new Exception("Error in saving player dewdrop");
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);

            }

        }

        [HttpPost]
        public IHttpActionResult Add(JB2.Bowtie.PlayerDewdrop pd)
        {
            try
            {
                var service = new DewdropService(JB2.Settings.Bowtie.UnitOfWork);

                var success = service.Save(pd);
                if (success)
                    return Ok();
                else
                    throw new Exception("Error in saving player dewdrop");
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);

            }


        }


    }
}
