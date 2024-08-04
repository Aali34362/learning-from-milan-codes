namespace Application.Products.GetProducts;

public sealed record CursorResponse<T>(
    long Cursor,
    T Data);
