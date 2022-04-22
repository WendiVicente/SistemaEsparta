using ComponentFactory.Krypton.Toolkit;
using Sistema.Forms.Extras;
using Sistema.Forms.modulo_proveedor;
using Sistema.Validations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CapaDatos.Models;
using CapaDatos.Repository;
using CapaDatos.Repository.PreciosRepository;
using CapaDatos.Models.Productos;
using CapaDatos.ListasPersonalizadas;
using sharedDatabase.Models;
using CapaDatos.Validation;
using CapaDatos.Models.Precios;

namespace Sistema.Forms.modulo_producto
{
    public partial class EditarProducto : BaseContext
    {
        private readonly ProductosRepository _productosRepository = null;
        private readonly TallasRepository _tallasRepository = null;
        private readonly TipoPrecioRepository _tipoPrecioRepository = null;
        private readonly ColoresRepository _coloresRepository = null;
        private readonly ProveedoresRepository _proveedoresRepository = null;
        private readonly MarcasRepository _marcasRepository = null;
        private readonly CategoriasPadreRepository _catPadreRepository = null;
        private readonly LineasProdRepository _lineaRepository = null;
        private readonly TomoEdicionRepository _tomoEdicionRepository = null;
        private readonly TiposClienteRepository _tiposClienteRepository = null;
        private readonly CategoriaProdRepository _categorias = null;
        private readonly ColoresRepository _detalleColor = null;
        private readonly TallasRepository _detalleTalla = null;
        private readonly TomoEdicionRepository _detalleTomoEdicion = null;

        public List<DetalleColor> _listaColores = null;
        public List<DetalleTalla> _listaTallas = null;
        public IList<DetalleTomoEdicion> _listaTomoEdicion = null;
        private List<ListarDetallePrecios> listaDetalles = null;
        private List<ListarDetallePrecios> lista1 = null, lista2 = null, lista3 = null, lista4 = null, _listaPreciosLocal = null;
        public List<DetalleColor> _listacoloresDel = new List<DetalleColor>();
        public List<DetalleTalla> _listaTallasDel = new List<DetalleTalla>();
        public IList<DetalleTomoEdicion> _listaTomoEdicionDel = new List<DetalleTomoEdicion>();
        private List<ListarDetallePrecios> _listaPreciosDel = new List<ListarDetallePrecios>();


        public bool listacoloresvacia = false;
        public bool listacolorestallas = false;
        public bool listacoloresnumeros = false;

        private DateTime fechaInicio => Convert.ToDateTime(dtpfechainicio.Value.ToLongDateString());
        private DateTime fechaFinal => Convert.ToDateTime(dtpfechafinal.Value.ToLongDateString());
        public string codigobarra;
        double cantidadSemanas = 0;
        byte[] filefoto = null;
        int selectedCategoria = 0;
        int escala = 0;
        private decimal Impuesto;

        public Producto _producto = null;
        private ListaProductos _listaProductos = null;
        public int stockValidar = 0;
        public int indiceProducto = 0;

        int escalaMin = 0;
        int escalaRev = 0;
        int escalaEsp = 0;
        int escalaGob = 0;

        public EditarProducto(ListaProductos listaProductos, Producto producto)
        {
            _listaProductos = listaProductos;
            _productosRepository = new ProductosRepository(_context);
            _tipoPrecioRepository = new TipoPrecioRepository(_context);
            _proveedoresRepository = new ProveedoresRepository(_context);
            _coloresRepository = new ColoresRepository(_context);
            _tallasRepository = new TallasRepository(_context);
            _marcasRepository = new MarcasRepository(_context);
            _catPadreRepository = new CategoriasPadreRepository(_context);
            _lineaRepository = new LineasProdRepository(_context);
            _tomoEdicionRepository = new TomoEdicionRepository(_context);
            _tiposClienteRepository = new TiposClienteRepository(_context);
            _categorias = new CategoriaProdRepository(_context);
            _detalleTomoEdicion = new TomoEdicionRepository(_context);
            _detalleColor = new ColoresRepository(_context);
            _detalleTalla = new TallasRepository(_context);

            listaDetalles = new List<ListarDetallePrecios>();
            lista1 = new List<ListarDetallePrecios>();
            lista2 = new List<ListarDetallePrecios>();
            lista3 = new List<ListarDetallePrecios>();
            lista4 = new List<ListarDetallePrecios>();
            _listaPreciosLocal = new List<ListarDetallePrecios>();
            _listaTomoEdicion = new List<DetalleTomoEdicion>();
            _listaColores = new List<DetalleColor>();
            _listaTallas = new List<DetalleTalla>();

            _producto = producto;

            InitializeComponent();
            WindowState = FormWindowState.Normal;
        }

