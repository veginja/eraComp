using System;
using System.Collections.Generic;

namespace era_Computacion.Models.db
{
    public partial class Tipoinstitucion
    {
        public Tipoinstitucion()
        {
            Institucion = new HashSet<Institucion>();
        }

        public int IdTipoInst { get; set; }
        public string Descripcion { get; set; }
        public int RStatus { get; set; }

        public Status RStatusNavigation { get; set; }
        public ICollection<Institucion> Institucion { get; set; }
    }
}
