using System;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using lua.Models;
using lua.Models.ViewModels;
using lua.Services;
using Microsoft.AspNet.Identity;

namespace lua.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CategoriaManagementService categoriaManagementService = new CategoriaManagementService();

        
        // GET: Categorias/Nuevo

        public ActionResult Nuevo()
        {
            return View();
        }

        // POST: Categorias/Nuevo
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Nuevo(CategoriaFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                Categoria categoria = model.categoria;
                categoria.UsuarioId = User.Identity.GetUserId();
                db.Categoria.Add(categoria);
                db.SaveChanges();
                ViewBag.resultado = new string[] {"sucess", "Creado correctamente" };
            }
            else
            {
                ViewBag.resultado = new string[] { "fail", "Error al Crear" };
            }

            return View(model);
        }

        // GET: Categorias/Editar/5

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaFormViewModel model = new CategoriaFormViewModel(db.Categoria.Find(id));
            if (model.categoria == null || model.categoria.UsuarioId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Categorias/Editar/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Editar(CategoriaFormViewModel model)
        {
            if (ModelState.IsValid)
            {

                Categoria categoria = db.Categoria.Find(model.categoria.Id);
                if (categoria.UsuarioId != User.Identity.GetUserId())
                {
                    return HttpNotFound();
                }


                try
                {
                    categoriaManagementService.GenerarGuardarCategoriaFormViewModel(model);
                    ViewBag.resultado = new string[] { "sucess", "Guardado correctamente" };
                }
                catch (Exception)
                {
                    ViewBag.resultado = new string[] { "fail", "Error al Guardar" };
                }
               
            }
            return View(model);
        }

        // GET: Categorias/Eliminar/5

        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria.UsuarioId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }


            CategoriaFormViewModel model = new CategoriaFormViewModel(categoria);
            if (model.categoria == null || model.categoria.UsuarioId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Categorias/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Categoria categoria = db.Categoria.Find(id);
            try
            {
                if (categoria.UsuarioId != User.Identity.GetUserId())
                {
                    return HttpNotFound();
                }

                db.Categoria.Remove(categoria);
                db.SaveChanges();
                TempData["resultado"] = new string[] { "sucess", "Eliminado categoría " + categoria.Nombre + " correctamente" };
            }
            catch (Exception)
            {
                TempData["resultado"] = new string[] { "fail", "Error al Eliminar" };

            }
            return RedirectToAction("Index","Cursos");
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
