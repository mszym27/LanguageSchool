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
        [Authorize(Roles = "Teacher, Student")]
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var materials = UnitOfWork.MaterialRepository.Get(m => !m.IsDeleted && m.LessonSubjectId == id);

            if (materials == null)
            {
                return HttpNotFound();
            }

            return View(materials.ToList());
        }

        [Authorize(Roles = "Teacher, Student")]
        public FileResult Download(int id)
        {
            var requestedMaterial = UnitOfWork.MaterialRepository.Get(m => !m.IsDeleted &&  m.Id == id).FirstOrDefault();

            return File(requestedMaterial.File, ".pdf", requestedMaterial.Name + ".pdf");
        }

        [Route("Material/Upload/{lessonSubjectId}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult Upload(int? lessonSubjectId)
        {
            if (lessonSubjectId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lessonSubject = UnitOfWork.LessonSubjectRepository.GetById(lessonSubjectId);

            if (lessonSubject == null)
            {
                return HttpNotFound();
            }

            var materialViewModel = new MaterialViewModel() { LessonSubjectId = lessonSubject.Id };

            return View(materialViewModel);
        }

        [HttpPost]
        [Route("Material/Upload/{lessonSubjectId}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult Upload([Bind(Include = "LessonSubjectId,Name,Comment,File")] MaterialViewModel materialViewModel)
        {
            try
            {
                var material = new Material();

                material.LessonSubjectId = materialViewModel.LessonSubjectId;
                material.Name = materialViewModel.Name;
                material.Comment = materialViewModel.Comment;

                byte[] file = new byte[materialViewModel.File.ContentLength];
                materialViewModel.File.InputStream.Read(file, 0, file.Length);

                material.File = file;

                UnitOfWork.MaterialRepository.Insert(material);
                UnitOfWork.Save();

                return RedirectToAction("Details", "LessonSubject", new { id = materialViewModel.LessonSubjectId });
            }
            catch (Exception ex)
            {
                var errorLogGuid = LogException(ex);

                TempData["Alert"] = new AlertViewModel(errorLogGuid);

                return View(materialViewModel);
            }
        }

        [Route("Material/Delete/{id}")]
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

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

                return RedirectToAction("Details", "LessonSubject", new { id = material.LessonSubjectId });
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
