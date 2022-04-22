using CapaDatos.Models.Productos;
using ComponentFactory.Krypton.Toolkit;
using sharedDatabase.Models;
using Sistema.Forms.Extras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Forms.modulo_producto
{
    public partial class AgregarNumero : BaseContext
    {
        private List<DetalleTomoEdicion> _listaNumeros = null;
        private List<DetalleTomoEdicion> _listatomos = null;
        private List<DetalleTomoEdicion> _listaediciones = null;
        private CrearProducto _nuevoProducto = null;

        private bool combo;
        public string tomo;
        public string edicion;
        public string codigobarra;

        public AgregarNumero(CrearProducto nuevoProducto, List<DetalleTomoEdicion> lista)
        {
            _nuevoProducto = nuevoProducto;
            _listaNumeros = new List<DetalleTomoEdicion>();
            _listatomos = new List<DetalleTomoEdicion>();
            _listaediciones = new List<DetalleTomoEdicion>();
            codigobarra = nuevoProducto.codigobarra;
            edicion = "";
            tomo = "";
            combo = false;
            InitializeComponent();
        }

        private void AgregarColorTalla_Load(object sender, EventArgs e)
        {
            CargarTomos();
            CargarEdiciones();
            //OcultarSubCombo();
            //OcultarCombocolor();
            //CargaDgv();
        }
        public void CargarTomos()
        {

            List<String> tomos = new List<string>();
            //tomos.Add("");
            tomos.Add(" Tomo I ");
            tomos.Add(" Tomo II ");
            tomos.Add(" Tomo III ");
            tomos.Add(" Tomo IV ");
            tomos.Add(" Tomo V ");
            tomos.Add(" Tomo VI ");
            cbTomos.DataSource = tomos;

        }
        public void CargarEdiciones()
        {
            List<String> ediciones = new List<string>();
            //ediciones.Add("");
            ediciones.Add(" 1ra. Edición ");
            ediciones.Add(" 2da. Edición ");
            ediciones.Add(" 3ra. Edición ");
            ediciones.Add(" 4ta. Edición ");
            ediciones.Add(" 5ta. Edición ");
            ediciones.Add(" 6ta. Edición ");
            ediciones.Add(" 7ma. Edición ");
            ediciones.Add(" 8va. Edición ");
            ediciones.Add(" 9na. Edición ");
            ediciones.Add(" 10ma. Edición ");
            ediciones.Add(" 11va. Edición ");
            ediciones.Add(" 12va. Edición ");
            cbEdiciones.DataSource = ediciones;
        }

        public DetalleTomoEdicion TomoEdicion()
        {
            var tomoEdicion = new DetalleTomoEdicion()
            {
                Codigo = codigobarra,
                Edicion = edicion,
                Tomo = tomo,
            };
            return tomoEdicion;
        }
        private void btnAgregarlista_Click(object sender, EventArgs e)
        {
            CargaListados();
            _nuevoProducto._listaNumero = _listaNumeros;
            _nuevoProducto.cambiarCheck(true, 3);
            Close();
        }

        public void CargaListados()
        {
            foreach (DetalleTomoEdicion item in _listatomos)
                _listaNumeros.Add(item);
            
            foreach (DetalleTomoEdicion item in _listaediciones)
                _listaNumeros.Add(item);
        }

        private void CargaDgvTomo()
        {
            BindingSource source = new BindingSource();
            source.DataSource = _listatomos;
            dgvTomo.DataSource = typeof(List<>);
            dgvTomo.DataSource = source;
            dgvTomo.Columns[0].Visible = false;
            dgvTomo.Columns[3].Visible = false;
           //dgvTomo.Columns[4].Visible = true;
            dgvTomo.Columns[5].Visible = false;
            dgvTomo.Columns[6].Visible = false;
            dgvTomo.Columns[7].Visible = false;
            dgvTomo.Columns[8].Visible = false;
            dgvTomo.Columns[9].Visible = false;
            dgvTomo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CargaDgvEdicion()
        {
            BindingSource source = new BindingSource();
            source.DataSource = _listaediciones;
            dgvEdicion.DataSource = typeof(List<>);
            dgvEdicion.DataSource = source;
            dgvEdicion.Columns[0].Visible = false;
            dgvEdicion.Columns[2].Visible = false;
            // dgvTomo.Columns[4].Visible = true;
            dgvEdicion.Columns[5].Visible = false;
            dgvEdicion.Columns[6].Visible = false;
            dgvEdicion.Columns[7].Visible = false;
            dgvEdicion.Columns[8].Visible = false;
            dgvEdicion.Columns[9].Visible = false;
            dgvEdicion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        public bool comprobarElemento(DetalleTomoEdicion tomoEdicion)
        {

            if (combo)
            {
                foreach (DataGridViewRow row in dgvTomo.Rows)
                {
                    if (row.Cells[2].Value.ToString() == tomoEdicion.Tomo)
                    {
                        return true;
                    }
                }
            }
            else 
            {
                foreach (DataGridViewRow row in dgvEdicion.Rows)
                {
                    if (row.Cells[3].Value.ToString() == tomoEdicion.Edicion)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }


        private void cbTomos_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo = true;
            tomo = cbTomos.SelectedItem.ToString();
            //cbEdiciones.SelectedIndex = 1;        
        }

        private void cbEdiciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo = false;
            edicion = cbEdiciones.SelectedItem.ToString();
            //cbTomos.SelectedIndex = 1;
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            var nuevoDetalle = TomoEdicion();
            if (comprobarElemento(nuevoDetalle))
            {
                KryptonMessageBox.Show("¡Elemento ya Ingresado!");
                return;
            }

            if (combo == true)
            {
                _listatomos.Add(nuevoDetalle);
                CargaDgvTomo();
            }
            else
            {
                _listaediciones.Add(nuevoDetalle);
                CargaDgvEdicion();
            }

        }
        
    }
}
