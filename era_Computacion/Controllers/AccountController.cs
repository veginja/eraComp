using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using era_Computacion.Models;
using era_Computacion.Models.db;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;

namespace era_Computacion.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public IActionResult Login(string return_url)
        {
            if (return_url != null)
                ViewData.Add("return_url", HtmlEncoder.Default.Encode(return_url));
            else
                ViewData.Add("return_url", "");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var key = "J9Yu^q#wtpKkCxtjULcWW5pt=wVWX#2j";

                model.Password = EncryptProvider.AESEncrypt(model.Password, key);
                var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;
                Usuario user = context.Usuario.Where(us => (us.Correo == model.Email || us.NombreUsuario == model.Email) && us.Contrasena == model.Password && us.RStatus == 1).FirstOrDefault();
                if (user != null)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, model.Email));
                    var userIdentity = new ClaimsIdentity(claims, "login");
                    var principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync("PKAT", principal);

                    HttpContext.Session.SetString("Correo", user.Correo);

                    HttpContext.Session.SetString("nombre", user.Nombre+" "+user.Apellidos);
                    HttpContext.Session.SetString("img", user.ImgUsuario);
                    HttpContext.Session.SetString("Pass", user.Contrasena);

                    HttpContext.Session.SetInt32("tipo", user.RTipo);
                    HttpContext.Session.SetInt32("Id_user", user.IdUsuario);

                    if(user.RTipo==1)
                        return RedirectToAction("HistorialPedidos", "Historial");
                    
                    if(user.RTipo==2)
                        return RedirectToAction("Estadisticas", "Finanzas");
                }
                else
                {
                    TempData["mensaje"] = "errorLogin";
                    return RedirectToAction("Login", "Account");
                }
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Logaout()
        {
            await HttpContext.SignOutAsync("PKAT");

            HttpContext.Session.Remove("Correo");
            HttpContext.Session.Remove("Pass");
            HttpContext.Session.Remove("Type");
            HttpContext.Session.Remove("Id_user");

            return Redirect("/Account/Login");
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Create
        public ActionResult Registro()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registro(Usuario e)
        {
 
            //crea contexto a la db
            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            //Verifica si ese cliente no existe
            var usuarioExistente = context.Usuario.FirstOrDefault(usr => usr.NombreUsuario == e.NombreUsuario);
            if (usuarioExistente == null)
            {
                //Si no existe se crea el cliente
                e.RStatus = 1;
                var key = "J9Yu^q#wtpKkCxtjULcWW5pt=wVWX#2j";

                e.Contrasena = EncryptProvider.AESEncrypt(e.Contrasena, key);


                    e.ImgUsuario = "era-logo-icon.png";
                e.RTipo = 1;
                context.Usuario.Add(e);
                context.SaveChanges();
                TempData["mensaje"] = "registroExitoso";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                //Si existe se devuelve un mensaje de error
                TempData["mensaje"] = "usuarioExistente";
                return RedirectToAction("Registro", "Account");
            }
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Login));
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Login));
            }
            catch
            {
                return View();
            }
        }
    }
}