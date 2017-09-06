using AutoMapper;
using lua.Models;
using lua.Models.DTOS;
using lua.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace lua.Services
{
    public class CursoManagementService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public CursoViewModel CreateViewModel()
        {
            return new CursoViewModel(CursoListToCursoDTOList());
        }
        public CursoFormViewModel CreateFormViewModel(int id)
        {
            return new CursoFormViewModel(db.Cursos.Find(id));
        }


        public List<CursoDTO> CursoListToCursoDTOList()
        {
            List<Curso> Cursos = db.Cursos.ToList();
            List<CursoDTO> CursosDTO = new List<CursoDTO>();
            foreach (var item in Cursos)
            {
                CursosDTO.Add(Mapper.Map<Curso, CursoDTO>(item));
            }
            return CursosDTO;
        }


        public List<CursoDTO> CursoListToCursoDTOList(List<Curso> Cursos)
        {
            List<CursoDTO> CursosDTO = new List<CursoDTO>();
            foreach (var item in Cursos)
            {
                CursosDTO.Add(Mapper.Map<Curso, CursoDTO>(item));
            }
            return CursosDTO;
        }

        public CategoriaAjaxDTO[] CategoriasConCursos(int maxCursos)
        {
            List<CategoriaAjaxDTO> resultado = new List<CategoriaAjaxDTO>();
            List<Categoria> categorias = db.Categoria.ToList();
            foreach (var categoria in categorias)
            {
                List<Curso> cursos = db.Cursos.Where(x => x.CategoriaId == categoria.Id && x.Activo).OrderBy(x => x.FechaCreacion).Take(maxCursos).ToList();
                CursoDTOAjaxListado[] cursosDTOAjax = cursos.Select(x => new CursoDTOAjaxListado() { Titulo = x.Titulo, Portada = x.Portada, Id = x.Id }).ToArray();
                CategoriaAjaxDTO categoriaAjax = new CategoriaAjaxDTO() { Nombre = categoria.Nombre, cursos = cursosDTOAjax };
                resultado.Add(categoriaAjax);
            }

            return resultado.ToArray();
        }

        public ListadoCompletoViewModel CreateListadoCompletoViewModel(string userId)
        {
            CategoriaManagementService categoriaManagementService = new CategoriaManagementService();
            SeccionManagementService seccionesManagementService = new SeccionManagementService();
            CapituloManagementService capitulosManagementService = new CapituloManagementService();

            List<CursoDTO> cursos = this.CursoListToCursoDTOList(db.Cursos.Where(x => x.UsuarioId == userId).ToList());
            List <CategoriaDTO> categorias = categoriaManagementService.CategoriaListToCategoriaDTOList(db.Categoria.Where(x => x.UsuarioId == userId).ToList());
            List<SeccionDTO> secciones = seccionesManagementService.SeccionListToSeccionDTOList(db.Secciones.Where(x => x.UsuarioId == userId).ToList());
            List<CapituloDTO> capitulos = capitulosManagementService.CapituloListToCapituloDTOList(db.Capitulos.Where(x => x.UsuarioId == userId).ToList());

            return new ListadoCompletoViewModel(cursos, categorias, secciones, capitulos);
        }

        public CursoFormViewModel CreateFormViewModelInicializado(string userId)
        {
            CategoriaManagementService categoriaManagementService = new CategoriaManagementService();

            CursoFormViewModel model = new CursoFormViewModel();
            model.categorias = categoriaManagementService.CategoriaListToCategoriaDTOList(db.Categoria.Where(x => x.Activo).ToList());
            if (!String.IsNullOrEmpty(userId))
            {
                model.categorias.AddRange(categoriaManagementService.CategoriaListToCategoriaDTOList(db.Categoria.Where(x => x.UsuarioId == userId).ToList()));
            }
            model.categorias = model.categorias.Distinct().ToList();
            return model;
        }

        public string GuardarImagenPortada(HttpPostedFileBase file)
        {
            string resultado = null;

            if (file != null && file.ContentType.Contains("image"))
            {
                Guid guid = Guid.NewGuid();
                string idPortada = guid.ToString();
                string nombreArchivo = idPortada + Path.GetExtension(file.FileName);
                string rutaGuardar = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/img/Cursos/"), nombreArchivo);
                resultado = "/Content/img/Cursos/" + nombreArchivo;
                // file is uploaded
                file.SaveAs(rutaGuardar);
            }
            return resultado;
        }

        public CursoFormViewModel InicializarCategoriasNormal(CursoFormViewModel model)
        {
            CategoriaManagementService categoriaManagementService = new CategoriaManagementService();

            string userId = model.curso.UsuarioId;
            model.categorias = categoriaManagementService.CategoriaListToCategoriaDTOList(db.Categoria.Where(x => x.Activo).ToList());
            if (!String.IsNullOrEmpty(userId))
            {
                model.categorias.AddRange(categoriaManagementService.CategoriaListToCategoriaDTOList(db.Categoria.Where(x => x.UsuarioId == userId).ToList()));
            }
            model.categorias = model.categorias.Distinct().ToList();
            return model;
        }
        

        public void GeneralGuardarCursoFormViewModel(CursoFormViewModel model)
        {
            Curso curso = model.curso;
            Curso cursoOld = db.Cursos.Find(model.curso.Id);
            cursoOld.Titulo = curso.Titulo;
            cursoOld.CategoriaId = curso.CategoriaId;
            if (!String.IsNullOrEmpty(curso.Portada))
            {
                cursoOld.Portada = curso.Portada;
            }
            db.Entry(cursoOld).State = EntityState.Modified;
            db.SaveChanges();

            curso.Portada = cursoOld.Portada;
        }


        public CursoCompletoViewModel CreateCursoCompletoViewModel(int id)
        {
            SeccionManagementService seccionesManagementService = new SeccionManagementService();
            CapituloManagementService capitulosManagementService = new CapituloManagementService();

            CursoCompletoViewModel model = new CursoCompletoViewModel();
            CursoDTO curso = Mapper.Map<Curso, CursoDTO>(db.Cursos.Find(id));


            CategoriaDTO categoria = Mapper.Map<Categoria, CategoriaDTO>(db.Categoria.Where(x => x.Id == curso.CategoriaId).First());
            IList<SeccionDTO> secciones = seccionesManagementService.SeccionListToSeccionDTOList(db.Secciones.Where(x => x.Activo && x.CursoId == curso.Id).ToList());
            secciones.OrderBy(x => x.Posicion);

            List<Capitulo> capitulosCompleto = new List<Capitulo>();

            foreach (var item in secciones)
            {
                capitulosCompleto.AddRange(db.Capitulos.Where(x => x.Activo && x.SeccionId != null && x.SeccionId == item.Id));
            }

            IList < CapituloDTO > capitulos = capitulosManagementService.CapituloListToCapituloDTOList(capitulosCompleto);
            capitulos.OrderBy(x => x.Posicion);
            
            model.curso = curso;
            model.categoria = categoria;
            model.secciones = secciones;
            model.capitulos = capitulos;

            return model;
        }

    }


}