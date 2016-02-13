using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.AspNet.Razor.TagHelpers;
using Microsoft.AspNet.Mvc.Rendering;

namespace SimplyCore.TagHelpers
{

    public class SimplyCoreTagHelper : TagHelper
    {
        public string Text { get; set; }

        private const string hrefAttributeName = "href";

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var path = ViewContext.HttpContext.Request.Path;
            output.TagName = null;
            output.TagMode = TagMode.SelfClosing;
            output.Content.AppendHtml($"<a href=\"{path}!SimplyCore\">{Text}</a> ");
        }
    }
}
