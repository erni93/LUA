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
    public class CategoriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CategoriaManagementService categoriaManagementService = new CategoriaManagementService();


        // GET: Categorias

        public ActionResult Index()
        {
            CategoriaViewModel model = categoriaManagementService.CreateViewModel();
            return View(model);
        }

        // GET: Categorias/Details/5
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaFormViewModel model = categoriaManagementService.CreateFormViewModel(id.Value);
            if (model.categoria == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Categorias/Create
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(CategoriaFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Categoria.Add(model.categoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Categorias/Edit/5
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaFormViewModel model = new CategoriaFormViewModel(db.Categoria.Find(id));
            if (model.categoria == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Edit(CategoriaFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model.categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Categorias/Delete/5
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaFormViewModel model = new CategoriaFormViewModel(db.Categoria.Find(id));
            if (model.categoria == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Categoria categoria = db.Categoria.Find(id);
            db.Categoria.Remove(categoria);
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
