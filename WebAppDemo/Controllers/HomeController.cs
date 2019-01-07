using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppDemo.Common;

namespace WebAppDemo.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index(string lang = "zh_cn")
        {
            CookieHelper.SetLanguage(lang);
            SessionHelper.SaveLanguage(lang);
            ViewBag.Search =Locale("搜索");
            ViewBag.Choose = Locale("请选择");
            ViewBag.HasLanguage = HasLanguagePack;
            ViewBag.Language = lang;
            return View();
        }
    }
}