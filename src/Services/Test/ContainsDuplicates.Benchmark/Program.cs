using BenchmarkDotNet.Running;
using ContainsDuplicates.Benchmark;

BenchmarkRunner.Run<Benchmark>();

Console.ReadKey();
