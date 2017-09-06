using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lua.Models.ViewModels
{
    public class CategoriaFormViewModel
    {
        public CategoriaFormViewModel() {
        
        }
        public CategoriaFormViewModel(Categoria categoria)
        {
            this.categoria = categoria;
        }
        public Categoria categoria { get; set; }
    }
}