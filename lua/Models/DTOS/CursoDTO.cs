using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace lua.Models.DTOS
{
    public class CursoDTO
    {
        public int Id { get; set; }

        [DisplayName("Título")]
        public string Titulo { get; set; }
        public string UsuarioId { get; set; }

        [DisplayName("Fecha de creación")]
        public System.DateTime FechaCreacion { get; set; }

        public Nullable<int> CategoriaId { get; set; }

        [DisplayName("Categoría")]
        public string CategoriaNombre { get; set; }

        public string Portada { get; set; }

        [DisplayName("Hoja de Estilos")]
        public string HojaEstilos { get; set; }

        public bool Activo { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as CursoDTO;

            if (item == null)
            {
                return false;
            }

            return this.Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

    }

    public class CursoDTOAjaxListado
    {
        public string Titulo { get; set; }
        public string Portada { get; set; }
        public int Id { get; set; }
    }
}