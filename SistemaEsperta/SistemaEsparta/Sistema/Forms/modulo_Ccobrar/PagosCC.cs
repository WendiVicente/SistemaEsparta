using CapaDatos.ListasPersonalizadas;
using CapaDatos.Models.CuentaCobrar;
using CapaDatos.WebServiceSAT;
using CapaDatos.Repository;
using CapaDatos.Validation;
using ComponentFactory.Krypton.Toolkit;
using sharedDatabase.Models;
using Sistema.Reports.Reporte_Creditos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebApi;
using sharedDatabase.Models.Caja;
using Newtonsoft.Json;
using CapaDatos.Models.DocumentoSAT;
using Sistema.Reports.Reporte_Facturacion;
using CapaDatos.Repository.SolicitudestoFacturar;
using CapaDatos.ListasPersonalizadas.VentasAcumuladas;
using CapaDatos.Models.ProductosToFacturar;
using POS.Recibo;
using POS.Validations;
using CapaDatos.Models.Pedidos;

namespace Sistema.Forms.modulo_Ccobrar
{
    public partial class PagosCC : BaseContext
    {
        private DetallePagoRepository _detallePagoRepostitory = null;
        private ClientesRepository _clientesRepository = null;
        private CuentasCobrarRepository _cuentasCRepository = null;
        private ProductosReservaRepository _productosRRepository = null;
        private readonly CertificadoSatRepository _certificadoSatRepository = null;
        private readonly VentasRepository _ventasRepository = null;
        private readonly ProductosRepository _productosRepository = null;
        private readonly CajasRepository _cajasRepository = null;
        private readonly FacturasRepository _facturasRepository = null;
        private SolicitudesRepository _solicitudesRepository = null;
        private ClienteCreditoRepository _clienteCreditoRepository = null;
        private DetalleProductoRepository _detalleProductoRepository = null;
        private DevolucionRepository _devolucionesRepository = null;
        private ColoresRepository _coloresRepository = null;
        private TallasRepository _tallasRepository = null;
        private TomoEdicionRepository _tomoEdicionRepository = null;

        private List<ProductosReserva> _listaReservaproductos = new List<ProductosReserva>();
        private List<ListarDevoluciones> _devoluciones = new List<ListarDevoluciones>();       
        private List<DetalleFactura> _listadoDetalleFacturas = null;
        private IList<Cliente> _clientesWithCuentas = null;
        private IList<ListarNotasPago> _notasDePago = null;
        private List<Devoluciones> _histdevoluciones = null;
        private ListaFacturacion Documento = new ListaFacturacion();

        private int sucursalId = UsuarioLogeadoSistemas.User.SucursalId;
        private DocumentoCertificadoSAT DocCertificado = null;
        private TokenSAT TokenObtenidoSat;        
        
        private CuentaCB _cuenta = null;
        private Guid CuentaCBId;     
        private decimal TotalDoc;
        private decimal TotalCredito;
        private decimal TotalPagado;
        private string TipoDetalle = "";
        private bool TieneDetalle = false;        
        private bool ValidarSaldo = false;
        private bool conectado = false;

        public PagosCC()
        {
            _clientesRepository = new ClientesRepository(_context);
            _detallePagoRepostitory = new DetallePagoRepository(_context);
            _cajasRepository = new CajasRepository(_context);
            _cuentasCRepository = new CuentasCobrarRepository(_context);
            _productosRRepository = new ProductosReservaRepository(_context);
            _facturasRepository = new FacturasRepository(_context);
            _certificadoSatRepository = new CertificadoSatRepository(_context);
            _ventasRepository = new VentasRepository(_context);
            _productosRepository = new ProductosRepository(_context);
            _solicitudesRepository = new SolicitudesRepository(_context);
            _clienteCreditoRepository = new ClienteCreditoRepository(_context);
            _detalleProductoRepository = new DetalleProductoRepository(_context);
            _devolucionesRepository = new DevolucionRepository(_context);
            _coloresRepository = new ColoresRepository(_context);
            _tallasRepository = new TallasRepository(_context);
            _tomoEdicionRepository = new TomoEdicionRepository(_context);
            _listadoDetalleFacturas = new List<DetalleFactura>();
            InitializeComponent();
        }

        private void Pagos_Load(object sender, EventArgs e)
        {
            cargarclienteCombo();
            cargarMovimientoCombo();
            CargarPago();
            CargarTokenSat();
            dtpFechaPago.Value = DateTime.Now;
        }

        private void CargarTokenSat()
        {
            Internet internet = new Internet(1);
            TokenObtenidoSat = internet.TokenObtenidoSat;
            conectado = internet.conectado;
        }

        private void CargarPago()
        {
            List<string> tipoPago = new List<string>();
            tipoPago.Add("Efectivo");
            tipoPago.Add("Cheque");
            tipoPago.Add("Tarjeta Debito");
            tipoPago.Add("Tarjeta Credito");
            tipoPago.Add("Boleta Deposito");
            tipoPago.Add("Transferencia");
            combopagos.DataSource = tipoPago;
            combopagos.SelectedIndex = 0;

        }
        private void cargarclienteCombo()
        {
            _clientesWithCuentas = _clientesRepository.GetClientesbyCredi();
            cbCliente.DataSource = _clientesWithCuentas;
            cbCliente.ValueMember = "Id";
            cbCliente.DisplayMember = "Nombres";
            cbCliente.SelectedIndex = 0;
        }

        private void cargarMovimientoCombo()
        {
            var tipomovimiento = _cuentasCRepository.GetlistaMovimientos();
            var filtromovi = tipomovimiento.Where(x => x.tipoMovimiento == "Pago").ToList();
            combomovimiento.DataSource = filtromovi;
            combomovimiento.ValueMember = "Id";
            combomovimiento.DisplayMember = "tipoMovimiento";
        }

        private void CargarDGVHistorial(Guid idCCB)
        {
            _notasDePago =  _cuentasCRepository.Getlistadepagoscreditos(idCCB);
            BindingSource source = new BindingSource();
            source.DataSource = _notasDePago;
            dgvNocredi.DataSource = typeof(List<>);
            dgvNocredi.DataSource = source;
            dgvNocredi.Columns[0].Visible = false;
            dgvNocredi.Columns[7].Visible = false;
            dgvNocredi.Columns[8].Visible = false;
            dgvNocredi.Columns[9].Visible = false;
            dgvNocredi.Columns[10].Visible = false;
            TotalPagado = _notasDePago.Sum(x => x.Monto);
        }

