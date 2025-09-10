namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatPatiosDTO
    {
        public int IdCatPatios { get; set; }
        [Required]
        public string RazonSocial { get; set; }

        [Required]

        public int IdCatAduana { get; set; }


        [Required]

        public int IdCatProveedor { get; set; }
        public string RazonSocialProveedor { get; set; }
        public string Acronimo { get; set; }
        public bool Active { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public int IdUsuarioRegistro { get; set; }


        public ICollection<CatPatiosConfigDTO> GetCatPatiosConfigs { get; set; }
        public ICollection<CatPatiosNavierasDTO> GetCatPatiosNavieras { get; set; }
    }
}
