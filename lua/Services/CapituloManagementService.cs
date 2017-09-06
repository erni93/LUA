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
    public class CapituloManagementService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public CapituloViewModel CreateViewModel()
        {
            return new CapituloViewModel(CapituloListToCapituloDTOList());
        }
        public CapituloFormViewModel CreateFormViewModel(int id)
        {
            return new CapituloFormViewModel(db.Capitulos.Find(id));
        }
        public List<CapituloDTO> CapituloListToCapituloDTOList()
        {
            List<Capitulo> capitulos = db.Capitulos.ToList();
            List<CapituloDTO> capitulosDTO = new List<CapituloDTO>();
            foreach (var item in capitulos)
            {
                capitulosDTO.Add(Mapper.Map<Capitulo, CapituloDTO>(item));
            }
            return capitulosDTO;
        }

        public List<CapituloDTO> CapituloListToCapituloDTOList(List<Capitulo> capitulos)
        {
            List<CapituloDTO> capitulosDTO = new List<CapituloDTO>();
            foreach (var item in capitulos)
            {
                capitulosDTO.Add(Mapper.Map<Capitulo, CapituloDTO>(item));
            }
            return capitulosDTO;
        }

        public CapituloFormViewModel CreateFormViewModelInicializado(string userId)
        {
            SeccionManagementService seccionManagementService = new SeccionManagementService();

            CapituloFormViewModel model = new CapituloFormViewModel();
            model.secciones = seccionManagementService.SeccionListToSeccionDTOList(db.Secciones.Where(x => x.Activo).ToList());
            if (!String.IsNullOrEmpty(userId))
            {
                model.secciones.AddRange(seccionManagementService.SeccionListToSeccionDTOList(db.Secciones.Where(x => x.UsuarioId == userId).ToList()));
            }
            model.secciones = model.secciones.Distinct().ToList();
            return model;
        }


        public CapituloFormViewModel InicializarSeccionesNormal(CapituloFormViewModel model)
        {
            SeccionManagementService seccionManagementService = new SeccionManagementService();

            string userId = model.capitulo.UsuarioId;
            model.secciones = seccionManagementService.SeccionListToSeccionDTOList(db.Secciones.Where(x => x.Activo).ToList());
            if (!String.IsNullOrEmpty(userId))
            {
                model.secciones.AddRange(seccionManagementService.SeccionListToSeccionDTOList(db.Secciones.Where(x => x.UsuarioId == userId).ToList()));
            }
            model.secciones = model.secciones.Distinct().ToList();
            return model;
        }

        public void GeneralGuardarCapituloFormViewModel(CapituloFormViewModel model)
        {
            Capitulo capitulo = model.capitulo;
            Capitulo capituloOld = db.Capitulos.Find(model.capitulo.Id);

            capituloOld.Titulo = capitulo.Titulo;
            capituloOld.SeccionId = capitulo.SeccionId;
            capituloOld.Posicion = capitulo.Posicion;
            capituloOld.ContenidoHTML = capitulo.ContenidoHTML;

            db.Entry(capituloOld).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}