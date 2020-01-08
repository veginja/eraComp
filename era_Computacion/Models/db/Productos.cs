using System;
using System.Collections.Generic;

namespace era_Computacion.Models.db
{
    public partial class Productos
    {
        public Productos()
        {
            Productosxpaquete = new HashSet<Productosxpaquete>();
        }

        public int IdProductos { get; set; }
        public string NombreProd { get; set; }
        public float PrecioCompraXunidad { get; set; }
        public float PrecioVentaXunidad { get; set; }
        public string Descripcion { get; set; }
        public string ImgProd { get; set; }
        public int RStatus { get; set; }
        public int? Cantidad { get; set; }
        public int? RCatProducto { get; set; }

        public CatProducto RCatProductoNavigation { get; set; }
        public Status RStatusNavigation { get; set; }
        public ICollection<Productosxpaquete> Productosxpaquete { get; set; }
    }
}
