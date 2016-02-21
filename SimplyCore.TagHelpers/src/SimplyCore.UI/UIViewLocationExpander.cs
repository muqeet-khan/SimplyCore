using Microsoft.AspNet.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplyCore.UI
{
    public class UIViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {            
            yield return "~/Views/{1}/{0}.cshtml";
            yield return "~/Views/Shared/{0}.cshtml";
            
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}
