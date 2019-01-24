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
            var userDatas = UnitOfWork.UserDataRepository.Get(ud => !ud.IsDeleted);
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
                int? courseId,
                int? roleId,
                bool showUserData = false,
                bool showContactRequests = true,
                string sortColumn = "CreationDate",
                string sortDirection = "desc", 
                int page = 1,
                int pageSize = 20)
        {
            var now = DateTime.Now;

            creationDateFrom = (creationDateFrom == null) ? (new DateTime(now.Year, now.Month, 1)) : creationDateFrom;
            creationDateTo = (creationDateTo == null) ? now : creationDateTo;

            PreferredHoursFrom = (PreferredHoursFrom == null) ? 8 : PreferredHoursFrom;
            PreferredHoursTo = (PreferredHoursTo == null) ? now.Hour + 1 : PreferredHoursTo;

            var contactInfo = UnitOfWork.GetContactInfoList(
                creationDateFrom,
                creationDateTo,
                (int)PreferredHoursFrom,
                (int)PreferredHoursTo,
                fullName,
                city,
                street,
                phoneNumber,
                emailAdress,
                courseId,
                roleId,
                showUserData,
                showContactRequests,
                sortColumn,
                sortDirection,
                page,
                pageSize);

            var totalRowCount = (contactInfo.Count == 0) ? 0 : (int)contactInfo.FirstOrDefault().TotalRowCount;

            page = (contactInfo.Count + pageSize) < (page * pageSize) ? 1 : page;

            //SelectList Courses
            //SelectList Roles

            ViewBag.Courses = new SelectList(UnitOfWork.CourseRepository.Get(),
                                         "Id",
                                         "Name");

            ViewBag.Roles = new SelectList(UnitOfWork.RoleRepository.Get(r => r.Id != (int)Consts.Roles.Admin),
                                         "Id",
                                         "PLName");

            ViewBag.creationDateFrom = creationDateFrom;
            ViewBag.creationDateTo = creationDateTo;
            ViewBag.PreferredHoursFrom = PreferredHoursFrom;
            ViewBag.PreferredHoursTo = PreferredHoursTo;
            ViewBag.fullName = fullName;
            ViewBag.city = city;
            ViewBag.street = street;
            ViewBag.phoneNumber = phoneNumber;
            ViewBag.emailAdress = emailAdress;
            ViewBag.courseId = courseId;
            ViewBag.roleId = roleId;
            ViewBag.showUserData = showUserData;
            ViewBag.showContactRequests = showContactRequests;
            ViewBag.sortColumn = sortColumn;
            ViewBag.sortDirection = sortDirection;
            ViewBag.page = page;

            var contactInfoPaged = new StaticPagedList<GetContactInfoListItem>(contactInfo, page, pageSize, totalRowCount);

            return View(contactInfoPaged);
        }

        [HttpGet]
        [Route("UserData/AddGroups/{id}")]
        public ActionResult AddGroups(int id)
        {
            var student = UnitOfWork.UserRepository.Get(u => u.UserData.Where(ud => ud.Id == id).Any()).FirstOrDefault();

            if (student == null)
            {
                return HttpNotFound();
            }

            var usersGroupViewModel = new UsersGroupViewModel(student);

            var allGroups = UnitOfWork.GroupRepository.Get(g => !g.IsDeleted && g.EndDate > DateTime.Now);

            foreach(var userGroup in student.UsersGroups.Where(ug => !ug.IsDeleted))
                allGroups = allGroups.Where(g => g.Course.Id != userGroup.Group.Course.Id);

            var userGroupTimes = new List<GroupTime>();

            foreach (var userGroup in student.UsersGroups.Where(ug => !ug.IsDeleted && ug.Group.EndDate > DateTime.Now))
            {
                userGroupTimes.AddRange(userGroup.Group.GroupTimes);
            }

            foreach (var group in allGroups)
            {
                if (!student.UsersGroups.Where(ug => !(!ug.IsDeleted && (
                    (ug.Group.StartDate <= group.StartDate && group.StartDate <= ug.Group.EndDate) ||
                    (ug.Group.StartDate <= group.EndDate && group.EndDate <= ug.Group.EndDate) ||
                    (group.StartDate <= ug.Group.StartDate && ug.Group.StartDate <= group.EndDate) ||
                    (group.StartDate <= ug.Group.EndDate && ug.Group.EndDate <= group.EndDate)
                ))).Any())
                {
                    var groupViewModel = new UsersGroupViewModel(group);

                    foreach (var groupTime in group.GroupTimes.Where(gt => gt.IsActive && !gt.IsDeleted))
                    {
                        var hour = new GroupTimeViewModel(groupTime);

                        groupViewModel.Hours.Add(hour);
                    }

                    usersGroupViewModel.GroupsAvaible.Add(groupViewModel);
                }
                else
                {
                    var groupViewModel = new UsersGroupViewModel(group);

                    foreach (var groupTime in group.GroupTimes.Where(gt => gt.IsActive && !gt.IsDeleted))
                    {
                        var hour = new GroupTimeViewModel(groupTime);

                        var conflictingHour = userGroupTimes.Where(ugt => ugt.DayOfWeekId == hour.DayOfWeekId && (
                            (ugt.StartTime <= hour.StartTime && hour.StartTime <= ugt.EndTime) ||
                            (ugt.StartTime <= hour.EndTime && hour.EndTime <= ugt.EndTime) ||
                            (hour.StartTime <= ugt.StartTime && ugt.StartTime <= hour.EndTime) ||
                            (hour.StartTime <= ugt.EndTime && ugt.EndTime <= hour.EndTime)
                        )).FirstOrDefault();

                        if (conflictingHour != null)
                        {
                            hour.IsBlocked = true;
                            hour.ConflictingDateFullName =
                                conflictingHour.Group.Course.Name + " (" +
                                conflictingHour.Group.Name + ") " +
                                conflictingHour.DayOfWeek.PLName + " " +
                                conflictingHour.StartTime + ".15 - " +
                                (conflictingHour.EndTime + 1) + ".15 ";
                        }

                        groupViewModel.Hours.Add(hour);
                    }

                    if (groupViewModel.Hours.Where(h => h.IsBlocked).Any())
                        usersGroupViewModel.GroupsNonavaible.Add(groupViewModel);
                    else
                        usersGroupViewModel.GroupsAvaible.Add(groupViewModel);
                } 
            }

            return View(usersGroupViewModel);
        }

        [HttpPost]
        [Route("UserData/AddGroups/{id}")]
        public ActionResult AddGroups(int id, string submit)
        {
            var student = UnitOfWork.UserRepository.Get(u => u.UserData.Where(ud => ud.Id == id).Any()).FirstOrDefault();

            student.UsersGroups.Add(new UsersGroup {
                CreationDate = DateTime.Now,
                GroupId = Int32.Parse(submit)
            });

            UnitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }

        // GET: UserData/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserData userData = UnitOfWork.UserDataRepository.GetById(id);

            if (userData == null)
            {
                return HttpNotFound();
            }

            userData.User.Password = Encryption.Decrypt(userData.User.Password);

            return View(userData);
        }

        [HttpGet]
        [Route("UserData/Create/{contactRequestId}")]
        [Authorize(Roles = "Secretary, Administrator")]
        public ActionResult Create(int contactRequestId)
        {
            var contactRequest = UnitOfWork.ContactRequestRepository.GetById(contactRequestId);

            UserDataViewModel userDataViewModel = new UserDataViewModel(contactRequest);

            userDataViewModel.Groups = new SelectList(UnitOfWork.GroupRepository.Get(g => g.CourseId == contactRequest.CourseId),
                                         "Id",
                                         "Name");

            return View(userDataViewModel);
        }

        [HttpGet]
        [Route("UserData/Create")]
        [Authorize(Roles = "Secretary, Administrator")]
        public ActionResult Create()
        {
            UserDataViewModel userDataViewModel = new UserDataViewModel();

            userDataViewModel.Roles = new SelectList(UnitOfWork.RoleRepository.Get(r => r.Id != (int)Consts.Roles.Admin).OrderByDescending(u => u.Id),
                                         "Id",
                                         "PLName");

            return View(userDataViewModel);
        }

        // POST: UserData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("UserData/Create")]
        [Route("UserData/Create/{Id}")]
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

                var userRole = UnitOfWork.RoleRepository.GetById(udvm.RoleId);

                if (udvm.OriginContactRequestId != null)
                {
                    var contactRequest = UnitOfWork.ContactRequestRepository.GetById(udvm.OriginContactRequestId);

                    contactRequest.IsAwaiting = false;

                    userRole = UnitOfWork.RoleRepository.GetById((int)Consts.Roles.Student);
                }

                User user = new User();

                user.Role = userRole;

                user.CreationDate = DateTime.Now;
                userData.CreationDate = DateTime.Now;

                user.Login = userRole.ENName[0] + "_" + userData.Name[0] + userData.Surname[0];
                user.Password = Encryption.Encrypt(Membership.GeneratePassword(8, 3));

                userData.User = user;

                UnitOfWork.UserDataRepository.Insert(userData);

                if(userRole.Id == (int)Consts.Roles.Student)
                {
                    var welcome = UnitOfWork.MessageRepository.Get(m => m.MessageTypeId == (int)Consts.MessageTypes.StudentWelcome).FirstOrDefault();

                    var userMessage = new UserMessage() { Message = welcome };

                    user.UsersMessages.Add(userMessage);
                }
                else
                {
                    user.Login = "BL\\" + user.Login;
                }

                UnitOfWork.Save();

                var randomInt = new Random(user.Id).Next(0, 10000);

                user.Login = user.Login + "_" + randomInt.ToString("00000");

                UnitOfWork.Save();

                TempData["Alert"] = new AlertViewModel(Consts.Success, "Konto zostało utworzone", "proszę przekazać użytkownikowi jego login i hasło");

                return RedirectToAction("Details", "UserData", new { id = userData.Id });
            }
            catch(Exception ex)
            {
                TempData["Alert"] = new AlertViewModel(Consts.Error, "Nastąpił nieoczekiwany wyjątek", "informując o błędzie przekaż obsłudze aplikacji następujący kod: " + LogException(ex).ToString());

                ViewBag.UserId = new SelectList(UnitOfWork.UserRepository.Get(u => !u.IsDeleted), "Id", "Login");

                return View(udvm);
            }
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
