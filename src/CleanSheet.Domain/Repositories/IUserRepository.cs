using CleanSheet.Domain.Entities;

namespace CleanSheet.Domain.Repositories;
public interface IUserRepository
{
    Task AddAsync(User user);
}
