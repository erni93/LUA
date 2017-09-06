using System;
using System.ComponentModel;

namespace lua.Models.DTOS
{
    public class CapituloDTO
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        [DisplayName("Título")]
        public string Titulo { get; set; }

        public Nullable<int> SeccionId { get; set; }

        [DisplayName("Sección")]
        public string SeccionTitulo { get; set; }

        [DisplayName("Contenido")]
        public string ContenidoHTML { get; set; }

        public byte Posicion { get; set; }
        public string Portada { get; set; }
        public bool Activo { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as CapituloDTO;

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