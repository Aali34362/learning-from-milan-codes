using SharedKernel;

namespace Domain.Workouts;

public sealed class Exercise : Entity
{
    public Exercise(Guid id, ExerciseType type, Distance distance)
        : base(id)
    {
        Type = type;
        Distance = distance;
    }

    public ExerciseType Type { get; private set; }

    public Distance Distance { get; private set; }
}