        private void EditarProducto_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarCategoriasPadre();
            CargarLineas();
            Cargarproveedores();
            CargarMarcas();
            CargarProducto();
            CargarDetalles();
            CargarEscalasPrecios();
            CargarEtiqueta();
            CargarImg();
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

        private void CargarImg()
        {
            if (_producto.Imagen == null) { return; }
            byte[] imageData = _producto.Imagen;
            MemoryStream mStream = new MemoryStream(imageData);
            pBox.Image = Image.FromStream(mStream);
            filefoto = imageData;
        }

        public void CargarProducto()
        {
            if (_producto != null)
            {
                if (_producto.CategoriaId > 0)
                {
                    Categoria cat = _categorias.GetCategoria(_producto.CategoriaId);
                    cbCategoriaPadre.SelectedValue = cat.CategoriaPadreId;
                    cbSubcategoria.SelectedValue = _producto.CategoriaId;
                }
                if (_producto.ProveedorId > 0)
                {
                    cbProveedor.SelectedValue = _producto.ProveedorId;
                }
                if (_producto.LineaId > 0)
                {
                    cbLinea.SelectedValue = _producto.LineaId;
                }
                if (_producto.MarcaId > 0)
                {
                    cbMarca.SelectedValue = _producto.MarcaId;
                }

                txtCodigoBarras.Text = _producto.CodigoBarras.ToString();
                txtCodigoCorto.Text = _producto.Id.ToString();
                txtdescripcionprod.Text = _producto.Descripcion.ToString();
                txtdescripcortaprod.Text = _producto.DescripcionCorta.ToString();
                Impuesto = _producto.Impuesto;
                txtPrecioCompra.Text = _producto.Coste.ToString();
                txtPrecioGeneral.Text = _producto.PrecioVenta.ToString();
                txtPrecioEspecial.Text = _producto.PrecioEspecial.ToString();
                txtPrecioMin.Text = _producto.PrecioMinimo.ToString();

                if (_producto.stockMinimo > 0)
                {
                    checkExistencia.Checked = true;
                    txtcantidadmin.Text = _producto.stockMinimo.ToString();
                }

                if (_producto.Imagen != null)
                {
                    byte[] imageData = _producto.Imagen;
                    MemoryStream mStream = new MemoryStream(imageData);
                    pBox.Image = Image.FromStream(mStream);
                }
                notasinternas.Text = _producto.Notas;
                stockproducto.Text = _producto.Stock.ToString();
                stockValidar = _producto.Stock;

                checkColor.Checked = _producto.TieneColor;
                checkTalla.Checked = _producto.TieneTalla;
                checkNumero.Checked = _producto.TieneTomoEdicion;
            }
        }

        public void CargarDetalles()
        {
            if (_detalleColor.GetColor(_producto.Id) != null)
            {
                var productodetalleColor = _coloresRepository.GetProductoListaColor(_producto.Id);
                //stockproducto.Text = productodetalleColor.Sum(x => x.Stock).ToString();
                _listaColores = productodetalleColor;
            }

            if (_tallasRepository.GetTallaListaProducto(_producto.Id) != null)
            {
                var productodetalleTalla = _tallasRepository.GetTallaListaProducto(_producto.Id);
                //stockproducto.Text = productodetalleTalla.Sum(x => x.Stock).ToString();
                _listaTallas = productodetalleTalla;
            }

            if (_tomoEdicionRepository.GetListTomosEdiciones(_producto.CodigoBarras) != null)
            {
                var productodetalleTE = _tomoEdicionRepository.GetListTomosEdiciones(_producto.CodigoBarras);
                //stockproducto.Text = productodetalleTE.Sum(x => x.Stock).ToString();
                _listaTomoEdicion = productodetalleTE;
            }
        }

