using sharedDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Models.Productos
{ 
    public class LineasProductos
    {
        public LineasProductos()
        {
            Productos = new List<Producto>();
        }

        public int Id { get; set; }

        public string Nombre { get; set; }

        public ICollection<Producto> Productos { get; set; }
        
        public string Descripcion { get; set; }

    }
}
