using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using EfCore.CompiledQuery.Benchmark;

BenchmarkRunner.Run<EfCompiledQueryBenchmark>(
    DefaultConfig.Instance.AddJob(
        Job.Default.WithToolchain(
            new InProcessEmitToolchain(
                TimeSpan.FromSeconds(30),
                false))));

Console.ReadKey();
