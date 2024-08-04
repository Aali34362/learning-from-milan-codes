using Domain.Entities;

namespace Domain.Repositories;

public interface IArticleRepository
{
    Task<Guid> InsertAsync(Article article);

    Task<Article?> GetByIdAsync(Guid id);

    Task UpdateAsync(Article article);
}
