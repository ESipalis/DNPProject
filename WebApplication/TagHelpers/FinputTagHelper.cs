using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApplication.TagHelpers
{
    public class FinputTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")] public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            output.Content.SetContent("");
        }
    }
}