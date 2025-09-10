namespace ALOG.Modelos.Modelos.DTO.Utilerias
{
    public abstract class FiltroBaseDTO
    {
        public int? IdCatAduana { get; set; } = null;
    }
    
    public class FiltroTarifarioServicioDTO : FiltroBaseDTO
    {
        public int IdCatEmpresa { get; set; }
        public int IdLineaNegocio { get; set; }
    }
    
    public class FiltroPatioDTO : FiltroBaseDTO
    {
        public string? ProveedorInfo { get; set; } = null;
    }

    public class FiltrosCancelacionElementoDTO
    {
        public int IdOrden { get; set; }
        public int IdContenedor { get; set; }

        // Solo se usa si es para cancelar un servicio
        public int? IdServicio { get; set; } = null;
    }
}



