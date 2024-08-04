using Application.Articles.CreateArticle;
using Application.Articles.GetArticleById;
using Application.Articles.PublishArticle;
using MediatR;

namespace Api.Endpoints;

public static class ArticleEndpoints
{
    public static void MapArticleEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/articles", async (CreateArticleRequest request, ISender sender) =>
        {
            var articleId = await sender.Send(
                new CreateArticleCommand(request.Title, request.Content, request.Tags));

            return Results.Ok(articleId);
        });

        app.MapPut("api/articles/{id}", async (Guid id, ISender sender) =>
        {
            await sender.Send(new PublishArticleCommand(id));

            return Results.NoContent();
        });

        app.MapGet("api/articles/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetArticleByIdQuery(id);

            var article = await sender.Send(query);

            return article is null ? Results.NotFound() : Results.Ok(article);
        });
    }
}
