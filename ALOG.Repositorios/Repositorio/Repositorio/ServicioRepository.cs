using ALOG.Modelos.Modelos;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Repositorio.IRepositorio;

namespace ALOG.Repositorios.Repositorio.Repositorio
{
    public class ServiciosRepository : IServicioRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ServiciosRepository(ApplicationDbContext db)
        {
            _db = db;
        }



        public bool ActualizarServicio(Servicios servicios)
        {
            servicios.fechaSolicitud = DateTime.Now;
            //_db.servicios.Update(servicios);
            return Guardar();

        }




        public bool BorrarServicio(Servicios servicios)
        {
            throw new NotImplementedException();
        }




        public bool CrearServicio(Servicios servicios)
        {
            servicios.fechaSolicitud = DateTime.Now;
            //_db.Servicios.Add(servicios);
            return Guardar();
        }




        public bool ExisteServicio(string referenciaAlo)
        {
            //bool valor =_db.Servicios.Any(x=> x.referenciaALO.ToLower().Trim()== referenciaAlo.ToLower().Trim());
            return false;
        }

        public bool ExisteServicio(int idServicio)
        {
            //bool valor = _db.Servicios.Any(x => x.id == idServicio);
            return false;
        }



        public ICollection<Servicios> GetServicios()
        {
            //return _db.Servicios.OrderBy(x => x.referenciaALO).ToList();
            throw new NotImplementedException();
        }

        public Servicios GetServicios(int idServicio)
        {
            // return _db.Servicios.FirstOrDefault(x => x.id == idServicio);
            throw new NotImplementedException();
        }

        public bool Guardar()
        {

            return _db.SaveChanges() >= 0 ? true : false;
        }



    }
}
