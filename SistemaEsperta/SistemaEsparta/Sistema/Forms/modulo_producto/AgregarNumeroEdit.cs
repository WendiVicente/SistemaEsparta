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
    public partial class AgregarNumeroEdit : BaseContext
    {
        private List<DetalleTomoEdicion> _listatomos = null;
        private List<DetalleTomoEdicion> _listaediciones = null;
        private List<DetalleTomoEdicion> _detalleTomoEdicion = null;
        private List<DetalleTomoEdicion> _tomoedicionlistalocal = null;
        private EditarProducto _editarProducto = null;
        private List<DetalleTomoEdicion> listadgvtemp = new List<DetalleTomoEdicion>();
        public int stockValidar = 0;
        private int stockPrevio = 0;

        private bool combo;
        public string tomo;
        public string edicion;
        public string codigobarra;
        private int stockcol = 4;
        private int stockTotal = 0;

        public AgregarNumeroEdit(EditarProducto editarProducto, List<DetalleTomoEdicion> lista)
        {
            _editarProducto = editarProducto;
            _tomoedicionlistalocal = new List<DetalleTomoEdicion>();
            _listaediciones = new List<DetalleTomoEdicion>();
            _listatomos = new List<DetalleTomoEdicion>();
            _detalleTomoEdicion = lista;
            codigobarra = editarProducto.codigobarra;
            edicion = "";
            tomo = "";
            combo = false;
            InitializeComponent();            
        }

        private void AgregarNumeroEdit_Load(object sender, EventArgs e)
        {
            CargarTomos();
            CargarEdiciones();
            CargaDGV();
            btnAgregar.Focus();
            
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
            btnAgregar.Focus();

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
            btnAgregar.Focus();
        }        

        private void CargaDGV()
        {
            if (_detalleTomoEdicion.Count == 0)
            {
                dgvTomo.DataSource = null;
                dgvEdicion.DataSource = null;
            }
            else
            {
                _tomoedicionlistalocal = _detalleTomoEdicion;
                CargarListados();
                CargaDgvTomo(_listatomos);
                CargaDgvEdicion(_listaediciones);
                dgvTomo.ClearSelection();
                dgvEdicion.ClearSelection();

                //int stocktomos = Convert.ToInt32(_listatomos.Sum(x => x.Stock).ToString());
                //stockValidar -= stocktomos;
                //int stockEdiciones = Convert.ToInt32(_listaediciones.Sum(x => x.Stock).ToString());
                //stockValidar -= stockEdiciones;
            }
        }

        public DetalleTomoEdicion Tomo()
        {
            var tomoEdicion = new DetalleTomoEdicion()
            {
                Codigo = codigobarra,
                Tomo = tomo
            };
            return tomoEdicion;
        }

        public DetalleTomoEdicion Edicion()
        {
            var tomoEdicion = new DetalleTomoEdicion()
            {
                Codigo = codigobarra,
                Edicion = edicion
            };
            return tomoEdicion;
        }

        private void CargaDgvTomo(List<DetalleTomoEdicion> _listatomos)
        {
            BindingSource source = new BindingSource();
            source.DataSource = _listatomos;
            dgvTomo.DataSource = typeof(List<>);
            dgvTomo.DataSource = source;
            dgvTomo.Columns[0].Visible = false;
            dgvTomo.Columns[3].Visible = false;
            dgvTomo.Columns[4].Visible = true;
            dgvTomo.Columns[5].Visible = false;
            dgvTomo.Columns[6].Visible = false;
            dgvTomo.Columns[7].Visible = false;
            dgvTomo.Columns[8].Visible = false;
            dgvTomo.Columns[9].Visible = false;
            dgvTomo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTomo.ClearSelection();
        }

        private void CargaDgvEdicion(List<DetalleTomoEdicion> _listaediciones)
        {
            BindingSource source = new BindingSource();
            source.DataSource = _listaediciones;
            dgvEdicion.DataSource = typeof(List<>);
            dgvEdicion.DataSource = source;
            dgvEdicion.Columns[0].Visible = false;
            dgvEdicion.Columns[2].Visible = false;
            dgvEdicion.Columns[4].Visible = true;
            dgvEdicion.Columns[5].Visible = false;
            dgvEdicion.Columns[6].Visible = false;
            dgvEdicion.Columns[7].Visible = false;
            dgvEdicion.Columns[8].Visible = false;
            dgvEdicion.Columns[9].Visible = false;
            dgvEdicion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEdicion.ClearSelection();
        }

        public void CargarListados()
        {
            var listado1 = _tomoedicionlistalocal.Where(x => x.Tomo != null).ToList();
            var listado2 = _tomoedicionlistalocal.Where(x => x.Edicion != null).ToList();

            if (listado1.Count > 0)
                _listatomos = listado1;
            if (listado2.Count > 0)
                _listaediciones = listado2;
        }

        private void cbTomos_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo = true;
            tomo = cbTomos.SelectedItem.ToString();      
        }

        private void cbEdiciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo = false;
            edicion = cbEdiciones.SelectedItem.ToString();
        }

        private void dgvEdicion_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var filaeliminada = dgvEdicion.CurrentRow;
            var filaActualEliminada = (DetalleTomoEdicion)dgvEdicion.CurrentRow.DataBoundItem;
            listadgvtemp.Add(filaActualEliminada);
            stockValidar += (int)filaeliminada.Cells[stockcol].Value;
        }

        private void dgvTomo_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var filaeliminada = dgvTomo.CurrentRow;
            var filaActualEliminada = (DetalleTomoEdicion)dgvTomo.CurrentRow.DataBoundItem;
            listadgvtemp.Add(filaActualEliminada);
            stockValidar += (int)filaeliminada.Cells[stockcol].Value;
        }

        private void dgvTomo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var fila = dgvTomo.CurrentRow;
            var detalle = (DetalleTomoEdicion)fila.DataBoundItem;
            int valor = RestarStock(detalle.Stock, dgvTomo);
            _listatomos.ElementAt(fila.Index).Tomo = detalle.Tomo;
            _listatomos.ElementAt(fila.Index).Stock = valor;
            fila.Cells[stockcol].Value = valor;
            
        }

        private void dgvEdicion_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var fila = dgvEdicion.CurrentRow;
            var detalle = (DetalleTomoEdicion)fila.DataBoundItem;
            int valor = RestarStock(detalle.Stock, dgvEdicion);
            _listaediciones.ElementAt(fila.Index).Tomo = detalle.Tomo;
            _listaediciones.ElementAt(fila.Index).Stock = valor;
            fila.Cells[stockcol].Value = valor;
            
        }

        private void dgvTomo_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            KryptonMessageBox.Show("¡Datos inválidos en la Modificación!");
            return;
        }

        private void dgvEdicion_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            KryptonMessageBox.Show("¡Datos inválidos en la Modificación!");
            return;
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            DetalleTomoEdicion nuevoDetalle = new DetalleTomoEdicion();

            if (combo == true)
            {
                nuevoDetalle = Tomo();
                if (comprobarElemento(nuevoDetalle))
                {
                    KryptonMessageBox.Show("¡Tomo ya Ingresado!");
                    return;
                }
                if (listadgvtemp.Count > 0)
                {
                    var det = listadgvtemp.Where(x => x.Tomo == nuevoDetalle.Tomo);

                    if (det.Count() > 0)
                    {
                        det.ElementAt(0).Stock = nuevoDetalle.Stock;
                        RestarStock(nuevoDetalle.Stock, dgvTomo);
                        _listatomos.Add(det.ElementAt(0));
                        listadgvtemp.Remove(det.ElementAt(0));
                    }
                    else
                    {
                        RestarStock(nuevoDetalle.Stock, dgvTomo);
                        _listatomos.Add(nuevoDetalle);
                    }
                }
                else
                {
                    RestarStock(nuevoDetalle.Stock, dgvTomo);
                    _listatomos.Add(nuevoDetalle);
                }
                CargaDgvTomo(_listatomos);
            }
            else
            {
                nuevoDetalle = Edicion();
                if (comprobarElemento(nuevoDetalle))
                {
                    KryptonMessageBox.Show("¡Edición ya Ingresada!");
                    return;
                }
                if (listadgvtemp.Count > 0)
                {
                    var det = listadgvtemp.Where(x => x.Edicion == nuevoDetalle.Edicion);

                    if (det.Count() > 0)
                    {
                        det.ElementAt(0).Stock = nuevoDetalle.Stock;
                        RestarStock(nuevoDetalle.Stock, dgvEdicion);
                        _listaediciones.Add(det.ElementAt(0));
                        listadgvtemp.Remove(det.ElementAt(0));
                    }
                    else
                    {
                        RestarStock(nuevoDetalle.Stock, dgvEdicion);
                        _listaediciones.Add(nuevoDetalle);
                    }
                }
                else
                {
                    RestarStock(nuevoDetalle.Stock, dgvEdicion);
                    _listaediciones.Add(nuevoDetalle);
                }
                CargaDgvEdicion(_listaediciones);
            }

        }

        private void btnAgregarlista_Click(object sender, EventArgs e)
        {
            CargaListados();
            _editarProducto._listaTomoEdicion = _tomoedicionlistalocal;
            _editarProducto._listaTomoEdicionDel = listadgvtemp;
            _editarProducto.cambiarCheck(true, 3);
            Close();
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

        private int RestarStock(int cantidad, DataGridView datag)
        {
            int value = 0;
            SumarTotalStock(datag);
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

        public void CargaListados()
        {
            _tomoedicionlistalocal = new List<DetalleTomoEdicion>();
            foreach (DetalleTomoEdicion item in _listatomos)
                _tomoedicionlistalocal.Add(item);

            foreach (DetalleTomoEdicion item in _listaediciones)
                _tomoedicionlistalocal.Add(item);
        }

        private void dgvTomo_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var fila = dgvTomo.CurrentRow;
            stockPrevio = Convert.ToInt32(fila.Cells[stockcol].Value.ToString());
            //SumarTotalStock(dgvTomo);
        }

        private void dgvEdicion_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var fila = dgvEdicion.CurrentRow;
            stockPrevio = Convert.ToInt32(fila.Cells[stockcol].Value.ToString());
            //SumarTotalStock(dgvEdicion);
        }

        private void SumarTotalStock(DataGridView datag)
        {
            stockTotal = 0;
            foreach (DataGridViewRow row in datag.Rows)
            {
                stockTotal += Convert.ToInt32(row.Cells[stockcol].Value.ToString());
            }            
        }
    }
}
