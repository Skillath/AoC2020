using System.Threading;
using System.Threading.Tasks;
using AoC.Common.DataLoader;

namespace AoC.Common.Puzzle
{
    public abstract class PuzzleBase<TInputType, TOutputType> : IPuzzle
    {
        protected abstract int Day { get; }
        protected virtual IDataLoader DataLoader { get; set; } = new DataLoaderFromDisk();

        public async ValueTask<string> Resolve(CancellationToken cancellationToken = default)
        {
            var loadedData = await DataLoader.Load(Day, cancellationToken).ConfigureAwait(false);
            var parsedData = await ParseLoadedData(loadedData, cancellationToken).ConfigureAwait(false);

            var partOne = await FirstPart(parsedData, cancellationToken).ConfigureAwait(false);
            var partTwo = await SecondPart(parsedData, cancellationToken).ConfigureAwait(false);

            return $"{this}\n{partOne}\n{partTwo}";
        }

        protected abstract ValueTask<TInputType> ParseLoadedData(string loadedData, CancellationToken cancellationToken = default);

        protected abstract ValueTask<TOutputType> FirstPart(TInputType input, CancellationToken cancellationToken = default);

        protected abstract ValueTask<TOutputType> SecondPart(TInputType input, CancellationToken cancellationToken = default);

        public override string ToString() => $"Puzzle {Day}";
    }
}
