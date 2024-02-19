using BenchmarkDotNet.Running;
using PerformancePlayground;

BenchmarkRunner.Run<LookupAccessBenchmarks>();

namespace PerformancePlayground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}