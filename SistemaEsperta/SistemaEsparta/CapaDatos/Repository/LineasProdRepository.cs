using CapaDatos.Data;
using sharedDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CapaDatos.Models.Productos;

namespace CapaDatos.Repository
{
    public class LineasProdRepository
    {
            private Context _context = null;

            public LineasProdRepository(Context context)
            {
                _context = context;
            }



            public void Add(LineasProductos linea, bool saveChanges = true)
            {
                _context.LineasProductos.Add(linea);

                if (saveChanges)
                {
                    _context.SaveChanges();

                }

            }


            public void Update(LineasProductos linea, bool saveChanges = true)
            {
                _context.Entry(linea).State = EntityState.Modified;

                if (saveChanges)
                {
                    _context.SaveChanges();

                }


            }


            public List<LineasProductos> GetAllLineas()
            {
                return _context.LineasProductos.ToList();


            }
            public LineasProductos GetLinea(int idLinea)
            {
                return _context.LineasProductos
                      .Where(r => r.Id == idLinea)
                    .FirstOrDefault();
            }

           
            public void DeleteLinea(LineasProductos linea)
            {
                _context.Entry(linea).State = EntityState.Deleted;
                _context.SaveChanges();


            }

        }
    }

