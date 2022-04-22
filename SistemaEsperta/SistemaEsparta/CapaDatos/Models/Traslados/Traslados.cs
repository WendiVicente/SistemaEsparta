using CapaDatos.Models.Sucursales;
using CapaDatos.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Models.Traslados
{
   public  class Traslados
    {

        public Traslados()
        {
            Detalletraslados = new List<DetalleTraslados>();

        }

        public Guid Id { get; set; }
        [ForeignKey("User")]
        public string VendedorSolicitante { get; set; }
      //  [ForeignKey("User")]
        public string VendedorRecibe { get; set; }
       
        [ForeignKey("Sucursal")]
        public int IdSucursalEmisor { get; set; }
       // [ForeignKey("Sucursal")]
        public int IdSucursalReceptor { get; set; }


        public DateTime FechaRecepcion { get; set; }
        public DateTime? FechaValidacion { get; set; }
        public bool Estado { get; set; } // 0 en peticion y 1 comprado

        public int NumeroTransaccion { get; set; }
        public DateTime? FechaLimite { get; set; }
        public bool IsActive { get; set; } // anulado o no
        public bool IsSolicitud { get; set; }
         public Sucursal Sucursal { get; set; }
         public User User { get; set; }

        public ICollection<DetalleTraslados> Detalletraslados { get; set; }

    }
}
