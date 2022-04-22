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
    public partial class AgregarColorEdit : BaseContext
    {
        private List<DetalleColor> listadgvtemp = new List<DetalleColor>();
        private List<DetalleColor> _coloresProductos = null;
        private List<DetalleColor> _coloreslistalocal = null;
        private List<DetalleColor> _listaTemp = null;
        public string colorDetalle;
        public string colorVincular;
        private EditarProducto _editarProducto = null;
        public int stockValidar = 0;
        private int stockPrevio;
        private int stockTotal = 0;

        public AgregarColorEdit(EditarProducto editarProducto, List<DetalleColor> detalles)
        {
            _editarProducto = editarProducto;
            _coloreslistalocal = new List<DetalleColor>();
            _coloresProductos = detalles;
            _listaTemp = detalles;
            InitializeComponent();
        }

        private void AgregarColor_Load(object sender, EventArgs e)
        {
            CargarComboColores();
            OcultarSubCombo();
            cargaDGV();
        }

        private void cargaDGV()
        {
            if (_coloresProductos.Count == 0)
            {
                dgvColoresadd.DataSource = null;
            }
            else
            {
                _coloreslistalocal = _coloresProductos;
                CargaDgv(_coloreslistalocal);
                //stockValidar -= Convert.ToInt32(_coloreslistalocal.Sum(x => x.Stock).ToString());
            }
        }

        private void CargaDgv(List<DetalleColor> listacoloresAc)
        {
            BindingSource source = new BindingSource();
            source.DataSource = listacoloresAc;
            dgvColoresadd.DataSource = typeof(List<>);
            dgvColoresadd.DataSource = source;
            dgvColoresadd.Columns[0].Visible = false;
            dgvColoresadd.Columns[1].Visible = false;
            for (int i = 5; i <= 19; i++)
            {
                dgvColoresadd.Columns[i].Visible = false;
            }
            dgvColoresadd.Columns[2].DisplayIndex = 3;
            dgvColoresadd.Columns[3].DisplayIndex = 2;
            dgvColoresadd.Columns[4].DisplayIndex = 1;
            dgvColoresadd.Columns[4].HeaderText = "Código";
            dgvColoresadd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            

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

        public DetalleColor Colores()
        {

            var listacolores = new DetalleColor()
            {
                CodigoGeneral = txtCodigo.Text,
                Color = colorDetalle,
                Stock = ObtenerStock()
            };
            return listacolores;
        }

        private int ObtenerStock()
        {
            int value = 0;
            if (txtStock.Text.Length > 0)
            {
                value = Convert.ToInt32(txtStock.Text);
            }
            else 
            {
                value = 0;
            }
            return value;
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
                txtCodigo.Visible = true;
            }
            else
            {
                lbColor.Visible = false;
                txtColor.Visible = false;
                lbvincular.Visible = false;
                txtVincularColor.Visible = false;
                txtCodigo.Visible = false;
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

        private void cbcoloresba_SelectedIndexChanged(object sender, EventArgs e)
        {
            colorDetalle = "";
            colorVincular = "";
            colorDetalle = cbcoloresba.SelectedItem.ToString();
        }

        private void AgregarColorEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_coloreslistalocal.Count == 0)
            {
                _editarProducto.cambiarCheck(false, 1);
            }
        }

        private void dgvColoresadd_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var filaeliminada = dgvColoresadd.CurrentRow;
            DetalleColor filaActualEliminada = (DetalleColor)dgvColoresadd.CurrentRow.DataBoundItem;
            listadgvtemp.Add(filaActualEliminada);            
            stockValidar += (int)filaeliminada.Cells[2].Value;
        }

        private void dgvColoresadd_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var fila = dgvColoresadd.CurrentRow;
            var detalle = (DetalleColor)fila.DataBoundItem;
            int valor = RestarStock(detalle.Stock);
            _coloreslistalocal.ElementAt(fila.Index).Color = detalle.Color;
            _coloreslistalocal.ElementAt(fila.Index).Stock = valor;
            fila.Cells[2].Value = valor;
            
        }

        private void dgvColoresadd_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            KryptonMessageBox.Show("¡Datos inválidos en la Modificación!");
            return;
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            if (!rbcolorespaleta.Checked && !rblistacolores.Checked && !rbcolorespersonal.Checked)
            {
                KryptonMessageBox.Show("¡Debe seleccionar una opcion para continuar!");
                return;
            }
            //if (string.IsNullOrEmpty(txtStock.Text) || txtStock.Text == "0")
            //{
            //    KryptonMessageBox.Show("¡Debe ingresar una cantidad valida!");
            //    return;
            //}
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
                if (listadgvtemp.Count > 0)
                {
                    var det = listadgvtemp.Where(x => x.Color == nuevoDetalle.Color);
                    if (det.Count() > 0)
                    {
                        det.ElementAt(0).Stock = nuevoDetalle.Stock;
                        RestarStock(nuevoDetalle.Stock);
                        _coloreslistalocal.Add(det.ElementAt(0));
                        listadgvtemp.Remove(det.ElementAt(0));
                        CargaDgv(_coloreslistalocal);
                    }
                    else
                    {
                        RestarStock(nuevoDetalle.Stock);
                        _coloreslistalocal.Add(nuevoDetalle);
                        CargaDgv(_coloreslistalocal);
                    }
                }
                else
                {
                    RestarStock(nuevoDetalle.Stock);
                    _coloreslistalocal.Add(nuevoDetalle);
                    CargaDgv(_coloreslistalocal);
                }
            }

            txtVincularColor.Text = "";
            txtColor.Text = "";
            txtCodigo.Text = "";
        }

        private void btnAgregarlista_Click(object sender, EventArgs e)
        {
            if (dgvColoresadd.RowCount <= 0) { KryptonMessageBox.Show("No hay ninguna color añadido"); return; }
          
            //DevolverList();
            _editarProducto._listaColores = _coloreslistalocal;
            _editarProducto._listacoloresDel = listadgvtemp;
            _editarProducto.cambiarCheck(true, 1);
            Close();
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

        public bool comprobarColor(DetalleColor colortoAdd)
        {
            foreach (DataGridViewRow row in dgvColoresadd.Rows)
            {
                if (row.Cells[3].Value.ToString() == colortoAdd.Color)
                {
                    return true;
                }
            }

            return false;
        } 

        private void DevolverList()
        {
            DetalleColor dt = null;

            foreach (DataGridViewRow dc in dgvColoresadd.Rows)
            {
                dt = new DetalleColor();
                dt.Id = (int)dc.Cells[0].Value;
                dt.ProductoId = (int)dc.Cells[1].Value;
                dt.Stock = (int)dc.Cells[2].Value;
                dt.Color = (string)dc.Cells[3].Value;
                dt.PrecioMayorista = (decimal)dc.Cells[5].Value;
                dt.PrecioRevendedor = (decimal)dc.Cells[7].Value;
                _listaTemp.Add(dt);
            }

        }

        private int RestarStock(int cantidad)
        {
            int value = 0;
            SumarTotalStock();
            int cantidadRestante = stockValidar - stockTotal;
            if (cantidadRestante == 0)
            {
                KryptonMessageBox.Show("¡La sumatoria es equivalente a la cantidad de Existencias!");
                return cantidad;
            }
            else if (cantidadRestante < 0)
            {
                KryptonMessageBox.Show("¡La cantidad excede a las existencias!");
                return stockPrevio;
            }
            else
            {
                value = cantidad;
            }
            return value;

        }

        private void dgvColoresadd_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var fila = dgvColoresadd.CurrentRow;
            stockPrevio = Convert.ToInt32(fila.Cells[2].Value.ToString());
        }

        private void SumarTotalStock()
        {
            stockTotal = 0;
            foreach (DataGridViewRow row in dgvColoresadd.Rows)
            {
                stockTotal += Convert.ToInt32(row.Cells[2].Value.ToString());
            }
        }
    }
}
