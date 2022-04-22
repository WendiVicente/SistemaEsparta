using CapaDatos.Data;
using CapaDatos.Models.Productos;
using sharedDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repository
{
    public class CategoriasPadreRepository
    {
        private Context _context = null;

        public CategoriasPadreRepository(Context context)
        {
            _context = context;
        }

        public void Add(CategoriaPadre categoriap, bool saveChanges = true)
        {
            _context.CategoriasPadres.Add(categoriap);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }


        public IList<CategoriaPadre> GetListCategoriasPadre()
        {
            return _context.CategoriasPadres.ToList();
        }


        public CategoriaPadre GetCategoriaPadre(int id)
        {
            return _context.CategoriasPadres
              .Where(a => a.Id == id).FirstOrDefault();
        }



        public void Update(CategoriaPadre categoriap, bool saveChanges = true)
        {
            _context.Entry(categoriap).State = System.Data.Entity.EntityState.Modified;

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(CategoriaPadre categoriap, bool saveChanges = true)
        {
            _context.Entry(categoriap).State = System.Data.Entity.EntityState.Deleted;

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

    }
}
