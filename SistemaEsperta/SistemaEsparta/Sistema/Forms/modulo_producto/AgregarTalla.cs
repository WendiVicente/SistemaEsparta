using CapaDatos.Models.Productos;
using ComponentFactory.Krypton.Toolkit;
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
    public partial class AgregarTalla : BaseContext
    {
        private List<DetalleTalla> _detalleTallas = null;
        private List<DetalleTalla> _tallaslistalocal = null;
        private CrearProducto _nuevoProducto = null;
        private List<DetalleTalla> listadgvtemp = new List<DetalleTalla>();
        private string tallaDetalle;
        private int stockTosave;
        public AgregarTalla(CrearProducto nuevoProducto, List<DetalleTalla> lista)
        {
            _nuevoProducto = nuevoProducto;
            _tallaslistalocal = new List<DetalleTalla>();
            _detalleTallas = lista;
            InitializeComponent();
        }


        private void AgregarTalla_Load(object sender, EventArgs e)
        {
            CargarTallasCombo();
            OcultarSubCombo();
            limpiarDGV();
           
        }



        public void CargarTallasCombo()
        {

            List<String> tallaslitacombo = new List<string>();
            tallaslitacombo.Add("XXL (Extra Extra Grande)");
            tallaslitacombo.Add("XL (Extra Grande)");
            tallaslitacombo.Add("L (Grande)");
            tallaslitacombo.Add("M (Mediana)");
            tallaslitacombo.Add("S (Pequeño)");
            tallaslitacombo.Add("XS (Extra Pequeño)");
            comboTallas.DataSource = tallaslitacombo;

        }


        private void OcultarSubCombo()
        {
            lbMedida.Visible = false;
            txtTalla.Visible = false;
            comboTallas.Visible = false;

        }

        public DetalleTalla Tallas()
        {
            var listaTallas = new DetalleTalla()
            {
                Talla = tallaDetalle,
            };


            return listaTallas;
        }

        bool enabledDeleteperman=false;
        private void btnAgregarlista_Click(object sender, EventArgs e)
        {
            
            if (dgvtallaslist.RowCount <= 0) { KryptonMessageBox.Show("No hay ninguna Talla añadida"); return; }

            _nuevoProducto._listaTallas = _tallaslistalocal;
            Close();

        }
        private void CargaDgv(List<DetalleTalla> lista)
        {
            BindingSource source = new BindingSource();
            source.DataSource = lista;
            dgvtallaslist.DataSource = typeof(List<>);
            dgvtallaslist.DataSource = source;
            for (int i = 0; i <= 12; i++)
            {
                dgvtallaslist.Columns[i].Visible = false;
                if(i == 3)
                    dgvtallaslist.Columns[i].Visible = true;
            }
            dgvtallaslist.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void Limpiartxt()
        {
            //tallaDetalle = "";
            txtTalla.Text = "";
        }
        private void btnagregar_Click(object sender, EventArgs e)
        {
            if (!rbcomboTallas.Checked && !rbTallaNumero.Checked && !rbtallapersonaliz.Checked)
            {
                KryptonMessageBox.Show("¡Debe seleccionar una opcion para continuar!");
                return;
            }

            if (rbtallapersonaliz.Checked) {

                if (txtTalla.Text.Length > 0)
                {
                    tallaDetalle = txtTalla.Text;
                }
                else 
                {
                    KryptonMessageBox.Show("El valor de medida no es válido");
                    return;
                }                
            }

            if (rbTallaNumero.Checked)
            {
                if (numericTalla.Value == 0)
                {
                    KryptonMessageBox.Show("El valor debe ser mayor a 0");
                    return;
                }
                else
                {
                    tallaDetalle = numericTalla.Value.ToString();
                }
            }

            var nuevoDetalle = Tallas();
            if (comprobarTalla(nuevoDetalle))
            {
                KryptonMessageBox.Show("¡Talla ya ingresado!");
                return;
            }
            else
            {
                _tallaslistalocal.Add(nuevoDetalle);
                CargaDgv(_tallaslistalocal);
                Limpiartxt();
            }
        }
        public bool comprobarTalla(DetalleTalla tallatoAdd)
        {
            foreach (DataGridViewRow row in dgvtallaslist.Rows)
            {
                if (row.Cells[3].Value.ToString() == tallatoAdd.Talla)
                {
                    return true;
                }
            }

            return false;
        }

        private void rblistacolores_CheckedChanged(object sender, EventArgs e)
        {
            if (rbcomboTallas.Checked)
            {
                lbMedida.Visible = false;
                txtTalla.Visible = false;
                comboTallas.Visible = true;
                numericTalla.Visible = false;
                tallaDetalle = comboTallas.Text;
            }

        }

        private void rbcolorespersonal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtallapersonaliz.Checked)
            {
                lbMedida.Visible = true;
                txtTalla.Visible = true;
                tallaDetalle = txtTalla.Text;
                comboTallas.Visible = false;
                numericTalla.Visible = false;
            }
            else
            {
                lbMedida.Visible = false;
                txtTalla.Visible = false;

            }
        }

        private void comboTallas_SelectedIndexChanged(object sender, EventArgs e)
        {
            tallaDetalle = comboTallas.SelectedItem.ToString();
        }

        private void AgregarTalla_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tallaslistalocal = _nuevoProducto._listaTallas;
            _detalleTallas = _nuevoProducto._listaTallas;
           // _listaTemp = _nuevoProducto._listacolores;
            if (listadgvtemp != null && enabledDeleteperman==false)
            {
                listadgvtemp.ForEach(item => _nuevoProducto._listaTallas.Add(item));

            }
            //if (dgvtallaslist.RowCount <= 0)
            //{
            //    _nuevoProducto.checktalla.Checked = false;
            //}
        }
      
        private void limpiarDGV()
        {
            if (_detalleTallas.Count == 0)
            {
                dgvtallaslist.DataSource = null;
            }
            else
            {
                _tallaslistalocal = _detalleTallas;
                CargaDgv(_tallaslistalocal);
            }
        }

       
        private void dgvtallaslist_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var filaeliminada = dgvtallaslist.CurrentRow;
            var filaActualEliminada = (DetalleTalla)dgvtallaslist.CurrentRow.DataBoundItem;
            listadgvtemp.Add(filaActualEliminada);
            //var pp = (int)filaeliminada.Cells[2].Value;
            stockTosave += (int)filaeliminada.Cells[2].Value;
        }

        private void ValidarCantidadTallas(List<DetalleTalla> lista)
        {
            if (lista != null)
            {
                var totaltallas = 0;
                foreach (var item in lista)
                {
                    totaltallas += item.Stock;
                }
                stockTosave -= totaltallas;
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void rbTallaNumero_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTallaNumero.Checked)
            {
                comboTallas.Visible = false;
                lbMedida.Visible = false;
                txtTalla.Visible = false;
                numericTalla.Visible = true;
            }
        }

        private void numericTalla_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}
