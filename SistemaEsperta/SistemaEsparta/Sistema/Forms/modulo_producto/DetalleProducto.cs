using CapaDatos.Data;
using CapaDatos.ListasPersonalizadas;
using CapaDatos.Models.Precios;
using CapaDatos.Models.Productos;
using CapaDatos.Repository;
using CapaDatos.Repository.PreciosRepository;
using CapaDatos.Validation;
using ComponentFactory.Krypton.Toolkit;
using sharedDatabase.Models;
using Sistema.Forms.Extras;
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

namespace Sistema.Forms.modulo_producto
{
    public partial class DetalleProducto : BaseContext

    {
        private ProductosRepository _productosRepository = null;
        private readonly ProveedoresRepository _proveedoresRepository = null;
        private readonly ClientesRepository _clientesRepository = null;
        private ColoresRepository _detalleColor = null;
        private readonly TipoPrecioRepository _tipoPrecioRepository = null;
        private readonly TiposClienteRepository _tiposClienteRepository = null;
        private TallasRepository _tallasRepository = null;
        private TallasyColoresRepository _tallasyColoresRepository = null;
        private PreciosDetallePepsRepository _detallePreciosRepository = null;
        // variables

        public int stockstockToValidar = 0;
        int ingreso;
        byte[] filefoto = null;
        public Producto _producto = null;
        //listas
        private List<ListarDetallePrecios> lista1 = null, lista2 = null, lista3 = null, lista4 = null, lista5 = null, listaPrecioDel = null;
        private List<DetallePrecio> _listaPreciosDel = new List<DetallePrecio>();
        private readonly ListaProductos _listaProductos;
        //colores tallas y ambos

        public List<DetalleColor> _listacolores = new List<DetalleColor>();
        public List<DetalleColor> _listacoloresProd = new List<DetalleColor>();
        public List<DetalleColor> _listacoloresDel = new List<DetalleColor>();
        public List<DetalleTalla> _listaTallas = new List<DetalleTalla>();
        public List<DetalleTalla> _listaTallasProd = new List<DetalleTalla>();
        public List<DetalleTalla> _listaTallasDel = new List<DetalleTalla>();
        public List<DetalleColorTalla> _listaColorTallas = new List<DetalleColorTalla>();
        public List<DetalleColorTalla> _listaColorTallasDel = new List<DetalleColorTalla>();
        private int _idProducto = 0;

        public DetalleProducto(Producto producto, ListaProductos listaProductos)
        {
            _listaProductos = listaProductos;
            _producto = producto;
            _detalleColor = new ColoresRepository(_context);

            _productosRepository = new ProductosRepository(_context);
            _tipoPrecioRepository = new TipoPrecioRepository(_context);
            _clientesRepository = new ClientesRepository(_context);
            _proveedoresRepository = new ProveedoresRepository(_context);
            _tiposClienteRepository = new TiposClienteRepository(_context);
            _tallasyColoresRepository = new TallasyColoresRepository(_context); //
            _tallasRepository = new TallasRepository(_context);
            _detalleColor = new ColoresRepository(_context);
            _detallePreciosRepository = new PreciosDetallePepsRepository(_context);

            lista1 = new List<ListarDetallePrecios>();
            lista2 = new List<ListarDetallePrecios>();
            lista3 = new List<ListarDetallePrecios>();
            lista4 = new List<ListarDetallePrecios>();
            lista5 = new List<ListarDetallePrecios>();
            listaPrecioDel = new List<ListarDetallePrecios>();
            InitializeComponent();
            cargarcombos();
        }

