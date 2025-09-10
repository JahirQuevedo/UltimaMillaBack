namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatClientesServicios
    {
        [Key]
        public int IdCatClientesServicios { get; set; }

        [Required]
        [ForeignKey("CatClientes")]
        public int IdCliente { get; set; }

        public CatClientes CatClientes { get; set; }

        [Required]

        [ForeignKey("CatServicios")]
        public int IdCatServicio { get; set; }
        public CatServicios CatServicios { get; set; }
        [Required]
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Required]
        public int IdUsuario { get; set; }


        //[ForeignKey("CatClientes")]

        //public int IdCatAduana { get; set; }

        //public CatAduana CatAduana { get; set; }

    }
}
