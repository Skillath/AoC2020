using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AoC.Common.Puzzle;

namespace AoC2020
{
    public class AoC2020_6Puzzle : PuzzleBase<IEnumerable<string>, int>
    {
        protected override int Day => 6;


        protected override async ValueTask<IEnumerable<string>> ParseLoadedData(string loadedData, CancellationToken cancellationToken = default)
        {
            return loadedData.Split(Environment.NewLine + Environment.NewLine);
        }

        protected override async ValueTask<int> FirstPart(IEnumerable<string> input, CancellationToken cancellationToken = default)
        {
            var value = await input.ToAsyncEnumerable()
                .SumAsync(group => group
                    .Replace(Environment.NewLine, "")
                    .Distinct()
                    .Count(), cancellationToken);

            return value;
        }

        protected override async ValueTask<int> SecondPart(IEnumerable<string> input, CancellationToken cancellationToken = default)
        {
            var value = await input.ToAsyncEnumerable()
                .SumAsync(group => group
                    .Split(Environment.NewLine)
                    .Aggregate((left, right) => new string(left.Intersect(right).ToArray()))
                    .Count(), cancellationToken);

            return value;
        }
    }
}