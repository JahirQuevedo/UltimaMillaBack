namespace ALOG.Modelos.Modelos.DTO.Provision
{
    public class ProvisionDetDTO
    {
        public int IdProvisionesDet { get; set; }
        public int IdProvisionesEnc { get; set; }

        public string ReferenciaFacturaALO { get; set; } //Referencia de factura
        public string Contenedor { get; set; }
        public int Partida { get; set; }
        public string ClaveServicioExt { get; set; }
        public int Cantidad { get; set; }
        public double Costo { get; set; }
        public double Impuesto { get; set; }
        public double TotalPartida { get; set; }
        public string Observaciones { get; set; }

        //public ProvisionEnc provisionesEnc { get; set; }
        // public int IdReferencia { get; set; }
        //public PeticionesReferencias referencia { get; set; }
        // public int IdContenedor { get; set; }
        //public PeticionesContenedores contenedor { get; set; }


    }
}
