using System;
using System.Threading;
using System.Threading.Tasks;
using TextCopy;

namespace AoC.Common
{
    public interface IOutput
    {
        Task WriteAsync(object output, CancellationToken cancellationToken = default);
    }

    public class ConsoleOutput : IOutput
    {
        public Task WriteAsync(object output, CancellationToken cancellationToken = default)
        {
            Console.WriteLine(output);
            return Task.CompletedTask;
        }
    }

    public class ClipboardOutput : IOutput
    {
        public async Task WriteAsync(object output, CancellationToken cancellationToken = default)
        {
            await ClipboardService.SetTextAsync(string.Empty).ConfigureAwait(false);
            await ClipboardService.SetTextAsync(output.ToString(), cancellationToken);
        }
    }
}
