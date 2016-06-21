using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MvcProject.Models;

namespace MvcProject.Infrastructure.Helpers
{
    public static class RatingHelpers
    {
        public static MvcHtmlString UsersWhoRatedMenu(this HtmlHelper html, IEnumerable<RatingViewModel> ratings, Func<int, string> userUrl)
        {
            TagBuilder thumbsUp = new TagBuilder("span");
            thumbsUp.AddCssClass("glyphicon glyphicon-thumbs-up");

            StringBuilder result = new StringBuilder();

            if (ratings == null || ratings.Count() == 0)
            {
                result.Append(UserWhoRated(0, "No ratings"));
            }
            else
            {
                int otherUsersRatings = 0;
                foreach (var rating in ratings)
                {
                    if (rating.User != null)
                    {
                        TagBuilder userFullName = new TagBuilder("a");
                        userFullName.MergeAttribute("href", userUrl(rating.User.Id));
                        userFullName.InnerHtml = $"{rating.User.FirstName} {rating.User.LastName}";

                        result.Append(UserWhoRated(rating.UserRate, userFullName.ToString()));
                    }
                    else
                    {
                        otherUsersRatings += rating.UserRate;
                    }
                }

                if (otherUsersRatings != 0)
                {
                    result.Append(UserWhoRated(otherUsersRatings, "Other users"));
                }
            }

            TagBuilder ul = new TagBuilder("ul");
            ul.InnerHtml = result.ToString();

            TagBuilder topLi = new TagBuilder("li");
            topLi.InnerHtml = $"{thumbsUp} Who rated {ul}";

            TagBuilder topUl = new TagBuilder("ul");
            topUl.AddCssClass("custom-menu");
            topUl.InnerHtml = topLi.ToString();

            return MvcHtmlString.Create(topUl.ToString());
        }

        public static MvcHtmlString StarsMenu(this HtmlHelper html, int itemsCount, string updateTargetId, string loadingElementId, Func<int, string> rateUrl)
        {
            TagBuilder star = new TagBuilder("span");
            star.AddCssClass("glyphicon glyphicon-star");

            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= itemsCount; i++)
            {
                TagBuilder li = new TagBuilder("li");
                li.InnerHtml = RepeatLinkedStars(i, updateTargetId, loadingElementId, rateUrl);
                result.Append(li);
            }

            TagBuilder ul = new TagBuilder("ul");
            ul.InnerHtml = result.ToString();

            TagBuilder topLi = new TagBuilder("li");
            topLi.InnerHtml = $"{star} Rate this photo {ul}";

            TagBuilder topUl = new TagBuilder("ul");
            topUl.AddCssClass("custom-menu");
            topUl.InnerHtml = topLi.ToString();

            return MvcHtmlString.Create(topUl.ToString());
        }

        private static string UserWhoRated(int rating, string userName)
        {
            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("user-who-rated");

            TagBuilder star = new TagBuilder("span");
            star.AddCssClass("glyphicon glyphicon-star");

            div.InnerHtml = $"{star} {rating} {userName}";
            return div.ToString();
        }

        private static string RepeatLinkedStars(int count, string updateTargetId, string loadingElementId, Func<int, string> rateUrl)
        {
            StringBuilder stars = new StringBuilder();
            TagBuilder linkedStars = new TagBuilder("a");
            linkedStars.MergeAttribute("href", rateUrl(count));
            linkedStars.MergeAttribute("data-ajax", "true");
            linkedStars.MergeAttribute("data-ajax-update", "#" + updateTargetId);
            linkedStars.MergeAttribute("data-ajax-loading", "#" + loadingElementId);
            linkedStars.MergeAttribute("data-ajax-mode", "replace");

            for (int i = 0; i < count; i++)
            {
                TagBuilder emptyStar = new TagBuilder("span");
                emptyStar.AddCssClass("glyphicon glyphicon-star-empty");
                stars.Append(emptyStar);
                stars.Append(" ");
            }

            linkedStars.InnerHtml = stars.ToString();
            return linkedStars.ToString();
        }

    }
}