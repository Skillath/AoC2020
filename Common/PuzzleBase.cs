using System.Threading;
using System.Threading.Tasks;
using AoC.Common;

namespace Common
{
    public interface IPuzzle
    {
        Task<string> Resolve(string input, CancellationToken cancellationToken = default);
    }

    public abstract class PuzzleBase : IPuzzle
    {
        protected abstract int Day { get; }
        protected IInputLoader InputLoader { get; set; } = new InputLoaderFromDisk();

        public abstract Task<string> Resolve(string input, CancellationToken cancellationToken = default);

        public override string ToString()
        {
            return $"Puzzle {Day}";
        }
    }
}
