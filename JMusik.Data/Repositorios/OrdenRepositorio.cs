using JMusik.Data.Contratos;
using JMusik.Models;
using Microsoft.Extensions.Logging;

namespace JMusik.Data.Repositorios
{
    public class OrdenRepositorio:GenericoRepositorio<JMusik.Models.Orden, TiendaDbContext>,IOrdenRepositorio
    {
        public OrdenRepositorio(TiendaDbContext context, ILogger<OrdenRepositorio> logger): base(context, logger)
        {

        }

    }
}
