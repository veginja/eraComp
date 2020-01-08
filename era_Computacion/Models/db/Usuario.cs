using System;
using System.Collections.Generic;

namespace era_Computacion.Models.db
{
    public partial class Usuario
    {
        public Usuario()
        {
            Cliente = new HashSet<Cliente>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string ImgUsuario { get; set; }
        public int RTipo { get; set; }
        public int RStatus { get; set; }

        public Status RStatusNavigation { get; set; }
        public Tipousuario RTipoNavigation { get; set; }
        public ICollection<Cliente> Cliente { get; set; }
    }
}
