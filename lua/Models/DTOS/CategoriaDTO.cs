using System;
using System.ComponentModel;

namespace lua.Models.DTOS
{
    public class CategoriaDTO
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as CategoriaDTO;

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

    public class CategoriaAjaxDTO
    {
        public string Nombre { get; set; }
        public CursoDTOAjaxListado[] cursos { get; set; }
    }
}