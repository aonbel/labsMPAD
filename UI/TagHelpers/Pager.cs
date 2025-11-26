using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UI.Extensions.TagHelpers;

[HtmlTargetElement("pager", Attributes = "current-page, total-pages, game-genre-id")]
public class Pager(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator) : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        context.AllAttributes.TryGetAttribute("current-page", out var currentPageAttribute);
        context.AllAttributes.TryGetAttribute("total-pages", out var totalPagesAttribute);
        context.AllAttributes.TryGetAttribute("game-genre-id", out var gameGenreIdAttribute);
        
        var currentPage = Convert.ToInt32(currentPageAttribute.Value.ToString());
        var totalPages = Convert.ToInt32(totalPagesAttribute.Value.ToString());
        var selectedGameGenreId = gameGenreIdAttribute.Value.ToString();
        
        output.TagName = "nav";
        output.Attributes.Clear();
        output.Attributes.SetAttribute("id", "pager-nav");

        var ul = new TagBuilder("ul");
        ul.AddCssClass("pagination");
        
        var prevLi = new TagBuilder("li");
        prevLi.AddCssClass("page-item");
        
        var prevLink = new TagBuilder("a");
        prevLink.AddCssClass("page-link");
        if (currentPage == 1)
        {
            prevLink.AddCssClass("disabled");
        }
        prevLink.MergeAttribute("href", linkGenerator.GetPathByAction(httpContextAccessor.HttpContext, "Index", "Games", new {pageNumber=currentPage - 1, gameGenreId=selectedGameGenreId}));
        
        var prevSpan = new TagBuilder("span");
        prevSpan.MergeAttribute("aria-hidden", "true");
        prevSpan.InnerHtml.Append("«");
        
        prevLink.InnerHtml.AppendHtml(prevSpan);
        prevLi.InnerHtml.AppendHtml(prevLink);
        ul.InnerHtml.AppendHtml(prevLi);

        for (var pageNum = 1; pageNum <= totalPages; pageNum++)
        {
            var pageLi = new TagBuilder("li");
            pageLi.AddCssClass("page-item");
        
            var pageLink = new TagBuilder("a");
            pageLink.AddCssClass("page-link");
            if (currentPage == pageNum)
            {
                pageLink.AddCssClass("disabled");
            }
            pageLink.MergeAttribute("href", linkGenerator.GetPathByAction(httpContextAccessor.HttpContext, "Index", "Games", new {pageNumber=pageNum, gameGenreId=selectedGameGenreId}));
        
            var pageSpan = new TagBuilder("span");
            pageSpan.MergeAttribute("aria-hidden", "true");
            pageSpan.InnerHtml.Append($"{pageNum}");
        
            pageLink.InnerHtml.AppendHtml(pageSpan);
            pageLi.InnerHtml.AppendHtml(pageLink);
            ul.InnerHtml.AppendHtml(pageLi);
        }
        
        var nextLi = new TagBuilder("li");
        nextLi.AddCssClass("page-item");
        
        var nextLink = new TagBuilder("a");
        nextLink.AddCssClass("page-link");
        if (currentPage == totalPages)
        {
            nextLink.AddCssClass("disabled");
        }
        nextLink.MergeAttribute("href", linkGenerator.GetPathByAction(httpContextAccessor.HttpContext, "Index", "Games", new {pageNumber=currentPage+1, gameGenreId=selectedGameGenreId}));
        
        var nextSpan = new TagBuilder("span");
        nextSpan.MergeAttribute("aria-hidden", "true");
        nextSpan.InnerHtml.Append("»");
        
        nextLink.InnerHtml.AppendHtml(nextSpan);
        nextLi.InnerHtml.AppendHtml(nextLink);
        ul.InnerHtml.AppendHtml(nextLi);
        
        output.Content.AppendHtml(ul);
    }
}