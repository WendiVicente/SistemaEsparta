using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.ListasPersonalizadas
{
    public class ListarTraslados
    {
        public Guid Id { get; set; }
        public string Vendedor_Solicitante { get; set; }
        public string Vendedor_Recibe { get; set; }

        public string SucursalEmisor { get; set; }
        public string SucursalReceptor { get; set; }


        public DateTime FechaRecepcion { get; set; }
        public DateTime? FechaValidacion { get; set; }
        public string Estado { get; set; } // 0 en peticion y 1 comprado

        public int NumeroTransaccion { get; set; }
        public DateTime? FechaLimite { get; set; }
        public string Activo { get; set; } // anulado o no
       // public bool IsSolicitud { get; set; }



    }
}
