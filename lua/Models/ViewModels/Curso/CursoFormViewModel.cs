using lua.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lua.Models.ViewModels
{
    public class CursoFormViewModel
    {
        public CursoFormViewModel()
        {

        }
        public CursoFormViewModel(Curso curso)
        {
            this.curso = curso;
        }
        public Curso curso { get; set; }
        public List<CategoriaDTO> categorias { get; set; }
        public HttpPostedFileBase imagenPortada { get; set; }
    }

   
}