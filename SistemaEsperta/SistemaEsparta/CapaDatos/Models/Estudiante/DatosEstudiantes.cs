using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaDatos.Models.Estudiante

  {
    public class DatosEstudiantes
    {
        [Key]
        public int Id { get; set; }
        public string EstudianteId { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Curso { get; set; }
        public DateTime Horario{ get; set; }
        public string Fecha { get; set; }
        public string Edad { get; set; }
        public string Códigopersonal  { get; set; }
        public string CUI{ get; set; }
        public string Residencia { get; set; }
        public string Establecimiento{ get; set; }
       public string Grado{ get; set; }
       public string Encargado{ get; set; }
       public String Telefono { get; set; }
 
      }
  }









