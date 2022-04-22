using CapaDatos.ListasPersonalizadas;
using CapaDatos.Models.DocumentoSAT;
using CapaDatos.Repository;
using Microsoft.Reporting.WinForms;
using POS.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Recibo
{
    public partial class ReporteReciboCliente : BaseContext
    {
        private readonly FacturasRepository _facturasRepository = null;
        private readonly DetalleProductoRepository _detalleProductoRepository = null;
        private IList<ListarDetalleFacturas> listadetalles = null;
        private readonly DocumentoCertificadoSAT DocCertificado = null;
        private Guid idfactura;
        string numeroFactura;
        private String fechaemision;
        public ReporteReciboCliente(Guid idfac, DocumentoCertificadoSAT DocCertif)
        {
            idfactura = idfac;
            DocCertificado = DocCertif;
            _facturasRepository = new FacturasRepository(_context);
            _detalleProductoRepository = new DetalleProductoRepository(_context);
            fechaemision = DateTime.Now.ToString();
            InitializeComponent();
        }

        private void ReporteReciboCliente_Load(object sender, EventArgs e)
        {
             CargarTabla();
          ///  listadetalles = _facturasRepository.GetDetallebyFactura(idfactura);
            traerparametros();
            this.rvporte.RefreshReport();

            //  pagoForms.Close();
        }
        private void CargarTabla()
        {
            var encabezadoFactura = _facturasRepository.Get(idfactura);
            numeroFactura = encabezadoFactura.NoFactura;
            var detallesproducto = _detalleProductoRepository.GetListNoDocumento(encabezadoFactura.NoFactura);
            listadetalles = _facturasRepository.GetDetallebyFactura(idfactura);

            IList<ListarDetalleFacturas> listado = new List<ListarDetalleFacturas>();
            if (detallesproducto.Count > 0)
            {
                foreach (var item in listadetalles)
                {
                    string detalles = "";
                    var details = detallesproducto.Where(x => x.ProductoId == item.ProductoId);
                    if (details.Count() > 0)
                    {
                        foreach (var detalle in details)
                        {
                            detalles = detalles + "\n" + " " + detalle.Cantidad + " " + detalle.Detalle;
                        }
                        detalles = item.Descripcion + "\n" + detalles;
                        item.Descripcion = detalles;
                        listado.Add(item);
                    }
                    else
                    {
                        listado.Add(item);
                    }
                }
            }
            else
            {
                listado = listadetalles;
            }

            //rvfacturafel.LocalReport.ReportEmbeddedResource = "POS.Recibo.FacturaFEL.rdlc";
            rvporte.LocalReport.ReportEmbeddedResource = "POS.Recibo.ReporteReciboCliente.rdlc";
            var rds1 = new ReportDataSource("detallefactura", listado);
            rvporte.LocalReport.DataSources.Clear();
            rvporte.LocalReport.DataSources.Add(rds1);

        }
       

        private void traerparametros()
        {
            decimal iva = 0.10714284m;
            var sumatotal = listadetalles.Sum(x => x.PrecioTotal);
            var direccionCl = _facturasRepository.Get(idfactura);
            var ivatotal = sumatotal * iva;

            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("noautorizacion",DocCertificado.Autorizacion ),
                new ReportParameter("nombrecomprador",DocCertificado.NOMBRE_COMPRADOR),
                new ReportParameter("nitcomprador",DocCertificado.NIT_COMPRADOR),
                new ReportParameter("fechaemision",fechaemision),
                new ReportParameter("nombreeface", DocCertificado.NOMBRE_EFACE),
                new ReportParameter("niteface", DocCertificado.NIT_EFACE),
                new ReportParameter("ivatotal",ivatotal.ToString("0.0000")),
                new ReportParameter("totalfactura",sumatotal.ToString("0.00")),
                new ReportParameter("direccion", "2da calle 3-29 zona 1 San Pedro sacatepequez San Marcos"),
                new ReportParameter("serie" ,DocCertificado.Serie),
                new ReportParameter("numero",DocCertificado.NUMERO),
                new ReportParameter("numeroFactura",numeroFactura),
            };
            rvporte.LocalReport.SetParameters(new ReportParameterCollection());
            rvporte.LocalReport.SetParameters(reportParameters);
        }

    }
}
