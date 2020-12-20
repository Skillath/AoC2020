using System.Threading;
using System.Threading.Tasks;
using TextCopy;

namespace AoC.Common.Output
{
    public class ClipboardOutput : IOutput
    {
        public async ValueTask WriteAsync(object output, CancellationToken cancellationToken = default)
        {
            await ClipboardService.SetTextAsync(string.Empty, cancellationToken).ConfigureAwait(false);
            await ClipboardService.SetTextAsync(output.ToString(), cancellationToken);
        }
    }
}