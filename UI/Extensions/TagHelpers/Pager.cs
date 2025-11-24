using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UI.Extensions.TagHelpers;

[HtmlTargetElement("pager")]
public class Pager(LinkGenerator linkGenerator) : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        throw new NotImplementedException();
    }
}