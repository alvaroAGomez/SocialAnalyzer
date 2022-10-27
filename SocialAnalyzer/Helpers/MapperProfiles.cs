using SocialAnalyzer.Models;
using AutoMapper;
using SocialAnalyzer.Services.Models;


namespace SocialAnalyzer.Helpers
{
    public class MapperProfiles :Profile
    {
        public MapperProfiles()
        {
            CreateMap<PlanDTO, Plan>()
                .ReverseMap();
            CreateMap<UsuarioNewDTO, Usuario>()
               .ReverseMap();
            CreateMap<UsuarioDTO, Usuario>()
          .ReverseMap();
            CreateMap<RolDTO, Rol>()
               .ReverseMap();
            CreateMap<EstadoUsuarioDTO, EstadoUsuario>()
               .ReverseMap();
            CreateMap<PlanInsertDTO, Plan>()
               .ReverseMap();
        }
    }
}
