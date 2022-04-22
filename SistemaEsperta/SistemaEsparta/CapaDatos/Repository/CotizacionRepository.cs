using CapaDatos.Data;
using CapaDatos.ListasPersonalizadas;
using CapaDatos.Models.Cotizacion;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repository
{
   public class CotizacionRepository
    {
        private Context _context = null;
        public CotizacionRepository(Context context)
        {
            _context = context;
        }

       

        public void AddEncabezado(Cotizacion cotizacion, bool saveChanges = true)
        {
            _context.Cotizaciones.Add(cotizacion);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }
        public void AddDetalles(DetalleCotizacion detallecotiza, bool saveChanges = true)
        {
            _context.DetalleCotizaciones.Add(detallecotiza);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }
        public void Updatecotizacion(Cotizacion cotiz)
        {
            _context.Entry(cotiz).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateDetalleCotizacion(DetalleCotizacion cotiz)
        {
            _context.Entry(cotiz).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public IList<ListarCotizaciones> GetListGenerales(int sucursalId)
        {

            var cotizacion = _context.Cotizaciones.AsQueryable();
            if (sucursalId != 0)
            {
                cotizacion = cotizacion.Where(a => a.SucursalId == sucursalId);
            }

            if (cotizacion.Count() > 0)
            {

                return cotizacion
                   .Include(a => a.DetalleCotizaciones).OrderByDescending(a => a.FechaRecepcion)

                   .Where(a => a.IsActive == false)
                   .Select(x => new ListarCotizaciones
                   {


                       Id = x.Id,
                       Nombre = x.Nombre,
                       Apellido = x.Apellido,
                       Telefono = x.Telefono,
                       Nit = x.Nit,
                       NoCotizacion = x.NoCotizacion,
                       FechaRecepcion = x.FechaRecepcion,
                       Sucursal = x.Sucursal.NombreSucursal,
                       Estado = x.Estado == true ? "Comprado" : "Petición",
                       FechaLimite = x.FechaLimite,
                       NombreVendedor = x.NombreVendedor,
                       Cliente = x.Cliente.Nombres,
                       ClienteId = x.ClienteId,
                       Total = x.DetalleCotizaciones.Sum(b => b.PrecioTotal)

                   }

                   )
                   .ToList();
            }
            else
            {
                return new List<ListarCotizaciones>();
            }

        }

        public Cotizacion GetCotizacion(Guid id)
        {
            return _context.Cotizaciones.Where(x => x.Id == id).FirstOrDefault();
        }
        public Cotizacion GetCotizByCorrel(string numero)
        {
            return _context.Cotizaciones.Where(x => x.NoCotizacion == numero).FirstOrDefault();
        }

        public List<ListarDetalleCotiz> GetListDetalleCotiz(Guid id)
        {
           
            

            return _context.DetalleCotizaciones
                    .Include(x => x.Producto)
                    .Where(a => a.CotizacionId == id)
                    .Select(x => new ListarDetalleCotiz
                    {

                        Id = x.Id,
                       CotizacionId= x.CotizacionId,
                        ProductoId = x.ProductoId == null ? 0 : (int)x.ProductoId,
                        ComboId=x.ComboId == null? Guid.Empty:(Guid)x.ComboId,
                       Color =x.DetalleColor.Color,
                       Talla=x.DetalleTalla.Talla,
                        Descripcion = x.ProductoId==null? x.Combo.Descripcion:x.Producto.Descripcion,
                        Cantidad = x.Cantidad,
                        Precio = x.Precio,
                        Total = x.PrecioTotal,
                        Descuento=x.Descuento,
                        DetalleColorId=x.DetalleColorId==null? 0:(int)x.DetalleColorId,
                        DetalleTallaId=x.DetalleTallaId == null ? 0 : (int)x.DetalleTallaId,
                        TallayColorId =x.DetalleColorTallaId == null ? 0 : (int)x.DetalleColorTallaId,

                    })
                    .ToList();
        }
        public List<DetalleCotizacion> GetDetalleCotizacionslista(Guid idcotizacion)
        {
            return _context.DetalleCotizaciones.Where(x => x.CotizacionId == idcotizacion).ToList();
        }


        public void DeleteDetalleCotizacion(DetalleCotizacion detalleCotizacion)
        {
            _context.Entry(detalleCotizacion).State = System.Data.Entity.EntityState.Deleted;
            _context.SaveChanges();
        }

        public DetalleCotizacion GetDetalleCotizacion(Guid Id)
        {
            return _context.DetalleCotizaciones.Where(x => x.Id == Id).ToList().FirstOrDefault();
        }

        public string GetLastCotizacion(int sucursal)
        {
            var listadocot = _context.Cotizaciones.ToList();
            if (listadocot.Count() > 0)
            {
                listadocot = listadocot.Where(x => x.SucursalId == sucursal).ToList();
                listadocot = listadocot.OrderByDescending(x => x.NoCotizacion).ToList();
                if (listadocot.Count() > 0)
                {
                    string noSolicitud = listadocot.FirstOrDefault().NoCotizacion;
                    return noSolicitud;
                }
                else
                {
                    return "";
                }
                
            }
            else
            {
                return "";
            }
        }
    }
}
