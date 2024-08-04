using MediatR;

namespace Application.Articles.GetArticleById;

public sealed record GetArticleByIdQuery(Guid Id) : IRequest<ArticleResponse?>;
