namespace ALOG.Modelos.Modelos.DTO.Solicitudes
{
    public class SolOrdenDTO
    {


        public int IdOrden { get; set; }

        [Required]
        public int IdCatCliente { get; set; }




        public int? IdCatSistema { get; set; }




        public int? IdCatAduana { get; set; }



        public int? IdCatProveedor { get; set; }



        [Required]
        public int IdCatEmpresa { get; set; }



        [Required]
        public int IdCatSucursal { get; set; }




        [Required]
        public int IdCatLineaNegocio { get; set; }




        public int? IdCatProyecto { get; set; }


        //[ForeignKey("peticionesReferencias")]
        //public int? IdPeticionReferencia { get; set; }
        //public virtual PeticionesReferencias peticionesReferencias { get; set; }

        [Required]
        public int? IdUsuario { get; set; }



        //[ForeignKey("IdPeticionReferencia")]
        //public PeticionesReferencias PeticionReferencia { get; set; }             

        public string ReferenciaALO { get; set; }


    }
}
