namespace Contracts;

public record GetArticleViewsRequest
{
    public Guid Id { get; set; }
}

public record GetArticleViewsResponse
{
    public Guid Id { get; set; }

    public long Views { get; set; }
}

public record ArticleNotFoundResponse
{
    public Guid Id { get; set; }
}
