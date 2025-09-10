using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;
using ALOG.Repositorios.Repositorio.Generico;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALOG.Repositorios.Repositorio.Catalogos
{
    public class CatTipoIncidenciasCronRepositorio : ICatTipoIncidenciasCronRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IGenericoRepositorio<CatTipoIncidenciaCron> _ctgenericoRepositorio;
        public CatTipoIncidenciasCronRepositorio(ApplicationDbContext context, IGenericoRepositorio<CatTipoIncidenciaCron> ctgenericoRepositorio)
        {
            _context = context;
            _ctgenericoRepositorio = ctgenericoRepositorio;

        }
        public async Task<List<CatTipoIncidenciaEvento>> obtenerCatTipoIncidenciaEvento()
        {
            try
            {
                var objRespuesta = await _context.catTipoIncidenciaEvento
                                                    .Include(i => i.catTipoIncidenciaCron)
                                                    .Include(i => i.catTipoEventosCron)
                                                    .Where(i =>
                                                                i.Activo &&
                                                                i.catTipoIncidenciaCron.Activo &&
                                                                i.catTipoEventosCron.Activo
                                                          )
                                                    .ToListAsync();
                return objRespuesta;
            }
            catch (Exception)
            {

                return new List<CatTipoIncidenciaEvento>();
            }
        }
    }
}
