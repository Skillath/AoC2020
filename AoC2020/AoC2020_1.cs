using Common;
using System;
using System.Collections.Generic;
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

        public override async Task<string> Resolve(string input, CancellationToken cancellationToken = default)
        {
            var inputData = (await InputLoader.Load(Day, cancellationToken).ConfigureAwait(false))
                .Split('\n')
                .Select(long.Parse)
                .ToArray();

            var partOne = PartOne(inputData);
            var partTwo = PartTwo(inputData);

            return $"Puzzle {Day}\n{partOne}\n{partTwo}";
        }

        private long PartOne(IEnumerable<long> data)
        {
            var input = data.ToArray();
            var diff = input.FirstOrDefault(d => input.Contains(Param - d));

            return diff * (Param - diff);
        }

        private long PartTwo(IEnumerable<long> data)
        {
            var input = data.ToArray();

            for (int x = 0; x < input.Length - 1; x++)
            {
                for (int y = 1; y < input.Length; y++)
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
