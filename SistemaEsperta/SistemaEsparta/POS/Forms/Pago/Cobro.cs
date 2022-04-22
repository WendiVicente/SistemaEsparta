﻿using CapaDatos.ListasPersonalizadas;
using CapaDatos.ListasPersonalizadas.VentasAcumuladas;
using CapaDatos.Models.Cotizacion;
using CapaDatos.Models.DocumentoSAT;
using CapaDatos.Models.Pedidos;
using CapaDatos.Models.Productos;
using CapaDatos.Models.Productos.Combos;
using CapaDatos.Models.ProductosToFacturar;
using CapaDatos.Models.Vales;
using CapaDatos.Repository;
using CapaDatos.Repository.SolicitudestoFacturar;
using CapaDatos.Validation;
using CapaDatos.WebServiceSAT;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Newtonsoft.Json;
using POS.Recibo;
using POS.Validations;
using QRCoder;
using sharedDatabase.Models;
using sharedDatabase.Models.Caja;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebApi;

namespace POS.Forms
{
    public partial class Cobro : BaseContext
    {
        private readonly CajasRepository _cajasRepository = null;
        private readonly DetallePagoRepository _detallePagoRepostitory = null;
        private readonly VentasRepository _ventasRepository = null;
        private readonly FacturasRepository _facturasRepository = null;
        private readonly ClientesRepository _clientesRepository = null;
        private readonly ProductosRepository _productosRepository = null;
        private readonly SolicitudesRepository _solicitudesRepository = null;
        private readonly CotizacionRepository _cotizacionRepository = null;
        private readonly ValesRepository _valesRepository = null;
        private readonly CertificadoSatRepository _certificadoSatRepository = null;
        private readonly DetalleProductoRepository _detalleProductoRepository = null;
        private readonly ProductosTempRepository _productosTempRepository = null;
        private readonly TomoEdicionRepository _tomoEdicionRepository = null;
        private readonly ColoresRepository _coloresRepository = null;
        private readonly TallasRepository _tallasRepository = null;
        private readonly TallasyColoresRepository _tallasycoloresRepository = null;
        private readonly CombosRepository _combosRepository = null;

        List<ListaFacturacion> _listadoFactura = null;
        List<DetalleFactura> _listadoDetalleFacturas = null;
        MonitorFacturacion _monitor = null;
        int sucursalId = UsuarioLogeadoPOS.User.SucursalId;
        private DocumentoCertificadoSAT DocCertificado = null;
        private TokenSAT TokenObtenidoSat;
        private PuntoDeVenta _pos = null;
        int entorno = 1;
        EmisorFEL emi;


        public Cobro(List<ListaFacturacion> listado, MonitorFacturacion monitor, PuntoDeVenta pos)
        {
            _cajasRepository = new CajasRepository(_context);
            _detallePagoRepostitory = new DetallePagoRepository(_context);
            _ventasRepository = new VentasRepository(_context);
            _facturasRepository = new FacturasRepository(_context);
            _clientesRepository = new ClientesRepository(_context);
            _productosRepository = new ProductosRepository(_context);
            _solicitudesRepository = new SolicitudesRepository(_context);
            _cotizacionRepository = new CotizacionRepository(_context);
            _valesRepository = new ValesRepository(_context);
            _certificadoSatRepository = new CertificadoSatRepository(_context);
            _detalleProductoRepository = new DetalleProductoRepository(_context);
            _productosTempRepository = new ProductosTempRepository(_context);
            _tomoEdicionRepository = new TomoEdicionRepository(_context);
            _coloresRepository = new ColoresRepository(_context);
            _tallasRepository = new TallasRepository(_context);
            _tallasycoloresRepository = new TallasyColoresRepository(_context);
            _combosRepository = new CombosRepository(_context);
            _monitor = monitor;
            _listadoFactura = listado;
            _pos = pos;
            _listadoDetalleFacturas = new List<DetalleFactura>();
            TokenObtenidoSat = new TokenSAT();
            emi = new EmisorFEL(entorno);
            InitializeComponent();
        }


        private void Cobro_Load(object sender, EventArgs e)
        {
            CargarCombo();
            CargarTamaño();
            CargarDGV(_listadoFactura);
            TokenObtenidoSat = JsonConvert.DeserializeObject<TokenSAT>(TokenPOST.GetToken());


        }

        private void checkBoxDPago_CheckedChanged(object sender, EventArgs e)
        {
            grupmontos.Visible = checkDividirPago.Checked;
        }

