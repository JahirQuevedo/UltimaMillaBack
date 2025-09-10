using ALOG.Modelos;
using ALOG.Modelos.Modelos.Orden;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ALOG.Repositorios.Repositorio.RepoOrdenes.IRepoOrdenes;
using Microsoft.Extensions.DependencyInjection;

namespace ALOG.Repositorios;

public class UnitOfWorkWMS : IDisposable
{
    private readonly ApplicationDbContext _context;
    private bool _disposed = false;

    private readonly IServiceProvider _serviceProvider;

    private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

    public UnitOfWorkWMS(ApplicationDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    public IGenericoRepositorio<Barco> BarcoRepositorio => GetRepository<Barco>();

    public IGenericoRepositorio<Viaje> ViajeRepositorio => GetRepository<Viaje>();

    public IGenericoRepositorio<Ordenes> OrdenesRepositorio => GetRepository<Ordenes>();

    public IBarcoRepositorio BarcoSeviceProvider => _serviceProvider.GetService<IBarcoRepositorio>();

    public IViajeRepositorio ViajeServiceProvider => _serviceProvider.GetService<IViajeRepositorio>();

    public IOrdenesRepositorio OrdenServiceProvider => _serviceProvider.GetService<IOrdenesRepositorio>();

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    private IGenericoRepositorio<T> GetRepository<T>() where T : class
    {
        if (_repositories.ContainsKey(typeof(T)))
        {
            return (IGenericoRepositorio<T>)_repositories[typeof(T)];
        }

        var repository = new GenericoRepositorio<T>(_context);
        _repositories.Add(typeof(T), repository);
        return repository;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}