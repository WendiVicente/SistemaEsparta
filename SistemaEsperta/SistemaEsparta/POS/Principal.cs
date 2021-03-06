using CapaDatos.Data;
using CapaDatos.ListasPersonalizadas;
using CapaDatos.ListasPersonalizadas.VentasAcumuladas;
using CapaDatos.Models.Cotizacion;
using CapaDatos.Models.Productos;
using CapaDatos.Models.ProductosToFacturar;
using CapaDatos.Models.Usuarios;
using CapaDatos.Repository;
using CapaDatos.Repository.SolicitudestoFacturar;
using ComponentFactory.Krypton.Toolkit;
using NSubstitute;
using POS.Forms;
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

namespace POS
{
    public partial class Principal : BaseContext
    {
        private CotizacionRepository _cotizacionRepository = null;
        private PedidoRepository _pedidoRepository = null;
        private ValesRepository _valesRepository = null;
        private ProductosRepository _productosRepository = null;
        private CombosRepository _combosRepository = null;
        private ColoresRepository _coloresRepository = null;
        private TallasRepository _tallasRepository = null;
        private TallasyColoresRepository _tallasyColoresRepository = null;
        private SolicitudesRepository _solicitudesRepository = null;
        private CategoriaProdRepository _categoriaProdRepository = null;

        //listas detalles
        public List<ListarDetalleCotiz> _listadetallesCotiz = new List<ListarDetalleCotiz>();
        public List<ListarDetallePedidos> _listadetallesPedidos = new List<ListarDetallePedidos>();
        public IList<ListarCombos> _listaCombos = new List<ListarCombos>();
        public IList<ListarProductos> _listaProductos = null, listareducidaDebusquedal= null;
        public List<ListarDetalleFacturas> _listaDetalleFacturas = new List<ListarDetalleFacturas>();
        public List<SolicitudDetalle> _listaSolicitudDetalles = null;
        public List<ListarAcumuladasEncabezado> _listaSolicitud = null;
        public List<Producto> _allProductosList = null, listatotalbusqueda = null;
        //variables
        public Guid idCotizacion;
        public Guid idPedido;
        private bool selectProducto = true;
        public  ListarVales _valeSelected = null;
        //objetos creados
        public  Principal(User user)
        
        {
            UsuarioLogeadoPOS.User = user;

             InitializeComponent();
            _cotizacionRepository = new CotizacionRepository(_context);
            _pedidoRepository = new PedidoRepository(_context);
            _valesRepository = new ValesRepository(_context);
            _productosRepository = new ProductosRepository(_context);
            _combosRepository = new CombosRepository(_context);
            _coloresRepository = new ColoresRepository(_context);
            _tallasRepository = new TallasRepository(_context);
            _tallasyColoresRepository = new TallasyColoresRepository(_context);
            _solicitudesRepository = new SolicitudesRepository(_context);
            _categoriaProdRepository = new CategoriaProdRepository(_context);
            cargarUsuarioYSucursal();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            //TODO: clickl en datagrid para luego agregar el detalle en la venta, unicamente si es cajero , vendedor no
           
            cargarListaVentasPendientes();

            var cargaCombo = ComboLoad();
            var cargaProducto = ProductosLoad1();
            _allProductosList = null;//_productosRepository.GetAllProductos().Take(50).ToList();
            listatotalbusqueda = null;// _productosRepository.GetAllProductos();
            //listareducidaDebusquedal = _productosRepository.GetList(UsuarioLogeadoPOS.User.SucursalId);
            _listaCombos =  cargaCombo;
            
            cargaProductosInit();

            cargarCateroriasb();
            CargarProductosGenerales(cargaProducto);
        }
        private void cargarListaVentasPendientes()
        {
            _listaSolicitud = _solicitudesRepository.GetSolicitudesxUser(UsuarioLogeadoPOS.User.Id);
            CargarVentasSolicitadas(_listaSolicitud);
        }


       private void CargarVentasSolicitadas(List<ListarAcumuladasEncabezado> lista)
        {
            BindingSource recurso = new BindingSource();

            recurso.DataSource = lista;
            dgVentasPendientes.DataSource = typeof(List<>);
            dgVentasPendientes.DataSource = recurso;
            dgVentasPendientes.AutoResizeColumns();
            dgVentasPendientes.Columns[0].Visible = false;
            dgVentasPendientes.Columns[3].Visible = false;

        }
        private void CargarProductosGenerales(IList<ListarProductos> lista)
        {
           //var lista= _productosRepository.GetList(UsuarioLogeadoPOS.User.SucursalId);
            BindingSource recurso = new BindingSource();

            recurso.DataSource = lista;
            dgvProductosBd.DataSource = typeof(List<>);
            dgvProductosBd.DataSource = recurso;
            dgvProductosBd.AutoResizeColumns();
            _listaProductos = lista;
            dgvProductosBd.ClearSelection();
        }
        private void CargarCombosGenerales(IList<ListarCombos> lista)
        {
            //var lista= _productosRepository.GetList(UsuarioLogeadoPOS.User.SucursalId);
            BindingSource recurso = new BindingSource();

            recurso.DataSource = lista;
            dgvProductosBd.DataSource = typeof(List<>);
            dgvProductosBd.DataSource = recurso;
            dgvProductosBd.AutoResizeColumns();
           // _listaCombos = lista;
        }

        private void cargarCateroriasb()
        {
            CargarButtonsCategoria(_categoriaProdRepository.GetListcategoriaToButton());
        }
        private void cargarUsuarioYSucursal()
        {

            lbsucursal.Text = UsuarioLogeadoPOS.User.Sucursal.NombreSucursal;
            lbusuariologeado.Text = UsuarioLogeadoPOS.User.UserName;


        }
      
        private IList<ListarCombos> ComboLoad()
        {
            var combosbd = _combosRepository.GetListarCombos(UsuarioLogeadoPOS.User.SucursalId);
            return combosbd;
        }
        private IList<ListarProductos> ProductosLoad1()
        {
            var productosbd = _productosRepository.GetList(UsuarioLogeadoPOS.User.SucursalId).Take(50).ToList();
            return productosbd;
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarBD();
        }
        public void CargarBD()
        {
            tableButtonCategoria.Controls.Clear();
            cargarCateroriasb();
            var cargaProducto = ProductosLoad1();
            _listaProductos = cargaProducto;
            cargaProductosInit();
            cargarListaVentasPendientes();
        }
        public void cargarValeLabel()
        {
            if (_valeSelected != null)
            {

                lbnumeroVale.Text = _valeSelected.NoVale;
                lbvalename.Visible = true;
            }
            else
            {
                lbvalename.Visible = false;
                lbnumeroVale.Text = null;
            }

           
        }

