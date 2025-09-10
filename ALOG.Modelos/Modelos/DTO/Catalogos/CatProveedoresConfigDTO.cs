namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatProveedoresConfigDTO
    {
        public int IdCatProvConfig { get; set; }


        public int IdCatProveedor { get; set; }


        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Required]

        public int IdCatTipoConfig { get; set; }


        public Double Valor1 { get; set; }
        public String Valor2 { get; set; }


        public int IdUsuarioRegistro { get; set; }

    }
}
