using AgendaSaludApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AgendaSaludDBContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(AgendaSaludDBContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> ObtenerPorIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> ObtenerTodosAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> BuscarAsync(Expression<Func<T, bool>> filtro)
    {
        return await _dbSet.Where(filtro).ToListAsync();
    }

    public async Task<T> AltaAsync(T entity)
    {
        try
        {
            _dbSet.Add(entity);
            /// Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return entity;
        }
        catch
        {

            throw;
        }
    }


    public async Task<bool> ActualizarAsync(T entity)
    {
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> EliminarAsync(T entity)
    {
        try
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }


    public async Task<T?> ObtenerAsync(Expression<Func<T, bool>> filtro)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(filtro);

        return entity;
    }
    public IQueryable<T> Query(Expression<Func<T, bool>>? filtro = null)
    {
        IQueryable<T> query = _dbSet;

        if (filtro != null)
            query = query.Where(filtro);

        return query;
    }

}