        public ProductosRepository AccesoProductoRepository()
        {
            _context = null;
            _context = new Context();
            _productosRepository = null;
            _productosRepository = new ProductosRepository(_context);

            return _productosRepository;
        }


        public ListarDetalleFacturas GetDetalleFactura()
        {
            return new ListarDetalleFacturas()
            {


            };

        }

        public void eliminarDGVDetalleFactura(int id)
        {
            if (_listaDetalleFacturas.Count > 0)
            {
                for (int i = 0; i <= _listaDetalleFacturas.Count; i++)
                {
                    int _Id = _listaDetalleFacturas.ElementAt(i).Id;
                    if (_Id == id)
                    {
                        dgvproductosadd.Rows.RemoveAt(i);
                    }
                }
            }
        }

        public void cargarDGVDetalleFactura(List<ListarDetalleFacturas> lista)
        {
            BindingSource recurso = new BindingSource();

            recurso.DataSource = lista;
            dgvproductosadd.DataSource = typeof(List<>);
            dgvproductosadd.DataSource = recurso;
            dgvproductosadd.AutoResizeColumns();
             dgvproductosadd.Columns[0].Visible = false;
             dgvproductosadd.Columns[9].Visible = false;
             dgvproductosadd.Columns[10].Visible = false;
             dgvproductosadd.Columns[11].Visible = false;
             dgvproductosadd.Columns[12].Visible = false;
             dgvproductosadd.Columns[13].Visible = false;
             dgvproductosadd.Columns[14].Visible = false;
             dgvproductosadd.Columns[15].Visible = false;
             dgvproductosadd.Columns[16].Visible = false;
            dgvproductosadd.ClearSelection();
        }
        public List<ListarDetalleFacturas> GetSolicitudToFacturaDetalle(List<ListaVentasAcumuladas> solicitudDetalleslista)
        {
            //  var list = new List<ListarDetalleFacturas>();

            foreach (var item in solicitudDetalleslista)
            {
                if (solicitudDetalleslista == null) { continue; }
                var detalleFactura = GetDetalleFactura();
                detalleFactura.Id = (int)item.Id;
                detalleFactura.DetalleColorId = (int)item.DetalleColorId ;
                detalleFactura.DetalleTallaId = (int)item.DetalleTallaId;
                detalleFactura.TallayColorId = item.TallayColorId;
                detalleFactura.Color = item.Color;
                detalleFactura.Talla = item.Talla;
                detalleFactura.ProductoId = (int)item.ProductoId;
                detalleFactura.Descripcion = item.Descripcion;
                detalleFactura.Cantidad = item.Cantidad;
                detalleFactura.Precio = item.Precio;
                detalleFactura.Descuento = item.Descuento;
                detalleFactura.PrecioTotal = item.PrecioTotal;
                detalleFactura.SubTotal = detalleFactura.PrecioTotal;
                detalleFactura.ComboId = item.ComboId;
                _listaDetalleFacturas.Add(detalleFactura);
            }
            return _listaDetalleFacturas;

        }

        public List<ListarDetalleFacturas> GetCotizacionToFacturaDetalle()
        {
            //  var list = new List<ListarDetalleFacturas>();

            foreach (var item in _listadetallesCotiz)
            {
                if (_listadetallesCotiz == null) { continue; }
                var detalleFactura = GetDetalleFactura();
                detalleFactura.DetalleColorId = item.DetalleColorId;
                detalleFactura.DetalleTallaId = item.DetalleTallaId;
                detalleFactura.TallayColorId = item.TallayColorId;
                detalleFactura.Color = item.Color;
                detalleFactura.Talla = item.Talla;
                detalleFactura.ProductoId = (int)item.ProductoId;
                detalleFactura.Descripcion = item.Descripcion;
                detalleFactura.Cantidad = item.Cantidad;
                detalleFactura.Precio = item.Precio;
                detalleFactura.Descuento = 0;
                detalleFactura.PrecioTotal = item.Total;
                detalleFactura.SubTotal = detalleFactura.PrecioTotal;
                detalleFactura.ComboId = item.ComboId;
                _listaDetalleFacturas.Add(detalleFactura);
            }
            return _listaDetalleFacturas;

        }
        public List<ListarDetalleFacturas> GetPedidoToFacturaDetalle()
        {
            // var list = new List<ListarDetalleFacturas>();

            foreach (var item in _listadetallesPedidos)
            {
                if (_listadetallesPedidos == null) { continue; }
                var detalleFactura = GetDetalleFactura();
                detalleFactura.DetalleColorId = item.DetalleColorId;
                detalleFactura.DetalleTallaId = item.DetalleTallaId;
                detalleFactura.TallayColorId = item.TallayColorId;
                detalleFactura.Color = item.Color;
                detalleFactura.Talla = item.Talla;
                detalleFactura.ProductoId = (int)item.ProductoId;
                detalleFactura.Descripcion = item.Descripcion;
                detalleFactura.Cantidad = item.Cantidad;
                detalleFactura.Precio = item.Precio;
                detalleFactura.Descuento = 0;
                detalleFactura.PrecioTotal = item.Total;
                detalleFactura.SubTotal = detalleFactura.PrecioTotal;
                detalleFactura.ComboId = item.ComboId;
                _listaDetalleFacturas.Add(detalleFactura);
            }
            return _listaDetalleFacturas;

        }

