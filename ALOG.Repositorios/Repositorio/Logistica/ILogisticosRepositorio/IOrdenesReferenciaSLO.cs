using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALOG.Modelos.Modelos.DTO.Consultas;
using ALOG.Modelos.Modelos.DTO.Solicitudes;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;

namespace ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio
{
    public interface IOrdenesReferenciaSLO : IGenericoRepositorio<Ordenes>
    {
        Task<Ordenes> GetOrden(int IdOrden);

        Task<Ordenes> CrearOrden(Ordenes pOrden);

        Ordenes CrearOrdenServicio(Ordenes pOrden);

        bool ExisteOrden(int IdOrden);

        Task<ICollection<Ordenes>> GetOrdenesCliente(int IdCliente);

        Task<ICollection<Ordenes>> GetOrdenes(FiltroOrdenesReferenciasDTO pFiltro);

        //Task<string> ObtenerReferenciaALO(Ordenes pOrden, int pIdCatMercancia, int pIdCatTipoOperacion, int pIdCatCliente);

        Task<string> obtenerPathOrden(SolPathOrdenDTO pPathOrdenDTO);

        string ObtenerPathOrdenServicio(SolPathOrdenDTO pPathOrdenDTO);
    }
}
