using System;
using System.Collections.Generic;

namespace era_Computacion.Models.db
{
    public partial class Paquete
    {
        public Paquete()
        {
            Productosxpaquete = new HashSet<Productosxpaquete>();
            Solicitud = new HashSet<Solicitud>();
        }

        public int IdPaquete { get; set; }
        public string Nombre { get; set; }
        public float Precio { get; set; }
        public string Descripcion { get; set; }
        public string ImgPaquete { get; set; }
        public int RStatus { get; set; }
        public int? EsPersonalizado { get; set; }

        public Status RStatusNavigation { get; set; }
        public ICollection<Productosxpaquete> Productosxpaquete { get; set; }
        public ICollection<Solicitud> Solicitud { get; set; }
    }
}
