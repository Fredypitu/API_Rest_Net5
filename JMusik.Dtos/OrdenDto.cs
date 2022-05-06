using JMusik.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JMusik.Dtos
{
    public class OrdenDto
    {
        public OrdenDto()
        {
            DetallesOrden = new List<DetalleOrdenDto>();
        }

        public int Id { get; set; }
        public decimal CantidadArticulos { get; set; }
        public decimal Importe { get; set; }        
        public DateTime FechaRegistro { get; set; } //--> Se establece en el servidor
        public int UsuarioId { get; set; }
        public string Usuario { get; set; }
        public EstatusOrden EstatusOrden { get; set; } //--> Se establece en el servidor
        public List<DetalleOrdenDto> DetallesOrden { get; set; }

    }
}
