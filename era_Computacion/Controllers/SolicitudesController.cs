using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using era_Computacion.Models;
using era_Computacion.Models.db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace era_Computacion.Controllers
{
    public class SolicitudesController : Controller
    {

        [Authorize]
        public IActionResult ListSolicitudes()
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var solicitud = context.Solicitud.ToList();
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
                var cliente = context.Cliente.Where(data => data.IdCliente == item.RCliente && data.RStatus==1).FirstOrDefault();
                var institucion = context.Institucion.Where(data => data.IdInstitucion == cliente.RInst).FirstOrDefault();
                cliente.RInstNavigation = institucion;
                var usuario = context.Usuario.Where(data => data.IdUsuario == cliente.RUsuario).FirstOrDefault();
                cliente.RUsuarioNavigation = usuario;
                item.RClienteNavigation = cliente;
                
                var status = context.Status.Where(data => data.IdStatus == item.RStatus).FirstOrDefault();
                item.RStatusNavigation = status;
                solicitudC.solicitud = item;
                listSolicitud.Add(solicitudC);
            }

            return View(listSolicitud);
        }

        [Authorize]
        public IActionResult agregarSolicitud()
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            ViewBag.camaras = context.Productos.Where(data => data.RCatProducto == 1).Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.RStatus == 1 ? parm.NombreProd : parm.NombreProd + " | No disponible",
                Value = parm.IdProductos.ToString()
            });

            ViewBag.dvrs = context.Productos.Where(data => data.RCatProducto == 2).Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.RStatus == 1 ? parm.NombreProd : parm.NombreProd + " | No disponible",
                Value = parm.IdProductos.ToString()
            });

            ViewBag.discoduro = context.Productos.Where(data => data.RCatProducto == 3).Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.RStatus == 1 ? parm.NombreProd : parm.NombreProd + " | No disponible",
                Value = parm.IdProductos.ToString()
            });

            ViewBag.instalacion = context.Productos.Where(data => data.RCatProducto == 4).Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.RStatus == 1 ? parm.NombreProd : parm.NombreProd + " | No disponible",
                Value = parm.IdProductos.ToString()

            });
            ViewBag.usuarios = context.Usuario.Where(data => data.RStatus == 1 && data.RTipo==1).Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.Nombre+" "+ parm.Apellidos+" | "+parm.NombreUsuario,
                Value = parm.IdUsuario.ToString()
            });

            ViewBag.paquetes = context.Paquete.Where(data => data.RStatus == 1 && data.EsPersonalizado==2).Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.Nombre,
                Value = parm.IdPaquete.ToString()
            });

            return View();
        }


        [Authorize]
        [HttpPost]
        public JsonResult getInstitucion(int id)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var clientes = context.Cliente.Where(data => data.RUsuario == id &&data.RStatus==1).ToList();
            string result = "";

            foreach (var item in clientes)
            {
                var institucion = context.Institucion.Where(data => data.IdInstitucion == item.RInst).FirstOrDefault();
                result += "<option value='" + item.IdCliente + "'>" + institucion.Nombre + "</option>";
            }
           

            return Json(result);

        }

        [HttpPost]
        [Authorize]
        //Metodo para crear un cliente
        public IActionResult agregarSolicitud(Solicitud e, int camaras, int cantcamaras, int dvr, int cantdvr, int discoduro, int cantdiscoduro, int instalacion, int cantinstalacion)
        {

            //crea contexto a la db
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            e.FechaSolicitud = DateTime.Now;
            e.RStatus = 4;
            if (e.RPaquete == 0)
            {
                var paquetesCustom = context.Paquete.Where(data => data.EsPersonalizado == 1).ToList();
                foreach(var item in paquetesCustom)
                {
                    Productosxpaquete verificaCamara = context.Productosxpaquete.Where(data => data.Cantidad == cantcamaras && data.RProductos == camaras && item.IdPaquete==data.RPaquetes).FirstOrDefault();
                    Productosxpaquete verificaDVR = context.Productosxpaquete.Where(data => data.Cantidad == cantdvr && data.RProductos == dvr && item.IdPaquete == data.RPaquetes).FirstOrDefault();
                    Productosxpaquete verificaHDD = context.Productosxpaquete.Where(data => data.Cantidad == cantdiscoduro && data.RProductos == discoduro && item.IdPaquete == data.RPaquetes).FirstOrDefault();
                    Productosxpaquete verificaInstalacion = context.Productosxpaquete.Where(data => data.Cantidad == cantinstalacion && data.RProductos == instalacion && item.IdPaquete == data.RPaquetes).FirstOrDefault();

                    if (verificaCamara != null && verificaDVR != null && verificaHDD != null && verificaInstalacion != null)
                    {
                        var getPaquete = verificaCamara.RPaquetes;
                        e.RPaquete = getPaquete;
                        context.Solicitud.Add(e);
                        context.SaveChanges();
                        TempData["mensaje"] = "addSolicitud";
                        return RedirectToAction("ListSolicitudes", "Solicitudes");
                    }
                }


                Paquete paqueteCustom = new Paquete();
                paqueteCustom.Nombre = "Paquete Personalizado";
                paqueteCustom.Descripcion = "Paquete Personalizado";
                paqueteCustom.ImgPaquete = "era-logo-icon.png";
                paqueteCustom.RStatus = 1;
                paqueteCustom.EsPersonalizado = 1;
                paqueteCustom.Precio = e.PrecioSolicitud;
                context.Paquete.Add(paqueteCustom);
                context.SaveChanges();

                Paquete lastPaquete = context.Paquete.LastOrDefault();

                Productosxpaquete camaraP = new Productosxpaquete();
                camaraP.Cantidad = cantcamaras;
                camaraP.RPaquetes = lastPaquete.IdPaquete;
                camaraP.RProductos = camaras;
                camaraP.RStatus = 1;

                Productosxpaquete dvrP = new Productosxpaquete();
                dvrP.Cantidad = cantdvr;
                dvrP.RPaquetes = lastPaquete.IdPaquete;
                dvrP.RProductos = dvr;
                dvrP.RStatus = 1;

                Productosxpaquete hdP = new Productosxpaquete();
                hdP.Cantidad = cantdiscoduro;
                hdP.RPaquetes = lastPaquete.IdPaquete;
                hdP.RProductos = discoduro;
                hdP.RStatus = 1;

                Productosxpaquete instalacionP = new Productosxpaquete();
                instalacionP.Cantidad = cantinstalacion;
                instalacionP.RPaquetes = lastPaquete.IdPaquete;
                instalacionP.RProductos = instalacion;
                instalacionP.RStatus = 1;

                context.Productosxpaquete.Add(camaraP);
                context.Productosxpaquete.Add(dvrP);
                context.Productosxpaquete.Add(hdP);
                context.Productosxpaquete.Add(instalacionP);
                context.SaveChanges();

                e.RPaquete = lastPaquete.IdPaquete;
                context.Solicitud.Add(e);
                context.SaveChanges();
                TempData["mensaje"] = "addSolicitudCustom";
                return RedirectToAction("ListSolicitudes", "Solicitudes");
            }
            else
            {
                context.Solicitud.Add(e);
                context.SaveChanges();
                TempData["mensaje"] = "addSolicitud";
                return RedirectToAction("ListSolicitudes", "Solicitudes");
            }
            


        }


        [Authorize]
        [HttpPost]
        //Método que desactiva a un usuario creado, se invoca por medio de jQuery
        public JsonResult establecerPendiente(int? id)
        {

            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            if (id == null) {
                TempData["mensaje"] = "error";
                return Json(data: "Pendiente");
            }

            var datos = context.Solicitud.FirstOrDefault(usr => usr.IdSolicitud == id) as Solicitud;
            if (datos.RStatus == 6)
            {
                TempData["mensaje"] = "pendienteAceptado";
                return Json(data: "Pendiente");
            }
                
            datos.RStatus = 4;

            using (var message = new MailMessage())
            {
                var cliente = context.Cliente.Where(data => data.IdCliente == datos.RCliente).FirstOrDefault();
                var usuario = context.Usuario.Where(data => data.IdUsuario == cliente.RUsuario).FirstOrDefault();
                var paquete = context.Paquete.Where(data => data.IdPaquete == datos.RPaquete).FirstOrDefault();
                var date2 = DateTime.Now;
   
                    message.To.Add(new MailAddress(usuario.Correo));

                message.From = new MailAddress("eracomputacion2019@gmail.com", "Rafael Palomo");
                //message.CC.Add(new MailAddress("cc@email.com", "CC Name"));
                //message.Bcc.Add(new MailAddress("bcc@email.com", "BCC Name"));
                message.Subject = "Su solicitud #"+datos.IdSolicitud+" a pasado a un estado Pendiente";


                message.Body ="Hola "+usuario.Nombre+".\n\nTu solicitud #"+datos.IdSolicitud+"\nPaquete: "+paquete.Nombre+ "\nHa pasado a estado: Pendiente\n\nPara más información:\nOficina. (229) 376 1703 Celular. 2293 23 0180\nCorreo electrónico. rafael@eracomputacion.net";
                message.IsBodyHtml = false;


                using (var client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Port = 587;
                    client.Credentials = new NetworkCredential("eracomputacion2019@gmail.com", "Carlitos3099");
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }
            context.SaveChanges();
            TempData["mensaje"] = "exitoPendiente";
            return Json(data: "Pendiente");


        }


        [Authorize]
        [HttpPost]
        //Método que desactiva a un usuario creado, se invoca por medio de jQuery
        public JsonResult establecerCancelado(int? id)
        {

            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            if (id == null)
            {
                TempData["mensaje"] = "error";
                return Json(data: "Cancelado");
            }

            var datos = context.Solicitud.FirstOrDefault(usr => usr.IdSolicitud == id) as Solicitud;
            if (datos.RStatus == 6)
            {
                TempData["mensaje"] = "canceladoAceptado";
                return Json(data: "Cancelado");
            }

            datos.RStatus = 5;

            using (var message = new MailMessage())
            {
                var cliente = context.Cliente.Where(data => data.IdCliente == datos.RCliente).FirstOrDefault();
                var usuario = context.Usuario.Where(data => data.IdUsuario == cliente.RUsuario).FirstOrDefault();
                var paquete = context.Paquete.Where(data => data.IdPaquete == datos.RPaquete).FirstOrDefault();
                var date2 = DateTime.Now;

                message.To.Add(new MailAddress(usuario.Correo));

                message.From = new MailAddress("eracomputacion2019@gmail.com", "Rafael Palomo");
                //message.CC.Add(new MailAddress("cc@email.com", "CC Name"));
                //message.Bcc.Add(new MailAddress("bcc@email.com", "BCC Name"));
                message.Subject = "Su solicitud #" + datos.IdSolicitud + " a pasado a un estado Rechazado";


                message.Body = "Hola " + usuario.Nombre + ".\n\nTu solicitud #" + datos.IdSolicitud + "\nPaquete: " + paquete.Nombre + "\nHa pasado a estado: Rechazado\n\nPara más información:\nOficina. (229) 376 1703 Celular. 2293 23 0180\nCorreo electrónico. rafael@eracomputacion.net";
                message.IsBodyHtml = false;


                using (var client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Port = 587;
                    client.Credentials = new NetworkCredential("eracomputacion2019@gmail.com", "Carlitos3099");
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }
            context.SaveChanges();
            TempData["mensaje"] = "exitoCancelado";
            return Json(data: "Cancelado");


        }


        [Authorize]
        [HttpPost]
        //Método que desactiva a un usuario creado, se invoca por medio de jQuery
        public JsonResult establecerAceptado(int? id,float monto)
        {

            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            if (id == null)
            {
                TempData["mensaje"] = "error";
                return Json(data: "Aceptado");
            }

            var datos = context.Solicitud.FirstOrDefault(usr => usr.IdSolicitud == id) as Solicitud;
            if (datos.RStatus == 6)
            {
                TempData["mensaje"] = "canceladoAceptado";
                return Json(data: "Aceptado");
            }

            datos.RStatus = 6;
            datos.InstalacionExtra = monto;
            var paquete1 = context.Paquete.Where(data => data.IdPaquete == datos.RPaquete).FirstOrDefault();
            var paqueteList = context.Productosxpaquete.Where(data => data.RPaquetes == paquete1.IdPaquete).ToList();
            foreach(var item in paqueteList)
            {
                Productos producto = context.Productos.Where(data => data.IdProductos == item.RProductos).FirstOrDefault();
                producto.Cantidad = producto.Cantidad - item.Cantidad;
                context.SaveChanges();
            }

            //Detalle Venta

            Detalleventa detalleVenta = new Detalleventa();
            detalleVenta.Fecha = DateTime.Now;
            detalleVenta.Descripcion = paquete1.Nombre;
            detalleVenta.RSolicitud = datos.IdSolicitud;
            detalleVenta.RStatus = 8;

            context.Detalleventa.Add(detalleVenta);

            using (var message = new MailMessage())
            {
                var cliente = context.Cliente.Where(data => data.IdCliente == datos.RCliente).FirstOrDefault();
                var usuario = context.Usuario.Where(data => data.IdUsuario == cliente.RUsuario).FirstOrDefault();
                var paquete = context.Paquete.Where(data => data.IdPaquete == datos.RPaquete).FirstOrDefault();
                var date2 = DateTime.Now;

                message.To.Add(new MailAddress(usuario.Correo));

                message.From = new MailAddress("eracomputacion2019@gmail.com", "Rafael Palomo");
                //message.CC.Add(new MailAddress("cc@email.com", "CC Name"));
                //message.Bcc.Add(new MailAddress("bcc@email.com", "BCC Name"));
                message.Subject = "Su solicitud #" + datos.IdSolicitud + " ha sido aceptada";


                message.Body = "Hola " + usuario.Nombre + ".\n\nTu solicitud #" + datos.IdSolicitud + "\nPaquete: " + paquete.Nombre + "\nHa pasado a estado: Aceptado\n\nPara más información:\nOficina. (229) 376 1703 Celular. 2293 23 0180\nCorreo electrónico. rafael@eracomputacion.net";
                message.IsBodyHtml = false;


                using (var client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Port = 587;
                    client.Credentials = new NetworkCredential("eracomputacion2019@gmail.com", "Carlitos3099");
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }
            context.SaveChanges();
            TempData["mensaje"] = "exitoAceptado";
            return Json(data: "Aceptado");


        }

        [Authorize]
        public IActionResult editarSolicitud(int? id)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var solicitud = context.Solicitud.Where(data => data.IdSolicitud == id).FirstOrDefault();
            var paquete = context.Paquete.Where(data => data.IdPaquete == solicitud.RPaquete).FirstOrDefault();
            var cliente = context.Cliente.Where(data => data.IdCliente == solicitud.RCliente).FirstOrDefault();
            var usuario = context.Usuario.Where(data => data.IdUsuario == cliente.RUsuario).FirstOrDefault();
            solicitud.RPaqueteNavigation = paquete;
            solicitud.RClienteNavigation = cliente;
            solicitud.RClienteNavigation.RUsuarioNavigation = usuario;
            ViewBag.camaras = context.Productos.Where(data => data.RCatProducto == 1).Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.RStatus == 1 ? parm.NombreProd : parm.NombreProd + " | No disponible",
                Value = parm.IdProductos.ToString()
            });

            ViewBag.dvrs = context.Productos.Where(data => data.RCatProducto == 2).Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.RStatus == 1 ? parm.NombreProd : parm.NombreProd + " | No disponible",
                Value = parm.IdProductos.ToString()
            });

            ViewBag.discoduro = context.Productos.Where(data => data.RCatProducto == 3).Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.RStatus == 1 ? parm.NombreProd : parm.NombreProd + " | No disponible",
                Value = parm.IdProductos.ToString()
            });

            ViewBag.instalacion = context.Productos.Where(data => data.RCatProducto == 4).Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.RStatus == 1 ? parm.NombreProd : parm.NombreProd + " | No disponible",
                Value = parm.IdProductos.ToString()

            });
            ViewBag.usuarios = context.Usuario.Where(data => data.RStatus == 1 && data.RTipo == 1).Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.Nombre + " " + parm.Apellidos + " | " + parm.NombreUsuario,
                Value = parm.IdUsuario.ToString()
            });

            ViewBag.paquetes = context.Paquete.Where(data => data.RStatus == 1 && data.EsPersonalizado==2).Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.Nombre,
                Value = parm.IdPaquete.ToString()
            });

            return View(solicitud);
        }


        [HttpPost]
        [Authorize]
        //Metodo para crear un cliente
        public IActionResult editarSolicitud(Solicitud e, int camaras, int cantcamaras, int dvr, int cantdvr, int discoduro, int cantdiscoduro, int instalacion, int cantinstalacion, int paqueteback)
        {

            //crea contexto a la db
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var solicitud = context.Solicitud.Where(data => data.IdSolicitud == e.IdSolicitud).FirstOrDefault();
            if (solicitud != null)
            {
                solicitud.FechaSolicitud = DateTime.Now;
                solicitud.RStatus = e.RStatus;
                solicitud.RCliente = e.RCliente;
                solicitud.PrecioSolicitud = e.PrecioSolicitud;
                if (e.RPaquete == 0)
                {
                    var paquetesCustom = context.Paquete.Where(data => data.EsPersonalizado == 1).ToList();
                    foreach (var item in paquetesCustom)
                    {
                        Productosxpaquete verificaCamara = context.Productosxpaquete.Where(data => data.Cantidad == cantcamaras && data.RProductos == camaras && item.IdPaquete == data.RPaquetes).FirstOrDefault();
                        Productosxpaquete verificaDVR = context.Productosxpaquete.Where(data => data.Cantidad == cantdvr && data.RProductos == dvr && item.IdPaquete == data.RPaquetes).FirstOrDefault();
                        Productosxpaquete verificaHDD = context.Productosxpaquete.Where(data => data.Cantidad == cantdiscoduro && data.RProductos == discoduro && item.IdPaquete == data.RPaquetes).FirstOrDefault();
                        Productosxpaquete verificaInstalacion = context.Productosxpaquete.Where(data => data.Cantidad == cantinstalacion && data.RProductos == instalacion && item.IdPaquete == data.RPaquetes).FirstOrDefault();

                        if (verificaCamara != null && verificaDVR != null && verificaHDD != null && verificaInstalacion != null)
                        {
                            var getPaquete = verificaCamara.RPaquetes;
                            solicitud.RPaquete = getPaquete;
                            context.SaveChanges();


                            using (var message = new MailMessage())
                            {
                                var cliente = context.Cliente.Where(data => data.IdCliente == solicitud.RCliente).FirstOrDefault();
                                var usuario = context.Usuario.Where(data => data.IdUsuario == cliente.RUsuario).FirstOrDefault();
                                var paquete = context.Paquete.Where(data => data.IdPaquete == solicitud.RPaquete).FirstOrDefault();
                                var date2 = DateTime.Now;

                                message.To.Add(new MailAddress(usuario.Correo));

                                message.From = new MailAddress("eracomputacion2019@gmail.com", "Rafael Palomo");
                                //message.CC.Add(new MailAddress("cc@email.com", "CC Name"));
                                //message.Bcc.Add(new MailAddress("bcc@email.com", "BCC Name"));
                                message.Subject = "Su solicitud #" + solicitud.IdSolicitud + " ha sido Editada";


                                message.Body = "Hola " + usuario.Nombre + ".\n\nTu solicitud #" + solicitud.IdSolicitud + " ha sido editada.\n\nPara más información:\nOficina. (229) 376 1703 Celular. 2293 23 0180\nCorreo electrónico. rafael@eracomputacion.net";
                                message.IsBodyHtml = false;


                                using (var client = new SmtpClient("smtp.gmail.com"))
                                {
                                    client.Port = 587;
                                    client.Credentials = new NetworkCredential("eracomputacion2019@gmail.com", "Carlitos3099");
                                    client.EnableSsl = true;
                                    client.Send(message);
                                }
                            }
                            TempData["mensaje"] = "editSolicitud";
                            return RedirectToAction("ListSolicitudes", "Solicitudes");
                        }
                    }

                    //temporal
                    solicitud.RPaquete = context.Paquete.LastOrDefault().IdPaquete ;
                    Paquete paqueteCustom = new Paquete();
                    paqueteCustom.Nombre = "Paquete Personalizado";
                    paqueteCustom.Descripcion = "Paquete Personalizado";
                    paqueteCustom.ImgPaquete = "era-logo-icon.png";
                    paqueteCustom.RStatus = 1;
                    paqueteCustom.EsPersonalizado = 1;
                    paqueteCustom.Precio = e.PrecioSolicitud;
                    context.Paquete.Add(paqueteCustom);
                    context.SaveChanges();

                    Paquete lastPaquete = context.Paquete.LastOrDefault();

                    Productosxpaquete camaraP = new Productosxpaquete();
                    camaraP.Cantidad = cantcamaras;
                    camaraP.RPaquetes = lastPaquete.IdPaquete;
                    camaraP.RProductos = camaras;
                    camaraP.RStatus = 1;

                    Productosxpaquete dvrP = new Productosxpaquete();
                    dvrP.Cantidad = cantdvr;
                    dvrP.RPaquetes = lastPaquete.IdPaquete;
                    dvrP.RProductos = dvr;
                    dvrP.RStatus = 1;

                    Productosxpaquete hdP = new Productosxpaquete();
                    hdP.Cantidad = cantdiscoduro;
                    hdP.RPaquetes = lastPaquete.IdPaquete;
                    hdP.RProductos = discoduro;
                    hdP.RStatus = 1;

                    Productosxpaquete instalacionP = new Productosxpaquete();
                    instalacionP.Cantidad = cantinstalacion;
                    instalacionP.RPaquetes = lastPaquete.IdPaquete;
                    instalacionP.RProductos = instalacion;
                    instalacionP.RStatus = 1;

                    context.Productosxpaquete.Add(camaraP);
                    context.Productosxpaquete.Add(dvrP);
                    context.Productosxpaquete.Add(hdP);
                    context.Productosxpaquete.Add(instalacionP);
                    context.SaveChanges();

                    solicitud.RPaquete = lastPaquete.IdPaquete;
                    context.SaveChanges();

                    using (var message = new MailMessage())
                    {
                        var cliente = context.Cliente.Where(data => data.IdCliente == solicitud.RCliente).FirstOrDefault();
                        var usuario = context.Usuario.Where(data => data.IdUsuario == cliente.RUsuario).FirstOrDefault();
                        var paquete = context.Paquete.Where(data => data.IdPaquete == solicitud.RPaquete).FirstOrDefault();
                        var date2 = DateTime.Now;

                        message.To.Add(new MailAddress(usuario.Correo));

                        message.From = new MailAddress("eracomputacion2019@gmail.com", "Rafael Palomo");
                        //message.CC.Add(new MailAddress("cc@email.com", "CC Name"));
                        //message.Bcc.Add(new MailAddress("bcc@email.com", "BCC Name"));
                        message.Subject = "Su solicitud #" + solicitud.IdSolicitud + " ha sido Editada";


                        message.Body = "Hola " + usuario.Nombre + ".\n\nTu solicitud #" + solicitud.IdSolicitud + " ha sido editada.\n\nPara más información:\nOficina. (229) 376 1703 Celular. 2293 23 0180\nCorreo electrónico. rafael@eracomputacion.net";
                        message.IsBodyHtml = false;


                        using (var client = new SmtpClient("smtp.gmail.com"))
                        {
                            client.Port = 587;
                            client.Credentials = new NetworkCredential("eracomputacion2019@gmail.com", "Carlitos3099");
                            client.EnableSsl = true;
                            client.Send(message);
                        }
                    }
                    TempData["mensaje"] = "editSolicitud";
                    return RedirectToAction("ListSolicitudes", "Solicitudes");
                }
                else
                {
                    solicitud.RPaquete = e.RPaquete;
                    context.SaveChanges();


                    using (var message = new MailMessage())
                    {
                        var cliente = context.Cliente.Where(data => data.IdCliente == solicitud.RCliente).FirstOrDefault();
                        var usuario = context.Usuario.Where(data => data.IdUsuario == cliente.RUsuario).FirstOrDefault();
                        var paquete = context.Paquete.Where(data => data.IdPaquete == solicitud.RPaquete).FirstOrDefault();
                        var date2 = DateTime.Now;

                        message.To.Add(new MailAddress(usuario.Correo));

                        message.From = new MailAddress("eracomputacion2019@gmail.com", "Rafael Palomo");
                        //message.CC.Add(new MailAddress("cc@email.com", "CC Name"));
                        //message.Bcc.Add(new MailAddress("bcc@email.com", "BCC Name"));
                        message.Subject = "Su solicitud #" + solicitud.IdSolicitud + " ha sido Editada";


                        message.Body = "Hola " + usuario.Nombre + ".\n\nTu solicitud #" + solicitud.IdSolicitud + " ha sido editada.\n\nPara más información:\nOficina. (229) 376 1703 Celular. 2293 23 0180\nCorreo electrónico. rafael@eracomputacion.net";
                        message.IsBodyHtml = false;


                        using (var client = new SmtpClient("smtp.gmail.com"))
                        {
                            client.Port = 587;
                            client.Credentials = new NetworkCredential("eracomputacion2019@gmail.com", "Carlitos3099");
                            client.EnableSsl = true;
                            client.Send(message);
                        }
                    }
                    TempData["mensaje"] = "editSolicitud";
                    return RedirectToAction("ListSolicitudes", "Solicitudes");
                }
            }
            else{
                TempData["mensaje"] = "editSolicitudError";
                return RedirectToAction("ListSolicitudes", "Solicitudes");
            }

        }

    }
}