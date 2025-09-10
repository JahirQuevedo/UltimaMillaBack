using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Catalogos;
using ALOG.Modelos.Modelos.DTO.Vacios;
using ALOG.Modelos.Modelos.Vacios;
using AutoMapper;

namespace ApiVacios.VaciosMapper
{
    public class VaciosMapper : Profile
    {
        public VaciosMapper()
        {
            //CreateMap<Servicios, ServiciosDTO>().ReverseMap();
            //CreateMap<Servicios, CrearServiciosDTO>().ReverseMap();
            CreateMap<PeticionesReferencias, PeticionesReferenciasDTO>().ReverseMap();
            CreateMap<PeticionesContenedores, PeticionesContenedoresDTO>().ReverseMap();
            CreateMap<PeticionesDocumentos, PeticionesDocumentosDTO>().ReverseMap();
            CreateMap<PeticionesServicios, PeticionesServiciosDTO>().ReverseMap();
            CreateMap<CatDocumentos, CatDocumentosDTO>().ReverseMap();
        }
    }
}
