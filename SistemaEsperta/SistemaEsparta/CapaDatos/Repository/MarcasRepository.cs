using CapaDatos.Data;
using CapaDatos.Models.Productos;
using sharedDatabase.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repository
{
    public class MarcasRepository
    {

        private Context _context = null;

        public MarcasRepository(Context context)
        {
            _context = context;
        }

        public void Add(Marca marca, bool saveChanges = true)
        {
            _context.Marcas.Add(marca);

            if (saveChanges)
            {
                _context.SaveChanges();

            }

        }


        public void Update(Marca marca, bool saveChanges = true)
        {
            _context.Entry(marca).State = EntityState.Modified;

            if (saveChanges)
            {
                _context.SaveChanges();

            }
        }


        public IList<Marca> GetAllMarcas()
        {
            return _context.Marcas.ToList();
        }

        public Marca GetMarca(int id)
        {
            return _context.Marcas
                 .Include(r => r.Id == id)
                .FirstOrDefault();
        }


        public void DeleteMarca(Marca marca)
        {
            _context.Entry(marca).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public IList<Marca> GetList()
        {
            return _context.Marcas
                .OrderBy(a => a.Nombre)
                .ToList();
        }


        public List<int> GetMarcaName(string name)
        {
            List<Marca> marcas = GetAllMarcas().ToList();
            List<int> ids = new List<int>();
            if (marcas.Count > 0)
            {
                marcas = marcas.Where(a =>  a.Nombre.Contains(name)).ToList();
                ids = marcas.Select(x => x.Id).ToList();
            }
            return ids;
        }
    }
}
