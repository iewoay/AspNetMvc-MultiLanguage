using System;
using System.Web;

namespace WebAppDemo.Common
{
    /// <summary>
    /// Cookie
    /// </summary>
    public class CookieHelper
    {
        private const string LANGUAGE = "LANGUAGE";

        /// <summary>
        /// 设置系统语言
        /// </summary>
        /// <param name="response"></param>
        /// <param name="lang"></param>
        public static void SetLanguage(string lang)
        {
            AddCookie(lang, LANGUAGE, 365);
        }

        /// <summary>
        /// 获取系统语言
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetLanguage()
        {
            return GetCookie(LANGUAGE);
        }

        public static void AddCookie(string value, string cookieName, int days = 1)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie==null)
            {
                cookie = new HttpCookie(cookieName);
            }
            cookie.Value = value;
            cookie.Expires.AddDays(days);
            cookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void RemoveCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Values.Clear();
                cookie.Expires = DateTime.Now.AddYears(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }

        }

        public static string GetCookie(string cookieName)
        {
            string value = string.Empty;
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                value = cookie.Value;
            }
            return value;
        }

    }
}
