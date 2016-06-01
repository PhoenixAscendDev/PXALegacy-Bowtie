using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using JB2.Bowtie.Web.Models;

namespace JB2.Bowtie.Web.Controllers
{
    public class ApplicationController : BaseController
    {
        // GET: Application
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult All()
        {

            var appService = new JB2.Bowtie.Service.ApplicationService(JB2.Settings.Bowtie.UnitOfWork);

            var appslist = appService.Retrieve();

            var model = new List<ApplicationViewModel>(appslist.Count());

            foreach(var a in appslist)
            {
                model.Add(AutoMapper.Mapper.Map<IApplication, ApplicationViewModel>(a));
            }

            ViewBag.ApplicationSelect = this.applicationSelectList();
           
            return View(model);
        }
    }
}