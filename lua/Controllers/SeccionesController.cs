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
using Microsoft.AspNet.Identity;

namespace lua.Controllers
{
    [Authorize]
    public class SeccionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private SeccionManagementService seccionManagementService = new SeccionManagementService();
        
        // GET: Secciones/Nuevo

        public ActionResult Nuevo()
        {
            SeccionFormViewModel model = this.seccionManagementService.CreateFormViewModelInicializado(User.Identity.GetUserId());
            return View(model);
        }

        // POST: Secciones/Nuevo
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Nuevo(SeccionFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                Seccion seccion = model.seccion;
                seccion.UsuarioId = User.Identity.GetUserId();
                db.Secciones.Add(seccion);
                db.SaveChanges();
                ViewBag.resultado = new string[] { "sucess", "Creado correctamente" };
            }
            else
            {
                ViewBag.resultado = new string[] { "fail", "Error al Crear" };
            }

            model = this.seccionManagementService.InicializarCursosNormal(model);
            return View(model);
        }

        // GET: Secciones/Editar/5

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seccion seccion = db.Secciones.Find(id);

            if (seccion == null || seccion.UsuarioId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }
            SeccionFormViewModel model = this.seccionManagementService.CreateFormViewModelInicializado(User.Identity.GetUserId());
            model.seccion = seccion;

            return View(model);
        }

        // POST: Secciones/Editar/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Editar(SeccionFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                Seccion seccion = db.Secciones.Find(model.seccion.Id);
                if (seccion.UsuarioId != User.Identity.GetUserId())
                {
                    return HttpNotFound();
                }
                try
                {
                    
                    seccionManagementService.GeneralGuardarSeccionFormViewModel(model);
                    ViewBag.resultado = new string[] { "sucess", "Guardado correctamente" };
                }
                catch (Exception)
                {
                    ViewBag.resultado = new string[] { "fail", "Error al Guardar" };
                }
            }

            SeccionFormViewModel newmodel = this.seccionManagementService.CreateFormViewModelInicializado(User.Identity.GetUserId());
            newmodel.seccion = model.seccion;
            return View(newmodel);
        }

        // GET: Secciones/Eliminar/5

        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seccion seccion = db.Secciones.Find(id);
            if (seccion.UsuarioId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }

            SeccionFormViewModel model = new SeccionFormViewModel(seccion);
            if (model.seccion == null || model.seccion.UsuarioId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Secciones/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Seccion seccion = db.Secciones.Find(id);
            try
            {
                if (seccion.UsuarioId != User.Identity.GetUserId())
                {
                    return HttpNotFound();
                }

                db.Secciones.Remove(seccion);
                db.SaveChanges();
                TempData["resultado"] = new string[] { "sucess", "Sección " + seccion.Titulo + " Eliminado correctamente" };
            }
            catch (Exception)
            {
                TempData["resultado"] = new string[] { "fail", "Error al Eliminar la Sección" };

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
