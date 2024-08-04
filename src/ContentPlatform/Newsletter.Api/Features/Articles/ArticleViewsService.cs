using Contracts;
using MassTransit;

namespace Newsletter.Api.Features.Articles;

public class ArticleViewsService
{
    private readonly IRequestClient<GetArticleViewsRequest> _client;
    private readonly ILogger<ArticleViewsService> _logger;

    public ArticleViewsService(IRequestClient<GetArticleViewsRequest> client, ILogger<ArticleViewsService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<long> GetViewsAsync(Guid id)
    {
        var response = await _client.GetResponse<GetArticleViewsResponse, ArticleNotFoundResponse>(
            new GetArticleViewsRequest { Id = id });

        if (response.Is(out Response<GetArticleViewsResponse> viewsResponse))
        {
            return viewsResponse.Message.Views;
        }

        if (response.Is(out Response<ArticleNotFoundResponse> notFoundResponse))
        {
            _logger.LogWarning("Article not found - {Id}", id);
        }

        return 0;
    }
}
