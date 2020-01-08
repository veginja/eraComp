using System;
using System.Collections.Generic;

namespace era_Computacion.Models.db
{
    public partial class Productosxpaquete
    {
        public int IdPxP { get; set; }
        public int RPaquetes { get; set; }
        public int RProductos { get; set; }
        public int RStatus { get; set; }
        public int? Cantidad { get; set; }

        public Paquete RPaquetesNavigation { get; set; }
        public Productos RProductosNavigation { get; set; }
        public Status RStatusNavigation { get; set; }
    }
}
