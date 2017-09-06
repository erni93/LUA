using lua.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lua.Models.ViewModels
{
    public class CapituloFormViewModel
    {
        public CapituloFormViewModel()
        {

        }
        public CapituloFormViewModel(Capitulo capitulo)
        {
            this.capitulo = capitulo;
        }
        public Capitulo capitulo { get; set; }
        public List<SeccionDTO> secciones { get; set; }
    }
}