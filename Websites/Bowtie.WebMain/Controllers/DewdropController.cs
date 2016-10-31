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

        public ActionResult Edit(string id)
        {
            var model = GetByID(id);

            ViewBag.Applications = this.applicationSelectList();
            ViewBag.GraphActions = this.graphactionsSelectList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DewdropViewModel m)
        {
            var service = this.DewdropService;
            var dewdrop = new Dewdrop(m.ID, m.Name, m.Description, m.ApplicationID, m.GraphID, m.jBeanCost);

            service.Save(dewdrop);

            return RedirectToAction("All");
        }

        
        public ActionResult Delete(string id)
        {
            var service = this.DewdropService;

            var dewdrop = service.RetrieveById(id);

            if (dewdrop != null)
                service.Remove(dewdrop);

            return RedirectToAction("All");


        }


        private DewdropViewModel GetByID(string id)
        {
            var service = this.DewdropService;

            var app = service.RetrieveById(id);

            var model = AutoMapper.Mapper.Map<IDewdrop, DewdropViewModel>(app);

            return model;
        }
    }
}