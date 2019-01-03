using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Cryptography;
using System.IO;

using LanguageSchool.DAL;
using LanguageSchool.Models;

namespace LanguageSchool.Controllers
{
    public class LanguageSchoolController : Controller
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        protected User GetLoggedUser()
        {
            int userId;

            Int32.TryParse(User.Identity.GetUserId(), out userId);

            return unitOfWork.UserRepository.GetById(userId);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
