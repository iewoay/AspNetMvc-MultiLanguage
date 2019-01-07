using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebAppDemo.Common
{
    /// <summary>
    /// Session
    /// </summary>
    public class SessionHelper
    {
        private const string LANGUAGE = "LANGUAGE";

        /// <summary>
        /// 写入session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Save(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        /// <summary>
        /// 取出Session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return HttpContext.Current.Session[key];
        }
        
        /// <summary>
        /// 保存系统语言
        /// </summary>
        /// <param name="session"></param>
        /// <param name="text"></param>
        public static void SaveLanguage(string text)
        {
            Save(LANGUAGE, text);
        }

        /// <summary>
        /// 获取系统语言
        /// </summary>
        /// <param name="session"></param>
        /// <param name="text"></param>
        public static string GetLanguage()
        {
            return Get(LANGUAGE)?.ToString();
        }
    }
}
