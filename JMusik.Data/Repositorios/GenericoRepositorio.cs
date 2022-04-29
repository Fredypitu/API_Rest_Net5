using JMusik.Data.Contratos;
using JMusik.Models;
using Microsoft.EntityFrameworkCore;
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
        public GenericoRepositorio(TDbContext context)
        {
            if (context==null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }
        public virtual async Task<TEntity> Agregar(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
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
            catch (Exception)
            {

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
            catch (Exception)
            {

                throw;
            }
        }

        public virtual async Task<List<TEntity>> ObtenerAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> ObtenerById(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
    }
}
