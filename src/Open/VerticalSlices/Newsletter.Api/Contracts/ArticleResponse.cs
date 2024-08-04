namespace Newsletter.Api.Contracts;

public class ArticleResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string? Preview { get; set; }

    public List<string> Tags { get; set; } = new();

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? PublishedOnUtc { get; set; }
}