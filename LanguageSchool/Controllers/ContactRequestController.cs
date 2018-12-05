using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;

namespace LanguageSchool.Controllers
{
    public class ContactRequestController : Controller
    {
        private LanguageSchoolEntities db = new LanguageSchoolEntities();

        // GET: ContactRequest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactRequest/Create
        [HttpPost]
        public ActionResult Create(ContactRequestViewModel crvm)
        {
            try
            {
                ContactRequest cr = new ContactRequest();

                cr.PhoneNumber = crvm.PhoneNumber;
                cr.EmailAdress = crvm.EmailAdress;
                cr.Comment = crvm.Comment;
                cr.CourseId = crvm.CourseId == 0 ? 7 : crvm.CourseId;
                cr.EntryTestId = crvm.EntryTestId;
                cr.Points = crvm.Points;

                cr.IsAwaiting = true;
                cr.CreationDate = DateTime.Now;

                db.ContactRequests.Add(cr);
                db.SaveChanges();

                return RedirectToAction("Index", "Course");
            }
            catch
            {
                return View();
            }
        }
    }
}
