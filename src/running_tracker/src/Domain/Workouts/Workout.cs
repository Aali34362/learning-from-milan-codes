using Domain.Users;
using SharedKernel;

namespace Domain.Workouts;

public sealed class Workout : Entity
{
    private readonly List<Exercise> _exercises = new();

    public Workout(Guid id, Guid userId, Name name)
        : base(id)
    {
        UserId = userId;
        Name = name;
    }

    private Workout()
    {
    }

    public Guid UserId { get; private set; }

    public Name Name { get; private set; }

    public List<Exercise> Exercises => _exercises.ToList();

    public decimal TotalKilometers => _exercises.Sum(exercise => exercise.Distance.Kilometers);

    public void AddExercise(ExerciseType type, decimal distanceInMeters)
    {
        var exercise = new Exercise(Guid.NewGuid(), type, new Distance(distanceInMeters));

        _exercises.Add(exercise);
    }

    public void RemoveExercise(Exercise exercise)
    {
        _exercises.Remove(exercise);
    }
}
