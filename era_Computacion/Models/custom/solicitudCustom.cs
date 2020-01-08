using era_Computacion.Models.db;
using System;
using System.Collections.Generic;

namespace era_Computacion.Models
{
    public partial class solicitudCustom
    {
        public solicitudCustom()
        {

        }

        public Solicitud solicitud { get; set; }
        public List<paqueteCustom2> arreglo { get; set; }

        public Infofacturacion factura { get; set; }

    }
}
