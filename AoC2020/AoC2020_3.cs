using System;
using System.Collections.Generic;
using Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AoC2020
{
    /*
     * https://adventofcode.com/2020/day/3
     */
    public class AoC2020_3 : PuzzleBase<IEnumerable<string>, long>
    {
        private const char Param = '#';
        protected override int Day => 3;

        protected override async ValueTask<IEnumerable<string>> ParseLoadedData(string loadedData, CancellationToken cancellationToken = default)
        {
            return loadedData.Split(Environment.NewLine);
        }

        protected override async ValueTask<long> FirstPart(IEnumerable<string> input, CancellationToken cancellationToken = default)
        {
            return await Algorithm(input, (3, 1), cancellationToken);
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