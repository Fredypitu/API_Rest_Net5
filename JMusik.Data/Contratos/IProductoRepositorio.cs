using JMusik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMusik.Data.Contratos
{
    public interface IProductoRepositorio:IGenericoRepositorio<Producto>
    {
        // Aqui van los metodos que son exclusivo para PRODUCTO
    }
}
