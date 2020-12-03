﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AoC.Common;
using Common;

namespace AoC2020
{
    public class Program
    {
        private static readonly IEnumerable<IPuzzle> _puzzle;
        private static readonly IEnumerable<IOutput> _outputs;

        static Program()
        {
            _puzzle = new[]
            {
                new AoC2020_1(),
            };

            _outputs = new[]
            {
                (IOutput)new ConsoleOutput(),
                (IOutput)new ClipboardOutput(),
            };
        }

        static async Task Main(string[] args)
        {
            foreach (var puzzle in _puzzle)
            {
                await WriteOutput(await puzzle.Resolve(""));
            }
        }

        private static async Task WriteOutput(object outputData, CancellationToken cancellationToken = default)
        {
            await Task.WhenAll(_outputs.Select(a => a.WriteAsync(outputData, cancellationToken)));
        }
    }
}