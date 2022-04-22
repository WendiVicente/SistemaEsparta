using CapaDatos.Models.Productos;
using sharedDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharedDatabase.Models
{
    public class Devoluciones
    {
        public Devoluciones()
        { }

        public int Id { get; set; }
        public string DetalleDocumentoId { get; set; }
        public string TipoDocumento { get; set; }
        public int? ProductoId { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal SubTotal { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public string Vendedor { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int? DetalleColorId { get; set; }
        public int? DetalleTallaId { get; set; }
        public int? DetalleColorTallaId { get; set; }
        public int? DetalleTomoEdicionId { get; set; }
        public Guid CuentaBCId { get; set; }
    }
}
