using Domain.Products;
using Marten;
using Marten.Schema;

namespace WebApi.Seed;

public class ProductsData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        Product[] products = Enumerable
            .Range(1, 100_000)
            .Select(x => new Product
            {
                Name = $"Product {x}",
                Price = x,
            })
            .ToArray();

        await using IDocumentSession lightweightSession = store.LightweightSession();

        lightweightSession.Insert(products);

        await lightweightSession.SaveChangesAsync(cancellation);
    }
}
