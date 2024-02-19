using BenchmarkDotNet.Attributes;
using System.Runtime.InteropServices;

namespace PerformancePlayground
{
    [MemoryDiagnoser]
    public class ListLoopingBenchmarks
    {
        public class Person
        {
            public int Id { get; set; } = 123;
            public string Name { get; set; } = "Name";
            public string Description { get; set; } = "Hello";
        }
        private const int _iterations = 10000;
        private const int _listSize = 10000;
        List<Person> data = new List<Person>();
        IReadOnlyList<Person> readonlyData = new List<Person>();

        [GlobalSetup]
        public void Setup()
        {
            data = Enumerable.Repeat(new Person(), _listSize).ToList();

            var tempData = Enumerable.Repeat(new Person(), _listSize).ToList();
            readonlyData = tempData;
        }

        [Benchmark]
        public void ExistingSolution()
        {
            for (int i = 0; i < _iterations; i++)
            {
                for (var j = 0; j < readonlyData.Count; j++)
                {
                    var data = readonlyData[j];
                    data.Name = "Jeff";
                }
            }
        }

        [Benchmark]
        public void ProposedSolution_IReadOnlyList()
        {
            for (int i = 0; i < _iterations; i++)
            {
                Span <Person> listAsSpan = CollectionsMarshal.AsSpan(readonlyData.ToList());
                for (var j = 0; j < listAsSpan.Length; j++)
                {
                    var data = listAsSpan[j];
                    data.Name = "Jeff";
                }
            }
        }

        [Benchmark]
        public void ProposedSolution_List()
        {
            for (int i = 0; i < _iterations; i++)
            {
                Span<Person> listAsSpan = CollectionsMarshal.AsSpan(data);
                for (var j = 0; j < listAsSpan.Length; j++)
                {
                    var data = listAsSpan[j];
                    data.Name = "Jeff";
                }
            }
        }
    }
}
