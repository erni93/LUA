using lua.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lua.Models.ViewModels
{
    public class SeccionViewModel
    {
        public SeccionViewModel(List<SeccionDTO> secciones)
        {
            this.secciones = secciones;
        }
        public List<SeccionDTO> secciones { get; set; }
       
    }
}