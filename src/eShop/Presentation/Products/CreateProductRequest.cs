namespace Presentation.Products;

public sealed record CreateProductRequest(
    string Name,
    decimal Price,
    List<string> Tags);
