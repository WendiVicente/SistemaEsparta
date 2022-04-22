using CapaDatos.Data;
using CapaDatos.ListasPersonalizadas;
using CapaDatos.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repository
{
    public class RepositoryUsuarios
    {
        private Context _context = null;

        public RepositoryUsuarios(Context context)
        {
            _context = context;
        }

        public IList<User> GetlistaUsuarios()
        {
            return _context.Users
                .Where(a => a.IsDeleted == false)
                
                .ToList();
        }
        public User Get(string iduser )
        {
            return _context.Users
                .Where(a => a.Id == iduser).FirstOrDefault();

                
        }
        public void Update(User user, bool saveChanges = true)
        {
            _context.Entry(user).State = System.Data.Entity.EntityState.Modified;

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public int LastToken()
        {
            var usuarios = _context.Users.ToList();
            if (usuarios.Count() > 0)
            {
                usuarios = usuarios.OrderBy(x => x.token).ToList();
                int token = usuarios.LastOrDefault().token;
                return token;
            }
            else
            {
                return 1;
            }
        }


        public IList<User> GetlistaUsuariosSucursal(int sucursal)
        {
            var usuarios = _context.Users.Where(a => a.IsDeleted == false);
            
            return usuarios
                .Where(a => a.SucursalId == sucursal)
                .ToList();
        }

        public IList<ListarUsuarios> GetListarUsuarios()
        {
            var usuarios = _context.Users.Where(a => a.IsDeleted == false);

            return usuarios
                .Select(x => new ListarUsuarios { 
                    Id = x.Id,
                    SucursalId = x.SucursalId,
                    Nombre = x.Name,
                    Usuario = x.UserName,
                    Sucursal = x.Sucursal.NombreSucursal,
                    Privilegios = x.Privilegios,
                    Token = x.token,
                    Estado = x.IsDeleted
                })
                //.Where(a => a.SucursalId == sucursal)                
                .ToList();
        }

    }
}
