using Franco.Core.Model;

namespace Franco.Core.Interface;

public interface IRepository<T> where T : BaseModel
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellation);
    
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
}