        // TableLayoutPanel tablilla = new TableLayoutPanel();
        //private TableLayoutPanel CreateTable(PictureBox pbimg, decimal precio, Label descripcion, int notabla, string idprod)
        //{
        //    var tablilla = new TableLayoutPanel();
        //    tablilla.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        //    tablilla.ColumnCount = 1;
        //    tablilla.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        //    //tablilla.Controls.Add(pbimg, 0, 0);
        //    tablilla.Controls.Add(descripcion, 0, 1);
        //    tablilla.Controls.Add(tablaDeprecios(precio, idprod), 0, 2);
        //    tablilla.Name = notabla.ToString();
        //    tablilla.Location = new System.Drawing.Point(5, 5);
        //    tablilla.Name = notabla.ToString();
        //    tablilla.RowCount = 3;
        //    tablilla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.55102F));
        //    tablilla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.44898F));
        //    tablilla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
        //    return tablilla;
        //}
        //private TableLayoutPanel tablaDeprecios(decimal precio, string idproducto)
        //{
        //    TableLayoutPanel tablaprecios2 = new TableLayoutPanel();
        //    tablaprecios2.ColumnCount = 2;
        //    tablaprecios2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
        //    tablaprecios2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
        //    tablaprecios2.Controls.Add(Createlabelprecio(precio), 1, 0);
        //    tablaprecios2.Controls.Add(crearbuttonprecios(idproducto), 0, 0);
        //    tablaprecios2.Dock = System.Windows.Forms.DockStyle.Fill;
        //    tablaprecios2.Location = new System.Drawing.Point(3, 101);
        //    tablaprecios2.Name = "tablaprecios";
        //    tablaprecios2.RowCount = 1;
        //    tablaprecios2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
        //    tablaprecios2.Size = new System.Drawing.Size(125, 47);
        //    tablaprecios2.TabIndex = 0;
        //    return tablaprecios2;
        //}
        //private TableLayoutPanel tablaCategorias(KryptonButton botoncl)
        //{
        //    TableLayoutPanel tablaCategoriasToselect = new TableLayoutPanel();
        //    tablaCategoriasToselect.BackColor = System.Drawing.Color.White;
        //    tablaCategoriasToselect.ColumnCount = 1;
        //    tablaCategoriasToselect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        //    tablaCategoriasToselect.Controls.Add(botoncl, 0, 0);
        //    //tablaCategoriasToselect.Controls.Add(this.btnfantasia, 0, 7);
        //    //tablaCategoriasToselect.Controls.Add(this.btnjuguetes, 0, 6);
        //    //tablaCategoriasToselect.Controls.Add(this.btnfiesta, 0, 5);
        //    //tablaCategoriasToselect.Controls.Add(this.btnropa, 0, 4);
        //    //tablaCategoriasToselect.Controls.Add(this.btnzapateria, 0, 3);
        //    //tablaCategoriasToselect.Controls.Add(this.btnlibreria, 0, 2);
        //    // tablaCategoriasToselect.Controls.Add(this.label3, 0, 1); tableLayoutPanel8
        //    tablaCategoriasToselect.Dock = System.Windows.Forms.DockStyle.Top;
        //    tablaCategoriasToselect.Location = new System.Drawing.Point(2, 2);
        //    tablaCategoriasToselect.Margin = new System.Windows.Forms.Padding(2);
        //    tablaCategoriasToselect.Name = "tableButtonCategoria";
        //    tablaCategoriasToselect.RowCount = 14;
        //    tablaCategoriasToselect.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.18841F));
        //    tablaCategoriasToselect.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.81159F));
        //    tablaCategoriasToselect.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 67F));
        //    tableButtonCategoria.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));

        //    this.tableButtonCategoria.Size = new System.Drawing.Size(122, 738);
        //    this.tableButtonCategoria.TabIndex = 0;
        //    return tableButtonCategoria;
        //}
        /*
        private Label crearLabelToCategory()
        {
            Label etiquetaCat = new Label();
            etiquetaCat.AutoSize = false;
            etiquetaCat.BackColor = System.Drawing.Color.Transparent;
            etiquetaCat.Dock = System.Windows.Forms.DockStyle.Top;
            etiquetaCat.Font = new System.Drawing.Font("Arial Black", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            etiquetaCat.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            etiquetaCat.Location = new System.Drawing.Point(2, 10);
            etiquetaCat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            etiquetaCat.Name = "etiquetaCat";
            etiquetaCat.Size = new System.Drawing.Size(118, 34);
            etiquetaCat.TabIndex = 9;
            etiquetaCat.Text = "Categorias";
            return etiquetaCat;
        }
        */
        //private Label crearbuttonprecios(string idp)
        //{
        //    Label btnprecioso = new Label();
        //    btnprecioso.AutoSize = false;
        //    btnprecioso.BackColor = System.Drawing.SystemColors.ActiveCaption;
        //    btnprecioso.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        //    btnprecioso.Dock = System.Windows.Forms.DockStyle.Top;
        //    btnprecioso.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    btnprecioso.Location = new System.Drawing.Point(3, 0);
        //    btnprecioso.Name = idp;
        //    btnprecioso.Size = new System.Drawing.Size(69, 47);
        //    btnprecioso.Click += new EventHandler(verprecios_DoubleClick);
        //    btnprecioso.TabIndex = 1;
        //    btnprecioso.Text = "Ver Precios";
        //    return btnprecioso;
        //}
        //private Label CreatelabelDesrip(string descrip, string id)
        //{
        //    Label lbdescrip = new Label();
        //    lbdescrip.AutoSize = true;
        //    lbdescrip.Dock = System.Windows.Forms.DockStyle.Fill;
        //    lbdescrip.Location = new System.Drawing.Point(3, 76);
        //    lbdescrip.Name = id.ToString();
        //    lbdescrip.Size = new System.Drawing.Size(120, 22);
        //    lbdescrip.TabIndex = 1;
        //    lbdescrip.Text = descrip;
        //    return lbdescrip;
        //}
        //private Label Createlabelprecio(decimal precio)
        //{
        //    Label lbprec = new Label();
        //    lbprec.AutoSize = true;
        //    lbprec.Dock = System.Windows.Forms.DockStyle.Right;
        //    lbprec.Location = new System.Drawing.Point(87, 98);
        //    //lbprec.Name = "lbprec";
        //    lbprec.Size = new System.Drawing.Size(36, 31);
        //    lbprec.TabIndex = 2;
        //    lbprec.Text = precio.ToString();
        //    lbprec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        //    return lbprec;
        //}

