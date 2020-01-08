using System;
using System.Collections.Generic;

namespace era_Computacion.Models.db
{
    public partial class Detalleventa
    {
        public Detalleventa()
        {
            Infofacturacion = new HashSet<Infofacturacion>();
        }

        public int IdDetalleVenta { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public int RSolicitud { get; set; }
        public int RStatus { get; set; }

        public Solicitud RSolicitudNavigation { get; set; }
        public Status RStatusNavigation { get; set; }
        public ICollection<Infofacturacion> Infofacturacion { get; set; }
    }
}
