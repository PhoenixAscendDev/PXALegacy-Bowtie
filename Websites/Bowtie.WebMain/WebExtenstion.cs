using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace JB2.Bowtie.Web
{
    public static class WebExtenstion
    {
        public static SelectListItem ToSelectItem(this JB2.Common.IIDNamePair<string, string> idnamePair)
        {
            if (idnamePair != null)
            {
                SelectListItem item = new SelectListItem();
                item.Value = idnamePair.ID;
                item.Text = idnamePair.Name;
                return item;
            }
            else
                return new SelectListItem();

        }

        public static IEnumerable<SelectListItem> ToSelectItems(this IEnumerable<JB2.Common.IIDNamePair<string, string>> idnamePair)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach(var i in idnamePair)
            {
                list.Add(i.ToSelectItem());
            }
            return list;
        }

    }
}