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
    public partial class AgregarTallaEdit : BaseContext
    {
        private List<DetalleTalla> _detalleTallas = null;
        private List<DetalleTalla> _tallaslistalocal = null;
        private EditarProducto _editarProducto = null;
        private List<DetalleTalla> listadgvtemp = new List<DetalleTalla>();
        private string tallaDetalle;
        public int stockValidar = 0;
        private int stockPrevio;
        private int stockTotal = 0;

        public AgregarTallaEdit(EditarProducto editarProducto, List<DetalleTalla> lista)
        {
            _editarProducto = editarProducto;
            _tallaslistalocal = new List<DetalleTalla>(); 
            _detalleTallas = lista;
            InitializeComponent();
        }

        private void AgregarTalla_Load(object sender, EventArgs e)
        {
            CargarTallasCombo();
            OcultarSubCombo();
            CargaDGV();
        }
        private void CargaDGV()
        {
            if (_detalleTallas.Count == 0)
            {
                dgvtallaslist.DataSource = null;
            }
            else
            {
                _tallaslistalocal = _detalleTallas;
                CargaDgv(_tallaslistalocal);
                stockValidar -= Convert.ToInt32(_tallaslistalocal.Sum(x => x.Stock).ToString());
            }
        }

        private void CargaDgv(List<DetalleTalla> lista)
        {
            BindingSource source = new BindingSource();
            source.DataSource = lista;
            dgvtallaslist.DataSource = typeof(List<>);
            dgvtallaslist.DataSource = source;
            for (int i = 0; i <= 12; i++)
            {
                if (i == 2)
                {
                    dgvtallaslist.Columns[i].Visible = true;
                    dgvtallaslist.Columns[i].DisplayIndex = 1;
                }
                else if (i == 3)
                {
                    dgvtallaslist.Columns[i].Visible = true;
                    dgvtallaslist.Columns[i].DisplayIndex = 0;
                }
                else
                {
                    dgvtallaslist.Columns[i].Visible = false;
                }
            }
            dgvtallaslist.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                Stock = ObtenerStock()
            };

            return listaTallas;
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
                comboTallas.Visible = false;
                numericTalla.Visible = false;
            }
            else
            {
                lbMedida.Visible = false;
                txtTalla.Visible = false;
            }
        }

        private void rbTallaNumero_CheckedChanged(object sender, EventArgs e)
        {
            numericTalla.Visible = rbTallaNumero.Checked;
        }

        private void comboTallas_SelectedIndexChanged(object sender, EventArgs e)
        {
            tallaDetalle = comboTallas.SelectedItem.ToString();
        }

        private void AgregarTalla_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_tallaslistalocal.Count == 0)
            {
                _editarProducto.cambiarCheck(false, 2);
            }
        }  

        private void dgvtallaslist_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var filaeliminada = dgvtallaslist.CurrentRow;
            var filaActualEliminada = (DetalleTalla)dgvtallaslist.CurrentRow.DataBoundItem;
            listadgvtemp.Add(filaActualEliminada);
            stockValidar += (int)filaeliminada.Cells[2].Value;
        }

        private void dgvtallaslist_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var fila = dgvtallaslist.CurrentRow;
            var detalle = (DetalleTalla)fila.DataBoundItem;
            int valor = RestarStock(detalle.Stock);
            _tallaslistalocal.ElementAt(fila.Index).Talla = detalle.Talla;
            _tallaslistalocal.ElementAt(fila.Index).Stock = valor;
            fila.Cells[2].Value = valor;
        }

        private void dgvtallaslist_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            KryptonMessageBox.Show("¡Datos inválidos en la Modificación!");
            return;
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            if (!rbcomboTallas.Checked && !rbTallaNumero.Checked && !rbtallapersonaliz.Checked)
            {
                KryptonMessageBox.Show("¡Debe seleccionar una opcion para continuar!");
                return;
            }
            //if (string.IsNullOrEmpty(txtCantidad.Text) || txtCantidad.Text == "0")
            //{
            //    KryptonMessageBox.Show("¡Debe ingresar una cantidad valida!");
            //    return;
            //}

            if (rbtallapersonaliz.Checked)
            {
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
                if (listadgvtemp.Count > 0)
                {
                    var det = listadgvtemp.Where(x => x.Talla == nuevoDetalle.Talla);
                    if (det.Count() > 0)
                    {
                        det.ElementAt(0).Stock = nuevoDetalle.Stock;
                        RestarStock(nuevoDetalle.Stock);
                        _tallaslistalocal.Add(det.ElementAt(0));
                        listadgvtemp.Remove(det.ElementAt(0));
                        CargaDgv(_tallaslistalocal);
                    }
                    else
                    {
                        RestarStock(nuevoDetalle.Stock);
                        _tallaslistalocal.Add(nuevoDetalle);
                        CargaDgv(_tallaslistalocal);
                    }
                }
                else
                {
                    RestarStock(nuevoDetalle.Stock);
                    _tallaslistalocal.Add(nuevoDetalle);
                    CargaDgv(_tallaslistalocal);
                }
                txtTalla.Text = "";
            }
        }

        private void btnAgregarlista_Click(object sender, EventArgs e)
        {
            if (dgvtallaslist.RowCount <= 0) { KryptonMessageBox.Show("No hay ninguna color añadido"); return; }
            //DevolverList();
            _editarProducto._listaTallas = _tallaslistalocal;
            _editarProducto._listaTallasDel = listadgvtemp;
            _editarProducto.cambiarCheck(true, 2);
            Close();
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

        private void DevolverList()
        {
            DetalleTalla dt = null;

            foreach (DataGridViewRow dc in dgvtallaslist.Rows)
            {
                dt = new DetalleTalla();
                dt.Id = (int)dc.Cells[0].Value;
                dt.ProductoId = (int)dc.Cells[1].Value;
                dt.Stock = (int)dc.Cells[2].Value;
                dt.Talla = (string)dc.Cells[3].Value;
                _detalleTallas.Add(dt);
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

        private void dgvtallaslist_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var fila = dgvtallaslist.CurrentRow;
            stockPrevio = Convert.ToInt32(fila.Cells[2].Value.ToString());
        }

        private void SumarTotalStock()
        {
            stockTotal = 0;
            foreach (DataGridViewRow row in dgvtallaslist.Rows)
            {
                stockTotal += Convert.ToInt32(row.Cells[2].Value.ToString());
            }
        }
    }
}
