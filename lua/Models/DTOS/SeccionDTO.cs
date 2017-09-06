using System;
using System.ComponentModel;

namespace lua.Models.DTOS
{
    public class SeccionDTO
    {
       
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        [DisplayName("Título")]
        public string Titulo { get; set; }
        public Nullable<int> CursoId { get; set; }

        [DisplayName("Curso")]
        public string CursoTitulo { get; set; }
        public byte Posicion { get; set; }
        public string Portada { get; set; }

        public bool Activo { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as SeccionDTO;

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
}