using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanSheet.Infrastructure.Persistence.Repositories;
public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task AddAsync(User user)
    {
        context.Add(user);
        await context.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email) =>
        await context.Users
            .SingleOrDefaultAsync(user => user.Email.Equals(email));
}
