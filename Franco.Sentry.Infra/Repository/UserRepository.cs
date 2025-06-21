using Franco.Sentry.Domain.Model;
using Franco.Sentry.Infra.Context;
using Franco.Core.Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Franco.Sentry.Infra.Repository;

public class UserRepository : BaseRepository<User, SentryContext>
{
    public UserRepository(SentryContext context) : base(context) {}
    
    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.User.FirstOrDefaultAsync(x => x.Username == username && x.Status);
    }
}