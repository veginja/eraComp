using era_Computacion.Models.db;
using System;
using System.Collections.Generic;

namespace era_Computacion.Models
{
    public partial class customUsuarioAdd
    {
        public customUsuarioAdd()
        {

        }

        public Usuario usuario { get; set; }
        public Institucion institucion { get; set; }

    }
}
