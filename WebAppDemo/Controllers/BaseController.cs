using MultiLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppDemo.Controllers
{
    public class BaseController : Controller
    {

        /// <summary>
        /// 是否有多语言包
        /// </summary>
        protected bool HasLanguagePack
        {
            get
            {
                return LanguageContext.HasPack;
            }
        }

        /// <summary>
        /// 本地化目标内容
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string Locale(string target)
        {
            return this.GetLocale(target);
        }
    }
}