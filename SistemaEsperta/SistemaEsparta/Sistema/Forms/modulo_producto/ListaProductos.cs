using CapaDatos.Data;
using CapaDatos.ListasPersonalizadas;
using CapaDatos.Repository;
using ComponentFactory.Krypton.Toolkit;
using Sistema.Reports.Reports_Productos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Forms.modulo_producto
{
    public partial class ListaProductos : BaseContext
    {
        private ProductosRepository _productosRepository = null;
        private IList<ListarProductos> _listadolocal = null;

        public ListaProductos()
        {
            _productosRepository = new ProductosRepository(_context);
            _listadolocal = new List<ListarProductos>();
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            RefrescarDataGridProductos(false);
            base.OnLoad(e);
        }

        public void RefrescarDataGridProductos(bool loadNewContext = true)
        {
            if (loadNewContext)
            {
                _context = null;
                _context = new Context();
                _productosRepository = null;
                _productosRepository = new ProductosRepository(_context);
            }

            BindingSource source = new BindingSource();
            _listadolocal = _productosRepository.GetList(UsuarioLogeadoSistemas.User.SucursalId);

            source.DataSource = _listadolocal;
            listproductos.DataSource = typeof(List<>);
            listproductos.DataSource = source;
            //listproductos.AutoResizeColumns();
        }

        public void RefrescaProductoGrid(int index, ListarProductos producto)
        {
            _listadolocal[index] = producto;
            listproductos.Rows[index].Cells[0].Value = producto.Id;
            listproductos.Rows[index].Cells[1].Value = producto.CodigoReferencia;
            listproductos.Rows[index].Cells[2].Value = producto.Sucursal;
            listproductos.Rows[index].Cells[3].Value = producto.Descripcion;
            listproductos.Rows[index].Cells[4].Value = producto.Categoria;
            listproductos.Rows[index].Cells[5].Value = producto.Stock;
            listproductos.Rows[index].Cells[6].Value = producto.PeriodoMovimiento;
            listproductos.Rows[index].Cells[7].Value = producto.InventarioBajo;
            listproductos.Rows[index].Cells[8].Value = producto.FechaModificacion;
            listproductos.Rows[index].Cells[9].Value = producto.Estado;
            listproductos.Rows[index].Cells[10].Value = producto.IncluyeColor;
            listproductos.Rows[index].Cells[11].Value = producto.Talla;
            listproductos.Rows[index].Cells[12].Value = producto.PrecioVenta;
            listproductos.Rows[index].Cells[13].Value = producto.PrecioMayorista;
            listproductos.Rows[index].Cells[14].Value = producto.PrecioEntidadGubernamental;
            listproductos.Rows[index].Cells[15].Value = producto.PrecioCuentaClave;
            listproductos.Rows[index].Cells[16].Value = producto.PrecioRevendedor;
            listproductos.Rows[index].Cells[17].Value = producto.DescuentoEspecial;
            listproductos.Rows[index].Cells[18].Value = producto.Coste;
            listproductos.Rows[index].Cells[19].Value = producto.Notas;
            listproductos.Rows[index].Cells[20].Value = producto.StockControl;
            listproductos.Rows[index].Cells[21].Value = producto.PermitirVenta;
            listproductos.Rows[index].Cells[22].Value = producto.PermitirCompra;
            listproductos.Rows[index].Cells[23].Value = producto.Ubicacion;
            listproductos.Rows[index].Cells[24].Value = producto.FechaIngreso;
            listproductos.Rows[index].Cells[25].Value = producto.Impuesto;
            listproductos.Rows[index].Cells[26].Value = producto.Deleted;
            listproductos.Rows[index].Cells[27].Value = producto.Acciones;
            listproductos.Rows[index].Cells[28].Value = producto.Imagen;
        }

        private void btnDetalleP_Click(object sender, EventArgs e)
        {
            if (listproductos.CurrentRow == null)
            {
                return;
            }
            int index = listproductos.CurrentRow.Index;
            var producto = (ListarProductos)listproductos.CurrentRow.DataBoundItem;
            var Getproducto = _productosRepository.Get(producto.Id);

            EditarProducto childForm = new EditarProducto(this, Getproducto); // me sirve para refrescar el dg cie el thiscunado regrese
            childForm.indiceProducto = index;
            childForm.Show();
        }

        private void buscarprod_Click(object sender, EventArgs e)
        {
            //BuscarProducto buscarproducto = new BuscarProducto(listproductos);
            //buscarproducto.Show();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            RefrescarDataGridProductos(true); // al refrescar activa el nuevo contexto()

        }

        public void ActualizarProducto(int index, ListarProductos lista)
        { 
            //listproductos.Rows[index] = prod;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (listproductos.CurrentRow == null)
            {
                return;
            }

            var dialog = KryptonMessageBox.Show("¿Está seguro que desea eliminar el producto de la lista?", "Eliminar producto",
               MessageBoxButtons.YesNoCancel,
               MessageBoxIcon.Question,
               MessageBoxDefaultButton.Button2);
            // dale proba okoko

            if (dialog== DialogResult.Yes)
            {
                var productolista = (ListarProductos)listproductos.CurrentRow.DataBoundItem;
                var productoObtenido = _productosRepository.Get(productolista.Id);
                productoObtenido.Deleted = true;
                _productosRepository.Update(productoObtenido);
                RefrescarDataGridProductos(true);

            }
            

        }

        private void listproductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ListaProductos_Load(object sender, EventArgs e)
        {

        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReporteProducto reporte = new ReporteProducto();
            reporte.Show();
        }

        private void TxtBuscador_TextChanged(object sender, EventArgs e)
        {
            var filter = _listadolocal.Where(a => a.Categoria != null &&
            a.Categoria.Contains(TxtBuscador.Text) ||
            (a.Descripcion != null && a.Descripcion.Contains(TxtBuscador.Text)) ||
            (a.CodigoReferencia != null && a.CodigoReferencia.Contains(TxtBuscador.Text)) ||
            (a.Ubicacion != null && a.Ubicacion.Contains(TxtBuscador.Text)) ||
            (a.Notas != null && a.Notas.Contains(TxtBuscador.Text)));
            listproductos.DataSource = filter.ToList();
        }        
    }
}
