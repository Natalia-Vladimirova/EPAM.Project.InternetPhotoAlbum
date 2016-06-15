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
        public static MvcHtmlString UsersWhoRated(this HtmlHelper html, IEnumerable<RatingViewModel> ratings, Func<int, string> userUrl)
        {
            if (ratings == null || ratings.Count() == 0)
            {
                return MvcHtmlString.Create(UserWhoRated(0, "No ratings ..."));
            }

            int otherUsersRatings = 0;
            StringBuilder result = new StringBuilder();
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
            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString StarsMenu(this HtmlHelper html, int itemsCount, Func<int, string> rateUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= itemsCount; i++)
            {
                TagBuilder div = new TagBuilder("div");
                div.AddCssClass("col-md-12");
                div.InnerHtml = RepeatLinkedStars(i, rateUrl);
                result.Append(div);
            }
            return MvcHtmlString.Create(result.ToString());
        }

        private static string UserWhoRated(int rating, string userName)
        {
            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("col-md-12 user-who-rated");

            TagBuilder star = new TagBuilder("span");
            star.AddCssClass("glyphicon glyphicon-star");

            div.InnerHtml = $"{star} {rating} {userName}";
            return div.ToString();
        }

        private static string RepeatLinkedStars(int count, Func<int, string> rateUrl)
        {
            StringBuilder stars = new StringBuilder();
            TagBuilder linkedStars = new TagBuilder("a");
            linkedStars.MergeAttribute("href", rateUrl(count));

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