﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Models.Pedidos
{
    public class TipoOperacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaIngreso { get; set; }

    }
}