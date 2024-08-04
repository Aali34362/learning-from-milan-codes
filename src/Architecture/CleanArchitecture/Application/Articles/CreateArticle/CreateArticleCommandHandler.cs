using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Articles.CreateArticle;

internal sealed class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Guid>
{
    private readonly IArticleRepository _articleRepository;

    public CreateArticleCommandHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<Guid> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = new Article
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Content = request.Content,
            Tags = request.Tags,
            CreatedOnUtc = DateTime.UtcNow
        };

        var articleId = await _articleRepository.InsertAsync(article);

        return articleId;
    }
}