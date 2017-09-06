using lua.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lua.Models.ViewModels
{
    public class CategoriaViewModel
    {
        public CategoriaViewModel(List<CategoriaDTO> categorias)
        {
            this.categorias = categorias;
        }
        public List<CategoriaDTO> categorias { get; set; }
    }
}