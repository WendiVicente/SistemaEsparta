using CapaDatos.Data;
using CapaDatos.ListasPersonalizadas;
using CapaDatos.Models.Productos;
using CapaDatos.Models.Productos.Combos;
using CapaDatos.Repository;
using CapaDatos.Validation;
using ComponentFactory.Krypton.Toolkit;
using POS.Validations;
using sharedDatabase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Forms.modulo_combos
{
    public partial class NuevoCombo : BaseContext
    {
        private CombosRepository _combosRepository = null;
        private int sucursalid = UsuarioLogeadoSistemas.User.SucursalId;
        private ProductosRepository _productosRepository = null;
        private ColoresRepository _coloresRepository = null;
        private TallasRepository _tallasRepository = null;
        private TallasyColoresRepository _tallasyColoresRepository = null;
       
        private List<ListarProductos> listarProductosTodos = null;
        private List<ComboDetalleLista> listaDetalles = null;
        private List<Producto> listaproductostoadd = null;
        //private List<ListarProductos> productoRow = null;
       //columnas del detall de combos para enviar ala bd
        private int idproducto = 2;
        private int referenciacol = 3;
        private int descripcol = 4;
        private int cantidadcol = 5;
        private int colorIdcol = 6;
        private int tallaIdcol = 8;
        private int colidtyc = 10;
        //columnas de dgv colores y tallas
        int IDTyCcol = 0;
        int productIDTyCcol = 1;
        
        int colorTyCcol = 3;
        int tallaTyCcol = 3;
        int colAccionesTyC = 5;
        byte[] filefoto = null;

        private readonly Layout formslayout = null;
        public NuevoCombo(Layout forms)
        {

            _productosRepository = new ProductosRepository(_context);
            _combosRepository = new CombosRepository(_context);
            _coloresRepository = new ColoresRepository(_context);
            _tallasRepository = new TallasRepository(_context);
            _tallasyColoresRepository = new TallasyColoresRepository(_context);
            listaDetalles = new List<ComboDetalleLista>();
            listaproductostoadd = new List<Producto>();
            formslayout = forms;
            InitializeComponent();
            TraerListabd();
        }
        
        public ProductosRepository AccesoProductoRepository()
        {
            _context = null;
            _context = new Context();
            _productosRepository = null;
            _productosRepository = new ProductosRepository(_context);

            return _productosRepository;
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ListaProductSelect.Rows.Count == 0)
            {
                KryptonMessageBox.Show("Debe ingresar productos");
                return;
            }

            
           GuardarCombo();
            RecargarNuevoCombo();
            Close();
        }

       
        private void NuevoCombo_Load(object sender, EventArgs e)
        {
            ocultarcombos();
            cargardatos();
        }

        private void LimpiarCampos()
        {
            txtcosto.Text = "0.00";
            txtdescripcion.Text = "";
            txtpreciocuentaclave.Text = "0.00";
            txtprecioentidad.Text = "0.00";
            txtpreciomayorista.Text = "0.00";
            txtprecioventa.Text = "0.00";
            txtrevendedor.Text = "0.00";
            txtstock.Text = "0";
            txtcodibarras.Text = "";
            
            ListaProductSelect.DataSource = null;
        }
        private void cargardatos()
        {
            txtcosto.Text = "0.00";
            txtdescripcion.Text = "";
            txtpreciocuentaclave.Text = "0.00";
            txtprecioentidad.Text = "0.00";
            txtpreciomayorista.Text = "0.00";
            txtprecioventa.Text = "0.00";
            txtrevendedor.Text = "0.00";
            txtstock.Text = "0";
            txtcodibarras.Text = "";
        }

        public Combo GetCombo()
        {
            return new Combo()
            {
                Id = Guid.NewGuid(),
                SucursalId = sucursalid,
                Descripcion = txtdescripcion.Text,
                FechaInicio = DateTime.Now,
                Precioventa = decimal.Parse(txtprecioventa.Text),
                PrecioMayorista = decimal.Parse(txtpreciomayorista.Text),
                PrecioCuentaClave = decimal.Parse(txtpreciocuentaclave.Text),
                PrecioEntidadGubernamental = decimal.Parse(txtprecioentidad.Text),
                PrecioRevendedor = decimal.Parse(txtrevendedor.Text),
                PrecioCompra = decimal.Parse(txtcosto.Text),
                TieneColor = false,
                FechaMovimiento = DateTime.Now,
                CategoriaId = 1,
                Stock= int.Parse(txtstock.Text),
                CodigoBarras= txtcodibarras.Text,
                Imagen = filefoto,
            };
        }
        
        public List<ComboDetalleLista> GetDatosDetalleCombo()
        {
            var List = new List<ComboDetalleLista>();

            foreach (DataGridViewRow item in ListaProductSelect.Rows)
            {
                if (item == null)
                {
                    continue;
                }

                ComboDetalleLista comboDetalleU = new ComboDetalleLista
                {
                    ProductoId = int.Parse(item.Cells[idproducto].Value.ToString()),
                    Referencia = item.Cells[referenciacol].Value.ToString(),
                    Descripcion = item.Cells[descripcol].Value.ToString(),
                    Cantidad = int.Parse(item.Cells[cantidadcol].Value.ToString()),
                    ColorId= int.Parse(item.Cells[colorIdcol].Value.ToString()),
                    TallaId= int.Parse(item.Cells[tallaIdcol].Value.ToString()),
                    TallayColorId=int.Parse(item.Cells[colidtyc].Value.ToString()),
                };

                List.Add(comboDetalleU);
            }

            return List;

        }
        
        public ComboDetalleLista GetmodelDetalle()
        {
            return new ComboDetalleLista
            {
               

            };

        }

        
        private void GuardarCombo()
        {
          

            try
            {
                var encabezadoCombo = GetCombo(); // encabezado
                var detalleCombo = GetDatosDetalleCombo();// detalle de combo
               
                if (!ModelState.IsValid(encabezadoCombo)) return;
                if (!ModelState.IsValid(detalleCombo)) return;
              
                _combosRepository.Add(encabezadoCombo);
              
        
                foreach (var item in detalleCombo)
                {
                    var detalle = new DetalleCombo()
                    {
                        ComboId = encabezadoCombo.Id,
                        Referencia = item.ProductoId.ToString(),
                        Descripcion = item.Descripcion,
                        Cantidad = item.Cantidad,
                    };


                    if (item.TallayColorId!=0)
                    { detalle.DetalleColorTallaId = item.TallayColorId; }
                    if (item.TallaId != 0){detalle.DetalleTallaId = item.TallaId;}
                    if (item.ColorId != 0){ detalle.DetalleColorId = item.ColorId;}

                    _combosRepository.AddDetalle(detalle);
                    _context.SaveChanges();
                }


                KryptonMessageBox.Show("Combo Registrado con exito");
               
                LimpiarCampos();
                limpiarSeleccion(dgListaProductos,27);
                limpiarSeleccion(dgvtallascolores,5 );
                listaproductostoadd.Clear();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void limpiarSeleccion(DataGridView data, int colacc)
        {

            foreach (DataGridViewRow Rows in data.Rows)
            {
                bool acciones = Convert.ToBoolean(Rows.Cells[colacc].Value);
                if (acciones)
                {
                    Rows.Cells[colacc].Value = false;
                }
            }
        }

      
        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            //var ProductoBuscado = ListadoProductosC.ListaDeProductos;
            var ProductoBuscado = listarProductosTodos;
            var filter = ProductoBuscado.Where(a => a.Descripcion.Contains(txtbuscar.Text) ||
            (a.CodigoReferencia != null && a.CodigoReferencia.Contains(txtbuscar.Text)) ||
            (a.Categoria != null && a.Categoria.Contains(txtbuscar.Text)) ||
            (a.Notas != null && a.Notas.Contains(txtbuscar.Text)));
            dgListaProductos.DataSource = filter.ToList();
        }

        private void TraerListabd()
        {
            listarProductosTodos = (List<ListarProductos>)_productosRepository.GetList(UsuarioLogeadoSistemas.User.SucursalId);
            CargarDgv();
        }
        public void CargarDgv()
        {
            BindingSource source = new BindingSource();
            source.DataSource = listarProductosTodos;
            dgListaProductos.DataSource = typeof(List<>);
            dgListaProductos.DataSource = source;
            for (int i = 0; i < 28; i++)
            {
                dgListaProductos.Columns[i].Visible = false;
            }
            dgListaProductos.Columns[1].Visible = true;
            dgListaProductos.Columns[3].Visible = true;
            dgListaProductos.Columns[4].Visible = true;
            dgListaProductos.Columns[27].Visible = true;
            dgListaProductos.AutoResizeColumns();

        }
      

        private void SeleccionAcciones(DataGridView datatgrid, List<Producto> Productolista)
        {


            if (datatgrid.RowCount <= 0) { return; }
            int filasSeleccion = 0;
            foreach (DataGridViewRow Rows in datatgrid.Rows)
            {
                var filasTotales = int.Parse(datatgrid.RowCount.ToString());


                bool acciones = Convert.ToBoolean(Rows.Cells[27].Value);
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
        private void cargarDGVPromos(List<ComboDetalleLista> lista)
        {
            BindingSource recurso = new BindingSource();
            recurso.DataSource = lista;
            ListaProductSelect.DataSource = typeof(List<>);
            ListaProductSelect.DataSource = recurso;
           // ListaProductSelect.Columns[0].Visible = false;
           // ListaProductSelect.Columns[1].Visible = false;
           // ListaProductSelect.Columns[2].Visible = false;
           // ListaProductSelect.Columns[6].Visible = false;
           // ListaProductSelect.Columns[8].Visible = false;
        }





        private void AgregarListarDetalles()
        {

            foreach (var item in listaproductostoadd)
            {
                var detalleCombo = GetmodelDetalle();
                detalleCombo.ProductoId = item.Id;
                detalleCombo.Referencia = item.CodigoBarras;
                detalleCombo.Cantidad = int.Parse(txtcantidad.Text);
                detalleCombo.Descripcion = item.Descripcion;
                if (combocolor.Items.Count > 0)
                {
                    detalleCombo.ColorId = int.Parse(combocolor.SelectedValue.ToString());
                    detalleCombo.Color = combocolor.Text;
                }
                if (combotalla.Items.Count > 0)
                {
                    detalleCombo.TallaId = int.Parse(combotalla.SelectedValue.ToString());
                    detalleCombo.Talla = combotalla.Text;
                }


                //if (!comprobarProducto(ListaProductSelect,detalleCombo))
                //{
                listaDetalles.Add(detalleCombo);
                //}
                //else
                //{
                //    KryptonMessageBox.Show("El producto: " + detalleCombo.Descripcion + " \n ya se encuentra añadido");
                //}

            }

            cargarDGVPromos(listaDetalles);

        }

        public bool comprobarProducto(DataGridView dataGrid, ComboDetalleLista detalles)
        {
            foreach (DataGridViewRow row in dataGrid.Rows)
            {/*
                
                if (row.Cells[referenciacol].Value.ToString() == detalles.Referencia.ToString())
                {
                    return true;
                }*/
            }

            return false;
        }



        private void btnAgregarCombo_Click(object sender, EventArgs e)
        {
            SeleccionAcciones(dgListaProductos, listaproductostoadd);
         //   comprobarProducto(dgListaProductos, listaproductostoadd);
            AgregarListarDetalles();
            listaproductostoadd.Clear();
            limpiarSeleccion(dgListaProductos,27);

        }

        private void dgListaProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void ocultarcombos()
        {
            lbcolor.Visible = false;
            lbtalla.Visible = false;
            combocolor.Visible = false;
            combotalla.Visible = false;

        }
        private void mostarcomvbos()
        {
            lbcolor.Visible = true;
            lbtalla.Visible = true;
            combocolor.Visible = true;
            combotalla.Visible = true;

        }

        public void LlenarTextBox(int IndiceDGV)
        {
           // txtcantidad.Text = dgListaProductos.Rows[IndiceDGV].Cells[0].Value.ToString();
            txtproducto.Text = dgListaProductos.Rows[IndiceDGV].Cells[1].Value.ToString(); //tipo de cliente
            var productoObtenido = _productosRepository.Get(int.Parse(dgListaProductos.Rows[IndiceDGV].Cells[0].Value.ToString()));
            if (productoObtenido.TieneColor)
            {
                var listadeColoresProductos= _coloresRepository.GetProductoListaColor(productoObtenido.Id);
                cargarComboColores(listadeColoresProductos);
               
            }
           else
            {
                combocolor.DataSource = null;
            }
            if (productoObtenido.TieneTalla)
            {
                var listadeTallaProductos = _tallasRepository.GetTallaListaProducto(productoObtenido.Id);
                cargarComboTallas(listadeTallaProductos);
               
            } else
            {
                combotalla.DataSource = null;
            } 
            if(productoObtenido.TieneTalla==true && productoObtenido.TieneColor == true)
            {
                
            }

        }

        private void cargarComboColores(List<DetalleColor> detalleColors)
        {
            combocolor.DataSource = detalleColors;
            combocolor.ValueMember = "Id";
            combocolor.DisplayMember = "Color";
           // combocolor.SelectedIndex = 0;
        }
        private void cargarComboTallas(List<DetalleTalla> listaTallas)
        {
            combotalla.DataSource = listaTallas;
            combotalla.ValueMember = "Id";
            combotalla.DisplayMember = "Talla";
           // combotalla.SelectedIndex = 0;
        }


        private void txtcantidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

       
        private void checktodas_CheckedChanged(object sender, EventArgs e)
        {
            if (checktodas.Checked == true)
            {
                foreach (DataGridViewRow Rows in dgListaProductos.Rows)
                {

                    Rows.Cells[27].Value = true;

                }
            }
            else
            {
                foreach (DataGridViewRow Rows in dgListaProductos.Rows)
                {

                    Rows.Cells[27].Value = false;

                }
            }
            
        }

       

        private void dgListaProductos_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            LlenarTextBox(dgListaProductos.CurrentRow.Index);
            mostarcomvbos();

            bool estadoActual = Convert.ToBoolean(dgListaProductos.CurrentRow.Cells[27].Value);
            if (estadoActual)
            {
                dgListaProductos.CurrentRow.Cells[27].Value = false;
            }
            else
            {
                dgListaProductos.CurrentRow.Cells[27].Value = true;

            }
            var productActual = (ListarProductos)dgListaProductos.CurrentRow.DataBoundItem;
            var productoBuscar = _productosRepository.Get(productActual.Id);
            if(productoBuscar.TieneColor==true && productoBuscar.TieneTalla == true)
            {

                CargarDg(productActual.Id);
                btnAgregarCombo.Enabled = false;
            }
            else
            {
                btnAgregarCombo.Enabled = true;
            }
           

        }
        public void CargarDg(int idproducto)
        {


            BindingSource source = new BindingSource();
            var detalleListsObtenida = _tallasyColoresRepository.GetListaDetalleColorTallas(idproducto);
            source.DataSource = detalleListsObtenida;
            dgvtallascolores.DataSource = typeof(List<>);
            dgvtallascolores.DataSource = source;
            dgvtallascolores.AutoResizeColumns();


        }

        private void txtstock_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtstock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnaddTallasycolores_Click(object sender, EventArgs e)
        {
            CargarSizeAndColorsToCombo();
            limpiarSeleccion(dgvtallascolores, 5);
            limpiarSeleccion(dgListaProductos, 27);
        }

        private void dgvtallascolores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
            seleccionarfila(dgvtallascolores, colAccionesTyC);

        }
        private void seleccionarfila(DataGridView data, int numberCol)
        {
            bool estadoActual = Convert.ToBoolean(data.CurrentRow.Cells[numberCol].Value);
            if (estadoActual)
            {
                data.CurrentRow.Cells[numberCol].Value = false;
            }
            else
            {
                data.CurrentRow.Cells[numberCol].Value = true;

            }
        }

        private void CargarSizeAndColorsToCombo()
        {

            if (dgvtallascolores.RowCount <= 0) { return; }
            int filasSeleccion = 0;

           
            foreach (DataGridViewRow Rows in dgvtallascolores.Rows)
            {
                var filasTotales = int.Parse(dgvtallascolores.RowCount.ToString());


                bool acciones = Convert.ToBoolean(Rows.Cells[colAccionesTyC].Value);
                if (!acciones)
                {
                    filasSeleccion += 1;
                }
                else
                {
                    var detalleCombo = GetmodelDetalle();
                   
                    detalleCombo.ProductoId = int.Parse(Rows.Cells[productIDTyCcol].Value.ToString());
                    var productoobtenido = _productosRepository.Get(detalleCombo.ProductoId);
                    detalleCombo.Referencia = productoobtenido.CodigoBarras;
                    detalleCombo.Cantidad = int.Parse(txtcantidad.Text);
                    detalleCombo.Descripcion = productoobtenido.Descripcion;
                    detalleCombo.Color = Rows.Cells[colorTyCcol].Value.ToString();
                    detalleCombo.Talla = Rows.Cells[tallaTyCcol].Value.ToString();
                    detalleCombo.TallayColorId = int.Parse(Rows.Cells[IDTyCcol].Value.ToString());

                    //  detalleCombo.ComboId = Guid.Parse(Rows.Cells[1].Value.ToString());
                    listaDetalles.Add(detalleCombo);

                }


                if (filasTotales == filasSeleccion)
                {
                    KryptonMessageBox.Show("Debera tener seleccionada  la columna 'Acciones'\n "
                        + "Selecione un Producto, dando click en la columna Acciones\n"
                        );

                    return;
                }

            }
            cargarDGVPromos(listaDetalles);

        }

        private void RecargarNuevoCombo()
        {
            NuevoCombo nuevo = new NuevoCombo(formslayout);
            nuevo.MdiParent = formslayout;
            nuevo.Show();

        }

        private void txtprecioventa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as KryptonTextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;

            }
        }

        private void txtpreciomayorista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as KryptonTextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;

            }
        }

        private void txtprecioentidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as KryptonTextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;

            }
        }

        private void txtpreciocuentaclave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as KryptonTextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;

            }
        }

        private void txtrevendedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as KryptonTextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;

            }
        }

        private void btnfoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog imagen = new OpenFileDialog();
            imagen.Filter = "Archivos JPG (*.jpg)|*.jpg| Archivos png(*.png)|*.png";
            DialogResult filestream = imagen.ShowDialog();

            if (filestream == DialogResult.OK)
            {
                pBox.Image = Image.FromFile(imagen.FileName);

                filestreamImagen();
            }
        }
        private void filestreamImagen()
        {
            filefoto = null;
            MemoryStream memoryStream = new MemoryStream();
            pBox.Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            memoryStream.GetBuffer();
            filefoto = memoryStream.GetBuffer();

        }

        private void dgListaProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
