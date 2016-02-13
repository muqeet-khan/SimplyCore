using Microsoft.AspNet.Razor.TagHelpers;
using System;

namespace SimplyCore.TagHelpers
{
    public class ScIfTagHelper : TagHelper
    {
        public bool ConditionValue { get; set; }

        public override int Order => -1000;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            output.TagName = null;

            if (!ConditionValue)
            {
                output.SuppressOutput();
            }
        }
    }
}
