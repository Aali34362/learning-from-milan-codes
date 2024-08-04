namespace Application.Queries;

public interface IGetArticleByIdQueryHandler
{
    Task<ArticleResponse?> Handle(Guid id);
}