        private void CargarCombo()
        {
            List<string> tipoPago = new List<string>();
            tipoPago.Add("Efectivo");
            tipoPago.Add("Cheque");
            tipoPago.Add("Tarjeta Debito");
            tipoPago.Add("Tarjeta Credito");
            tipoPago.Add("Boleta Deposito");
            tipoPago.Add("Transferencia");
            cbTipoPago.DataSource = tipoPago;
            cbTipoPago.SelectedIndex = 0;
        }
        private void CargarTamaño()
        {
            List<string> tipotamaño = new List<string>();
            tipotamaño.Add("Ticket");
            tipotamaño.Add("Carta");
            cmbTamañoFactura.DataSource = tipotamaño;
            cmbTamañoFactura.SelectedIndex = 0;
        }
        private void FacturarPedido(Guid IdFact)
        {
            //facturar
            try
            {
                foreach (ListaFacturacion item in _listadoFactura)
                {
                    int tipo = item.idTipoDocumento;
                    switch (tipo)
                    {
                        case 16:
                        case 17:
                            FacturarSolicitud(item.idDocumento, IdFact);
                            break;
                        case 21:
                            FacturarCotizacion(item.idDocumento, IdFact);
                            break;
                        case 22:
                            FacturarVale(item.idDocumento, IdFact);
                            break;
                    }
                }

            }
            catch
            {
                MessageBox.Show("Se produjo un erro interno al obtener\n la información.", "Notificación");
            }
        }

