﻿using System;
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
        public ActionResult Index()
        {
            var loggedUser = GetLoggedUser();

            if (loggedUser == null)
            {
                return RedirectToAction("Default");
            }
            else
            {
                if (loggedUser.UsersMessages.Where(um => (um.HasBeenReceived == false)).Any())
                    return this.RedirectToAction("Index", "Message");

                switch (loggedUser.Role.Id)
                {
                    case ((int)Consts.Roles.Secretary): return RedirectToAction("List", "Course");
                    case ((int)Consts.Roles.Teacher): return RedirectToAction("Timetable", "User");
                    case ((int)Consts.Roles.Student): return RedirectToAction("Timetable", "User");
                    default: return RedirectToAction("Default");
                }
            }
        }

        public ActionResult Default()
        {
            return View();
        }
    }
}
