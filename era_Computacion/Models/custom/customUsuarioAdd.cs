using era_Computacion.Models.db;
using System;
using System.Collections.Generic;

namespace era_Computacion.Models
{
    public partial class ClienteCustom
    {
        public ClienteCustom()
        {

        }

        public int id { get; set; }
        public int estatusid { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string paquete { get; set; }
        public string estatus { get; set; }
        public List<Institucion> instituciones { get; set; }

    }
}
