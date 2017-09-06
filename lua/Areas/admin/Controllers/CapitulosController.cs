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

namespace lua.Areas.admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CapitulosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CapituloManagementService capituloManagementService = new CapituloManagementService();
        // GET: Capitulos

        public ActionResult Index()
        {
            CapituloViewModel model = capituloManagementService.CreateViewModel();
            return View(model);
        }

        // GET: Capitulos/Details/5
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CapituloFormViewModel model = capituloManagementService.CreateFormViewModel(id.Value);
            if (model.capitulo == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Capitulos/Create
        [ValidateInput(false)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Capitulos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(CapituloFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Capitulos.Add(model.capitulo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Capitulos/Edit/5
        [ValidateInput(false)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CapituloFormViewModel model = new CapituloFormViewModel(db.Capitulos.Find(id));
            if (model.capitulo == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Capitulos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(CapituloFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model.capitulo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Capitulos/Delete/5
        [ValidateInput(false)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CapituloFormViewModel model = new CapituloFormViewModel(db.Capitulos.Find(id));
            if (model.capitulo == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Capitulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Capitulo capitulo = db.Capitulos.Find(id);
            db.Capitulos.Remove(capitulo);
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
