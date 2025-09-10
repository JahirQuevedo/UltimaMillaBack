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
    public class CatTipoEventosCronRepositorio : ICatTipoEventosCronRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IGenericoRepositorio<CatTipoEventosCron> _ctgenericoRepositorio;
        public CatTipoEventosCronRepositorio(ApplicationDbContext context, IGenericoRepositorio<CatTipoEventosCron> ctgenericoRepositorio)
        {
            _context = context;
            _ctgenericoRepositorio = ctgenericoRepositorio;

        }

        public async Task<List<CatTipoEventosCron>> obtenerTipoEventosCronCoincidencia()
        {
            try
            {
                var objRespuesta = await _context.catTipoEventosCron.ToListAsync();
                return objRespuesta;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
