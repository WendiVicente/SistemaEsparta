using CapaDatos.ListasPersonalizadas;
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
    public partial class ReciboVenta : BaseContext
    {
       
        private readonly FacturasRepository _facturasRepository = null;
        private readonly DetalleProductoRepository _detalleProductoRepository = null;
        private Guid facturaID;
        private IList<ListarDetalleFacturas> listadetalles = null;
        private string empleado = "";
        public ReciboVenta(Guid idfact, string userName)
        {
            facturaID = idfact;
            empleado = userName;
            listadetalles = new List<ListarDetalleFacturas>();
            _facturasRepository = new FacturasRepository(_context);
            _detalleProductoRepository = new DetalleProductoRepository(_context);
            InitializeComponent();            
        }

        private void ReciboVenta_Load(object sender, EventArgs e)
        {
            CargarTabla();
            traerparametros();
            this.rvRecibo.RefreshReport();            
        }

        private void CargarTabla()
        {
            var encabezadoFactura = _facturasRepository.Get(facturaID);
            var detallesproducto = _detalleProductoRepository.GetListNoDocumento(encabezadoFactura.NoFactura);
            listadetalles = _facturasRepository.GetDetallebyFactura(facturaID);

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
                       


            rvRecibo.LocalReport.ReportEmbeddedResource = "POS.Recibo.ReciboVenta.rdlc";
            var rds1 = new ReportDataSource("detalleFactura", listado);
            //rvRecibo.LocalReport.DataSources.Clear();
            rvRecibo.LocalReport.DataSources.Add(rds1);
        }
        private void traerparametros()
        {
            var encabezadoFactura= _facturasRepository.Get(facturaID);

            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("fecha",encabezadoFactura.FechaVenta.ToString() ),
                new ReportParameter("comprobante",encabezadoFactura.NoFactura ),
                new ReportParameter("nombrecliente",encabezadoFactura.Nombre ),
                new ReportParameter("nitcliente",encabezadoFactura.NIT ),
                new ReportParameter("direccioncliente",encabezadoFactura.Direccion ),
                new ReportParameter("empleado",empleado ),         
            };
            rvRecibo.LocalReport.SetParameters(new ReportParameterCollection());
            rvRecibo.LocalReport.SetParameters(reportParameters);
        }
    }
}
