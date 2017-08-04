using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

using JB2.Bowtie.Service;
using JB2.Bowtie.Web;

namespace JB2.Bowtie.Web.Models
{
    public class BaseViewModel : JB2.Common.IDNamePair<string, string>
    {

        public virtual IEnumerable<SelectListItem> GetApplicationSelectList(string headerText = "Select Application")
        {
            var list = BaseViewModel.ApplicationSelectList().ToList();
            list.Insert(0, new SelectListItem() { Text = headerText, Value = "-1" });
            list.Insert(1, new SelectListItem() { Text = "-------------", Value = "-1" });
            list.Insert(2, new SelectListItem() { Text = "-N/A-", Value = "" });

            return list;
        }

        public virtual IEnumerable<SelectListItem> GetCurrencySystemSelectList(string headerText = "Select Currency")
        {
            var list = BaseViewModel.SystemSelectList("currency").ToList();
            list.Insert(0, new SelectListItem() { Text = headerText, Value = "-1" });
            list.Insert(1, new SelectListItem() { Text = "-------------", Value = "-1" });
            list.Insert(2, new SelectListItem() { Text = "-N/A-", Value = "" });

            return list;
        }

        public virtual IEnumerable<SelectListItem> GetPointSystemSelectList(string headerText = "Select Point")
        {
            var list = BaseViewModel.SystemSelectList("point").ToList();
            list.Insert(0, new SelectListItem() { Text = headerText, Value = "-1" });
            list.Insert(1, new SelectListItem() { Text = "-------------", Value = "-1" });
            list.Insert(2, new SelectListItem() { Text = "-N/A-", Value = "" });

            return list;
        }


        #region Static
        protected static IUnitOfWork _unitOfWork;
        public static IUnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork != null)
                    return JB2.Settings.Bowtie.UnitOfWork;
                else
                    return _unitOfWork;
                
            }
            set
            {
                _unitOfWork = value;
            }
        }


        #region Application
        public static IEnumerable<SelectListItem> ApplicationSelectList(IUnitOfWork uofw)
        {          
            var service = new ApplicationService(uofw);
            var appsAll = service.Retrieve();
            return appsAll.ToSelectItems();
        }

        public static IEnumerable<SelectListItem> ApplicationSelectList()
        {
            return ApplicationSelectList(BaseViewModel.UnitOfWork);
        }

        #endregion Application

        #region Graph

        public static IEnumerable<SelectListItem> GraphObjectsSelectList(IUnitOfWork uofw)
        {
            var service = new GraphService(uofw);
            var all = service.RetrieveObjects();
            return all.ToSelectItems();
        }

        public static IEnumerable<SelectListItem> GraphObjectsSelectList()
        {
            return GraphObjectsSelectList(BaseViewModel.UnitOfWork);
        }

        public static IEnumerable<SelectListItem> GraphPropertiesSelectList(IUnitOfWork uofw)
        {
            var service = new GraphService(uofw);
            var all = service.RetreiveProperties();
            return all.ToSelectItems();
        }

        public static IEnumerable<SelectListItem> GraphPropertiesSelectList()
        {
            return GraphPropertiesSelectList(BaseViewModel.UnitOfWork);
        }

        public static IEnumerable<SelectListItem> GraphActionSelectList(IUnitOfWork uofw)
        {
            var service = new GraphService(uofw);
            var all = service.RetrieveActions();
            return all.ToSelectItems();
        }

        public static IEnumerable<SelectListItem> GraphActionSelectList()
        {
            return GraphActionSelectList(BaseViewModel.UnitOfWork);
        }

        public static IEnumerable<SelectListItem> GraphStorySelectList(IUnitOfWork uofw)
        {
            var service = new GraphService(uofw);
            var all = service.RetrieveStories();
            return all.ToSelectItems();
        }

        public static IEnumerable<SelectListItem> GraphDataTypeSelectList(IUnitOfWork uofw)
        {
            var service = new GraphService(uofw);
            var all = service.RetrievePropertyTypes();
            return all.ToSelectItems();
        }

        public static IEnumerable<SelectListItem> GraphDataTypeSelectList()
        {
            return GraphDataTypeSelectList(BaseViewModel.UnitOfWork);
        }


        #endregion Graph

        #region System
        protected static IEnumerable<SelectListItem> SystemSelectList(string category, IUnitOfWork uofw)
        {

            var service = new SystemService(uofw);
            List<SelectListItem> items = new List<SelectListItem>();

            var systemAll = service.RetrieveConfigsByCategory(category);

            foreach (JB2.Common.IIDNamePair<string, string> a in systemAll)
            {
                items.Add(new SelectListItem { Text = a.GetName(), Value = a.GetID() });
            }
            return items;
        }

        protected static IEnumerable<SelectListItem> SystemSelectList(string category)
        {
            return SystemSelectList(category, BaseViewModel.UnitOfWork);
        }

        #endregion System

        #endregion Static
    }
}