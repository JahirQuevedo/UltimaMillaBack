
namespace ALOG.Modelos.Modelos.Catalogos {
    public class CatCondicionPago {
        public int IdcatCondicionPago { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdCatUsuario { get; set; }
    }
}
