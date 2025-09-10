namespace ALOG.Modelos.Modelos.DTO.Vacios
{
    public class PeticionesServiciosClienteExternoDTO
    {
        [Required(ErrorMessage = "El campo IdCatServicio es requerido")]
        public int IdCatServicio { get; set; }

        #region CAMPOS REQUERIDOS PARA EL SERVICIO DE MANIOBRA DE VACIO(Servicio == 1)
        #region DATOS DEL TRANSPORTISTA
        private string _transporteRFC;
        public string TransporteRFC 
        { 
            get => _transporteRFC; 
            set => _transporteRFC = value?.ToUpper(); 
        }

        private string _transporteRazonSocial;
        public string TransporteRazonSocial
        {
            get => _transporteRazonSocial;
            set => _transporteRazonSocial = value?.ToUpper();
        }

        private string _transporteNombreContacto;
        public string TransporteNombreContacto
        {
            get => _transporteNombreContacto;
            set => _transporteNombreContacto = value?.ToUpper();
        }
        
        public string TransporteEmailContacto { get; set; }

        #endregion DATOS DEL TRANSPORTISTA

        #region DATOS NAVIERA
        private string _numeroBL;
        public string NumeroBL
        {
            get => _numeroBL;
            set => _numeroBL = value?.ToUpper();
        }

        private string _navieraRFC;
        public string NavieraRFC
        {
            get => _navieraRFC;
            set => _navieraRFC = value?.ToUpper();
        }

        private string _navieraRazonSocial;
        public string NavieraRazonSocial
        {
            get => _navieraRazonSocial;
            set => _navieraRazonSocial = value?.ToUpper();
        }

        private string _nombreBuque;
        public string NombreBuque
        {
            get => _nombreBuque;
            set => _nombreBuque = value?.ToUpper();
        }
        public string NumeroViaje { get; set; }

        public PeticionesDocumentosClienteExternoDTO BL { get; set; }

        #region SI NAVIERA ES HYUNDAI DEBE INDICAR EL PATIO
        public PeticionesDocumentosClienteExternoDTO SoporteNaviera { get; set; }
        #region SI EL PATIO ES HAZESA LLENAR LOS SIGUIENTES DATOS
        private string _nombreConductor;
        public string NombreConductor
        {
            get => _nombreConductor;
            set => _nombreConductor = value?.ToUpper();
        }
        private string _numLicenciaConductor;
        public string NumLicenciaConductor
        {
            get => _numLicenciaConductor;
            set => _numLicenciaConductor = value?.ToUpper();
        }
        private string _numUnidad;
        public string NumUnidad
        {
            get => _numUnidad;
            set => _numUnidad = value?.ToUpper();
        }
        private string _placaUnidad;
        public string PlacaUnidad
        {
            get => _placaUnidad;
            set => _placaUnidad = value?.ToUpper();
        }
        public PeticionesDocumentosClienteExternoDTO EirDeLleno { get; set; }

        #endregion SI EL PATIO ES HAZESA LLENAR LOS SIGUIENTES DATOS
        #endregion SI NAVIERA ES HYUNDAI DEBE INDICAR EL PATIO

        #endregion DATOS NAVIERA
        #endregion CAMPOS REQUERIDOS PARA EL SERVICIO DE MANIOBRA DE VACIO(Servicio == 1)

        #region CAMPOS REQUERIDOS PARA EL SERVICIO DE GESTION DE EIR(Servicio == 2)
        public int IdCatPatio { get; set; }

        private string _patioRFC;
        public string PatioRFC 
        { 
            get => _patioRFC; 
            set => _patioRFC = value?.ToUpper(); 
        }

        private string _patioRazonSocial;
        public string PatioRazonSocial
        {
            get => _patioRazonSocial;
            set => _patioRazonSocial = value?.ToUpper();
        }
        public string FolioManiobra { get; set; }
        #endregion

        #region CAMPOS REQUERIDOS PARA EL SERVICIO DE RECUPERACION DE GARANTIAS (Servicio == 3)
        private string _consignatarioRFC;
        public string ConsignatarioRFC
        {
            get => _consignatarioRFC;
            set => _consignatarioRFC = value?.ToUpper();
        }

        private string _consignatarioRazonSocial;
        public string ConsignatarioRazonSocial
        {
            get => _consignatarioRazonSocial;
            set => _consignatarioRazonSocial = value?.ToUpper();
        }

        private string _nombreInstitucionBancaria;
        public string NombreInstitucionBancaria
        {
            get => _nombreInstitucionBancaria;
            set => _nombreInstitucionBancaria = value?.ToUpper();
        }
        public int  NumeroCuentaEgresoPago { get; set; }
        public double MontoSoliciud { get; set; }
        public double MontoTotal { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime? FechaTocaPiso { get; set; }

        //Recibir os siguientes documentos aqui:
        // ComprobantePago -> Documento (No imagenes)
        // Corte de demoras
        #endregion
        public string ReferenciaClienteFacturar { get; set; }
        public string RFCFacturar { get; set; }
        public string RazonSocialFacturar { get; set; }
    }
}
