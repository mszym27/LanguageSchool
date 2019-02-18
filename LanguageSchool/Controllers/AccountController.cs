using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Text;

using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;

namespace LanguageSchool.Controllers
{
    public class AccountController : LanguageSchoolController
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                if (Request.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);
            }

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginInfo, string returnUrl)
        {
            try
            {
                var passwordEncrypted = Encryption.Encrypt(loginInfo.Password.Trim());

                var loginUser = UnitOfWork.UserRepository.Get(u => !u.IsDeleted && (u.Login == loginInfo.Login.Trim() && u.Password == passwordEncrypted)).FirstOrDefault();

                if (loginUser != null)
                {
                    this.LogUserIn(loginUser, loginInfo.RememberMe);

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        var decodedUrl = Server.UrlDecode(returnUrl);
                        return Redirect(decodedUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Niewłaściwe dane logowania");
                }
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return View(loginInfo);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            try
            {
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                authenticationManager.SignOut();

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return RedirectToAction("Index", "Home");
            }
        }

        private void LogUserIn(User user, bool rememberMe)
        {
            var claims = new List<Claim>();

            try
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, user.Login));
                claims.Add(new Claim(ClaimTypes.Role, user.Role.ENName));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = rememberMe, ExpiresUtc = DateTime.Now.AddDays(7)
                }, claimIdenties);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}