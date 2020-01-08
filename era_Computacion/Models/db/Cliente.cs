using System;
using System.Collections.Generic;

namespace era_Computacion.Models.db
{
    public partial class Cliente
    {
        public Cliente()
        {
            Solicitud = new HashSet<Solicitud>();
        }

        public int IdCliente { get; set; }
        public int? RInst { get; set; }
        public int RUsuario { get; set; }
        public int RStatus { get; set; }

        public Institucion RInstNavigation { get; set; }
        public Status RStatusNavigation { get; set; }
        public Usuario RUsuarioNavigation { get; set; }
        public ICollection<Solicitud> Solicitud { get; set; }
    }
}