        private void FacturarSolicitud(Guid ID, Guid idFact)
        {
            var solicitudes = _solicitudesRepository.GetDetallebySolicitud(ID);
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

        private void FacturarCotizacion(Guid ID, Guid idFact)
        {
            DetalleFactura factura = new DetalleFactura();
            var cotizaciones = _cotizacionRepository.GetDetalleCotizacionslista(ID);
            foreach (var item in cotizaciones)
            {
                var producto = _productosRepository.Get((int)item.ProductoId);
                var detalle = new DetalleFactura()
                {
                    FacturaId = idFact,
                    Cantidad = item.Cantidad,
                    Precio = item.PrecioTotal / item.Cantidad,
                    Descuento = item.Descuento,
                    PrecioTotal = item.PrecioTotal
                };

                if (item.ProductoId != 0)
                {
                    detalle.ProductoId = producto.Id;
                }
                if (item.DetalleColorId != 0)
                {
                    detalle.DetalleColorId = item.DetalleColorId;
                }
                if (item.DetalleTallaId != 0)
                {
                    detalle.DetalleTallaId = item.DetalleTallaId;
                }
                if (item.DetalleTomoEdicionId != 0)
                {
                    detalle.DetalleTomoEdicionId = item.DetalleTomoEdicionId;
                }
                if (item.ProductoId == 0)
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

        private void FacturarVale(Guid ID, Guid idFact)
        {
            DetalleFactura factura = new DetalleFactura();
            var asigVale = _valesRepository.GetAsignacionVale(ID).FirstOrDefault();
            var vales = _valesRepository.GetListadoDetalleVales(asigVale.Id);
            foreach (var item in vales)
            {
                var producto = _productosRepository.Get((int)item.ProductoId);
                var detalle = new DetalleFactura()
                {
                    FacturaId = idFact,
                    Cantidad = item.Cantidad,
                    Precio = item.precio,
                    Descuento = item.Descuento,
                    PrecioTotal = item.PrecioTotal
                };

                if (item.ProductoId != 0)
                {
                    detalle.ProductoId = producto.Id;
                }
                if (item.DetalleColorId != 0)
                {
                    detalle.DetalleColorId = item.DetalleColorId;
                }
                if (item.DetalleTallaId != 0)
                {
                    detalle.DetalleTallaId = item.DetalleTallaId;
                }
                if (item.DetalleTomoEdicionId != 0)
                {
                    detalle.DetalleTomoEdicionId = item.DetalleTomoEdicionId;
                }
                if (item.ProductoId == 0)
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

        private void CargarDGV(List<ListaFacturacion> listado)
        {
            decimal total = 0.00m;
            decimal valor = 0.00m;
            foreach (var item in listado)
            {
                dgvDatosFacturacion.Rows.Add(new DataGridViewRow());
                int indice = dgvDatosFacturacion.Rows.Count - 1;
                var data = dgvDatosFacturacion.Rows[indice];

                data.Cells[0].Value = cbTipoPago.Text;
                data.Cells[1].Value = item.Documento;
                switch (item.idTipoDocumento)
                {
                    case 16:
                    case 17:
                        valor = _monitor.ObtenerTotalSol(item.idDocumento);
                        data.Cells[2].Value = "Q " + valor;
                        total += valor;
                        break;
                    case 21:
                        valor = _monitor.ObtenerTotalCot(item.idDocumento);
                        data.Cells[2].Value = "Q " + valor;
                        total += valor;
                        break;
                    case 22:
                        var Asigvale = _valesRepository.GetAsignacionVale(item.idDocumento).FirstOrDefault();
                        valor = _monitor.ObtenerTotalVal(Asigvale.Id);
                        data.Cells[2].Value = "Q " + valor;
                        total += valor;
                        break;
                }
            }
            txtTotalCobrado.Text = "Q " + total.ToString();
            txtTotalCobrar.Text = "Q " + total.ToString();
            txtCantidadValor.Text = total.ToString();
            dgvDatosFacturacion.ClearSelection();
        }

        private void cbTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoPago = cbTipoPago.Text;
            switch (tipoPago)
            {
                case "Efectivo":
                    MostrarCampos(false);
                    TipoPagoDocumento(tipoPago);
                    txtEfectivo.Text = "EFECTIVO:  Q";
                    break;
                case "Cheque":
                    MostrarCampos(true);
                    TipoPagoDocumento(tipoPago);
                    lbNumero.Text = "NÚMERO CHEQUE:";
                    lbFecha.Text = "FECHA COBRO:";
                    break;
                case "Tarjeta Debito":
                    txtEfectivo.Text = "MONTO:  Q";
                    TipoPagoDocumento(tipoPago);
                    MostrarCampos(false);
                    break;
                case "Tarjeta Credito":
                    txtEfectivo.Text = "MONTO:  Q";
                    TipoPagoDocumento(tipoPago);
                    MostrarCampos(false);
                    break;
                case "Boleta Deposito":
                    MostrarCampos(true);
                    TipoPagoDocumento(tipoPago);
                    lbNumero.Text = "NÚMERO BOLETA:";
                    lbFecha.Text = "FECHA DEPÓSITO:";
                    break;
                case "Transferencia":
                    MostrarCampos(true);
                    TipoPagoDocumento(tipoPago);
                    lbNumero.Text = "NÚMERO BOLETA:";
                    lbFecha.Text = "FECHA DEPÓSITO:";
                    break;
            }
        }

        private void MostrarCampos(bool mostrar)
        {
            tblDetalleEntidad.Visible = mostrar;
            txtNumero.Visible = mostrar;
            txtEntidad.Visible = mostrar;
            dtpFecha.Visible = mostrar;
        }

        private void pbCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvDatosFacturacion_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var fila = dgvDatosFacturacion.CurrentRow;
            string Documento = fila.Cells[1].Value.ToString();
            int indice = 0;
            int contador = 0;
            foreach (var item in _listadoFactura)
            {
                if (item.Documento == Documento)
                {
                    indice = contador;
                }
                contador++;
            }
            _listadoFactura.RemoveAt(indice);
        }

        private void pbAceptarCobro_Click(object sender, EventArgs e)
        {
            if (ValidarMontos())
            {
                if (checkStorageFact.Checked)
                {
                    GuardarATemporales();
                }
                else
                {
                    GuardarCambios();
                    _listadoDetalleFacturas = new List<DetalleFactura>();
                }
            }
        }

        private void txtCantidadEfectivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtCantidadEfectivo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtCantidadTotal_TextChanged(object sender, EventArgs e)
        {
            CargarSaldos();
        }

        private void CargarSaldos()
        {
            decimal montoIngresado = 0.00m;
            decimal pagoTotal = 0.00m;
            decimal validarVuelto = 0.00m;
            if (string.IsNullOrEmpty(txtCantidadTotal.Text))
            {
                txtCantidadVuelto.Text = "0.00";
                txtCantidadVuelto.BackColor = Color.White;
                return;
            }

            montoIngresado = decimal.Parse(txtCantidadTotal.Text.ToString());
            pagoTotal = decimal.Parse(txtCantidadValor.Text.ToString());

            if (!checkDividirPago.Checked)
            {
                validarVuelto = montoIngresado - pagoTotal;
                txtCantidadVuelto.Text = validarVuelto.ToString();

                if (validarVuelto < 0)
                {
                    txtCantidadVuelto.BackColor = Color.Red;
                    txtCantidadVuelto.ForeColor = Color.Black;
                }
                if (validarVuelto >= 0)
                {
                    txtCantidadVuelto.BackColor = Color.White;
                    txtCantidadVuelto.ForeColor = Color.Black;
                }
            }
        }

        private void TipoPagoDocumento(string tipoPago)
        {
            int filas = dgvDatosFacturacion.RowCount;
            if (filas > 0)
            {
                foreach (DataGridViewRow row in dgvDatosFacturacion.Rows)
                {
                    row.Cells[0].Value = tipoPago;
                }
            }
        }

        private void GuardarATemporales()
        {
            try
            {
                foreach (var item in _listadoFactura)
                {
                    switch (item.idTipoDocumento)
                    {
                        case 16:
                        case 17:
                            var detallesolicitud = _solicitudesRepository.GetDetallebySolicitud((Guid)item.idDocumento);
                            foreach (var solicitud in detallesolicitud)
                            {
                                var producto = _productosRepository.Get((int)solicitud.ProductoId);
                                var producTemps = NuevoProductosTempBySolicitud(producto, solicitud);
                                _productosTempRepository.Add(producTemps, true);
                                ValidarExistencias(new DetalleFactura { Cantidad = solicitud.Cantidad, ProductoId = solicitud.ProductoId }, item.Documento, true);
                            }
                            break;
                        case 21:
                            var detallecotizacion = _cotizacionRepository.GetListDetalleCotiz((Guid)item.idDocumento);
                            foreach (var cotizacion in detallecotizacion)
                            {
                                var producto = _productosRepository.Get((int)cotizacion.ProductoId);
                                var producTemps = NuevoProductosTempByCotizacion(producto, cotizacion);
                                _productosTempRepository.Add(producTemps, true);
                                ValidarExistencias(new DetalleFactura { Cantidad = cotizacion.Cantidad, ProductoId = cotizacion.ProductoId }, item.Documento, true);
                            }
                            break;
                        case 22:
                            var asignVale = _valesRepository.GetAsignacionVale((Guid)item.idDocumento).FirstOrDefault();
                            var detallesvales = _valesRepository.GetListadoDetalleVales(asignVale.Id);
                            foreach (var vale in detallesvales)
                            {
                                var producto = _productosRepository.Get((int)vale.ProductoId);
                                var producTemps = NuevoProductosTempByVale(producto, vale);
                                _productosTempRepository.Add(producTemps, true);
                                ValidarExistencias(new DetalleFactura { Cantidad = vale.Cantidad, ProductoId = vale.ProductoId }, item.Documento, true);
                            }
                            break;
                    }
                }

                foreach (var item in _listadoFactura)
                {
                    var productoDetalle = _detalleProductoRepository.GetListNoDocumento(item.Documento);
                    foreach (var elemento in productoDetalle)
                    {
                        elemento.FechaDocumento = DateTime.Now;
                        elemento.noDocumento = "TMP";
                        _detalleProductoRepository.Update(elemento);
                    }
                    ActualizarDocumento(item);
                }

                MessageBox.Show("Datos Guardados", "Notificación");
                _monitor.Close();
                MonitorFacturacion monitor = new MonitorFacturacion(_pos);
                monitor.Show();
                Close();



            }
            catch
            {
                MessageBox.Show("No se han podido guardar los cambios.", "ERROR");
            }


        }

        private TemporalProductos NuevoProductosTempBySolicitud(Producto producto, SolicitudDetalle item)
        {
            var temporalP = new TemporalProductos()
            {
                //FacturaId = idfact,
                Cantidad = item.Cantidad,
                Precio = item.Precio,
                Descuento = item.Descuento,
                SubTotal = (item.Cantidad * item.Precio),
                PrecioTotal = (item.Cantidad * item.Precio) - item.Descuento,
            };

            if (item.ProductoId != 0)
            {
                temporalP.ProductoId = producto.Id;
            }
            if (item.ProductoId == 0)
            {
                temporalP.ComboId = item.ComboId;
            }

            return temporalP;
        }

        private TemporalProductos NuevoProductosTempByCotizacion(Producto producto, ListarDetalleCotiz item)
        {
            var temporalP = new TemporalProductos()
            {
                //FacturaId = idfact,
                Cantidad = item.Cantidad,
                Precio = item.Precio,
                Descuento = item.Descuento,
                SubTotal = (item.Cantidad * item.Precio),
                PrecioTotal = (item.Cantidad * item.Precio) - item.Descuento,
            };

            if (item.ProductoId != 0)
            {
                temporalP.ProductoId = producto.Id;
            }
            if (item.ProductoId == 0)
            {
                temporalP.ComboId = item.ComboId;
            }

            return temporalP;
        }

        private TemporalProductos NuevoProductosTempByVale(Producto producto, DetalleVale item)
        {
            var temporalP = new TemporalProductos()
            {
                //FacturaId = idfact,
                Cantidad = item.Cantidad,
                Precio = item.precio,
                Descuento = item.Descuento,
                SubTotal = (item.Cantidad * item.precio),
                PrecioTotal = (item.Cantidad * item.precio) - item.Descuento,
            };

            if (item.ProductoId != 0)
            {
                temporalP.ProductoId = producto.Id;
            }
            if (item.ProductoId == 0)
            {
                temporalP.ComboId = item.ComboId;
            }

            return temporalP;
        }

        private void GuardarCambios()
        {
            try
            {
                if (_cajasRepository.GetCajaAperturada(sucursalId) != null)
                {
                    var cajaObtenida = _cajasRepository.GetCajaAperturada(sucursalId);
                    List<Factura> listadofacturas = new List<Factura>();
                    string tipopago = cbTipoPago.Text;
                    foreach (var item in _listadoFactura)
                    {
                        var modelFactura = GetViewModel(item.ClienteId);
                        var detalleCaja = new DetalleCaja
                        {
                            Descripcion = tipopago,
                            CajaId = cajaObtenida.Id,
                            FacturaId = modelFactura.Id,
                        };
                        SetearMontos(detalleCaja);

                        bool validarFact = ModelState.IsValid(modelFactura);
                        bool validarCaja = ModelState.IsValid(detalleCaja);
                        if (validarCaja && validarFact)
                        {
                            int tipo = item.idTipoDocumento;
                            switch (tipo)
                            {
                                case 16:
                                case 17:
                                    FacturarSolicitud(item.idDocumento, modelFactura.Id);
                                    break;
                                case 21:
                                    FacturarCotizacion(item.idDocumento, modelFactura.Id);
                                    break;
                                case 22:
                                    FacturarVale(item.idDocumento, modelFactura.Id);
                                    break;
                            }

                            if (tipopago == "Cheque" || tipopago == "Boleta Deposito" || tipopago == "Transferencia")
                            {
                                var detallePago = GetModelPago(modelFactura);
                                _detallePagoRepostitory.Add(detallePago);
                            }
                            _ventasRepository.Add(modelFactura);
                            _cajasRepository.AddDetalleCaja(detalleCaja);

                            var detallesDoc = _detalleProductoRepository.GetListNoDocumento(item.Documento);
                            foreach (var detalle in detallesDoc)
                            {
                                detalle.noDocumento = modelFactura.NoFactura;
                                detalle.FechaDocumento = modelFactura.FechaVenta;
                                _detalleProductoRepository.Update(detalle);
                            }

                            if (_listadoDetalleFacturas.Count() > 0)
                            {
                                foreach (var detalle in _listadoDetalleFacturas)
                                {
                                    _facturasRepository.Add(detalle, true);
                                    if (item.Facturar)
                                    {
                                        ValidarExistencias(detalle, modelFactura.NoFactura, true);
                                    }
                                    else
                                    {
                                        ValidarExistencias(detalle, modelFactura.NoFactura, true);
                                    }
                                }
                            }
                            ActualizarDocumento(item);
                            listadofacturas.Add(modelFactura);
                            _listadoDetalleFacturas.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Se han encontrado datos inválidos.", "ERROR");
                        }
                    }
                    MessageBox.Show("Venta Registrada con éxito.", "Notificación");
                    _monitor.Close();
                    MonitorFacturacion monitor = new MonitorFacturacion(_pos);
                    monitor.Show();
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
                Vendedor = UsuarioLogeadoPOS.User.Name,
                TipoPago = cbTipoPago.Text,
                UserId = UsuarioLogeadoPOS.User.Id,
            };
        }

        private string Crearcorrelativo()
        {
            var cliente = _facturasRepository.GetLastFact();
            return ObtenerNumero(cliente, "FC-");
        }

        private string ObtenerNumero(string Solic, string CL)
        {
            string numero = "";
            if (Solic.Length > 0)
            {
                int maxsol = Convert.ToInt32(Solic.Split('-')[1]) + 1;
                if (maxsol < 10)
                    numero = CL + "00" + maxsol;
                else if (maxsol < 100)
                    numero = CL + "0" + maxsol;
                else
                    numero = CL + maxsol;
            }
            else
            {
                numero = CL + "001";
            }
            return numero;
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

        private DetallePago GetModelPago(Factura factura)
        {
            return new DetallePago
            {
                Documento = factura.NoFactura,
                Pago = cbTipoPago.Text,
                Entidad = txtEntidad.Text,
                Numero = txtNumero.Text,
                Monto = Convert.ToDecimal(txtCantidadTotal.Text),
                Fecha = dtpFecha.Value
            };
        }

        private bool ValidarMontos()
        {
            bool validacion;
            var montoTotalaPagar = decimal.Parse(txtCantidadTotal.Text);
            if (checkDividirPago.Checked)
            {
                decimal montoMontos = decimal.Parse(txtCantidadTotal.Text);
                if (montoMontos < montoTotalaPagar)
                {
                    MessageBox.Show("¡Monto ingresado es menor al Total a Pagar!", "Advertencia");
                    validacion = false;
                }
                //else if (montoMontos > montoTotalaPagar)
                //{
                //    MessageBox.Show("¡La sumatoria es mayor a lo pagado! \npor favor validar montos.", "Advertencia");
                //    validacion = false;
                //}
                else
                {
                    validacion = true;
                }
            }
            else
            {
                decimal vuelto = decimal.Parse(txtCantidadVuelto.Text);
                if (vuelto < 0)
                {
                    MessageBox.Show("¡Monto ingresado es menor al Total a Pagar!", "Notificación");
                    validacion = false;
                }
                else
                {
                    validacion = true;
                }
            }
            return validacion;
        }
        public int validarTallaColor(Producto producto)
        {
            int value = 0;
            if (producto.TieneColor == true && producto.TieneTalla == false)
            {
                //tiene solamente color
                value = 1;
            }
            else if (producto.TieneColor == false && producto.TieneTalla == true)
            {
                //tiene solamente talla
                value = 2;
            }
            else if (producto.TieneColor == true && producto.TieneTalla == true)
            {
                //tiene talla y color
                value = 3;
            }
            else
            {
                //no tiene ninguno
                value = 0;
            }

            return value;
        }

        public bool ValidarExistencias(DetalleFactura detallefactura, string noDocumento, bool save = true)
        {
            int Cantidad = detallefactura.Cantidad;
            bool validacion = true;
            try
            {
                //validaciones de existencias para el producto
                if (detallefactura.ProductoId != 0)
                {
                    Producto producto = new Producto();
                    producto = _productosRepository.Get((int)detallefactura.ProductoId);
                    if (producto.Stock > producto.stockMinimo)
                    {
                        int _stockRestante = producto.Stock - producto.stockMinimo;
                        //valida si hay sufiente stock 
                        if (_stockRestante >= Cantidad)
                        {
                            //si se quiere actualizar en la bd save debe ser true
                            if (save)
                            {

                                int opc = validarTallaColor(producto);

                                switch (opc)
                                {
                                    case 1:
                                        var listaobtenidaDetalleColor = _coloresRepository.GetProductoListaColor(producto.Id);
                                        var detalleColorToLess = listaobtenidaDetalleColor.Where(x => x.Id == detallefactura.DetalleColorId).FirstOrDefault();
                                        validacion = actualizarStock(Cantidad, detalleColorToLess, producto, 1);
                                        break;
                                    case 2:
                                        var listTallabyProducto = _tallasRepository.GetTallaListaProducto(producto.Id);
                                        var detalleTallaToLess = listTallabyProducto.Where(x => x.Id == detallefactura.DetalleTallaId).FirstOrDefault();
                                        validacion = actualizarStock(Cantidad, detalleTallaToLess, producto, 2);
                                        break;
                                    case 3:
                                        var tallasyColores = _tallasycoloresRepository.GetTallaColorListaProducto(producto.Id);
                                        var colorytallatoLess = tallasyColores.Where(x => x.Id == detallefactura.DetalleColorTallaId).FirstOrDefault();
                                        validacion = actualizarStock(Cantidad, colorytallatoLess, producto, 3);
                                        break;
                                    default:
                                        validacion = actualizarStock(Cantidad, null, producto, 0);
                                        break;
                                }
                            }
                            //si no solo mostrara que si puede continuar pero que no seran acutalizada la info
                            else
                            {
                                validacion = true;
                            }
                        }
                        else
                        {
                            validacion = false;
                        }
                    }
                    else
                    {
                        validacion = false;
                    }
                }
                else
                {
                    validacion = false;
                }
            
            
                
            }
            catch (Exception io)
            {
                MessageBox.Show("Error en ValidarExistencias() " + io.Message);
                return false;
            }
            return validacion;

        }

        public bool actualizarStock(int Cantidad, Object detalle, Producto producto, int opc)
        {
            bool actualizado = false;
            if (detalle != null)
            {
                switch (opc)
                {
                    case 1:
                        var _detalleC = (DetalleColor)detalle;
                        _detalleC.Stock -= Cantidad;
                        _coloresRepository.Update(_detalleC);
                        producto.Stock -= Cantidad;
                        _productosRepository.Update(producto);
                        actualizado = true;
                        break;
                    case 2:
                        var _detalleT = (DetalleTalla)detalle;
                        _detalleT.Stock -= Cantidad;
                        _tallasRepository.Update(_detalleT);
                        producto.Stock -= Cantidad;
                        _productosRepository.Update(producto);
                        actualizado = true;
                        break;
                    case 3:
                        var _detalleTC = (DetalleTomoEdicion)detalle;
                        _detalleTC.Stock -= Cantidad;
                        _tomoEdicionRepository.Update(_detalleTC);
                        producto.Stock -= Cantidad;
                        _productosRepository.Update(producto);
                        actualizado = true;
                        break;
                    case 4:
                        var _combos = (Combo)detalle;
                        _combos.Stock -= Cantidad;
                        _combosRepository.Update(_combos);
                        actualizado = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (producto != null)
                {
                    producto.Stock -= Cantidad;
                    _productosRepository.Update(producto);
                    actualizado = true;
                }
                else
                {
                    actualizado = false;
                }
            }

            return actualizado;
        }

        private void ActualizarDocumento(ListaFacturacion item)
        {
            switch (item.idTipoDocumento)
            {
                case 16:
                    var solicitudc = _solicitudesRepository.Get(item.idDocumento);
                    solicitudc.Estado = true;
                    _solicitudesRepository.Update(solicitudc);
                    break;
                case 17:
                    var solicitud = _solicitudesRepository.Get(item.idDocumento);
                    solicitud.Estado = true;
                    _solicitudesRepository.Update(solicitud);
                    break;
                case 21:
                    var cotizacion = _cotizacionRepository.GetCotizacion(item.idDocumento);
                    cotizacion.Estado = true;
                    _cotizacionRepository.Updatecotizacion(cotizacion);
                    break;
                case 22:
                    var vale = _valesRepository.GetVale(item.idDocumento);
                    vale.IsActive = true;
                    _valesRepository.Update(vale);
                    var asigVale = _valesRepository.GetAsignacionVale(vale.Id).FirstOrDefault();
                    asigVale.Estado = "1";
                    _valesRepository.UpdateAsignacion(asigVale);
                    break;
            }
        }

        private void AbrirFactura(List<Factura> listadofacturas)
        {
            if (checkFEL.Checked)
            {
                if (cmbTamañoFactura.Text == "Carta")
                {
                    foreach (var item in listadofacturas)
                        AperturaReportecarta(item.Id);
                }
                else if(cmbTamañoFactura.Text=="Ticket")
                {
                    foreach (var item in listadofacturas)
                        AperturaReporteFEl(item.Id);
                }
                
            }
            if (checkRecibo.Checked)
            {
                foreach (var item in listadofacturas)
                    AperturaRecibo(item.Id);
            }
        }

        private void AperturaRecibo(Guid FacturaId)
        {
            ReciboVenta reciboVenta = new ReciboVenta(FacturaId, UsuarioLogeadoPOS.User.Name);
            reciboVenta.Show();
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
                MessageBox.Show("Se Alcanzado el limite de DTE.", "Notificación");
            }
        }
        private void AperturaReportecarta(Guid FacturaId)
        {
            generaFEL(FacturaId);

            if (DocCertificado != null)
            {
                if (cmbTamañoFactura.Text == "Carta")
                {

                    ReciboCliente facturaFELCarta = new ReciboCliente(FacturaId, DocCertificado,emi.EmisorFACT.direccionEmisor);
                    

                    facturaFELCarta.Show();

                }
            }
            else
            {
                MessageBox.Show("Se Alcanzado el limite de DTE. Carta", "Notificación");
            }
        }
        private void generaFEL(Guid guidFacturaid)
        {
            var factura = _facturasRepository.Get(guidFacturaid);
            var clienteformado = Getmodelcliente((int)factura.ClienteId);
            var listaDetallefactura = _facturasRepository.GetDetallebyFactura(guidFacturaid);
            var xmlValidado = ValidarXML.enviarxml(TokenObtenidoSat.Token, clienteformado, listaDetallefactura, factura.NoFactura);

            DocCertificado = JsonConvert.DeserializeObject<DocumentoCertificadoSAT>(xmlValidado);
            DateTime now = DateTime.Now;
            TimeSpan time = new TimeSpan(now.Hour, now.Minute, now.Second);
            DocCertificado.Fecha_DTE = now;
            if (DocCertificado == null) { return; }
            try
            {
                decimal totfactura = listaDetallefactura.Sum(x => x.PrecioTotal);
                string url = ObtenerURL(DocCertificado.Autorizacion, DocCertificado.NIT_COMPRADOR, totfactura);
                DocCertificado.FacturaId = guidFacturaid;

                QRCodeGenerator qrgenerador = new QRCodeGenerator();
                QRCodeData qrDatos = qrgenerador.CreateQrCode(url, QRCodeGenerator.ECCLevel.H);
                QRCode qrcodigo = new QRCode(qrDatos);
                Bitmap qrImagen = qrcodigo.GetGraphic(3, Color.Black, Color.White, false);
               
            
                DocCertificado.QRImagen = Convertir_Imagen_Bytes(qrImagen);
               
                _certificadoSatRepository.add(DocCertificado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static byte[] Convertir_Imagen_Bytes(System.Drawing.Image img)
        {


            string sTemp = Path.GetTempFileName();
            FileStream fs = new FileStream(sTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            img.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            fs.Position = 0;

            int imgLength = Convert.ToInt32(fs.Length);
            byte[] bytes = new byte[imgLength];
            fs.Read(bytes, 0, imgLength);
            fs.Close();
            return bytes;
        }

        private string ObtenerURL(string autorizacion, string nit_coomprador, decimal totfactura)
        {
            EmisorFEL emisor = new EmisorFEL(2);
            string url = "https://felpub.c.sat.gob.gt/verificador-web/publico/vistas/verificacionDte.jsf?tipo=autorizacion&numero=" + autorizacion + "&emisor=" + emisor.EmisorFACT.nitemisor + "&receptor=" + nit_coomprador + "&monto=" + totfactura;
            return url;
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

        private void SetearMontos(DetalleCaja detalleCaja)
        {
            if (checkDividirPago.Checked)
            {
                detalleCaja.Efectivo = txtefectivoMonto.Text.Trim() == "" ? 0.00M : decimal.Parse(txtefectivoMonto.Text);
                detalleCaja.Cheques = txtchequeMonto.Text.Trim() == "" ? 0.00M : decimal.Parse(txtchequeMonto.Text);
                detalleCaja.TarjetaCredito = txtcreditoMonto.Text.Trim() == "" ? 0.00M : decimal.Parse(txtcreditoMonto.Text);
                detalleCaja.TarjetaDebito = txtdebitoMonto.Text.Trim() == "" ? 0.00M : decimal.Parse(txtdebitoMonto.Text);
                detalleCaja.Transferencia = txttrasnferMonto.Text.Trim() == "" ? 0.00M : decimal.Parse(txttrasnferMonto.Text);
            }
            else
            {
                if (cbTipoPago.Text == "Efectivo") { detalleCaja.Efectivo = decimal.Parse(txtCantidadTotal.Text); }
                if (cbTipoPago.Text == "Cheque") { detalleCaja.Cheques = decimal.Parse(txtCantidadTotal.Text); }
                if (cbTipoPago.Text == "Tarjeta Debito") { detalleCaja.TarjetaDebito = decimal.Parse(txtCantidadTotal.Text); }
                if (cbTipoPago.Text == "Tarjeta Credito") { detalleCaja.TarjetaCredito = decimal.Parse(txtCantidadTotal.Text); }
                if (cbTipoPago.Text == "Boleta Deposito") { detalleCaja.Transferencia = decimal.Parse(txtCantidadTotal.Text); }
                if (cbTipoPago.Text == "Transferencia") { detalleCaja.Transferencia = decimal.Parse(txtCantidadTotal.Text); }
            }
        }

        private void txtefectivoMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtchequeMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtcreditoMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txttrasnferMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtdebitoMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtefectivoMonto_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtefectivoMonto.Text))
            {
                operacionSuma();
            }
        }

        private void txtchequeMonto_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtchequeMonto.Text))
            {
                operacionSuma();
            }
        }

        private void txtcreditoMonto_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtcreditoMonto.Text))
            {
                operacionSuma();
            }
        }

        private void txtdebitoMonto_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtdebitoMonto.Text))
            {
                operacionSuma();
            }
        }

        private void txttrasnferMonto_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txttrasnferMonto.Text))
            {
                operacionSuma();
            }
        }

        private void operacionSuma()
        {
            decimal totalValor = decimal.Parse(txtCantidadValor.Text);
            decimal totalIngresado = SumarTextBox();
            decimal totalVuelto = totalIngresado - totalValor;
            txtCantidadTotal.Text = totalIngresado.ToString("0.0");       
            txtCantidadVuelto.Text = totalVuelto.ToString();
            if (totalVuelto < 0)
            {
                txtCantidadVuelto.BackColor = Color.Red;
                txtCantidadVuelto.ForeColor = Color.White;
            }
            if (totalVuelto >= 0)
            {
                txtCantidadVuelto.BackColor = Color.White;
                txtCantidadVuelto.ForeColor = Color.Black;
            }
        }

        private decimal SumarTextBox() 
        {
            decimal lbv14 = txtefectivoMonto.Text.Trim() == "" ? 0.00M : decimal.Parse(txtefectivoMonto.Text);
            decimal lbv15 = txtchequeMonto.Text.Trim() == "" ? 0.00M : decimal.Parse(txtchequeMonto.Text);
            decimal lbv16 = txtcreditoMonto.Text.Trim() == "" ? 0.00M : decimal.Parse(txtcreditoMonto.Text);
            decimal lbv17 = txtdebitoMonto.Text.Trim() == "" ? 0.00M : decimal.Parse(txtdebitoMonto.Text);
            decimal lbv18 = txttrasnferMonto.Text.Trim() == "" ? 0.00M : decimal.Parse(txttrasnferMonto.Text);
            decimal totalToshow = lbv14 + lbv15 + lbv16 + lbv17 + lbv18;
            return totalToshow;
        }

        private void checkStorageFact_CheckedChanged(object sender, EventArgs e)
        {
            if (checkStorageFact.Checked)
            {
                checkFEL.Enabled = false;
                checkRecibo.Enabled = false;
            }
            else
            {
                checkFEL.Enabled = true;
                checkRecibo.Enabled = true;
            }
        }

        private void checkFEL_CheckedChanged(object sender, EventArgs e)
        {
            if (checkFEL.Checked)
            {
                checkRecibo.Enabled = false;
                checkStorageFact.Enabled = false;
                LbTamFact.Visible = true;
                cmbTamañoFactura.Visible = true;
            }
            else
            {
                checkRecibo.Enabled = true;
                checkStorageFact.Enabled = true;
                LbTamFact.Visible = false;
                cmbTamañoFactura.Visible = false;
            }
        }

        private void checkRecibo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkRecibo.Checked)
            {
                checkFEL.Enabled = false;
                checkStorageFact.Enabled = false;
            }
            else
            {
                checkFEL.Enabled = true;
                checkStorageFact.Enabled = true;
            }
        }

        private void cmbTamañoFactura_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pbImpresora_Click(object sender, EventArgs e)
        {

        }
    }
}
