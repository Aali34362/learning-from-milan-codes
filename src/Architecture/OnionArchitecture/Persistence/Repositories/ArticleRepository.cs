using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories;

public sealed class ArticleRepository : IArticleRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ArticleRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> InsertAsync(Article article)
    {
        _dbContext.Articles.Add(article);

        await _dbContext.SaveChangesAsync();

        return article.Id;
    }

    public async Task<Article?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Articles.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task UpdateAsync(Article article)
    {
        _dbContext.Update(article);

        await _dbContext.SaveChangesAsync();
    }
}
