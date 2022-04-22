using CapaDatos.Data;
using CapaDatos.ListasPersonalizadas;
using CapaDatos.Models.Traslados;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repository
{
    public class TrasladosRepository
    {
        private Context _context = null;
        public TrasladosRepository(Context context)
        {
            _context = context;
        }
        public string AddEncabezado(Traslados traslado, bool saveChanges = true)
        {
            _context.Traslados.Add(traslado);

            if (saveChanges)
            {
                _context.SaveChanges();
                return "OK";
            }
            else
            {
                return "Error";
            }
        }
        public void AddDetalles(DetalleTraslados detalleTraslados, bool saveChanges = true)
        {
            _context.DetalleTraslados.Add(detalleTraslados);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }
        public void Update(Traslados trasladosid, bool saveChanges = true)
        {
            _context.Entry(trasladosid).State = System.Data.Entity.EntityState.Modified;

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }
        public List<Traslados> GetTrasladoSolicitud(int  idsucursal)
        {
            return _context.Traslados.Where(x => x.IdSucursalReceptor == idsucursal ).ToList();
        }

        public List<Traslados> GetTrasladoPeticiones(int idsucursal)
        {
            return _context.Traslados.Where(x => x.IdSucursalEmisor == idsucursal).ToList();
        }

        public IList<ListarTraslados> GetListTraslados(int sucursalId,bool  Ispeticion)
        {

            var trasladosx = _context.Traslados.AsQueryable();
            if (Ispeticion)
            {
                trasladosx = trasladosx.Where(x => x.IdSucursalEmisor == sucursalId);
            }
            else
            {
                trasladosx = trasladosx.Where(x => x.IdSucursalReceptor == sucursalId);
            }
            return trasladosx
               .Include(a => a.Sucursal)
               .Include(a=> a.User)

               //.Where(a => a.IsActive == false)
               .Select(x => new ListarTraslados
               {


                   Id = x.Id,
                   FechaRecepcion = x.FechaRecepcion,
                   SucursalEmisor = x.Sucursal.NombreSucursal,
                   SucursalReceptor= x.Sucursal.NombreSucursal,
                   Estado = x.Estado == true ? "TRASLADADO" : "PETICIÓN",
                   FechaLimite = x.FechaLimite,
                   Vendedor_Solicitante = x.User.Name,
                   Vendedor_Recibe= x.User.Name,
                   Activo= x.IsActive==true? "ANULADO":"ACTIVO",
                  

               }

               )
               .ToList();
        }

        public List<ListarDetalleTraslados> GetListDetallePedido(Guid id)
        {
            // return _context.DetallePedidos.Where(x => x.PedidoId == id).ToList();


            return _context.DetalleTraslados
                   // .Include(x => x.pro)
                    .Where(a => a.Id == id)
                    .Select(x => new ListarDetalleTraslados
                    {

                        Id = x.Id,
                        ProductoId = x.ProductoId == null ? 0 : (int)x.ProductoId,
                        
                        Color = x.DetalleColor.Color,
                        Talla = x.DetalleTalla.Talla,
                        Descripcion = x.ProductoId == null ? x.Combo.Descripcion : x.Producto.Descripcion,
                        Cantidad = x.Cantidad,
                        DetalleColorId = x.DetalleColorId == null ? 0 : (int)x.DetalleColorId,
                        DetalleTallaId = x.DetalleTallaId == null ? 0 : (int)x.DetalleTallaId,
                        TallayColorId = x.DetalleColorTallaId == null ? 0 : (int)x.DetalleColorTallaId,
                        

                    })
                    .ToList();
        }

        public Traslados GetbyNoTransac(int transaccion)
        {
            var traslados = _context.Traslados.ToList();
            Traslados trasl = new Traslados();
            if (traslados.Count() > 0)
            {
                trasl = traslados.Where(x => x.NumeroTransaccion == transaccion).FirstOrDefault();
            }
            return trasl;
        }
    }
}
