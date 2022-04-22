using CapaDatos.ListasPersonalizadas;
using CapaDatos.Models.Traslados;
using CapaDatos.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Forms.modulo_traslados
{
    public partial class VerTraslados : BaseContext
    {
        private TrasladosRepository _trasladosRepository = null;
        private SucursalesRepository _sucursalesRepository = null;
        public VerTraslados()
        {
            InitializeComponent();
            _trasladosRepository = new TrasladosRepository(_context);
            _sucursalesRepository = new SucursalesRepository(_context);
        }

        private void VerTraslados_Load(object sender, EventArgs e)
        {
            RefrescarDataGridSolicitud();
            RefrescarDataGridPeticiones();
        }

        public void RefrescarDataGridSolicitud(bool loadNewContext = true)
        {

            BindingSource source = new BindingSource();
            var listaSolicitudesTraslado = _trasladosRepository.GetListTraslados(UsuarioLogeadoSistemas.User.SucursalId,false);
            source.DataSource = listaSolicitudesTraslado;
            dgvSolicitudesTraslados.DataSource = typeof(List<>);
            dgvSolicitudesTraslados.DataSource = source;
        }
        public void RefrescarDataGridPeticiones(bool loadNewContext = true)
        {

            BindingSource source = new BindingSource();
            var listaSolicitudesTraslado = _trasladosRepository.GetListTraslados(UsuarioLogeadoSistemas.User.SucursalId, true);
            source.DataSource = listaSolicitudesTraslado;
            dgvPeticionestras.DataSource = typeof(List<>);
            dgvPeticionestras.DataSource = source;
        }

        private void dgvSolicitudesTraslados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSolicitudesTraslados.RowCount > 0)
            {
                var fila = (ListarTraslados)dgvSolicitudesTraslados.CurrentRow.DataBoundItem;
                CargarFm(fila);
            }
        }
        private void CargarFm(ListarTraslados trasladoSolicitud)
        {
            if (Application.OpenForms["DetalleTraslado"] == null)
            {
                DetalleTraslado sel = new DetalleTraslado(this, trasladoSolicitud);
                sel.Show();
            }
            else
            {
                Application.OpenForms["DetalleTraslado"].Activate();
            }
        }
    }
}
