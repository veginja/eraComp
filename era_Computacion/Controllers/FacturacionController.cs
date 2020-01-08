using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using era_Computacion.Models.db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace era_Computacion.Controllers
{
    public class FacturacionController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        public FacturacionController(IHostingEnvironment enviroment)
        {
            _hostingEnvironment = enviroment;
        }

        [Authorize]
        public IActionResult ListFacturas()
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var facturacion = context.Infofacturacion.ToList();
            foreach (var item in facturacion)
            {
                var detalleVenta = context.Detalleventa.Where(data => data.IdDetalleVenta == item.RVentas).FirstOrDefault();
                item.RVentasNavigation = detalleVenta;
                var solicitud = context.Solicitud.Where(data => data.IdSolicitud == detalleVenta.RSolicitud).FirstOrDefault();
                item.RVentasNavigation.RSolicitudNavigation = solicitud;
                var cliente = context.Cliente.Where(data => data.IdCliente == solicitud.RCliente).FirstOrDefault();
                item.RVentasNavigation.RSolicitudNavigation.RClienteNavigation = cliente;
                var institucion = context.Institucion.Where(data => data.IdInstitucion == cliente.RInst).FirstOrDefault();
                item.RVentasNavigation.RSolicitudNavigation.RClienteNavigation.RInstNavigation = institucion;
            }

            return View(facturacion);
        }

        [Authorize]
        public IActionResult agregarFactura()
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            ViewBag.pedidos = context.Detalleventa.Where(data => data.RStatus == 8).Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.Descripcion+" | "+parm.Fecha +" | "+parm.RSolicitudNavigation.RClienteNavigation.RInstNavigation.Nombre+ " | "+parm.RSolicitudNavigation.RClienteNavigation.RUsuarioNavigation.Nombre,
                Value = parm.IdDetalleVenta.ToString()
            });

            return View();
        }

        [HttpPost]
        [Authorize]
        //Metodo para crear un cliente
        public async Task<IActionResult> agregarFactura(Infofacturacion e, List<IFormFile> files)
        {

            //crea contexto a la db
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            //Verifica si ese cliente no existe
            var usuarioExistente = context.Infofacturacion.FirstOrDefault(usr => usr.ClaveFacturacion.ToLower() == e.ClaveFacturacion.ToLower());
            if (usuarioExistente == null)
            {
                //Si no existe se crea el cliente
                e.RStatus = 1;
                e.FechaFacturacion = DateTime.Now;

                var detalleVenta = context.Detalleventa.Where(data => data.IdDetalleVenta == e.RVentas).FirstOrDefault();
                detalleVenta.RStatus = 9;
                long sum = files.Sum(f => f.Length);
                if (sum > 0)
                {


                    var filePath = Path.GetTempFileName();

                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images/facturas");
                    foreach (var formFile in files)
                    {


                        if (formFile.Length > 0)
                        {
                            string guid = Guid.NewGuid().ToString() + ".pdf";
                            using (var fileStream = new FileStream(Path.Combine(uploads, guid), FileMode.Create))
                            {

                                await formFile.CopyToAsync(fileStream);
                                e.DocFacturacion = guid;
                            }
                        }
                    }
                }
                else
                {
                    e.DocFacturacion = "era-logo-icon.png";
                }
                context.Infofacturacion.Add(e);
                context.SaveChanges();

                context.SaveChanges();

                TempData["mensaje"] = "addFactura";
                return RedirectToAction("ListFacturas", "Facturacion");
            }
            else
            {
                //Si existe se devuelve un mensaje de error
                TempData["mensaje"] = "facturaExistente";
                return RedirectToAction("ListFacturas", "Facturacion");
            }

        }



        [Authorize]
        [HttpPost]
        //Método que desactiva a un usuario creado, se invoca por medio de jQuery
        public JsonResult desactivateFactura(int? id)
        {

            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            if (id == null)
                return Json(data: "No desactivado");

            var datos = context.Infofacturacion.FirstOrDefault(usr => usr.IdInfoFac == id) as Infofacturacion;
            datos.RStatus = 2;


            context.Infofacturacion.Update(datos);
            context.SaveChanges();
            return Json(data: "Desactivado");


        }



        [Authorize]
        [HttpPost]

        //Método que activa a un usuario creado, se invoca por medio de jQuery
        public JsonResult activateFactura(int? id)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            if (id == null)
                return Json(data: "No activado");

            var datos = context.Infofacturacion.FirstOrDefault(usr => usr.IdInfoFac == id) as Infofacturacion;
            datos.RStatus = 1;


            context.Infofacturacion.Update(datos);
            context.SaveChanges();
            return Json(data: "Activado");

        }


        [Authorize]
        public IActionResult editarFactura(int? id)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            ViewBag.pedidos = context.Detalleventa.Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.Descripcion + " | " + parm.Fecha + " | " + parm.RSolicitudNavigation.RClienteNavigation.RInstNavigation.Nombre + " | " + parm.RSolicitudNavigation.RClienteNavigation.RUsuarioNavigation.Nombre,
                Value = parm.IdDetalleVenta.ToString()
            });

            var factura = context.Infofacturacion.Where(data => data.IdInfoFac == id).FirstOrDefault();
            return View(factura);
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        //Método que realiza la edicion de un usuario utilizando un objeto de tipo usuario
        public async Task<IActionResult> editarFactura(Infofacturacion e, List<IFormFile> files)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var entity = context.Infofacturacion.FirstOrDefault(usr => usr.IdInfoFac == e.IdInfoFac);

            if (entity != null)
            {

                entity.ClaveFacturacion = e.ClaveFacturacion;
                entity.FechaFacturacion = DateTime.Now;

                entity.RVentas = e.RVentas;


                long sum = files.Sum(f => f.Length);
                if (sum > 0)
                {
                    var filePath = Path.GetTempFileName();

                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images/facturas");
                    foreach (var formFile in files)
                    {


                        if (formFile.Length > 0)
                        {
                            string guid = Guid.NewGuid().ToString() + ".pdf";
                            using (var fileStream = new FileStream(Path.Combine(uploads, guid), FileMode.Create))
                            {

                                await formFile.CopyToAsync(fileStream);
                                entity.DocFacturacion = guid;
                            }
                        }
                    }
                }

                context.Infofacturacion.Update(entity);
                context.SaveChanges();
                TempData["mensaje"] = "facturaEdit";
                return RedirectToAction("ListFacturas", "Facturacion");

            }
            else
            {
                TempData["mensaje"] = "ErrorFacturaEdit";
                return RedirectToAction("ListFacturas", "Facturacion");
            }


        }




    }
}