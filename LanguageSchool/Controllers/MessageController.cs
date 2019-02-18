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

using LanguageSchool.DAL;
using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;
using LanguageSchool.Models.ViewModels.MessageViewModels;

namespace LanguageSchool.Controllers
{
    [Authorize]
    public class MessageController : LanguageSchoolController
    {
        public ActionResult Index(string sortColumn = "sentDate", string sortDirection = "desc", int page = 1)
        {
            try
            {
                var loggedUser = GetLoggedUser();

                var userMessages = UnitOfWork.UserMessageRepository.Get(
                    um => (
                        !um.IsDeleted
                        && um.UserId == loggedUser.Id
                    )
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

                var userMessageShortDetailsVMs = userMessages
                    .Select(um => new UserMessageShortDetailsVM(um))
                    .ToPagedList(page, 20);

                return View(userMessageShortDetailsVMs);
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return RedirectToAction("Index", "Home");
            }
        }

        [Route("Message/{userMessageId}")]
        public ActionResult Details(int? userMessageId)
        {
            if (userMessageId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var userMessage = GetLoggedUser().UsersMessages
                    .Where(um => um.Id == userMessageId)
                    .FirstOrDefault();

                if (userMessage == null)
                {
                    return HttpNotFound();
                }

                if (!userMessage.HasBeenReceived)
                {
                    userMessage.HasBeenReceived = true;
                    userMessage.ReceivedDate = DateTime.Today;

                    UnitOfWork.UserMessageRepository.Update(userMessage);
                    UnitOfWork.Save();
                }

                var userMessageViewModel = new UserMessageDetailsVM(userMessage);

                return View(userMessageViewModel);
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [Route("Message/SendToUser/{userId}")]
        [Authorize(Roles = "Secretary, Teacher")]
        public ActionResult SendToUser(int? userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = UnitOfWork.UserRepository.GetById(userId);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(new SendToUserInputVM(user));
        }

        [HttpPost]
        [Route("Message/SendToUser/{userId}")]
        [Authorize(Roles = "Secretary, Teacher")]
        public ActionResult SendToUser(SendToUserInputVM messageToSendVM)
        {
            try
            {
                var user = UnitOfWork.UserRepository.GetById(messageToSendVM.UserId);

                Message message = new Message();

                message.MessageTypeId = (int)Consts.MessageTypes.ToUser;

                message.Header = messageToSendVM.Topic;
                message.Contents = messageToSendVM.Contents;
                message.IsSystem = messageToSendVM.IsSystem;

                UserMessage userMessage;

                userMessage = new UserMessage() {
                    User = user,
                    Message = message,
                };

                user.UsersMessages.Add(userMessage);

                UnitOfWork.Save();

                TempData["Alert"] = new AlertViewModel(Consts.Success, "Komunikat został wysłany pomyślnie", "proszę czekać na ewentualny kontakt ze strony odbiorcy.");

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return View(messageToSendVM);
            }
        }

        [HttpGet]
        [Route("Message/Send/")]
        [Authorize(Roles = "Secretary")]
        public ActionResult Send()
        {
            var messageInputVM = new MessageInputVM();

            PopulateInputLists(ref messageInputVM);

            return View(messageInputVM);
        }

        [HttpPost]
        [Route("Message/Send")]
        [Authorize(Roles = "Secretary")]
        public ActionResult Send(MessageInputVM messageInputVM)
        {
            try
            {
                var message = new Message();

                message.Header = messageInputVM.Topic;
                message.Contents = messageInputVM.Contents;
                message.MessageTypeId = messageInputVM.MessageTypeId;
                message.IsSystem = messageInputVM.IsSystem;
                message.UsersMessages = new List<UserMessage>();

                UserMessage userMessage;

                switch (messageInputVM.MessageTypeId)
                {
                    case (int)Consts.MessageTypes.ToGroup:
                        message.GroupId = messageInputVM.GroupId;

                        foreach (User u in UnitOfWork.UserRepository.Get(u => (u.UsersGroups.Where(g => g.GroupId == messageInputVM.GroupId).Any() && !u.IsDeleted)))
                        {
                            userMessage = new UserMessage() { User = u };

                            message.UsersMessages.Add(userMessage);
                        }

                        break;
                    case (int)Consts.MessageTypes.ToCourse:
                        message.CourseId = messageInputVM.CourseId;

                        foreach (User u in UnitOfWork.UserRepository.Get(u => (u.UsersGroups.Where(g => g.Group.CourseId == messageInputVM.CourseId).Any() && !u.IsDeleted)))
                        {
                            userMessage = new UserMessage() { User = u };

                            message.UsersMessages.Add(userMessage);
                        }

                        break;
                    case (int)Consts.MessageTypes.ToRole:
                        message.RoleId = messageInputVM.RoleId;

                        foreach (User u in UnitOfWork.UserRepository.Get(u => (u.RoleId == messageInputVM.RoleId && !u.IsDeleted)))
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

                TempData["Alert"] = new AlertViewModel(Consts.Success, "Komunikat został wysłany pomyślnie", "proszę czekać na ewentualny kontakt ze strony odbiorców");

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                PopulateInputLists(ref messageInputVM);

                return RedirectToAction("Index", "Home");
            }
        }
        
        [Route("Message/Delete/{Id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var now = DateTime.Now;

                var userMessage = UnitOfWork.UserMessageRepository.GetById(id);

                userMessage.IsDeleted = true;
                userMessage.DeletionDate = now;

                UnitOfWork.UserMessageRepository.Update(userMessage);

                UnitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return RedirectToAction("Index");
            }
        }

        private void PopulateInputLists(ref MessageInputVM messageInputVM)
        {
            messageInputVM.Groups = new SelectList(UnitOfWork.GroupRepository.Get(g => !g.IsDeleted),
                "Id",
                "Name");

            messageInputVM.Courses = new SelectList(UnitOfWork.CourseRepository.Get(c => !c.IsDeleted),
                "Id",
                "Name");

            messageInputVM.Roles = new SelectList(Consts.RoleList,
                "Key",
                "Value");

            messageInputVM.MessageTypes = new SelectList(Consts.MessageTypeList,
                "Key",
                "Value");
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
    }
}
