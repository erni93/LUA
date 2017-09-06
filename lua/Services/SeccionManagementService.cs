using AutoMapper;
using lua.Models;
using lua.Models.DTOS;
using lua.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace lua.Services
{
    public class SeccionManagementService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public SeccionViewModel CreateViewModel()
        {
            return new SeccionViewModel(SeccionListToSeccionDTOList());
        }
        public SeccionFormViewModel CreateFormViewModel(int id)
        {
            return new SeccionFormViewModel(db.Secciones.Find(id));
        }
        public List<SeccionDTO> SeccionListToSeccionDTOList()
        {
            List<Seccion> secciones = db.Secciones.ToList();
            List<SeccionDTO> seccionesDTO = new List<SeccionDTO>();
            foreach (var item in secciones)
            {
                seccionesDTO.Add(Mapper.Map<Seccion, SeccionDTO>(item));
            }
            return seccionesDTO;
        }

        public List<SeccionDTO> SeccionListToSeccionDTOList(List<Seccion> secciones)
        {
            List<SeccionDTO> seccionesDTO = new List<SeccionDTO>();
            foreach (var item in secciones)
            {
                seccionesDTO.Add(Mapper.Map<Seccion, SeccionDTO>(item));
            }
            return seccionesDTO;
        }


        public SeccionFormViewModel CreateFormViewModelInicializado(string userId)
        {
            CursoManagementService cursosManagementService = new CursoManagementService();

            SeccionFormViewModel model = new SeccionFormViewModel();
            model.cursos = cursosManagementService.CursoListToCursoDTOList(db.Cursos.Where(x => x.Activo).ToList());
            if (!String.IsNullOrEmpty(userId))
            {
                model.cursos.AddRange(cursosManagementService.CursoListToCursoDTOList(db.Cursos.Where(x => x.UsuarioId == userId).ToList()));
            }
            model.cursos = model.cursos.Distinct().ToList();
            return model;
        }

        public SeccionFormViewModel InicializarCursosNormal(SeccionFormViewModel model)
        {
            CursoManagementService cursosManagementService = new CursoManagementService();

            string userId = model.seccion.UsuarioId;
            model.cursos = cursosManagementService.CursoListToCursoDTOList(db.Cursos.Where(x => x.Activo).ToList());
            if (!String.IsNullOrEmpty(userId))
            {
                model.cursos.AddRange(cursosManagementService.CursoListToCursoDTOList(db.Cursos.Where(x => x.UsuarioId == userId).ToList()));
            }
            model.cursos = model.cursos.Distinct().ToList();
            return model;
        }

        public void GeneralGuardarSeccionFormViewModel(SeccionFormViewModel model)
        {
            Seccion seccion = model.seccion;
            Seccion seccionOld = db.Secciones.Find(model.seccion.Id);
            seccionOld.Titulo = seccion.Titulo;
            seccionOld.CursoId = seccion.CursoId;
            seccionOld.Posicion = seccion.Posicion;

            db.Entry(seccionOld).State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}