using CapaDatos.ListasPersonalizadas;
using CapaDatos.Models;
using CapaDatos.Repository;
using CapaDatos.Repository.PreciosRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Reports.Reports_Compras
{
    public partial class ReporteCompras : BaseContext
    {

        private ComprasRepository _comprasRepository = null;
        private ProductosRepository _productosRepository = null;
        private PreciosDetallePepsRepository _preciosDetallePepsRepository = null;



        private DateTime fechaInicio => Convert.ToDateTime(dtpfechainicio.Value.ToLongDateString());
        private DateTime fechaFinal => Convert.ToDateTime(dtpfechafinal.Value.ToLongDateString());

        public ReporteCompras()
        {
            _comprasRepository = new ComprasRepository(_context);
            _productosRepository = new ProductosRepository(_context);
            _preciosDetallePepsRepository = new PreciosDetallePepsRepository(_context);


            InitializeComponent();
        }

        private void ReporteCompras_Load(object sender, EventArgs e)
        {

            // this.vistareporte.RefreshReport();
            if (rbbydetalle.Checked == true)
            {
                CargarListAllDetalle();
            }
            else if (rbEncabezado.Checked == true)
            {
                Cargar();
            }
         
        }

        private void Cargar()
        {
            BindingSource source = new BindingSource();
            var Productos = _comprasRepository.GetListGenerales(UsuarioLogeadoSistemas.User.SucursalId);
            source.DataSource = Productos;
            dgvcompras.DataSource = typeof(List<>);
            dgvcompras.DataSource = source;
            dgvcompras.AutoResizeColumns();


        }
        private void CargarListAllDetalle()
        {
            BindingSource source = new BindingSource();
            var Productos = _comprasRepository.GetListAllDetalle();
            source.DataSource = Productos;
            dgvcompras.DataSource = typeof(List<>);
            dgvcompras.DataSource = source;
            dgvcompras.AutoResizeColumns();


        }

        private void rbEncabezado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbbydetalle.Checked == true)
            {
                CargarListAllDetalle();
            }
            else if (rbEncabezado.Checked == true)
            {
                Cargar();
            }

        }

        private void rbbydetalle_CheckedChanged(object sender, EventArgs e)
        {
            if (rbbydetalle.Checked == true)
            {
                CargarListAllDetalle();
            }
            else if (rbEncabezado.Checked == true)
            {
                Cargar();
            }

        }

        private void dgvcompras_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (rbEncabezado.Checked == true)
            {
                var filaselected = (ListarCompras)dgvcompras.CurrentRow.DataBoundItem;

            }
           

        }

        private void cargarPeps()
        {

        }

        private void btngenerarReporte_Click(object sender, EventArgs e)
        {

        }
    }
}
