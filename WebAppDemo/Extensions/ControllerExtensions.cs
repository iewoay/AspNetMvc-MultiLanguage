using MultiLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppDemo.Common;

namespace System.Web.Mvc
{
    /// <summary>
    /// Controller扩展类
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// 获取控制器绑定的多语言内容
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string GetLocale(this Controller controller, string target)
        {
            var lang = CookieHelper.GetLanguage() ?? SessionHelper.GetLanguage();
            if (string.IsNullOrEmpty(lang))
            {
                return target;
            }
            var route = controller.RouteData;
            var area = route.DataTokens["area"];
            var areaName = area == null ? "" : $"{area}.".ToString().ToLower();
            var bindingName = $"{areaName}{route.Values["controller"].ToString().ToLower()}";
            var result = LanguageContext.GetContent(target, lang, bindingName);
            return result;
        }
    }

    /// <summary>
    /// HtmlHelper辅助方法扩展类
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// 本地化目标内容
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static MvcHtmlString Locale(this HtmlHelper helper, string target)
        {
            var controller = helper.ViewContext.Controller as Controller;
            var value = controller.GetLocale(target);
            return MvcHtmlString.Create(value);
        }
    }

}