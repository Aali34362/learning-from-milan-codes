using BenchmarkDotNet.Attributes;

namespace ContainsDuplicates.Benchmark;

[MemoryDiagnoser]
public class Benchmark
{
    private static int[] _collection;

    [Params(100, 1000, 10000)]
    public int Size { get; set; }

    [Params(0.3, 0.6, 0.8)]
    public double DuplicateLocation { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        _collection = Enumerable.Range(1, Size).ToArray();

        var index = (int)(DuplicateLocation * Size);

        _collection[index] = _collection[index + 1];
    }

    [Benchmark]
    public bool BruteForce()
    {
        return ContainsDuplicates.BruteForce(_collection);
    }

    [Benchmark]
    public bool ForEach()
    {
        return ContainsDuplicates.ForEach(_collection);
    }

    [Benchmark]
    public bool Any()
    {
        return ContainsDuplicates.Any(_collection);
    }

    [Benchmark]
    public bool All()
    {
        return ContainsDuplicates.All(_collection);
    }

    [Benchmark]
    public bool GroupBy()
    {
        return ContainsDuplicates.GroupBy(_collection);
    }

    [Benchmark]
    public bool Distinct()
    {
        return ContainsDuplicates.Distinct(_collection);
    }

    [Benchmark]
    public bool ToHashSet()
    {
        return ContainsDuplicates.ToHashSet(_collection);
    }

    [Benchmark]
    public bool NewHashSet()
    {
        return ContainsDuplicates.NewHashSet(_collection);
    }
}