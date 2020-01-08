using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using era_Computacion.Models;
using era_Computacion.Models.db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace era_Computacion.Controllers
{
    public class PaquetesController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        public PaquetesController(IHostingEnvironment enviroment)
        {
            _hostingEnvironment = enviroment;
        }
        [Authorize]
        public IActionResult ListPaquetes()
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var paquetes = context.Paquete.ToList();
            List<paqueteCustom> listPaquetes = new List<paqueteCustom>();
            foreach (var item in paquetes)
            {
                paqueteCustom paqueteC = new paqueteCustom();
                paqueteC.paquete = item;
                var productos = context.Productosxpaquete.Where(data => data.RPaquetes == item.IdPaquete).ToList();
                if (productos.Count()>0)
                {
                    List<paqueteCustom2> productoObjeto = new List<paqueteCustom2>();
                    foreach(var itemP in productos)
                    {
                        paqueteCustom2 paquetecustom2 = new paqueteCustom2();
                        var producto = context.Productos.Where(data => data.IdProductos == itemP.RProductos).FirstOrDefault();

                        paquetecustom2.productosporpaquete = itemP;
                        paquetecustom2.producto = producto;
                        productoObjeto.Add(paquetecustom2);
                    }
                    paqueteC.arreglo = productoObjeto;
                }
                listPaquetes.Add(paqueteC);


            }

            return View(listPaquetes);
        }

        [Authorize]
        public IActionResult agregarPaquete()
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            ViewBag.camaras = context.Productos.Where(data => data.RCatProducto == 1).Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.RStatus == 1 ? parm.NombreProd : parm.NombreProd + " | No disponible",
                Value = parm.IdProductos.ToString()
            });

            ViewBag.dvrs = context.Productos.Where(data => data.RCatProducto == 2).Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.RStatus==1 ? parm.NombreProd : parm.NombreProd+" | No disponible",
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

        [Authorize]
        [HttpPost]
        public JsonResult getPrecioProducto(int id)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var camara = context.Productos.Where(data => data.IdProductos == id).FirstOrDefault();


            return Json(camara.PrecioVentaXunidad);

        }

        [HttpPost]
        [Authorize]
        //Metodo para crear un cliente
        public async Task<IActionResult> agregarPaquete(Paquete e, int camaras,int cantcamaras,int dvr, int cantdvr, int discoduro, int cantdiscoduro,int instalacion,int cantinstalacion, List<IFormFile> files)
        {

            //crea contexto a la db
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
         
            //Verifica si ese cliente no existe
            var usuarioExistente = context.Paquete.FirstOrDefault(usr => usr.Nombre.ToLower() == e.Nombre.ToLower());
            if (usuarioExistente == null)
            {
                //Si no existe se crea el cliente
                e.RStatus = 1;
                e.EsPersonalizado = 2;
                long sum = files.Sum(f => f.Length);
                if (sum > 0)
                {


                    var filePath = Path.GetTempFileName();

                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images/paquetes");
                    foreach (var formFile in files)
                    {


                        if (formFile.Length > 0)
                        {
                            string guid = Guid.NewGuid().ToString() + ".jpg";
                            using (var fileStream = new FileStream(Path.Combine(uploads, guid), FileMode.Create))
                            {

                                await formFile.CopyToAsync(fileStream);
                                e.ImgPaquete = guid;
                            }
                        }
                    }
                }
                else
                {
                    e.ImgPaquete = "era-logo-icon.png";
                }
                context.Paquete.Add(e);
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

                TempData["mensaje"] = "addPaquete";
                return RedirectToAction("ListPaquetes", "Paquetes");
            }
            else
            {
                //Si existe se devuelve un mensaje de error
                TempData["mensaje"] = "paqueteExistente";
                return RedirectToAction("ListPaquetes", "Paquetes");
            }

        }


        [Authorize]
        [HttpPost]
        //Método que desactiva a un usuario creado, se invoca por medio de jQuery
        public JsonResult desactivatePaquete(int? id)
        {

            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            if (id == null)
                return Json(data: "No desactivado");

            var datos = context.Paquete.FirstOrDefault(usr => usr.IdPaquete == id) as Paquete;
            datos.RStatus = 2;

            var productos = context.Productosxpaquete.Where(data => data.RPaquetes == id).ToList();
            foreach (var item in productos)
            {
                item.RStatus = 2;
            }
            context.Paquete.Update(datos);
            context.Productosxpaquete.UpdateRange(productos);
            context.SaveChanges();
            return Json(data: "Desactivado");


        }



        [Authorize]
        [HttpPost]

        //Método que activa a un usuario creado, se invoca por medio de jQuery
        public JsonResult activatePaquete(int? id)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            if (id == null)
                return Json(data: "No activado");

            var datos = context.Paquete.FirstOrDefault(usr => usr.IdPaquete == id) as Paquete;
            datos.RStatus = 1;
   
            var productos = context.Productosxpaquete.Where(data => data.RPaquetes == id).ToList();
            foreach(var item in productos)
            {
                item.RStatus = 1;
            }
            context.Paquete.Update(datos);
            context.Productosxpaquete.UpdateRange(productos);
            context.SaveChanges();
            return Json(data: "Activado");

        }


        [Authorize]
        public IActionResult editarPaquete(int id)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var paquete = context.Paquete.Where(data => data.IdPaquete == id).FirstOrDefault();
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
            return View(paquete);
        }


        [Authorize]
        [HttpPost]

        //Método que activa a un usuario creado, se invoca por medio de jQuery
        public JsonResult getDatos(int? id)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            List<Productosxpaquete> productosback = context.Productosxpaquete.Where(data => data.RPaquetes == id).ToList();
            var productosCatagolo = context.Productos.Where(data => productosback.Any(t1 => data.IdProductos == t1.RProductos)).ToList();
            productosCatagolo = productosCatagolo.OrderBy(data => data.RCatProducto).ToList();
            List<Productosxpaquete> productos = new List<Productosxpaquete>();
            foreach (var item in productosCatagolo)
            {
                Productosxpaquete producto = context.Productosxpaquete.Where(data => data.RPaquetes == id && data.RProductos == item.IdProductos).FirstOrDefault();
                productos.Add(producto);
            }
            List<paqueteCustomEdit> lista = new List<paqueteCustomEdit>();
            foreach(var item in productos)
            {
                paqueteCustomEdit paquete = new paqueteCustomEdit();
                paquete.cantidad = item.Cantidad;
                paquete.producto = item.RProductos;
                lista.Add(paquete);
            }

            List<float> listaProductos = new List<float>();
            float? total = 0;
            foreach(var item in lista)
            {
                var producto1 = context.Productos.Where(data => data.IdProductos == item.producto).FirstOrDefault();
                listaProductos.Add(producto1.PrecioVentaXunidad);
                total += producto1.PrecioVentaXunidad * item.cantidad;
            }

            
            return Json(new {data=lista,total=total,precios=listaProductos });

        }


        [HttpPost]
        [Authorize]
        //Metodo para crear un cliente
        public async Task<IActionResult> editarPaquete(Paquete e, int camaras, int cantcamaras, int dvr, int cantdvr, int discoduro, int cantdiscoduro, int instalacion, int cantinstalacion, List<IFormFile> files)
        {

            //crea contexto a la db
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            //Verifica si ese cliente no existe
            var paqueteExistente = context.Paquete.FirstOrDefault(usr => usr.IdPaquete == e.IdPaquete);
            if (paqueteExistente != null)
            {
                paqueteExistente.Nombre = e.Nombre;
                paqueteExistente.Precio = e.Precio;
                paqueteExistente.Descripcion = e.Descripcion;

                long sum = files.Sum(f => f.Length);
                if (sum > 0)
                {


                    var filePath = Path.GetTempFileName();

                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images/paquetes");
                    foreach (var formFile in files)
                    {


                        if (formFile.Length > 0)
                        {
                            string guid = Guid.NewGuid().ToString() + ".jpg";
                            using (var fileStream = new FileStream(Path.Combine(uploads, guid), FileMode.Create))
                            {

                                await formFile.CopyToAsync(fileStream);
                                paqueteExistente.ImgPaquete = guid;
                            }
                        }
                    }
                }
                

                context.SaveChanges();

                Paquete lastPaquete = context.Paquete.Where(data => data.IdPaquete == e.IdPaquete).FirstOrDefault();

                if(lastPaquete!=null)
                    context.Productosxpaquete.RemoveRange(context.Productosxpaquete.Where(data => data.RPaquetes == lastPaquete.IdPaquete));
                
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

                TempData["mensaje"] = "editPaquete";
                return RedirectToAction("ListPaquetes", "Paquetes");
            }
            else
            {
                //Si existe se devuelve un mensaje de error
                TempData["mensaje"] = "ErrorEditPaquete";
                return RedirectToAction("ListPaquetes", "Paquetes");
            }

        }


    }
}