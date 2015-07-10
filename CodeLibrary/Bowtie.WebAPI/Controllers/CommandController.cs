using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

using Bowtie.WebAPI.Filters;


namespace Bowtie.WebAPI.Controllers
{
    
    [RoutePrefix("api/v1/commands")]
    public class CommandController : ApiController
    {
        private JB2.Bowtie.IUnitOfWork _unitOfWork;


        public CommandController()
            : base()
        {
            this._unitOfWork = new Bowtie.WebAPI.Models.UnitOfWork(JB2.Bowtie.Enum.RepoDataSource.Standard);
        }

        // GET api/v1/commands/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            JB2.Bowtie.IApplicationRepository repo = (JB2.Bowtie.IApplicationRepository)this._unitOfWork.GetRepository(JB2.Bowtie.Enum.RepositoryType.Application);


            JB2.Bowtie.IApplication app = repo.GetById("1");
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            var Name = ClaimsPrincipal.Current.Identity.Name;

            return Ok(Order.CreateOrders());
        }

        [Route("")]
        public IHttpActionResult Post(JB2.Bowtie.GameCommand cmd)
        {
            //cmd.CommandCode = "test";
            return Ok(1);
        }

        public IEnumerable<JB2.Bowtie.GameCommand> GetAllCommands()
        {
            List<JB2.Bowtie.GameCommand> list = new List<JB2.Bowtie.GameCommand>();

            list.Add(new JB2.Bowtie.GameCommand());

            return list;
        }




    }

    #region Helpers

    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string ShipperCity { get; set; }
        public Boolean IsShipped { get; set; }


        public static List<Order> CreateOrders()
        {
            List<Order> OrderList = new List<Order> 
            {
                new Order {OrderID = 10248, CustomerName = "Taiseer Joudeh", ShipperCity = "Amman", IsShipped = true },
                new Order {OrderID = 10249, CustomerName = "Ahmad Hasan", ShipperCity = "Dubai", IsShipped = false},
                new Order {OrderID = 10250,CustomerName = "Tamer Yaser", ShipperCity = "Jeddah", IsShipped = false },
                new Order {OrderID = 10251,CustomerName = "Lina Majed", ShipperCity = "Abu Dhabi", IsShipped = false},
                new Order {OrderID = 10252,CustomerName = "Yasmeen Rami", ShipperCity = "Kuwait", IsShipped = true}
            };

            return OrderList;
        }
    }

    #endregion
}
