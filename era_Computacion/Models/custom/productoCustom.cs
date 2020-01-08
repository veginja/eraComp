using era_Computacion.Models.db;
using System;
using System.Collections.Generic;

namespace era_Computacion.Models
{
    public partial class productoCustom
    {
        public productoCustom()
        {

        }

        public Productos producto { get; set; }
        public CatProducto categoriaProducto { get; set; }

    }
}
