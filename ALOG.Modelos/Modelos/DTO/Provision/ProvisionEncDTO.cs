namespace ALOG.Modelos.Modelos.DTO.Provision
{
    public class ProvisionEncDTO
    {
        public int IdProvisionesEnc { get; set; }
        public string UUID { get; set; }

        public DateTime FechaTimbrado { get; set; }
        public string Folio { get; set; }
        //public int IdTipoMoneda { get; set; }
        public string tipoMoneda { get; set; }
        public double TipoCambio { get; set; }
        public double ImporteTotal { get; set; }
        public double Importe { get; set; }
        public List<ProvisionDetDTO> provisionesDet { get; set; }
    }
}
