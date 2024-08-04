using SharedKernel;

namespace Domain.Workouts;

public sealed record Distance
{
    private const decimal OneKilometer = 1000.0m;

    public Distance(decimal meters)
    {
        Ensure.GreaterThanZero(meters);

        Meters = meters;
    }

    public decimal Meters { get; private set; }

    public decimal Kilometers => Meters / OneKilometer;
}
