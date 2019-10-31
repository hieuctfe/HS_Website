namespace RealEstate.Infrastructure
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.Routing;

    public static class HtmlHelperExtension
    {
        public static MvcHtmlString BuildSortFieldFor<TModel, TProperty>(this HtmlHelper<IEnumerable<TModel>> html,
            Expression<Func<TModel, TProperty>> expression, string pattern, string sortField,
            string actionName, string controllerName, RouteValueDictionary routeValues,
            IDictionary<string, object> htmlAttributes)
        {
            string propertyName = (expression.Body as MemberExpression).ToString().SubAfter($".{pattern}.");
            routeValues["SortField"] = $"{propertyName}_{(sortField == $"{propertyName}_asc" ? "desc" : "asc")}";
            return html.ActionLink(WebUtility.HtmlDecode(html.DisplayNameFor(expression).ToString()), actionName, controllerName, routeValues, htmlAttributes);
        }

        public static string CombineQueryString(this HtmlHelper htmlHelper, string defaultQueryString = "", Dictionary<string, object> includeKeyValuePairs = null)
        {
            return includeKeyValuePairs?
                .Where(x => !string.IsNullOrEmpty(Convert.ToString(x.Value)))
                .Aggregate(defaultQueryString, (a, b) => $"{a}&{b.Key}={b.Value}", r => r.Substring(1));
        }

        public static string BuildQueryString(this HtmlHelper htmlHelper, RouteValueDictionary routeValues, params string[] excludeKeys)
        {
            excludeKeys.ForEach(x =>
            {
                if (routeValues.ContainsKey(x))
                    routeValues.Remove(x);
            });

            return routeValues.Where(x => !string.IsNullOrEmpty(Convert.ToString(x.Value)))
                .Aggregate(string.Empty, (previous, current) => $"{previous}&{current.Key}={current.Value}",
                result => result.Substring(1));
        }
    }
}