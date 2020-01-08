using era_Computacion.Models.db;
using System;
using System.Collections.Generic;

namespace era_Computacion.Models
{
    public partial class paqueteCustom2
    {
        public paqueteCustom2()
        {

        }

        public Productos producto { get; set; }
        public Productosxpaquete productosporpaquete { get; set; }

    }
}