        //private PictureBox CreatePBimg(MemoryStream img, string id)
        //{
        //    PictureBox pbimg = new PictureBox();
        //    pbimg.Dock = System.Windows.Forms.DockStyle.Fill;
        //    // pbimg.Image = Image.FromFile(@"C:\Users\alexa\Pictures\umg\38546035.jpg");
        //    pbimg.Image = Image.FromStream(img);
        //    pbimg.Location = new System.Drawing.Point(3, 3);
        //    pbimg.Size = new System.Drawing.Size(120, 70);
        //    pbimg.TabIndex = 0;
        //    pbimg.TabStop = false;
        //    pbimg.Name = id;
        //    pbimg.Click += new EventHandler(AddtoGrid_Click);

        //    // pbimg.MouseDown += new MouseEventHandler(fs_MouseDown);
        //    return pbimg;
        //}
        private KryptonButton CreateButtonCategory(string nombreBT)
        {
            KryptonButton btncategoria = new KryptonButton();
            btncategoria.Dock = System.Windows.Forms.DockStyle.Fill;
            btncategoria.Location = new System.Drawing.Point(2, 397);
            btncategoria.Margin = new System.Windows.Forms.Padding(2);
            btncategoria.Name = nombreBT;
            btncategoria.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            btncategoria.Size = new System.Drawing.Size(118, 66);
            btncategoria.TabIndex = 7;
            btncategoria.Values.Text = nombreBT;
            btncategoria.Click += new System.EventHandler(this.btnCatGenerico_Click);
            return btncategoria;

            }
            //void AddtoGrid_Click(object sender, EventArgs e)
            //{
            //    try
            //    {//TODO: error al momento de guardar clavo en click de imagen
            //        int idproducto;
            //        PictureBox picture = sender as PictureBox;
            //        if (Int32.TryParse(picture.Name.ToString(), out idproducto))
            //        {
            //            // var idproducto = int.Parse(picture.Name.ToString());
            //            var productolista = listareducidaDebusquedal.Where(x => x.Id == idproducto).FirstOrDefault();
            //            var productoObtenido = listatotalbusqueda.Where(x => x.Id == idproducto).FirstOrDefault();
            //            if (productoObtenido.TieneColor == true || productoObtenido.TieneTalla == true)
            //            {
            //                abrirPrecios(idproducto.ToString());
            //            }
            //            else
            //            {
            //                cargarProdToDgvFact(productolista);
            //            }

            //        }
            //        else
            //        {

            //            abrirPrecios(picture.Name.ToString());

            //        }


            //    }
            //    catch (Exception ex)
            //    {

            //        KryptonMessageBox.Show(ex.Message);
            //    }


            //    //  cargarProdToDgvFact(productolista);

            //}


            //void verprecios_DoubleClick(object sender, EventArgs e)
            //{
            //    Label lbbutoton = sender as Label;
            //    var idproducto = (lbbutoton.Name.ToString());
            //    abrirPrecios(idproducto);

            //}


            //private void abrirPrecios(string idproducto)
            //{
            //    if (Application.OpenForms["Mas"] == null)
            //    {


            //        Mas mas = new Mas(this, idproducto);

            //        mas.Show();
            //    }

            //    else { Application.OpenForms["Mas"].Activate(); }
            //}


            private void cargarProdToDgvFact(ListarProductos producto)
        {
            var detallefactura = GetDetalleFactura();

            detallefactura.ProductoId = producto.Id;
            detallefactura.Cantidad = 1;
            detallefactura.Descripcion = producto.Descripcion;
            detallefactura.Precio = producto.PrecioVenta;
            detallefactura.SubTotal = detallefactura.Cantidad * detallefactura.Precio;
            detallefactura.PrecioTotal = detallefactura.SubTotal;
            _listaDetalleFacturas.Add(detallefactura);
            cargarDGVDetalleFactura(_listaDetalleFacturas);

        }

        //private void CargaImgProductos(IList<ListarProductos> listaproductos, bool isListaproducto)
        //{
        //    contador = 0;
        //    PictureBox creapbox; 
        //    if (isListaproducto)
        //    {

        //        foreach (var item in listaproductos)
        //        {

        //            if (item.Imagen == null)
        //            {
        //                creapbox = null;
        //                //creapbox = CreateImgGenerica(item.Id.ToString());
        //                //  continue;
        //            }
        //            else
        //            {
        //                var imgResult = cargarImg(item.Imagen);
        //                 creapbox = CreatePBimg(imgResult, item.Id.ToString());
        //            }
                    
        //            var creaLbDescrip = CreatelabelDesrip(item.Descripcion, item.Id.ToString());
        //            tabla = CreateTable(creapbox, item.PrecioVenta, creaLbDescrip, contador, item.Id.ToString());

        //            for (int x = 0; x < tablaProductos.ColumnCount; x++)//columnas (fila, columna)
        //            {
        //                tablaProductos.Controls.Add(tabla, contador, x);
        //            }

        //            contador += 1;
        //        }

        //    }
        //    else
        //    {
        //        foreach (var item in _listaCombos)
        //        {
        //             creapbox = new PictureBox();
        //            if (item.Imagen != null)
        //            {
        //                var imgResult = cargarImg(item.Imagen);
        //                creapbox = CreatePBimg(imgResult, item.IdCombo.ToString());
        //            }

        //            var creaLbDescrip = CreatelabelDesrip(item.Descripcion, item.IdCombo.ToString());
        //            tabla = CreateTable(creapbox, item.Precioventa, creaLbDescrip, contador, item.IdCombo.ToString());

        //            for (int x = 0; x < tablaProductos.ColumnCount; x++)//columnas (fila, columna)
        //            {
        //                tablaProductos.Controls.Add(tabla, contador, x);
        //            }

        //            contador += 1;
        //        }

        //    }

        //}

        private void CargarButtonsCategoria(IList<ListarCategoriaProd> listaCategoria)
        {
            if(listaCategoria==null) { KryptonMessageBox.Show("Debera crear las categorias para continuar"); Close(); }
            var contadorx = 0;
            //Label etiquedatitulo= crearLabelToCategory();
            tableButtonCategoria.RowCount = listaCategoria.Count;
           // tableButtonCategoria.Controls.Add(etiquedatitulo, 0, 0);


            foreach (var item in listaCategoria)
            {
                var buttonActual = CreateButtonCategory(item.Categoria);

                this.tableButtonCategoria.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.18841F));
                //  var tablaBts = tablaCategorias(buttonActual);


