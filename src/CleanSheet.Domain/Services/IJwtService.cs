using CleanSheet.Domain.Entities;

namespace CleanSheet.Domain.Services;
public interface IJwtService
{
    string Generate(User user);
}
