using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using System.Text;

using JB2.Common;
using JB2.Bowtie;

namespace JB2.Bowtie.Web.Mvc
{
    public static class ActionLinks
    {
        public static MvcHtmlString ApplicationLink(this HtmlHelper htmlHelper, string id, IUnitOfWork unitOfWork = null, ActionType action = ActionType.Edit,IDictionary<string, object> htmlAttributes = null)
        {
            string actionName = "Edit";
            string controllerName = "Application";
            string linkText = string.Empty;

            RouteValueDictionary routeValues = new RouteValueDictionary();

            switch(action)
            {
                case ActionType.Edit:
                    actionName = "Edit";
                    break;
                case ActionType.Create:
                    actionName = "Create";
                    break;
            }

            

            if (!string.IsNullOrEmpty(id))
            {
                routeValues.Add("id", id);
                try
                {
                    if (unitOfWork == null)
                        unitOfWork = JB2.Bowtie.Web.Models.BaseViewModel.UnitOfWork;

                    var service = new Service.ApplicationService(unitOfWork);
                    var app = service.RetrieveById(id);
                    linkText = app.Name;
                }
                catch (Exception ex)
                {
                    ex.BowtieLog();
                }
                return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
            }
            else
                return new MvcHtmlString(null);




        }

        public static MvcHtmlString GraphActionLink(this HtmlHelper htmlHelper, string id, IUnitOfWork unitOfWork = null, ActionType action = ActionType.Edit, IDictionary<string, object> htmlAttributes = null)
        {
            return GraphLink<GraphAction>(htmlHelper, id, unitOfWork, action, htmlAttributes);
        }

        public static MvcHtmlString GraphStoryLink(this HtmlHelper htmlHelper, string id, IUnitOfWork unitOfWork = null, ActionType action = ActionType.Edit, IDictionary<string, object> htmlAttributes = null)
        {
            return GraphLink<GraphStory>(htmlHelper, id, unitOfWork, action, htmlAttributes);
        }

        public static MvcHtmlString GraphPropertyLink(this HtmlHelper htmlHelper, string id, IUnitOfWork unitOfWork = null, ActionType action = ActionType.Edit, IDictionary<string, object> htmlAttributes = null)
        {
            return GraphLink<GraphProperty>(htmlHelper, id, unitOfWork, action, htmlAttributes);
        }

        public static MvcHtmlString GraphObjectLink(this HtmlHelper htmlHelper, string id, IUnitOfWork unitOfWork = null, ActionType action = ActionType.Edit, IDictionary<string, object> htmlAttributes = null)
        {
            return GraphLink<GraphObject>(htmlHelper, id, unitOfWork, action, htmlAttributes);
        }

        public static MvcHtmlString GraphLink<TGraphElement>(this HtmlHelper htmlHelper, string id, IUnitOfWork unitOfWork = null, ActionType action = ActionType.Edit, IDictionary<string, object> htmlAttributes = null)
            where TGraphElement : IGraphElement, new()
        {
            

           
            IGraphElement element = null;
            string actionName = "Edit";
            string controllerName = "Graph";
            RouteValueDictionary routeValues = new RouteValueDictionary();
            string linkText = string.Empty;

            switch (action)
            {
                case ActionType.Edit:
                    actionName = "Edit";
                    break;
                case ActionType.Create:
                    actionName = "Create";
                    break;
            }


            if (!string.IsNullOrEmpty(id))
            {

                if (unitOfWork == null)
                    unitOfWork = JB2.Bowtie.Web.Models.BaseViewModel.UnitOfWork;
                var service = new Service.GraphService(unitOfWork);

                routeValues.Add("id", id);
                TGraphElement entity = new TGraphElement(); // typeof(T) would return the System.Type, not an instance!

                switch (entity.ElementType)
                {
                    case Enum.GraphElementType.Property:
                        element = service.RetrievePropertyByID(id);
                        break;
                    case Enum.GraphElementType.Object:
                        element = service.RetrieveObjectByID(id);
                        break;
                    case Enum.GraphElementType.Action:
                        element = service.RetrieveActionByID(id);
                        break;
                    case Enum.GraphElementType.Story:
                        element = service.RetrieveStoryByID(id);
                        break;
                }

                linkText = element.Name;
                return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
            }
            else
                return htmlHelper.ActionLink("None", actionName, controllerName, routeValues, htmlAttributes);


        }
    }
}