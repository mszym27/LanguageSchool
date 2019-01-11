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
            var materials = unitOfWork.MaterialRepository.Get(m => !m.IsDeleted && m.IsActive && m.LessonSubjectId == id);
            return View(materials.ToList());
        }

        [HttpGet]
        public FileResult Download(int id)
        {
            var requestedMaterial = unitOfWork.MaterialRepository.Get(m => !m.IsDeleted && m.IsActive && m.Id == id).FirstOrDefault();

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

            unitOfWork.MaterialRepository.Insert(material);
            unitOfWork.Save();
            return RedirectToAction("Index", "Home");
            //}

            //ViewBag.LessonSubjectId = new SelectList(unitOfWork.LessonSubjectRepository.Get(), "Id", "Name", material.LessonSubjectId);
            //return View(material);
        }

        /*
        materiały dostępne do pobrania bezpośrednio z poziomu indeksu
        może ograniczone do wybranego kursu? Powiedzmy że użytkownik wchodzi do serwisu, widzi swój plan zajęć
        i może z jego poziomu przejść do wybranego kursu. Widzi tam opis, testy oraz materiały które zostały mu wystawione.
        Można też pomyśleć o jakimś znaczniku ,,Nowy test/materiał" przy danym kursie, ale wtedy trzeba byłoby pomyśleć również
        o przechowywaniu daty ostatniego logowania. Chociaż byłoby to też pewnie możliwe do wykorzystania w wiadomościach.
        */

        //// GET: Material/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Material material = db.Materials.Find(id);
        //    if (material == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.LessonSubjectId = new SelectList(db.LessonSubjects, "Id", "Name", material.LessonSubjectId);
        //    return View(material);
        //}

        //// POST: Material/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,IsDeleted,CreationDate,DeletionDate,LessonSubjectId,Name,File,Comment,IsActive")] Material material)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(material).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.LessonSubjectId = new SelectList(db.LessonSubjects, "Id", "Name", material.LessonSubjectId);
        //    return View(material);
        //}

        //// GET: Material/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Material material = db.Materials.Find(id);
        //    if (material == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(material);
        //}

        //// POST: Material/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Material material = db.Materials.Find(id);
        //    db.Materials.Remove(material);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
