using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace LanguageSchool.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var role = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value).FirstOrDefault();

            switch (role)
            {
                case ("Admin"): return RedirectToAction("FullList", "Course");
                case ("Secretary"): return RedirectToAction("List", "Course");
                //case ("Teacher"): return RedirectToAction("Timetable", "Report");
                //case ("Student"): return RedirectToAction("Timetable", "Report");
                default: return View();
            }
        }
    }
}
