namespace Api.Endpoints;

public class CreateArticleRequest
{
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public List<string> Tags { get; set; } = new();
}