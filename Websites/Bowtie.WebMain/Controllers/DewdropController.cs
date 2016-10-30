using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using JB2.Bowtie.Web.Models;

namespace JB2.Bowtie.Web.Controllers
{
    public class DewdropController : BaseController
    {
        // GET: Dewdrop
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult All()
        {
            var service = this.DewdropService;

            var list = service.Retrieve();

            var model = new List<DewdropViewModel>(list.Count());

            foreach (var d in list)
            {
                model.Add(AutoMapper.Mapper.Map<IDewdrop, DewdropViewModel>(d));
            }

            ViewBag.ApplicationSelect = this.applicationSelectList();

            return View(model);
        }
    }
}