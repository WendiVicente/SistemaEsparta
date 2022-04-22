using CapaDatos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos.Models.Estudiante;
using CapaDatos.ListasPersonalizadas;
using System.Data.Entity;


namespace CapaDatos.Repository
{
   
        public class EstudianteRepository
        {
            private Context _context = null;

            public EstudianteRepository(Context context)
            {
                _context = context;

            }

            public IList<ListadoEstudiantes> GetEstudiantes(int id)
            {
                var estudiante = _context.estudiantes.AsQueryable();
                if (id != 0)
                {
                    estudiante = estudiante.Where(x => x.Id == id);
                }

                return estudiante
                    .Include(r => r.EstudianteId)
                    .Select(x => new ListadoEstudiantes
                    {
                        Id = x.Id,
                        EstudianteId = x.EstudianteId,
                        Apellido = x.Apellido,
                        Nombre = x.Nombre,   
                        Curso = x.Curso,
                        Horario = x.Horario,
                        Fecha= x.Fecha,
                        Edad= x.Edad,
                        Códigopersonal = x.Códigopersonal,
                        CUI = x.CUI,
                        Residencia = x.Residencia,
                        Establecimiento = x.Establecimiento,
                        Grado = x.Grado,
                        Encargado = x.Encargado,
                       Telefono= x.Telefono,
                        

                    }).ToList();
            }

            

            }
        }

