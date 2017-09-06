using lua.Models.DTOS;
using System.Collections.Generic;

namespace lua.Models.ViewModels
{
    public class SeccionFormViewModel
    {

        public SeccionFormViewModel()
        {

        }
        public SeccionFormViewModel(Seccion seccion)
        {
            this.seccion = seccion;
        }
        public Seccion seccion { get; set; }
        public List<CursoDTO> cursos { get; set; }
}
}