        private DetalleCaja getdetalleCaja()
        {
            return new DetalleCaja()
            {
                Descripcion = combopagos.SelectedItem.ToString()
            };
        }


        private NotaPago NuevaNotapago()
        {
            return new NotaPago()
            {
                Id = Guid.NewGuid(),
                NoDocumento = Numero(),
                Descripcion = txtconcepto.Text,
                Monto = decimal.Parse(txtMontoPagar.Text),
                FechaTransaccion = DateTime.Now,
                UserId = UsuarioLogeadoSistemas.User.Id,
                CuentaCBId = _cuenta.Id,
                MovimientoId = int.Parse(combomovimiento.SelectedValue.ToString()),
            };
        }

        private string Numero()
        {
            string notapago = _cuentasCRepository.GetLastNotaPago();
            string tipo = "NP-";
            string numero = "";
            
            if (notapago.Length > 0)
            {
                int maxsol = Convert.ToInt32(notapago.Split('-')[1]) + 1;
                if (maxsol < 10)
                    numero = tipo + "00" + maxsol;
                else if (maxsol < 100)
                    numero = tipo + "0" + maxsol;
                else
                    numero = tipo + maxsol;
            }
            else
            {
                numero = tipo + "001";
            }
            return numero;
        }

        public string GenerateRandom(string tipo)
        {
            Random randomGenerate = new System.Random();
            string nocuenta = tipo;
            var cadena = System.Convert.ToString(randomGenerate.Next(00000001, 99999999));
            var resulto = String.Concat(nocuenta, cadena);
            return resulto;
        }

        private void btnguardarnp_Click(object sender, EventArgs e)
        {
            try
            {
                var totaltosend = decimal.Parse(txtMontoPagar.Text);
                if (totaltosend == 0)
                {
                    KryptonMessageBox.Show("No ha ingresado ningún monto válido");
                    return;
                }

                if (_cajasRepository.GetCajaAperturada(sucursalId) == null)
                {
                    KryptonMessageBox.Show("No hay ninguna caja aperturada en esta sucursal");
                    return;
                }

                if (string.IsNullOrEmpty(txtconcepto.Text))
                {
                    KryptonMessageBox.Show("No ha colocado una concepto para el pago");
                    return;
                }

                var notapago = NuevaNotapago();
                string tipopago = combopagos.Text;
                if (tipopago == "Cheque" || tipopago == "Boleta Deposito" || tipopago == "Transferencia")
                {
                    var detallePago = GetModelPago(notapago.NoDocumento, notapago.Monto);
                    _detallePagoRepostitory.Add(detallePago);
                }

                if (totaltosend > _cuenta.SaldoActual)
                {
                    decimal residuo = totaltosend - _cuenta.SaldoActual;
                    _cuenta.SaldoActual -= (totaltosend - residuo);
                }
                else
                {
                    _cuenta.SaldoActual -= totaltosend;
                }
                _cuentasCRepository.Update(_cuenta);
                var cajaObtenida = _cajasRepository.GetCajaAperturada(sucursalId);
                var detalleCaja = new DetalleCaja
                {
                    Descripcion = "Nota de pago -"+tipopago,
                    CajaId = cajaObtenida.Id,
                };
                SetearMontos(detalleCaja);
                _cajasRepository.AddDetalleCaja(detalleCaja);
                _cuentasCRepository.AddNotaPago(notapago);
                KryptonMessageBox.Show("Registro guardado correctamente!");

                if (checkRecibo.Checked)
                {
                    cargarReporte(notapago, _cuenta);
                }
                Close();


            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message);

            }
        }

        private DetallePago GetModelPago(string documento, decimal Monto)
        {
            return new DetallePago
            {
                Documento = documento,
                Pago = combopagos.Text,
                Entidad = txtEntidad.Text,
                Numero = txtNumero.Text,
                Monto = Monto,
                Fecha = dtpFechaPago.Value
            };
        }

        private void cargarReporte(NotaPago notpago, CuentaCB cuenta)
        {
            ReporteNotapago notapago = new ReporteNotapago(notpago, cuenta);
            notapago.Show();
        }

        private void cargartextCliente()
        {
            if (!(cbCliente.SelectedValue is Cliente))
            {
                var clienteSeleccionado = int.Parse(cbCliente.SelectedValue.ToString());
                var clienteObtenido = _clientesRepository.Get(clienteSeleccionado);
                if (clienteObtenido != null)
                {
                    lbNit.Text = clienteObtenido.Nit;
                    lbNombre.Text = clienteObtenido.Nombres + " " + clienteObtenido.Apellidos;
                    lbDireccion.Text = clienteObtenido.Direccion;

                    var cuenta = _cuentasCRepository.GetCcbyCliente(clienteObtenido.Id);
                    if (cuenta != null)
                    {
                        _cuenta = cuenta;
                        CuentaCBId = cuenta.Id;
                        lbCuentaPorCobrar.Text = cuenta.NoCuenta;
                        lbsaldoActual.Text = cuenta.SaldoActual.ToString();
                        CargarDGVHistorial(CuentaCBId);
                        CargarHistorialDevoluciones(CuentaCBId);
                    }
                }
            }
        }

        private void cbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ListaFacturacion> lis = new List<ListaFacturacion>();
            CargarDocumentoDGV(new List<ListarDetalleDocumento>());
            cargartextCliente();
            CargarDocumentos(lis); 
            CargaDGV(lis, dgvDocs, false);
            CargaDGV(lis, dgvListadoPago, true);
            
