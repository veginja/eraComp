using System;
using System.Collections.Generic;

namespace era_Computacion.Models.db
{
    public partial class Status
    {
        public Status()
        {
            CatProducto = new HashSet<CatProducto>();
            Cliente = new HashSet<Cliente>();
            Detalleventa = new HashSet<Detalleventa>();
            Infofacturacion = new HashSet<Infofacturacion>();
            Institucion = new HashSet<Institucion>();
            Paquete = new HashSet<Paquete>();
            Productos = new HashSet<Productos>();
            Productosxpaquete = new HashSet<Productosxpaquete>();
            Solicitud = new HashSet<Solicitud>();
            Tipoinstitucion = new HashSet<Tipoinstitucion>();
            Tipousuario = new HashSet<Tipousuario>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdStatus { get; set; }
        public string ClaveStatus { get; set; }
        public string Descripcion { get; set; }

        public ICollection<CatProducto> CatProducto { get; set; }
        public ICollection<Cliente> Cliente { get; set; }
        public ICollection<Detalleventa> Detalleventa { get; set; }
        public ICollection<Infofacturacion> Infofacturacion { get; set; }
        public ICollection<Institucion> Institucion { get; set; }
        public ICollection<Paquete> Paquete { get; set; }
        public ICollection<Productos> Productos { get; set; }
        public ICollection<Productosxpaquete> Productosxpaquete { get; set; }
        public ICollection<Solicitud> Solicitud { get; set; }
        public ICollection<Tipoinstitucion> Tipoinstitucion { get; set; }
        public ICollection<Tipousuario> Tipousuario { get; set; }
        public ICollection<Usuario> Usuario { get; set; }
    }
}
