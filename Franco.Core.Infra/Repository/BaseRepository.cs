using Microsoft.EntityFrameworkCore;
using Franco.Core.Interface;
using Franco.Core.Model;

namespace Franco.Core.Infra.Repository;

public class BaseRepository<T, TContext> : IRepository<T>
    where T : BaseModel
    where TContext : DbContext
{
    protected readonly TContext _context;
    protected readonly DbSet<T> _dbSet;
    
    private Func<DbSet<T>, IQueryable<T>> _includeBuilder;
    
    public BaseRepository(TContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
        _includeBuilder = x => x;
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await ApplyIncludes().Where(x => x.Id == id).FirstAsync(cancellationToken);
    }
    
    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await ApplyIncludes().ToListAsync(cancellationToken);
    }
    
    protected IQueryable<T> ApplyIncludes()
    {
        var query = _includeBuilder(_dbSet);
        _includeBuilder = q => q;
        
        return query;
    }
}