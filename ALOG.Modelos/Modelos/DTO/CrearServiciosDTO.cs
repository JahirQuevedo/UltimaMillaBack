namespace ALOG.Modelos.Modelos.DTO
{
    public class CrearServiciosDTO
    {

        public int id { get; set; }

        [Required(ErrorMessage = "La referencia es obligada")]
        [MaxLength(20, ErrorMessage = "El npumero máximo es de 60 caracteres")]
        public string referenciaALO { get; set; }

        //Se agrega en el guardado
        //public DateTime fechaSolicitud { get; set; }

        public int idCliente { get; set; }
    }
}
