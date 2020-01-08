using System;
using System.Collections.Generic;

namespace era_Computacion.Models.db
{
    public partial class Solicitud
    {
        public Solicitud()
        {
            Detalleventa = new HashSet<Detalleventa>();
        }

        public int IdSolicitud { get; set; }
        public int RCliente { get; set; }
        public int RPaquete { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public float PrecioSolicitud { get; set; }
        public int RStatus { get; set; }
        public float? InstalacionExtra { get; set; }

        public Cliente RClienteNavigation { get; set; }
        public Paquete RPaqueteNavigation { get; set; }
        public Status RStatusNavigation { get; set; }
        public ICollection<Detalleventa> Detalleventa { get; set; }
    }
}
