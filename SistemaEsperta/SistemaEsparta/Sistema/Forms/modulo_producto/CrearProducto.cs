using CapaDatos.ListasPersonalizadas;
using CapaDatos.Models.Precios;
using CapaDatos.Models.Productos;
using CapaDatos.Repository;
using CapaDatos.Repository.PreciosRepository;
using CapaDatos.Validation;
using ComponentFactory.Krypton.Toolkit;
using sharedDatabase.Models;
using Sistema.Forms.Extras;
using Sistema.Forms.modulo_proveedor;
using Sistema.Validations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Sistema.Forms.modulo_producto
{
    public partial class CrearProducto : BaseContext
    {
        private readonly ProductosRepository _productosRepository = null;
        private readonly TallasRepository _tallasRepository = null;
        private readonly TipoPrecioRepository _tipoPrecioRepository = null;
        private readonly ValidarExistenciaProducto _validarExistenciaProducto = null;
        private readonly ColoresRepository _coloresRepository = null;
        private readonly ClientesRepository _clientesRepository = null;
        private readonly ProveedoresRepository _proveedoresRepository = null;
        private readonly PreciosDetallePepsRepository _preciosDetallePepsRepository = null;
        private readonly MarcasRepository _marcasRepository = null;
        private readonly CategoriasPadreRepository _catPadreRepository = null;
        private readonly LineasProdRepository _lineaRepository = null;
        private readonly TomoEdicionRepository _tomoEdicionRepository = null;
        private readonly TiposClienteRepository _tiposRepository = null;
        public List<ColoresProducto> _listacolores = new List<ColoresProducto>();
        public List<DetalleTalla> _listaTallas = new List<DetalleTalla>();
        private List<ListarDetallePrecios> listaDetalles = null;
        private List<ListarDetallePrecios> lista1 = null, lista2 = null, lista3 = null, lista4 = null;
        public List<DetalleTomoEdicion> _listaNumero = null;
        private DateTime fechaInicio => Convert.ToDateTime(dtpfechainicio.Value.ToLongDateString());
        private DateTime fechaFinal => Convert.ToDateTime(dtpfechafinal.Value.ToLongDateString());
        public string codigobarra;
        double cantidadSemanas = 0;
        private Layout layout = null;
        byte[] filefoto = null;
        int selectedCategoria = 0;
        int escala = 0;
        public bool listacoloresvacia = false;
        public bool listacolorestallas = false;
        public bool listacoloresnumeros = false;

        public CrearProducto(Layout ll)
        {
            layout = ll;
            _validarExistenciaProducto = new ValidarExistenciaProducto(_context);
            _preciosDetallePepsRepository = new PreciosDetallePepsRepository(_context);
            _productosRepository = new ProductosRepository(_context);
            _tipoPrecioRepository = new TipoPrecioRepository(_context);
            _proveedoresRepository = new ProveedoresRepository(_context);
            _clientesRepository = new ClientesRepository(_context);
            _coloresRepository = new ColoresRepository(_context);
            _tallasRepository = new TallasRepository(_context);
            _marcasRepository = new MarcasRepository(_context);
            _catPadreRepository = new CategoriasPadreRepository(_context);
            _lineaRepository = new LineasProdRepository(_context);
            _tomoEdicionRepository = new TomoEdicionRepository(_context);
            _tiposRepository = new TiposClienteRepository(_context);

            listaDetalles = new List<ListarDetallePrecios>();

            lista1 = new List<ListarDetallePrecios>();
            lista2 = new List<ListarDetallePrecios>();
            lista3 = new List<ListarDetallePrecios>();
            lista4 = new List<ListarDetallePrecios>();
            _listaNumero = new List<DetalleTomoEdicion>();
            InitializeComponent();
            WindowState = FormWindowState.Normal;
        }

        private void NuevoProducto_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarCategoriasPadre();
            CargarLineas();
            Cargarproveedores();
            CargarMarcas();
            Cargapanel(PanelGeneral);
            //AddRowEscala(1);
        }

        public void Cargapanel(Panel p)
        {
            using (Control c = new Control() { Parent = p, Dock = DockStyle.Bottom })
            {
                p.ScrollControlIntoView(c);
                c.Parent = null;
            }
        }
        public void CargarCategorias()
        {
            if (selectedCategoria == 0)
            {
                cbSubcategoria.DataSource = _productosRepository.GetCategoriasList();
                cbSubcategoria.DisplayMember = "Nombre";
                cbSubcategoria.ValueMember = "Id";
                cbSubcategoria.Invalidate();
            }
            else
            {
                cbSubcategoria.DataSource = _productosRepository.GetCategoriasList(selectedCategoria);
                cbSubcategoria.DisplayMember = "Nombre";
                cbSubcategoria.ValueMember = "Id";
                cbSubcategoria.Invalidate();
            }
        }

        public void CargarCategoriasPadre()
        {
            cbCategoriaPadre.DataSource = _catPadreRepository.GetListCategoriasPadre();
            cbCategoriaPadre.DisplayMember = "Nombre";
            cbCategoriaPadre.ValueMember = "Id";
            cbCategoriaPadre.Invalidate();
        }

        public void CargarLineas()
        {
            cbLinea.DataSource = _lineaRepository.GetAllLineas();
            cbLinea.DisplayMember = "Nombre";
            cbLinea.ValueMember = "Id";
            cbLinea.Invalidate();
        }

        public void Cargarproveedores()
        {
            try
            {
                cbProveedor.DataSource = _proveedoresRepository.GetList();
                cbProveedor.DisplayMember = "Nombre";
                cbProveedor.ValueMember = "Id";
                cbProveedor.Invalidate();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("no hay ningun proveedor,deberá ingresar uno", ex.Message);
            }
        }

        public void CargarMarcas()
        {
            try
            {
                cbMarca.DataSource = _marcasRepository.GetList();
                cbMarca.DisplayMember = "Nombre";
                cbMarca.ValueMember = "Id";
                cbMarca.Invalidate();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("¡No hay ninguna marca, deberá ingresar una! ", ex.Message);
            }
        }

        private Producto GetviewModel()
        {
            var producto = new Producto()
            {
                CategoriaId = Convert.ToInt32(cbSubcategoria.SelectedValue.ToString()),
                ProveedorId = Convert.ToInt32(cbProveedor.SelectedValue.ToString()),
                LineaId = Convert.ToInt32(cbLinea.SelectedValue.ToString()),
                MarcaId = Convert.ToInt32(cbMarca.SelectedValue.ToString()),
                SucursalId = UsuarioLogeadoSistemas.User.SucursalId,

                CodigoBarras = txtCodigoBarras.Text,
                Descripcion = txtdescripcionprod.Text,
                DescripcionCorta = txtdescripcortaprod.Text,
                Impuesto = 0.12M,
                Coste = Convert.ToDecimal(txtPrecioCompra.Text),
                PrecioVenta = Convert.ToDecimal(txtPrecioGeneral.Text),
                PrecioEspecial = Convert.ToDecimal(txtPrecioEspecial.Text),
                PrecioMinimo = Convert.ToDecimal(txtPrecioMin.Text),

                TieneEscalas = checkEscala.Checked,
                TieneColor = checkColor.Checked,
                TieneTalla = checkTalla.Checked,

                stockMinimo = Convert.ToInt32(txtcantidadmin.Text),
                Imagen = filefoto,
                FechaIngreso = DateTime.Now,
                FechaModificacion = DateTime.Now,
                Notas = notasinternas.Text,
                Stock = Convert.ToInt32(stockproducto.Text),
                PeriodoMovimiento = cantidadSemanas.ToString(),

                //UnidadMedida = txtunidadmedida.Text,
                //Numeral = txtnumeral.Text,
            };

            return producto;

        }

        private TipoPrecio GetModelTipoPrecio()
        {
            var tipoprecio = new TipoPrecio()
            {
                Id = Guid.NewGuid(),
                FechaInicio = DateTime.Now,

            };
            return tipoprecio;
        }

        private DetalleColor GetModeloColor(ColoresProducto detalle)
        {
            var DetalleColor = new DetalleColor()
            {
                Color = detalle.Color,
                Stock = detalle.Cantidad,
                CodigoGeneral = detalle.Codigo,
            };
            return DetalleColor;
        }

        private DetalleTalla GetModeloTalla(DetalleTalla detalle)
        {
            var DetalleTalla = new DetalleTalla()
            {
                Talla = detalle.Talla,
                Stock = detalle.Stock,
                ProductoId = detalle.ProductoId,
                Producto = detalle.Producto
            };
            return DetalleTalla;
        }

        private DetalleTomoEdicion GetModeloTE(DetalleTomoEdicion detalle)
        {
            var DetalleTE = new DetalleTomoEdicion()
            {
                Tomo = detalle.Tomo,
                Codigo = detalle.Codigo,
                Edicion = detalle.Edicion,
                Stock = detalle.Stock
            };
            return DetalleTE;
        }

        private void txtPrecioCompra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
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

        private void checkEscala_CheckedChanged(object sender, EventArgs e)
        {
            OcultarMostrarEscalas();
        }

        private void checkColor_CheckedChanged(object sender, EventArgs e)
        {
            checkcolorPropertis();
        }

        private void checkTalla_CheckedChanged(object sender, EventArgs e)
        {
            checktallaPropertis();
        }

        private void checkNumero_CheckedChanged(object sender, EventArgs e)
        {
            checknumeroProperties();
        }

        private void checkExistencia_CheckedChanged(object sender, EventArgs e)
        {
            if (checkExistencia.Checked == false)
            {
                lbcantidadmin.Visible = false;
                txtcantidadmin.Visible = false;
            }
            else
            {

                lbcantidadmin.Visible = true;
                txtcantidadmin.Visible = true;
            }
        }

        private void checkMovimiento_CheckedChanged(object sender, EventArgs e)
        {
            if (checkMovimiento.Checked == false)
            {
                rbcuatro.Visible = false;
                rbcuatro.Checked = false;
                rbochosem.Visible = false;
                rbochosem.Checked = false;
                rbpersonal.Visible = false;
                rbpersonal.Checked = false;
                dtpfechainicio.Visible = false;
                dtpfechafinal.Visible = false;
                lbfechainicio.Visible = false;
                lbfechafinal.Visible = false;
            }
            else
            {
                rbcuatro.Visible = true;
                rbochosem.Visible = true;
                rbpersonal.Visible = true;
            }
        }

        private void rbpersonal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbpersonal.Checked == false)
            {
                dtpfechafinal.Visible = false;
                dtpfechainicio.Visible = false;
                lbfechainicio.Visible = false;
                lbfechafinal.Visible = false;
            }
            else
            {
                dtpfechafinal.Visible = true;
                dtpfechainicio.Visible = true;
                lbfechainicio.Visible = true;
                lbfechafinal.Visible = true;
            }

        }

        private void cbCategoriaPadre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCategoriaPadre.SelectedValue != null)
            {
                object value = cbCategoriaPadre.SelectedValue;
                string v = value.ToString();
                int number;
                if (Int32.TryParse(v, out number))
                {
                    selectedCategoria = Convert.ToInt32(cbCategoriaPadre.SelectedValue);
                }
                else
                {
                    var element = (CategoriaPadre)cbCategoriaPadre.SelectedValue;
                    int pausa = element.Id;

                }

            }

            CargarCategorias();
        }

        private void btnCrearCodigo_Click(object sender, EventArgs e)
        {
            // KryptonMessageBox.Show("estas aca");
            try
            {
                int numeroA1 = new Random().Next(100, 9999);
                int numeroA2 = new Random().Next(100, 9999);
                int max = _productosRepository.GetAllProductos().Count + 1;
                string codigoBarras = numeroA1 + "" + numeroA2 + "" + max;
                BarcodeLib.Barcode codeBar = new BarcodeLib.Barcode();
                codeBar.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                codeBar.IncludeLabel = true;
                codeBar.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;

                BarcodeLib.TYPE bCodeType = BarcodeLib.TYPE.CODE39;
                System.Drawing.Image imgTmpCodeBar = codeBar.Encode(bCodeType, codigoBarras, System.Drawing.Color.Black, System.Drawing.Color.White, 200, 200);

                MemoryStream memoryStream = new MemoryStream();
                imgTmpCodeBar.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                pbCodigoBarras.Image = imgTmpCodeBar;
                txtCodigoBarras.Text = codigoBarras;
                txtCodigoCorto.Text = max.ToString();
                btnGuardarEtiqueta.Enabled = true;

            }
            catch (Exception err)
            {
                KryptonMessageBox.Show("Ocurrió un error \n {0}", err.Message);
            }
        }

        private void btnNuevoProveedor_Click(object sender, EventArgs e)
        {
            cbProveedor.Enabled = false;

            if (Application.OpenForms["NuevoProveedor"] == null)
            {
                NuevoProveedor proveedor = new NuevoProveedor();
                proveedor.crearprod = this;
                proveedor.MdiParent = this.MdiParent;
                proveedor.Show();
            }

            cbProveedor.Enabled = true;
        }
        private void btnNuevaMarca_Click(object sender, EventArgs e)
        {
            cbMarca.Enabled = false;
            if (Application.OpenForms["AgregarMarca"] == null)
            {
                NuevaMarca marca = new NuevaMarca();
                marca.crearprod = this;
                marca.Show();
            }

            cbMarca.Enabled = true;
        }

        private void btnGuardarEtiqueta_Click(object sender, EventArgs e)
        {
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        this.pbCodigoBarras.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        this.pbCodigoBarras.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        this.pbCodigoBarras.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }

        private void btnAddMin_Click(object sender, EventArgs e)
        {
            contarEscalas(1);
            dgvMinorista.Rows.Add();
            dgvMinorista.ClearSelection();
        }

        private void btnAddRev_Click(object sender, EventArgs e)
        {
            contarEscalas(2);
            dgvRevendedor.Rows.Add();
            dgvRevendedor.ClearSelection();
        }

        private void btnAddEsp_Click(object sender, EventArgs e)
        {
            contarEscalas(3);
            dgvEspecial.Rows.Add();
            dgvEspecial.ClearSelection();
        }

        private void btnAddGob_Click(object sender, EventArgs e)
        {
            contarEscalas(4);
            dgvGobierno.Rows.Add();
            dgvGobierno.ClearSelection();
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

        private void btnProductoNuevo_Click(object sender, EventArgs e)
        {
            CalcularTiempo();
            if (_validarExistenciaProducto.ValidarRegistro(txtCodigoBarras.Text, UsuarioLogeadoSistemas.User.SucursalId))
            {
                KryptonMessageBox.Show("Ya existe un producto con este código.", "ERROR", MessageBoxButtons.OK);
                return;
            }
            string validacion = ValidarNullos();
            if (validacion != "")
            {
                string msj = validacion;
                KryptonMessageBox.Show(msj);
                return;
            }
            try
            {
                var model = GetviewModel();
                if (!ModelState.IsValid(model)) return;
                _productosRepository.Add(model);
                GuardarColores();
                GuardarTallas();
                GuardarNumeros();
                if (checkEscala.Checked == true)
                {
                    GuardarPrecios();
                }
                KryptonMessageBox.Show("¡Producto Guardado Exitosamente!");
                RecargarForm();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Valide los campos ingresados, no se aceptan nullos", ex.Message);
                return;
            }
        }

        private void DataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            KryptonMessageBox.Show("Datos Inválidos, revise la información ingresada.", "Notificación");
        }

        private void dgvMinorista_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DeleteRowEscala(0);
        }

        private void dgvRevendedor_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DeleteRowEscala(1);
        }

        private void dgvEspecial_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DeleteRowEscala(2);
        }

        private void dgvGobierno_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DeleteRowEscala(3);
        }

        private void checkcolorPropertis()
        {

            if (_listacolores == null)
            {
                _listacolores = new List<ColoresProducto>();
            }

            if (checkColor.Checked == true)
            {
                if (Application.OpenForms["AgregarColor"] == null)
                {
                    AgregarColor agregarColor = new AgregarColor(this, _listacolores);
                    agregarColor.Show();
                }
                else { Application.OpenForms["AgregarColor"].Activate(); }
            }
            else
            {
                _listacolores.Clear();
            }
        }

        private void checktallaPropertis()
        {

            if (_listaTallas == null)
            {
                _listaTallas = new List<DetalleTalla>();
            }
            if (checkTalla.Checked == true)
            {

                if (Application.OpenForms["AgregarTalla"] == null)
                {
                    AgregarTalla agregarTalla = new AgregarTalla(this, _listaTallas);
                    agregarTalla.Show();
                }
                else { Application.OpenForms["AgregarTalla"].Activate(); }
            }
            else
            {
                _listaTallas.Clear();
            }
        }

        private void checknumeroProperties()
        {
            if (txtCodigoBarras.Text == "")
            {
                KryptonMessageBox.Show("¡Debe ingresar un codigo de referencia para continuar!");
                return;
            }
            if (_listaNumero == null)
            {
                _listaNumero = new List<DetalleTomoEdicion>();
            }
            if (checkNumero.Checked == true)
            {
                if (Application.OpenForms["AgregarNumero"] == null)
                {
                    codigobarra = txtCodigoBarras.Text;
                    AgregarNumero AgregarNumero = new AgregarNumero(this, _listaNumero);
                    AgregarNumero.Show();
                }
                else { Application.OpenForms["AgregarNumero"].Activate(); }
            }
            else
            {
                _listaNumero.Clear();
            }
        }

        private void OcultarMostrarEscalas()
        {
            if (checkEscala.Checked == true)
            {
                tlpEscalas.Visible = true;
            }
            else
            {
                tlpEscalas.Visible = false;
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

        public void CalcularTiempo()
        {

            if (rbcuatro.Checked)
            {
                cantidadSemanas = 4;
            }
            if (rbochosem.Checked)
            {
                cantidadSemanas = 8;
            }

            if (rbpersonal.Checked)
            {
                cantidadSemanas = (fechaFinal - fechaInicio).TotalDays / 7;

            }
        }

        private void contarEscalas(int opc)
        {
            int r = 0;
            switch (opc)
            {
                case 1:
                    r = dgvMinorista.Rows.Count + 1;
                    AddRowEscala(r);
                    break;
                case 2:
                    r = dgvRevendedor.Rows.Count + 1;
                    AddRowEscala(r);
                    break;
                case 3:
                    r = dgvEspecial.Rows.Count + 1;
                    AddRowEscala(r);
                    break;
                case 4:
                    r = dgvGobierno.Rows.Count + 1;
                    AddRowEscala(r);
                    break;
            }

        }

        private void AddRowEscala(int contador)
        {
            escala = dgvEscalas.Rows.Count;
            if (contador > escala)
            {
                escala++;
                dgvEscalas.Rows.Add("Escala " + escala);
                dgvEscalas.ClearSelection();
            }
        }

        private void DeleteRowEscala(int grid)
        {
            List<int> filasgrids = new List<int>();
            filasgrids.Add(dgvMinorista.Rows.Count);
            filasgrids.Add(dgvRevendedor.Rows.Count);
            filasgrids.Add(dgvEspecial.Rows.Count);
            filasgrids.Add(dgvGobierno.Rows.Count);

            int max = filasgrids.Max();
            int index = filasgrids.IndexOf(max);

            List<int> tmp = filasgrids.FindAll(x => x.Equals(max));

            if (tmp.Count() == 1)
            {
                if (index == grid)
                {
                    max -= 1;
                    if (escala > max)
                    {
                        dgvEscalas.Rows.RemoveAt(max);
                        escala -= 1;
                    }
                }
            }
        }

        public void cambiarCheck(bool check, int elemento)
        {
            switch (elemento)
            {
                case 1:
                    checkColor.Checked = check;
                    break;
                case 2:
                    checkTalla.Checked = check;
                    break;
                case 3:
                    checkNumero.Checked = check;
                    break;
            }
        }

        private void GuardarColores()
        {
            if (!listacoloresvacia)
            {
                Producto producto = ObtenerProdG();

                if (producto != null)
                {
                    if (checkColor.Checked == true && _listacolores.Any() == true)
                    {
                        try
                        {
                            foreach (var item in _listacolores)
                            {

                                var detalleModel = GetModeloColor(item);
                                detalleModel.ProductoId = producto.Id;
                                detalleModel.Producto = producto;
                                if (!ModelState.IsValid(detalleModel)) return;
                                _coloresRepository.Add(detalleModel);
                            }
                        }
                        catch (Exception err)
                        {
                            KryptonMessageBox.Show("Error en guardar colores" + err.Message);
                        }

                    }
                }
            }
        }

        private void GuardarTallas()
        {
            if (!listacolorestallas)
            {
                Producto producto = ObtenerProdG();

                if (producto != null)
                {
                    if (checkTalla.Checked == true && _listaTallas.Any() == true)
                    {
                        foreach (var item in _listaTallas)
                        {
                            item.ProductoId = producto.Id;
                            item.Producto = producto;

                            var detalleModel = GetModeloTalla(item);
                            if (!ModelState.IsValid(detalleModel)) return;
                            _tallasRepository.Add(detalleModel);
                        }
                    }
                }
            }
        }

        private void GuardarNumeros()
        {
            if (!listacoloresnumeros)
            {
                Producto producto = ObtenerProdG();

                if (producto != null)
                {
                    if (checkNumero.Checked == true && _listaNumero.Any() == true)
                    {
                        foreach (var item in _listaNumero)
                        {
                            var detalleModel = GetModeloTE(item);
                            if (!ModelState.IsValid(detalleModel)) return;
                            _tomoEdicionRepository.Add(detalleModel);
                        }
                    }
                }
            }
        }

        private Producto ObtenerProdG()
        {
            if (_productosRepository.GetProductByBarCode(txtCodigoBarras.Text, UsuarioLogeadoSistemas.User.SucursalId) == null) return new Producto();

            var productoObtenido = _productosRepository.GetProductByBarCode(txtCodigoBarras.Text, UsuarioLogeadoSistemas.User.SucursalId);

            return productoObtenido;
        }

        private void GuardarPrecios()
        {
            try
            {
                Producto productoReciente = ObtenerProdG();

                if (productoReciente != null)
                {
                    DatagridToList();
                    var listaEscalas = new List<ListarDetallePrecios>();
                    var listaDetalle = new List<DetallePrecio>();

                    listaEscalas = lista1.Concat(lista2).Concat(lista3).Concat(lista4).ToList();

                    var encabezadoTipoprecio = GetModelTipoPrecio();

                    if (!ModelState.IsValid(encabezadoTipoprecio)) return;

                    encabezadoTipoprecio.ProductoId = productoReciente.Id;
                    _tipoPrecioRepository.Add(encabezadoTipoprecio);

                    foreach (var item in listaEscalas)
                    {
                        DetallePrecio detallePrecio = new DetallePrecio();
                        detallePrecio.Id = Guid.NewGuid();
                        detallePrecio.TipoPrecioId = encabezadoTipoprecio.Id;
                        detallePrecio.Escala = item.Escala;
                        detallePrecio.Precio = item.Precio;
                        detallePrecio.RangoInicio = item.RangoInicio;
                        detallePrecio.RangoFinal = item.RangoFinal;
                        detallePrecio.TiposId = item.TiposId;
                        listaDetalle.Add(detallePrecio);
                    }

                    foreach (var detailPrecio in listaDetalle)
                    {
                        _tipoPrecioRepository.AddDetallePrecio(detailPrecio);
                    }
                }
            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show("Revise las escalas de precio y los valores ingresados", ex.Message);
            }
        }

        private void DatagridToList()
        {
            if (dgvMinorista.RowCount > 0)
            {
                dgvTolista(lista1, dgvMinorista, "Minorista");
            }
            if (dgvRevendedor.RowCount > 0)
            {
                dgvTolista(lista2, dgvRevendedor, "Revendedor");
            }
            if (dgvEspecial.RowCount > 0)
            {
                dgvTolista(lista3, dgvEspecial, "Especial");
            }
            if (dgvGobierno.RowCount > 0)
            {
                dgvTolista(lista4, dgvGobierno, "Gubernamental");
            }
        }

        private void dgvMinorista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                var fila = dgvMinorista.CurrentRow;
                int count = dgvMinorista.RowCount - 1;
                if (fila.Index == count)
                {
                    btnAddMin_Click(null, null);
                }
            }
        }

        private void dgvRevendedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                var fila = dgvRevendedor.CurrentRow;
                int count = dgvRevendedor.RowCount - 1;
                if (fila.Index == count)
                {
                    btnAddRev_Click(null, null);
                }
            }
        }

        private void dgvEspecial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                var fila = dgvEspecial.CurrentRow;
                int count = dgvEspecial.RowCount - 1;
                if (fila.Index == count)
                {
                    btnAddEsp_Click(null, null);
                }
            }
        }

        private void dgvGobierno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                var fila = dgvGobierno.CurrentRow;
                int count = dgvGobierno.RowCount - 1;
                if (fila.Index == count)
                {
                    btnAddGob_Click(null, null);
                }
            }
        }

        private List<ListarDetallePrecios> dgvTolista(List<ListarDetallePrecios> lista, DataGridView dataGrid, string cliente)
        {
            int ContadorFilaActual = 0;
            foreach (DataGridViewRow rows in dataGrid.Rows)
            {
                if (rows.Cells[0].Value != null && rows.Cells[1].Value != null && rows.Cells[2].Value != null)
                {
                    ListaDetallePrecio nueva = new ListaDetallePrecio();
                    int value = ContadorFilaActual + 1;
                    nueva.Escala = "E" + value;
                    nueva.PrecioMinimo = rows.Cells[0].Value.ToString();
                    nueva.PrecioMaximo = rows.Cells[1].Value.ToString();
                    nueva.Precio = rows.Cells[2].Value.ToString();

                    if (nueva.PrecioMaximo.Length > 0 && nueva.Precio.Length > 0 && nueva.PrecioMinimo.Length > 0)
                    {
                        var tmp = GetModelListarDetallePrecios(nueva, cliente);
                        lista.Add(tmp);
                    }
                    ContadorFilaActual++;
                }
            }
            return lista;
        }

        private void cbLinea_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbSubcategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private ListarDetallePrecios GetModelListarDetallePrecios(ListaDetallePrecio lista, string cliente)
        {
            var DetallePrecios = new ListarDetallePrecios()
            {
                Id = Guid.NewGuid(),
                Escala = lista.Escala,
                Precio = decimal.Parse(lista.Precio),
                Tipos = cliente,
                TiposId = _tiposRepository.GetTipoName(cliente).Id,
                RangoFinal = int.Parse(lista.PrecioMaximo.ToString()),
                RangoInicio = int.Parse(lista.PrecioMinimo.ToString()),
            };
            return DetallePrecios;
        }

        private string ValidarNullos()
        {
            string msj = "";
            if (cbSubcategoria.SelectedValue == null)
            {
                msj = "Debe seleccionar una categoria";
            }
            else
            if (cbProveedor.SelectedValue == null)
            {
                msj = "Debe seleccionar un proveedor";
            }
            else
            if (string.IsNullOrEmpty(txtCodigoBarras.Text))
            {
                msj = "Debe ingresar un codigo de referencia";
            }
            else
            if (string.IsNullOrEmpty(txtdescripcionprod.Text))
            {
                msj = "Debe ingresar una descripcion del producto";
            }
            //else
            //if (!Int32.TryParse(stockproducto.Text, out var n) || stockproducto.Text=="0")
            //{

            //     msj= "Debe ingresar una cantidad de stock valida";
            //}
            else
            if (!decimal.TryParse(txtPrecioCompra.Text, out var a) || txtPrecioCompra.Text == "0.00")
            {

                msj = "Debe ingresar un precio valido en Precio de Compra";
            }
            else
            if (!decimal.TryParse(txtPrecioGeneral.Text, out var b) || txtPrecioGeneral.Text == "0.00")
            {

                msj = "Debe ingresar un precio valido en Precio General";
            }
            //else
            //if (filefoto == null)
            //{
            //    msj = "Debe ingresar una imagen";
            //}
            //if (checkEscala.Checked == false)
            //{
            //    if (string.IsNullOrEmpty(txtPrecioGeneral.Text) || !Int32.TryParse(txtPrecioGeneral.Text, out var numberStyles))
            //    {
            //        msj = "Debe ingresar un precio de venta valido";
            //    }
            //}
            return msj;
        }

        private void RecargarForm()
        {
            Hide();
            CrearProducto ProductoNuevo = new CrearProducto(this.layout);
            ProductoNuevo.MdiParent = layout;
            ProductoNuevo.Show();
            Close();
        }
    }
}

