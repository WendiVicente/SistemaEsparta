using CapaDatos.Repository;
using CapaDatos.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDatos.ListasPersonalizadas;
using ComponentFactory.Krypton.Toolkit;
using CapaDatos.Models.Traslados;
using CapaDatos.Validation;

namespace Sistema.Forms.modulo_traslados
{
    public partial class VistaPrincipalTraslado : BaseContext
    {
        private ProductosRepository _productosRepository = null;
        private TallasRepository _tallasRepository = null;
        private ColoresRepository _coloresRepository = null;
        private TallasyColoresRepository _tallasyColoresRepository = null;
        private TrasladosRepository _trasladosRepository = null;
        private SucursalesRepository _sucursalesRepository = null;

        private IList<ListarProductos> listaProductos = null;
        public VistaPrincipalTraslado()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            _sucursalesRepository = new SucursalesRepository(_context);
            _productosRepository = new ProductosRepository(_context);
            _tallasRepository = new TallasRepository(_context);
            _coloresRepository = new ColoresRepository(_context);
            _tallasyColoresRepository = new TallasyColoresRepository(_context);
            _trasladosRepository = new TrasladosRepository(_context);
            listaProductos = new List<ListarProductos>();
        }

        private void VistaPrincipalTraslado_Load(object sender, EventArgs e)
        {
           // ListaFilasenDGV = new List<Producto>();
           
            
            CargarSucursales();
            RefrescarDataGridProductos();
            lbnoVale.Text = Guid.NewGuid().ToString();
            lbcorrelativo.Text = "TR-2211232234";
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
            listaProductos = _productosRepository.GetList(UsuarioLogeadoSistemas.User.SucursalId);
            source.DataSource = listaProductos;
            dgvProductos.DataSource = typeof(List<>);
            dgvProductos.DataSource = source;

            for (int i = 0; i < 31; i++)
            {
                dgvProductos.Columns[i].Visible = false;
            }
            dgvProductos.Columns[1].Visible = true;
            dgvProductos.Columns[3].Visible = true;
           // dgvProductos.Columns[27].Visible = true;

         //   dgvProductos.AutoResizeColumns();
        }
        private void CargarSucursales()
        {
            var sucursal = _sucursalesRepository.GetList();

            // agregar nuevo item a la lista
            sucursal.Add(new ListarSucursales { Id = 0, NombreSucursal = "Todas" });
            var s = sucursal.OrderBy(a => a.Id).ToList();

            // mostrar datos en dgv
            cbsucursales.DataSource = s;
            cbsucursales.DisplayMember = "NombreSucursal";
            cbsucursales.ValueMember = "Id";
            //cbsucursales.Text = "Seleccione una Sucursal"; // 
            cbsucursales.SelectedIndex = 0;
            cbsucursales.Invalidate();
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if(dgvProductos.CurrentRow.DataBoundItem!= null)
            {
                var itemselected = (ListarProductos)dgvProductos.CurrentRow.DataBoundItem;
                var lista = _productosRepository.GetlistaAllSucursales(Convert.ToInt32(this.cbsucursales.SelectedValue.ToString()),itemselected.CodigoReferencia);
                CargarDGVProductosEncontrador(lista);
            }
            else
            {
                KryptonMessageBox.Show("Debe Selecionar un producto");
            }
          
        }
        private void CargarDGVProductosEncontrador(IList<ListarProductos>listaEnviada)
        {
            BindingSource source = new BindingSource();
           
            source.DataSource = listaEnviada;
            dgvproductosEncontrados.DataSource = typeof(List<>);
            dgvproductosEncontrados.DataSource = source;

        }
        private void CargarDGVProductosToRequerir(IList<DetalleTraslados> listaEnviada2)
        {
            BindingSource source = new BindingSource();

            source.DataSource = listaEnviada2;
            dgvListaAPedir.DataSource = typeof(List<>);
            dgvListaAPedir.DataSource = source;

        }

        private void txtbuscarp_TextChanged(object sender, EventArgs e)
        {
            var filter = listaProductos.Where(a => a.Descripcion.Contains(txtbuscarp.Text) ||
              (a.CodigoReferencia != null && a.CodigoReferencia.Contains(txtbuscarp.Text)) ||
              (a.Categoria != null && a.Categoria.Contains(txtbuscarp.Text)) ||
              (a.Notas != null && a.Notas.Contains(txtbuscarp.Text)));
            dgvProductos.DataSource = filter.ToList();
        }


        private void btnAgregartoProducto_Click(object sender, EventArgs e)
        {
            try
            {
                var productoToPedir = (ListarProductos)dgvproductosEncontrados.CurrentRow.DataBoundItem;
                convertirATraslado(productoToPedir);
            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show(ex.Message);
            }
           
        }

        private List<DetalleTraslados> listaTraslados = new List<DetalleTraslados>(); 
        private void convertirATraslado(ListarProductos productoEnviado)
        {
            try
            {
                DetalleTraslados sTraslado = new DetalleTraslados();
                sTraslado.Cantidad = 1;
                sTraslado.ProductoId = productoEnviado.Id;
                sTraslado.TrasladosId = Guid.Parse(lbnoVale.Text);
                //sTraslado.Color= productoEnviado.IncluyeColor == "Si"?//_coloresRepository.GetColor()
                //sTraslado.de = productoEnviado.Descripcion;
                //sTraslado.DetalleColorId= productoEnviado.
                listaTraslados.Add(sTraslado);
                CargarDGVProductosToRequerir(listaTraslados);

            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show(ex.Message);
            }
           
        }

