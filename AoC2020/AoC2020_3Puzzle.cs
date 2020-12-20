using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AoC.Common.Puzzle;

namespace AoC2020
{
    /*
     * https://adventofcode.com/2020/day/3
     */
    public class AoC2020_3Puzzle : PuzzleBase<IEnumerable<string>, long>
    {
        private const char Param = '#';
        protected override int Day => 3;

        protected override ValueTask<IEnumerable<string>> ParseLoadedData(string loadedData, CancellationToken cancellationToken = default)
        {
            return new ValueTask<IEnumerable<string>>(loadedData.Split(Environment.NewLine));
        }

        protected override ValueTask<long> FirstPart(IEnumerable<string> input, CancellationToken cancellationToken = default)
        {
            return Algorithm(input, (3, 1), cancellationToken);
        }

        protected override async ValueTask<long> SecondPart(IEnumerable<string> input, CancellationToken cancellationToken = default)
        {
            var velocities = (new[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2), }).ToAsyncEnumerable();

            return await velocities
                .SelectAwait(velocity => Algorithm(input, velocity, cancellationToken))
                .AggregateAsync((left, right) => left * right, cancellationToken);
        }

        private static async ValueTask<long> Algorithm(IEnumerable<string> data, (int X, int Y) velocity, CancellationToken cancellationToken = default)
        {
            var input = await data.ToAsyncEnumerable()
                .ToArrayAsync(cancellationToken);

            var count = 0;
            var treeCount = 0;

            for (var i = velocity.Y; i < input.Length; i += velocity.Y)
            {
                count += velocity.X;

                var row = input[i];

                if (row[count % row.Length] == Param)
                    treeCount++;
            }

            return treeCount;
        }
    }

}