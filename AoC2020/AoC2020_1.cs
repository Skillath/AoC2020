using System;
using AoC.Common;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2020
{
    // https://adventofcode.com/2020/day/1
    class AoC2020_1
    {
        private const int CurrentDay = 1;
        private const long Param = 2020L;

        private static IInputLoader _loader;

        static async Task Main(string[] args)
        {
            _loader = new InputLoaderFromDisk();

            var input = (await _loader.Load(CurrentDay))
                .Split('\n')
                .Select(int.Parse)
                .ToDictionary(key => (long)key, value => Param - value);

            var output = input.Where(valuePair => input.ContainsKey(valuePair.Value))
                .Select(i => i.Value)
                .Aggregate((left, right) => left * right);


            Console.WriteLine(output);
        }
    }
}
