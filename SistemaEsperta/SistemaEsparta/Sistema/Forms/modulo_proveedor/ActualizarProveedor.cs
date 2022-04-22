using CapaDatos.Repository;
using CapaDatos.Repository.PersonalRepository;
using CapaDatos.Validation;
using ComponentFactory.Krypton.Toolkit;
using sharedDatabase.Models.Proveedores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Forms.modulo_proveedor
{
    public partial class ActualizarProveedor : BaseContext
    {
        private readonly Proveedor _proveedor = null;
        private readonly ProveedoresRepository _proveedorRepository = null;
        private readonly VerProveedores _listarproveedorForm = null;
        private PropiedadesRepository _propiedadesRepository = null;
        private SucursalesRepository _sucursalesRepository = null;

        public ActualizarProveedor(Proveedor proveedor, VerProveedores listaproveedor)
        {
            _proveedor= proveedor;
            _proveedorRepository = new ProveedoresRepository(_context);
            _listarproveedorForm= listaproveedor;
            _propiedadesRepository = new PropiedadesRepository(_context);
            _sucursalesRepository = new SucursalesRepository(_context);

            InitializeComponent();
        }

        private void ActualizarProveedor_Load(object sender, EventArgs e)
        {
            CargarModelo();
            CargarBancos();
            CargarFrecuencia();
            CargarRubro();
            CargarSucursales();
            CargarTipoProveedor();
        }
        private void CargarSucursales()
        {
            var sucursal = _sucursalesRepository.GetList();
            comboSucursal.DataSource = sucursal;
            comboSucursal.DisplayMember = "NombreSucursal";
            comboSucursal.ValueMember = "Id";
            comboSucursal.SelectedIndex = 0;
            comboSucursal.Invalidate();
        }
        private void CargarRubro()
        {
            var rubro = _propiedadesRepository.GetListRubros();
            comboRubro.DataSource = rubro;
            comboRubro.DisplayMember = "Descripcion";
            comboRubro.ValueMember = "Id";
            comboRubro.SelectedIndex = 0;
            comboRubro.Invalidate();
        }


        private void CargarFrecuencia()
        {
            var frecuencia = _propiedadesRepository.GetListFrecuencias();
            comboFrecuencia.DataSource = frecuencia;
            comboFrecuencia.DisplayMember = "Periodo";
            comboFrecuencia.ValueMember = "Id";
            comboFrecuencia.SelectedIndex = 0;
            comboFrecuencia.Invalidate();



        }
        private void CargarTipoProveedor()
        {
            var frecuencia = _propiedadesRepository.GetListTipoProveedores();
            comboTiposprov.DataSource = frecuencia;
            comboTiposprov.DisplayMember = "Descripcion";
            comboTiposprov.ValueMember = "Id";
            comboTiposprov.SelectedIndex = 0;
            comboTiposprov.Invalidate();



        }
        private void CargarBancos()
        {
            var bancos = _propiedadesRepository.GetListBancos();
            comboBancos.DataSource = bancos;
            comboBancos.DisplayMember = "Entidad";
            comboBancos.ValueMember = "Id";
            comboBancos.SelectedIndex = 0;
            comboBancos.Invalidate();

        }
        private void CargarModelo()
        {
            txtEntidad.Text = _proveedor.Nombre;
            txtdirecc.Text = _proveedor.Direccion;
            txttelefono.Text = _proveedor.Telefonos;
            checkEstado.Checked = _proveedor.IsActive;
            txttelefono2.Text = _proveedor.Telefonos2;
            comboSucursal.SelectedValue = _proveedor.SucursalId.ToString();
            comboRubro.SelectedValue = _proveedor.RubroId.ToString();
            comboFrecuencia.SelectedValue = _proveedor.FrecuenciaId.ToString();
            comboBancos.SelectedValue = _proveedor.BancoId.ToString();
            comboTiposprov.SelectedValue = _proveedor.TipoProveedorId.ToString();
            txtobservacion.Text = _proveedor.Observaciones;
            txtnit.Text = _proveedor.Nit;
            txtcuenta.Text = _proveedor.NoCuentaBancaria;
            txtemail.Text = _proveedor.Correo;
            txtcel1.Text = _proveedor.Celular2;
            txtcel2.Text = _proveedor.Celular;
        }
        private Proveedor GetViewModel(Proveedor provee)
        {
            provee.Nombre = txtEntidad.Text;
               provee.Direccion = txtdirecc.Text;
            provee.Telefonos = txttelefono.Text;
            provee.IsActive = checkEstado.Checked;
            provee.Telefonos2 = txttelefono2.Text;
            provee.SucursalId = int.Parse(comboSucursal.SelectedValue.ToString());
            provee.RubroId = int.Parse(comboRubro.SelectedValue.ToString());// txtrubro.Text,
            provee.FrecuenciaId = int.Parse(comboFrecuencia.SelectedValue.ToString());
            provee.BancoId = int.Parse(comboBancos.SelectedValue.ToString());
            provee.TipoProveedorId = int.Parse(comboTiposprov.SelectedValue.ToString());
            provee.Observaciones = txtobservacion.Text;
            provee.Nit = txtnit.Text;
            provee.Ingreso = DateTime.Now;
            provee.NoCuentaBancaria = txtcuenta.Text;
            provee.Correo = txtemail.Text;
            provee.Celular2 = txtcel2.Text;
            provee.Celular = txtcel1.Text;

            return provee;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var cliente = _proveedorRepository.Get(_proveedor.Id);

            var GetCliente = GetViewModel(cliente);

            if (ModelState.IsValid(GetCliente))
            {
                try
                {
                    _proveedorRepository.Update(GetCliente);
                    _listarproveedorForm.RefrescarDataGrid(true);
                    Close();
                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK);
                }
            }
            else
            {
                KryptonMessageBox.Show("Hay campos obligatorios sin llenar", "ERROR", MessageBoxButtons.OK);

            }
        }
    }
}
