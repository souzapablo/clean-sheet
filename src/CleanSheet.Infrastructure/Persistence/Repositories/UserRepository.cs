using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;

namespace CleanSheet.Infrastructure.Persistence.Repositories;
public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task AddAsync(User user)
    {
        context.Add(user);
        await context.SaveChangesAsync();
    }
}
