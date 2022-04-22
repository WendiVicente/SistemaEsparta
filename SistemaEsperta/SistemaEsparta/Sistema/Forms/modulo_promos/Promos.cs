using CapaDatos.Data;
using CapaDatos.ListasPersonalizadas;
using CapaDatos.Models.Precios;
using CapaDatos.Models.Productos.Promocion;
using CapaDatos.Models.Sucursales;
using CapaDatos.Repository;
using CapaDatos.Repository.PreciosRepository;
using CapaDatos.Validation;
using ComponentFactory.Krypton.Toolkit;
using sharedDatabase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Forms.modulo_promos
{
    public partial class Promos : BaseContext
    {
        private SucursalesRepository _sucursalesRepository = null;
        private ProductosRepository _productosRepository = null;
        private PromocionRepository _promocionRepository = null;
        private CombosRepository _combosRepository = null;
        private IList<ListarProductos> listaProductos = null;
        private List<Producto> ListaFilasenDGV = null;
        private List<DetallePromocion> DetallePromocionsFilas = null;
        private List<ListarDetallePromocion> _secondListaDetallePromos = null;
        private TipoPrecioRepository _tipoPrecioRepository = null;
        public List<ListarSucursales> SucursalesSeleccionadas = null;
        private IList<ListarCombos> combos = null;
        private int codDetalle = 0;
        private int comboidcol = 1;
      
        private readonly int colAccionesProd = 0;
        private readonly int colAccionesComb = 0;
        public Promos()
        {
            _productosRepository = new ProductosRepository(_context);
            _sucursalesRepository = new SucursalesRepository(_context);
            _promocionRepository = new PromocionRepository(_context);
            _combosRepository = new CombosRepository(_context);
            listaProductos = new List<ListarProductos>();
            _tipoPrecioRepository = new TipoPrecioRepository(_context);
            _secondListaDetallePromos = new List<ListarDetallePromocion>();
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        private void Promos_Load(object sender, EventArgs e)
        {
           
            listaProductos = _productosRepository.GetList(UsuarioLogeadoSistemas.User.SucursalId);
            RefrescarDataGridProductos();
            DetallePromocionsFilas = new List<DetallePromocion>();
            ListaFilasenDGV = new List<Producto>();
            cargarDescuentos();
            cargarsucursales();
            CargarHora();
            CargarDgvCombos();
            CargarProductos();
        }
        private void CargarProductos()
        {
            BindingSource source = new BindingSource();
            listaProductos = _productosRepository.GetList(UsuarioLogeadoSistemas.User.SucursalId).ToList();
            source.DataSource = listaProductos;
            DgvProductos.DataSource = typeof(List<>);
            DgvProductos.DataSource = source;
            DgvProductos.ClearSelection();
        }
        public void RefrescarDataGridProductos(bool loadNewContext = true)
        {
            //BindingSource source = new BindingSource();
            //source.DataSource = listaProductos;
            //DgvProductos.DataSource = typeof(List<>);
            //DgvProductos.DataSource = source;
            //DgvProductos.ClearSelection();
        }

        public void CargarDgvCombos(bool loadNewContext = true)
        {
            if (loadNewContext)
            {
                _context = null;
                _context = new Context();
                _combosRepository = null;
                _combosRepository = new CombosRepository(_context);
            }

            BindingSource source = new BindingSource();
            combos = _combosRepository.GetListarCombos(UsuarioLogeadoSistemas.User.SucursalId);
            source.DataSource = combos;
            dgvCombos.DataSource = typeof(List<>);

            dgvCombos.DataSource = source;
            dgvCombos.AutoResizeColumns();
            for (int i = 0; i < 15; i++)
            {
                dgvCombos.Columns[i].Visible = false;
            }
            dgvCombos.Columns[2].Visible = true;
            dgvCombos.Columns[3].Visible = true;
            dgvCombos.Columns[4].Visible = true;
            dgvCombos.Columns[6].Visible = true;
            dgvCombos.Columns[colAccionesProd].Visible = true;

        }

        public void CargarDg(Guid idcombo)
        {


            BindingSource source = new BindingSource();
           var  detalleListsObtenida = _combosRepository.GetListDetalleCombo(idcombo);
            source.DataSource = detalleListsObtenida;
            dgvDetalleCombo.DataSource = typeof(List<>);
            dgvDetalleCombo.DataSource = source;
           
            dgvDetalleCombo.Columns[codDetalle].Visible = false;
            dgvDetalleCombo.Columns[comboidcol].Visible = false;
            dgvDetalleCombo.Columns[2].Visible = false;
            dgvDetalleCombo.Columns[3].Visible = false;
            dgvDetalleCombo.Columns[6].Visible = false;
            dgvDetalleCombo.Columns[8].Visible = false;
            dgvDetalleCombo.AutoResizeColumns();


        }
        private void SeleccionarFila(DataGridView datagrid, int acciones)
        {
            bool estadoActual = Convert.ToBoolean(datagrid.CurrentRow.Cells[acciones].Value);
            if (estadoActual)
            {
                datagrid.CurrentRow.Cells[acciones].Value = false;
            }
            else
            {
                datagrid.CurrentRow.Cells[acciones].Value = true;

            }

        }


        private void txtbuscarprod_TextChanged(object sender, EventArgs e)
        {
          
                var filter = listaProductos.Where(a => a.Descripcion.Contains(txtbuscarprod.Text) ||
                (a.CodigoReferencia != null && a.CodigoReferencia.Contains(txtbuscarprod.Text)) ||
                (a.Categoria != null && a.Categoria.Contains(txtbuscarprod.Text)) ||
                (a.Notas != null && a.Notas.Contains(txtbuscarprod.Text)));
                DgvProductos.DataSource = filter.ToList();
            
        }
        private void SeleccionAcciones(DataGridView datatgrid, List<Producto> Productolista)
        {


            if (datatgrid.RowCount <= 0) { return; }
            int filasSeleccion = 0;
            foreach (DataGridViewRow Rows in datatgrid.Rows)
            {
                var filasTotales = int.Parse(datatgrid.RowCount.ToString());


                bool acciones = Convert.ToBoolean(Rows.Cells[colAccionesProd].Value);
                if (!acciones)
                {
                    filasSeleccion += 1;
                }
                else
                {
                    var Id = int.Parse(Rows.Cells[0].Value.ToString());
                    var ProductoObtenido = _productosRepository.Get(Id);

                    Productolista.Add(ProductoObtenido);
                }


                if (filasTotales == filasSeleccion)
                {
                    KryptonMessageBox.Show("Debera tener seleccionada  la columna 'Acciones'\n "
                        + "Selecione un Producto, dando click en la columna Acciones\n"
                        );

                    return;
                }

            }


        }

        private void guardarPromo(int idsucursal)
        {
           
                var encabezadoPromo = GetModelPromocion();
            var detallePromo = GetDetallePromo();
            if (!ModelState.IsValid(detallePromo)) return;
            if (!ModelState.IsValid(encabezadoPromo)) return;
            encabezadoPromo.Descripcion = txtdescripromos.Text;
                 encabezadoPromo.SucursalId = idsucursal;
           
                _promocionRepository.AddPromocion(encabezadoPromo);
            foreach (var item in _secondListaDetallePromos)
            {
                if (item.ProductoId != 0)
                {
                    var detalle = new DetallePromocion()
                    {
                        Id = Guid.NewGuid(),
                        ProductoId = item.ProductoId,
                        PromocionId = encabezadoPromo.Id,
                        DescuentoPromosId = Guid.Parse(comboDescuento.SelectedValue.ToString()),
                        // ComboId = item.ComboId,
                    };
                    _promocionRepository.AddDetallePromocion(detalle);

                }
                else
                {
                    var detalle = new DetallePromocion()
                    {
                        Id = Guid.NewGuid(),
                        // ProductoId = item.ProductoId,
                        PromocionId = encabezadoPromo.Id,
                        DescuentoPromosId = Guid.Parse(comboDescuento.SelectedValue.ToString()),
                        ComboId = item.ComboId, //analizar
                    };
                    _promocionRepository.AddDetallePromocion(detalle);
                }

            }
            //  KryptonMessageBox.Show("Registro guardado correctamente!");
            // Close();
        }


        private List<ListarProductos> SeleccionAcciones()
        {
            List<ListarProductos> listadoseleccion = new List<ListarProductos>();
            foreach (DataGridViewRow Rows in DgvProductos.Rows)
            {
                ListarProductos product = (ListarProductos)Rows.DataBoundItem;
                if (product.Acciones)
                {
                    listadoseleccion.Add(product);
                }
            }
            return listadoseleccion;
        }
        private List<ListarCombos> SeleccionAccionesCombo()
        {
            List<ListarCombos> listadoseleccion = new List<ListarCombos>();
            foreach (DataGridViewRow Rows in dgvCombos.Rows)
            {
                ListarCombos product = (ListarCombos)Rows.DataBoundItem;
                if (product.Acciones)
                {
                    listadoseleccion.Add(product);
                }
            }
            return listadoseleccion;
        }
        private void CargarDGVPromos(List<ListarDetallePromocion> lista)
        {
            BindingSource recurso = new BindingSource();
            recurso.DataSource = lista;
            Dgvlistapromos.DataSource = typeof(List<>);
            Dgvlistapromos.DataSource = recurso;
            Dgvlistapromos.ClearSelection();
        }
        private void AgregarListarDetallesPedidos(List<ListarProductos> listadoseleccion)
        {
            foreach (var item in listadoseleccion)
            {
                ListarDetallePromocion detallepromocion = new ListarDetallePromocion
                {
                    ProductoId = item.Id,
                    Referencia=item.CodigoReferencia,
                    Descripcion = item.Descripcion,
                    Descuento= comboDescuento.Text != "" ? decimal.Parse(comboDescuento.Text) : 0.00m,

            };
                var exist =_secondListaDetallePromos.Find(x => x.ProductoId == detallepromocion.ProductoId);
                if (exist == null)
                    _secondListaDetallePromos.Add(detallepromocion);
            }
            CargarDGVPromos(_secondListaDetallePromos);

        }
        private void AgregarListarDetallesCombos(List<ListarCombos> listadoseleccion)
        {
            foreach (var item in listadoseleccion)
            {
                ListarDetallePromocion detallepromocion = new ListarDetallePromocion
                {
                    ComboId = item.IdCombo,
                    Referencia = item.CodigoBarras,
                    Descripcion = item.Descripcion,
                    Descuento = comboDescuento.Text != "" ? decimal.Parse(comboDescuento.Text) : 0.00m,

                };
                var exist = _secondListaDetallePromos.Find(x => x.ComboId == detallepromocion.ComboId);
                if (exist == null)
                    _secondListaDetallePromos.Add(detallepromocion);
            }
            CargarDGVPromos(_secondListaDetallePromos);

        }
        private void LimpiarSeleccionPromoProducto()
        {
            foreach (ListarProductos prod in listaProductos)
            {
                prod.Acciones = false;
            }
            BindingSource source = new BindingSource
            {
                DataSource = listaProductos
            };
            DgvProductos.DataSource = typeof(List<>);
            DgvProductos.DataSource = source;
            DgvProductos.ClearSelection();


        }
        private void LimpiarSeleccionPromoCombo()
        {
            foreach (ListarCombos prod in combos)
            {
                prod.Acciones = false;
            }
            BindingSource source = new BindingSource
            {
                DataSource = combos
            };
            dgvCombos.DataSource = typeof(List<>);
            dgvCombos.DataSource = source;
            dgvCombos.ClearSelection();


        }
        private void btnaddtopromo_Click(object sender, EventArgs e)
        {

            //SeleccionAcciones(DgvProductos, ListaFilasenDGV);
            //AgregarlistaToDgv();
            //AgregarListarDetalles();
            //ListaFilasenDGV.Clear();
            //limpiarSeleccion();
            List<ListarProductos> seleccionados = SeleccionAcciones();
            if (seleccionados.Count > 0)
            {
                AgregarListarDetallesPedidos(seleccionados);
                LimpiarSeleccionPromoProducto();
                AgregarlistaToDgv();
            }
            else
            {
                KryptonMessageBox.Show("No hay productos seleccionados", "Advertencia");
            }
        }

        private void btnadddescuento_Click(object sender, EventArgs e)
        {
            guardarDescuento();
            txtnuevodescuento.Text = "";
            cargarDescuentos();


        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtdescripromos.Text)) { KryptonMessageBox.Show("Debe de ingresar el nombre de la promoción");return; }
            if (Dgvlistapromos.RowCount == 0)
            {
                KryptonMessageBox.Show("Debe de ingresar Productos a la promoción");
                return;
            }
            if (checkSucursales.Checked == true && SucursalesSeleccionadas.Count > 0)
            {
                foreach (var item in SucursalesSeleccionadas)
                {
                    guardarPromo(item.Id);
                }
                KryptonMessageBox.Show("Registro guardado correctamente!");
                 Close();

            }
            else { guardarPromo(int.Parse(comboSucursales.SelectedValue.ToString()));
                KryptonMessageBox.Show("Registro guardado correctamente!");
                Close();
            }
          
        }


        private void CargarHora()
        {
            string[] Horas = {
                "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00",
                "08:00" ,"09:00","10:00","11:00","12:00",
                "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00",
                "20:00" ,"21:00","22:00","23:00","24:00",
            };
           var listaHoras = new List<string>(Horas);
            foreach (var item in listaHoras)
            {
                combohorafin.Items.Add(item);
                combohorainicio.Items.Add(item);
            }
            combohorafin.SelectedIndex = 0;
            combohorainicio.SelectedIndex = 0;
           

        }


        private void AgregarlistaToDgv()
        {

            foreach (var item in ListaFilasenDGV)
            {
                var detallePromos = GetDetallePromo();
                detallePromos.ProductoId = item.Id;
                if (comboDescuento.Items.Count > 0) 
                {
                    detallePromos.DescuentoPromosId = Guid.Parse(comboDescuento.SelectedValue.ToString());
                }
                
                DetallePromocionsFilas.Add(detallePromos);
            }

           // cargarDGVPromos(DetallePromocionsFilas);

        }
        private void AgregarListarDetalles()
        {

            foreach (var item in ListaFilasenDGV)
            {
                var detallePromos = GetListarPromosDetail();
                detallePromos.Referencia = item.CodigoBarras;
                detallePromos.ProductoId = item.Id;
                detallePromos.Descripcion = item.Descripcion;
                detallePromos.Promocion = txtdescripromos.Text;
                detallePromos.Descuento = comboDescuento.Text != "" ? decimal.Parse(comboDescuento.Text) : 0.00m;
                if (comboDescuento.Items.Count > 0)
                    detallePromos.DescuentoPromosId= Guid.Parse(comboDescuento.SelectedValue.ToString());

                 _secondListaDetallePromos.Add(detallePromos);
              

                
            }

            cargarDGVPromos(_secondListaDetallePromos);

        }

        public bool comprobarProducto(DataGridView dataGrid, ListarDetallePromocion detalles)
        {
            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                
                if (row.Cells[1].Value.ToString() == detalles.ProductoId.ToString())
                {
                    return true;
                }
            }

            return false;
        }

        private void limpiarSeleccion()
        {

            foreach (DataGridViewRow Rows in DgvProductos.Rows)
            {
                bool acciones = Convert.ToBoolean(Rows.Cells[colAccionesProd].Value);
                if (acciones)
                {
                    Rows.Cells[colAccionesProd].Value = false;
                }
            }
        }

       
        private DetallePromocion  GetDetallePromo()
        {
            return new DetallePromocion()
            {
            };
        }
        private ListarDetallePromocion GetListarPromosDetail()
        {
            return new ListarDetallePromocion()
            {

            };
        }

        private DescuentoPromos GetDescuentoPromos()
        {
            return new DescuentoPromos()
            {
                Id = Guid.NewGuid(),
                Descuento = int.Parse(txtnuevodescuento.Text),
            };
        }



        private Promocion GetModelPromocion()
        {
            return new Promocion()
            {
                Id = Guid.NewGuid(),
                SucursalId =int.Parse(comboSucursales.SelectedValue.ToString()),
                Descripcion = txtdescripromos.Text,
                FechaInicio = dtpinicio.Value,
                FechaFin=  dtpfin.Value,
                HoraInicio= combohorainicio.Text,
                HoraFin= combohorafin.Text,

            };
        }
        
      




        private void guardarDescuento()
        {
            if (string.IsNullOrEmpty(txtnuevodescuento.Text)) { return; }
            var descuentonuevo = GetDescuentoPromos();
            if (!ModelState.IsValid(descuentonuevo)) { return; }
            _tipoPrecioRepository.AddDescuento(descuentonuevo);


        }



        private void cargarDescuentos()
        {
            comboDescuento.DataSource = _tipoPrecioRepository.GetDescuentopromosAll();
            comboDescuento.ValueMember = "Id";
            comboDescuento.DisplayMember = "Descuento";
            //comboDescuento.SelectedIndex = 0;


        }
        private void cargarsucursales()
        {
            comboSucursales.DataSource = _sucursalesRepository.GetList();
            comboSucursales.ValueMember = "Id";
            comboSucursales.DisplayMember = "NombreSucursal";
            //comboSucursales.SelectedIndex = 0;
        }




        private void cargarDGVPromos(List<ListarDetallePromocion>lista)
        {
            BindingSource recurso = new BindingSource();
            recurso.DataSource = lista;
            Dgvlistapromos.DataSource = typeof(List<>);
            Dgvlistapromos.DataSource = recurso;
            Dgvlistapromos.Columns[0].Visible = false;
            Dgvlistapromos.Columns[5].Visible = false;
            Dgvlistapromos.Columns[6].Visible = false;
            Dgvlistapromos.Columns[7].Visible = false;
            Dgvlistapromos.Columns[8].Visible = false;
            Dgvlistapromos.Columns[9].Visible = false;




        }

        private void btnseleccionall_Click(object sender, EventArgs e)
        {
           
        }

        private void checkSucursales_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSucursales.Checked == true)
            {
                if (Application.OpenForms["AgregarMasSucursales"] == null)
                {


                    AgregarMasSucursales elegirMasTipos = new AgregarMasSucursales(this);
                    elegirMasTipos.Show();


                }

                else { Application.OpenForms["AgregarMasSucursales"].Activate(); }

            }
            else
            {
                SucursalesSeleccionadas = null;
            }
        }

        private void dgvCombos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //var comboActual = (ListarCombos)dgvCombos.CurrentRow.DataBoundItem;
            //    CargarDg(comboActual.IdCombo);
            //SeleccionarFila(dgvCombos, colAccionesProd);
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            //var colacciones = 1;
            //SeleccionarFila(DgvProductos, colacciones);
        }

        private void CargarComboToPromos()
        {
           
            if (dgvCombos.RowCount <= 0) { return; }
            int filasSeleccion = 0;

            var listaDetallePromo = new List<ListarDetallePromocion>();

            foreach (DataGridViewRow Rows in dgvCombos.Rows)
            {
                var filasTotales = int.Parse(dgvCombos.RowCount.ToString());


                bool acciones = Convert.ToBoolean(Rows.Cells[colAccionesProd].Value);
                if (!acciones)
                {
                    filasSeleccion += 1;
                }
                else
                {
                        var detallePromos = GetListarPromosDetail();
                        detallePromos.ComboId = Guid.Parse(Rows.Cells[1].Value.ToString());
                        detallePromos.Descripcion = Rows.Cells[3].Value.ToString();
                        detallePromos.Promocion = txtdescripromos.Text;
                        detallePromos.Descuento = decimal.Parse(comboDescuento.Text);
                        detallePromos.DescuentoPromosId = Guid.Parse(comboDescuento.SelectedValue.ToString());

                    _secondListaDetallePromos.Add(detallePromos);
                    
                }


                if (filasTotales == filasSeleccion)
                {
                    KryptonMessageBox.Show("Debera tener seleccionada  la columna 'Acciones'\n "
                        + "Selecione un Producto, dando click en la columna Acciones\n"
                        );

                    return;
                }

            }
            cargarDGVPromos(_secondListaDetallePromos);

        }

        private void btnaddcombos_Click(object sender, EventArgs e)
        {
            List<ListarCombos> seleccionados = SeleccionAccionesCombo();
            if (seleccionados.Count > 0)
            {
                AgregarListarDetallesCombos(seleccionados);
                LimpiarSeleccionPromoCombo();
                AgregarlistaToDgv();
            }
            else
            {
                KryptonMessageBox.Show("No hay productos seleccionados", "Advertencia");
            }
            //CargarComboToPromos();
            //limpiarSeleccion();


        }

        private void checktodas_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Rows in DgvProductos.Rows)
            {
                bool acciones = Convert.ToBoolean(Rows.Cells[colAccionesProd].Value);
                if (!acciones)
                {
                    Rows.Cells[colAccionesProd].Value = true;
                }
                else
                {
                    Rows.Cells[colAccionesProd].Value = false;
                }
            }
        }

        private void checktodascombos_CheckedChanged(object sender, EventArgs e)
        {
          
            foreach (DataGridViewRow Rows in dgvCombos.Rows)
            {
                bool acciones = Convert.ToBoolean(Rows.Cells[colAccionesProd].Value);
                if (!acciones)
                {
                    Rows.Cells[colAccionesProd].Value = true;
                }
                else
                {
                    Rows.Cells[colAccionesProd].Value = false;
                }
            }
        }

        private void txtbuscarcombo_TextChanged(object sender, EventArgs e)
        {

            var filter = combos.Where(a => a.Descripcion.Contains(txtbuscarcombo.Text) ||
               (a.CodigoBarras != null && a.CodigoBarras.Contains(txtbuscarcombo.Text)) 
               );
            dgvCombos.DataSource = filter.ToList();

        }

        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = DgvProductos.CurrentRow.Index;
            if (DgvProductos.CurrentCell.ColumnIndex == colAccionesProd)
            {
                var fila = DgvProductos.CurrentRow;
                ListarProductos listarProductos = (ListarProductos)fila.DataBoundItem;
                if (listarProductos.Acciones)
                {
                    DgvProductos.CurrentRow.Cells[colAccionesProd].Value = false;
                }
                else
                {
                    DgvProductos.CurrentRow.Cells[colAccionesProd].Value = true;
                }                           
            }
        }

        private void dgvCombos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvCombos.CurrentRow.Index;
            if (dgvCombos.CurrentCell.ColumnIndex == colAccionesComb)
            {
                var fila = dgvCombos.CurrentRow;
                ListarCombos listarCombo = (ListarCombos)fila.DataBoundItem;
                if (listarCombo.Acciones)
                {
                    dgvCombos.CurrentRow.Cells[colAccionesComb].Value = false;
                }
                else
                {
                    dgvCombos.CurrentRow.Cells[colAccionesComb].Value = true;
                }


            }
        }
    }
}
