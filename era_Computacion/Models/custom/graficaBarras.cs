using era_Computacion.Models.db;
using System;
using System.Collections.Generic;

namespace era_Computacion.Models
{
    public partial class graficaBarras
    {
        public graficaBarras()
        {

        }

        public Paquete paquete { get; set; }
        public int cantvendidos { get; set; }


    }
}
