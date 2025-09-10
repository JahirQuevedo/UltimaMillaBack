
using ALOG.Modelos.Modelos.Catalogos;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Modelos.Modelos.Vacios;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ALOG.Repositorios.Repositorio.Logistica
{
    public class VaciosValidacionRepositorio : IVaciosValidacionRepositorio
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _db;
        public VaciosValidacionRepositorio(ApplicationDbContext db, ILogger<VaciosValidacionRepositorio> logger)
        {
            _db = db;
            _logger = logger;
        }

        public bool validarAgregarPeticionReferencia(PeticionesReferencias peticionesReferencias, out List<string> pError)
        {
            pError = new List<string>();
            string pErrorSrt = "";
            //Validar ticket existente
            if (validaTicketExistente((int)peticionesReferencias.Ticket, out pErrorSrt))
            {
                pError.Add("El ticket " + peticionesReferencias.Ticket + " ya existe");
            }
            //Validar referencia NAD-contenedor-estado contenedor.
            foreach (PeticionesContenedores pcont in peticionesReferencias.Contenedores)
            {
                if (validaContenedorActivo(pcont.Contenedor))
                {
                    pError.Add("Ticket:" + peticionesReferencias.Ticket + ". El contenedor en la solicitud:" + pcont.RefenciaCliente + " contenedor: " + pcont.Contenedor + " se encuentra en operación no se puede agregar en una nueva referencia");
                }
                foreach (PeticionesServicios pServ in pcont.Servicios)
                {
                    if (validaServicioExistente(pServ.IdTipoServicio, pcont, peticionesReferencias, out pErrorSrt))
                    {
                        pError.Add("Contenedor:" + pcont.Contenedor + " Servicio:" + pServ.IdTipoServicio + " existe");
                    }
                }

            }


            //Validar referencia NAD

            //Validar referencia NAD cliente.
            //Validar contenedor existente y activo.
            //Validar tipo de servicio existente.
            //Validar datos por tipo de servicio.
            //Validar servicio existente por contenedor.

            return false;
        }

        public CatAduana obtenerAduanaClave(string pClaveAduana, List<string> pError)
        {
            CatAduana objcatAduana = _db.catAduana.Where(x => x.IdCatAduana == int.Parse(pClaveAduana)).FirstOrDefault();
            return objcatAduana;
        }

        public CatClientes obtenerClienteRFC(string pRFC, List<string> pError)
        {
            CatClientes objCatClientes = _db.catClientes.Where(x => x.RFC == pRFC).FirstOrDefault();
            return objCatClientes;
        }

        public CatClientes obtenerConsignadoRFC(string pRFC, List<string> pError)
        {
            throw new NotImplementedException();
        }

        public CatNavieras obtenerNavieraRFC(string pRFC, List<string> pError)
        {
            CatNavieras objCatNavieras = _db.catNavieras.Where(x => x.RFC == pRFC).FirstOrDefault();
            return objCatNavieras;
        }

        public CatPatios obtenerPatioNombre(string pNombrePatio, List<string> pError)
        {
            CatPatios objCatPatios = _db.catPatios.Where(x => x.RazonSocial == pNombrePatio).FirstOrDefault();
            return objCatPatios;
        }

        public CatTransportistas obtenerTransporteRFC(string pRFC, List<string> pError)
        {
            CatTransportistas objCatTransportistas = _db.catTransportistas.Where(x => x.RFC == pRFC).FirstOrDefault();
            return objCatTransportistas;
        }

        public bool validaContenedorActivo(string pContenedor)
        {
            if (_db.peticionesContenedores.Where(x => x.Contenedor == pContenedor && x.Activo == true).ToList().Count > 0)
            {
                //Hay un contenedor activo.
                return true;
            }
            return false;
        }

        public string validaContenedorEstado()
        {
            throw new NotImplementedException();
        }

        public bool validaContenedorExistente(PeticionesContenedores pContenedor, PeticionesReferencias pReferencia, out List<string> pError)
        {
            throw new NotImplementedException();
        }

        public bool validaOrdenEnProceso(Ordenes pIdOrden, out List<string> pError)
        {
            throw new NotImplementedException();
        }

        public bool validaOrdenExistente(int pIdOrden)
        {
            throw new NotImplementedException();
        }



        public bool validaReferenciaActivo(int pIdReferencia)
        {
            throw new NotImplementedException();
        }

        public bool validaReferenciaClienteActiva(string pReferenciaCliente, out string pError)
        {
            throw new NotImplementedException();
        }

        public bool validaReferenciaClienteExistente(string pReferenciaCliente, out string pError)
        {
            throw new NotImplementedException();
        }

        public string validaReferenciaEstado()
        {
            throw new NotImplementedException();
        }

        public bool validaReferenciaExistente(string pReferenciaCliente, out string pError)
        {
            throw new NotImplementedException();
        }

        public bool validaServicioActivo(int pIdServicio)
        {
            throw new NotImplementedException();
        }

        public string validaServicioEstado()
        {
            throw new NotImplementedException();
        }

        public bool validaServicioExistente(int pIdServicio, PeticionesContenedores pContenedor, PeticionesReferencias pReferencia, out string pError)
        {
            pError = "";
            //bool respuesta = _db.peticionesReferencias.Include(c => c.Contenedores).Any(c => c.IdReferencia == IdReferencia && c.Contenedores.Any(x => x.Contenedor.Equals(contenedor.Trim())));
            if (_db.peticionesContenedores.Include(c => c.Servicios).Any(c => c.Contenedor == pContenedor.Contenedor && c.Servicios.Any(x => x.IdTipoServicio == pIdServicio)))
            {
                return true;
            }

            return false;
        }

        public bool validaTicketExistente(int pIdTicket, out string pError)
        {
            pError = "";
            return true;
        }

        public bool validaReferencia(PeticionesReferencias peticionesReferencias, out List<string> pError)
        {
            throw new NotImplementedException();
        }
    }
}
