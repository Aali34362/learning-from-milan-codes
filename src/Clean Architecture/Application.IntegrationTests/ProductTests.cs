using Application.Products.Create;
using Application.Products.GetById;
using Domain.Products;

namespace Application.IntegrationTests;

public class ProductTests : BaseIntegrationTest
{
    public ProductTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Create_ShouldThrowArgumentException_WhenSkuIsInvalid()
    {
        // Arrange
        var command = new CreateProductCommand("Database", "123", "USD", 99.99m);

        // Act
        Task Action() => Sender.Send(command);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(Action);
    }

    [Fact]
    public async Task Create_ShouldAddProduct_WhenCommandIsValid()
    {
        // Arrange
        var command = new CreateProductCommand("Database", "12312312", "USD", 99.99m);

        // Act
        var productId = await Sender.Send(command);

        // Assert
        var product = DbContext.Products.FirstOrDefault(p => p.Id == new ProductId(productId));

        Assert.NotNull(product);
    }

    [Fact]
    public async Task GetById_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var command = new CreateProductCommand("Database", "12312312", "USD", 99.99m);
        var productId = await Sender.Send(command);
        var query = new GetProductQuery(new ProductId(productId));

        // Act
        var product = await Sender.Send(query);

        // Assert
        Assert.NotNull(product);
    }
}