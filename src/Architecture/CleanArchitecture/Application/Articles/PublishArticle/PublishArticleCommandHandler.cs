using Domain.Repositories;
using MediatR;

namespace Application.Articles.PublishArticle;

internal sealed class PublishArticleCommandHandler : IRequestHandler<PublishArticleCommand>
{
    private readonly IArticleRepository _articleRepository;

    public PublishArticleCommandHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task Handle(PublishArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.Id);

        if (article is null)
        {
            throw new ApplicationException("Article not found");
        }

        article.PublishedOnUtc = DateTime.UtcNow;

        await _articleRepository.UpdateAsync(article);
    }
}