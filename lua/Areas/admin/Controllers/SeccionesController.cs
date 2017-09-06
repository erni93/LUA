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
    [Authorize(Roles = "Administrator")]
    public class SeccionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private SeccionManagementService seccionManagementService = new SeccionManagementService();
        // GET: Secciones

        public ActionResult Index()
        {
            SeccionViewModel model = seccionManagementService.CreateViewModel();
            return View(model);
        }

        // GET: Secciones/Details/5
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeccionFormViewModel model = seccionManagementService.CreateFormViewModel(id.Value);
            if (model.seccion == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Secciones/Create
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Secciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(SeccionFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Secciones.Add(model.seccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Secciones/Edit/5
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeccionFormViewModel model = new SeccionFormViewModel(db.Secciones.Find(id));
            if (model.seccion == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Secciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Edit(SeccionFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model.seccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Secciones/Delete/5
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeccionFormViewModel model = new SeccionFormViewModel(db.Secciones.Find(id));
            if (model.seccion == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Secciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Seccion seccion = db.Secciones.Find(id);
            db.Secciones.Remove(seccion);
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
