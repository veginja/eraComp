using System;
using System.Collections.Generic;

namespace era_Computacion.Models.db
{
    public partial class Infofacturacion
    {
        public int IdInfoFac { get; set; }
        public string ClaveFacturacion { get; set; }
        public string DocFacturacion { get; set; }
        public DateTime? FechaFacturacion { get; set; }
        public int? RVentas { get; set; }
        public int RStatus { get; set; }

        public Status RStatusNavigation { get; set; }
        public Detalleventa RVentasNavigation { get; set; }
    }
}
