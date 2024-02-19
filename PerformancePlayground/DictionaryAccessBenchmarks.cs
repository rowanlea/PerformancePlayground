using BenchmarkDotNet.Attributes;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PerformancePlayground
{
    [MemoryDiagnoser]
    public class DictionaryAccessBenchmarks
    {
        public class Thing
        {
            public int Id { get; set; } = 123;
            public string Name { get; set; } = "Name";
            public string Description { get; set; } = "Hello";
        }
        private const int _iterations = 100000000;
        Dictionary<string, Thing> data = new Dictionary<string, Thing>();
        IReadOnlyDictionary<string, Thing> readonlyData = new Dictionary<string, Thing>();

        [GlobalSetup]
        public void Setup()
        {
            data.Add("a", new Thing());
            data.Add("b", new Thing());
            data.Add("c", new Thing());
            data.Add("d", new Thing());
            data.Add("e", new Thing());
            
            var tempDictionary = new Dictionary<string, Thing>
            {
                { "a", new Thing() },
                { "b", new Thing() },
                { "c", new Thing() },
                { "d", new Thing() },
                { "e", new Thing() }
            };
            readonlyData = tempDictionary;
        }

        [Benchmark]
        public void ExistingSolution()
        {
            for (int i = 0; i < _iterations; i++)
            {
                if(readonlyData.TryGetValue("d", out var thing))
                {
                    thing.Name = "Jeff";
                }
            }
        }

        [Benchmark]
        public void ProposedSolution_IReadOnlyDictionary()
        {
            for (int i = 0; i < _iterations; i++)
            {
                ref var thing = ref CollectionsMarshal.GetValueRefOrNullRef((Dictionary<string, Thing>)readonlyData, "d");
                if (!Unsafe.IsNullRef(ref thing))
                {
                    thing.Name = "Jeff";
                }
            }
        }

        [Benchmark]
        public void ProposedSolution_Dictionary()
        {
            for (int i = 0; i < _iterations; i++)
            {
                ref var thing = ref CollectionsMarshal.GetValueRefOrNullRef(data, "d");
                if (!Unsafe.IsNullRef(ref thing))
                {
                    thing.Name = "Jeff";
                }
            }
        }
    }
}
