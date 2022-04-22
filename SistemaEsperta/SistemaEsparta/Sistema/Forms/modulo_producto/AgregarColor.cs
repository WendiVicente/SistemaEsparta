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
    public partial class AgregarColor : BaseContext
    {
        //  private Producto _producto = null;
        private List<ColoresProducto> listadgvtemp = new List<ColoresProducto>();
        private  List<ColoresProducto> _coloresProductos = null;
        private  List<ColoresProducto> _coloreslistalocal = null;
        private List<ColoresProducto> _listaTemp = null;
        public string colorDetalle;
        public string colorVincular;
        private CrearProducto _nuevoProducto = null;

        public AgregarColor (CrearProducto nuevoProducto, List<ColoresProducto> lista)
        {
            //_producto = producto;
            _nuevoProducto = nuevoProducto;
             _coloreslistalocal = new List<ColoresProducto>();
            _coloresProductos = lista;
            _listaTemp = lista;
            InitializeComponent();
        }    

        private void AgregarColor_Load(object sender, EventArgs e)
        {
            CargarComboColores();
            OcultarSubCombo();
            limpiarDGV();
        }

        private void limpiarDGV()
        {
            if (_coloresProductos.Count == 0)
            {
                dgvColoresadd.DataSource = null;
            }
            else
            {
                _coloreslistalocal = _coloresProductos;
                CargaDgv(_coloreslistalocal);
            }
        }
        private void btnagregar_Click(object sender, EventArgs e)
        {
            if (!rbcolorespaleta.Checked && !rblistacolores.Checked && !rbcolorespersonal.Checked)
            {
                KryptonMessageBox.Show("¡Debe seleccionar una opcion para continuar!");
                return;
            }
            if (rbcolorespersonal.Checked) 
            {
                colorVincular = txtColor.Text;
                colorDetalle = txtVincularColor.Text;
            }
            var nuevoDetalle = Colores();

            if (comprobarColor(nuevoDetalle))
            {
                KryptonMessageBox.Show("¡Color ya ingresado!");
                return;
            }
            else
            {
                _coloreslistalocal.Add(nuevoDetalle);
                CargaDgv(_coloreslistalocal);
                Limpiartxt();
            }

            txtVincularColor.Text = "";
            txtColor.Text = "";
        }

        public ColoresProducto Colores()
        {
            var listacolores = new ColoresProducto()
            {
                Codigo = colorVincular,
                Color = colorDetalle,       
            };
              

        return listacolores;
        }
        private void CargaDgv(List<ColoresProducto> listacoloresAc)
        {
            BindingSource source = new BindingSource();
            source.DataSource = listacoloresAc;
            dgvColoresadd.DataSource = typeof(List<>);
            dgvColoresadd.DataSource = source;
            dgvColoresadd.Columns[0].Visible = false;
            dgvColoresadd.Columns[3].Visible = false;
            dgvColoresadd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void Limpiartxt()
        {
           // txtCantidadColores.Text = "0";
          // ColorDetalle = "";
        }

       
        private void rbcolorespaleta_CheckedChanged(object sender, EventArgs e)
        {
            colorDetalle = "";
            colorVincular = "";
            if (rbcolorespaleta.Checked)
            {
                ColorDialog dialog = new ColorDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string colorName;
                    if (dialog.Color.IsKnownColor)
                        colorName = dialog.Color.ToKnownColor().ToString();
                    else if (dialog.Color.IsNamedColor)
                        colorName = dialog.Color.Name;
                    else
                        colorName = dialog.Color.ToString();
                    //   MessageBox.Show(colorName);
                    colorDetalle = colorName;
                    btPaleta.BackColor = dialog.Color;
                    btPaleta.Visible = true;
                }
            }
            else
            {
                btPaleta.Visible = false;
                // paleta.ShowDialog();
            }


        }

        private void rbcolorespersonal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbcolorespersonal.Checked)
            {
                lbColor.Visible = true;
                txtColor.Visible = true;
                colorDetalle = txtColor.Text;
                lbvincular.Visible = true;
                txtVincularColor.Visible = true;
            }
            else
            {
                lbColor.Visible = false;
                txtColor.Visible = false;
                lbvincular.Visible = false;
                txtVincularColor.Visible = false;

            }
        }

        private void rblistacolores_CheckedChanged(object sender, EventArgs e)
        {
            if (rblistacolores.Checked)
            {
                cbcoloresba.Visible = true;
                colorDetalle = cbcoloresba.SelectedItem.ToString();
            }
            else
            {
                cbcoloresba.Visible = false;
            }


        }

        public void CargarComboColores()
        {
            List<String> coloreslista = new List<string>();
            coloreslista.Add("Blanco");
            coloreslista.Add("Negro");
            coloreslista.Add("Azul");
            coloreslista.Add("Amarillo");
            coloreslista.Add("verde");
            coloreslista.Add("Rojo");
            cbcoloresba.DataSource = coloreslista; 
        }
        private void OcultarSubCombo()
        {
            lbColor.Visible = false;
            txtColor.Visible = false;
            cbcoloresba.Visible = false;
            lbvincular.Visible = false;
            txtVincularColor.Visible = false;

        }

        private void cbcoloresba_SelectedIndexChanged(object sender, EventArgs e)
        {
            colorDetalle = "";
            colorVincular = "";
            colorDetalle = cbcoloresba.SelectedItem.ToString();
        }

        private void btnAgregarlista_Click(object sender, EventArgs e)
        {
            if (dgvColoresadd.RowCount <= 0) { KryptonMessageBox.Show("No hay ninguna color añadido"); return; }

            _nuevoProducto._listacolores = _coloreslistalocal;
            _nuevoProducto.cambiarCheck(true, 1);
            Close();
        }
      

      
        public bool comprobarColor(ColoresProducto colortoAdd)
        {
            foreach (DataGridViewRow row in dgvColoresadd.Rows)
            {
                if (row.Cells[1].Value.ToString() == colortoAdd.Color)
                {
                    return true;
                }
            }

            return false;
        }

        private void AgregarColor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //_coloreslistalocal = _nuevoProducto._listacolores;
            //_coloresProductos = _nuevoProducto._listacolores;
            //_listaTemp = _nuevoProducto._listacolores;
            //if (listadgvtemp != null)
            //{
            //    listadgvtemp.ForEach(item => _nuevoProducto._listacolores.Add(item));

            //}
            // Close();

            if (_coloreslistalocal.Count == 0)
            {
                _nuevoProducto.cambiarCheck(false, 1);
            }
        }

        
        private void dgvColoresadd_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
          //  KryptonMessageBox.Show("Este color se eliminara permanentemente");
            //var filaeliminada = dgvColoresadd.CurrentRow;
            //var filaActualEliminada = (ColoresProducto)dgvColoresadd.CurrentRow.DataBoundItem;
            //listadgvtemp.Add(filaActualEliminada);
            //stockTosave += (int)filaeliminada.Cells[1].Value;
        }

        

        private void btnVincularColor_Click(object sender, EventArgs e)
        {
            //ColorDialog dialog = new ColorDialog();
            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            //    string colorName;
            //    if (dialog.Color.IsKnownColor)
            //        colorName = dialog.Color.ToKnownColor().ToString();
            //    else if (dialog.Color.IsNamedColor)
            //        colorName = dialog.Color.Name;
            //    else
            //        colorName = dialog.Color.ToString();
            //    //   MessageBox.Show(colorName);
            //    colorDetalle = colorName;
            //    btnVincularColor.BackColor = dialog.Color;
            //    btnVincularColor.Text = "";
            //}
        }
    }
}
