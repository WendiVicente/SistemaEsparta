using CapaDatos.Data;
using sharedDatabase.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repository
{
    public class DevolucionRepository
    {
        private Context _context = null;

        public DevolucionRepository(Context context)
        {
            _context = context;
        }

        public void Add(Devoluciones devolociones, bool saveChanges = true)
        {
            _context.Devoluciones.Add(devolociones);

            if (saveChanges)
            {
                _context.SaveChanges();

            }
        }

        public void Update(Devoluciones devolociones, bool saveChanges = true)
        {
            _context.Entry(devolociones).State = EntityState.Modified;

            if (saveChanges)
            {
                _context.SaveChanges();

            }
        }

        public IList<Devoluciones> GetAll()
        {
            return _context.Devoluciones.ToList();
        }

        public Devoluciones GetDevoluciones(int id)
        {
            return _context.Devoluciones
                 .Include(r => r.Id == id)
                .FirstOrDefault();
        }


        public void Delete(Devoluciones devoluciones)
        {
            _context.Entry(devoluciones).State = EntityState.Deleted;
            _context.SaveChanges();
        }


        public List<Devoluciones> DevolucionesByCuenta(Guid CuentaCBId) 
        {
            var devoluciones = _context.Devoluciones.ToList();
            devoluciones = devoluciones.Where(x => x.CuentaBCId == CuentaCBId).ToList();
            return devoluciones;
        }

    }
}
