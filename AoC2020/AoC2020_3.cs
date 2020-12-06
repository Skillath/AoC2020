using Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AoC2020
{
    /*
     * https://adventofcode.com/2020/day/3
     */
    public class AoC2020_3 : PuzzleBase
    {
        private const char Param = '#';
        protected override int Day => 3;

        public override async Task<string> Resolve(string input, CancellationToken cancellationToken = default)
        {
            var inputData = (await InputLoader.Load(Day, cancellationToken).ConfigureAwait(false))
                .Split("\r\n");

            return $"Puzzle {Day}\n{PartOne(inputData)}\n{PartTwo(inputData)}";
        }

        private int PartOne(string[] input)
        {
            return Algorithm(input, (3, 1));
        }

        /**
        Right 1, down 1.
        Right 3, down 1. (This is the slope you already checked.)
        Right 5, down 1.
        Right 7, down 1.
        Right 1, down 2.
         */
        private long PartTwo(string[] input)
        {
            var velocities = new[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2), };

            return velocities
                .Select(velocity => (long)Algorithm(input, velocity))
                .Aggregate((left, right) => left * right);
        }

        private int Algorithm(string[] input, (int X, int Y) velocity)
        {
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