using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;
using LanguageSchool.Models.ViewModels.ContactRequestViewModels;

namespace LanguageSchool.Controllers
{
    public class ContactRequestController : LanguageSchoolController
    {
        // GET: ContactRequest/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = UnitOfWork.CourseRepository.GetById(id);

            if (course == null || course.IsDeleted || !course.IsActive)
            {
                return HttpNotFound();
            }

            var contactRequestVM = new ContactRequestInputVM();
            contactRequestVM.CourseId = (int)id;

            return View(contactRequestVM);
        }

        // POST: ContactRequest/Create
        [HttpPost]
        public ActionResult Create(ContactRequestInputVM contactRequestVM)
        {
            if (!ModelState.IsValid)
            {
                return View(contactRequestVM);
            }

            try
            {
                ContactRequest contactRequest = new ContactRequest();

                contactRequest.Name = contactRequestVM.Name;
                contactRequest.Surname = contactRequestVM.Surname;
                contactRequest.PhoneNumber = contactRequestVM.PhoneNumber;
                contactRequest.EmailAdress = contactRequestVM.EmailAdress;
                contactRequest.Comment = contactRequestVM.Comment;
                contactRequest.PreferredHoursFrom = contactRequestVM.PreferredHoursFrom;
                contactRequest.PreferredHoursTo = contactRequestVM.PreferredHoursTo;
                contactRequest.CourseId = contactRequestVM.CourseId;

                contactRequest.IsAwaiting = true;

                UnitOfWork.ContactRequestRepository.Insert(contactRequest);
                UnitOfWork.Save();

                TempData["Alert"] = new AlertViewModel(Consts.Success, "Wysłano pomyślnie", "proszę czekać aż jeden z naszych pracowników odpowie na prośbę o kontakt");

                return RedirectToAction("Index", "Course");
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return View(contactRequestVM);
            }
        }

        [Route("ContactRequest/{id}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var contactRequest = UnitOfWork.ContactRequestRepository.GetById(id);

            if (contactRequest == null)
            {
                return HttpNotFound();
            }

            var contactRequestVM = new ContactRequestsDetailsVM(contactRequest);

            return View(contactRequestVM);
        }

        [HttpGet]
        [Route("ContactRequest/Edit/{id}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var contactRequest = UnitOfWork.ContactRequestRepository.GetById(id);

            if (contactRequest == null)
            {
                return HttpNotFound();
            }

            var contactRequestVM = new ContactRequestInputVM(contactRequest);

            return View(contactRequestVM);
        }

        [HttpPost]
        [Route("ContactRequest/Edit/{id}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult Edit(ContactRequestInputVM contactRequestVM)
        {
            if (!ModelState.IsValid)
            {
                return View(contactRequestVM);
            }

            try
            {
                var contactRequest = UnitOfWork.ContactRequestRepository.GetById(contactRequestVM.ContactRequestId);

                contactRequest.Name = contactRequestVM.Name;
                contactRequest.Surname = contactRequestVM.Surname;
                contactRequest.PhoneNumber = contactRequestVM.PhoneNumber;
                contactRequest.EmailAdress = contactRequestVM.EmailAdress;
                contactRequest.Comment = contactRequestVM.Comment;
                contactRequest.PreferredHoursFrom = contactRequestVM.PreferredHoursFrom;
                contactRequest.PreferredHoursTo = contactRequestVM.PreferredHoursTo;
                contactRequest.IsAwaiting = contactRequestVM.IsAwaiting;

                contactRequest.ModificationDate = DateTime.Now;

                UnitOfWork.ContactRequestRepository.Update(contactRequest);
                UnitOfWork.Save();

                return RedirectToAction("Details", new { id = contactRequestVM.ContactRequestId });
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return View(contactRequestVM);
            }
        }
    }
}
