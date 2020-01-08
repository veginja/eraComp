using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using era_Computacion.Models.db;
using era_Computacion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace era_Computacion.Controllers
{
    public class HistorialController : Controller
    {
        [Authorize]
        public IActionResult HistorialPedidos()
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var clientes = context.Cliente.Where(data => data.RUsuario == HttpContext.Session.GetInt32("Id_user")).ToList();
            var solicitud = context.Solicitud.Where(data => clientes.Any(t1 => data.RCliente == t1.IdCliente)).ToList();
            List<solicitudCustom> listSolicitud = new List<solicitudCustom>();
            foreach (var item in solicitud)
            {
                solicitudCustom solicitudC = new solicitudCustom();

                var paquete = context.Paquete.Where(data => data.IdPaquete == item.RPaquete).FirstOrDefault();
                var productos = context.Productosxpaquete.Where(data => data.RPaquetes == paquete.IdPaquete).ToList();
                if (productos.Count() > 0)
                {
                    List<paqueteCustom2> productoObjeto = new List<paqueteCustom2>();
                    foreach (var itemP in productos)
                    {
                        paqueteCustom2 paquetecustom2 = new paqueteCustom2();
                        var producto = context.Productos.Where(data => data.IdProductos == itemP.RProductos).FirstOrDefault();

                        paquetecustom2.productosporpaquete = itemP;
                        paquetecustom2.producto = producto;
                        productoObjeto.Add(paquetecustom2);
                    }
                    solicitudC.arreglo = productoObjeto;
                }

                item.RPaqueteNavigation = paquete;
                var cliente = context.Cliente.Where(data => data.IdCliente == item.RCliente && data.RStatus == 1).FirstOrDefault();
                var institucion = context.Institucion.Where(data => data.IdInstitucion == cliente.RInst).FirstOrDefault();
                cliente.RInstNavigation = institucion;
                var usuario = context.Usuario.Where(data => data.IdUsuario == cliente.RUsuario).FirstOrDefault();
                cliente.RUsuarioNavigation = usuario;
                item.RClienteNavigation = cliente;

                var status = context.Status.Where(data => data.IdStatus == item.RStatus).FirstOrDefault();
                item.RStatusNavigation = status;

                var detalleventa = context.Detalleventa.Where(data => data.RSolicitud == item.IdSolicitud).FirstOrDefault();
                if (detalleventa != null)
                {
                    if (detalleventa.RStatus == 9)
                    {
                        var factura = context.Infofacturacion.Where(data => data.RVentas == detalleventa.IdDetalleVenta).FirstOrDefault();
                        solicitudC.factura = factura;
                    }
                    else
                        solicitudC.factura = null;
                } else
                    solicitudC.factura = null;

                solicitudC.solicitud = item;
                listSolicitud.Add(solicitudC);
            }

            return View(listSolicitud);
        }


        [Authorize]
        [HttpPost]
        public JsonResult verDetalle(int id)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var paquete = context.Paquete.Where(data => data.IdPaquete == id ).FirstOrDefault();
            string result = "<h2>"+paquete.Nombre+ "</h2><table  class='table table-striped'><thead class='thead-light'><tr><th scope='col'>Producto</th><th scope='col'>Cantidad</th><th scope='col'>Monto</th></tr></thead><tbody>";

            var productospaquete = context.Productosxpaquete.Where(data => data.RPaquetes == paquete.IdPaquete).ToList();

            foreach (var item in productospaquete)
            {
                var producto = context.Productos.Where(data => data.IdProductos == item.RProductos).FirstOrDefault();

                result += "<tr><td>"+producto.NombreProd+"</td><td>" + item.Cantidad + "</td><td>$" + producto.PrecioVentaXunidad*item.Cantidad + "</td></tr>";
            }
            result += "</tbody></table>";


            return Json(result);

        }

    }
}