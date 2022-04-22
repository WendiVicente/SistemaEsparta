using CapaDatos.ListasPersonalizadas;
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
    public partial class DetalleTraslado : BaseContext
    {
        private VerTraslados _formspadre = null;
        private ListarTraslados _TrasladoActual = null;
        private TrasladosRepository _trasladoRepository = null;
        public DetalleTraslado(VerTraslados frm, ListarTraslados trasladoselected)
        {
            _formspadre = frm;
            _TrasladoActual = trasladoselected;
            _trasladoRepository = new TrasladosRepository(_context);
            InitializeComponent();
        }

        private void DetalleTraslado_Load(object sender, EventArgs e)
        {
            cargarTxt();
            cargardetalles();
        }
        private void cargarTxt()
        {
            this.txtsucursalorigen.Text = _TrasladoActual.SucursalEmisor;
            this.txtfechasolicitud.Text = _TrasladoActual.FechaRecepcion.ToString();
        }

        private void cargardetalles()
        {

        }
        private void recargarFrm()
        {

        }
        private void recargarfrmPadre()
        {

        }
    }
}
