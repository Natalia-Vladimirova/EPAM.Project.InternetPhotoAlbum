using System;
using System.Text;
using System.Web.Mvc;
using MvcProject.Models;

namespace MvcProject.Infrastructure.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pageInfo, Func<int, string> pageUrl)
        {
            if (pageInfo == null || pageUrl == null) { return null; }

            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder btn = new TagBuilder("a");
                btn.MergeAttribute("href", pageUrl(i));
                btn.InnerHtml = i.ToString();

                if (i == pageInfo.PageNumber)
                {
                    btn.AddCssClass("btn btn-gray");
                }
                btn.AddCssClass("btn btn-default");

                result.Append(btn.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}