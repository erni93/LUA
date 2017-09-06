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
    public class CategoriaManagementService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public CategoriaViewModel CreateViewModel()
        {
            return new CategoriaViewModel(CategoriaListToCategoriaDTOList());
        }
        public CategoriaFormViewModel CreateFormViewModel(int id)
        {
            return new CategoriaFormViewModel(db.Categoria.Find(id));
        }
        public List<CategoriaDTO> CategoriaListToCategoriaDTOList()
        {
            List<Categoria> categorias = db.Categoria.ToList();
            List<CategoriaDTO> categoriasDTO = new List<CategoriaDTO>();
            foreach (var item in categorias)
            {
                categoriasDTO.Add(Mapper.Map<Categoria, CategoriaDTO>(item));
            }
            return categoriasDTO;
        }


        public List<CategoriaDTO> CategoriaListToCategoriaDTOList(List<Categoria> categorias)
        {
            List<CategoriaDTO> categoriasDTO = new List<CategoriaDTO>();
            foreach (var item in categorias)
            {
                categoriasDTO.Add(Mapper.Map<Categoria, CategoriaDTO>(item));
            }
            return categoriasDTO;
        }

        public void GenerarGuardarCategoriaFormViewModel(CategoriaFormViewModel model)
        {
            Categoria categoria = model.categoria;
            Categoria categoriaOld = db.Categoria.Find(model.categoria.Id);
            categoriaOld.Nombre = categoria.Nombre;
            db.Entry(categoriaOld).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}