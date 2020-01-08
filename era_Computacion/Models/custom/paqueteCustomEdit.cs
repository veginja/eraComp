using era_Computacion.Models.db;
using System;
using System.Collections.Generic;

namespace era_Computacion.Models
{
    public partial class paqueteCustomEdit
    {
        public paqueteCustomEdit()
        {

        }

        public int? producto { get; set; }
        public int? cantidad { get; set; }
    }
}
