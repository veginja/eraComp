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
using NETCore.Encrypt;

namespace era_Computacion.Controllers
{
    public class EditarPerfilController : Controller
    {

        private IHostingEnvironment _hostingEnvironment;
        public EditarPerfilController(IHostingEnvironment enviroment)
        {
            _hostingEnvironment = enviroment;
        }
        [Authorize]
        public IActionResult editarInformacion()
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var cliente = context.Usuario.Where(data => data.IdUsuario == HttpContext.Session.GetInt32("Id_user")).FirstOrDefault();

            return View(cliente);
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        //Método que realiza la edicion de un usuario utilizando un objeto de tipo usuario
        public async Task<IActionResult> editarInformacion(Usuario e, List<IFormFile> files)
        {
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
            var entity = context.Usuario.FirstOrDefault(usr => usr.IdUsuario == HttpContext.Session.GetInt32("Id_user"));

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
                                entity.ImgUsuario = guid;
                            }
                        }
                    }
                }
                context.Usuario.Update(entity);
                context.SaveChanges();
                TempData["mensaje"] = "usuarioEdit";
                return RedirectToAction("editarInformacion", "EditarPerfil");

            }
            else
            {
                TempData["mensaje"] = "ErrorusuarioEdit";
                return RedirectToAction("editarInformacion", "EditarPerfil");
            }


        }

    }
}