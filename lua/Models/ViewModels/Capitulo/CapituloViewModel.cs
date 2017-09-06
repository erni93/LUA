using lua.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lua.Models.ViewModels
{
    public class CapituloViewModel
    {
        public CapituloViewModel(List<CapituloDTO> capitulos)
        {
            this.capitulos = capitulos;
        }
        public List<CapituloDTO> capitulos { get; set; }
    }
}