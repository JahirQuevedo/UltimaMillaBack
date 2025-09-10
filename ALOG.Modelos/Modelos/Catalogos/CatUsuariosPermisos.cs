using ALOG.Modelos.Modelos.DTO;
namespace ALOG.Modelos.Modelos.Catalogos
{
    public class CatUsuariosPermisos : IActivable
    {
        [Key]
        public int IdCatUsuariosPermisos { get; set; }

        [ForeignKey("CatUsuarios")]
        public int IdCatUsuarios { get; set; }
        public CatUsuarios CatUsuarios { get; set; }

        [ForeignKey("CatPermisos")]
        public int IdCatPermisos { get; set; }
        public CatPermisos CatPermisos { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public bool Crear { get; set; }
        public bool Guardar { get; set; }
        public bool Actualizar { get; set; }
        public bool Eliminar { get; set; }
        public bool Impresion { get; set; }
        public bool Exportar { get; set; }
        public bool Notificar { get; set; }
        public bool Autorizar { get; set; }
        public bool Enviar { get; set; }

    }
}
