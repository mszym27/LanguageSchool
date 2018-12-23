using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace LanguageSchool.Controllers
{
    public class HomeController : LanguageSchoolController
    {
        // GET: Home
        public ActionResult Index()
        {
            int userId;
                
            Int32.TryParse(User.Identity.GetUserId(), out userId);

            if(userId == 0)
            {
                return View();
            }
            else
            {
                var loggedUser = unitOfWork.UserRepository.GetById(userId);

                if (loggedUser.UsersMessages.Where(um => (um.HasBeenReceived == false)).Any())
                    return this.RedirectToAction("Index", "Message");

                switch (loggedUser.Role.Name)
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
}
