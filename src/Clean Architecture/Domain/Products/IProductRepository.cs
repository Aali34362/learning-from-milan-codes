namespace Domain.Products;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(ProductId id);

    void Add(Product product);

    void Update(Product product);

    void Remove(Product product);
}