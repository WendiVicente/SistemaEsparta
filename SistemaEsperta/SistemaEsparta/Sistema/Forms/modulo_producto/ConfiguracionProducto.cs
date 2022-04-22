using CapaDatos.Data;
using CapaDatos.ListasPersonalizadas;
using CapaDatos.Models.Productos;
using CapaDatos.Repository;
using CapaDatos.Validation;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sistema.Forms.modulo_producto
{
    public partial class ConfiguracionProducto : BaseContext
    {
        private CategoriaProdRepository _categoriaRepository = null;
        private CategoriasPadreRepository _categoriaPadreRepository = null;
        private LineasProdRepository _lineasprodRepository = null;
        private int estadolinea = 0;
        private int estadocat = 0;
        private int estadosubcat = 0;

        public ConfiguracionProducto()
        {
            InitializeComponent();
        }

        private void ConfiguracionProducto_Load(object sender, EventArgs e)
        {
            CargaLineas();
            CargaCategorias();
            CargaSubcategorias();
        }

        #region LINEAS

        public void CargaLineas(bool LoadNewContext = true)
        {
            if (LoadNewContext)
            {
                _context = null;
                _context = new Context();
                _lineasprodRepository = null;
                _lineasprodRepository = new LineasProdRepository(_context);
            }

            var lista = _lineasprodRepository.GetAllLineas();
            BindingSource source = new BindingSource();
            source.DataSource = lista;
            DgvLineas.DataSource = typeof(List<>);
            DgvLineas.DataSource = source;
        }

        private void LimpiarLinea_Click(object sender, EventArgs e)
        {
            LimpiarLinea();
            estadolinea = 0;
        }

        private void BtnGuardarLinea_Click(object sender, EventArgs e)
        {
            GuardarLinea();
            LimpiarLinea();
        }

        private void BtnActLineas_Click(object sender, EventArgs e)
        {
            if (DgvLineas.CurrentRow == null)
            {
                return;
            }
            if (estadolinea == 1)
            {
                var row = (LineasProductos)DgvLineas.CurrentRow.DataBoundItem;
                var lineas = _lineasprodRepository.GetLinea(row.Id);

                var modeloEditar = GetModelLinea(lineas);
                if (String.IsNullOrEmpty(TxtNombLinea.Text)) { KryptonMessageBox.Show("Campo Vacio, ingresar una linea"); return; }
                if (!ModelState.IsValid(modeloEditar)) { return; }
                _lineasprodRepository.Update(lineas);
                CargaLineas(true);
                LimpiarLinea();
            }
        }

        private LineasProductos GetModelLinea(LineasProductos Lineaprod)
        {
            Lineaprod.Nombre = TxtNombLinea.Text;
            Lineaprod.Descripcion = TxtDescLinea.Text;
            return Lineaprod;
        }

        private void GuardarLinea()
        {
            if (estadolinea == 0)
            {
                var linea = GetModelLinea(new LineasProductos());
                if (String.IsNullOrEmpty(TxtNombLinea.Text)) { KryptonMessageBox.Show("Campo Vacio, ingresar un nombre de linea"); return; }
                if (!ModelState.IsValid(linea)) { return; }
                _lineasprodRepository.Add(linea);
                CargaLineas(true);
            }
        }

        public void LlenarTextLinea(int IndiceDGV)
        {
            var linea = (LineasProductos)DgvLineas.Rows[IndiceDGV].DataBoundItem;
            TxtCodLinea.Text = linea.Id.ToString();
            TxtNombLinea.Text = linea.Nombre.ToString();
            TxtDescLinea.Text = linea.Descripcion.ToString();
        }

        private void DgvLineas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LlenarTextLinea(DgvLineas.CurrentRow.Index);
            estadolinea = 1;
        }

        private void LimpiarLinea()
        {
            TxtCodLinea.Text = "";
            TxtNombLinea.Text = "";
            TxtDescLinea.Text = "";
        }

        #endregion

        #region CATEGORIAS

        public void CargaCategorias(bool LoadNewContext = true)
        {
            if (LoadNewContext)
            {
                _context = null;
                _context = new Context();
                _categoriaPadreRepository = null;
                _categoriaPadreRepository = new CategoriasPadreRepository(_context);
            }

            var lista = _categoriaPadreRepository.GetListCategoriasPadre();
            BindingSource source = new BindingSource();
            source.DataSource = lista;
            DgvCategorias.DataSource = typeof(List<>);
            DgvCategorias.DataSource = source;

            CbCategorias.DataSource = lista;
            CbCategorias.DisplayMember = "Nombre";
            CbCategorias.ValueMember = "Id";
            CbCategorias.Invalidate();
        }

        private void LimpiarCategoria_Click(object sender, EventArgs e)
        {
            LimpiarCategoria();
            estadocat = 0;
        }

        private void BtnGuardarCategoria_Click(object sender, EventArgs e)
        {
            GuardarCategoria();
            LimpiarCategoria();
        }

        private void BtnActCategorias_Click(object sender, EventArgs e)
        {
            if (DgvCategorias.CurrentRow == null)
            {
                return;
            }
            if (estadocat == 1)
            {
                var row = (CategoriaPadre)DgvCategorias.CurrentRow.DataBoundItem;
                var Categorias = _categoriaPadreRepository.GetCategoriaPadre(row.Id);

                var modeloEditar = GetModelCategoria(Categorias);
                if (String.IsNullOrEmpty(TxtDescCategoria.Text)) { KryptonMessageBox.Show("Campo Vacio, ingresar una Categoria"); return; }
                if (!ModelState.IsValid(modeloEditar)) { return; }
                _categoriaPadreRepository.Update(Categorias);
                CargaCategorias(true);
                LimpiarCategoria();
            }
        }

        private CategoriaPadre GetModelCategoria(CategoriaPadre Categoriaprod)
        {
            Categoriaprod.Nombre = TxtDescCategoria.Text;
            return Categoriaprod;
        }

        private void GuardarCategoria()
        {
            if (estadocat == 0)
            {
                var Categoria = GetModelCategoria(new CategoriaPadre());
                if (String.IsNullOrEmpty(TxtDescCategoria.Text)) { KryptonMessageBox.Show("Campo Vacio, ingresar un nombre de Categoria"); return; }
                if (!ModelState.IsValid(Categoria)) { return; }
                _categoriaPadreRepository.Add(Categoria);
                CargaCategorias(true);
            }
        }

        public void LlenarTextCategoria(int IndiceDGV)
        {
            var Categoria = (CategoriaPadre)DgvCategorias.Rows[IndiceDGV].DataBoundItem;
            TxtCodCategoria.Text = Categoria.Id.ToString();
            TxtDescCategoria.Text = Categoria.Nombre.ToString();
        }

        private void DgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LlenarTextCategoria(DgvCategorias.CurrentRow.Index);
            estadocat = 1;
        }

        private void LimpiarCategoria()
        {
            TxtCodCategoria.Text = "";
            TxtDescCategoria.Text = "";
        }

        #endregion

        #region SUBCATEGORIAS

        public void CargaSubcategorias(bool LoadNewContext = true)
        {
            if (LoadNewContext)
            {
                _context = null;
                _context = new Context();
                _categoriaRepository = null;
                _categoriaRepository = new CategoriaProdRepository(_context);
            }

            var lista = _categoriaRepository.GetListSubcategoria();
            BindingSource source = new BindingSource();
            source.DataSource = lista;
            DgvSubcategorias.DataSource = typeof(List<>);
            DgvSubcategorias.DataSource = source;
        }

        private void LimpiarSubcategoria_Click(object sender, EventArgs e)
        {
            LimpiarSubcategoria();
            estadosubcat = 0;
        }

        private void BtnGuardarSubcategoria_Click(object sender, EventArgs e)
        {
            GuardarSubcategoria();
            LimpiarSubcategoria();
        }

        private void BtnActSubcategorias_Click(object sender, EventArgs e)
        {
            if (DgvSubcategorias.CurrentRow == null)
            {
                return;
            }
            if (estadosubcat == 1)
            {
                var row = (ListarCategoriaProd)DgvSubcategorias.CurrentRow.DataBoundItem;
                var Subcategorias = _categoriaRepository.GetCategoria(row.Id);
                var modeloEditar = GetModelSubcategoria(Subcategorias);
                if (String.IsNullOrEmpty(TxtDescSubcategoria.Text)) { KryptonMessageBox.Show("Campo Vacio, ingresar una Subcategoria"); return; }
                if (!ModelState.IsValid(modeloEditar)) { return; }
                _categoriaRepository.Update(Subcategorias);
                CargaSubcategorias(true);
                LimpiarSubcategoria();
            }
        }

        private Categoria GetModelSubcategoria(Categoria Subcategoriaprod)
        {
            Subcategoriaprod.Nombre = TxtDescSubcategoria.Text;
            Subcategoriaprod.Inactivo = CheckDescSubc.Checked;
            Subcategoriaprod.CategoriaPadreId = CbCategorias.SelectedValue != null ? Convert.ToInt32(CbCategorias.SelectedValue) : 0;
            
            return Subcategoriaprod;
        }

        private void GuardarSubcategoria()
        {
            if (estadosubcat == 0)
            {
                var Subcategoria = GetModelSubcategoria(new Categoria());
                if (String.IsNullOrEmpty(TxtDescSubcategoria.Text)) { KryptonMessageBox.Show("Campo Vacio, ingresar un nombre de Subcategoria"); return; }
                if (!ModelState.IsValid(Subcategoria)) { return; }
                _categoriaRepository.Add(Subcategoria);
                CargaSubcategorias(true);
            }
        }

        public void LlenarTextSubcategoria(int IndiceDGV)
        {
            var Subcategoria = (ListarCategoriaProd)DgvSubcategorias.Rows[IndiceDGV].DataBoundItem;
            TxtCodSubcategoria.Text = Subcategoria.Id.ToString();
            TxtDescSubcategoria.Text = Subcategoria.Subcategoria.ToString();
            CheckDescSubc.Text = Subcategoria.Estado;
        }

        private void DgvSubcategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LlenarTextSubcategoria(DgvSubcategorias.CurrentRow.Index);
            estadosubcat = 1;
        }

        private void LimpiarSubcategoria()
        {
            TxtCodSubcategoria.Text = "";
            TxtDescSubcategoria.Text = "";
        }

        #endregion

    }
}

