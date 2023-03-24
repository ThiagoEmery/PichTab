using AutoMapper;
using PichTabs_API.Modelos;
using PichTabs_API.Modelos.Dto;

namespace PichTabs_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<EquipoModel, EquipoDto>();
            CreateMap<EquipoDto, EquipoModel>();

            CreateMap<EquipoModel, EquipoCreateDto>().ReverseMap();
            CreateMap<EquipoModel, EquipoUpdateDto>().ReverseMap();
           
        }
    }
}
