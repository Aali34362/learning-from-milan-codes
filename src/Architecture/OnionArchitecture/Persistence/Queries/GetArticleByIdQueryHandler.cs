using Application.Queries;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Queries;
public sealed class GetArticleByIdQueryHandler : IGetArticleByIdQueryHandler
{
    private readonly ApplicationDbContext _dbContext;

    public GetArticleByIdQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ArticleResponse?> Handle(Guid id)
    {
        var articleResponse = await _dbContext
            .Articles
            .AsNoTracking()
            .Where(article => article.Id == id)
            .Select(article => new ArticleResponse
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                Tags = article.Tags,
                CreatedOnUtc = article.CreatedOnUtc,
                PublishedOnUtc = article.PublishedOnUtc
            })
            .FirstOrDefaultAsync();

        return articleResponse;
    }
}
