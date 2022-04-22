using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.ListasPersonalizadas
{
    public class ListarDevoluciones
    {
        public object DetalleDocumentoId { get; set; }
        public int ProductoId { get; set; }
        public string Documento { get; set; }
        public string Descripcion { get; set; }
        public string TipoDetalle { get; set; }
        public string Detalle { get; set; }
        public int TipoDetalleId { get; set; }
        //public string Talla { get; set; }
        //public string Tomo { get; set; }
        //public string Edicion { get; set; }
        public DateTime Fecha { get; set; }

        //public int ColorId { get; set; }
        //public int TallaId { get; set; }
        //public int TomoEdicionId { get; set; }
        public int Cantidad { get; set; }

    }
}
