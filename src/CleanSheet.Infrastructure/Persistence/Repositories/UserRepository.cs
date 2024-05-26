using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanSheet.Infrastructure.Persistence.Repositories;
public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        context.Add(user);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) =>
        await context.Users
            .SingleOrDefaultAsync(user => user.Email.Equals(email), cancellationToken);

    public async Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken = default) =>
        await context.Users
            .Include(user => user.Careers)
            .ThenInclude(career => career.Teams)
            .SingleOrDefaultAsync(user => user.Id == id, cancellationToken);
}
