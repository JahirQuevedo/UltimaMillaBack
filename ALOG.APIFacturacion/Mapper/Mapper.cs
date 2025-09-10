using ALOG.Modelos.Modelos.DTO.Provision;
using ALOG.Modelos.Modelos.DTO.Vacios;
using ALOG.Modelos.Modelos.Provision;
using ALOG.Modelos.Modelos.Vacios;
using AutoMapper;

namespace ALOG.APIFacturacion.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<PeticionesReferencias, PeticionesReferenciasDTO>().ReverseMap();
            CreateMap<PeticionesContenedores, PeticionesContenedoresDTO>().ReverseMap();
            CreateMap<PeticionesDocumentos, PeticionesDocumentosDTO>().ReverseMap();
            CreateMap<PeticionesServicios, PeticionesServiciosDTO>().ReverseMap();

            // Nuevas configuraciones de mapeo
            CreateMap<ProvisionEnc, ProvisionEncDTO>()
                .ForMember(dest => dest.provisionesDet, opt => opt.MapFrom(src => src.provisionesDet))
                .ReverseMap();

            CreateMap<ProvisionDet, ProvisionDetDTO>()
                .ForMember(dest => dest.ReferenciaFacturaALO, opt => opt.MapFrom(src => src.peticionesReferencia))
                .ForMember(dest => dest.Contenedor, opt => opt.MapFrom(src => src.peticionesContenedor))
                .ReverseMap();
        }
    }
}
