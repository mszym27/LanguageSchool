using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Microsoft.AspNet.Identity;

using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;

namespace LanguageSchool.Controllers
{
    [Authorize]
    public class MessageController : LanguageSchoolController
    {
        public ActionResult Index(string sortColumn = "sentDate", string sortDirection = "desc", int page = 1)
        {
            var userId = Int32.Parse(User.Identity.GetUserId());

            var userMessages = unitOfWork.UserMessageRepository.Get(um => (um.UserId == userId && !um.IsDeleted));

            if (page == 1)
            {
                sortDirection = (sortDirection == "desc") ? "asc" : "desc";
            }

            userMessages = this.Sort(userMessages, sortColumn, sortDirection);

            ViewBag.sortColumn = sortColumn;
            ViewBag.sortDirection = sortDirection;
            ViewBag.page = page;

            var userMessagesViewModels = new List<UserMessageViewModel>();

            foreach (UserMessage um in userMessages)
            {
                userMessagesViewModels.Add(new UserMessageViewModel(um));
            }

            return View(userMessagesViewModels.ToPagedList(page, 20));
        }

        [HttpGet]
        [Route("Message/{userMessageId}")]
        public ActionResult Details(int? userMessageId)
        {
            int userId = Int32.Parse(User.Identity.GetUserId());

            var loggedUser = unitOfWork.UserRepository.GetById(userId);

            var userMessage = loggedUser.UsersMessages.Where(um => um.Id == userMessageId).FirstOrDefault();

            if (userMessage == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!userMessage.HasBeenReceived)
            {
                userMessage.HasBeenReceived = true;
                userMessage.ReceivedDate = DateTime.Today;
                unitOfWork.UserMessageRepository.Update(userMessage);
                unitOfWork.Save();
            }

            var userMessageViewModel = new UserMessageViewModel(userMessage);

            return View(userMessageViewModel);
        }

        [HttpPost]
        [Route("Message/{userMessageId}")]
        public ActionResult Details(int userMessageId)
        {
            var userMessage = unitOfWork.UserMessageRepository.GetById(userMessageId);

            userMessage.IsDeleted = true;
            unitOfWork.UserMessageRepository.Update(userMessage);
            unitOfWork.Save();

            return RedirectToAction("Index");
        }

        //// GET: Message
        ////[Authorize(Roles = "Secretary, Administrator")]
        //// GET: Message/Create
        //[Route("Message/Create/")]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        [Authorize(Roles = "Secretary")]
        [HttpGet]
        [Route("Message/Create")]
        public ActionResult Create()
        {
            UserMessageViewModel userMessage = new UserMessageViewModel();

            return View(userMessage);
        }

        [Authorize(Roles = "Secretary")]
        [HttpPost]
        [Route("Message/Create")]
        public ActionResult Create(UserMessageViewModel userMessage)
        {
            //try
            //{
            //    ContactRequest cr = new ContactRequest();

            //    cr.PhoneNumber = crvm.PhoneNumber;
            //    cr.EmailAdress = crvm.EmailAdress;
            //    cr.Comment = crvm.Comment;
            //    cr.PreferredHours = crvm.PreferredHours;
            //    cr.CourseId = crvm.CourseId;
            //    cr.EntryTestId = crvm.EntryTestId;
            //    cr.Points = crvm.Points;

            //    cr.IsAwaiting = true;
            //    cr.CreationDate = DateTime.Now;

            //    unitOfWork.ContactRequestRepository.Insert(cr);
            //    unitOfWork.Save();

            //    TempData["Alert"] = new AlertViewModel()
            //    {
            //        Title = "Wysłano pomyślnie",
            //        Message = "proszę czekać aż jeden z naszych pracowników odpowie na prośbę o kontakt",
            //        AlertColor = "green"
            //    };

            //    return RedirectToAction("Index", "Course");
            //}
            //catch
            //{
            //    return View();
            //}
            return View();
        }

        private IEnumerable<UserMessage> Sort(IEnumerable<UserMessage> userMessages, string sortColumn, string sortDirection)
        {
            switch (sortColumn)
            {
                case "SentDate":
                    if (sortDirection == "asc")
                        userMessages = userMessages.OrderBy(um => um.CreationDate);
                    else
                        userMessages = userMessages.OrderByDescending(um => um.CreationDate);
                    break;
                case "ReceivedDate":
                    if (sortDirection == "asc")
                        userMessages = userMessages.OrderBy(um => um.ReceivedDate);
                    else
                        userMessages = userMessages.OrderByDescending(um => um.ReceivedDate);
                    break;
                case "Topic":
                    if (sortDirection == "asc")
                        userMessages = userMessages.OrderBy(um => um.Message.Header);
                    else
                        userMessages = userMessages.OrderByDescending(um => um.Message.Header);
                    break;
            }

            return userMessages;
        }

        //// GET: Message
        //[Authorize(Roles = "Secretary, Administrator")]
        //// POST: Message/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Header,Contents")] Message message)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Messages.Add(message);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(message);
        //}

        //// GET: Message/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Message message = db.Messages.Find(id);
        //    if (message == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(message);
        //}

        //// POST: Message/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Header,Contents")] Message message)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(message).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(message);
        //}

        //// GET: Message/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Message message = db.Messages.Find(id);
        //    if (message == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(message);
        //}
    }
}
