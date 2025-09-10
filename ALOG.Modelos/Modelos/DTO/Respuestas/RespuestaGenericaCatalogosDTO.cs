namespace ALOG.Modelos.Modelos.DTO.Respuestas
{
    public class RespuestaGenericaCatalogosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string RFC { get; set; }
        public string Acronimo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
        public int IdCatAduana { get; set; }
        public string Aduana { get; set; }

    }
}
