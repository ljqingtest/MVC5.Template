﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Template.Resources;

namespace Template.Components.Extensions.Html
{
    public static class FormExtensions
    {
        public const String LabelClass = "control-label col-sm-3 col-md-3 col-lg-2";

        public static FormColumn FormColumn(this HtmlHelper html)
        {
            return new FormColumn(html.ViewContext.Writer);
        }
        public static FormGroup FormGroup(this HtmlHelper html)
        {
            return new FormGroup(html.ViewContext.Writer);
        }
        public static InputGroup InputGroup(this HtmlHelper html)
        {
            return new InputGroup(html.ViewContext.Writer);
        }
        public static FormActions FormActions(this HtmlHelper html)
        {
            return new FormActions(html.ViewContext.Writer);
        }
        public static MvcHtmlString FormSubmit(this HtmlHelper html)
        {
            var submit = new TagBuilder("input");
            submit.AddCssClass("btn btn-primary");
            submit.MergeAttribute("type", "submit");
            submit.MergeAttribute("value", Resources.Shared.Resources.Submit);

            return new MvcHtmlString(submit.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString BootstrapLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            TagBuilder label = new TagBuilder("label");
            label.MergeAttribute("for", TagBuilder.CreateSanitizedId(ExpressionHelper.GetExpressionText(expression)));
            label.InnerHtml = ResourceProvider.GetPropertyTitle(expression);
            label.AddCssClass(LabelClass);
            
            return new MvcHtmlString(label.ToString());
        }
        public static MvcHtmlString BootstrapRequiredLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            TagBuilder label = new TagBuilder("label");
            label.MergeAttribute("for", TagBuilder.CreateSanitizedId(ExpressionHelper.GetExpressionText(expression)));
            label.InnerHtml = ResourceProvider.GetPropertyTitle(expression);
            label.AddCssClass(LabelClass);

            TagBuilder requiredSpan = new TagBuilder("span");
            requiredSpan.AddCssClass("required");
            requiredSpan.InnerHtml = " *";

            label.InnerHtml += requiredSpan.ToString();

            return new MvcHtmlString(label.ToString());
        }
        public static MvcHtmlString BootstrapFormLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            if (expression.IsRequired())
                return html.BootstrapRequiredLabelFor(expression);

            return html.BootstrapLabelFor(expression);
        }
        public static MvcHtmlString BootstrapTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, String format = null, Object htmlAttributes = null)
        {
            var attributes = AddClass(htmlAttributes, "form-control");
            if (!attributes.ContainsKey("autocomplete")) attributes.Add("autocomplete", "off");
            return new MvcHtmlString(Wrap(html.TextBoxFor(expression, format, attributes)));
        }
        public static MvcHtmlString BootstrapDatePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            String format = String.Format("{{0:{0}}}", CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
            return html.BootstrapTextBoxFor(expression, format, new { @class = "datepicker" });
        }
        public static MvcHtmlString BootstrapPasswordFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return new MvcHtmlString(Wrap(html.PasswordFor(expression, AddClass(null, "form-control"))));
        }
        public static MvcHtmlString BootstrapValidationFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            return new MvcHtmlString(Wrap(html.ValidationMessageFor(expression)));
        }

        private static String Wrap(Object innerHtml)
        {
            return new FormColumn(innerHtml).ToString();
        }
        private static RouteValueDictionary AddClass(Object attributes, String value)
        {
            var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(attributes);
            htmlAttributes["class"] = String.Format("{0} {1}", htmlAttributes["class"], value).Trim();

            return htmlAttributes;
        }
        private static Boolean IsRequired<T, V>(this Expression<Func<T, V>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression must be a member expression");

            return memberExpression.Member.GetCustomAttribute<RequiredAttribute>() != null;
        }
    }
}