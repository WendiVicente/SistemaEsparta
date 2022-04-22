using CapaDatos.Data;
using CapaDatos.Models.DocumentoSAT;
using CapaDatos.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Forms.modulo_facturacion
{
    public partial class verCertificadosFel : BaseContext
    {
        private CertificadoSatRepository _certificadoSatRepository = null;
        private List<DocumentoCertificadoSAT> _listaDocumentos= null;
        public verCertificadosFel()
        {
            _certificadoSatRepository = new CertificadoSatRepository(_context);
           
            InitializeComponent();
        }

        private void verCertificadosFel_Load(object sender, EventArgs e)
        {
            _listaDocumentos = _certificadoSatRepository.GetlistaDoCertificadoSAT();
            RefrescarDataGridCert(_listaDocumentos);
        }
        public void RefrescarDataGridCert(List<DocumentoCertificadoSAT> lista, bool loadNewContext = true)
        {
            if (loadNewContext)
            {
                _context = null;
                _context = new Context();
                _certificadoSatRepository = null;
                _certificadoSatRepository = new CertificadoSatRepository(_context);
            }

            BindingSource source = new BindingSource();
            // var clientes = _clientesRepository.GetList(filtroid);
            source.DataSource = lista;
           // dgvcertificados.DataSource = typeof(List<>);
            dgvcertificados.DataSource = source;
            dgvcertificados.AutoResizeColumns();
           // dgvcertificados.Columns[0].Visible = false;


        }

    }
}
