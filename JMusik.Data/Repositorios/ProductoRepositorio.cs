using JMusik.Data.Contratos;
using Microsoft.Extensions.Logging;
using System;
using JMusik.Models;
using System.Threading.Tasks;
using JMusik.Models.Enums;

namespace JMusik.Data.Repositorios
{
    public class ProductoRepositorio : GenericoRepositorio<JMusik.Models.Producto, TiendaDbContext>, IProductoRepositorio
    {
        public ProductoRepositorio(TiendaDbContext context, ILogger<ProductoRepositorio> logger) : base(context, logger)
        {
     
        }

        public override async Task<JMusik.Models.Producto> Agregar(JMusik.Models.Producto producto)
        {
            try
            {
                producto.FechaRegistro = DateTime.UtcNow;
                producto.Estatus = EstatusProducto.Activo;
                await _context.Set<JMusik.Models.Producto>().AddAsync(producto);
                await _context.SaveChangesAsync();
                return producto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Agregar)}: {ex.Message}");
                return null;
            }
        }

        public override async Task<bool> Modificar(JMusik.Models.Producto producto)
        {
            try
            {
                var productoNuevo = await ObtenerById(producto.Id);
                if (productoNuevo is null)
                {
                    return false;
                }
                productoNuevo.Nombre = producto.Nombre;
                productoNuevo.Precio = producto.Precio;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Modificar)}: {ex.Message}");
                throw;
            }

        }

    }
}
