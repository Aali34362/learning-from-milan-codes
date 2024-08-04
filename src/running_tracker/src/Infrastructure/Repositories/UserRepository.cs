using Domain.Users;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<bool> IsEmailUniqueAsync(Email email)
    {
        return !await _dbContext.Users.AnyAsync(u => u.Email == email);
    }

    public void Insert(User user)
    {
        _dbContext.Users.Add(user);
    }
}
