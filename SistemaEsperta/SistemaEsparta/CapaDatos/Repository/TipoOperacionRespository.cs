using CapaDatos.Data;
using CapaDatos.Models.Pedidos;
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
    public class TipoOperacionRespository
    {
        private Context _context = null;

        public TipoOperacionRespository(Context context)
        {
            _context = context;
        }

        public void Add(TipoOperacion tipo, bool saveChanges = true)
        {
            _context.tipoOperacions.Add(tipo);

            if (saveChanges)
            {
                _context.SaveChanges();

            }
        }

        public void Update(TipoOperacion operacion, bool saveChanges = true)
        {
            _context.Entry(operacion).State = EntityState.Modified;

            if (saveChanges)
            {
                _context.SaveChanges();

            }
        }


        public IList<TipoOperacion> GetAllTipoOperaciones()
        {
            return _context.tipoOperacions.ToList();
        }

        public TipoOperacion GetTipoOperacion(int id)
        {
            return _context.tipoOperacions
                 .Include(r => r.Id == id)
                .FirstOrDefault();
        }


        public void DeleteTipoOperacion(TipoOperacion operacion)
        {
            _context.Entry(operacion).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public IList<TipoOperacion> GetList()
        {
            return _context.tipoOperacions
                .OrderBy(a => a.Nombre)
                .ToList();
        }
    }
}
