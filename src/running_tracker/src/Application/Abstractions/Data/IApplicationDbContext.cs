using Domain.Followers;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }

    DbSet<Follower> Followers { get; set; }
}
