using era_Computacion.Models.db;
using System;
using System.Collections.Generic;

namespace era_Computacion.Models
{
    public partial class paqueteCustom
    {
        public paqueteCustom()
        {

        }

        public Paquete paquete { get; set; }
        public List<paqueteCustom2> arreglo { get; set; }

    }
}
