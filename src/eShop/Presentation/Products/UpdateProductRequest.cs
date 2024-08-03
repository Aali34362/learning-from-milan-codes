namespace Presentation.Products;

public sealed record UpdateProductRequest(
    string Name,
    decimal Price,
    List<string> Tags);
