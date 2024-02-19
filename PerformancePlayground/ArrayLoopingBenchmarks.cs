using BenchmarkDotNet.Attributes;

namespace PerformancePlayground
{
    [MemoryDiagnoser]
    public class ArrayLoopingBenchmarks
    {
        public class Person
        {
            public int Id { get; set; } = 123;
            public string Name { get; set; } = "Name";
            public string Description { get; set; } = "Hello";
        }
        private const int _iterations = 10000;
        private const int _arraySize = 10000;
        Person[] data;

        [GlobalSetup]
        public void Setup()
        {
            data = Enumerable.Repeat(new Person(), _arraySize).ToArray();
        }

        [Benchmark]
        public void ExistingSolution()
        {
            for (int i = 0; i < _iterations; i++)
            {
                for (var j = 0; j < data.Length; j++)
                {
                    var person = data[j];
                    person.Name = "Jeff";
                }
            }
        }

        [Benchmark]
        public void ProposedSolution()
        {
            for (int i = 0; i < _iterations; i++)
            {
                Span<Person> arrayAsSpan = data;
                for (var j = 0; j < arrayAsSpan.Length; j++)
                {
                    var person = arrayAsSpan[j];
                    person.Name = "Jeff";
                }
            }
        }
    }
}