        private void CargarEscalasPrecios()
        {
            if (_producto.TieneEscalas != false)
            {
                checkEscala.Checked = true;
                tlpEscalas.Visible = true;
                var tipo = _tipoPrecioRepository.Get(_producto.Id);
                if (tipo != null)
                {
                    var detalleprecioslista = _tipoPrecioRepository.GetDetallePrecioListar(tipo.Id, 0);
                    if (detalleprecioslista != null)
                    {
                        foreach (var item in detalleprecioslista)
                        {
                            var tipoObtendio = _tiposClienteRepository.GetTipo(item.TiposId);
                            if (tipoObtendio.TipoCliente == "Minorista")
                            {
                                lista1.Add(item);
                            }
                            if (tipoObtendio.TipoCliente == "Revendedor")
                            {
                                lista2.Add(item);
                            }
                            if (tipoObtendio.TipoCliente == "Especial")
                            {
                                lista3.Add(item);
                            }
                            if (tipoObtendio.TipoCliente == "Gubernamental")
                            {
                                lista4.Add(item);
                            }
                        }
                        CargarDGVPrecios(lista1, dgvMinorista);
                        CargarDGVPrecios(lista2, dgvRevendedor);
                        CargarDGVPrecios(lista3, dgvEspecial);
                        CargarDGVPrecios(lista4, dgvGobierno);

                        List<int> vl = new List<int>();
                        escalaMin = lista1.Count + 1;
                        vl.Add(lista1.Count);
                        escalaRev = lista2.Count + 1;
                        vl.Add(lista2.Count);
                        escalaEsp = lista3.Count + 1;
                        vl.Add(lista3.Count);
                        escalaGob = lista4.Count + 1;
                        vl.Add(lista4.Count);
                        int max = vl.Max();
                        for (int i = 1; i <= max; i++)
                        {
                            dgvEscalas.Rows.Add("Escala " + i);
                        }
                        escala = dgvEscalas.Rows.Count + 1;
                        dgvEscalas.ClearSelection();
                    }
                }
            }
        }

        //private void CargarDGVPrecios(List<ListarDetallePrecios> lista, DataGridView datag)
        //{
        //    foreach (ListarDetallePrecios det in lista)
        //    {
        //        datag.Rows.Add(det.Id, det.TipoPrecioId, det.TiposId, det.RangoInicio, det.RangoFinal, det.Precio);
        //    }
        //    datag.ClearSelection();
            
        //}

