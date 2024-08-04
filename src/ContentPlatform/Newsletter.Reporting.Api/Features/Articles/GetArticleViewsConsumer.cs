using Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newsletter.Reporting.Api.Database;
using Newsletter.Reporting.Api.Entities;

namespace Newsletter.Reporting.Api.Features.Articles;

public class GetArticleViewsConsumer : IConsumer<GetArticleViewsRequest>
{
    private readonly ApplicationDbContext _dbContext;

    public GetArticleViewsConsumer(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<GetArticleViewsRequest> context)
    {
        if (!await _dbContext.Articles.AnyAsync(a => a.Id == context.Message.Id))
        {
            await context.RespondAsync(new ArticleNotFoundResponse { Id = context.Message.Id });

            return;
        }

        var views = await _dbContext.ArticleEvents
            .Where(e => e.EventType == ArticleEventType.View &&
                        e.ArticleId == context.Message.Id)
            .CountAsync();

        var response = new GetArticleViewsResponse
        {
            Id = context.Message.Id,
            Views = views
        };

        await context.RespondAsync(response);
    }
}
