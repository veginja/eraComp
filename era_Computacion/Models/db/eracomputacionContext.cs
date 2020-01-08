using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace era_Computacion.Models.db
{
    public partial class eracomputacionContext : DbContext
    {
        private string connectionString;

        public eracomputacionContext(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public eracomputacionContext(DbContextOptions<eracomputacionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CatProducto> CatProducto { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Detalleventa> Detalleventa { get; set; }
        public virtual DbSet<Infofacturacion> Infofacturacion { get; set; }
        public virtual DbSet<Institucion> Institucion { get; set; }
        public virtual DbSet<Paquete> Paquete { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<Productosxpaquete> Productosxpaquete { get; set; }
        public virtual DbSet<Solicitud> Solicitud { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Tipoinstitucion> Tipoinstitucion { get; set; }
        public virtual DbSet<Tipousuario> Tipousuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(connectionString);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatProducto>(entity =>
            {
                entity.HasKey(e => e.IdcatProducto);

                entity.ToTable("cat_producto");

                entity.HasIndex(e => e.RStatus)
                    .HasName("ct_cat_producto_status_idx");

                entity.Property(e => e.IdcatProducto)
                    .HasColumnName("idcat_producto")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.RStatus)
                    .HasColumnName("R_Status")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.RStatusNavigation)
                    .WithMany(p => p.CatProducto)
                    .HasForeignKey(d => d.RStatus)
                    .HasConstraintName("ct_cat_producto_status");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("cliente");

                entity.HasIndex(e => e.RInst)
                    .HasName("Cliente.Institucion_idx");

                entity.HasIndex(e => e.RStatus)
                    .HasName("Cliente.Status_idx");

                entity.HasIndex(e => e.RUsuario)
                    .HasName("Cliente.usuario_idx");

                entity.Property(e => e.IdCliente)
                    .HasColumnName("idCliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RInst)
                    .HasColumnName("R_inst")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RStatus)
                    .HasColumnName("R_Status")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RUsuario)
                    .HasColumnName("R_Usuario")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.RInstNavigation)
                    .WithMany(p => p.Cliente)
                    .HasForeignKey(d => d.RInst)
                    .HasConstraintName("Cliente_Institucion");

                entity.HasOne(d => d.RStatusNavigation)
                    .WithMany(p => p.Cliente)
                    .HasForeignKey(d => d.RStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Cliente_Status");

                entity.HasOne(d => d.RUsuarioNavigation)
                    .WithMany(p => p.Cliente)
                    .HasForeignKey(d => d.RUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Cliente_Usuario");
            });

            modelBuilder.Entity<Detalleventa>(entity =>
            {
                entity.HasKey(e => e.IdDetalleVenta);

                entity.ToTable("detalleventa");

                entity.HasIndex(e => e.RSolicitud)
                    .HasName("DetalleVenta.Venta_idx");

                entity.HasIndex(e => e.RStatus)
                    .HasName("DetalleVenta.Status_idx");

                entity.Property(e => e.IdDetalleVenta)
                    .HasColumnName("id_DetalleVenta")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(450)");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.RSolicitud)
                    .HasColumnName("R_Solicitud")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RStatus)
                    .HasColumnName("R_Status")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.RSolicitudNavigation)
                    .WithMany(p => p.Detalleventa)
                    .HasForeignKey(d => d.RSolicitud)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DetalleVenta_Solicitud");

                entity.HasOne(d => d.RStatusNavigation)
                    .WithMany(p => p.Detalleventa)
                    .HasForeignKey(d => d.RStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DetalleVenta_Status");
            });

            modelBuilder.Entity<Infofacturacion>(entity =>
            {
                entity.HasKey(e => e.IdInfoFac);

                entity.ToTable("infofacturacion");

                entity.HasIndex(e => e.RStatus)
                    .HasName("Factura_Status_idx");

                entity.HasIndex(e => e.RVentas)
                    .HasName("Factura_Venta_idx");

                entity.Property(e => e.IdInfoFac)
                    .HasColumnName("idInfoFac")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClaveFacturacion).HasColumnType("varchar(20)");

                entity.Property(e => e.DocFacturacion).HasColumnType("varchar(300)");

                entity.Property(e => e.FechaFacturacion)
                    .HasColumnName("Fecha_Facturacion")
                    .HasColumnType("date");

                entity.Property(e => e.RStatus)
                    .HasColumnName("R_Status")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RVentas)
                    .HasColumnName("R_Ventas")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.RStatusNavigation)
                    .WithMany(p => p.Infofacturacion)
                    .HasForeignKey(d => d.RStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Factura_Status");

                entity.HasOne(d => d.RVentasNavigation)
                    .WithMany(p => p.Infofacturacion)
                    .HasForeignKey(d => d.RVentas)
                    .HasConstraintName("Factura_Venta");
            });

            modelBuilder.Entity<Institucion>(entity =>
            {
                entity.HasKey(e => e.IdInstitucion);

                entity.ToTable("institucion");

                entity.HasIndex(e => e.RStatus)
                    .HasName("Institucion.Status_idx");

                entity.HasIndex(e => e.RTipoIns)
                    .HasName("Institucion.TipoInst_idx");

                entity.Property(e => e.IdInstitucion)
                    .HasColumnName("Id_Institucion")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(450)");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasColumnType("varchar(450)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(450)");

                entity.Property(e => e.PaginaWeb).HasColumnType("varchar(200)");

                entity.Property(e => e.RStatus)
                    .HasColumnName("R_Status")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RTipoIns)
                    .HasColumnName("R_TipoIns")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                entity.HasOne(d => d.RStatusNavigation)
                    .WithMany(p => p.Institucion)
                    .HasForeignKey(d => d.RStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Institucion_Status");

                entity.HasOne(d => d.RTipoInsNavigation)
                    .WithMany(p => p.Institucion)
                    .HasForeignKey(d => d.RTipoIns)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Institucion_TipoInst");
            });

            modelBuilder.Entity<Paquete>(entity =>
            {
                entity.HasKey(e => e.IdPaquete);

                entity.ToTable("paquete");

                entity.HasIndex(e => e.RStatus)
                    .HasName("Paquete.Status_idx");

                entity.Property(e => e.IdPaquete)
                    .HasColumnName("id_Paquete")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(450)");

                entity.Property(e => e.EsPersonalizado)
                    .HasColumnName("esPersonalizado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ImgPaquete)
                    .HasColumnName("img_paquete")
                    .HasColumnType("varchar(450)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.RStatus)
                    .HasColumnName("R_Status")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.RStatusNavigation)
                    .WithMany(p => p.Paquete)
                    .HasForeignKey(d => d.RStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Paquete_Status");
            });

            modelBuilder.Entity<Productos>(entity =>
            {
                entity.HasKey(e => e.IdProductos);

                entity.ToTable("productos");

                entity.HasIndex(e => e.RCatProducto)
                    .HasName("ct_producto_catproducto_idx");

                entity.HasIndex(e => e.RStatus)
                    .HasName("Productos.Status_idx");

                entity.Property(e => e.IdProductos)
                    .HasColumnName("id_Productos")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cantidad).HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.ImgProd)
                    .IsRequired()
                    .HasColumnName("Img_prod")
                    .HasColumnType("varchar(450)");

                entity.Property(e => e.NombreProd)
                    .IsRequired()
                    .HasColumnName("Nombre_Prod")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.PrecioCompraXunidad).HasColumnName("Precio_CompraXUnidad");

                entity.Property(e => e.PrecioVentaXunidad).HasColumnName("Precio_VentaXUnidad");

                entity.Property(e => e.RCatProducto)
                    .HasColumnName("R_CatProducto")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RStatus)
                    .HasColumnName("R_Status")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.RCatProductoNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.RCatProducto)
                    .HasConstraintName("ct_producto_catproducto");

                entity.HasOne(d => d.RStatusNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.RStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Productos.Status");
            });

            modelBuilder.Entity<Productosxpaquete>(entity =>
            {
                entity.HasKey(e => e.IdPxP);

                entity.ToTable("productosxpaquete");

                entity.HasIndex(e => e.RPaquetes)
                    .HasName("PxP_Paquetes_idx");

                entity.HasIndex(e => e.RProductos)
                    .HasName("PxP_Productos_idx");

                entity.HasIndex(e => e.RStatus)
                    .HasName("PxP_Status_idx");

                entity.Property(e => e.IdPxP)
                    .HasColumnName("idPxP")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cantidad).HasColumnType("int(11)");

                entity.Property(e => e.RPaquetes)
                    .HasColumnName("R_Paquetes")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RProductos)
                    .HasColumnName("R_Productos")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RStatus)
                    .HasColumnName("R_Status")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.RPaquetesNavigation)
                    .WithMany(p => p.Productosxpaquete)
                    .HasForeignKey(d => d.RPaquetes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PxP_Paquetes");

                entity.HasOne(d => d.RProductosNavigation)
                    .WithMany(p => p.Productosxpaquete)
                    .HasForeignKey(d => d.RProductos)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PxP_Productos");

                entity.HasOne(d => d.RStatusNavigation)
                    .WithMany(p => p.Productosxpaquete)
                    .HasForeignKey(d => d.RStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PxP_Status");
            });

            modelBuilder.Entity<Solicitud>(entity =>
            {
                entity.HasKey(e => e.IdSolicitud);

                entity.ToTable("solicitud");

                entity.HasIndex(e => e.RCliente)
                    .HasName("Venta.Cliente_idx");

                entity.HasIndex(e => e.RPaquete)
                    .HasName("Venta.Paquete_idx");

                entity.HasIndex(e => e.RStatus)
                    .HasName("Venta.Status_idx");

                entity.Property(e => e.IdSolicitud)
                    .HasColumnName("id_Solicitud")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FechaSolicitud)
                    .HasColumnName("Fecha_Solicitud")
                    .HasColumnType("date");

                entity.Property(e => e.InstalacionExtra).HasColumnName("Instalacion_Extra");

                entity.Property(e => e.PrecioSolicitud).HasColumnName("Precio_Solicitud");

                entity.Property(e => e.RCliente)
                    .HasColumnName("R_Cliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RPaquete)
                    .HasColumnName("R_Paquete")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RStatus)
                    .HasColumnName("R_Status")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.RClienteNavigation)
                    .WithMany(p => p.Solicitud)
                    .HasForeignKey(d => d.RCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Solicitud_Cliente");

                entity.HasOne(d => d.RPaqueteNavigation)
                    .WithMany(p => p.Solicitud)
                    .HasForeignKey(d => d.RPaquete)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Solicitud_Paquete");

                entity.HasOne(d => d.RStatusNavigation)
                    .WithMany(p => p.Solicitud)
                    .HasForeignKey(d => d.RStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Solicitud_Status");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus);

                entity.ToTable("status");

                entity.Property(e => e.IdStatus).HasColumnType("int(11)");

                entity.Property(e => e.ClaveStatus)
                    .IsRequired()
                    .HasColumnName("Clave_Status")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(450)");
            });

            modelBuilder.Entity<Tipoinstitucion>(entity =>
            {
                entity.HasKey(e => e.IdTipoInst);

                entity.ToTable("tipoinstitucion");

                entity.HasIndex(e => e.RStatus)
                    .HasName("TipoInst.Status_idx");

                entity.Property(e => e.IdTipoInst)
                    .HasColumnName("id_TipoInst")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(450)");

                entity.Property(e => e.RStatus)
                    .HasColumnName("R_Status")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.RStatusNavigation)
                    .WithMany(p => p.Tipoinstitucion)
                    .HasForeignKey(d => d.RStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TipoInst_Status");
            });

            modelBuilder.Entity<Tipousuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario);

                entity.ToTable("tipousuario");

                entity.HasIndex(e => e.RStatus)
                    .HasName("TipoUsuario.Status_idx");

                entity.Property(e => e.IdTipoUsuario)
                    .HasColumnName("id_TipoUsuario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClaveTipo)
                    .IsRequired()
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.RStatus)
                    .HasColumnName("R_Status")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.RStatusNavigation)
                    .WithMany(p => p.Tipousuario)
                    .HasForeignKey(d => d.RStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TipoUsuario_Status");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("usuario");

                entity.HasIndex(e => e.IdUsuario)
                    .HasName("id_usuario_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.RStatus)
                    .HasName("usuario.Status_idx");

                entity.HasIndex(e => e.RTipo)
                    .HasName("usuario.TipoUsuario_idx");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Celular).HasColumnType("varchar(20)");

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Direccion).HasColumnType("varchar(450)");

                entity.Property(e => e.ImgUsuario)
                    .HasColumnName("Img_usuario")
                    .HasColumnType("varchar(450)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.RStatus)
                    .HasColumnName("R_status")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RTipo)
                    .HasColumnName("R_tipo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Telefono).HasColumnType("varchar(20)");

                entity.HasOne(d => d.RStatusNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.RStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Usuario_Status");

                entity.HasOne(d => d.RTipoNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.RTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Usuario_TipoUsuario");
            });
        }
    }
}
