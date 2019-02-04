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
using LanguageSchool.Models.ViewModels.SendMessageViewModels;
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

        [Authorize]
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

        [Authorize(Roles = "Secretary, Teacher")]
        [HttpGet]
        [Route("Message/SendToUser/{userId}")]
        public ActionResult SendToUser(int userId)
        {
            var user = UnitOfWork.UserRepository.GetById(userId);

            return View(new SendToUserVM(user));
        }

        [Authorize(Roles = "Secretary, Teacher")]
        [HttpPost]
        [Route("Message/SendToUser/{userId}")]
        public ActionResult SendToUser(SendToUserVM messageToSend)
        {
            try
            {
                var user = UnitOfWork.UserRepository.GetById(messageToSend.UserId);

                Message message = new Message();

                message.MessageTypeId = (int)Consts.MessageTypes.ToUser;
                message.CreationDate = DateTime.Now;

                message.Header = messageToSend.Topic;
                message.Contents = messageToSend.Contents;
                message.IsSystem = messageToSend.IsSystem;

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

                return View(messageToSend);
            }
        }

        [Authorize(Roles = "Secretary")]
        [HttpGet]
        [Route("Message/Send/")]
        public ActionResult Send()
        {
            UserMessageViewModel userMessageViewModel = new UserMessageViewModel();

            var types = UnitOfWork.MessageTypeRepository.Get(
                t => t.Id != (int)Consts.MessageTypes.ToUser
                    && t.Id != (int)Consts.MessageTypes.StudentWelcome
            );

            userMessageViewModel.MessageTypes = new SelectList(types,
                                         "Id",
                                         "Name");

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
        [Route("Message/Send")]
        public ActionResult Send(UserMessageViewModel uMVM)
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

                TempData["Alert"] = new AlertViewModel(Consts.Success, "Komunikat został wysłany pomyślnie", "proszę czekać na ewentualny kontakt ze strony odbiorców");

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

        [Authorize]
        [Route("Message/Delete/{Id}")]
        public ActionResult Delete(int Id)
        {
            try
            {
                var now = DateTime.Now;

                var userMessage = UnitOfWork.UserMessageRepository.GetById(Id);

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

                return RedirectToAction("Index", "Home");
            }
        }
    }
}
