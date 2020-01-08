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
using NETCore.Encrypt;

namespace era_Computacion.Controllers
{
    public class UsuariosController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        public UsuariosController(IHostingEnvironment enviroment)
        {
            _hostingEnvironment = enviroment;
        }
        [Authorize]
        public IActionResult ListClientes()
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var user = context.Usuario.Where(data => data.RTipo == 1).ToList();
            List<ClienteCustom> listCliente = new List<ClienteCustom>();
            foreach (var item in user)
            {
                ClienteCustom cliente = new ClienteCustom();
                cliente.id = item.IdUsuario;
                cliente.direccion = item.Direccion;
                cliente.nombre = item.Nombre + " " + item.Apellidos;
                cliente.telefono = item.Telefono;
                cliente.estatusid = item.RStatus;
                var lastSolicitud = context.Solicitud.Where(data =>data.RCliente == item.IdUsuario).LastOrDefault();
                if(lastSolicitud!=null)
                {

                
                var paquete = context.Paquete.Where(data => data.IdPaquete == lastSolicitud.RPaquete).FirstOrDefault();
               if(paquete!=null)
                {
                    var estado = context.Status.Where(data => data.IdStatus == paquete.RStatus).FirstOrDefault();
                    cliente.paquete = paquete.Nombre;
                    cliente.estatus = estado.Descripcion;
                }
                }
                listCliente.Add(cliente);
               
            }
            foreach(var item in listCliente)
            {
                List<Cliente> clienteInstitucion = context.Cliente.Where(data => data.RUsuario == item.id && data.RStatus==1).ToList();
                if (clienteInstitucion.Count>0)
                {
                    List<Institucion> instituciones = context.Institucion.Where(data => clienteInstitucion.Any(t1 => data.IdInstitucion == t1.RInst)).ToList();
                    if (instituciones != null)
                        item.instituciones = instituciones;
                    else
                        item.instituciones = null;
                }
                else
                    item.instituciones = null;
            }
            return View(listCliente);
        }

        [Authorize]
        public IActionResult agregarCliente()
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            ViewBag.tipos = context.Tipousuario.Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.Descripcion,
                Value = parm.IdTipoUsuario.ToString()
            });
            return View();
        }

        [HttpPost]
        [Authorize]
        //Metodo para crear un cliente
        public async Task<IActionResult> agregarCliente(customUsuarioAdd e, List<IFormFile> files)
        {
            Usuario usuario = e.usuario;
            //crea contexto a la db
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            //Verifica si ese cliente no existe
            var usuarioExistente = context.Usuario.FirstOrDefault(usr => usr.NombreUsuario == usuario.NombreUsuario);
            if (usuarioExistente == null)
            {
                //Si no existe se crea el cliente
                usuario.RStatus = 1;
                var key = "J9Yu^q#wtpKkCxtjULcWW5pt=wVWX#2j";

                usuario.Contrasena = EncryptProvider.AESEncrypt(usuario.Contrasena, key);

                long sum = files.Sum(f => f.Length);
                if (sum > 0)
                {

                
                var filePath = Path.GetTempFileName();

                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images/user");
                foreach (var formFile in files)
                {


                    if (formFile.Length > 0)
                    {
                        string guid = Guid.NewGuid().ToString() + ".jpg";
                        using (var fileStream = new FileStream(Path.Combine(uploads, guid), FileMode.Create))
                        {

                            await formFile.CopyToAsync(fileStream);
                            usuario.ImgUsuario = guid;
                        }
                    }
                }
                }
                else
                {
                    usuario.ImgUsuario = "era-logo-icon.png";
                }
                context.Usuario.Add(usuario);
                context.SaveChanges();
                TempData["mensaje"] = "addCliente";
                return RedirectToAction("ListClientes", "Usuarios");
            }
            else
            {
                //Si existe se devuelve un mensaje de error
                TempData["mensaje"] = "usuarioExistente";
                return RedirectToAction("addCliente", "Administracion");
            }

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
            var institucion = context.Institucion.Where(data => data.IdInstitucion == id).FirstOrDefault();
            ViewBag.tipos = context.Tipoinstitucion.Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.Descripcion,
                Value = parm.IdTipoInst.ToString()
            });
            return View(institucion);
        }

        [Authorize]
        public IActionResult editarCliente(int id)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var cliente = context.Usuario.Where(data => data.IdUsuario == id).FirstOrDefault();
            ViewBag.tipos = context.Tipousuario.Select(parm => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = parm.Descripcion,
                Value = parm.IdTipoUsuario.ToString()
            });
            return View(cliente);
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        //Método que realiza la edicion de un usuario utilizando un objeto de tipo usuario
        public async Task<IActionResult> editarCliente(Usuario e, List<IFormFile> files)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var entity = context.Usuario.FirstOrDefault(usr => usr.IdUsuario == e.IdUsuario);

            if (entity != null)
            {
                entity.Nombre = e.Nombre;
                entity.Apellidos = e.Apellidos;
                entity.Direccion = e.Direccion;
                entity.Correo = e.Correo;
                entity.NombreUsuario = e.NombreUsuario;
                entity.Direccion = e.Direccion;
                entity.Telefono = e.Telefono;
                entity.Celular = e.Celular;



                if (entity.Contrasena != e.Contrasena)
                {
                    //Encriptación de password

                    var key = "J9Yu^q#wtpKkCxtjULcWW5pt=wVWX#2j";

                    e.Contrasena = EncryptProvider.AESEncrypt(e.Contrasena, key);
                    entity.Contrasena = e.Contrasena;
                }
                entity.RTipo = e.RTipo;



                long sum = files.Sum(f => f.Length);
                if (sum > 0) { 
                var filePath = Path.GetTempFileName();

                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images/user");
                foreach (var formFile in files)
                {


                    if (formFile.Length > 0)
                    {
                        string guid = Guid.NewGuid().ToString() + ".jpg";
                        using (var fileStream = new FileStream(Path.Combine(uploads, guid), FileMode.Create))
                        {

                            await formFile.CopyToAsync(fileStream);
                            entity.ImgUsuario = guid;
                        }
                    }
                }
                }
                context.Usuario.Update(entity);
                context.SaveChanges();
                TempData["mensaje"] = "usuarioEdit";
                return RedirectToAction("ListClientes", "Usuarios");

            }
            else
            {
                TempData["mensaje"] = "ErrorusuarioEdit";
                return RedirectToAction("ListClientes", "Usuarios");
            }


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
            TempData["mensaje"] = "addInstitucion";

            return RedirectToAction("ListClientes", "Usuarios");
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

                return RedirectToAction("ListClientes", "Usuarios");
            }
            else
            {
                TempData["mensaje"] = "errorEditInstitucion";
                return View();
            }
 
        }

        [Authorize]
        [HttpPost]
        public JsonResult getSelects()
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var listUsuarios = context.Usuario.Where(data => data.RTipo == 1).ToList();
            var listInstituciones = context.Institucion.Where(data => data.RStatus == 1).ToList();
                          
            var StringFinal = "";
            foreach (var str in listUsuarios)
            {
                if (str.IdUsuario == listUsuarios.First().IdUsuario)
                {
                    StringFinal = StringFinal + "<option selected value='" + str.IdUsuario + "'>" + str.Nombre+" "+ str.Apellidos+" | "+str.NombreUsuario + "</option>";
                }
                else
                    StringFinal = StringFinal + "<option value='" + str.IdUsuario + "'>" + str.Nombre + " " + str.Apellidos + " | " + str.NombreUsuario + "</option>";
            };

            var StringFinal2 = "";
            foreach (var str in listInstituciones)
            {
                if (str.IdInstitucion == listInstituciones.First().IdInstitucion)
                {
                    StringFinal2 = StringFinal2 + "<option selected value='" + str.IdInstitucion + "'>" + str.Nombre + " | "+str.Correo + "</option>";
                }
                else
                    StringFinal2 = StringFinal2 + "<option value='" + str.IdInstitucion + "'>" + str.Nombre + " | " + str.Correo + "</option>";
            };


            var result = new { usuarios = StringFinal, instituciones = StringFinal2 };

            return Json(result);

        }


        [Authorize]
        [HttpGet]
        public JsonResult asignarInstitucion(int idUsuario, int idInstitucion)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            Cliente verifica = context.Cliente.Where(data => data.RInst == idInstitucion && data.RUsuario == idUsuario).FirstOrDefault();
            if (verifica != null)
            {
                if (verifica.RStatus == 2)
                {
                    verifica.RStatus = 1;
                    context.SaveChanges();
                    TempData["mensaje"] = "asignaInstitucion";
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                
                Cliente nuevoCliente = new Cliente();
                nuevoCliente.RInst = idInstitucion;
                nuevoCliente.RUsuario = idUsuario;
                nuevoCliente.RStatus = 1;
                context.Cliente.Add(nuevoCliente);
                context.SaveChanges();
                TempData["mensaje"] = "asignaInstitucion";
                return Json(true);
            }


        }


        [Authorize]
        [HttpPost]
        public JsonResult desactivateInstitucion(int id, int idUser)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            Cliente verifica = context.Cliente.Where(data => data.RInst ==id  && data.RUsuario == idUser).FirstOrDefault();
            if (verifica != null)
            {
                verifica.RStatus = 2;
                context.SaveChanges();
                TempData["mensaje"] = "desacoplaInstitucion";
                return Json("Desactivado");
            }

            return Json(true);

        }



        [Authorize]
        [HttpPost]
        //Método que desactiva a un usuario creado, se invoca por medio de jQuery
        public JsonResult desactivateUsuario(int? id)
        {

            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            if (id == null)
                return Json(data: "No desactivado");

            var datos = context.Usuario.FirstOrDefault(usr => usr.IdUsuario == id) as Usuario;
            datos.RStatus = 2;
            context.Usuario.Update(datos);
            context.SaveChanges();
            return Json(data: "Desactivado");


        }



        [Authorize]
        [HttpPost]

        //Método que activa a un usuario creado, se invoca por medio de jQuery
        public JsonResult activateUsuario(int? id)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            if (id == null)
                return Json(data: "No activado");

            var datos = context.Usuario.FirstOrDefault(usr => usr.IdUsuario == id) as Usuario;
            datos.RStatus = 1;
            context.Usuario.Update(datos);
            context.SaveChanges();
            return Json(data: "Activado");

        }

    }
}