        public void CargarTextBoxs()
        {
            try
            {
                _idProducto = _producto.Id;
                txtnombredelprod.Text = _producto.Descripcion;
                puedeservendido.Checked = _producto.PermitirVenta;
                puedesercmprado.Checked = _producto.PermitirCompra;
                controlarstock.Checked = _producto.StockControl;
                checkColor.Checked = _producto.TieneColor;
                ubicaciontxt.Text = _producto.Ubicacion;
                codigoreferencia.Text = _producto.CodigoBarras;
                precioventatxt.Text = _producto.PrecioVenta.ToString();
                costetxt.Text = _producto.Coste.ToString();
                stockproducto.Text = _producto.Stock.ToString();
                notasinternas.Text = _producto.Notas;
                labelFechaingreso.Text = _producto.FechaIngreso.ToString();
                txtmayorista.Text = _producto.PrecioMayorista.ToString();
                txtcuentaclave.Text = _producto.PrecioCuentaClave.ToString();
                txtentidad.Text = _producto.PrecioEntidadGubernamental.ToString();
                txtrevendedor.Text = _producto.PrecioRevendedor.ToString();
                stockstockToValidar = _producto.Stock;
                if (_producto.TieneEscalas)
                {
                    checkEscala.Checked = true;
                    OcultarGrupos();
                }
                TraerEscalasPrecios();
                if (_producto.TieneColor == true && _producto.TieneTalla == false)
                {
                    TraerColor();
                }
                else if (_producto.TieneColor == false && _producto.TieneTalla == true)
                {
                    TraerTalla();
                }
                else if (_producto.TieneColor == true && _producto.TieneTalla == true)
                {
                    TraerTallayColor();
                }


            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show(ex.Message);
            }
        }
        public void TraerColor()
        {
            if (_detalleColor.GetColor(_producto.Id) != null)
            {
                var productodetalleColor = _detalleColor.GetProductoListaColor(_producto.Id);
                stockproducto.Text = productodetalleColor.Sum(x => x.Stock).ToString();
                _listacolores = productodetalleColor;
            }
        }
        public void TraerTalla()
        {
            if (_tallasRepository.GetTallaListaProducto(_producto.Id) != null)
            {
                var productodetalleTalla = _tallasRepository.GetTallaListaProducto(_producto.Id);
                stockproducto.Text = productodetalleTalla.Sum(x => x.Stock).ToString();
                _listaTallas = productodetalleTalla;
            }
        }
        public void TraerTallayColor()
        {
            if (_tallasyColoresRepository.GetTallaColorListaProducto(_producto.Id) != null)
            {
                var listatallaycolorbyproduc = _tallasyColoresRepository.GetTallaColorListaProducto(_producto.Id);
                stockproducto.Text = listatallaycolorbyproduc.Sum(x => x.Stock).ToString();
                _listaColorTallas = listatallaycolorbyproduc;
            }
        }

        private void TraerEscalasPrecios()
        {
            if (_producto.TieneEscalas != false)
            {
                var tipo = _tipoPrecioRepository.Get(_producto.Id);
                if (tipo != null)
                {
                    var detalleprecioslista = _tipoPrecioRepository.GetDetallePrecioListar(tipo.Id, 0);
                    if (detalleprecioslista != null)
                    {
                        foreach (var item in detalleprecioslista)
                        {
                            var tipoObtendio = _tiposClienteRepository.GetTipo(item.TiposId);
                            if (tipoObtendio.TipoCliente == "Mayorista")
                            {
                                lista1.Add(item);
                            }
                            if (tipoObtendio.TipoCliente == "Minorista")
                            {
                                lista2.Add(item);
                            }
                            if (tipoObtendio.TipoCliente == "Cuenta Clave")
                            {
                                lista3.Add(item);
                            }
                            if (tipoObtendio.TipoCliente == "Revendedor")
                            {
                                lista4.Add(item);
                            }
                            if (tipoObtendio.TipoCliente == "Gubernamental")
                            {
                                lista5.Add(item);
                            }
                        }

                        cargarDGVPrecios(lista1, dgvmayorista);
                        cargarDGVPrecios(lista2, Dgvunitario);
                        cargarDGVPrecios(lista3, dgvcuentaclave);
                        cargarDGVPrecios(lista4, dgvrevendedor);
                        cargarDGVPrecios(lista5, dgvgubernamental);

                    }


                }

            }

        }
        private void cargarDGVPrecios(List<ListarDetallePrecios> lista, DataGridView datag)
        {
            BindingSource source = new BindingSource();

            source.DataSource = lista;
            datag.DataSource = typeof(List<>);
            datag.DataSource = source;
            datag.AutoResizeColumns();
            datag.Columns[0].Visible = false;
            datag.Columns[1].Visible = false;

        }

