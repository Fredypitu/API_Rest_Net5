using JMusik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMusik.Data.Contratos
{
    public interface IGenericoRepositorio<TEntity> where TEntity:class
    {
        Task<TEntity> Agregar(TEntity entity);
        Task<bool> Modificar(TEntity entity);
        Task<bool> Eliminar(TEntity entity);
        Task<TEntity> ObtenerById(int id);
        Task<List<TEntity>> ObtenerAll();
    }
}
