using ALOG.Modelos.Modelos.DTO.Vacios;
using ALOG.Modelos.Modelos.Vacios;
using AutoMapper;

namespace ALOG.APIControl.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            //CreateMap<Servicios, ServiciosDTO>().ReverseMap();
            //CreateMap<Servicios, CrearServiciosDTO>().ReverseMap();
            CreateMap<PeticionesReferencias, PeticionesReferenciasDTO>().ReverseMap();
            CreateMap<PeticionesContenedores, PeticionesContenedoresDTO>().ReverseMap();
            CreateMap<PeticionesDocumentos, PeticionesDocumentosDTO>().ReverseMap();
            CreateMap<PeticionesServicios, PeticionesServiciosDTO>().ReverseMap();
        }
    }
}
