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

    public class CursosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CursoManagementService cursoManagementService = new CursoManagementService();


        [Authorize]
        public ActionResult Index()
        {
            ListadoCompletoViewModel listadoCompletoViewModel = this.cursoManagementService.CreateListadoCompletoViewModel(User.Identity.GetUserId());

            if (TempData["resultado"] != null)
            {
                ViewBag.resultado = TempData["resultado"];
            }
            return View(listadoCompletoViewModel);
        }
       
        // POST: Cursos/ListadoCursosAjax
        [HttpPost]
        public ActionResult ListadoCursosAjax()
        {
            return Json(cursoManagementService.CategoriasConCursos(5), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ver(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            CursoCompletoViewModel model = this.cursoManagementService.CreateCursoCompletoViewModel(id.Value);

            if (!model.IsValid())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(model);
        }

        [Authorize]
        // GET: Cursos/Nuevo
        public ActionResult Nuevo()
        {
            CursoFormViewModel model = this.cursoManagementService.CreateFormViewModelInicializado(User.Identity.GetUserId());
            return View(model);
        }


        [Authorize]
        // POST: Cursos/Nuevo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Nuevo(CursoFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Curso curso = model.curso;
                    curso.FechaCreacion = DateTime.Now;
                    curso.Portada = this.cursoManagementService.GuardarImagenPortada(model.imagenPortada);
                    curso.UsuarioId = User.Identity.GetUserId();

                    db.Cursos.Add(model.curso);
                    db.SaveChanges();
                    ViewBag.resultado = new string[] { "sucess", "Creado correctamente" };
                }
                catch (Exception)
                {
                    ViewBag.resultado = new string[] { "fail", "Error al Guardar" };
                }
            }
            model = this.cursoManagementService.InicializarCategoriasNormal(model);
            return View(model);
        }


        [Authorize]
        // GET: Cursos/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Cursos.Find(id);

            if (curso == null || curso.UsuarioId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }
            CursoFormViewModel model = this.cursoManagementService.CreateFormViewModelInicializado(User.Identity.GetUserId());
            model.curso = curso;

            return View(model);
        }

        // POST: Cursos/Editar/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(CursoFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                Curso curso = db.Cursos.Find(model.curso.Id);
                if (curso.UsuarioId != User.Identity.GetUserId())
                {
                    return HttpNotFound();
                }
                try
                {
                    if (model.imagenPortada != null)
                    {
                        model.curso.Portada = this.cursoManagementService.GuardarImagenPortada(model.imagenPortada);
                    }
                    cursoManagementService.GeneralGuardarCursoFormViewModel(model);
                    ViewBag.resultado = new string[] { "sucess", "Curso guardado correctamente" };
                }
                catch (Exception)
                {
                    ViewBag.resultado = new string[] { "fail", "Error al guardar el curso"};
                }
            }

            CursoFormViewModel newmodel = this.cursoManagementService.CreateFormViewModelInicializado(User.Identity.GetUserId());
            newmodel.curso = model.curso;
            return View(newmodel);
        }



        // GET: Cursos/Eliminar/5
        [Authorize]
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Cursos.Find(id);
            if (curso.UsuarioId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }


            CursoFormViewModel model = new CursoFormViewModel(curso);
            if (model.curso == null || model.curso.UsuarioId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [Authorize]
        // POST: Cursos/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            Curso curso = db.Cursos.Find(id);
            try
            {
                if (curso.UsuarioId != User.Identity.GetUserId())
                {
                    return HttpNotFound();
                }

                db.Cursos.Remove(curso);
                db.SaveChanges();
                TempData["resultado"] = new string[] { "sucess", "Curso " + curso.Titulo + "Eliminado correctamente" };
            }
            catch (Exception)
            {
                TempData["resultado"] = new string[] { "fail", "Error al Eliminar el curso" };

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
