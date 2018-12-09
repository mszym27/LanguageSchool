﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

using LanguageSchool.DAL;

namespace LanguageSchool.Controllers
{
    public class LanguageSchoolController : Controller
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

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
