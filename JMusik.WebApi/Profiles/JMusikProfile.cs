using AutoMapper;
using JMusik.Data;
using JMusik.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMusik.WebApi.Profiles
{
    public class JMusikProfile:Profile
    {
        public JMusikProfile()
        {
            this.CreateMap<Producto, ProductoDto>().ReverseMap();
            this.CreateMap<Perfil, PerfilDto>().ReverseMap();
            this.CreateMap<Orden, OrdenDto>()
                .ForMember(dto => dto.Usuario, o => o.MapFrom(orden => orden.Usuario))
                .ReverseMap()
                .ForMember(orden => orden.Usuario, dto => dto.Ignore());
            this.CreateMap<DetalleOrden, DetalleOrdenDto>()
                .ForMember(dto => dto.Producto, d => d.MapFrom(detalleOrd => detalleOrd.Producto))
                .ReverseMap()
                .ForMember(detalleOrd => detalleOrd.Producto, dto => dto.Ignore());
        }
    }
}
