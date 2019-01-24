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
        protected UnitOfWork UnitOfWork = new UnitOfWork();

        protected User GetLoggedUser()
        {
            int userId;

            Int32.TryParse(User.Identity.GetUserId(), out userId);

            return UnitOfWork.UserRepository.GetById(userId);
        }

        protected Guid LogException (Exception ex)
        {
            var guid = Guid.NewGuid();

            StringBuilder builder = new StringBuilder();
            builder
                .AppendLine(guid.ToString())
                .AppendLine("----------")
                .AppendLine(DateTime.Now.ToString())
                .AppendFormat("Source:\t{0}", ex.Source)
                .AppendLine()
                .AppendFormat("Target:\t{0}", ex.TargetSite)
                .AppendLine()
                .AppendFormat("Type:\t{0}", ex.GetType().Name)
                .AppendLine()
                .AppendFormat("Message:\t{0}", ex.Message)
                .AppendLine()
                .AppendFormat("Stack:\t{0}", ex.StackTrace)
                .AppendLine();

            string filePath = this.HttpContext.Server.MapPath("~/App_Data/Error.log");

            using (StreamWriter writer = System.IO.File.AppendText(filePath))
            {
                writer.Write(builder.ToString());
                writer.Flush();
            }

            return guid;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
