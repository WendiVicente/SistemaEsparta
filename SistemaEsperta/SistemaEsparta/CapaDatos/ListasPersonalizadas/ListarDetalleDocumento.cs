using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.ListasPersonalizadas
{
	public class ListarDetalleDocumento
	{
		public string Documento { get; set; }
		public string DetalleDocumentoId { get; set; }
		public int ProductoId { get; set; }
		public string Descripcion { get; set; }
		public int Cantidad { get; set; }
		public decimal Precio { get; set; }
		public decimal Descuento { get; set; }
		public decimal Subtotal { get; set; }
		public bool Devolver { get; set; }
    }
}
