using System;
using System.Collections.Generic;

namespace era_Computacion.Models.db
{
    public partial class Institucion
    {
        public Institucion()
        {
            Cliente = new HashSet<Cliente>();
        }

        public int IdInstitucion { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string PaginaWeb { get; set; }
        public string Descripcion { get; set; }
        public int RTipoIns { get; set; }
        public int RStatus { get; set; }

        public Status RStatusNavigation { get; set; }
        public Tipoinstitucion RTipoInsNavigation { get; set; }
        public ICollection<Cliente> Cliente { get; set; }
    }
}
