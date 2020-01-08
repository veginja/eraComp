using era_Computacion.Models.db;
using System;
using System.Collections.Generic;

namespace era_Computacion.Models
{
    public partial class solicitudCustomCliente
    {
        public solicitudCustomCliente()
        {

        }

        public Solicitud solicitud { get; set; }
        public List<Paquete> arreglo { get; set; }

    }
}
