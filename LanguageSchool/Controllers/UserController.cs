using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;

namespace LanguageSchool.Controllers
{
    public class UserController : LanguageSchoolController
    {
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid) //validating the user inputs  
            {
                int? userRoleId = unitOfWork.UserRepository.Get()
                    .Where(u => u.Login == user.Login)
                    .Where(u => u.Password == user.Password)
                    .Where(c => !c.IsDeleted)
                    .First().RoleId;

                if (userRoleId != null)
                {
                    //LoginModels _loginCredentials = _entity.tblLogins.Where(x => x.UserName.Trim().ToLower() == _login.UserName.Trim().ToLower()).Select(x => new LoginModels
                    //{
                    //    UserName = x.UserName,
                    //    RoleName = x.tblRole.Roles,
                    //    UserRoleId = x.RoleId,
                    //    UserId = x.Id
                    //}).FirstOrDefault();  // Get the login user details and bind it to LoginModels class

                    List<MenuViewModel> menus = Consts.menus.Where(m => m.RoleId == userRoleId).ToList();
                        
                    //    _entity.tblSubMenus.Where(x => x.RoleId == _loginCredentials.UserRoleId).Select(x => new MenuModels
                    //{
                    //    MainMenuId = x.tblMainMenu.Id,
                    //    MainMenuName = x.tblMainMenu.MainMenu,
                    //    SubMenuId = x.Id,
                    //    SubMenuName = x.SubMenu,
                    //    ControllerName = x.Controller,
                    //    ActionName = x.Action,
                    //    RoleId = x.RoleId,
                    //    RoleName = x.tblRole.Roles
                    //}).ToList(); //Get the Menu details from entity and bind it in MenuModels list.  

                    FormsAuthentication.SetAuthCookie(user.Login, false); // set the formauthentication cookie  
                    Session["User"] = user; // Bind the _logincredentials details to "LoginCredentials" session  
                    Session["Menus"] = menus; //Bind the _menus list to MenuMaster session  
                    Session["Login"] = user.Login;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMsg = "Please enter the valid credentials!...";
                    return View();
                }
            }

            return View();
        }
    }
}