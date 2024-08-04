namespace Domain.Products;

// Stock Keeping Unit
public record Sku
{
    private const int DefaultLength = 8;

    private Sku(string value) => Value = value;

    public string Value { get; init; }

    public static explicit operator string(Sku sku) => sku.Value;

    public static Sku? Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        if (value.Length != DefaultLength)
        {
            return null;
        }

        return new Sku(value);
    }
}