        private void CargarDGVPrecios(List<ListarDetallePrecios> lista, DataGridView datag)
        {
            BindingSource source = new BindingSource();
            source.DataSource = lista;
            datag.DataSource = typeof(List<>);
            datag.DataSource = source;
            datag.Columns[0].Visible = false;
            datag.Columns[1].Visible = false;
            datag.Columns[2].Visible = false;
            datag.Columns[3].Visible = false;
            datag.Columns[4].Visible = true;
            datag.Columns[5].Visible = true;
            datag.Columns[6].Visible = true;
            datag.Columns[7].Visible = false;

            datag.Columns[4].HeaderText = "De";
            datag.Columns[5].HeaderText = "A";
            datag.Columns[6].HeaderText = "Precio";
            datag.ClearSelection();
            datag.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void CargarEtiqueta()
        {
            if (txtCodigoBarras.TextLength > 0)
            {
                BarcodeLib.Barcode codeBar = new BarcodeLib.Barcode();
                codeBar.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                codeBar.IncludeLabel = true;
                codeBar.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;
                string codigo = txtCodigoBarras.Text.Trim();
                BarcodeLib.TYPE bCodeType = BarcodeLib.TYPE.CODE39;
                System.Drawing.Image imgTmpCodeBar = codeBar.Encode(bCodeType, codigo, System.Drawing.Color.Black, System.Drawing.Color.White, 200, 200);

                MemoryStream memoryStream = new MemoryStream();
                imgTmpCodeBar.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                pbCodigoBarras.Image = imgTmpCodeBar;
            }
        }

        private Producto GetviewModel(Producto Prod)
        {

            Prod.CategoriaId = Convert.ToInt32(cbSubcategoria.SelectedValue.ToString());
            Prod.ProveedorId = Convert.ToInt32(cbProveedor.SelectedValue.ToString());
            Prod.LineaId = Convert.ToInt32(cbLinea.SelectedValue.ToString());
            Prod.MarcaId = Convert.ToInt32(cbMarca.SelectedValue.ToString());
            Prod.SucursalId = UsuarioLogeadoSistemas.User.SucursalId;

            Prod.CodigoBarras = txtCodigoBarras.Text;
            Prod.Descripcion = txtdescripcionprod.Text;
            Prod.DescripcionCorta = txtdescripcortaprod.Text;

            Prod.Coste = Convert.ToDecimal(txtPrecioCompra.Text);
            Prod.PrecioVenta = Convert.ToDecimal(txtPrecioGeneral.Text);
            Prod.PrecioEspecial = Convert.ToDecimal(txtPrecioEspecial.Text);
            Prod.PrecioMinimo = Convert.ToDecimal(txtPrecioMin.Text);

            Prod.TieneEscalas = checkEscala.Checked;
            Prod.TieneColor = checkColor.Checked;
            Prod.TieneTalla = checkTalla.Checked;
            Prod.TieneTomoEdicion = checkNumero.Checked;

            Prod.stockMinimo = Convert.ToInt32(txtcantidadmin.Text);
            Prod.Imagen = filefoto;
            Prod.FechaIngreso = DateTime.Now;
            Prod.FechaModificacion = DateTime.Now;
            Prod.Notas = notasinternas.Text;
            Prod.Stock = Convert.ToInt32(stockproducto.Text);
            Prod.PeriodoMovimiento = cantidadSemanas.ToString();

            return Prod;

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
            //if (checkColor.Checked == true)
            //{
            //    checkcolorPropertis();
            //}
            //else
            //{
            //    OcultarForm();

            //}
        }

        private void checkTalla_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkTalla.Checked == true)
            //{
            //    checktallaPropertis();
            //}
            //else
            //{
            //    OcultarForm();
            //}
        }

