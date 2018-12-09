using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;

namespace LanguageSchool.Controllers
{
    public class ContactRequestController : LanguageSchoolController
    {
        // GET: ContactRequest/Create
        public ActionResult Create(int id)
        {
            ContactRequestViewModel crvm = new ContactRequestViewModel();

            crvm.CourseId = id;

            return View(crvm);
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
                cr.CourseId = crvm.CourseId;
                cr.EntryTestId = crvm.EntryTestId;
                cr.Points = crvm.Points;

                cr.IsAwaiting = true;
                cr.CreationDate = DateTime.Now;

                unitOfWork.ContactRequestRepository.Insert(cr);
                unitOfWork.Save();

                return RedirectToAction("Index", "Course");
            }
            catch
            {
                return View();
            }
        }
    }
}
