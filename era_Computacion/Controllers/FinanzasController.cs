using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using era_Computacion.Models;

using era_Computacion.Models.custom;
using era_Computacion.Models.db;
using Microsoft.AspNetCore.Mvc;

namespace era_Computacion.Controllers
{
    public class FinanzasController : Controller
    {
        public IActionResult Estadisticas()
        {
            return View();
        }

        [HttpPost]
        public JsonResult showMesesEmpresa()
        {

            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            string returnString = "";
            returnString = "<option >Selecciona un mes</option>";

            int len = 0;

            string currentMonth = DateTime.Now.Month.ToString();
            short currentMontInt = Convert.ToInt16(currentMonth);
            for (int i = 1; i < 13; i++)
            {


                if (i == 1)
                    if (i == currentMontInt)
                        returnString = returnString + "<option selected value=" + i + ">Enero</option>";
                    else
                        returnString = returnString + "<option value=" + i + ">Enero</option>";
                if (i == 2)
                    if (i == currentMontInt)
                        returnString = returnString + "<option selected value=" + i + ">Febrero</option>";
                    else
                        returnString = returnString + "<option value=" + i + ">Febrero</option>";
                if (i == 3)
                    if (i == currentMontInt)
                        returnString = returnString + "<option selected value=" + i + ">Marzo</option>";
                    else
                        returnString = returnString + "<option value=" + i + ">Marzo</option>";
                if (i == 4)
                    if (i == currentMontInt)
                        returnString = returnString + "<option selected value=" + i + ">Abril</option>";
                    else
                        returnString = returnString + "<option value=" + i + ">Abril</option>";
                if (i == 5)
                    if (i == currentMontInt)
                        returnString = returnString + "<option selected value=" + i + ">Mayo</option>";
                    else
                        returnString = returnString + "<option value=" + i + ">Mayo</option>";
                if (i == 6)
                    if (i == currentMontInt)
                        returnString = returnString + "<option selected value=" + i + ">Junio</option>";
                    else
                        returnString = returnString + "<option value=" + i + ">Junio</option>";
                if (i == 7)
                    if (i == currentMontInt)
                        returnString = returnString + "<option selected value=" + i + ">Julio</option>";
                    else
                        returnString = returnString + "<option value=" + i + ">Julio</option>";
                if (i == 8)
                    if (i == currentMontInt)
                        returnString = returnString + "<option selected value=" + i + ">Agosto</option>";
                    else
                        returnString = returnString + "<option value=" + i + ">Agosto</option>";
                if (i == 9)
                    if (i == currentMontInt)
                        returnString = returnString + "<option selected value=" + i + ">Septiembre</option>";
                    else
                        returnString = returnString + "<option value=" + i + ">Septiembre</option>";
                if (i == 10)
                    if (i == currentMontInt)
                returnString = returnString + "<option selected value=" + i + ">Octubre</option>";
            else
                returnString = returnString + "<option value=" + i + ">Octubre</option>";
            if (i == 11)
                    if (i == currentMontInt)
                returnString = returnString + "<option selected value=" + i + ">Noviembre</option>";
            else
                returnString = returnString + "<option value=" + i + ">Noviembre</option>";
            if (i == 12)
                    if (i == currentMontInt)
                returnString = returnString + "<option selected value=" + i + ">Diciembre</option>";
            else
                returnString = returnString + "<option value=" + i + ">Diciembre</option>";


            len++;


        }

                return Json(new { data = returnString, len = len});


        }

        [HttpPost]
        public JsonResult showAnioEmpresa()
        {


            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;


            string returnString = "";
            returnString = "<option >Selecciona un año</option>";
            string currentYear = DateTime.Now.Year.ToString();
            short currentYearInt = Convert.ToInt16(currentYear);
            int len = 0;

            for (int i=2019;i<2024;i++)
            {
                if (i == currentYearInt)
                    returnString = returnString + "<option selected value=" + i + ">" + i + "</option>";
                else
                    returnString = returnString + "<option value=" + i + ">" + i + "</option>";
                len++;


            }

            return Json(new { data = returnString, len = len });


        }



