using lua.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lua.Models.ViewModels
{
    public class CursoViewModel
    {
        public CursoViewModel(List<CursoDTO> cursos)
        {
            this.cursos = cursos;
        }
        public List<CursoDTO> cursos { get; set; }
    }

    public class CursoCompletoViewModel
    {
        public CursoDTO curso { get; set; }
        public CategoriaDTO categoria { get; set; }
        public IList<SeccionDTO> secciones { get; set; }
        public IList<CapituloDTO> capitulos { get; set; }


        public bool IsValid()
        {
            foreach (var item in secciones)
            {
                if (item == null) { return false; }
            }
            foreach (var item in capitulos)
            {
                if (item == null) { return false; }
            }
            if (curso == null && categoria == null)
            {
                return false;
            }
            return true;
        }
    }

}