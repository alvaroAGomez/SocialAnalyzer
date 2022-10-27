using AutoMapper;
using SocialAnalyzer.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace SocialAnalyzer.Services.Helpers
{
    public class MapperProfiles : Profile
    {
        //ejemplo
        //CreateMap<Actor, DAL.Models.Actor>().ReverseMap();

        public MapperProfiles()
        {

            CreateMap<Plan, DAL.Models.Plan>()
                .ReverseMap();

            CreateMap<Usuario, DAL.Models.Usuario>()
                 .ForMember(dto => dto.IdRol, opt => opt.MapFrom(x => x.IdRol.ToString() == "" ? null : x.IdRol))
                
                .ReverseMap();

            CreateMap<Rol, DAL.Models.Rol>()
                .ReverseMap();

            CreateMap<EstadoUsuario, DAL.Models.EstadoUsuario>()
                .ReverseMap();

        }
    }
}
