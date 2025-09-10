using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;


namespace ALOG.Repositorios.Repositorio.Generico
{
    public class GenericoRepositorio<T> : IGenericoRepositorio<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;


        public GenericoRepositorio(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T AgregarGenerico(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                //_context.SaveChanges();
                if (_context.SaveChanges() >= 0 ? true : false)
                    return entity;
                else return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public T ActualizarGenerico(int Id, T entity)
        {

            var parentEntity = _dbSet.Find(Id);
            if (parentEntity == null)
            {
                return null;
            }
            try
            {
                _context.Entry(parentEntity).CurrentValues.SetValues(entity);
                _context.Entry(parentEntity).State = EntityState.Modified;
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public T BorrarGenerico(T entity)
        {
            //_dbSet.Remove(entity);

            // GuardarAsync();
            return null;
        }

        public bool GuardarGenerico()
        {
            try
            {
                _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                //////Console.WriteLine(ex.Message);
                return false;
            }


        }

        bool IGenericoRepositorio<T>.AltaGenerico(int Id, Expression<Func<T, object>> campo, object nuevoValor)
        {
            var parentEntity = _dbSet.Find(Id);

            if (parentEntity == null)
            {
                throw new Exception("Entity not found");
            }

            // Safely extract the property info from the expression
            MemberExpression memberExpression;
            if (campo.Body is UnaryExpression unaryExpression)
            {
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else if (campo.Body is MemberExpression directMemberExpression)
            {
                memberExpression = directMemberExpression;
            }
            else
            {
                //throw new InvalidCastException("The expression is not a valid member expression.");
                return false;
            }

            if (memberExpression == null)
            {
                //throw new InvalidOperationException("The provided expression is not valid.");
                return false;
            }

            var propiedad = (PropertyInfo)memberExpression.Member;
            propiedad.SetValue(parentEntity, nuevoValor);

            // Mark the specific property as modified
            _context.Entry(parentEntity).Property(propiedad.Name).IsModified = true;

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }



        }
        bool IGenericoRepositorio<T>.BajaGenerico(int Id, Expression<Func<T, object>> campo, object nuevoValor)
        {
            var parentEntity = _dbSet.Find(Id);

            if (parentEntity == null)
            {
                throw new Exception("Entity not found");
            }

            // Safely extract the property info from the expression
            MemberExpression memberExpression;
            if (campo.Body is UnaryExpression unaryExpression)
            {
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else if (campo.Body is MemberExpression directMemberExpression)
            {
                memberExpression = directMemberExpression;
            }
            else
            {
                //throw new InvalidCastException("The expression is not a valid member expression.");
                return false;
            }

            if (memberExpression == null)
            {
                //throw new InvalidOperationException("The provided expression is not valid.");
                return false;
            }

            var propiedad = (PropertyInfo)memberExpression.Member;
            propiedad.SetValue(parentEntity, nuevoValor);

            // Mark the specific property as modified
            _context.Entry(parentEntity).Property(propiedad.Name).IsModified = true;

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }






        public async Task<T> obtenerPorIdGenerico(int Id)
        {
            return await _dbSet.FindAsync(Id);
        }



        public Task<List<T>> obtenerListaTodosGenerico()
        {
            return _dbSet.ToListAsync();
            //throw new NotImplementedException();
        }
    }
}
