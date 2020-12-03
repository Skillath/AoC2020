using AoC.Common;
using Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AoC2020
{
    // https://adventofcode.com/2020/day/1
    public class AoC2020_1 : PuzzleBase
    {
        private const long Param = 2020L;
        protected override int Day => 1;

        public AoC2020_1()
        {
            InputLoader = new InputLoaderFromDisk();
        }

        public override async Task<string> Resolve(string input, CancellationToken cancellationToken = default)
        {
            var inputData = (await InputLoader.Load(Day, cancellationToken).ConfigureAwait(false))
                .Split('\n')
                .Select(int.Parse)
                .ToDictionary(key => (long)key, value => Param - value);

            var output = inputData.Where(valuePair => inputData.ContainsKey(valuePair.Value))
                .Select(i => i.Value)
                .Aggregate((left, right) => left * right);

            return output.ToString();
        }
    }
}
