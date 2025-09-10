namespace ALOG.Modelos.Modelos.DTO.Catalogos
{
    public class CatFormatoReferenciasDTO
    {
        public int IdCatFormatoRef { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]

        public int IdCatLineaNegocio { get; set; }

        public int IdUsuarioRegistro { get; set; }

        public ICollection<CatFormatoRefDetDTO> CatFormatoRefDet { get; set; }


    }
}
