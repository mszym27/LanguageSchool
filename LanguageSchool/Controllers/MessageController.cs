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
using LanguageSchool.DAL;

namespace LanguageSchool.Controllers
{
    [Authorize]
    public class MessageController : LanguageSchoolController
    {
        public ActionResult Index(string sortColumn = "sentDate", string sortDirection = "desc", int page = 1)
        {
            var loggedUser = GetLoggedUser();

            var userMessages = UnitOfWork.UserMessageRepository.Get(um => (um.UserId == loggedUser.Id && !um.IsDeleted)
                , orderBy: q => q.OrderByDescending(d => d.Message.CreationDate)
            );

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
            var userMessage = GetLoggedUser().UsersMessages.Where(um => um.Id == userMessageId).FirstOrDefault();

            if (userMessage == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!userMessage.HasBeenReceived)
            {
                userMessage.HasBeenReceived = true;
                userMessage.ReceivedDate = DateTime.Today;
                UnitOfWork.UserMessageRepository.Update(userMessage);
                UnitOfWork.Save();
            }

            var userMessageViewModel = new UserMessageViewModel(userMessage);

            return View(userMessageViewModel);
        }

        [HttpPost]
        [Route("Message/{userMessageId}")]
        public ActionResult Details(int userMessageId)
        {
            var userMessage = UnitOfWork.UserMessageRepository.GetById(userMessageId);

            userMessage.IsDeleted = true;
            UnitOfWork.UserMessageRepository.Update(userMessage);
            UnitOfWork.Save();

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
        [Route("Message/Create/")]
        public ActionResult Create(int? userId)
        {
            UserMessageViewModel userMessageViewModel = new UserMessageViewModel();

            userMessageViewModel.MessageTypes = new SelectList(UnitOfWork.MessageTypeRepository.Get(),
                                         "Id",
                                         "Name");

            SelectList usersSelectList;

            if (userId != null)
            {
                var selectedUser = UnitOfWork.UserRepository.GetById(userId);
                usersSelectList = PopulateList.AllUsers(selectedUser);
            }
            else
            {
                usersSelectList = PopulateList.AllUsers();
            }

            userMessageViewModel.Users = usersSelectList;

            userMessageViewModel.Groups = new SelectList(UnitOfWork.GroupRepository.Get(g => !g.IsDeleted),
                                         "Id",
                                         "Name");

            userMessageViewModel.Courses = new SelectList(UnitOfWork.CourseRepository.Get(c => !c.IsDeleted),
                                         "Id",
                                         "Name");

            userMessageViewModel.Roles = new SelectList(UnitOfWork.RoleRepository.Get(),
                                         "Id",
                                         "PLName");

            return View(userMessageViewModel);
        }

        [Authorize(Roles = "Secretary")]
        [HttpPost]
        [Route("Message/Create")]
        public ActionResult Create(UserMessageViewModel uMVM)
        {
            try
            {
                Message message = new Message();

                message.Header = uMVM.Topic;
                message.Contents = uMVM.Contents;
                message.MessageType = UnitOfWork.MessageTypeRepository.GetById(uMVM.MessageTypeId);
                message.CreationDate = DateTime.Now;
                message.IsSystem = uMVM.IsSystem;
                message.UsersMessages = new List<UserMessage>();

                UserMessage userMessage;

                switch (uMVM.MessageTypeId)
                {
                    case (int)Consts.MessageTypes.ToUser:
                        message.UserId = uMVM.UserId;

                        userMessage = new UserMessage() { UserId = uMVM.UserId };

                        message.UsersMessages.Add(userMessage);

                        break;
                    case (int)Consts.MessageTypes.ToGroup:
                        message.GroupId = uMVM.GroupId;

                        foreach (User u in UnitOfWork.UserRepository.Get(u => (u.UsersGroups.Where(g => g.GroupId == uMVM.GroupId).Any() && !u.IsDeleted)))
                        {
                            userMessage = new UserMessage() { User = u };

                            message.UsersMessages.Add(userMessage);
                        }

                        break;
                    case (int)Consts.MessageTypes.ToCourse:
                        message.CourseId = uMVM.CourseId;

                        foreach (User u in UnitOfWork.UserRepository.Get(u => (u.UsersGroups.Where(g => g.Group.CourseId == uMVM.CourseId).Any() && !u.IsDeleted)))
                        {
                            userMessage = new UserMessage() { User = u };

                            message.UsersMessages.Add(userMessage);
                        }

                        break;
                    case (int)Consts.MessageTypes.ToRole:
                        message.RoleId = uMVM.RoleId;

                        foreach (User u in UnitOfWork.UserRepository.Get(u => (u.RoleId == uMVM.RoleId && !u.IsDeleted)))
                        {
                            userMessage = new UserMessage() { User = u };

                            message.UsersMessages.Add(userMessage);
                        }

                        break;
                    case (int)Consts.MessageTypes.ToAll:

                        foreach(User u in UnitOfWork.UserRepository.Get(u => !u.IsDeleted))
                        {
                            userMessage = new UserMessage() { User = u };

                            message.UsersMessages.Add(userMessage);
                        }

                        break;
                }

                UnitOfWork.MessageRepository.Insert(message);
                UnitOfWork.Save();

                TempData["Alert"] = new AlertViewModel(Consts.Success, "Wiadomość wysłana pomyślnie", "proszę czekać na ewentualny kontakt ze strony odbiorcy/ów");

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        private IEnumerable<UserMessage> Sort(IEnumerable<UserMessage> userMessages, string sortColumn, string sortDirection)
        {
            switch (sortColumn)
            {
                case "SentDate":
                    if (sortDirection == "asc")
                        userMessages = userMessages.OrderBy(um => um.Message.CreationDate);
                    else
                        userMessages = userMessages.OrderByDescending(um => um.Message.CreationDate);
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
