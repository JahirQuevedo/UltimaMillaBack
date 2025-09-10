using ALOG.Modelos.Modelos.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALOG.Repositorios.Repositorio.Catalogos.ICatalogo
{
    public interface ICatTipoEventosCronRepositorio
    {
        Task<List<CatTipoEventosCron>> obtenerTipoEventosCronCoincidencia();

    }
}
