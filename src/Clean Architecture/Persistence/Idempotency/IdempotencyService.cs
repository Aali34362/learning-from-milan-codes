using Application.Abstractions.Idempotency;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Idempotency;

internal sealed class IdempotencyService : IIdempotencyService
{
    private readonly ApplicationDbContext _context;

    public IdempotencyService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> RequestExistsAsync(Guid requestId)
    {
        return await _context.Set<IdempotentRequest>().AnyAsync(r => r.Id == requestId);
    }

    public async Task CreateRequestAsync(Guid requestId, string name)
    {
        var idempotentRequest = new IdempotentRequest
        {
            Id = requestId,
            Name = name,
            CreatedOnUtc = DateTime.UtcNow
        };

        _context.Add(idempotentRequest);

        await _context.SaveChangesAsync();
    }
}
