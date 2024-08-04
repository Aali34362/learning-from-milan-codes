using MediatR;

namespace Application.Articles.CreateArticle;

public sealed record CreateArticleCommand(string Title, string Content, List<string> Tags)
    : IRequest<Guid>;