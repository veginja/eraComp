#pragma checksum "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dd77f950ec33b4ada5e692f51ef7b3eaeb958397"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Solicitudes_ListSolicitudes), @"mvc.1.0.view", @"/Views/Solicitudes/ListSolicitudes.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Solicitudes/ListSolicitudes.cshtml", typeof(AspNetCore.Views_Solicitudes_ListSolicitudes))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\_ViewImports.cshtml"
using era_Computacion;

#line default
#line hidden
#line 2 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\_ViewImports.cshtml"
using era_Computacion.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dd77f950ec33b4ada5e692f51ef7b3eaeb958397", @"/Views/Solicitudes/ListSolicitudes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8be5ae78e3179e1b1dae5bf89f16bb81779f55dc", @"/Views/_ViewImports.cshtml")]
    public class Views_Solicitudes_ListSolicitudes : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<era_Computacion.Models.solicitudCustom>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(60, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
#line 4 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
  
    ViewData["Title"] = "Solicitudes";

    string ru = ViewData["ReturnURL"] as string;

#line default
#line hidden
            BeginContext(163, 462, true);
            WriteLiteral(@"

<div class=""container-fluid"">
    <!-- ============================================================== -->
    <!-- Start Page Content -->
    <!-- ============================================================== -->
    <div class=""row"">

        <div class=""col-12"">
            <div class=""page-breadcrumb"">
                <div class=""row align-items-center"">
                    <div class=""col-5"">
                        <h4 class=""page-title"">");
            EndContext();
            BeginContext(626, 17, false);
#line 21 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                          Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(643, 450, true);
            WriteLiteral(@"</h4>
                        <div class=""d-flex align-items-center"">

                        </div>
                    </div>
                    <div class=""col-7"">
                        <div class=""row col-md-12"">
                            <div class=""col-md-12 col-lg-12 col-xl-12 text-right"">
                                <br />
                                <div class="" upgrade-btn"">
                                    <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1093, "\"", 1145, 1);
#line 31 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
WriteAttributeValue("", 1100, Url.Action("agregarSolicitud","Solicitudes"), 1100, 45, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1146, 1652, true);
            WriteLiteral(@" class=""btn btn-info text-white"">Agregar solicitud</a>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <br />
            </div>
            <div class=""card"">
                <div class=""card-body"">


                    <div class=""table-responsive "">
                        <table id=""tablaPaquetes"" class=""table"">
                            <thead>


                                <tr>
                                    <th>
                                        ID
                                    </th>
                                    <th>
                                        Cliente
                                    </th>
                                    <th>
                                        Paquete
                                    </th>
                                    <th>
                                        Fecha
 ");
            WriteLiteral(@"                                   </th>
                                    <th>
                                        Monto
                                    </th>
                                    <th>
                                        Comentarios
                                    </th>
                                    <th> Estado </th>
                                    <th>
                                        Editar / Mostrar
                                    </th>
                                </tr>
                            </thead>
                            <tbody>


");
            EndContext();
#line 77 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                 foreach (var item in Model)
                                {

#line default
#line hidden
            BeginContext(2895, 132, true);
            WriteLiteral("                                    <tr>\r\n                                        <td>\r\n                                            ");
            EndContext();
            BeginContext(3028, 56, false);
#line 81 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.solicitud.IdSolicitud));

#line default
#line hidden
            EndContext();
            BeginContext(3084, 139, true);
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
            EndContext();
            BeginContext(3224, 89, false);
#line 84 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.solicitud.RClienteNavigation.RUsuarioNavigation.Nombre));

#line default
#line hidden
            EndContext();
            BeginContext(3313, 3, true);
            WriteLiteral(" | ");
            EndContext();
            BeginContext(3317, 86, false);
#line 84 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                                                                                                                    Write(Html.DisplayFor(modelItem => item.solicitud.RClienteNavigation.RInstNavigation.Nombre));

#line default
#line hidden
            EndContext();
            BeginContext(3403, 139, true);
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
            EndContext();
            BeginContext(3543, 70, false);
#line 87 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.solicitud.RPaqueteNavigation.Nombre));

#line default
#line hidden
            EndContext();
            BeginContext(3613, 139, true);
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
            EndContext();
            BeginContext(3753, 59, false);
#line 90 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.solicitud.FechaSolicitud));

#line default
#line hidden
            EndContext();
            BeginContext(3812, 159, true);
            WriteLiteral("\r\n                                        </td>\r\n                                        <td class=\"text-center\">\r\n                                            ");
            EndContext();
            BeginContext(3972, 60, false);
#line 93 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.solicitud.PrecioSolicitud));

#line default
#line hidden
            EndContext();
            BeginContext(4032, 95, true);
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n");
            EndContext();
#line 96 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                               var itemLast = item.arreglo.LastOrDefault();
                                                int control = 0;

#line default
#line hidden
            BeginContext(4287, 43, true);
            WriteLiteral("                                           ");
            EndContext();
#line 98 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                            foreach(var item2 in item.arreglo)
                                           {
                                               if(item2.producto.Cantidad< item2.productosporpaquete.Cantidad && item.solicitud.RStatus!=6)
                                               {
                                                   

#line default
#line hidden
            BeginContext(4656, 150, false);
#line 102 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                              Write(Html.Raw("- Necesitas " + (item2.productosporpaquete.Cantidad - item2.producto.Cantidad )+" "+item2.producto.NombreProd +" para armar el paquete<br>"));

#line default
#line hidden
            EndContext();
#line 102 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                                                                                                                                                                                          ;
                                                   control = 1;
                                               }
                                               else
                                               {
                                                   if (itemLast == item2 && control==0)
                                                   {
                                                       

#line default
#line hidden
            BeginContext(5226, 27, false);
#line 109 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                                  Write(Html.Raw("Sin comentarios"));

#line default
#line hidden
            EndContext();
#line 109 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                                                                   ;
                                                   }
                                               }
                                           }

#line default
#line hidden
            BeginContext(5406, 92, true);
            WriteLiteral("                                        </td>\r\n\r\n                                        <td");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 5498, "\"", 5564, 2);
#line 115 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
WriteAttributeValue("", 5503, Html.DisplayFor(modelItem => item.solicitud.IdSolicitud), 5503, 57, false);

#line default
#line hidden
            WriteAttributeValue("", 5560, "Dato", 5560, 4, true);
            EndWriteAttribute();
            BeginContext(5565, 71, true);
            WriteLiteral(">\r\n                                            <div class=\"dropdown\">\r\n");
            EndContext();
#line 117 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                                  if (item.solicitud.RStatus == 4)
                                                    {

#line default
#line hidden
            BeginContext(5775, 348, true);
            WriteLiteral(@"                                                        <button class=""btn btn-warning dropdown-toggle"" type=""button"" id=""dropdownMenuButton"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                                                            Pendiente
                                                        </button>
");
            EndContext();
#line 122 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                                    }
                                                    else if (item.solicitud.RStatus == 5)
                                                    {

#line default
#line hidden
            BeginContext(6324, 347, true);
            WriteLiteral(@"                                                        <button class=""btn btn-danger dropdown-toggle"" type=""button"" id=""dropdownMenuButton"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                                                            Rechazado
                                                        </button>
");
            EndContext();
#line 128 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                                    }
                                                    else if (item.solicitud.RStatus == 6)
                                                    {

#line default
#line hidden
            BeginContext(6872, 347, true);
            WriteLiteral(@"                                                        <button class=""btn btn-success dropdown-toggle"" type=""button"" id=""dropdownMenuButton"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                                                            Aceptado
                                                        </button>
");
            EndContext();
#line 134 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                                    }

                                                

#line default
#line hidden
            BeginContext(7327, 184, true);
            WriteLiteral("\r\n                                            <div class=\"dropdown-menu\" aria-labelledby=\"dropdownMenuButton\">\r\n                                                <a class=\"dropdown-item\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 7511, "\"", 7559, 3);
            WriteAttributeValue("", 7521, "pendiente(", 7521, 10, true);
#line 139 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
WriteAttributeValue("", 7531, item.solicitud.IdSolicitud, 7531, 27, false);

#line default
#line hidden
            WriteAttributeValue("", 7558, ")", 7558, 1, true);
            EndWriteAttribute();
            BeginContext(7560, 97, true);
            WriteLiteral(" href=\"#\">Pendiente</a>\r\n                                                <a class=\"dropdown-item\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 7657, "\"", 7705, 3);
            WriteAttributeValue("", 7667, "rechazado(", 7667, 10, true);
#line 140 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
WriteAttributeValue("", 7677, item.solicitud.IdSolicitud, 7677, 27, false);

#line default
#line hidden
            WriteAttributeValue("", 7704, ")", 7704, 1, true);
            EndWriteAttribute();
            BeginContext(7706, 25, true);
            WriteLiteral(" href=\"#\">Rechazado</a>\r\n");
            EndContext();
#line 141 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                                 if (control == 0)
                                                {

#line default
#line hidden
            BeginContext(7850, 76, true);
            WriteLiteral("                                                    <a class=\"dropdown-item\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 7926, "\"", 7973, 3);
            WriteAttributeValue("", 7936, "aceptado(", 7936, 9, true);
#line 143 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
WriteAttributeValue("", 7945, item.solicitud.IdSolicitud, 7945, 27, false);

#line default
#line hidden
            WriteAttributeValue("", 7972, ")", 7972, 1, true);
            EndWriteAttribute();
            BeginContext(7974, 24, true);
            WriteLiteral(" href=\"#\">Aceptado</a>\r\n");
            EndContext();
#line 144 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                                }

#line default
#line hidden
            BeginContext(8049, 201, true);
            WriteLiteral("\r\n                                            </div>\r\n                                            </div>\r\n\r\n                                        </td>\r\n                                        <td>\r\n");
            EndContext();
#line 151 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                             if (item.solicitud.RStatus!=6 )
                                            {

#line default
#line hidden
            BeginContext(8375, 74, true);
            WriteLiteral("                                            <a class=\'btn btn-info btm-sm\'");
            EndContext();
            BeginWriteAttribute("href", " href=\'", 8449, "\'", 8545, 2);
            WriteAttributeValue("", 8456, "/Solicitudes/editarSolicitud?id=", 8456, 32, true);
#line 153 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
WriteAttributeValue("", 8488, Html.DisplayFor(modelItem => item.solicitud.IdSolicitud), 8488, 57, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(8546, 33, true);
            WriteLiteral("><i class=\'fa fa-edit\'></i></a>\r\n");
            EndContext();
#line 154 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                            }
                                            else
                                            {
                                                

#line default
#line hidden
            BeginContext(8772, 32, false);
#line 157 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                           Write(Html.Raw("No es posible editar"));

#line default
#line hidden
            EndContext();
#line 157 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                                                                 ;
                                            }

#line default
#line hidden
            BeginContext(8854, 92, true);
            WriteLiteral("\r\n                                        </td>\r\n                                    </tr>\r\n");
            EndContext();
#line 162 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
                                }

#line default
#line hidden
            BeginContext(8981, 2742, true);
            WriteLiteral(@"                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- ============================================================== -->
    <!-- End PAge Content -->
    <!-- ============================================================== -->
    <!-- ============================================================== -->
    <!-- Right sidebar -->
    <!-- ============================================================== -->
    <!-- .right-sidebar -->
    <!-- ============================================================== -->
    <!-- End Right sidebar -->
    <!-- ============================================================== -->
</div>
<!-- Page Heading -->

<script>

    function pendiente(id) {
        alert(id);
    }
    
    $(document).ready(function calldata() {

        $('#tablaPaquetes').DataTable({
            ""language"": {
                ""sProcessing"": ""Procesando...");
            WriteLiteral(@""",
                ""sLengthMenu"": ""Mostrar _MENU_ registros"",
                ""sZeroRecords"": ""No se encontraron resultados"",
                ""sEmptyTable"": ""Ningún dato disponible en esta tabla"",
                ""sInfo"": ""Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros"",
                ""sInfoEmpty"": ""Mostrando registros del 0 al 0 de un total de 0 registros"",
                ""sInfoFiltered"": ""(filtrado de un total de _MAX_ registros)"",
                ""sInfoPostFix"": """",
                ""sSearch"": ""Buscar:"",
                ""sUrl"": """",
                ""sInfoThousands"": "","",
                ""sLoadingRecords"": ""Cargando..."",
                ""oPaginate"": {
                    ""sFirst"": ""Primero"",
                    ""sLast"": ""Último"",
                    ""sNext"": ""Siguiente"",
                    ""sPrevious"": ""Anterior""
                },
                ""oAria"": {
                    ""sSortAscending"": "": Activar para ordenar la columna de manera ascendente"",
      ");
            WriteLiteral(@"              ""sSortDescending"": "": Activar para ordenar la columna de manera descendente""
                }
            },
            ""destroy"": true,
            ""pageLength"": 10,
            ""order"": [[0, ""desc""]],
            'bVisible': false,

            ""aoColumnDefs"": [
                { ""sClass"": ""text-center"", ""aTargets"": [2, 3] }
            ]
        });

    });
</script>

<script>


    function pendiente(DataID) {
        if (confirm(""¿Está seguro de establecer el estado: Pendiente?"")) {
            pendienteF(DataID);
        }
        else {
            return false;
        }
    }


    function pendienteF(DataID) {
        var url = '");
            EndContext();
            BeginContext(11724, 17, false);
#line 244 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
              Write(Url.Content("~/"));

#line default
#line hidden
            EndContext();
            BeginContext(11741, 521, true);
            WriteLiteral(@"' + ""Solicitudes/establecerPendiente"";
        $.post(url, { ID: DataID }, function (data) {
            if (data == ""Pendiente"") {
                window.location.href=""/Solicitudes/ListSolicitudes""
            } 
        });
    }


        function rechazado(DataID) {
        if (confirm(""¿Está seguro de establecer el estado: Rechazado?"")) {
            canceladoF(DataID);
        }
        else {
            return false;
        }
    }


    function canceladoF(DataID) {
        var url = '");
            EndContext();
            BeginContext(12263, 17, false);
#line 264 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
              Write(Url.Content("~/"));

#line default
#line hidden
            EndContext();
            BeginContext(12280, 703, true);
            WriteLiteral(@"' + ""Solicitudes/establecerCancelado"";
        $.post(url, { ID: DataID }, function (data) {
            if (data == ""Cancelado"") {
                window.location.href=""/Solicitudes/ListSolicitudes""
            } 
        });
    }

            function aceptado(DataID) {
                if (confirm(""¿Está seguro de establecer el estado: Aceptado?"")) {
                    var monto = prompt(""Monto de importes extra"");
                    if (monto == null)
                        monto = 0;
                  
            aceptadoF(DataID,monto);
        }
        else {
            return false;
        }
    }


    function aceptadoF(DataID,monto) {
        var url = '");
            EndContext();
            BeginContext(12984, 17, false);
#line 287 "C:\Users\agus_\Downloads\EraCom-master\era_Computacion\era_Computacion\Views\Solicitudes\ListSolicitudes.cshtml"
              Write(Url.Content("~/"));

#line default
#line hidden
            EndContext();
            BeginContext(13001, 263, true);
            WriteLiteral(@"' + ""Solicitudes/establecerAceptado"";
        $.post(url, { ID: DataID,monto:monto }, function (data) {
            if (data == ""Aceptado"") {
                window.location.href=""/Solicitudes/ListSolicitudes""
            } 
        });
    }

</script>
");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<era_Computacion.Models.solicitudCustom>> Html { get; private set; }
    }
}
#pragma warning restore 1591
