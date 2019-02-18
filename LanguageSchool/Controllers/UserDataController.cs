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
        [Authorize(Roles = "Teacher")]
        public ActionResult Index()
        {
            var userDatas = UnitOfWork.UserDataRepository.Get(ud => !ud.IsDeleted);
            return View(userDatas.ToList());
        }

        [Route("UserData/List/")]
        [Authorize(Roles = "Secretary")]
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
                int page = 1)
        {
            if (page == 1)
            {
                sortDirection = (sortDirection == "desc") ? "asc" : "desc";
            }

            var now = DateTime.Now;

            creationDateFrom = (creationDateFrom == null) ? (new DateTime(now.Year, now.Month, 1)) : creationDateFrom;
            creationDateTo = (creationDateTo == null) ? now : creationDateTo;

            PreferredHoursFrom = (PreferredHoursFrom == null) ? now.Hour : PreferredHoursFrom;
            PreferredHoursTo = (PreferredHoursTo == null) ? 20 : PreferredHoursTo;

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
                page);

            var totalRowCount = (contactInfo.Count == 0) ? 0 : (int)contactInfo.FirstOrDefault().TotalRowCount;

            ViewBag.Courses = new SelectList(UnitOfWork.CourseRepository.Get(c => !c.IsDeleted),
                                         "Id",
                                         "Name");

            ViewBag.Roles = new SelectList(Consts.RoleList.Where(r => r.Key != (int)Consts.Roles.Secretary),
                                         "Key",
                                         "Value");

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

            var contactInfoPaged = new StaticPagedList<GetContactInfoListItem>(contactInfo, page, 20, totalRowCount);

            return View(contactInfoPaged);
        }

        [HttpGet]
        [Route("UserData/AddGroups/{userId}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult AddGroups(int? userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = UnitOfWork.UserRepository.GetById(userId);

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
                if (!student.UsersGroups.Where(ug => (!ug.IsDeleted && (
                    (ug.Group.StartDate <= group.StartDate && group.StartDate <= ug.Group.EndDate) ||
                    (group.EndDate <= ug.Group.EndDate && ug.Group.StartDate <= group.EndDate) ||
                    (group.StartDate <= ug.Group.StartDate && ug.Group.StartDate <= group.EndDate) ||
                    (group.StartDate <= ug.Group.EndDate && ug.Group.EndDate <= group.EndDate)
                ))).Any())
                {
                    var groupViewModel = new UsersGroupViewModel(group);

                    foreach (var groupTime in group.GroupTimes.Where(gt => !gt.IsDeleted))
                    {
                        var hour = new GroupTimeViewModel(groupTime);

                        groupViewModel.Hours.Add(hour);
                    }

                    usersGroupViewModel.GroupsAvaible.Add(groupViewModel);
                }
                else
                {
                    var groupViewModel = new UsersGroupViewModel(group);

                    foreach (var groupTime in group.GroupTimes.Where(gt => !gt.IsDeleted))
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

        // todo
        [HttpPost]
        [Route("UserData/AddGroups/{userId}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult AddGroups(int userId, string submit)
        {
            var student = UnitOfWork.UserRepository.GetById(userId);

            student.UsersGroups.Add(new UserGroup
            {
                GroupId = Int32.Parse(submit)
            });

            UnitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }

        // GET: UserData/Details/5
        [Route("UserData/Details/{id}")]
        [Authorize(Roles = "Secretary")]
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
        [Authorize(Roles = "Secretary")]
        public ActionResult Create(int? contactRequestId)
        {
            if (contactRequestId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var contactRequest = UnitOfWork.ContactRequestRepository.GetById(contactRequestId);

            if (contactRequest == null)
            {
                return HttpNotFound();
            }

            UserDataViewModel userDataViewModel = new UserDataViewModel(contactRequest);

            PopulateInputLists(ref userDataViewModel);

            return View(userDataViewModel);
        }

        [HttpGet]
        [Route("UserData/Create")]
        [Authorize(Roles = "Secretary")]
        public ActionResult Create()
        {
            UserDataViewModel userDataViewModel = new UserDataViewModel();

            PopulateInputLists(ref userDataViewModel);

            return View(userDataViewModel);
        }

        [HttpPost]
        [Route("UserData/Create")]
        [Route("UserData/Create/{Id}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Secretary")]
        public ActionResult Create(UserDataViewModel udvm)
        {
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

                var userRole = UnitOfWork.DictionaryItemRepository.GetById(udvm.RoleId);

                if (udvm.OriginContactRequestId != null)
                {
                    var contactRequest = UnitOfWork.ContactRequestRepository.GetById(udvm.OriginContactRequestId);

                    contactRequest.IsAwaiting = false;

                    userRole = UnitOfWork.DictionaryItemRepository.GetById((int)Consts.Roles.Student);
                }

                User user = new User();

                user.Role = userRole;

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

                return RedirectToAction("Details", new { id = userData.UserId });
            }
            catch(Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                PopulateInputLists(ref udvm);

                return View(udvm);
            }
        }

        [HttpGet]
        [Route("UserData/Edit/{Id}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userData = UnitOfWork.UserDataRepository.GetById(id);

            if (userData == null)
            {
                return HttpNotFound();
            }

            var userDataViewModel = new UserDataViewModel(userData);

            return View(userDataViewModel);
        }

        [HttpPost]
        [Route("UserData/Edit/{Id}")]
        [Authorize(Roles = "Secretary")]
        public ActionResult Edit(UserDataViewModel userDataViewModel)
        {
            try
            {
                var userData = UnitOfWork.UserDataRepository.GetById(userDataViewModel.UserId);

                userData.Name = userDataViewModel.Name;
                userData.Surname = userDataViewModel.Surname;
                userData.City = userDataViewModel.City;
                userData.Street = userDataViewModel.Street;
                userData.HouseNumber = userDataViewModel.HouseNumber;
                userData.HomeNumber = userDataViewModel.HomeNumber;
                userData.PublicPhoneNumber = userDataViewModel.PublicPhoneNumber;
                userData.PrivatePhoneNumber = userDataViewModel.PrivatePhoneNumber;
                userData.EmailAdress = userDataViewModel.EmailAdress;
                userData.Comment = userDataViewModel.Comment;

                UnitOfWork.UserDataRepository.Update(userData);
                UnitOfWork.Save();

                return RedirectToAction("Details", new { userId = userDataViewModel.UserId });
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                PopulateInputLists(ref userDataViewModel);

                return View(userDataViewModel);
            }
        }

        private void PopulateInputLists(ref UserDataViewModel udvm)
        {
                if (udvm.OriginContactRequestId != null)
                {
                    var courseId = udvm.CourseId;

                    var groups = UnitOfWork.GroupRepository.Get(g => g.CourseId == courseId && !g.IsDeleted);

                    udvm.Groups = new SelectList(groups,
                                            "Id",
                                            "Name");
                }
                else
                {
                    udvm.Roles = new SelectList(Consts.RoleList.Where(r => r.Key != 1001),
                                            "Key",
                                            "Value");
                }
        }
    }
}
