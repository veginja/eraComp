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
    public class ProductosController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        public ProductosController(IHostingEnvironment enviroment)
        {
            _hostingEnvironment = enviroment;
        }
        [Authorize]
        public IActionResult ListProductos()
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var user = context.Productos.ToList();
            List<productoCustom> listProductos = new List<productoCustom>();
            foreach (var item in user)
            {
                productoCustom productoC = new productoCustom();
                productoC.producto = item;
                var categoria = context.CatProducto.Where(data => data.IdcatProducto == item.RCatProducto).FirstOrDefault();
                if (categoria != null)
                {
                    productoC.categoriaProducto = categoria;
                }

                listProductos.Add(productoC);

            }

            return View(listProductos);
        }

        [Authorize]
        public IActionResult agregarProducto()
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            ViewBag.tipos = context.CatProducto.Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.Nombre,
                Value = parm.IdcatProducto.ToString()
            });
            return View();
        }

        [HttpPost]
        [Authorize]
        //Metodo para crear un cliente
        public async Task<IActionResult> agregarProducto(Productos e, List<IFormFile> files)
        {
     
            //crea contexto a la db
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            //Verifica si ese cliente no existe
            var usuarioExistente = context.Productos.FirstOrDefault(usr => usr.NombreProd.ToLower() == e.NombreProd.ToLower());
            if (usuarioExistente == null)
            {
                //Si no existe se crea el cliente
                e.RStatus = 1;

                long sum = files.Sum(f => f.Length);
                if (sum > 0)
                {


                    var filePath = Path.GetTempFileName();

                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images/productos");
                    foreach (var formFile in files)
                    {


                        if (formFile.Length > 0)
                        {
                            string guid = Guid.NewGuid().ToString() + ".jpg";
                            using (var fileStream = new FileStream(Path.Combine(uploads, guid), FileMode.Create))
                            {

                                await formFile.CopyToAsync(fileStream);
                                e.ImgProd = guid;
                            }
                        }
                    }
                }
                else
                {
                    e.ImgProd = "era-logo-icon.png";
                }
                context.Productos.Add(e);
                context.SaveChanges();
                TempData["mensaje"] = "addProducto";
                return RedirectToAction("ListProductos", "Productos");
            }
            else
            {
                //Si existe se devuelve un mensaje de error
                TempData["mensaje"] = "productoExistente";
                return RedirectToAction("ListProductos", "Productos");
            }

        }


        //Método que desactiva a un usuario creado, se invoca por medio de jQuery
        public JsonResult desactivateProducto(int? id)
        {

            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            if (id == null)
                return Json(data: "No desactivado");

            var datos = context.Productos.FirstOrDefault(usr => usr.IdProductos == id) as Productos;
            datos.RStatus = 2;
            context.Productos.Update(datos);
            context.SaveChanges();
            return Json(data: "Desactivado");


        }



        [Authorize]
        [HttpPost]

        //Método que activa a un usuario creado, se invoca por medio de jQuery
        public JsonResult activateProducto(int? id)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            if (id == null)
                return Json(data: "No activado");

            var datos = context.Productos.FirstOrDefault(usr => usr.IdProductos == id) as Productos;
            datos.RStatus = 1;
            context.Productos.Update(datos);
            context.SaveChanges();
            return Json(data: "Activado");

        }

        [Authorize]
        public IActionResult editarProducto(int id)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var productos = context.Productos.Where(data => data.IdProductos == id).FirstOrDefault();
            ViewBag.tipos = context.CatProducto.Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.Nombre,
                Value = parm.IdcatProducto.ToString()
            });
            return View(productos);
        }



        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        //Método que realiza la edicion de un usuario utilizando un objeto de tipo usuario
        public async Task<IActionResult> editarProducto(Productos e, List<IFormFile> files)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var entity = context.Productos.FirstOrDefault(usr => usr.IdProductos == e.IdProductos);

            if (entity != null)
            {
                entity.NombreProd = e.NombreProd;
                entity.PrecioCompraXunidad = e.PrecioCompraXunidad;
                entity.PrecioVentaXunidad = e.PrecioVentaXunidad;
                entity.Descripcion = e.Descripcion;
                entity.Cantidad = e.Cantidad;
                entity.RCatProducto = e.RCatProducto;



                long sum = files.Sum(f => f.Length);
                if (sum > 0)
                {
                    var filePath = Path.GetTempFileName();

                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images/productos");
                    foreach (var formFile in files)
                    {


                        if (formFile.Length > 0)
                        {
                            string guid = Guid.NewGuid().ToString() + ".jpg";
                            using (var fileStream = new FileStream(Path.Combine(uploads, guid), FileMode.Create))
                            {

                                await formFile.CopyToAsync(fileStream);
                                entity.ImgProd = guid;
                            }
                        }
                    }
                }
                context.Productos.Update(entity);
                context.SaveChanges();
                TempData["mensaje"] = "productosEdit";
                return RedirectToAction("ListProductos", "Productos");

            }
            else
            {
                TempData["mensaje"] = "ErrorproductosEdit";
                return RedirectToAction("ListProductos", "Productos");
            }


        }



    }


}