using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LanguageSchool.Models;
using LanguageSchool.Models.ViewModels;

namespace LanguageSchool.Controllers
{
    [Authorize]
    public class MaterialController : LanguageSchoolController
    {
        // GET: Material
        [Route("Material/Index/{id}")]
        public ActionResult Index(int id)
        {
            var materials = UnitOfWork.MaterialRepository.Get(m => !m.IsDeleted && m.IsActive && m.LessonSubjectId == id);
            return View(materials.ToList());
        }

        [HttpGet]
        public FileResult Download(int id)
        {
            var requestedMaterial = UnitOfWork.MaterialRepository.Get(m => !m.IsDeleted && m.IsActive && m.Id == id).FirstOrDefault();

            return File(requestedMaterial.File, ".pdf", requestedMaterial.Name + ".pdf");
        }

        [Route("Material/Upload/{lessonSubjectId}")]
        public ActionResult Upload(int lessonSubjectId)
        {
            return View(new MaterialViewModel() { LessonSubjectId = lessonSubjectId });
        }

        [Route("Material/Upload/{lessonSubjectId}")]
        // POST: Material/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload([Bind(Include = "LessonSubjectId,Name,Comment,File")] MaterialViewModel materialViewModel)
        {
            var material = new Material();

            material.CreationDate = DateTime.Now;
            material.IsActive = true;

            material.LessonSubjectId = materialViewModel.LessonSubjectId;
            material.Name = materialViewModel.Name;
            material.Comment = materialViewModel.Comment;

            byte[] file = new byte[materialViewModel.File.ContentLength];
            materialViewModel.File.InputStream.Read(file, 0, file.Length);

            material.File = file;

            UnitOfWork.MaterialRepository.Insert(material);
            UnitOfWork.Save();
            return RedirectToAction("Details", "LessonSubject", new { id = materialViewModel.LessonSubjectId });
            //}

            //ViewBag.LessonSubjectId = new SelectList(unitOfWork.LessonSubjectRepository.Get(), "Id", "Name", material.LessonSubjectId);
            //return View(material);
        }

        [Route("Material/Delete/{id}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int id)
        {
            try
            {
                var now = DateTime.Now;

                var material = UnitOfWork.MaterialRepository.GetById(id);

                if (material == null)
                {
                    return HttpNotFound();
                }

                material.IsDeleted = true;
                material.DeletionDate = now;

                UnitOfWork.Save();

                return RedirectToAction("Details", "Group", new { id = material.LessonSubjectId });
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return RedirectToAction("Index", "Home");
            }
        }
    }
}
