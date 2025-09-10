namespace ALOG.Repositorios.Utilerias
{
    public class EnumEstados
    {
        public enum EstadosReferencias
        {
            Abierta = 1,
            EnProceso = 4,
            Terminada = 5,
            Cancelada = 6,
            Pendiente = 7
        }
        public enum TipoEstados
        {
            Abierta = 1,
            EnProceso = 2,
            Terminada = 3,
            Cancelada = 4,
            Pendiente = 5
        }
    }
}
