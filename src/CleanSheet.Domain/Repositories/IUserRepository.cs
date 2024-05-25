using CleanSheet.Domain.Entities;

namespace CleanSheet.Domain.Repositories;
public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
}
