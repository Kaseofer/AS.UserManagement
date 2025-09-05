using System.Linq.Expressions;

public interface IGenericRepository<T> where T : class
{
    Task<T?> ObtenerPorIdAsync(int id);
    Task<IEnumerable<T>> ObtenerTodosAsync();
    Task<IEnumerable<T>> BuscarAsync(Expression<Func<T, bool>> filtro);
    Task<T?> ObtenerAsync(Expression<Func<T, bool>> filtro);
    Task<T> AltaAsync(T entity);
    Task<bool> ActualizarAsync(T entity);
    Task<bool> EliminarAsync(T entity);
}
