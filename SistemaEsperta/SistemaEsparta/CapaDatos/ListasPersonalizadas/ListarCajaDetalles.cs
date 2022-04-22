using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.ListasPersonalizadas
{
   public  class ListarCajaDetalles
    {

        public int Id { get; set; }//0
        public int CajaId { get; set; }//1
        public Guid? FacturaId { get; set; }//2
        public Guid? CompraId { get; set; }//3
        public string Descripcion { get; set; }//4
        public decimal Efectivo { get; set; }//5
        public decimal Cheques { get; set; }//6
        public decimal TarjetaCredito { get; set; }//7
        public decimal TarjetaDebito { get; set; }//8
        public decimal Egreso { get; set; }//9
        public decimal Transferencias { get; set; }//10
      //  public decimal TotalEgresos { get; set; }



    }
}
