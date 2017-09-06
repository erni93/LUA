using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lua.Models;
using lua.Models.ViewModels;
using lua.Models.DTOS;
using AutoMapper;
using lua.Services;

namespace lua.Areas.Admin.Controllers
{
    public class CursosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CursoManagementService cursoManagementService = new CursoManagementService();
        // GET: Cursos
        public ActionResult Index()
        {
            CursoViewModel model = cursoManagementService.CreateViewModel();
            return View(model);

        }

        // GET: Cursos/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursoFormViewModel model = cursoManagementService.CreateFormViewModel(id.Value);
            if (model.curso == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Cursos/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cursos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(CursoFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Cursos.Add(model.curso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Cursos/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursoFormViewModel model = new CursoFormViewModel(db.Cursos.Find(id));
            if (model.curso == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Cursos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(CursoFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model.curso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Cursos/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursoFormViewModel model = new CursoFormViewModel(db.Cursos.Find(id));
            if (model.curso == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Cursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Curso curso = db.Cursos.Find(id);
            db.Cursos.Remove(curso);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
