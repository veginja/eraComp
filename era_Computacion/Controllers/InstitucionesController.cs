using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using era_Computacion.Models.db;
using Microsoft.AspNetCore.Mvc;
using era_Computacion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
namespace era_Computacion.Controllers
{
    public class InstitucionesController : Controller
    {
        public IActionResult ListInstituciones()
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var clientes = context.Cliente.Where(data => data.RUsuario == HttpContext.Session.GetInt32("Id_user")).ToList();
            foreach(var item in clientes)
            {
                var institucion = context.Institucion.Where(data => data.IdInstitucion == item.RInst).FirstOrDefault();
                item.RInstNavigation = institucion;

                var tipo = context.Tipoinstitucion.Where(data => data.IdTipoInst == institucion.RTipoIns).FirstOrDefault();
                item.RInstNavigation.RTipoInsNavigation = tipo;
            }
            return View(clientes);
        }

        [Authorize]
        public IActionResult agregarInstitucion()
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            ViewBag.tipos = context.Tipoinstitucion.Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.Descripcion,
                Value = parm.IdTipoInst.ToString()
            });
            return View();
        }

        [Authorize]
        public IActionResult editarInstitucion(int id)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            var clientes = context.Cliente.Where(data => data.RUsuario == HttpContext.Session.GetInt32("Id_user") && data.RInst == id).FirstOrDefault();
            if(clientes==null)
                return RedirectToAction("ListInstituciones", "Instituciones");
            var institucion = context.Institucion.Where(data => data.IdInstitucion == id).FirstOrDefault();
            ViewBag.tipos = context.Tipoinstitucion.Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.Descripcion,
                Value = parm.IdTipoInst.ToString()
            });
            return View(institucion);
        }


        [HttpPost]
        [Authorize]
        //Metodo para crear un cliente
        public IActionResult agregarInstitucion(Institucion e)
        {

            //crea contexto a la db
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            e.RStatus = 1;
            context.Institucion.Add(e);
            context.SaveChanges();

            Cliente nuevoCliente = new Cliente();
            nuevoCliente.RInst = context.Institucion.LastOrDefault().IdInstitucion;
            nuevoCliente.RUsuario = (int)HttpContext.Session.GetInt32("Id_user");
            nuevoCliente.RStatus = 1;
            context.Cliente.Add(nuevoCliente);
            context.SaveChanges();

            TempData["mensaje"] = "addInstitucion";

            return RedirectToAction("ListInstituciones", "Instituciones");
        }

        [HttpPost]
        [Authorize]
        //Metodo para crear un cliente
        public IActionResult editarInstitucion(Institucion e)
        {

            //crea contexto a la db
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var institucionOriginal = context.Institucion.Where(data => data.IdInstitucion == e.IdInstitucion).FirstOrDefault();
            if (institucionOriginal != null)
            {
                institucionOriginal.Correo = e.Correo;
                institucionOriginal.Descripcion = e.Descripcion;
                institucionOriginal.Direccion = e.Direccion;
                institucionOriginal.Nombre = e.Nombre;
                institucionOriginal.PaginaWeb = e.PaginaWeb;
                institucionOriginal.RTipoIns = e.RTipoIns;
                institucionOriginal.Telefono = e.Telefono;
                context.SaveChanges();
                TempData["mensaje"] = "editInstitucion";

                return RedirectToAction("ListInstituciones", "Instituciones");
            }
            else
            {
                TempData["mensaje"] = "errorEditInstitucion";
                return View();
            }

        }


        [Authorize]
        [HttpPost]
        //Método que desactiva a un usuario creado, se invoca por medio de jQuery
        public JsonResult desactivateInstitucion(int? id)
        {

            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            if (id == null)
                return Json(data: "No desactivado");

            var datos = context.Cliente.FirstOrDefault(usr => usr.IdCliente == id) as Cliente;
            datos.RStatus = 2;
            context.Cliente.Update(datos);
            context.SaveChanges();
            return Json(data: "Desactivado");


        }



        [Authorize]
        [HttpPost]

        //Método que activa a un usuario creado, se invoca por medio de jQuery
        public JsonResult activateInstitucion(int? id)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            if (id == null)
                return Json(data: "No activado");

            var datos = context.Cliente.FirstOrDefault(usr => usr.IdCliente == id) as Cliente;
            datos.RStatus = 1;
            context.Cliente.Update(datos);
            context.SaveChanges();
            return Json(data: "Activado");

        }


    }
}