using ALOG.Modelos.Modelos.DTO.Provision;
using ALOG.Modelos.Modelos.Provision;

namespace ALOG.Repositorios.Repositorio.Provision.IProvision
{
    public interface IProvisionesEncRepositorio
    {
        List<ProvisionEnc> obtenerProvisionPorUUID(List<string> pUuid);
        List<ProvisionEnc> obtenerProvisionPorUUIDPart(List<ProvisionSolUUIDPartDTO> pUuid, out List<string> lstOutError);
    }
}