        [HttpGet]
        public JsonResult getRubros(int anio, int mes)
        {

            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            var detalleVenta = context.Detalleventa.Where(data=>data.Fecha.Year== anio && data.Fecha.Month==mes).ToList();

            List<graficaBarras> listBarras = new List<graficaBarras>();
            foreach(var item in detalleVenta)
            {
                
                var solicitud = context.Solicitud.Where(data => data.IdSolicitud == item.RSolicitud).FirstOrDefault();
                var paquete = context.Paquete.Where(data => data.IdPaquete == solicitud.RPaquete).FirstOrDefault();

                if(listBarras.Where(data=>data.paquete == paquete).FirstOrDefault() != null)
                {
                    var barraUpdate = listBarras.Where(data => data.paquete == paquete).FirstOrDefault();
                    barraUpdate.cantvendidos += 1;
                }
                else
                {
                    graficaBarras elemento = new graficaBarras();
                    elemento.paquete = paquete;
                    elemento.cantvendidos += 1;
                    listBarras.Add(elemento);

                }
            }

            List<graficaBarrasDato> listDatos = new List<graficaBarrasDato>();
            foreach(var item in listBarras)
            {
                graficaBarrasDato dato = new graficaBarrasDato();
                var productosporpaquete = context.Productosxpaquete.Where(data => data.RPaquetes == item.paquete.IdPaquete).ToList();
                foreach(var item2 in productosporpaquete)
                {
                    var producto = context.Productos.Where(data => data.IdProductos == item2.RProductos).FirstOrDefault();
                    dato.ganancia += producto.PrecioVentaXunidad - producto.PrecioCompraXunidad;
                }
                dato.ganancia = dato.ganancia * item.cantvendidos;
                dato.nombrePaquete = item.paquete.Nombre;
                listDatos.Add(dato);
            }
            listDatos = listDatos.OrderByDescending(data => data.ganancia).Take(5).ToList();
            var rangoValores = listDatos.ToList();


            int i = 8434687;


            List<ObjectCharJSPeriodos> listObjetChartJs = new List<ObjectCharJSPeriodos>();


            ObjectCharJSPeriodos objectCharJS = new ObjectCharJSPeriodos();


            //List de data
            List<double?> listDouble = new List<double?>();


            //List de string 
            List<string> listString = new List<string>();


            //List de string 
            List<string> pointList = new List<string>();

            //String de label
            string stringLabel = "Rubros";

            foreach (var item in rangoValores)
            {

                listDouble.Add(item.ganancia);

                pointList.Add("#" + (i).ToString("X"));
                listString.Add("#" + i.ToString("X"));
                i = i + 800;
            }



            objectCharJS.data = listDouble;
            objectCharJS.backgroundColor = listString;
            objectCharJS.borderColor = "whitesmoke";
            objectCharJS.pointBackgroundColor = pointList;
            objectCharJS.label = stringLabel;
            objectCharJS.borderWidth = 1.5;
            objectCharJS.fill = "" + false;

            listObjetChartJs.Add(objectCharJS);



            RootObjectChartJsPeriodo rootObject = new RootObjectChartJsPeriodo();
            rootObject.datasets = listObjetChartJs;


            List<string> labels = new List<string>();


            foreach (var item2 in rangoValores)
            {
                labels.Add(item2.nombrePaquete);
            }

            rootObject.labels = labels;


            return Json(rootObject);


        }


        [HttpGet]
        public JsonResult getTipoPoliza(int anio, int mess)
        {

            var context = HttpContext.RequestServices.GetService(typeof(eracomputacionContext)) as eracomputacionContext;

            var detalleVenta = context.Detalleventa.Where(data => data.Fecha.Year == anio && data.Fecha.Month == mess).ToList();

            List<graficaBarras> listBarras = new List<graficaBarras>();
            foreach (var item in detalleVenta)
            {

                var solicitud = context.Solicitud.Where(data => data.IdSolicitud == item.RSolicitud).FirstOrDefault();
                var paquete = context.Paquete.Where(data => data.IdPaquete == solicitud.RPaquete).FirstOrDefault();

                if (listBarras.Where(data => data.paquete == paquete).FirstOrDefault() != null)
                {
                    var barraUpdate = listBarras.Where(data => data.paquete == paquete).FirstOrDefault();
                    barraUpdate.cantvendidos += 1;
                }
                else
                {
                    graficaBarras elemento = new graficaBarras();
                    elemento.paquete = paquete;
                    elemento.cantvendidos += 1;
                    listBarras.Add(elemento);

                }
            }

            listBarras = listBarras.OrderByDescending(data => data.cantvendidos).Take(5).ToList();
            var rangoValores = listBarras.ToList();



            int i = 8434687;


            List<ObjectCharJSPeriodos> listObjetChartJs = new List<ObjectCharJSPeriodos>();


            ObjectCharJSPeriodos objectCharJS = new ObjectCharJSPeriodos();


            //List de data
            List<double?> listDouble = new List<double?>();


            //List de string 
            List<string> listString = new List<string>();


            //List de string 
            List<string> pointList = new List<string>();

            //String de label
            string stringLabel = "Tipos poliza";

            foreach (var item in rangoValores)
            {

                listDouble.Add(item.cantvendidos);

                pointList.Add("#" + (i).ToString("X"));
                listString.Add("#" + i.ToString("X"));
                i = i + 1200;
            }



            objectCharJS.data = listDouble;
            objectCharJS.backgroundColor = listString;
            objectCharJS.borderColor = "whitesmoke";
            objectCharJS.pointBackgroundColor = pointList;
            objectCharJS.label = stringLabel;
            objectCharJS.borderWidth = 1.5;
            objectCharJS.fill = "" + false;

            listObjetChartJs.Add(objectCharJS);



            RootObjectChartJsPeriodo rootObject = new RootObjectChartJsPeriodo();
            rootObject.datasets = listObjetChartJs;


            List<string> labels = new List<string>();


            foreach (var item2 in rangoValores)
            {
                labels.Add(item2.paquete.Nombre);
            }

            rootObject.labels = labels;


            return Json(rootObject);


        }

    }
}