        private void checkNumero_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkNumero.Checked == true)
            //{
            //    checknumeroProperties();
            //}
            //else
            //{
            //    OcultarForm();
            //}

        }

        private void checkNumero_Click(object sender, EventArgs e)
        {
            checknumeroProperties();
        }

        private void checkTalla_Click(object sender, EventArgs e)
        {
            checktallaPropertis();
        }

        private void checkColor_Click(object sender, EventArgs e)
        {
            checkcolorPropertis();
        }

        private void checkExistencia_CheckedChanged(object sender, EventArgs e)
        {
            lbcantidadmin.Visible = checkExistencia.Checked;
            txtcantidadmin.Visible = checkExistencia.Checked;
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
                dtpfechainicio.Visible = true;
                dtpfechafinal.Visible = true;
                lbfechainicio.Visible = true;
                lbfechafinal.Visible = true;

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

        private void stockproducto_TextChanged(object sender, EventArgs e)
        {
            if (stockproducto.Text.Length > 0)
            {
                stockValidar = Convert.ToInt32(stockproducto.Text);
                if (stockValidar > 0)
                {
                    checkColor.Checked = false;
                    checkTalla.Checked = false;
                    checkNumero.Checked = false;
                }
            }

        }

        private void btnCrearCodigo_Click(object sender, EventArgs e)
        {
            try
            {
                int numeroA1 = new Random().Next(100, 9999);
                int numeroA2 = new Random().Next(100, 9999);
                int max = _producto.Id;
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
                proveedor.editarprod = this;
                proveedor.MdiParent = this.MdiParent;

                proveedor.Show();
            }

            cbProveedor.Enabled = true;
        }

        private void btnNuevaMarca_Click(object sender, EventArgs e)
        {
            cbMarca.Enabled = false;

            if (Application.OpenForms["NuevaMarca"] == null)
            {
                NuevaMarca marca = new NuevaMarca();
                marca.editarprod = this;
                marca.MdiParent = this.MdiParent;
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
            ListarDetallePrecios det = new ListarDetallePrecios();
            det.Escala = "E" + escalaMin++;
            lista1.Add(det);
            CargarDGVPrecios(lista1, dgvMinorista);
            dgvMinorista.ClearSelection();
        }

        private void btnAddRev_Click(object sender, EventArgs e)
        {
            contarEscalas(2);
            ListarDetallePrecios det = new ListarDetallePrecios();
            det.Escala = "E" + escalaRev++;
            lista2.Add(det);
            CargarDGVPrecios(lista2, dgvRevendedor);
            dgvRevendedor.ClearSelection();
        }

        private void btnAddEsp_Click(object sender, EventArgs e)
        {
            contarEscalas(3);
            ListarDetallePrecios det = new ListarDetallePrecios();
            det.Escala = "E" + escalaEsp++;
            lista3.Add(det);
            CargarDGVPrecios(lista3, dgvEspecial);
            dgvEspecial.ClearSelection();
        }

        private void btnAddGob_Click(object sender, EventArgs e)
        {
            contarEscalas(4);
            ListarDetallePrecios det = new ListarDetallePrecios();
            det.Escala = "E" + escalaGob++;
            lista4.Add(det);
            CargarDGVPrecios(lista4, dgvGobierno);
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

        private DetallePrecio GetModelDetallePrecio(ListarDetallePrecios list)
        {
            var detalle = new DetallePrecio()
            {
                Id = list.Id,
                TiposId = list.TiposId,
                TipoPrecioId = list.TipoPrecioId,
                Escala = list.Escala,
                RangoInicio = list.RangoInicio,
                RangoFinal = list.RangoFinal,
                Precio = list.Precio
            };
            return detalle;
        }

        private void dgvMinorista_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DeleteRowEscala(0);
            var filaeliminada = dgvMinorista.CurrentRow;
            var filaActualEliminada = (ListarDetallePrecios)dgvMinorista.CurrentRow.DataBoundItem;
            _listaPreciosDel.Add(filaActualEliminada);
            
        }

        private void dgvRevendedor_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {            
            var filaeliminada = dgvRevendedor.CurrentRow;
            var filaActualEliminada = (ListarDetallePrecios)dgvRevendedor.CurrentRow.DataBoundItem;
            _listaPreciosDel.Add(filaActualEliminada);
            DeleteRowEscala(1);
        }

        private void dgvEspecial_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {            
            var filaeliminada = dgvEspecial.CurrentRow;
            var filaActualEliminada = (ListarDetallePrecios)dgvEspecial.CurrentRow.DataBoundItem;
            _listaPreciosDel.Add(filaActualEliminada);
            DeleteRowEscala(2);
        }

        private void dgvGobierno_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {            
            var filaeliminada = dgvGobierno.CurrentRow;
            var filaActualEliminada = (ListarDetallePrecios)dgvGobierno.CurrentRow.DataBoundItem;
            _listaPreciosDel.Add(filaActualEliminada);
            DeleteRowEscala(3);
        }

        private void AgregarAEliminados(DataGridViewRow datag) 
        {
            var _guid = datag.Cells[0].Value;
            if (_guid != null && _guid.ToString() != "")
            {
                DetallePrecio detalle = _tipoPrecioRepository.GetDetalle((System.Guid)_guid);
                //_listaPreciosDel.Add(detalle);
            }            
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var producto = _productosRepository.Get(_producto.Id);
                var GetProducto = GetviewModel(producto);

                if (checkColor.Checked == true)
                {
                    GetProducto.TieneColor = true;
                    guardarColores();
                }

                if (checkTalla.Checked == true)
                {
                    GetProducto.TieneTalla = true;
                    guardarTallas();
                }

                if (checkNumero.Checked == true)
                {
                    GetProducto.TieneTomoEdicion = true;
                    guardarNumeros();
                }

                if (!ModelState.IsValid(GetProducto)) return;
                _productosRepository.Update(GetProducto);

                if (checkEscala.Checked)
                {
                    GuardarPrecios(GetProducto);
                }
                
                ListarProductos prod = _productosRepository.GetListaProducto(GetProducto.Id);
                //if (prod != null)
                //{
                //    _listaProductos.RefrescaProductoGrid(indiceProducto, prod);
                //}
                //else
                //{
                    _listaProductos.RefrescarDataGridProductos(true);
                //}
                CerrarVentanas();
                Close();
            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show(ex.Message);
            }
        }

        private void checkcolorPropertis()
        {
            if (_listaColores == null)
            {
                _listaColores = new List<DetalleColor>();
            }

            if (validarListadosDetalle(2))
            {
                if (checkColor.Checked == true)
                {
                    if (Application.OpenForms["AgregarColorEdit"] == null)
                    {
                        AgregarColorEdit agregarColor = new AgregarColorEdit(this, _listaColores);
                        agregarColor.stockValidar = stockValidar;
                        agregarColor.Show();
                    }
                    else { Application.OpenForms["AgregarColorEdit"].Activate(); }
                }
                else
                {
                    _listaColores.Clear();
                }
            }            
        }

        public void guardarColores()
        {
            if (_listaColores.Count > 0)
            {
                foreach (DetalleColor dc in _listaColores)
                {
                    if (!ModelState.IsValid(dc)) return;
                    var detalleColor = _detalleColor.GetDetalleColor(dc.Id);
                    if (detalleColor != null)
                    {
                        detalleColor.Stock = dc.Stock;
                        _detalleColor.Update(detalleColor);
                    }
                    else
                    {
                        dc.ProductoId = _producto.Id;
                        _detalleColor.Add(dc);
                    }
                }
            }
            eliminarColores();

        }

        private void eliminarColores()
        {
            if (_listacoloresDel.Count > 0)
            {
                foreach (DetalleColor dc in _listacoloresDel)
                {
                    if (dc.Id > 0)
                    {
                        if (!ModelState.IsValid(dc)) return;
                        _detalleColor.DeleteDetalleColor(dc);
                    }                    
                }
            }
        }

        private void checktallaPropertis()
        {
            if (_listaTallas == null)
            {
                _listaTallas = new List<DetalleTalla>();
            }
            if (validarListadosDetalle(3))
            {
                if (checkTalla.Checked == true)
                {

                    if (Application.OpenForms["AgregarTallaEdit"] == null)
                    {
                        AgregarTallaEdit agregarTalla = new AgregarTallaEdit(this, _listaTallas);
                        agregarTalla.stockValidar = stockValidar;
                        agregarTalla.Show();
                    }
                    else { Application.OpenForms["AgregarTallaEdit"].Activate(); }
                }
                else
                {
                    _listaTallas.Clear();
                }
            }            
        }

        public void guardarTallas()
        {
            if (_listaTallas.Count > 0)
            {
                foreach (DetalleTalla dt in _listaTallas)
                {
                    if (!ModelState.IsValid(dt)) return;
                    var detalleTalla = _detalleTalla.GetDetalleTalla(dt.Id);
                    if (detalleTalla != null)
                    {
                        detalleTalla.Stock = dt.Stock;
                        _detalleTalla.Update(detalleTalla);
                    }
                    else
                    {
                        dt.ProductoId = _producto.Id;
                        _detalleTalla.Add(dt);
                    }
                }
            }
            eliminarTallas();
        }

        private void eliminarTallas() 
        {
            if (_listaTallasDel.Count > 0)
            {
                foreach (DetalleTalla dt in _listaTallasDel)
                {
                    if (dt.Id > 0)
                    {
                        if (!ModelState.IsValid(dt)) return;
                        _detalleTalla.DeleteDetalleTalla(dt);
                    }                    
                }
            }
        }

        private void checknumeroProperties()
        {
            if (txtCodigoBarras.Text == "")
            {
                KryptonMessageBox.Show("¡Debe ingresar un codigo de referencia para continuar!");
                return;
            }

            if (_listaTomoEdicion == null)
            {
                _listaTomoEdicion = new List<DetalleTomoEdicion>();
            }

            if (validarListadosDetalle(1))
            {
                if (checkNumero.Checked == true)
                {
                    if (Application.OpenForms["AgregarNumeroEdit"] == null)
                    {
                        codigobarra = txtCodigoBarras.Text;
                        AgregarNumeroEdit AgregarNumero = new AgregarNumeroEdit(this, _listaTomoEdicion.ToList());
                        AgregarNumero.stockValidar = stockValidar;
                        AgregarNumero.Show();
                    }
                    else { Application.OpenForms["AgregarNumeroEdit"].Activate(); }
                }
                else
                {
                    _listaTomoEdicion.Clear();
                }
            }   
        }

        public void guardarNumeros()
        {
            try
            {
                if (_listaTomoEdicion.Count > 0)
                {
                    foreach (DetalleTomoEdicion dt in _listaTomoEdicion)
                    {
                        if (!ModelState.IsValid(dt)) return;
                        var detalleTE = _detalleTomoEdicion.GetDetalleTE(dt.Id);
                        if (detalleTE != null)
                        {
                            detalleTE.Stock = dt.Stock;
                            _detalleTomoEdicion.Update(detalleTE);
                        }
                        else
                        {
                            dt.Codigo = _producto.CodigoBarras;
                            _detalleTomoEdicion.Add(dt);
                        }
                    }
                }
                eliminarNumeros();
            }
            catch (Exception ex)
            {
                String mes = ex.Message;
            }            
        }

        private void eliminarNumeros()
        {
            if (_listaTomoEdicionDel.Count > 0)
            {
                foreach (DetalleTomoEdicion dt in _listaTomoEdicionDel)
                {
                    if (dt.Id > 0)
                    {
                        var detalletoDelete = _tomoEdicionRepository.GetDetalleTE(dt.Id);
                        if (!ModelState.IsValid(detalletoDelete)) return;
                        _detalleTomoEdicion.DeleteDetalleTE(detalletoDelete);
                    }                    
                }
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
       
        private void OcultarForm()
        {
            AgregarColorEdit formColor = null;
            AgregarTallaEdit formTalla = null;
            AgregarNumeroEdit formNumero = null;


            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(AgregarColorEdit))
                {
                    formColor = (AgregarColorEdit)frm;
                }
                if (frm.GetType() == typeof(AgregarTallaEdit))
                {
                    formTalla = (AgregarTallaEdit)frm;
                }
                if (frm.GetType() == typeof(AgregarNumeroEdit))
                {
                    formNumero = (AgregarNumeroEdit)frm;
                }
            }

            if (formColor != null)
            {
                formColor.Close();
            }
            if (formTalla != null)
            {
                formTalla.Close();
            }
            if (formNumero != null)
            {
                formNumero.Close();
            }
        }

        private void GuardarPrecios(Producto productoEdit)
        {
            try
            {
                DatagridToList();
                var listaEscalas = new List<ListarDetallePrecios>();
                var listaDetalle = new List<DetallePrecio>();

                listaEscalas = _listaPreciosLocal;

                if (listaEscalas.Count > 0)
                {
                    var encabezadoTipoprecio = _tipoPrecioRepository.Get(productoEdit.Id);

                    if (!ModelState.IsValid(encabezadoTipoprecio)) return;

                    foreach (var item in listaEscalas)
                    {
                        DetallePrecio detallePrecio = new DetallePrecio();
                        detallePrecio.Id = item.Id;
                        detallePrecio.TipoPrecioId = item.TipoPrecioId;
                        detallePrecio.Escala = item.Escala;
                        detallePrecio.Precio = item.Precio;
                        detallePrecio.RangoInicio = item.RangoInicio;
                        detallePrecio.RangoFinal = item.RangoFinal;
                        detallePrecio.TiposId = item.TiposId;
                        detallePrecio.Tipos = _tiposClienteRepository.GetTipoName(item.Tipos);
                        listaDetalle.Add(detallePrecio);
                    }

                    foreach (var detailPrecio in listaDetalle)
                    {
                        var tmp = _tipoPrecioRepository.GetDetalle(detailPrecio.Id);

                        if (tmp != null)
                        {
                            tmp.RangoInicio = detailPrecio.RangoInicio;
                            tmp.RangoFinal = detailPrecio.RangoFinal;
                            tmp.Precio = detailPrecio.Precio;
                            _tipoPrecioRepository.UpdateDetallePrecio(tmp);
                        }
                        else
                        {
                            detailPrecio.Id = Guid.NewGuid();
                            detailPrecio.TipoPrecioId = encabezadoTipoprecio.Id;
                            _tipoPrecioRepository.AddDetallePrecio(detailPrecio);
                        }
                    }
                }

                eliminarDetallePrecios(productoEdit);
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

        private List<ListarDetallePrecios> dgvTolista(List<ListarDetallePrecios> lista, DataGridView dataGrid, string cliente)
        {
            int ContadorFilaActual = 0;
            foreach (DataGridViewRow rows in dataGrid.Rows)
            {
                var fila = (ListarDetallePrecios)dataGrid.Rows[ContadorFilaActual].DataBoundItem;
                fila.Tipos = cliente;
                _listaPreciosLocal.Add(fila);                
                ContadorFilaActual++;
            }
            return lista;
        }

        //Metodos en editar producto 
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

        private void DataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            KryptonMessageBox.Show("Datos Inválidos, revise la información ingresada.", "Notificación");
        }

        private void eliminarDetallePrecios(Producto productoEdit)
        {
            try
            {
                if (_listaPreciosDel.Count > 0)
                {
                    foreach (var item in _listaPreciosDel)
                    {
                        var tmp = _tipoPrecioRepository.GetDetalle(item.Id);
                        if (tmp != null)
                        {
                            _tipoPrecioRepository.DeleteDetallePreciop(tmp);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                KryptonMessageBox.Show("Error en eliminacion de los detalle de precios.", e.Message);
            }
        }

        public bool comprobarEscala(DataGridView dataGrid, ListarDetallePrecios detallePrecio)
        {
            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                if (row.Cells[3].Value.ToString() == detallePrecio.Escala)
                {
                    return true;
                }
                                
            }

            return false;
        }


        private bool validarListadosDetalle(int check)
        {
            bool validacion = true;

            var dtomoEdiciontmp = _tomoEdicionRepository.GetListTomosEdiciones(_producto.CodigoBarras);
            var dtallastmp = _tallasRepository.GetTallaListaProducto(_producto.Id);
            var dcolortmp =  _detalleColor.GetProductoListaColor(_producto.Id);

            //validacion para eliminar la informacion de talla/color 
            if (dtomoEdiciontmp.Count > 0 && check != 1)
            {
                DialogResult dialogResult = KryptonMessageBox.Show("Se ha encontrado un listado en Talla y Color, si continua con el proceso se eliminará.\n¿Desea Continuar?", "Advertencia", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    _listaTomoEdicionDel = dtomoEdiciontmp;
                    eliminarNumeros();
                    validacion = true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    validacion = false;
                }
            }
            //validacion para eliminar la informacion de color 
            if (dcolortmp.Count > 0 && check != 2)
            {
                DialogResult dialogResult = KryptonMessageBox.Show("Se ha encontrado un listado en Color, si continua con el proceso se eliminará.\n¿Desea Continuar?", "Advertencia", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    _listacoloresDel = dcolortmp;
                    eliminarColores();
                    validacion = true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    validacion = false;
                }
            }

            //validacion para eliminar la informacion de talla
            if (dtallastmp.Count > 0 && check != 3)
            {
                DialogResult dialogResult = KryptonMessageBox.Show("Se ha encontrado un listado en Talla, si continua con el proceso se eliminará.\n¿Desea Continuar?", "Advertencia", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    _listaTallasDel = dtallastmp;
                    eliminarTallas();
                    validacion = true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    validacion = false;
                }
            }
            return validacion;
        }

        private void CerrarVentanas()
        {
            if (Application.OpenForms["AgregarColorEdit"] != null)
            {
                Application.OpenForms["AgregarColorEdit"].Close();
            }
            if (Application.OpenForms["AgregarTallaEdit"] != null)
            {
                Application.OpenForms["AgregarTallaEdit"].Close();
            }
            if (Application.OpenForms["AgregarNumeroEdit"] != null)
            {
                Application.OpenForms["AgregarNumeroEdit"].Close();
            }
            if (Application.OpenForms["NuevoProveedor"] != null)
            {
                Application.OpenForms["NuevoProveedor"].Close();
            }
            if (Application.OpenForms["NuevaMarca"] != null)
            {
                Application.OpenForms["NuevaMarca"].Close();
            }
        }

       


    }
}
