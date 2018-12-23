﻿using System;
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
    public class MessageController : LanguageSchoolController
    {
        [Authorize]
        public ActionResult Index(string sortColumn = "sentDate", string sortDirection = "asc", int page = 1)
        {
            var userId = Int32.Parse(User.Identity.GetUserId());

            var userMessages = unitOfWork.UserMessageRepository.Get(um => (um.UserId == userId)).ToList();

            var userMessagesViewModels = new List<UserMessageViewModel>();

            foreach (UserMessage um in userMessages)
            {
                userMessagesViewModels.Add(new UserMessageViewModel(um));
            }

            return View(userMessagesViewModels.ToPagedList(page, 20));
        }

        //// GET: Message/Details/5
        //public ActionResult Details(int? id)
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

        //// GET: Message
        //[Authorize(Roles = "Secretary, Administrator")]
        //// GET: Message/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

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

        //// POST: Message/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Message message = db.Messages.Find(id);
        //    db.Messages.Remove(message);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
