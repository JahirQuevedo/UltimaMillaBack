using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.DTO.Respuestas;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ApiVacios.Controllers.Catalogos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ALOG.APICatalogos.Controllers.Catalogos
{
    [Route("[controller]")]
    [ApiController]
    public class CatDocumentosLNegocioController : GenericoController<CatDocumentosLNegocio>
    {
        private readonly IGenericoRepositorio<CatDocumentosLNegocio> _repositorio;
        private readonly ApplicationDbContext _db;
        public CatDocumentosLNegocioController(ApplicationDbContext db, IGenericoRepositorio<CatDocumentosLNegocio> repositorio) : base(repositorio)
        {
            _db = db;
        }        
    }
}