        private void cargarcombos()
        {

            OcultarMovimiento();
            OcultarAlertaStock();
            CargarEscala();
            CargarTipos();
            //OcultarGrupos();
            Cargarproveedores();
        }

        private void cargarCategoria()
        {

            categoriaprod.DataSource = _productosRepository.GetCategoriasList();
            categoriaprod.DisplayMember = "Nombre";
            categoriaprod.ValueMember = "Id";
            categoriaprod.SelectedValue = _producto.CategoriaId;
            categoriaprod.Invalidate();


        }

        private void cargarImg()
        {
            if (_producto.Imagen == null) { return; }
            byte[] imageData = _producto.Imagen;
            MemoryStream mStream = new MemoryStream(imageData);
            pBox.Image = Image.FromStream(mStream);
        }

        private void DetalleProducto_Load(object sender, EventArgs e)
        {
            CargarTextBoxs();
            cargarCategoria();
            Ocultarcolor();
            cargarImg();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var producto = _productosRepository.Get(_producto.Id);

                var GetProducto = GetModelProducto(producto);

                if (checkColor.Checked == true)
                {
                    guardarColores();
                }

                if (checktalla.Checked == true)
                {
                    guardarTallas();
                }

                if (rdcolorytalla.Checked == true)
                {
                    guardarColoresTallas();
                }

                

                if (!ModelState.IsValid(GetProducto)) return;
                _productosRepository.Update(GetProducto);

                GuardarTipoyDetalles(GetProducto);

                _listaProductos.RefrescarDataGridProductos(true);

                Close();
            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show(ex.Message);
            }
        }



