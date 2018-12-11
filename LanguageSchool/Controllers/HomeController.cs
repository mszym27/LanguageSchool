using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LanguageSchool.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            // w zaleznosci od tego czy uzytkownik jest zalogowany
            // rozne strony domowe - zalogowany powinien otrzymac liste kursow
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Course");

            return View();
        }
    }
}
