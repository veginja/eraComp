using System;
using System.Collections.Generic;

namespace era_Computacion.Models.db
{
    public partial class Tipousuario
    {
        public Tipousuario()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int IdTipoUsuario { get; set; }
        public string ClaveTipo { get; set; }
        public string Descripcion { get; set; }
        public int RStatus { get; set; }

        public Status RStatusNavigation { get; set; }
        public ICollection<Usuario> Usuario { get; set; }
    }
}
