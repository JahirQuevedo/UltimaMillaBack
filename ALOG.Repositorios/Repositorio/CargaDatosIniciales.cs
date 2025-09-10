using ALOG.Repositorios.Data;

namespace ALOG.Repositorios.Repositorio
{
    public class CargaDatosIniciales
    {
        private readonly ApplicationDbContext _db;
        public CargaDatosIniciales(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool CargaInicial()
        {
            /*CatAduana objCatAduana = new CatAduana();
            objCatAduana.Denominación = "VERACRUZ, VERACRUZ, VERACRUZ.";
            objCatAduana.Aduana = 43;
            objCatAduana.Seccion = 0;
            objCatAduana.Acronimo = "VCZ";
            _db.CatAduana.Add(objCatAduana);
            

            CatClientes objCatCliente = new CatClientes();
            objCatCliente.Ciudad = "VERACRUZ";
            objCatCliente.Estado = "VERACRUZ";
            objCatCliente.Calle = "Calle";
            objCatCliente.CodigoPostal = "91800";
            objCatCliente.Colonia = "CENTRO";
            objCatCliente.Correo = "gerencia.ti@autolog.com.mx";
            objCatCliente.NumeroExterior = "605";
            objCatCliente.NumeroInterior = "A";
            objCatCliente.RazonSocial = "NAD";
            objCatCliente.RFC = "NAD4565341E1";
            objCatCliente.telefono = "3333333333";           
            
            _db.CatClientes.Add(objCatCliente);

            CatEmpresas objCatEmpresas = new CatEmpresas();      
            objCatEmpresas.RazonSocial = "ALO";
            objCatEmpresas.RFC = "NAD4565341E1";
            
            _db.CatEmpresas.Add(objCatEmpresas);

            CatLineaNegocio objCatLineaNegocio = new CatLineaNegocio();
            objCatLineaNegocio.Nombre = "VACIOS";
            objCatLineaNegocio.Acronimo = "VC";
            _db.CatLineaNegocios.Add(objCatLineaNegocio);

            CatSistemas objCatSistemas = new CatSistemas();
            objCatSistemas.Nombre = "NAD Logística";
            objCatSistemas.IdCatCliente = 1;
            _db.CatSistemas.Add(objCatSistemas);

            CatSucursales objCatSucursales = new CatSucursales();
            objCatSucursales.Nombre = "Veracruz";
            objCatSucursales.RFC = "ALORFC";
            _db.CatSucursales.Add(objCatSucursales);*/

            return Guardar();



        }

    }


}
