using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;

using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;

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

        [Route("UserData/List/")]
        [Authorize(Roles = "Secretary, Administrator")]
        public ActionResult List(
                Nullable<System.DateTime> creationDateFrom,
                Nullable<System.DateTime> creationDateTo,
                int? PreferredHoursFrom,
                int? PreferredHoursTo,
                string fullName,
                string city,
                string street,
                string phoneNumber,
                string emailAdress,
                bool showUserData = true,
                bool showContactRequests = false,
                string sortColumn = "CreationDate",
                string sortDirection = "desc", 
                int page = 1,
                int pageSize = 20)
        {
            var now = DateTime.Now;

            creationDateFrom = (creationDateFrom == null) ? (new DateTime(now.Year, now.Month, 1)) : creationDateFrom;
            creationDateTo = (creationDateTo == null) ? now : creationDateTo;

            PreferredHoursFrom = (PreferredHoursFrom == null) ? 8 : PreferredHoursFrom;
            PreferredHoursTo = (PreferredHoursTo == null) ? now.Hour : PreferredHoursTo;

            var contactInfo = unitOfWork.GetContactInfoList(
                creationDateFrom,
                creationDateTo,
                (int)PreferredHoursFrom,
                (int)PreferredHoursTo,
                fullName,
                city,
                street,
                phoneNumber,
                emailAdress,
                showUserData,
                showContactRequests,
                sortColumn,
                sortDirection,
                page,
                pageSize);

            var totalRowCount = (contactInfo.Count == 0) ? 0 : (int)contactInfo.FirstOrDefault().TotalRowCount;

            page = (contactInfo.Count + pageSize) < (page * pageSize) ? 1 : page;

            ViewBag.creationDateFrom = creationDateFrom;
            ViewBag.creationDateTo = creationDateTo;
            ViewBag.PreferredHoursFrom = PreferredHoursFrom;
            ViewBag.PreferredHoursTo = PreferredHoursTo;
            ViewBag.fullName = fullName;
            ViewBag.city = city;
            ViewBag.street = street;
            ViewBag.phoneNumber = phoneNumber;
            ViewBag.emailAdress = emailAdress;
            ViewBag.showUserData = showUserData;
            ViewBag.showContactRequests = showContactRequests;
            ViewBag.sortColumn = sortColumn;
            ViewBag.sortDirection = sortDirection;
            ViewBag.page = page;

            var contactInfoPaged = new StaticPagedList<GetContactInfoListItem>(contactInfo, page, pageSize, totalRowCount);

            return View(contactInfoPaged);
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

            userData.User.Password = unitOfWork.Decrypt(userData.User.Password);

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
                user.Password = unitOfWork.Encrypt(Membership.GeneratePassword(8, 3));

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

                return RedirectToAction("Details", "UserData", new { id = userData.Id });
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
