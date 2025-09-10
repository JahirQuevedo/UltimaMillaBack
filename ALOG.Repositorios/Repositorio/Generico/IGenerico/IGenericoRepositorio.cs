using System.Linq.Expressions;

namespace ALOG.Repositorios.Repositorio.Generico.IGenerico
{
    public interface IGenericoRepositorio<T> where T : class
    {
        T AgregarGenerico(T entity);
        T ActualizarGenerico(int Id, T entity);
        T BorrarGenerico(T entity);
        bool GuardarGenerico();

        // Otros métodos CRUD pueden ser definidos aquí
        Task<List<T>> obtenerListaTodosGenerico();

        Task<T> obtenerPorIdGenerico(int Id);

        bool AltaGenerico(int Id, Expression<Func<T, object>> campo, object nuevoValor);
        bool BajaGenerico(int Id, Expression<Func<T, object>> campo, object nuevoValor);



    }
}
