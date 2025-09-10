using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Modelos.Modelos.Peticiones;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio
{
    public interface IPeticionesContenedorCronRepo
    {
        Task<List<PeticionesContenedoresCron>> obtenerCronologiaporContenedor(int pIdContenedor, int pIdServicio);
        Task<RespuestaGenericaDTO> agregarCronologiaporContenedor(PeticionesContenedoresCron pPeticionesContenedoresCron);
        Task<RespuestaGenericaDTO> actualizarCronologiaporContenedor(PeticionesContenedoresCron pPeticionesContenedoresCron);
        Task<RespuestaGenericaDTO> bajaCronologiaporContenedor(int pIdContenedorCron);


    }
}
