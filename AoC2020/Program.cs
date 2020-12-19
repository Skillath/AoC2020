using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AoC.Common;
using Common;

namespace AoC2020
{
    public class Program
    {
        private static readonly string Path = @"./Result/";
        private static readonly string FileName = @"result.txt";

        private static readonly IAsyncEnumerable<IPuzzle> _puzzles;
        private static readonly IAsyncEnumerable<IOutput> _outputs;

        private static readonly CancellationTokenSource _cancellationTokenSource;

        static Program()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _puzzles = (new[]
            {
                (IPuzzle)new AoC2020_1(),
                (IPuzzle)new AoC2020_2(),
                (IPuzzle)new AoC2020_3(),
                (IPuzzle)new AoC2020_4(),
            }).ToAsyncEnumerable();

            _outputs = new[]
            {
                (IOutput)new ConsoleOutput(),
                (IOutput)new FileOutput(Path, FileName),
                //(IOutput)new ClipboardOutput(),
            }.ToAsyncEnumerable();
        }

        private static async Task Main(string[] args)
        {
            var ct = _cancellationTokenSource.Token;

            await foreach (var puzzle in _puzzles.WithCancellation(ct))
            {
                await WriteOutput(await puzzle.Resolve(ct) + "\n", ct);
            }
        }

        private static async ValueTask WriteOutput(object outputData, CancellationToken cancellationToken = default)
        {
            await foreach (var output in _outputs.WithCancellation(cancellationToken))
            {
                await output.WriteAsync(outputData, cancellationToken);
            }
        }
    }
}