using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AoC.Common.Puzzle;

namespace AoC2020
{
    // https://adventofcode.com/2020/day/1
    public class AoC2020_1Puzzle : PuzzleBase<IEnumerable<long>, long>
    {
        private const long Param = 2020L;
        protected override int Day => 1;

        protected override async ValueTask<IEnumerable<long>> ParseLoadedData(string loadedData, CancellationToken cancellationToken = default)
        {
            var result = loadedData.Split('\n')
                .Select(long.Parse);

            return result;
        }

        protected override async ValueTask<long> FirstPart(IEnumerable<long> data, CancellationToken cancellationToken = default)
        {
            var input = data.ToAsyncEnumerable();
            var diff = await input.FirstOrDefaultAwaitWithCancellationAsync((d, ct) => input.ContainsAsync(Param - d, ct), cancellationToken);

            return diff * (Param - diff);
        }

        protected override async ValueTask<long> SecondPart(IEnumerable<long> data, CancellationToken cancellationToken = default)
        {
            var input = data.ToArray();

            for (var x = 0; x < input.Length - 1; x++)
            {
                for (var y = 1; y < input.Length; y++)
                {
                    var first = input[x];
                    var second = input[y];

                    var diff = Param - (first + second);

                    if (input.Except(new[] { first, second }).Contains(diff))
                        return diff * first * second;
                }
            }

            throw new Exception("Not Found");
        }
    }
}
