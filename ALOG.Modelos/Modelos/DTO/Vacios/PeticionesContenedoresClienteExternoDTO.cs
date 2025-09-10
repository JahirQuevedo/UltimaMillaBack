namespace ALOG.Modelos.Modelos.DTO.Vacios
{
    public class PeticionesContenedoresClienteExternoDTO
    {
        #region BACKING FIELDS
        private string _contenedor;
        private string _clienteRFC;
        private string _clienteRazonSocial;
        private string _nombreEjecutivoSolicitante;
        #endregion BACKING FIELDS

        public string Contenedor 
        { get => _contenedor; 
          set => _contenedor = value?.ToUpper(); 
        }
        public int IdCatTipoContenedor { get; set; }
        public string ClienteRFC
        {
            get => _clienteRFC;
            set => _clienteRFC = value?.ToUpper();
        }
        public string ClienteRazonSocial
        {
            get => _clienteRazonSocial;
            set => _clienteRazonSocial = value?.ToUpper();
        }
        public string NombreEjecutivoSolicitante
        {
            get => _nombreEjecutivoSolicitante;
            set => _nombreEjecutivoSolicitante = value?.ToUpper();
        }
        private string _referenciaCliente;
        public string ReferenciaCliente 
        { 
            get => _referenciaCliente; 
            set => _referenciaCliente = value?.ToUpper(); 
        }
        public ICollection<PeticionesServiciosClienteExternoDTO> Servicios { get; set; }
    }
}