        public void guardarColores()
        {
            if (_listacoloresProd.Count > 0)
            {
                foreach (DetalleColor dc in _listacoloresProd)
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
                        _detalleColor.Add(dc);
                    }
                }
            }
            eliminarColores();
            
        }

        public void guardarTallas() {
            if (_listaTallasProd.Count > 0)
            {
                foreach (DetalleTalla dt in _listaTallasProd)
                {
                    if (!ModelState.IsValid(dt)) return;
                    var detalleTalla = _tallasRepository.GetDetalleTalla(dt.Id);
                    if (detalleTalla != null)
                    {
                        detalleTalla.Stock = dt.Stock;
                        _tallasRepository.Update(detalleTalla);
                    }
                    else
                    {
                        _tallasRepository.Add(dt);
                    }
                }
            }
            eliminarTallas();
        }

        public void guardarColoresTallas()
        {

            if (_listaColorTallas.Count > 0)
            {
                foreach (DetalleColorTalla dct in _listaColorTallas)
                {
                    if (!ModelState.IsValid(dct)) return;
                    var detalleColorTalla = _tallasyColoresRepository.GetColorTalla(dct.Id);
                    if (detalleColorTalla != null)
                    {
                        detalleColorTalla.Stock = dct.Stock;
                        _tallasyColoresRepository.Update(detalleColorTalla);
                    }
                    else
                    {
                        _tallasyColoresRepository.Add(dct);
                    }
                }
            }
            eliminarColoresTallas();
        }



        private void eliminarColores() 
        {
            if (_listacoloresDel.Count > 0)
            {
                foreach (DetalleColor dc in _listacoloresDel)
                {
                    if (!ModelState.IsValid(dc)) return;
                    _detalleColor.DeleteDetalleColor(dc);
                }
            }
        }

        private void eliminarTallas() 
        {
            if (_listaTallasDel.Count > 0)
            {
                foreach (DetalleTalla dt in _listaTallasDel)
                {
                    if (!ModelState.IsValid(dt)) return;
                    _tallasRepository.DeleteDetalleTalla(dt);
                }
            }
        }

        private void eliminarColoresTallas()
        {
            if (_listaColorTallasDel.Count > 0)
            {
                foreach (DetalleColorTalla dt in _listaColorTallasDel)
                {
                    if (!ModelState.IsValid(dt)) return;
                    _tallasyColoresRepository.DeleteDetalleTallaColor(dt);
                }
            }
        }



        public Producto GetModelProducto(Producto producto)
        {

            producto.CodigoBarras = codigoreferencia.Text;
            producto.CategoriaId = Convert.ToInt32(categoriaprod.SelectedValue.ToString());
            producto.Descripcion = txtnombredelprod.Text;
            producto.PermitirVenta = puedeservendido.Checked;
            producto.PermitirCompra = puedesercmprado.Checked;
            producto.StockControl = controlarstock.Checked;
            producto.Ubicacion = ubicaciontxt.Text;
            producto.PrecioVenta = Convert.ToDecimal(precioventatxt.Text);
            producto.Coste = Convert.ToDecimal(costetxt.Text);
            producto.Stock = Convert.ToInt32(stockproducto.Text);
            producto.Notas = notasinternas.Text;
            producto.PrecioRevendedor = Convert.ToDecimal(txtrevendedor.Text);
            producto.PrecioMayorista = Convert.ToDecimal(txtmayorista.Text);
            producto.PrecioEntidadGubernamental = Convert.ToDecimal(txtentidad.Text);
            producto.PrecioCuentaClave = Convert.ToDecimal(txtcuentaclave.Text);
            producto.FechaModificacion = DateTime.Now;
            producto.Imagen = filefoto;
            return producto;
        }

        private DetalleColor GetModelColor(DetalleColor detallecolor)
        {
            // detallecolor.ProductoId = _producto.Id;
            detallecolor.Stock = int.Parse(stockproducto.Text);
            //detallecolor.Color = txtColor.Text;

            return detallecolor;
        }

        private DetalleColor Getmodelo()
        {

            var DetalleColor = new DetalleColor()
            {
                ProductoId = _producto.Id,
                //Color = txtColor.Text,
                Stock = int.Parse(stockproducto.Text),

            };
            return DetalleColor;
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

        private void Cargarproveedores()
        {
            try
            {
                cbproveedor.DataSource = _proveedoresRepository.GetList();
                cbproveedor.DisplayMember = "Nombre";
                cbproveedor.ValueMember = "Id";
                cbproveedor.Invalidate();
            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show("no hay ningun proveedor,deberá ingresar uno", ex.Message);
            }

        }



        private void OcultarForm()
        {
            AgregarColorEdit formColor = null;
            NuevaMarca formTalla = null;
            //AgregarColorTallaEdit formColorTalla = null;


            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(AgregarColorEdit))
                {
                    formColor = (AgregarColorEdit)frm;
                }
                if (frm.GetType() == typeof(NuevaMarca))
                {
                    formTalla = (NuevaMarca)frm;
                }
                //if (frm.GetType() == typeof(AgregarColorTallaEdit))
                //{
                //    formColorTalla = (AgregarColorTallaEdit)frm;
                //}
            }

            if (formColor != null)
            {
                formColor.Close();
            }
            if (formTalla != null)
            {
                formTalla.Close();
            }
            //if (formColorTalla != null)
            //{
            //    formColorTalla.Close();
            //}
        }

        private void Ocultarcolor()
        {
            //lbColor.Visible = false;
            //txtColor.Visible = false;
            //txtColor.Text = "";
        }

        private void OcultarAlertaStock()
        {

            lbcantidadmin.Visible = false;
            txtcantidadmin.Visible = false;

        }

        private void OcultarMovimiento()
        {
            rbcuatro.Visible = false;
            rbochosem.Visible = false;
            rbpersonal.Visible = false;
            Ocultartiempos();
        }

        private void OcultarGrupos()
        {
            groupEscala.Visible = false;
            btnAddPrecio.Visible = false;
        }

        private void Ocultartiempos()
        {
            dtpfechafinal.Visible = false;
            dtpfechainicio.Visible = false;
            lbfechafinal.Visible = false;
            lbfechainicio.Visible = false;

        }



        private void VerTiempos()
        {
            dtpfechafinal.Visible = true;
            dtpfechainicio.Visible = true;
            lbfechafinal.Visible = true;
            lbfechainicio.Visible = true;

        }

        private void VerMovimiento()
        {
            rbcuatro.Visible = true;
            rbochosem.Visible = true;
            rbpersonal.Visible = true;

        }

        private void CargarTipos()
        {
            var tipos = _clientesRepository.GetTipos();

            cbTiposClientes.DataSource = tipos;
            cbTiposClientes.DisplayMember = "TipoCliente";
            cbTiposClientes.ValueMember = "Id";
            //cbsucursales.Text = "Seleccione una Sucursal"; 
            cbTiposClientes.SelectedIndex = 0;
            cbTiposClientes.Invalidate();
        }

        private void CargarEscala()
        {
            cbEscala.Items.Insert(0, "E1");
            cbEscala.Items.Insert(1, "E2");
            cbEscala.Items.Insert(2, "E3");
            cbEscala.Items.Insert(3, "E4");
            cbEscala.SelectedIndex = 0;

        }

        private ListarDetallePrecios GetDetallePreciosModel()
        {

            var DetallePrecios = new ListarDetallePrecios()
            {
                Id = Guid.NewGuid(),
                Escala = cbEscala.Text,
                Precio = decimal.Parse(txtprecioVar.Text),
                Tipos = cbTiposClientes.Text, //
                TiposId = int.Parse(cbTiposClientes.SelectedValue.ToString()),
                RangoFinal = int.Parse(txtRangoFinal.Text),
                RangoInicio = int.Parse(txtRangoInic.Text),

            };
            return DetallePrecios;
        }

        public void RefrescarDetallePrecios(DataGridView data, List<ListarDetallePrecios> lista)
        {

            BindingSource source = new BindingSource();

            source.DataSource = lista;
            data.DataSource = typeof(List<>);
            data.DataSource = source;
            data.AutoResizeColumns();
            data.Columns[0].Visible = false;
            data.Columns[1].Visible = false;

        }



        private void checkcolorPropertis()
        {
            if (string.IsNullOrEmpty(stockproducto.Text) || int.Parse(stockproducto.Text) <= 0)
            {
                if (ingreso == 0)
                {
                    KryptonMessageBox.Show("Por favor ingrese el stock, antes de continuar..");
                    ingreso += 1;
                    checkColor.Checked = false;
                    return;
                }
                checkColor.Checked = false;
            }

            bool validacion = validarListadosDetalle(2);
            
            if (checkColor.Checked == true && validacion == true)
            {
                ingreso = 0;
                if (_listacolores == null)
                {
                    _listacolores = new List<DetalleColor>();
                }
                if (Application.OpenForms["AgregarColorEdit"] == null)
                {
                    _listacolores = _detalleColor.GetProductoListaColor(_producto.Id);
                    //AgregarColorEdit agregarColor = new AgregarColorEdit(this, _listacolores);
                    //agregarColor.Show();
                }
                else { Application.OpenForms["AgregarColorEdit"].Activate(); }
            }
            else
            {
                _listacolores.Clear();
            }
        }

        private void checktallaPropertis()
        {
            if (string.IsNullOrEmpty(stockproducto.Text) || int.Parse(stockproducto.Text) <= 0)
            {
                if (ingreso == 0)
                {
                    KryptonMessageBox.Show("Por favor ingrese el stock, antes de continuar..");
                    ingreso += 1;
                    checktalla.Checked = false;

                    return;
                }
                checktalla.Checked = false;
            }

            bool validacion = validarListadosDetalle(3);
            
            if (checktalla.Checked == true && validacion == true)
            {
                ingreso = 0;
                if (_listaTallas == null)
                {
                    _listaTallas = new List<DetalleTalla>();
                }
                if (Application.OpenForms["AgregarTalla"] == null)
                {
                    //_listaTallas = _tallasRepository.GetTallaListaProducto(_producto.Id);
                    //NuevaMarca agregarTalla = new NuevaMarca(this, _listaTallas);
                    //agregarTalla.Show();
                }
                else { Application.OpenForms["AgregarTalla"].Activate(); }
            }
            else
            {
                _listaTallas.Clear();
            }
        }

        private void checkcolorytallaProperties() 
        {
            if (string.IsNullOrEmpty(stockproducto.Text) || int.Parse(stockproducto.Text) <= 0)
            {
                if (ingreso == 0)
                {
                    KryptonMessageBox.Show("Por favor ingrese el stock, antes de continuar..");
                    ingreso += 1;
                    checkColor.Checked = false;
                    return;
                }
                checkColor.Checked = false;
            }

            bool validacion = validarListadosDetalle(1);
           
            if (rdcolorytalla.Checked == true && validacion == true)
            {
                ingreso = 0;
                if (_listaColorTallas == null)
                {
                    _listaColorTallas = new List<DetalleColorTalla>();
                }

                if (Application.OpenForms["AgregarColorTallaEdit"] == null)
                {
                    _listaColorTallas = _tallasyColoresRepository.GetTallaColorListaProducto(_producto.Id);
                    //AgregarColorTallaEdit agregarColorTalla = new AgregarColorTallaEdit(this, _listaColorTallas);
                    //agregarColorTalla.Show();
                }
                else 
                { Application.OpenForms["AgregarColorTallaEdit"].Activate(); }
            }
            else
            {
                _listaColorTallas.Clear();
            }
        }



        private void checkColor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkColor.Checked == true)
            {
                checkcolorPropertis();
            }
            else 
            {
                OcultarForm();
                
            }
            
        }

        private void checktalla_CheckedChanged(object sender, EventArgs e)
        {
            if (checktalla.Checked == true)
            {
                checktallaPropertis();
            }
            else
            {
                OcultarForm();
            }
        }

        private void rdcolorytalla_CheckedChanged(object sender, EventArgs e)
        {
            if (rdcolorytalla.Checked == true)
            {
                checkcolorytallaProperties();
            }
            else
            {
                OcultarForm();
            }
        }

        private void checkEscala_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEscala.Checked == false)
            {
                groupEscala.Visible = false;
                groupPrecios.Visible = true;
                btnAddPrecio.Visible = false;

            }
            else
            {
                groupPrecios.Visible = false;
                groupEscala.Visible = true;
                btnAddPrecio.Visible = true;

            }
        }       



        private void lbLimpiar_Paint(object sender, PaintEventArgs e)
        {

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

        private void btnAddPrecio_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtprecioVar.Text) || decimal.Parse(txtprecioVar.Text) <= 0)
            {
                KryptonMessageBox.Show("Debe ingresar un Precio valido para continuar");
                return;
            }
            if (cbTiposClientes.Text == "Minorista")
            {
                var Detalle1 = GetDetallePreciosModel();
                if (!comprobarEscala(Dgvunitario, Detalle1))
                {
                    obtenerPrecioIngresado(lista2, Detalle1);
                    RefrescarDetallePrecios(Dgvunitario, lista2);
                }
            }
            if (cbTiposClientes.Text == "Mayorista")
            {
                var Detalle2 = GetDetallePreciosModel();
                if (!comprobarEscala(dgvmayorista, Detalle2))
                {
                    obtenerPrecioIngresado(lista1, Detalle2);
                    RefrescarDetallePrecios(dgvmayorista, lista1);
                }


            }
            if (cbTiposClientes.Text == "Cuenta Clave")
            {
                var Detalle3 = GetDetallePreciosModel();
                if (!comprobarEscala(dgvcuentaclave, Detalle3))
                {
                    obtenerPrecioIngresado(lista3, Detalle3);
                    RefrescarDetallePrecios(dgvcuentaclave, lista3);
                }


            }
            if (cbTiposClientes.Text == "Revendedor")
            {
                var Detalle4 = GetDetallePreciosModel();
                if (!comprobarEscala(dgvrevendedor, Detalle4))
                {
                    obtenerPrecioIngresado(lista4, Detalle4);
                    RefrescarDetallePrecios(dgvrevendedor, lista4);
                }
            }
            if (cbTiposClientes.Text == "Gubernamental")
            {
                var Detalle5 = GetDetallePreciosModel();
                if (!comprobarEscala(dgvgubernamental, Detalle5))
                {
                    obtenerPrecioIngresado(lista5, Detalle5);
                    RefrescarDetallePrecios(dgvgubernamental, lista5);
                }
            }
        }

        private void obtenerPrecioIngresado(List<ListarDetallePrecios> listado, ListarDetallePrecios detalleP)
        {
            if (listaPrecioDel.Count > 0)
            {
                var tmp = listaPrecioDel.Find(x => x.Id == detalleP.Id);
                if (tmp != null)
                {
                    tmp.Precio = detalleP.Precio;
                    tmp.RangoInicio = detalleP.RangoFinal;
                    tmp.RangoFinal = detalleP.RangoFinal;
                    listado.Add(tmp);
                }
                else
                {
                    listado.Add(detalleP);
                }
            }
            else 
            {
                listado.Add(detalleP);
            }
        }

        private void cbEscala_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEscala.SelectedIndex == 0)
            {
                txtRangoInic.Text = "1";
                txtRangoFinal.Text = "11";

            }
            if (cbEscala.SelectedIndex == 1)
            {
                txtRangoInic.Text = "12";
                txtRangoFinal.Text = "49";

            }
            if (cbEscala.SelectedIndex == 2)
            {
                txtRangoInic.Text = "50";
                txtRangoFinal.Text = "1000";

            }
            if (cbEscala.SelectedIndex == 3)
            {
                txtRangoInic.Text = "1001";
                txtRangoFinal.Text = "10000";

            }

        }

        private bool validarListadosDetalle(int check)
        {
            bool validacion = true;

            var dtallacolortmp = _tallasyColoresRepository.GetTallaColorListaProducto(_producto.Id);
            var dtallastmp = _tallasRepository.GetTallaListaProducto(_producto.Id);
            var dcolortmp = _listacolores = _detalleColor.GetProductoListaColor(_producto.Id);

            //validacion para eliminar la informacion de talla/color 
            if (dtallacolortmp.Count > 0 && check != 1)
            {
                DialogResult dialogResult = KryptonMessageBox.Show("Se ha encontrado un listado en Talla y Color, si continua con el proceso se eliminará.\n¿Desea Continuar?", "Advertencia", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    _listaColorTallasDel = dtallacolortmp;
                    eliminarColoresTallas();
                    validacion = true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    validacion = false;
                }
            }
            //validacion para eliminar la informacion de color 
            if (dcolortmp.Count > 0  && check != 2)
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

        private void GuardarTipoyDetalles(Producto productoEdit)
        {
            try
            {
                DatagridToList();
                var listaEscalas = new List<ListarDetallePrecios>();
                var listaDetalle = new List<DetallePrecio>();

                listaEscalas = lista1.Concat(lista2).Concat(lista3).Concat(lista4).Concat(lista5).ToList();

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
            if (Dgvunitario.RowCount > 0)
            {
                dgvTolista(lista1, Dgvunitario);
            }
            if (dgvmayorista.RowCount > 0)
            {
                dgvTolista(lista2, dgvmayorista);
            }
            if (dgvcuentaclave.RowCount > 0)
            {
                dgvTolista(lista3, dgvcuentaclave);
            }
            if (dgvrevendedor.RowCount > 0)
            {
                dgvTolista(lista4, dgvrevendedor);
            }
            if (dgvgubernamental.RowCount > 0)
            {
                dgvTolista(lista5, dgvgubernamental);
            }
        }

        private List<ListarDetallePrecios> dgvTolista(List<ListarDetallePrecios> lista,DataGridView dataGrid)
        {
            int ContadorFilaActual = 0;
            foreach (DataGridViewRow rows in dataGrid.Rows)
            {
                var fila = (ListarDetallePrecios)dataGrid.Rows[ContadorFilaActual].DataBoundItem;

                if (!comprobarEscala(dataGrid, fila))
                {
                    lista.Add(fila);
                }
                ContadorFilaActual++;
            }
            return lista;
        }

        private void dgvEliminiarPrecio_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DataGridView dgvTemp = sender as DataGridView;

            //para obtener los valores de las celdas 
            var filaeliminada = dgvTemp.CurrentRow;

            //para agregarlo al listado de precios eliminados
            var filaActualEliminada = (ListarDetallePrecios)dgvTemp.CurrentRow.DataBoundItem;
            listaPrecioDel.Add(filaActualEliminada);

        }

        private void eliminarDetallePrecios(Producto productoEdit) 
        {
            try
            {
                if (listaPrecioDel.Count > 0)
                {
                    var listaDetalle = new List<DetallePrecio>();

                    foreach (var item in listaPrecioDel)
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

    }

}
