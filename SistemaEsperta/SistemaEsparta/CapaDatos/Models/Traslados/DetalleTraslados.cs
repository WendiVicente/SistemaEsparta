using CapaDatos.Models.Pedidos;
using CapaDatos.Models.Productos;
using CapaDatos.Models.Productos.Combos;
using sharedDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Models.Traslados
{
    public class DetalleTraslados
    {
        public Guid Id { get; set; }

        public int? ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        //public decimal PrecioTotal { get; set; }
        
        public Guid? ComboId { get; set; }
        public int? DetalleColorId { get; set; }
        public int? DetalleTallaId { get; set; }
        public int? DetalleColorTallaId { get; set; }
        public int? DetalleTomoId { get; set; }
        public int? DetalleEdicionId { get; set; }
        public Guid TrasladosId { get; set; }
        public Traslados Traslados { get; set; }
        public Producto Producto { get; set; }
        public Combo Combo { get; set; }
        public DetalleColor DetalleColor { get; set; }
        public DetalleTalla DetalleTalla { get; set; }
        public DetalleColorTalla DetalleColorTalla { get; set; }
    }
}
