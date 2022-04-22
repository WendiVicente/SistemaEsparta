using sharedDatabase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Models.Productos
{
    public class Categoria
    {
        public Categoria()
        {
            Productos = new List<Producto>();
        }

        public int Id { get; set; }
        [Required]
        public int CategoriaPadreId { get; set; }
        public string Nombre { get; set; }
        public bool Inactivo { get; set; }

        public ICollection<Producto> Productos { get; set; }
        public CategoriaPadre CategoriaPadre { get; set; }
    }
}
