using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDatos.ListasPersonalizadas;
using CapaDatos.Models.Estudiante;
using CapaDatos.Repository;
using CapaDatos.Validation;
using ComponentFactory.Krypton.Toolkit;




namespace Sistema.Forms.modulo_estudiantes
{
    public partial class Estudiante : BaseContext
    {
        private EstudianteRepository _EstudianteRepository = null;
        public Estudiante()
        {
            _EstudianteRepository = new EstudianteRepository(_context);
            InitializeComponent();
        }

        private void Estudiante_Load(object sender, EventArgs e)
        {
            

        }
        private void GuardaEstudiante()
        {
            //if (string.IsNullOrEmpty(txtapellido.Text) || string.IsNullOrEmpty(txtNombre.Text) ||
            //     string.IsNullOrEmpty(txtEstablecimiento.Text))|| string.IsNullOrEmpty(txtCUI.Text) ||
            //{ KryptonMessageBox.Show("Campos Vacios, Todos son Obligatorios "); return; }

            //var modeloEstudiante = GetnewEstudiante();
            //if (!ModelState.IsValid(modeloEstudiante)) { return; }
            //_.Add(modeloEstudiante);

            //KryptonMessageBox.Show("Estudiante Guardado!");


        }

        
    }
}