//using ApiVacios.Controllers.Catalogos;
using ApiVacios.Data;
using ApiModelos.Modelos.Catalogos;
using ApiModelos.Modelos.Orden;
using ApiModelos.Modelos.Vacios;
using ApiVacios.Repositorio.Generico.IGenerico;
using ApiVacios.Repositorio.IRepoOrdenes;
using ApiVacios.Repositorio.Logistica.ILogisticosRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiVacios.Controllers;


namespace ApiVacios.Controllers.CtrllOrdenes
{
    //[Authorize(Roles = "Admin,Externo")]
    [AllowAnonymous]
    [Route("operativo/[controller]")]
    [ApiController]
    public class OrdenesController : GenericoController<Ordenes>
    {
        private readonly IGenericoRepositorio<Ordenes> _ctRepoGen;
        private readonly IVaciosRepositorio _ctRepovacios;
        private readonly IOrdenesRepositorio _ctRepoOrdenes;
 
        public OrdenesController( IGenericoRepositorio<Ordenes> ctRepoGen) : base(ctRepoGen)
        {
            _ctRepoGen = ctRepoGen;
           
            

        }

    }
}
