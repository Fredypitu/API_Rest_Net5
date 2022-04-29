using JMusik.Data.Contratos;
using JMusik.Models;
using JMusik.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMusik.Data.Repositorios
{
    public class ProductoRepositorio : GenericoRepositorio<Producto, TiendaDbContext>, IProductoRepositorio
    {
        public ProductoRepositorio(TiendaDbContext context) : base(context)
        {
     
        }

        public override async Task<Producto> Agregar(Producto producto)
        {
            try
            {
                producto.FechaRegistro = DateTime.UtcNow;
                producto.Estatus = EstatusProducto.Activo;
                await _context.Set<Producto>().AddAsync(producto);
                await _context.SaveChangesAsync();
                return producto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override async Task<bool> Modificar(Producto producto)
        {
            try
            {
                var productoNuevo = await ObtenerById(producto.Id);
                productoNuevo.Nombre = producto.Nombre;
                productoNuevo.Precio = producto.Precio;

                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception)
            {

                return false;
            }

        }

    }
}
