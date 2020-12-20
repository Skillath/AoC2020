using System.Threading;
using System.Threading.Tasks;

namespace AoC.Common.Output
{
    public interface IOutput
    {
        ValueTask WriteAsync(object output, CancellationToken cancellationToken = default);
    }
}