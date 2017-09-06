using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using lua.Models;
using lua.Models.ViewModels;
using lua.Services;
using Microsoft.AspNet.Identity;
using System;

namespace lua.Controllers
{
    [Authorize]
    public class CapitulosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CapituloManagementService capituloManagementService = new CapituloManagementService();


        // GET: Capitulos/Nuevo
        [ValidateInput(false)]
        public ActionResult Nuevo()
        {
            CapituloFormViewModel model = this.capituloManagementService.CreateFormViewModelInicializado(User.Identity.GetUserId());
            return View(model);
        }

        // POST: Capitulos/Nuevo
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Nuevo(CapituloFormViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Capitulo capitulo = model.capitulo;
                    capitulo.UsuarioId = User.Identity.GetUserId();

                    db.Capitulos.Add(model.capitulo);
                    db.SaveChanges();
                    ViewBag.resultado = new string[] { "sucess", "Creado correctamente" };
                }
                catch (Exception)
                {
                    ViewBag.resultado = new string[] { "fail", "Error al Guardar" };
                }
            }
            model = this.capituloManagementService.InicializarSeccionesNormal(model);
            return View(model);
        }

        // GET: Capitulos/Editar/5
        [ValidateInput(false)]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capitulo capitulo = db.Capitulos.Find(id);

            if (capitulo == null || capitulo.UsuarioId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }
            CapituloFormViewModel model = this.capituloManagementService.CreateFormViewModelInicializado(User.Identity.GetUserId());
            model.capitulo = capitulo;

            return View(model);
        }

        // POST: Capitulos/Editar/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Editar(CapituloFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                Capitulo capitulo = db.Capitulos.Find(model.capitulo.Id);
                if (capitulo.UsuarioId != User.Identity.GetUserId())
                {
                    return HttpNotFound();
                }
                try
                {
                    capituloManagementService.GeneralGuardarCapituloFormViewModel(model);
                    ViewBag.resultado = new string[] { "sucess", "Guardado correctamente" };
                }
                catch (Exception)
                {
                    ViewBag.resultado = new string[] { "fail", "Error al Guardar" };
                }
            }

            CapituloFormViewModel newmodel = this.capituloManagementService.CreateFormViewModelInicializado(User.Identity.GetUserId());
            newmodel.capitulo = model.capitulo;
            return View(newmodel);
        }

        // GET: Capitulos/Eliminar/5

        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capitulo capitulo = db.Capitulos.Find(id);
            if (capitulo.UsuarioId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }


            CapituloFormViewModel model = new CapituloFormViewModel(capitulo);
            if (model.capitulo == null || model.capitulo.UsuarioId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Capitulos/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Capitulo capitulo = db.Capitulos.Find(id);
            try
            {
                if (capitulo.UsuarioId != User.Identity.GetUserId())
                {
                    return HttpNotFound();
                }

                db.Capitulos.Remove(capitulo);
                db.SaveChanges();
                TempData["resultado"] = new string[] { "sucess", "Capítulo " + capitulo.Titulo + " Eliminado correctamente" };
            }
            catch (Exception)
            {
                TempData["resultado"] = new string[] { "fail", "Error al Eliminar el Capítulo" };

            }
            return RedirectToAction("Index", "Cursos");
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
