using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.ListasPersonalizadas
{
    public class ListarProductos
    {
        // escribo lo que quiero listar en la vista, es personalizado
        // tiene que ser en orden

        public int Id { get; set; }//0
        public string CodigoReferencia { get; set; }//1
        public string Sucursal { get; set; }//2
        public string Descripcion { get; set; }//3
        public string Categoria { get; set; }//4
        public int Stock { get; set; }//5
        public string PeriodoMovimiento { get; set; }//6
        public string InventarioBajo { get; set; }//7
        public DateTime FechaModificacion { get; set; }//8
        
        public string Estado { get; set; }//9
        public string IncluyeColor { get; set; }//10
        public string Talla { get; set; }//11
        public string TomoEdicion { get; set; }//12

        public decimal PrecioVenta { get; set; }//13

        public decimal PrecioMayorista { get; set; }//14

        public decimal PrecioEntidadGubernamental { get; set; }//15
        public decimal PrecioCuentaClave { get; set; }//16
        public decimal PrecioRevendedor { get; set; }//17
        public decimal DescuentoEspecial { get; set; }//18
        public decimal Coste { get; set; }//19

        public string Notas { get; set; }//20

        public string DescipcionStockPrecio { get; set; }//21

        public string StockControl { get; set; }//22
      
        public string PermitirVenta { get; set; }//23
        public string PermitirCompra { get; set; }//24

        public string Ubicacion { get; set; }//25
        public DateTime FechaIngreso { get; set; }//26
        
        public decimal Impuesto { get; set; }//27
        public bool Deleted { get; set; }//28
        public bool  Acciones { get; set; }//29
        public byte[] Imagen { get; set; }//30
        public int Marca { get; set; }//posicion 30 total 31
        
    }
}
