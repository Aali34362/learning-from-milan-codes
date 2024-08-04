using Application.Queries;
using Application.Services;

namespace Api.Endpoints;

public static class ArticleEndpoints
{
    public static void MapArticleEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/articles",async (CreateArticleRequest request, ArticleService articleService) =>
        {
            var articleId = await articleService.CreateAsync(
                request.Title,
                request.Content,
                request.Tags);

            return Results.Ok(articleId);
        });

        app.MapPut("api/articles/{id}", async (Guid id, ArticleService articleService) =>
        {
            await articleService.PublishAsync(id);

            return Results.NoContent();
        });

        app.MapGet("api/articles/{id}", async (Guid id, IGetArticleByIdQueryHandler handler) =>
        {
            var article = await handler.Handle(id);

            return article is null ? Results.NotFound() : Results.Ok(article);
        });
    }
}
