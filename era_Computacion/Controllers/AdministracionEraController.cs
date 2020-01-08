using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using era_Computacion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace era_Computacion.Controllers
{
    public class AdministracionEra : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            String correo = HttpContext.Session.GetString("Correo");
            String pass = HttpContext.Session.GetString("Pass");

            if (HttpContext.Session.GetInt32("Type") == 0)
                return RedirectToAction("Logaut", "Account");
            int tipo = (int)HttpContext.Session.GetInt32("tipo");
            int id = (int)HttpContext.Session.GetInt32("Id_user");

            if (correo == null || pass == null || tipo == 0 || id == 0)
                return RedirectToAction("Logaut", "Account");

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
