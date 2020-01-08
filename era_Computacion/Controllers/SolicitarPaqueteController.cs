using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using era_Computacion.Models.db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using era_Computacion.Models;
using System.Net.Mail;
using System.Net;

namespace era_Computacion.Views.SolicitarPaquete
{
    public class SolicitarPaqueteController : Controller
    {
        [Authorize]
        public IActionResult Solicitar()
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            var usuario = context.Cliente.Where(data => data.RUsuario == HttpContext.Session.GetInt32("Id_user")).ToList();
            if (usuario.Count == 0)
                ViewBag.tieneEmpresa = 0;
            else
                ViewBag.tieneEmpresa = 1;

            var clientes = context.Cliente.Where(data => data.RUsuario == HttpContext.Session.GetInt32("Id_user") && data.RStatus == 1).ToList();

            var instituciones = context.Institucion.Where(data => clientes.Any(t1 => data.IdInstitucion == t1.RInst) && data.RStatus == 1).ToList();

            ViewBag.instituciones = instituciones.Where(data => data.RStatus == 1).Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.Nombre,
                Value = clientes.Where(data => data.RInst == parm.IdInstitucion).Select(data => data.IdCliente).FirstOrDefault().ToString()
            });


            var paquetes = context.Paquete.Where(data => data.RStatus == 1 && data.EsPersonalizado == 2).ToList();
            solicitudCustomCliente solicitud = new solicitudCustomCliente();
            solicitud.solicitud = new Solicitud();
            solicitud.arreglo = paquetes;
            return View(solicitud);
        }



        [HttpPost]
        [Authorize]
        //Metodo para crear un cliente
        public IActionResult Solicitar(solicitudCustomCliente custom,int paquete)
        {
            Solicitud e = custom.solicitud;
            //crea contexto a la db
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            e.FechaSolicitud = DateTime.Now;
            e.RStatus = 4;
            if (paquete != 0)
            {
                e.RPaquete = paquete;
                var paqueteObj = context.Paquete.Where(data => data.IdPaquete == paquete).FirstOrDefault();
                e.PrecioSolicitud = paqueteObj.Precio;
            }
                
            
            context.Solicitud.Add(e);
            context.SaveChanges();


            using (var message = new MailMessage())
            {
                var usuario = context.Usuario.Where(data => data.RTipo == 2).ToList();
                var date2 = DateTime.Now;

                foreach(var item in usuario)
                {
                    message.To.Add(new MailAddress(item.Correo));
                }
                

                message.From = new MailAddress("eracomputacion2019@gmail.com", "Rafael Palomo");
                //message.CC.Add(new MailAddress("cc@email.com", "CC Name"));
                //message.Bcc.Add(new MailAddress("bcc@email.com", "BCC Name"));
                message.Subject = "Se ha registrado un nuevo pedido";


                message.Body = "Se ha registrado un nuevo pedido.\n\nPara más información:\nOficina. (229) 376 1703 Celular. 2293 23 0180\nCorreo electrónico. rafael@eracomputacion.net";
                message.IsBodyHtml = false;


                using (var client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Port = 587;
                    client.Credentials = new NetworkCredential("eracomputacion2019@gmail.com", "Carlitos3099");
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }

            TempData["mensaje"] = "addSolicitud";
            return RedirectToAction("HistorialPedidos", "Historial");
            


        }




        [Authorize]
        public IActionResult paquetePersonalizado()
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            var clientes = context.Cliente.Where(data => data.RUsuario == HttpContext.Session.GetInt32("Id_user") && data.RStatus==1).ToList();

            var instituciones = context.Institucion.Where(data => clientes.Any(t1 => data.IdInstitucion == t1.RInst) && data.RStatus==1).ToList();

            ViewBag.instituciones = instituciones.Where(data => data.RStatus == 1).Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.Nombre,
                Value = clientes.Where(data => data.RInst == parm.IdInstitucion).Select(data => data.IdCliente).FirstOrDefault().ToString()
            });


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


            return View();
        }


        [HttpPost]
        [Authorize]
        //Metodo para crear un cliente
        public IActionResult paquetePersonalizado(Solicitud e, int camaras, int cantcamaras, int dvr, int cantdvr, int discoduro, int cantdiscoduro, int instalacion, int cantinstalacion)
        {

            //crea contexto a la db
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            e.FechaSolicitud = DateTime.Now;
            e.RStatus = 4;
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
                        e.RPaquete = getPaquete;
                        context.Solicitud.Add(e);
                        context.SaveChanges();
                        TempData["mensaje"] = "addSolicitud";

                        using (var message = new MailMessage())
                        {
                            var usuario = context.Usuario.Where(data => data.RTipo == 2).ToList();
                            var date2 = DateTime.Now;

                            foreach (var item2 in usuario)
                            {
                                message.To.Add(new MailAddress(item2.Correo));
                            }


                            message.From = new MailAddress("eracomputacion2019@gmail.com", "Rafael Palomo");
                            //message.CC.Add(new MailAddress("cc@email.com", "CC Name"));
                            //message.Bcc.Add(new MailAddress("bcc@email.com", "BCC Name"));
                            message.Subject = "Se ha registrado un nuevo pedido";


                            message.Body = "Se ha registrado un nuevo pedido.\n\nPara más información:\nOficina. (229) 376 1703 Celular. 2293 23 0180\nCorreo electrónico. rafael@eracomputacion.net";
                            message.IsBodyHtml = false;


                            using (var client = new SmtpClient("smtp.gmail.com"))
                            {
                                client.Port = 587;
                                client.Credentials = new NetworkCredential("eracomputacion2019@gmail.com", "Carlitos3099");
                                client.EnableSsl = true;
                                client.Send(message);
                            }
                        }
                        return RedirectToAction("HistorialPedidos", "Historial");
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

                using (var message = new MailMessage())
                {
                    var usuario = context.Usuario.Where(data => data.RTipo == 2).ToList();
                    var date2 = DateTime.Now;

                    foreach (var item in usuario)
                    {
                        message.To.Add(new MailAddress(item.Correo));
                    }


                    message.From = new MailAddress("eracomputacion2019@gmail.com", "Rafael Palomo");
                    //message.CC.Add(new MailAddress("cc@email.com", "CC Name"));
                    //message.Bcc.Add(new MailAddress("bcc@email.com", "BCC Name"));
                    message.Subject = "Se ha registrado un nuevo pedido";


                    message.Body = "Se ha registrado un nuevo pedido.\n\nPara más información:\nOficina. (229) 376 1703 Celular. 2293 23 0180\nCorreo electrónico. rafael@eracomputacion.net";
                    message.IsBodyHtml = false;


                    using (var client = new SmtpClient("smtp.gmail.com"))
                    {
                        client.Port = 587;
                        client.Credentials = new NetworkCredential("eracomputacion2019@gmail.com", "Carlitos3099");
                        client.EnableSsl = true;
                        client.Send(message);
                    }
                }
                return RedirectToAction("HistorialPedidos", "Historial");
            }
            else
            {
                context.Solicitud.Add(e);
                context.SaveChanges();
                TempData["mensaje"] = "addSolicitud";

                using (var message = new MailMessage())
                {
                    var usuario = context.Usuario.Where(data => data.RTipo == 2).ToList();
                    var date2 = DateTime.Now;

                    foreach (var item in usuario)
                    {
                        message.To.Add(new MailAddress(item.Correo));
                    }


                    message.From = new MailAddress("eracomputacion2019@gmail.com", "Rafael Palomo");
                    //message.CC.Add(new MailAddress("cc@email.com", "CC Name"));
                    //message.Bcc.Add(new MailAddress("bcc@email.com", "BCC Name"));
                    message.Subject = "Se ha registrado un nuevo pedido";


                    message.Body = "Se ha registrado un nuevo pedido.\n\nPara más información:\nOficina. (229) 376 1703 Celular. 2293 23 0180\nCorreo electrónico. rafael@eracomputacion.net";
                    message.IsBodyHtml = false;


                    using (var client = new SmtpClient("smtp.gmail.com"))
                    {
                        client.Port = 587;
                        client.Credentials = new NetworkCredential("eracomputacion2019@gmail.com", "Carlitos3099");
                        client.EnableSsl = true;
                        client.Send(message);
                    }
                }
                return RedirectToAction("HistorialPedidos", "Historial");
            }



        }


    }
}