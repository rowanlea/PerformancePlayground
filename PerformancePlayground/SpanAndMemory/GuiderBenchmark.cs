using BenchmarkDotNet.Attributes;

namespace PerformancePlayground.SpanAndMemory
{
    [MemoryDiagnoser(false)]
    public class GuiderBenchmark
    {
        private static readonly Guid TestGuid = Guid.Parse("C7ECBD1D-0273-4E39-9913-EE97C41A5EF4");
        private const string TestString = "IdE4LgfmQU_B17r5K4R_kw";

        [Benchmark]
        public Guid ToGuidFromString()
        {
            return Guider.ToGuidFromStringBefore(TestString);
        }

        [Benchmark]
        public string ToStringFromGuid()
        {
            return Guider.ToStringFromGuidBefore(TestGuid);
        }
    }
}