            if (lis.Count() > 0)
            {
                ValidarPagos();
                btnPagar.Enabled = true;
            }
            else
            {
                btnPagar.Enabled = false;
            }
        }

        private void ValidarPagos() 
        {
            if (TotalPagado > 0.00m && TotalCredito > 0.00m)
            {
                decimal residuo = TotalPagado - TotalCredito;
                if (residuo == 0)
                {
                    btnguardarnp.Enabled = false;
                    pnlPagados.Visible = true;
                    CargaDGV(new List<ListaFacturacion>(), dgvDocs, false);
                    CargarDocumentoDGV(new List<ListarDetalleDocumento>());                    
                    pgPagos.Visible = true;
                    knPagos.SelectedPage = pgPagos;
                }
            }            
            else
            {
                pgPagos.Visible = false;
                btnguardarnp.Enabled = true;
                pnlPagados.Visible = false;
            }

            var cuenta = _cuentasCRepository.Get(CuentaCBId);
            if (cuenta != null)
            {
                if (cuenta.SaldoActual == 0)
                {
                    btnguardarnp.Enabled = false;
                    pnlPagados.Visible = true;
                    CargaDGV(new List<ListaFacturacion>(), dgvDocs, false);
                    CargarDocumentoDGV(new List<ListarDetalleDocumento>());
                    pgPagos.Visible = true;
                    knPagos.SelectedPage = pgPagos;
                }
            }

            checkFEL.Enabled = conectado;
            if (!conectado)
            {
                KryptonMessageBox.Show("No posee conexión a internet, no podrá realizar FEL.", "Notificación");
            }
        }

        private void CargarDocumentos(List<ListaFacturacion> listado)
        {
            decimal total = 0.00m;
            ListaFacturacion documento = new ListaFacturacion();
            var solicitudes = solicitudesByCuenta();
            foreach (var item in solicitudes)
            {
                decimal totalsol = ObtenerTotalSol(item.Id);
                documento.idDocumento = item.Id;
                documento.idTipoDocumento = 17;
                documento.Documento = item.NoSolicitud;
                documento.Fecha = item.FechaVenta;
                if (item.ClienteId <= 0)
                {
                    documento.Nit = item.NitCliente;
                    documento.Nombre = item.NombreCliente;
                }
                else
                {
                    ObtenerCliente(item.ClienteId, documento);
                }
                documento.Vendedor = item.Vendedor;
                documento.Valor = "Q " + totalsol;
                total += totalsol;
                documento.Facturar = false;
                listado.Add(documento);
                documento = new ListaFacturacion();
            }            
            lbSaldoTotal.Text = total.ToString();
            TotalCredito = total;
        }
        private void ObtenerCliente(int idcliente, ListaFacturacion documento)
        {
            var cliente = _clientesRepository.Get(idcliente);
            if (cliente != null)
            {
                documento.ClienteId = cliente.Id;
                documento.Nombre = cliente.Nombres + " " + cliente.Apellidos;
                documento.Nit = cliente.Nit;
            }
        }

        private void CargaDGV(List<ListaFacturacion> listado, DataGridView datag, bool mostrarFacturar)
        {
            datag.DataSource = typeof(List<>);
            datag.DataSource = listado;
            datag.Columns[0].Visible = false;
            datag.Columns[1].Visible = false;
            datag.Columns[8].Visible = mostrarFacturar;
            datag.Columns[9].Visible = false;
            datag.Columns[10].Visible = false;
            datag.ClearSelection();
        }

        private void CargarHistorialDevoluciones(Guid idCCB)
        {
            _histdevoluciones = _devolucionesRepository.DevolucionesByCuenta(idCCB);
            dgvDevoluciones.DataSource = typeof(List<>);
            dgvDevoluciones.DataSource = _histdevoluciones;
            dgvDevoluciones.Columns[0].Visible = false;
            dgvDevoluciones.Columns[1].Visible = false;
            dgvDevoluciones.Columns[3].Visible = false;
            dgvDevoluciones.Columns[13].Visible = false;
            dgvDevoluciones.Columns[14].Visible = false;
            dgvDevoluciones.Columns[15].Visible = false;
            dgvDevoluciones.Columns[16].Visible = false;
            dgvDevoluciones.Columns[17].Visible = false;            
            dgvDevoluciones.ClearSelection();
        }

        public decimal ObtenerTotalSol(Guid id)
        {
            decimal Total = 0.00m;
            try
            {
                var solicitudes = _solicitudesRepository.GetDetallebySolicitud(id);
                solicitudes = solicitudes.Where(x => x.EstadoDevolucion == null || x.EstadoDevolucion == false).ToList();
                if (solicitudes.Count > 0)
                {
                    Total = solicitudes.Sum(x => x.PrecioTotal);
                }
            }
            catch
            {
                MessageBox.Show("No pudo obtener valor Total del documento.", "Advertencia");
                Total = 0.00m;
            }
            return Total;
        }

        public List<SolicitudToFacturar> solicitudesByCuenta()
        {
            List<SolicitudToFacturar> solicitudes = new List<SolicitudToFacturar>();
            var documentos = _clienteCreditoRepository.GetByCuenta(CuentaCBId);
            foreach (var item in documentos)
            {
                var solicitud = _solicitudesRepository.Get(item.DocumentoId);
                if(!solicitud.Estado)
                    solicitudes.Add(solicitud);
            }
            return solicitudes;
        }


        private void CargarSolicitud(Guid id)
        {
            try
            {
                var solicitud = _solicitudesRepository.Get(id);
                if (solicitud != null)
                {
                    var detallesolicitudes = _solicitudesRepository.GetDetallebySolicitud(solicitud.Id);
                    detallesolicitudes = detallesolicitudes.Where(x => x.EstadoDevolucion == false || x.EstadoDevolucion == null).ToList();
                    List<ListarDetalleDocumento> listadoDet = new List<ListarDetalleDocumento>();
                    ListarDetalleDocumento detalleDoc = new ListarDetalleDocumento();
                    foreach (SolicitudDetalle item in detallesolicitudes)
                    {
                        var producto = _productosRepository.Get((int)item.ProductoId);
                        decimal pDescuento = 0.00m;
                        if (item.Descuento >= 1)
                        {
                            pDescuento = item.Precio - (item.Descuento / item.Cantidad);
                        }
                        detalleDoc.Documento = solicitud.NoSolicitud;
                        detalleDoc.DetalleDocumentoId = item.Id.ToString();
                        detalleDoc.ProductoId = producto.Id;
                        detalleDoc.Descripcion = producto.Descripcion;
                        detalleDoc.Cantidad = item.Cantidad;
                        detalleDoc.Precio = producto.PrecioVenta;
                        detalleDoc.Descuento = pDescuento;
                        detalleDoc.Subtotal = item.PrecioTotal;
                        listadoDet.Add(detalleDoc);
                        detalleDoc = new ListarDetalleDocumento();
                    }
                    CargarDocumentoDGV(listadoDet);
                    decimal totalprod = listadoDet.Sum(x => x.Subtotal);
                    lbSaldoProductos.Text = totalprod.ToString();
                    var cuenta = _cuentasCRepository.Get(CuentaCBId);
                    if (cuenta.SaldoActual < totalprod)
                    {
                        ValidarSaldo = true;
                    }
                }
                else
                {
                    MessageBox.Show("¡No se pudo obtener la venta!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("¡No se pudo obtener la venta! \n" + ex.Message);
            }
        }

        private void CargarDocumentoDGV(List<ListarDetalleDocumento> _listadoDetalles)
        {
            dgvDetails.DataSource = typeof(List<>);
            dgvDetails.DataSource = _listadoDetalles;
            dgvDetails.Columns[0].Visible = false;
            dgvDetails.Columns[1].Visible = false;
            dgvDetails.Columns[2].Visible = false;
            dgvDetails.ClearSelection();
        }

        private void dgvDocs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                var fila = dgvDocs.Rows[e.RowIndex];
                Documento = (ListaFacturacion)fila.DataBoundItem;
                TotalDoc = ObtenerTotalSol(Documento.idDocumento);
                CargarSolicitud(Documento.idDocumento);
            }
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvDocs.GetCellCount(DataGridViewElementStates.Selected);
            var fila = dgvDocs.CurrentRow;
            if (selectedCellCount > 0 && fila != null)
            {
                lbnodoc.Visible = true;
                lbNoDocumento.Visible = true;
                lbNoDocumento.Text = Documento.Documento;
                pgPagos.Visible = true;
                knPagos.SelectedPage = pgPagos;                
            }
            else
            {
                KryptonMessageBox.Show("Seleccione un documento para continuar.", "Advertencia");
            }

            
        }

        private void checkMostrarTotal_CheckedChanged(object sender, EventArgs e)
        {
            if (checkMostrarTotal.Checked)
            {
                lbsaldoActual.Text = lbSaldoTotal.Text;
            }
            else 
            {
                lbsaldoActual.Text = TotalDoc.ToString();
            }
        }

        private void txtMontoPagar_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtMontoPagar_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMontoPagar.Text) || txtMontoPagar.Text == "." || txtMontoPagar.Text == "0") { return; }
            if (txtMontoPagar.Text == "0") { txtnuevosaldo.Text = ""; }
            var saldoActual = decimal.Parse(lbsaldoActual.Text);
            var montoingresado = decimal.Parse(txtMontoPagar.Text);
            txtnuevosaldo.Text = (saldoActual - montoingresado).ToString();
        }

        private void combopagos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoPago = combopagos.Text;
            switch (tipoPago)
            {
                case "Efectivo":
                    MostrarCampos(false);
                    break;
                case "Cheque":
                    MostrarCampos(true);
                    break;
                case "Tarjeta Debito":
                    MostrarCampos(false);
                    break;
                case "Tarjeta Credito":
                    MostrarCampos(false);
                    break;
                case "Boleta Deposito":
                    MostrarCampos(true);
                    lbNumero.Text = "Boleta";
                    break;
                case "Transferencia":
                    MostrarCampos(true);
                    lbNumero.Text = "Boleta";
                    break;
            }
        }

        private void MostrarCampos(bool mostrar)
        {
            txtNumero.Visible = mostrar;
            txtEntidad.Visible = mostrar;
            dtpFechaBoleta.Visible = mostrar;
            lbEntidad.Visible = mostrar;
            lbNumero.Visible = mostrar;
            lbFecha.Visible = mostrar;
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            List<ListaFacturacion> listado = new List<ListaFacturacion>();
            ListaFacturacion documento = new ListaFacturacion();
            foreach (DataGridViewRow row in dgvListadoPago.Rows)
            {
                bool Facturar = Convert.ToBoolean(row.Cells[8].Value);
                if (Facturar)
                {
                    documento = (ListaFacturacion)row.DataBoundItem;
                    listado.Add(documento);
                }
                documento = new ListaFacturacion();
            }

            if (listado.Count() == 0)
            {
                KryptonMessageBox.Show("Debe seleccionar al menos un documento.", "Notificación");
                return;
            }

            try
            {
                if (_cajasRepository.GetCajaAperturada(sucursalId) != null)
                {
                    var cajaObtenida = _cajasRepository.GetCajaAperturada(sucursalId);
                    List<Factura> listadofacturas = new List<Factura>();
                    foreach (var item in listado)
                    {
                        var modelFactura = GetViewModel(item.ClienteId);

                        bool validarFact = ModelState.IsValid(modelFactura);
                        if (validarFact)
                        {
                            int tipo = item.idTipoDocumento;
                            FacturarSolicitud(item.idDocumento, modelFactura.Id);
                            if (_listadoDetalleFacturas.Count() > 0)
                            {
                                _ventasRepository.Add(modelFactura);
                                var detallesDoc = _detalleProductoRepository.GetListNoDocumento(item.Documento);
                                foreach (var detalle in detallesDoc)
                                {
                                    detalle.noDocumento = modelFactura.NoFactura;
                                    detalle.FechaDocumento = modelFactura.FechaVenta;
                                    _detalleProductoRepository.Update(detalle);
                                }

                                foreach (var detalle in _listadoDetalleFacturas)
                                {
                                    _facturasRepository.Add(detalle, true);
                                }
                                ActualizarDocumento(item);
                                listadofacturas.Add(modelFactura);
                                _listadoDetalleFacturas.Clear();
                                KryptonMessageBox.Show("Datos registrados correctamente", "Notificación");
                            }
                            else
                            {
                                KryptonMessageBox.Show("Listado de productos para solicitud vacio", "ERROR");
                            }                            
                        }
                        else
                        {
                            KryptonMessageBox.Show("Se han encontrado datos inválidos.", "ERROR");
                        }
                    }                    
                    AbrirFactura(listadofacturas);
                    Close();
                }
                else
                {
                    MessageBox.Show("No hay ninguna caja aperturada en esta sucursal.", "Advertencia");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se han podido guardar los cambios.\n" + ex.Message, "ERROR");
            }
        }

        private void AbrirFactura(List<Factura> listadofacturas)
        {
            if (checkFEL.Checked)
            {
                foreach (var item in listadofacturas)
                    AperturaReporteFEl(item.Id);
            }
            else 
            {
                foreach (var item in listadofacturas)
                    AperturaRecibo(item.Id);
            }
        }

        private void AperturaReporteFEl(Guid FacturaId)
        {
            generaFEL(FacturaId);

            if (DocCertificado != null)
            {
                if (checkFEL.Checked == true)
                {
                    ReporteReciboCliente facturaFEL = new ReporteReciboCliente(FacturaId, DocCertificado);
                    facturaFEL.Show();
                }
            }
            else
            {
                MessageBox.Show("Ha ocurrio un error interno al generar FEL.", "ERROR");
            }
        }

        private void generaFEL(Guid guidFacturaid)
        {
            var factura = _facturasRepository.Get(guidFacturaid);
            var clienteformado = Getmodelcliente((int)factura.ClienteId);
            var listaDetallefactura = _facturasRepository.GetDetallebyFactura(guidFacturaid);
            var xmlValidado = ValidarXML.enviarxml(TokenObtenidoSat.Token, clienteformado, listaDetallefactura, factura.NoFactura);

            DocCertificado = JsonConvert.DeserializeObject<DocumentoCertificadoSAT>(xmlValidado);
            if (DocCertificado == null) { return; }
            try
            {
                DocCertificado.FacturaId = guidFacturaid;
                _certificadoSatRepository.add(DocCertificado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private RESPONSE Getmodelcliente(int ClienteId)
        {
            var cliente = _clientesRepository.Get(ClienteId);
            string[] direccion = cliente.Direccion.Split(',');
            RESPONSE ClienteFactura = new RESPONSE();

            ClienteFactura.NOMBRE = cliente.Nombres + " " + cliente.Apellidos;
            ClienteFactura.NIT = cliente.Nit;

            if (direccion.Length > 1 && direccion.Length <= 3)
            {
                ClienteFactura.DEPARTAMENTO = direccion[0].Trim();
                ClienteFactura.MUNICIPIO = direccion[1].Trim();
                ClienteFactura.PAIS = direccion[2].Trim();
            }
            else
            {
                ClienteFactura.DEPARTAMENTO = cliente.Direccion;
                ClienteFactura.PAIS = "GT";
            }
            return ClienteFactura;
        }

        private void AperturaRecibo(Guid FacturaId)
        {
            ReciboVenta reciboVenta = new ReciboVenta(FacturaId, UsuarioLogeadoSistemas.User.Name);
            reciboVenta.Show();
        }

        private void ActualizarDocumento(ListaFacturacion item)
        {
            var solicitudc = _solicitudesRepository.Get(item.idDocumento);
            solicitudc.Estado = true;
            _solicitudesRepository.Update(solicitudc);
        }

        public Factura GetViewModel(int ClienteId)
        {
            var cliente = _clientesRepository.Get(ClienteId);
            string correlativo = Crearcorrelativo();
            return new Factura()
            {
                Id = Guid.NewGuid(),
                Nombre = cliente.Nombres + " " + cliente.Apellidos,
                Direccion = cliente.Direccion,
                NIT = cliente.Nit,
                FechaVenta = DateTime.Now,
                ClienteId = ClienteId,
                NoFactura = correlativo,
                Vendedor = UsuarioLogeadoSistemas.User.Name,
                TipoPago = "Efectivo",
                UserId = UsuarioLogeadoSistemas.User.Id,
            };
        }

        private string Crearcorrelativo()
        {
            string tipo = "FC-";
            string numerocorrel = "";
            do
            {
                numerocorrel = GenerarNumeroAleatorios.GenerateRandom(tipo);
            }
            while (ExisteFactura(numerocorrel));
            return numerocorrel;
        }

        private bool ExisteFactura(string cadena)
        {
            var factunumero = _ventasRepository.GetbyNoCorrelativo(cadena);
            if (factunumero == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void FacturarSolicitud(Guid ID, Guid idFact)
        {
            var solicitudes = _solicitudesRepository.GetDetallebySolicitud(ID);
            solicitudes = solicitudes.Where(x => x.EstadoDevolucion == null || x.EstadoDevolucion == false).ToList();
            foreach (var item in solicitudes)
            {
                var producto = _productosRepository.Get((int)item.ProductoId);
                var detalle = new DetalleFactura()
                {
                    FacturaId = idFact,
                    Cantidad = item.Cantidad,
                    Precio = item.Precio,
                    Descuento = item.Descuento,
                    SubTotal = item.SubTotal,
                    PrecioTotal = item.PrecioTotal
                };

                if (item.ProductoId != null)
                {
                    detalle.ProductoId = producto.Id;
                }
                if (item.DetalleColorId != null)
                {
                    detalle.DetalleColorId = item.DetalleColorId;
                }
                if (item.DetalleTallaId != null)
                {
                    detalle.DetalleTallaId = item.DetalleTallaId;
                }
                if (item.DetalleTomoEdicionId != null)
                {
                    detalle.DetalleTomoEdicionId = item.DetalleTomoEdicionId;
                }
                if (item.ProductoId == null)
                {
                    detalle.ComboId = item.ComboId;
                }
                if (detalle.Descuento > 0)
                {
                    detalle.Precio = item.PrecioTotal / item.Cantidad;
                }
                _listadoDetalleFacturas.Add(detalle);
            }
        }

        private void SetearMontos(DetalleCaja detalleCaja)
        {
            if (combopagos.Text == "Efectivo") { detalleCaja.Efectivo = decimal.Parse(txtMontoPagar.Text); }
            if (combopagos.Text == "Cheque") { detalleCaja.Cheques = decimal.Parse(txtMontoPagar.Text); }
            if (combopagos.Text == "Tarjeta Debito") { detalleCaja.TarjetaDebito = decimal.Parse(txtMontoPagar.Text); }
            if (combopagos.Text == "Tarjeta Credito") { detalleCaja.TarjetaCredito = decimal.Parse(txtMontoPagar.Text); }
            if (combopagos.Text == "Boleta Deposito") { detalleCaja.Transferencia = decimal.Parse(txtMontoPagar.Text); }
            if (combopagos.Text == "Transferencia") { detalleCaja.Transferencia = decimal.Parse(txtMontoPagar.Text); }
        }

        private void btnDevolucion_Click(object sender, EventArgs e)
        {
            List<ListarDevoluciones> listado = new List<ListarDevoluciones>();
            ListarDevoluciones devolucion = new ListarDevoluciones();
            foreach (DataGridViewRow row in dgvDetails.Rows)
            {
                bool Facturar = Convert.ToBoolean(row.Cells[8].Value);
                if (Facturar)
                {
                    var fila = (ListarDetalleDocumento)row.DataBoundItem;
                    devolucion = new ListarDevoluciones
                    {
                        DetalleDocumentoId = fila.DetalleDocumentoId,
                        ProductoId = fila.ProductoId,
                        Documento = fila.Documento,
                        Descripcion = fila.Descripcion,
                        Fecha = dtpFechaDevolucion.Value
                        //Cantidad = fila.Cantidad
                    };
                    listado.Add(devolucion);
                }
            }

            if (listado.Count() == 0)
            {
                KryptonMessageBox.Show("Debe seleccionar al menos un producto.", "Notificación");
                return;
            }
            else
            {
                pgDevoluciones.Visible = true;
                CargarDGVDevoluciones(listado);                
            }
        }

        private void CargarDGVDevoluciones(List<ListarDevoluciones> devoluciones)
        {
            int rowCount = dgvDevolucionesProducto.RowCount;
            if (rowCount == 0)
            {
                _devoluciones = devoluciones;
                dgvDevolucionesProducto.DataSource = typeof(List<>);
                dgvDevolucionesProducto.DataSource = _devoluciones;
            }
            else
            {
                foreach (ListarDevoluciones item in devoluciones)
                {
                    var devs = _devoluciones.Where(x => x.DetalleDocumentoId == item.DetalleDocumentoId && item.ProductoId == x.ProductoId);
                    if (devs.Count() == 0)
                    {
                        _devoluciones.Add(item);
                    }                    
                }
                dgvDevolucionesProducto.DataSource = typeof(List<>);
                dgvDevolucionesProducto.DataSource = _devoluciones;
            }
            dgvDevolucionesProducto.Columns[0].Visible = false;
            dgvDevolucionesProducto.Columns[1].Visible = false;
            dgvDevolucionesProducto.Columns[6].Visible = false;
            dgvDevolucionesProducto.ClearSelection();
        }

        private void dgvDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 8)
                {
                    btnDevolucion.Enabled = !ValidarSaldo;
                    int rowcount = dgvDetails.RowCount;
                    int contador = 0;

                    var _prod = (ListarDetalleDocumento)dgvDetails.Rows[e.RowIndex].DataBoundItem;
                    if (_prod.Devolver)
                        _prod.Devolver = false;
                    else
                        _prod.Devolver = true;

                    foreach (DataGridViewRow item in dgvDetails.Rows)
                    {
                        var element = (ListarDetalleDocumento)item.DataBoundItem;
                        if (element.Devolver)
                        {
                            contador++;
                        }
                    }
                    if (rowcount == contador)
                        checkAll.Checked = true;
                    else
                        checkAll.Checked = false;
                }
            }
        }

        private void checkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAll.Checked)
            {
                btnDevolucion.Enabled = !ValidarSaldo;
                foreach (DataGridViewRow item in dgvDetails.Rows)
                {
                    var element = (ListarDetalleDocumento)item.DataBoundItem;
                    element.Devolver = true;
                }
            }
            else
            {
                btnDevolucion.Enabled = !ValidarSaldo;
                foreach (DataGridViewRow item in dgvDetails.Rows)
                {
                    var element = (ListarDetalleDocumento)item.DataBoundItem;
                    element.Devolver = false;
                }
            }            
        }

        private void dgvDevolucionesProducto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                var fila = dgvDevolucionesProducto.Rows[e.RowIndex];
                var devolucion = (ListarDevoluciones)fila.DataBoundItem;
                var detalleproductos = _detalleProductoRepository.GetListNoDocumento(devolucion.Documento);
                if (detalleproductos.Count > 0)
                {
                    List<DetalleProducto> details = detalleproductos.Where(x => x.ProductoId == devolucion.ProductoId).ToList();
                    if (details.Count() > 0)
                    {
                        string detalle = details.FirstOrDefault().TipoDetalle;
                        ActivarCombos(details, detalle);
                        TieneDetalle = true;
                    }
                    else
                    {
                        int dev = Convert.ToInt32(devolucion.DetalleDocumentoId);
                        var detallesol = _solicitudesRepository.GetDetalleSolicitud(dev);
                        lbCantProdVenta.Text = detallesol.Cantidad.ToString();
                        TieneDetalle = false;
                    }
                }
                else
                {
                    int id = Convert.ToInt32(devolucion.DetalleDocumentoId); 
                    var detallesol = _solicitudesRepository.GetDetalleSolicitud(id);
                    lbCantProdVenta.Text = detallesol.Cantidad.ToString();
                    TieneDetalle = false;
                }
            }
        }

        private void ActivarCombos(List<DetalleProducto> listado, string detalle)
        {
            TipoDetalle = detalle;
            switch (detalle)
            {
                case "Color":
                    cbColor.DataSource = listado;
                    cbColor.ValueMember = "Id";
                    cbColor.DisplayMember = "Detalle";
                    cbColor.Enabled = true;
                    cbTalla.Enabled = false;
                    cbTomo.Enabled = false;
                    cbEdicion.Enabled = false;
                    break;
                case "Talla":
                    cbTalla.DataSource = listado;
                    cbTalla.ValueMember = "Id";
                    cbTalla.DisplayMember = "Detalle";
                    cbColor.Enabled = false;
                    cbTalla.Enabled = true;
                    cbTomo.Enabled = false;
                    cbEdicion.Enabled = false;
                    break;
                case "Tomo":
                    cbTomo.DataSource = listado;
                    cbTomo.ValueMember = "Id";
                    cbTomo.DisplayMember = "Detalle";
                    cbColor.Enabled = false;
                    cbTalla.Enabled = false;
                    cbTomo.Enabled = true;
                    cbEdicion.Enabled = false;
                    break;
                case "Edicion":
                    cbEdicion.DataSource = listado;
                    cbEdicion.ValueMember = "Id";
                    cbEdicion.DisplayMember = "Detalle";
                    cbColor.Enabled = false;
                    cbTalla.Enabled = false;
                    cbTomo.Enabled = false;
                    cbEdicion.Enabled = true;
                    break;
                default:
                    cbColor.Enabled = false;
                    cbTalla.Enabled = false;
                    cbTomo.Enabled = false;
                    cbEdicion.Enabled = false;
                    cbColor.Text = "";
                    cbTalla.Text = "";
                    cbTomo.Text = "";
                    cbEdicion.Text = "";
                    txtCantidad.Text = "";
                    lbCantProdVenta.Text = "0";
                    break;
            }
        }

        private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            var color = (DetalleProducto)cbColor.SelectedItem;
            lbCantProdVenta.Text = color.Cantidad.ToString();
        }

        private void cbTalla_SelectedIndexChanged(object sender, EventArgs e)
        {
            var talla = (DetalleProducto)cbTalla.SelectedItem;
            lbCantProdVenta.Text = talla.Cantidad.ToString();
        }

        private void cbTomo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tomo = (DetalleProducto)cbTomo.SelectedItem;
            lbCantProdVenta.Text = tomo.Cantidad.ToString();
        }

        private void cbEdicion_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Edicion = (DetalleProducto)cbEdicion.SelectedItem;
            lbCantProdVenta.Text = Edicion.Cantidad.ToString();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCantidad.Text))
            {
                KryptonMessageBox.Show("Cantidad Inválida", "Notificacion");
            }
            else
            {
                int cantidadVenta = Convert.ToInt32(lbCantProdVenta.Text);
                int cantidadIngreso = Convert.ToInt32(txtCantidad.Text);

                if (cantidadIngreso > cantidadVenta)
                {
                    KryptonMessageBox.Show("Cantidad Ingresada mayor a cantidad de venta", "Notificacion");
                }
                else
                {
                    var fila = dgvDevolucionesProducto.CurrentRow;
                    if (fila == null)
                    {
                        KryptonMessageBox.Show("¡Seleccione un producto!", "Notificacion");
                    }
                    else
                    {
                        if (TieneDetalle)
                        {
                            ValidarDetalle(cantidadIngreso, fila.Index);
                        }
                        else
                        {
                            _devoluciones.ElementAt(fila.Index).Cantidad = cantidadIngreso;
                            CargarDGVProdDevolucion(_devoluciones);
                        }
                    }                    
                }    
            }
            txtCantidad.Text = "";
            lbCantProdVenta.Text = "0";
        }

        private void ValidarDetalle(int cantidad, int indice)
        {            
            DetalleProducto detailprod = null;
            bool agregarElemento = true;
            ListarDevoluciones dvo = null;
            switch (TipoDetalle) 
            {
                case "Color":
                    detailprod = (DetalleProducto)cbColor.SelectedItem;
                    break;
                case "Talla":
                    detailprod = (DetalleProducto)cbTalla.SelectedItem;
                    break;
                case "Tomo":
                    detailprod = (DetalleProducto)cbTomo.SelectedItem;
                    break;
                case "Edicion":
                    detailprod = (DetalleProducto)cbEdicion.SelectedItem;
                    break;
            }

            foreach (DataGridViewRow item in dgvDevolucionesProducto.Rows)
            {
                var elemento = (ListarDevoluciones)item.DataBoundItem;
                if (elemento.TipoDetalleId == detailprod.DetalleId)
                {
                    agregarElemento = false;
                }
                else if (elemento.TipoDetalleId == 0)
                {
                    agregarElemento = false;
                }
                else if (elemento.TipoDetalleId == detailprod.DetalleId && elemento.ProductoId == detailprod.ProductoId && elemento.Documento == detailprod.noDocumento)
                {
                    dvo = elemento;
                }
            }
            if (dvo == null)
            {
                dvo = _devoluciones.ElementAt(indice);
            }
            if (agregarElemento)
            {
                ListarDevoluciones nuevo = new ListarDevoluciones 
                {
                    DetalleDocumentoId = dvo.DetalleDocumentoId,
                    ProductoId = dvo.ProductoId,
                    Documento = dvo.Documento,
                    Descripcion = dvo.Descripcion,
                    TipoDetalle = detailprod.TipoDetalle,
                    Detalle = detailprod.Detalle,
                    TipoDetalleId = detailprod.DetalleId,
                    Cantidad = cantidad,
                    Fecha = dtpFechaDevolucion.Value
                };
                _devoluciones.Add(nuevo);               
            }
            else
            {
                dvo.Cantidad = cantidad;
                dvo.TipoDetalle = detailprod.TipoDetalle;
                dvo.Detalle = detailprod.Detalle;
                dvo.TipoDetalleId = detailprod.DetalleId;
            }
            CargarDGVProdDevolucion(_devoluciones);
            ActivarCombos(new List<DetalleProducto>(), "");
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                if (_cajasRepository.GetCajaAperturada(sucursalId) == null)
                {
                    KryptonMessageBox.Show("No hay ninguna caja aperturada en esta sucursal");
                    return;
                }

                if (string.IsNullOrEmpty(txtNombreDev.Text))
                {
                    KryptonMessageBox.Show("No ha ingresado un valor para nombre.", "Notificación");
                    return;
                }
                int itemsCantidadCero = 0;
                foreach (ListarDevoluciones item in _devoluciones)
                {
                    if (item.Cantidad == 0)
                        itemsCantidadCero++;
                }

                if (itemsCantidadCero > 0)
                {
                    KryptonMessageBox.Show("Existen productos a devolver con cantidad 0", "Notificación");
                    return;
                }

                foreach (ListarDevoluciones item in _devoluciones)
                {
                    if (item.Documento.Contains("VCR"))
                    {
                        int id = Convert.ToInt32(item.DetalleDocumentoId);
                        var detallesolicitud = _solicitudesRepository.GetDetalleSolicitud(id);
                        var detalles = _detalleProductoRepository.GetListNoDocumento(item.Documento);
                        var Devolucion = GetModelDevolucion(item, detallesolicitud, item.Documento);

                      
                        _cuenta.SaldoActual -= Devolucion.SubTotal;
                        _cuentasCRepository.Update(_cuenta);
                        var cajaObtenida = _cajasRepository.GetCajaAperturada(sucursalId);
                        var detalleCaja = new DetalleCaja
                        {
                            Descripcion = "Devolucion " + Devolucion.TipoDocumento,
                            CajaId = cajaObtenida.Id,
                            Efectivo = Devolucion.SubTotal
                        };
                        _cajasRepository.AddDetalleCaja(detalleCaja);

                        if (!ModelState.IsValid(Devolucion)) { return; }
                        _devolucionesRepository.Add(Devolucion);

                        if (detalles.Count() > 0)
                        {
                            detalles = detalles.Where(x => x.ProductoId == item.ProductoId).ToList();
                            if (detalles.Count() > 0)
                            {
                                detalles = detalles.Where(x => x.DetalleId == item.TipoDetalleId).ToList();
                            }
                        }

                        if (detalles.Count() > 0)
                        {
                            var detalleproducto = detalles.FirstOrDefault();
                            detalleproducto.Cantidad -= item.Cantidad;
                            _detalleProductoRepository.Update(detalleproducto);
                        }

                        if (detallesolicitud != null)
                        {
                            var details = _devoluciones.Where(x => x.DetalleDocumentoId == item.DetalleDocumentoId);
                            int totDev = details.Sum(x => x.Cantidad);
                            if (detallesolicitud.Cantidad == totDev)
                            {
                                detallesolicitud.EstadoDevolucion = true;
                                _solicitudesRepository.UpdateDetalle(detallesolicitud);
                            }
                            else
                            {
                                detallesolicitud.Cantidad -= item.Cantidad;
                                detallesolicitud.SubTotal = item.Cantidad * detallesolicitud.Precio;
                                detallesolicitud.PrecioTotal = detallesolicitud.SubTotal;
                                _solicitudesRepository.UpdateDetalle(detallesolicitud);
                            }
                        }

                        var producto = _productosRepository.Get((int)Devolucion.ProductoId);
                        producto.Stock += Devolucion.Cantidad;
                        _productosRepository.Update(producto);


                        var listadodetalles = _solicitudesRepository.GetDetallebySolicitud(detallesolicitud.SolicitudToFacturarId);
                        if(listadodetalles.Count() > 0)
                        {
                            listadodetalles = listadodetalles.Where(x => x.EstadoDevolucion == null || x.EstadoDevolucion == false).ToList();
                            if (listadodetalles.Count() == 0)
                            {
                                var solicitud = _solicitudesRepository.Get(detallesolicitud.SolicitudToFacturarId);
                                solicitud.Estado = true;
                                _solicitudesRepository.Update(solicitud);
                            }
                        }
                    }
                }

                _devoluciones.Clear();
                CargarDGVProdDevolucion(_devoluciones);
                ActivarCombos(new List<DetalleProducto>(), "");
                CargarDocumentoDGV(new List<ListarDetalleDocumento>());
                KryptonMessageBox.Show("Devoluciones realizadas con exito.", "Notificacion");

                knPagos.SelectedPage = pgCcliente;
                pgDevoluciones.Visible = false;
                this.Pagos_Load(null, null);
                checkAll.Checked = false;
                btnDevolucion.Enabled = false;
                lbSaldoProductos.Text = "0.00";
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Ha ocurrido un error. \n" + ex.Message, "Notificacion");
            }            
        }

        private Devoluciones GetModelDevolucion(ListarDevoluciones item, SolicitudDetalle solicitudDetalle, string tipo)
        {
            var producto = _productosRepository.Get((int)item.ProductoId);
            decimal descuento = 0.00m;
            if (solicitudDetalle.Descuento > 0.00m)
            {
                descuento = (solicitudDetalle.Descuento / solicitudDetalle.Cantidad) * item.Cantidad;
            }
            var devolucion = new Devoluciones()
            {
                DetalleColorId = Convert.ToInt32(item.DetalleDocumentoId),
                TipoDocumento = tipo,
                ProductoId = item.ProductoId,
                Descripcion = item.Descripcion,
                Cantidad = item.Cantidad,
                Precio = solicitudDetalle.Precio,
                Descuento = descuento,
                SubTotal = item.Cantidad * solicitudDetalle.Precio,
                FechaDevolucion = dtpFechaDevolucion.Value,
                Vendedor = UsuarioLogeadoSistemas.User.Name,
                Nombre = txtNombreDev.Text,
                Direccion = txtDireccionDev.Text,
                CuentaBCId = CuentaCBId
            };

            string TipoDetalle = item.TipoDetalle;

            switch (TipoDetalle)
            {
                case "Color":
                    devolucion.DetalleColorId = item.TipoDetalleId;
                    var color = _coloresRepository.GetDetalleColor(item.TipoDetalleId);
                    color.Stock += item.Cantidad;
                    _coloresRepository.Update(color);
                    break;
                case "Talla":
                    devolucion.DetalleTallaId = item.TipoDetalleId;
                    var talla = _tallasRepository.GetDetalleTalla(item.TipoDetalleId);
                    talla.Stock += item.Cantidad;
                    _tallasRepository.Update(talla);
                    break;
                case "Tomo":
                    devolucion.DetalleTomoEdicionId = item.TipoDetalleId;
                    var tomo = _tomoEdicionRepository.GetDetalleTE(item.TipoDetalleId);
                    tomo.Stock += item.Cantidad;
                    _tomoEdicionRepository.Update(tomo);
                    break;
                case "Edicion":
                    devolucion.DetalleTomoEdicionId = item.TipoDetalleId;
                    var edicion = _tomoEdicionRepository.GetDetalleTE(item.TipoDetalleId);
                    edicion.Stock += item.Cantidad;
                    _tomoEdicionRepository.Update(edicion);
                    break;
            }
            return devolucion;
        }


        private void CargarDGVProdDevolucion(List<ListarDevoluciones> listado)
        {
            dgvDevolucionesProducto.DataSource = typeof(List<>);
            dgvDevolucionesProducto.DataSource = listado;
            dgvDevolucionesProducto.Columns[0].Visible = false;
            dgvDevolucionesProducto.Columns[1].Visible = false;
            dgvDevolucionesProducto.Columns[6].Visible = false;
            dgvDevolucionesProducto.ClearSelection();
        }

        private void txtBuscarPagos_TextChanged(object sender, EventArgs e)
        {
            var filter = _notasDePago.Where(x => x.NoCuenta.Contains(txtBuscarPagos.Text) || x.NoDocumento.Contains(txtBuscarPagos.Text)
            || x.Descripcion.Contains(txtBuscarPagos.Text) || x.Responsable.Contains(txtBuscarPagos.Text));
            dgvNocredi.DataSource = typeof(List<>);
            dgvNocredi.DataSource = filter;
            dgvNocredi.Columns[0].Visible = false;
            dgvNocredi.Columns[7].Visible = false;
            dgvNocredi.Columns[8].Visible = false;
            dgvNocredi.Columns[9].Visible = false;
            dgvNocredi.Columns[10].Visible = false;
        }

        private void txtHistDevolucion_TextChanged(object sender, EventArgs e)
        {
            var filter = _histdevoluciones.Where(x => x.Descripcion.Contains(txtHistDevolucion.Text) || x.Nombre.Contains(txtHistDevolucion.Text)
            || x.TipoDocumento.Contains(txtHistDevolucion.Text) || x.Vendedor.Contains(txtHistDevolucion.Text));
            dgvDevoluciones.DataSource = typeof(List<>);
            dgvDevoluciones.DataSource = filter;
            dgvDevoluciones.Columns[0].Visible = false;
            dgvDevoluciones.Columns[1].Visible = false;
            dgvDevoluciones.Columns[3].Visible = false;
            dgvDevoluciones.Columns[13].Visible = false;
            dgvDevoluciones.Columns[14].Visible = false;
            dgvDevoluciones.Columns[15].Visible = false;
            dgvDevoluciones.Columns[16].Visible = false;
            dgvDevoluciones.Columns[17].Visible = false;
            dgvDevoluciones.ClearSelection();
        }
    }
}
