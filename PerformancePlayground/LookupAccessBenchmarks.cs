using BenchmarkDotNet.Attributes;
using System.Runtime.InteropServices;

namespace PerformancePlayground
{
    [MemoryDiagnoser]
    public class LookupAccessBenchmarks
    {
        public class Thing
        {
            public int Id { get; set; } = 123;
            public string Name { get; set; } = "Name";
            public string Description { get; set; } = "Hello";
        }

        public struct ThingHolder
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        private const int _iterations = 10000000;
        ILookup<int, Thing> lookupData;

        [GlobalSetup]
        public void Setup()
        {
            Thing[] tempData = {
                new Thing() { Id = 1 },
                new Thing() { Id = 2 },
                new Thing() { Id = 3 },
                new Thing() { Id = 6 },
                new Thing() { Id = 5 },
                new Thing() { Id = 1 },
                new Thing() { Id = 2 },
                new Thing() { Id = 3 },
                new Thing() { Id = 4 },
                new Thing() { Id = 4 },
                new Thing() { Id = 4 },
                new Thing() { Id = 4 },
                new Thing() { Id = 4 },
                new Thing() { Id = 4 },
                new Thing() { Id = 5 }
            };
            lookupData = tempData.ToLookup(item => item.Id);
        }

        [Benchmark]
        public void ExistingSolution()
        {
            for (int i = 0; i < _iterations; i++)
            {

                foreach (var data in lookupData[4])
                {
                    data.Name = "Jeff";
                }
            }
        }

        [Benchmark]
        public void ProposedSolution_Array()
        {
            for (int i = 0; i < _iterations; i++)
            {
                var data = lookupData[4].ToArray();
                for (int j = 0; j < data.Length; j++)
                {
                    data[j].Name = "jeff";
                }
            }
        }

        [Benchmark]
        public void ProposedSolution_Array_Span()
        {
            for (int i = 0; i < _iterations; i++)
            {
                Span<Thing> arrayAsSpan = lookupData[4].ToArray();
                for (int j = 0; j < arrayAsSpan.Length; j++)
                {
                    arrayAsSpan[j].Name = "jeff";
                }
            }
        }

        //[Benchmark]
        public void ProposedSolution_Array_SpanOfStructs()
        {
            for (int i = 0; i < _iterations; i++)
            {
                var structData = from item in lookupData[4] select new ThingHolder { Id = item.Id, Name = item.Name, Description = item.Description };
                Span<ThingHolder> arrayAsSpan = structData.ToArray();
                for (int j = 0; j < arrayAsSpan.Length; j++)
                {
                    arrayAsSpan[j].Name = "jeff";
                }
            }
        }
    }
}
