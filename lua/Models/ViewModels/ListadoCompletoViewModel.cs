using lua.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lua.Models.ViewModels
{
    public class ListadoCompletoViewModel
    {
        public ListadoCompletoViewModel(List<CursoDTO> cursos, List<CategoriaDTO> categorias, List<SeccionDTO> secciones, List<CapituloDTO> capitulos)
        {
            this.cursos = cursos;
            this.categorias = categorias;
            this.secciones = secciones;
            this.capitulos = capitulos;
        }
        public List<CursoDTO> cursos { get; set; }
        public List<CategoriaDTO> categorias { get; set; }
        public List<SeccionDTO> secciones { get; set; }
        public List<CapituloDTO> capitulos { get; set; }
    }

}