namespace Domain.Abstractions;

public interface IAuditable
{
    DateTime CreatedOnUtc { get; set; }

    DateTime? ModifiedOnUtc { get; set; }
}
