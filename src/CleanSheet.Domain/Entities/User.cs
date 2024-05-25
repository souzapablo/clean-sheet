using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Primitives;

namespace CleanSheet.Domain.Entities;
public class User(string name, string email, string passwordHash, UserRole role) : Entity
{
    private readonly List<Career> _careers = [];

    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public string PasswordHash { get; private set; } = passwordHash;
    public UserRole Role { get; private set; } = role;
    public IReadOnlyList<Career> Careers => _careers;
}
