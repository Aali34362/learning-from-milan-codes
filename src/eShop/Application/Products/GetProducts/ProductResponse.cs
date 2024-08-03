namespace Application.Products.GetProducts;

public sealed record ProductResponse(
    long Id,
    string Name,
    decimal Price,
    List<string> Tags);