        private Traslados createModelo()
        {
            var nuevotraslado = new Traslados()
            {
                Id = Guid.Parse(lbnoVale.Text),// Guid.NewGuid(),
                FechaRecepcion = DateTime.Now,
                Estado = false,
                IdSucursalEmisor = UsuarioLogeadoSistemas.User.SucursalId,
                VendedorSolicitante = UsuarioLogeadoSistemas.User.Id,
                IdSucursalReceptor = Convert.ToInt32(cbsucursales.SelectedValue.ToString()),
                NumeroTransaccion = Convert.ToInt32(lbcorrelativo.Text),
            };
            return nuevotraslado;



        }
        
        private List<DetalleTraslados> listaDetalle()
        {
            var List = new List<DetalleTraslados>();

            foreach (DataGridViewRow row in dgvListaAPedir.Rows)
            {
                if (row == null)
                {
                    continue;
                }

                //DetalleTraslados detallePedido = new DetalleTraslados
                //{
                //    Id = Guid.NewGuid(),
                //    ProductoId = int.Parse(item.Cells[productidColcotiz].Value.ToString()),
                //    Cantidad = int.Parse(item.Cells[cantidadcolcotiz].Value.ToString()),
                //    Precio = decimal.Parse(item.Cells[preciocolcotiz].Value.ToString()),
                //    PrecioTotal = decimal.Parse(item.Cells[subtotalcolcotiz].Value.ToString()),
                //    PedidoId = Guid.Parse(lbnoVale.Text),
                //    ComboId = Guid.Parse(item.Cells[combocolcotiz].Value.ToString()),
                //    DetalleColorId = int.Parse(item.Cells[coloridColcotiz].Value.ToString()),
                //    DetalleTallaId = int.Parse(item.Cells[tallaidColcotiz].Value.ToString()),
                //    DetalleColorTallaId = int.Parse(item.Cells[tallaColorIdCotiz].Value.ToString()),

                //};

                var filaActual = (DetalleTraslados)row.DataBoundItem;
                filaActual.Id = Guid.NewGuid();
                

                List.Add(filaActual);
            }

            return List;
        }
       


        private void btnSolicitarProductos_Click(object sender, EventArgs e)
        {
            if (dgvListaAPedir.RowCount == 0) { KryptonMessageBox.Show("no ha seleccionado ningun producto"); return; }
            try
            {
                if (!ModelState.IsValid(createModelo()))
                {
                    return;
                }
               if(_trasladosRepository.AddEncabezado(createModelo())=="OK")
                {
                    if (listaDetalle() != null)
                    {
                        foreach (var item in listaDetalle())
                        {
                            _trasladosRepository.AddDetalles(item);

                        }
                    }
                    MessageBox.Show("Registro guardado correctamente");
                    Close();
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void cargarForms()
        {
            
        }
        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var  productoselected= (ListarProductos)dgvProductos.CurrentRow.DataBoundItem;
            int opc = 0;

            if (productoselected.IncluyeColor == "Sí" && productoselected.Talla=="No") 
            { opc = 1;
                CargarFm(opc, productoselected);
            }
            else if(productoselected.IncluyeColor == "No" && productoselected.Talla == "Sí")
            {
                opc = 2;
                CargarFm(opc, productoselected);
            }
            else if (productoselected.IncluyeColor == "Si" && productoselected.Talla == "Sí")
            {
                opc = 3;
                CargarFm(opc, productoselected);
            }


           
        }


        private void CargarFm(int opc, ListarProductos productoselected)
        {
            if (Application.OpenForms["SeleccionarElemento"] == null)
            {
                SeleccionarElemento sel = new SeleccionarElemento(opc, this, productoselected);
                sel.Show();
            }
            else
            {
                Application.OpenForms["SeleccionarElemento"].Activate();
            }
        }

        private List<DetalleTraslados> listaDetalleTraslados = new List<DetalleTraslados>();
        public void AsignarValoresDetalle(int opcion,int cantidad, int colorId,int tallaId,int tomoId,int edicionId, int productoid)
        {
            var adddetalleTraslados = new DetalleTraslados()
            {
                Cantidad = cantidad,
                DetalleColorId = colorId,
                DetalleColorTallaId = tallaId,
                DetalleTomoId = tomoId,
                DetalleEdicionId = edicionId,
                TrasladosId = Guid.Parse(lbnoVale.Text),
                Id = Guid.NewGuid(),
                ProductoId = productoid,

            };

            listaDetalleTraslados.Add(adddetalleTraslados);

            CargarDGVProductosToRequerir(listaDetalleTraslados);


        }

        private void dgvproductosEncontrados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var productoselected = (ListarProductos)dgvProductos.CurrentRow.DataBoundItem;
            int opc = 0;

            if (productoselected.IncluyeColor == "Sí" && productoselected.Talla == "No")
            {
                opc = 1;
                CargarFm(opc, productoselected);
            }
            else if (productoselected.IncluyeColor == "No" && productoselected.Talla == "Sí")
            {
                opc = 2;
                CargarFm(opc, productoselected);
            }
            else if (productoselected.IncluyeColor == "Si" && productoselected.Talla == "Sí")
            {
                opc = 3;
                CargarFm(opc, productoselected);
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
           // int correlativo = CrearCorrelativo();
            
        }

        private int CrearCorrelativo()
        {
            //string tipo = "TL-";
            int numero;
            do
            {
                numero = GenerateRandom();
            }

            while (ExisteCuenta(numero));

            return numero;
        }

        public int GenerateRandom()
        {
            Random randomGenerate = new System.Random();
            int correlativo = randomGenerate.Next(0000001, 9999999);
            return correlativo;
        }

        private bool ExisteCuenta(int correlativo)
        {
            var cuenta = _trasladosRepository.GetbyNoTransac(correlativo);
            if (cuenta == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
