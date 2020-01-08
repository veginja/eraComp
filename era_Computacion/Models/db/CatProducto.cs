using System;
using System.Collections.Generic;

namespace era_Computacion.Models.db
{
    public partial class CatProducto
    {
        public CatProducto()
        {
            Productos = new HashSet<Productos>();
        }

        public int IdcatProducto { get; set; }
        public string Nombre { get; set; }
        public int? RStatus { get; set; }

        public Status RStatusNavigation { get; set; }
        public ICollection<Productos> Productos { get; set; }
    }
}
