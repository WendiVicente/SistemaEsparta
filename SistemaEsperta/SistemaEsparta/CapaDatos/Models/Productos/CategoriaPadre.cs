using sharedDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Models.Productos
{
    public class CategoriaPadre
    {

        public CategoriaPadre()
        {
            Categorias = new List<Categoria>();
        }

        public int Id { get; set; }
       
        public string Nombre { get; set; }

        public ICollection<Categoria> Categorias { get; set; }
    }
}
