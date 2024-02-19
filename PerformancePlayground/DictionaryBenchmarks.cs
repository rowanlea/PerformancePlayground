using BenchmarkDotNet.Attributes;

namespace PerformancePlayground
{
    [MemoryDiagnoser]
    public class DictionaryBenchmarks
    {
        Dictionary<string, string> d = new Dictionary<string, string> { { "a", "b" }, { "c", "d" }, { "e", "f" }, { "g", "h" } };

        //[Benchmark]
        public void TryGetValue()
        {
            for (int i = 0; i != 10000000; i++)
            {
                string x;
                if (!d.TryGetValue("a", out x)) throw new ApplicationException("Oops");
            }
        }

        //[Benchmark]
        public void TryGetValueGlobalDeclare()
        {
            string x;
            for (int i = 0; i != 10000000; i++)
            {
                if (!d.TryGetValue("a", out x)) throw new ApplicationException("Oops");
            }
        }

        [Benchmark]
        public void TryGetValueDeclareOutVar()
        {
            for (int i = 0; i != 10000000; i++)
            {
                if (!d.TryGetValue("a", out var x)) throw new ApplicationException("Oops");
            }
        }

        //[Benchmark]
        public void ContainsValue()
        {
            for (int i = 0; i != 10000000; i++)
            {
                string x;
                if (d.ContainsKey("a"))
                {
                    x = d["a"];
                }
            }
        }

        //[Benchmark]
        public void ContainsValueGlobalDeclare()
        {
            string x;
            for (int i = 0; i != 10000000; i++)
            {
                if (d.ContainsKey("a"))
                {
                    x = d["a"];
                }
            }
        }

        //[Benchmark]
        public void GetValue()
        {
            for (int i = 0; i != 10000000; i++)
            {
                string x;
                x = d["a"];
            }
        }

        //[Benchmark]
        public void GetValueSameLine()
        {
            for (int i = 0; i != 10000000; i++)
            {
                string x = d["a"];
            }
        }

        [Benchmark]
        public void GetValueGlobalDeclare()
        {
            string x;
            for (int i = 0; i != 10000000; i++)
            {
                x = d["a"];
            }
        }

        [Benchmark]
        public void TryGetValueDeclareOutVarNoIf()
        {
            for (int i = 0; i != 10000000; i++)
            {
                d.TryGetValue("a", out var x);
            }
        }
    }
}
