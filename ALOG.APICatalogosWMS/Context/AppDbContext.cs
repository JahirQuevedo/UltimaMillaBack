using ALOG.Enums;
using ALOG.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ALOG.APICatalogosWMS;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)   
    {
        
    }
    public DbSet<Barco> Barcos { get; set; }
    public DbSet<Viaje> Vajes { get; set; }
    public DbSet<Referencia> Referencias { get; set; }
    public DbSet<Paquete> Paquetes { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<TipoCargaAlmacen> TipoCargaAlmacenes { get; set; }
    public DbSet<Almacen> Almacenes { get; set; }
    public DbSet<TipoZonaAlmacenaje> TipoZonaAlmacenajes { get; set; }
    public DbSet<ZonaAlmacen> ZonaAlmacenes { get; set; }
    public DbSet<Ubicacion> Ubicaciones { get; set; }
    public DbSet<UnidadMedida> UnidadMedidas { get; set; }
    public DbSet<TipoEmbalaje> TipoEmbalajes { get; set; }
    public DbSet<Inventario> Inventarios { get; set; }
    public DbSet<Tarja> Tarjas { get; set; }
    public DbSet<Partida> Partidas { get; set; }
    public DbSet<InventarioALO> InventarioALOs { get; set; }
    public DbSet<FolioServicio> FolioServicios { get; set; }
    public DbSet<TipoTransporte> TipoTransportes { get; set; }
    public DbSet<LineaTransporte> LineaTransportes { get; set; }
    public DbSet<LineaOperador> LineaOperador { get; set; }
    public DbSet<ControlTransporte> ControlTransportes { get; set; }
    public DbSet<Maniobrista> Maniobristas { get; set; }
    public DbSet<FolioServicioMasterDetalle> FolioServicioMasterDetalles { get; set; }
    public DbSet<SolicitudTraslado> SolicitudTraslados { get; set; }
    public DbSet<ServicioFotografia> ServicioFotografias { get; set; }
    public DbSet<CodigoDesperfecto> CodigoDesperfectos { get; set; }
    public DbSet<TipoDesperfecto> TipoDesperfectos { get; set; }
    public DbSet<TipoSeveridad> TipoSeveridades { get; set; }
    public DbSet<BitacoraAveriaInventario> BitacoraAveriaInventarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Viaje>()
            .Property(p => p.PesoBLs)
            .HasColumnType("decimal(12,3)");

        modelBuilder.Entity<Paquete>()
            .HasMany<Servicio>(s => s.Servicios)
            .WithMany(c => c.Paquetes)
            .UsingEntity(d => d.ToTable("WMS_006_PAQUETE_SERVICIO"));

        modelBuilder.Entity<Referencia>()
            .Property(p => p.PesoInicial)
            .HasColumnType("decimal(14,3)");

        modelBuilder.Entity<Referencia>()
            .Property(p => p.PesoFinal)
            .HasColumnType("decimal(14,3)");

        modelBuilder.Entity<Ubicacion>()
            .Property(p => p.Altura)
            .HasColumnType("decimal(12,3)");

        modelBuilder.Entity<Ubicacion>()
            .Property(p => p.Capacidad)
            .HasColumnType("decimal(12,3)");

        modelBuilder.Entity<Inventario>()
            .Property(p => p.PesoInicial)
            .HasColumnType("decimal(12,3)");

        modelBuilder.Entity<Inventario>()
            .Property(p => p.PesoFinal)
            .HasColumnType("decimal(12,3)");

    }
}
