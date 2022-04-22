using CapaDatos.Repository;
using ComponentFactory.Krypton.Toolkit;
using sharedDatabase.Models;
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
    public partial class NuevaMarca : BaseContext
    {

        private readonly MarcasRepository _marcasRepository = null;
        public CrearProducto crearprod = null;
        public EditarProducto editarprod = null;

        public NuevaMarca()
        {
            _marcasRepository = new MarcasRepository(_context);
            InitializeComponent();
        }


        private void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDescripcion.Text == null)
                {
                    KryptonMessageBox.Show("El campo Descripcion esta vacio");
                }
                else if (txtNombre.Text == null)
                {
                    KryptonMessageBox.Show("El campo Nombre esta vacio");
                }
                else
                {
                    Marca nuevaMarca = new Marca();
                    nuevaMarca.Nombre = txtNombre.Text;
                    nuevaMarca.Descripcion = txtDescripcion.Text;
                    _marcasRepository.Add(nuevaMarca);
                    KryptonMessageBox.Show("¡Marca Guardada Exitosamente!");
                    if (crearprod != null)
                    {
                        crearprod.CargarMarcas();
                    }
                    if (editarprod != null)
                    {
                        editarprod.CargarMarcas();
                    }
                }
                
                Close();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Ha ocurrido un error interno: " + ex.Message);
            }

        }
    }

}