                tableButtonCategoria.Controls.Add(buttonActual,0 ,contadorx );
                contadorx++;

            }
        }





        //private MemoryStream cargarImg(byte[] imgbyte)
        //{

        //    MemoryStream mStream = new MemoryStream(imgbyte);



        //    return mStream;
        //}

   


        //private PictureBox CreateImgGenerica( string id)
        //{
        //    PictureBox imgp = new PictureBox();
        //    imgp.Dock = System.Windows.Forms.DockStyle.Fill;
        //    imgp.Image = Image.FromFile(@"C:\SoftwareRedDowl\generico.jpg");
        //    //pbimg.Image = Image.FromStream(img);
        //    imgp.Location = new System.Drawing.Point(3, 3);
        //    imgp.Size = new System.Drawing.Size(120, 70);
        //    imgp.TabIndex = 0;
        //    imgp.TabStop = false;
        //    imgp.Name = id;
        //    imgp.Click += new EventHandler(AddtoGrid_Click);

        //    // pbimg.MouseDown += new MouseEventHandler(fs_MouseDown);
        //    return imgp;
        //}


        // creacion de buttones por medio de las categorias creadas









        private void btncotizacion_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["AgregarCotizacion"] == null)
            {

                AgregarCotizacion AddCotizacion = new AgregarCotizacion(this);

                AddCotizacion.Show();
            }
            else
            {
                Application.OpenForms["AgregarCotizacion"].Activate();
            }
        }

        private void btnpedidos_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["AgregarPedido"] == null)
            {
                AgregarPedido AddCotizacion = new AgregarPedido(this);

                AddCotizacion.Show();
            }
            else
            {
                Application.OpenForms["AgregarPedido"].Activate();
            }
        }

        private void btnVales_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["AgregarVale"] == null)
            {
                AgregarVale AddCotizacion = new AgregarVale(this);

                AddCotizacion.Show();
            }
            else
            {
                Application.OpenForms["AgregarVale"].Activate();
            }
        }

        private void btnproductos_Click(object sender, EventArgs e)
        {
            cargaProductosInit();
        }
        private void cargaProductosInit()
        {
            //  tablaProductos.Controls.Clear();
            CargarProductosGenerales(_listaProductos);
            selectProducto = true;
        }
        private void btncombos_Click(object sender, EventArgs e)
        {
            // tablaProductos.Controls.Clear();
            // CargaImgProductos(_listaProductos, false);
            CargarCombosGenerales(_listaCombos);
             selectProducto = false;
        }


        private void btnCatGenerico_Click(object sender, EventArgs e)
        {
            try
            {
                KryptonButton btnCatGeneric = sender as KryptonButton;
                var filtro = _listaProductos.Where(x => x.Categoria.Contains(btnCatGeneric.Text));
                var contadorproductos = _listaProductos.Count;
                dgvProductosBd.DataSource = filtro.ToList();
                //var rowcount = tablaProductos.RowCount;
                //var columcount = tablaProductos.ColumnCount;
                //tablaProductos.Controls.Clear();
                // CargaImgProductos(filtro.ToList(), true);

                selectProducto = true;
            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show(ex.Message);
            }
           
        }

      

        //private void btnzapateria_Click(object sender, EventArgs e)
        //{
           
        //    var filtro = _listaProductos.Where(x => x.Categoria.Contains(categoria));
        //    tablaProductos.Controls.Clear();
        //    CargaImgProductos(filtro.ToList(), true);
        //    selectProducto = true;

        //}

        //private void btnropa_Click(object sender, EventArgs e)
        //{
        //    var filtro = _listaProductos.Where(x => x.Categoria.Contains(btnropa.Text));
        //    tablaProductos.Controls.Clear();
        //    CargaImgProductos(filtro.ToList(), true);
        //    selectProducto = true;
        //}

        //private void btnfiesta_Click(object sender, EventArgs e)
        //{
           
        //    var filtro = _listaProductos.Where(x => x.Categoria.Contains(categoria));
        //    tablaProductos.Controls.Clear();
        //    CargaImgProductos(filtro.ToList(), true);
        //    selectProducto = true;
        //}

        //private void btnjuguetes_Click(object sender, EventArgs e)
        //{
        //    var filtro = _listaProductos.Where(x => x.Categoria.Contains(btnjuguetes.Text));
        //    tablaProductos.Controls.Clear();
        //    CargaImgProductos(filtro.ToList(), true);
        //    selectProducto = true;
        //}

        //private void btnfantasia_Click(object sender, EventArgs e)
        //{
           
        //    var filtro = _listaProductos.Where(x => x.Categoria.Contains(categoria));
        //    tablaProductos.Controls.Clear();
        //    CargaImgProductos(filtro.ToList(), true);
        //    selectProducto = true;
        //}

        //private void btnotros_Click(object sender, EventArgs e)
        //{
        //    var filtro = _listaProductos.Where(x => x.Categoria.Contains(btnotros.Text));
        //    tablaProductos.Controls.Clear();
        //    CargaImgProductos(filtro.ToList(), true);
        //    selectProducto = true;
        //}
        private void txtbuscarcombo_TextChanged(object sender, EventArgs e)
        {
            BuscarCombos();
        }
        private void txtbuscar2_TextChanged(object sender, EventArgs e)
        {
            BuscarProductos();

        }

        //private void dgvProductoyCombos_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (selectProducto == true)
        //    {
        //        var selectFila = (ListarProductos)dgvProductoyCombos.CurrentRow.DataBoundItem;
        //        var detallefactura = GetDetalleFactura();
        //        detallefactura.Cantidad = 1;
        //        detallefactura.Descripcion = selectFila.Descripcion;
        //        detallefactura.Precio = selectFila.PrecioVenta;
        //        detallefactura.SubTotal = detallefactura.Cantidad * detallefactura.Precio;
        //        _listaDetalleFacturas.Add(detallefactura);
        //        cargarDGVDetalleFactura(_listaDetalleFacturas);
        //    }
        //    else
        //    {
        //        var selectFila = (ListarCombos)dgvProductoyCombos.CurrentRow.DataBoundItem;
        //        var detallefactura = GetDetalleFactura();
        //        detallefactura.Cantidad = 1;
        //        detallefactura.Descripcion = selectFila.Descripcion;
        //        detallefactura.Precio = selectFila.Precioventa;
        //        detallefactura.SubTotal = detallefactura.Cantidad * detallefactura.Precio;
        //        _listaDetalleFacturas.Add(detallefactura);
        //        cargarDGVDetalleFactura(_listaDetalleFacturas);

        //    }

        //}
        decimal valor1, valor2;
        //variables
        double primero, segundo, resultado;
        string operacion;
        private void concatenarNumeros(string numero)
        {
            var actual = txtcalculador.Text;
            string contenido = actual + numero;

            txtcalculador.Text = contenido;
            valor1 = Convert.ToDecimal(contenido);
        }
        private void concatenarNumeroSegundo(string numero)
        {
            var actual = txtcalculador.Text;
            string contenido = actual + numero;

            txtcalculador.Text = contenido;
            valor2 = Convert.ToDecimal(contenido);
        }


        private void btn1_Click(object sender, EventArgs e)
        {
            //concatenarNumeros("1");
            txtcalculador.Text = txtcalculador.Text + "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtcalculador.Text = txtcalculador.Text + "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtcalculador.Text = txtcalculador.Text + "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtcalculador.Text = txtcalculador.Text + "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtcalculador.Text = txtcalculador.Text + "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtcalculador.Text = txtcalculador.Text + "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtcalculador.Text = txtcalculador.Text + "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtcalculador.Text = txtcalculador.Text + "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtcalculador.Text = txtcalculador.Text + "9";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtcalculador.Text = txtcalculador.Text + "0";
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            txtcalculador.Text = "";
        }


        private void btnmenos_Click(object sender, EventArgs e)
        {

            operacion = "-";
            primero = double.Parse(txtcalculador.Text);
            txtcalculador.Clear();
        }

        private void btnmas_Click(object sender, EventArgs e)
        {
            operacion = "+";
            primero = double.Parse(txtcalculador.Text);
            txtcalculador.Clear();
        }

        private void btndivi_Click(object sender, EventArgs e)
        {
            operacion = "/";
            primero = double.Parse(txtcalculador.Text);
            txtcalculador.Clear();
        }

        private void btnMulti_Click(object sender, EventArgs e)
        {
            operacion = "*";
            primero = double.Parse(txtcalculador.Text);
            txtcalculador.Clear();
        }

        private void btnpunto_Click(object sender, EventArgs e)
        {
            txtcalculador.Text = txtcalculador.Text + ".";
        }
        private void btnigual_Click(object sender, EventArgs e)
        {
            segundo = double.Parse(txtcalculador.Text);
            switch (operacion)
            {
                case "+":
                    resultado = primero + segundo;
                    txtcalculador.Text = resultado.ToString();
                    break;
                case "-":
                    resultado = primero - segundo;
                    txtcalculador.Text = resultado.ToString();
                    break;
                case "/":
                    resultado = primero / segundo;
                    txtcalculador.Text = resultado.ToString();
                    break;
                case "*":
                    resultado = primero * segundo;
                    txtcalculador.Text = resultado.ToString();



                    break;
            }
        }




        private void txtcodreferencia_TextChanged(object sender, EventArgs e)
        {


        }

        private void txtcodreferencia_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                var codigoReferencia = txtcodreferencia.Text.Trim();
                //var productox = listareducidaDebusquedal.Where(x => x.CodigoReferencia == codigoReferencia).FirstOrDefault();
                var productox = _productosRepository.GetProductByBarCode(codigoReferencia, UsuarioLogeadoPOS.User.SucursalId);
                // var comprobarEnTabla = new ComprobarExistenciaEnTabla(ListaProductSelect);

                
                if (productox == null) return;
                if (e.KeyCode == Keys.Enter)
                {

                    var detallefactura = GetDetalleFactura();
                    detallefactura.Cantidad = 1;
                    detallefactura.Descripcion = productox.Descripcion;
                    detallefactura.Precio = productox.PrecioVenta;
                    detallefactura.SubTotal = detallefactura.Cantidad * detallefactura.Precio;
                    _listaDetalleFacturas.Add(detallefactura);
                    cargarDGVDetalleFactura(_listaDetalleFacturas);
                }
            }
            catch 
            {
                return;
            }
            


          //  }
        }
        private void BuscarProductos()
        {
            try
            {
                var filter = _listaProductos.Where(a => a.Categoria != null && a.Categoria.Contains(txtbuscar2.Text) ||
           a.CodigoReferencia != null && a.CodigoReferencia.Contains(txtbuscar2.Text) ||
          // (a.Descripcion != null && a.Descripcion.Contains(btnCotizacion.Text)) ||
          (a.Categoria != null && a.Categoria.Contains(txtbuscar2.Text)) ||
          (a.Ubicacion != null && a.Ubicacion.Contains(txtbuscar2.Text)) ||
          (a.Notas != null && a.Notas.Contains(txtbuscar2.Text))).ToList();
                dgvProductosBd.DataSource = filter.ToList();
                // tablaProductos.Controls.Clear();
                // CargaImgProductos(filter, true);
            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show(ex.Message);
            }
           

        }
        private void BuscarCombos()
        {
          
          
            try
            {
                var filter = _listaCombos.Where(a => a.CodigoBarras != null && a.CodigoBarras.Contains(txtbuscarcombo.Text) ||
                           (a.Descripcion != null && a.Descripcion.Contains(txtbuscarcombo.Text))).ToList();
                dgvProductosBd.DataSource = filter.ToList();
            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show(ex.Message);
            }
            
          
            //   tablaProductos.Controls.Clear();
            // CargaImgProductos(filter, true);

        }


        private void txtbuscar2_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dgvproductosadd_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
            OperacionPorFila();
            SumaFilas();
        }

        private void dgvproductosadd_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            SumaFilas();
        }

        private void dgvproductosadd_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            SumaFilas();
        }

        private void SumaFilas()
        {
            decimal? total = _listaDetalleFacturas.Sum(x => x.SubTotal);
           
            txttotalf.Text = total.ToString();


        }

        private void dgvproductosadd_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            SumaFilas();
        }


        private void OperacionPorFila()
        {
            try
            {
                var filaActual = (ListarDetalleFacturas)dgvproductosadd.CurrentRow.DataBoundItem;
                if (filaActual != null)
                {
                    filaActual.SubTotal = filaActual.Cantidad * filaActual.Precio;
                    filaActual.PrecioTotal = filaActual.SubTotal - filaActual.Descuento;
                }
            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show(ex.Message);
            }
            
             
              

            
           
        }

        private void btncobrar_Click(object sender, EventArgs e)
        {
            VentanaCajeroyVendedor();

        }

      private void ValidarMontoInsert()
        {
         //   var filaActual = (ListarDetalleFacturas)dgvproductosadd.CurrentRow.DataBoundItem;


        }



        private void VentanaCajeroyVendedor()
        {

            if (dgvproductosadd.RowCount <= 0) return;
            var listaErrores = new List<String>();
            var cadenadeError = "";

            foreach (var item in _listaDetalleFacturas)
            {
                if (!ValidarExistencias(item, false))
                {
                    cadenadeError += "No hay suficiente stock del producto:"
                                  + item.Descripcion + " " + item.Color + " " + item.Talla + " " + "Revise existencias!\n";
                    listaErrores.Add(cadenadeError);
                    continue;
                }
            }


            var listaobtenidaDetalle = CrearListaBySelected(dgvproductosadd, 17);


            if (listaErrores.Count > 0)
            {
                KryptonMessageBox.Show(cadenadeError);
            }
            else
            {
                if (listaobtenidaDetalle.Count == 0) { KryptonMessageBox.Show("Debe seleccionar un producto a facturar"); return; }
                /*
                        * Administrador
            Usuario Estandar
            Solo Venta
            Solo Caja
            Solo POS
            solo Administracion
                 * 
                 * */
                if (UsuarioLogeadoPOS.User.Privilegios == "Solo Venta" || UsuarioLogeadoPOS.User.Privilegios == "Usuario Estandar"|| UsuarioLogeadoPOS.User.Privilegios == "Solo POS")
                {
                    CargarFormsAcumuladas(listaobtenidaDetalle);
                }
                if (UsuarioLogeadoPOS.User.Privilegios == "Administrador" || UsuarioLogeadoPOS.User.Privilegios == "Solo Caja")
                {
                    CargarFormsPago(listaobtenidaDetalle);
                }


            }
          
        }
        
        private void CargarFormsPago(List<ListarDetalleFacturas>listaobtenidaDetalle)
        {
            if (Application.OpenForms["Pago"] != null) return;

            Pago childForm = new Pago(this, listaobtenidaDetalle, ventaAcumuladaSelected);
            childForm.Show();
        }
        private void CargarFormsAcumuladas(List<ListarDetalleFacturas> listaobtenidaDetalle)
        {
            if (Application.OpenForms["VentaAcumulada"] != null) return;

            VentaAcumulada childForm = new VentaAcumulada(this, listaobtenidaDetalle);
            childForm.Show();
        }

        public bool ValidarExistencias(ListarDetalleFacturas detallefactura, bool save = false)
        {
            Producto producto = new Producto();
            if (detallefactura.ProductoId != 0)
            {
                producto = _productosRepository.Get(detallefactura.ProductoId);
                if (producto.StockControl == true)
                {
                    if (producto.TieneColor == false && producto.TieneTalla == false)
                    {
                        if (producto.Stock > producto.stockMinimo)
                        {
                            var stock = producto.Stock - producto.stockMinimo;


                            if (stock >= detallefactura.Cantidad)
                            {
                                if (save)
                                {
                                    producto.Stock -= detallefactura.Cantidad;
                                    _productosRepository.Update(producto);
                                    return true;
                                }

                                return true;
                            }
                            else { return false; }

                        }
                        else { return false; }
                    }//descuento en tabla DetalleColors
                    else if (producto.TieneColor == true && producto.TieneTalla == false)
                    {
                        var listaobtenidaDetalleColor = _coloresRepository.GetProductoListaColor(producto.Id);
                        var detalleColorToLess = listaobtenidaDetalleColor.Where(x => x.Id == detallefactura.DetalleColorId).FirstOrDefault();

                        if (detalleColorToLess != null)
                        {
                            if (detalleColorToLess.Stock > producto.stockMinimo)
                            {
                                var stock = detalleColorToLess.Stock - producto.stockMinimo;

                                if (stock >= detallefactura.Cantidad)
                                {

                                    if (save)
                                    {
                                        detalleColorToLess.Stock -= detallefactura.Cantidad;
                                        _coloresRepository.Update(detalleColorToLess);
                                        producto.Stock -= detallefactura.Cantidad;
                                        _productosRepository.Update(producto);
                                        return true;
                                    }

                                    return true;
                                }
                                else
                                {
                                    return false;
                                }


                            }
                            else { return false; }
                        }
                        else 
                        {
                            return false;
                        }

                        }//Resta en tabla DetalleTalla stock 
                        else if (producto.TieneColor == false && producto.TieneTalla == true)
                        {
                            var listTallabyProducto = _tallasRepository.GetTallaListaProducto(producto.Id);
                            var detalleTallaToLess = listTallabyProducto.Where(x => x.Id == detallefactura.DetalleTallaId).FirstOrDefault();

                            if (detalleTallaToLess != null)
                            {
                                if (detalleTallaToLess.Stock > producto.stockMinimo)
                                {
                                    var stock = detalleTallaToLess.Stock - producto.stockMinimo;

                                    if (stock >= detallefactura.Cantidad)
                                    {

                                        if (save)
                                        {
                                            detalleTallaToLess.Stock -= detallefactura.Cantidad;
                                            _tallasRepository.Update(detalleTallaToLess);
                                            producto.Stock -= detallefactura.Cantidad;
                                            _productosRepository.Update(producto);
                                            return true;
                                        }

                                        return true;

                                    }
                                    else { return false; }
                                }
                                else
                                { return false; }
                            }
                            else 
                            {
                                return false;
                            }
                            
                        }
                        else if (producto.TieneColor == true && producto.TieneTalla == true)
                        {
                            var tallasyColores = _tallasyColoresRepository.GetTallaColorListaProducto(producto.Id);
                            var colorytallatoLess = tallasyColores.Where(x => x.Id == detallefactura.TallayColorId).FirstOrDefault();

                            if (colorytallatoLess != null)
                            {
                                if (colorytallatoLess.Stock > producto.stockMinimo)
                                {
                                    var stock = colorytallatoLess.Stock - producto.stockMinimo;

                                    if (stock >= detallefactura.Cantidad)
                                    {

                                        if (save)
                                        {
                                            colorytallatoLess.Stock -= detallefactura.Cantidad;
                                            _tallasyColoresRepository.Update(colorytallatoLess);
                                            producto.Stock -= detallefactura.Cantidad;
                                            _productosRepository.Update(producto);
                                            return true;
                                        }
                                        return true;
                                    }
                                    else { return false; }
                                }
                                else
                                { return false; }
                            }
                            else
                            {
                                return false;
                            }   

                        }

                    }
                }
                else
                {
                    var comboToLess = _combosRepository.Get(detallefactura.ComboId);
                    if (comboToLess.Stock >= detallefactura.Cantidad)
                    {

                        if (save)
                        {
                            comboToLess.Stock -= detallefactura.Cantidad;
                            _combosRepository.Update(comboToLess);
                            return true;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }

                return false;
            }



        public  List<ListarDetalleFacturas> CrearListaBySelected(DataGridView datagrid,int colAcciones)
        {
           // int colAcciones = 17;
            int filasSeleccion = 0;
            int contador = 0;
            var listaDetalles = new List<ListarDetalleFacturas>();
          //  if (dgvproductosadd.RowCount == 0) { return; }

            foreach (DataGridViewRow rows in datagrid.Rows)
            {
                
                var filasTotales = int.Parse(datagrid.RowCount.ToString());
                bool acciones = Convert.ToBoolean(rows.Cells[colAcciones].Value);
                if (!acciones)
                {
                    filasSeleccion += 1;
                }
                else
                {
                    var fila = (ListarDetalleFacturas)datagrid.Rows[contador].DataBoundItem;
                    listaDetalles.Add(fila);

                }
                contador += 1;


            }

            return listaDetalles;
        }

        private void btnborrarvale_Click(object sender, EventArgs e)
        {
            _valeSelected = null;
            cargarValeLabel();
        }

        private void dgvproductosadd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SeleccionarFila(dgvproductosadd, 17);
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

        private void btnVentasPendientes_Click(object sender, EventArgs e)
        {
         if (Application.OpenForms["VentasPendientes"] == null)
                {
                    VentasPendientes VentasPendientesFACT = new VentasPendientes(this);

                    VentasPendientesFACT.Show();
                }
         else
                {
                    Application.OpenForms["VentasPendientes"].Activate();
                }

        }

        private void dgvproductosadd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               // var filaSeleted= (Listar)
            }
            catch (Exception ex )
            {

                KryptonMessageBox.Show(ex.Message);
            }
        }

        public ListarAcumuladasEncabezado ventaAcumuladaSelected = null;

        private void dgvProductosBd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (selectProducto == true)
                {
                    var selectFila = (ListarProductos)dgvProductosBd.CurrentRow.DataBoundItem;
                    var detallefactura = GetDetalleFactura();
                    detallefactura.Cantidad = 1;
                    detallefactura.Descripcion = selectFila.Descripcion;
                    detallefactura.Precio = selectFila.PrecioVenta;
                    detallefactura.SubTotal = detallefactura.Cantidad * detallefactura.Precio;
                    detallefactura.ProductoId = selectFila.Id;
                    if (detallefactura.Precio > 0 && selectFila.Stock > 0)
                    {
                        _listaDetalleFacturas.Add(detallefactura);
                        cargarDGVDetalleFactura(_listaDetalleFacturas);
                    }
                    else
                    {
                        KryptonMessageBox.Show("El producto contiene informacion que no es valida (precio/stock)\npor favor revisar el Detalle del Producto.");
                    }
                    
                }
                else
                {
                    var selectFila = (ListarCombos)dgvProductosBd.CurrentRow.DataBoundItem;
                    var detallefactura = GetDetalleFactura();
                    detallefactura.Cantidad = 1;
                    detallefactura.Descripcion = selectFila.Descripcion;
                    detallefactura.Precio = selectFila.Precioventa;
                    detallefactura.SubTotal = detallefactura.Cantidad * detallefactura.Precio;
                    detallefactura.ComboId = selectFila.IdCombo;
                    _listaDetalleFacturas.Add(detallefactura);
                    cargarDGVDetalleFactura(_listaDetalleFacturas);

                }
            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show(ex.Message);
            }
        }

        private void checkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int acciones = 17;
                if (checkAll.Checked == true)
                {
                    if (dgvproductosadd.RowCount > 0)
                    {
                        foreach (DataGridViewRow row in dgvproductosadd.Rows)
                        {
                            row.Cells[acciones].Value = true;


                        }
                    }

                }
                else
                {
                    foreach (DataGridViewRow row in dgvproductosadd.Rows)
                    {
                        row.Cells[acciones].Value = false;


                    }

                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message);
            }

           
        }

        private void dgVentasPendientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var filaObj = (ListarAcumuladasEncabezado)dgVentasPendientes.CurrentRow.DataBoundItem;
            ventaAcumuladaSelected = filaObj;
            if (ValidarRol())
            {
                CargarDetalleToFactura(filaObj);
            }
        }

     
        private void dgVentasPendientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private bool ValidarRol()
        {
            
            if (UsuarioLogeadoPOS.User.UserName == "admin@admin")
            {
                return true;
            }
            else { return false; }


        }


        private void CargarDetalleToFactura(ListarAcumuladasEncabezado encabezado)
        {
            Guid idSolicitud = encabezado.Id;
            var detalleObtenido = _solicitudesRepository.GetListaVentasAcumuladasbyUser(idSolicitud);
            if(detalleObtenido!= null)
            {
              //var listaDetalles= GetSolicitudToFacturaDetalle(detalleObtenido);
               // _listadetallesPedidos = listaDetalles;
                cargarDGVDetalleFactura(GetSolicitudToFacturaDetalle(detalleObtenido));
            }
        }

        private void tablaProductos_Paint(object sender, PaintEventArgs e)
        {

        }

      
    }




} 

