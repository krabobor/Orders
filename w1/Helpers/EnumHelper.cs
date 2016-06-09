using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace w1.Helpers
{
    public static class EnumHelper
    {
        public static IHtmlString DisplayEnumFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> ex, Type enumType)
        {
            var value = ModelMetadata.FromLambdaExpression(ex, html.ViewData).Model;
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes == null) return new HtmlString(string.Empty);
            return (descriptionAttributes.Length > 0) ? new HtmlString(descriptionAttributes[0].Name) : new HtmlString(value.ToString());
        }
    }
}