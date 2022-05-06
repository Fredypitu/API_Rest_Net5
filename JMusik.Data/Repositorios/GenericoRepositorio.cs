using JMusik.Data.Contratos;
using JMusik.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMusik.Data.Repositorios
{
    public abstract class GenericoRepositorio<TEntity, TDbContext> : IGenericoRepositorio<TEntity>
        where TEntity : class
        where TDbContext : DbContext
    {
        protected readonly TDbContext _context;
        protected readonly ILogger<GenericoRepositorio<TEntity, TDbContext>> _logger;
        public GenericoRepositorio(TDbContext context, ILogger<GenericoRepositorio<TEntity, TDbContext>> logger)
        {
            if (context==null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
            _logger = logger;
        }

        public virtual async Task<TEntity> Agregar(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Agregar)}: {ex.Message}");
                return null;
            }

        }

        public virtual async Task<bool> Eliminar(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            try
            {
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Eliminar)}: {ex.Message}");
                throw;
            }
        }

        public virtual async Task<bool> Modificar(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Modificar)}: {ex.Message}");
                return false;
            }
        }

        public virtual async Task<List<TEntity>> ObtenerAll()
        {
            var xxx1 = typeof(TEntity);
            var xxx2 = await _context.Set<TEntity>().ToListAsync();
            return xxx2;
        }

        public virtual async Task<TEntity> ObtenerById(int id)
        {
            //Si no existe la Entity devuelve null
            return await _context.Set<TEntity>().FindAsync(id);
        }
    }
}
