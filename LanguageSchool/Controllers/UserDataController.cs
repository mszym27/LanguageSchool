﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;
using System.Web.Security;

namespace LanguageSchool.Controllers
{
    [Authorize]
    public class UserDataController : LanguageSchoolController
    {
        // GET: UserData
        // dla nauczycieli oraz studentow, ograniczone do ich grup
        public ActionResult Index()
        {
            var userDatas = unitOfWork.UserDataRepository.Get(ud => !ud.IsDeleted);
            return View(userDatas.ToList());
        }

        // GET: UserData/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserData userData = unitOfWork.UserDataRepository.GetById(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            return View(userData);
        }

        // GET: UserData/Create
        [Authorize(Roles = "Secretary, Administrator")]
        public ActionResult Create()
        {
            UserDataViewModel userDataViewModel = new UserDataViewModel();

            userDataViewModel.Roles = new SelectList(unitOfWork.RoleRepository.Get(),
                                         "Id",
                                         "PLName");

            return View(userDataViewModel);
        }

        // POST: UserData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Secretary, Administrator")]
        public ActionResult Create(UserDataViewModel udvm)
        {
            //if (ModelState.IsValid)
            try
            {
                UserData userData = new UserData();

                userData.Name = udvm.Name;
                userData.Surname = udvm.Surname;
                userData.City = udvm.City;
                userData.Street = udvm.Street;
                userData.HouseNumber = udvm.HouseNumber;
                userData.HomeNumber = udvm.HomeNumber;
                userData.PublicPhoneNumber = udvm.PublicPhoneNumber;
                userData.PrivatePhoneNumber = udvm.PrivatePhoneNumber;
                userData.EmailAdress = udvm.EmailAdress;
                userData.Comment = udvm.Comment;

                User user = new User();

                user.CreationDate = DateTime.Now;
                userData.CreationDate = DateTime.Now;

                user.Role = unitOfWork.RoleRepository.GetById(udvm.RoleId);

                user.Login = "BL\\" + user.Role.ENName[0] + "_" + userData.Name[0] + userData.Surname[0];
                user.Password = Membership.GeneratePassword(8, 3);

                userData.User = user;

                unitOfWork.UserDataRepository.Insert(userData);

                var randomInt = new Random().Next(0, 10000);

                user.Login = user.Login + "_" + randomInt.ToString("00000");

                unitOfWork.Save();

                TempData["Alert"] = new AlertViewModel()
                {
                    Title = "Konto zostało utworzone pomyślne",
                    Message = "proszę przekazać użytkownikowi jego login i hasło",
                    AlertType = Consts.Success
                };

                return RedirectToAction("Details", "User", new { id = userData.UserId });
            }
            catch
            {
                return View();
            }
            //ViewBag.UserId = new SelectList(unitOfWork.UserRepository.Get(u => !u.IsDeleted), "Id", "Login");
            //return View(udvm);
        }

        //// GET: UserData/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserData userData = db.UserDatas.Find(id);
        //    if (userData == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "Login", userData.UserId);
        //    return View(userData);
        //}

        //// POST: UserData/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,IsDeleted,CreationDate,DeletionDate,UserId,Name,Surname,City,Street,HouseNumber,HomeNumber,PublicPhoneNumber,PrivatePhoneNumber,EmailAdress,Comment")] UserData userData)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(userData).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "Login", userData.UserId);
        //    return View(userData);
        //}

        //// GET: UserData/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserData userData = db.UserDatas.Find(id);
        //    if (userData == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(userData);
        //}

        //// POST: UserData/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    UserData userData = db.UserDatas.Find(id);
        //    db.UserDatas.Remove(userData);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
